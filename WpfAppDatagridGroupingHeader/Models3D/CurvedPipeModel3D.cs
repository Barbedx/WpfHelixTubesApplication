using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using WpfAppDatagridGroupingHeader.Extensions;
using WpfAppDatagridGroupingHeader.Helpers;
using WpfAppDatagridGroupingHeader.Models;
using geo = GeometRi;

namespace WpfAppDatagridGroupingHeader.Models3D
{
    internal class CurvedPipeModel3D : PipeModel3D<CurvedItemModel>
    { 
 
        public CurvedPipeModel3D(CurvedItemModel model) : base(model)
        { 
           
        }

        private Vector3D StartDirection => InnerModel.StartDirection;


        private Vector3D EndDirection => InnerModel.EndDirection;


        private double Length => InnerModel.Length;

        private Point3D QuadraticCurvedPosition { get; set; }

        //private Bezier Bezier { get; set; }
        public override void AppearanceChanged([CallerMemberName] string caller = null)
        { 
            var sd = StartDirection.GetNormalized(); //Нормализированые векторы 
            var ed = EndDirection.GetNormalized(); 
            var vector = sd + ed;
            vector *= Length; //Вектор хорды
            var ep = StartPosition + vector; //конечная точка 
            
            var l = new geo.Line3d(StartPosition.ToGeometRIPoint3D(), sd.ToGeometRIVector3D());
            var l2 = new geo.Line3d(ep.ToGeometRIPoint3D(), ed.ToGeometRIVector3D());
            var intersectionPoint = l.IntersectionWith(l2); //Точка пересечений линий

            if (intersectionPoint is geo.Point3d interPoint)
            {
               QuadraticCurvedPosition= interPoint.ToPoint3D();

                var bezier = new Bezier(StartPosition, QuadraticCurvedPosition, ep, 100); 
                var pts = bezier.points.Select(x => x.ToPoint3D()).ToArray();

                var gb = new MeshBuilder();
                gb.AddTube(path: pts,
                  diameter: this.Diameter,
                  thetaDiv: ThetaDiv,
                  isTubeClosed: false
                  );

                GeometryModel3D.Geometry = gb.ToMesh();

            }

        } 
    }
}