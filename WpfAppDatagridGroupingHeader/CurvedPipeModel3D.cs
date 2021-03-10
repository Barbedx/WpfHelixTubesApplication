using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

using geo = GeometRi;

using HelixToolkit.Wpf;

using WpfAppDatagridGroupingHeader.Extensions;
using WpfAppDatagridGroupingHeader.Helpers;
using System.Runtime.CompilerServices;

namespace WpfAppDatagridGroupingHeader
{
    internal class CurvedPipeModel3D : PipeModel3D<CurvedItemModel>
    { 
 
        public CurvedPipeModel3D(CurvedItemModel model) : base(model)
        { 
           
        }
 
        public Vector3D StartDirection => InnerModel.StartDirection;

      

        public Vector3D EndDirection => InnerModel.EndDirection;



        public double Length => InnerModel.Length; 
        
        private Point3D quadraticCurvedPosition;

        public Point3D QuadraticCurvedPosition
        {
            get => quadraticCurvedPosition;
            private set => quadraticCurvedPosition = value;
        }
 
        //private Bezier Bezier { get; set; }
        public override void AppearanceChanged([CallerMemberName] string caller = null)
        {
            // if (caller == nameof(EndPosition))
            // {
            //     return;
            // }
            var sd = StartDirection.GetNormalized();// StartDirection.Normalize();
            var ed = EndDirection.GetNormalized();// .Normalize();
            var vector = sd + ed;
            vector *= Length;
            var ep = StartPosition + vector;
            //EndPosition = ep; //TODO:Check if we need it ??

            var l = new geo.Line3d(StartPosition.ToGeometRIPoint3D(), sd.ToGeometRIVector3D());
            var l2 = new geo.Line3d(EndPosition.ToGeometRIPoint3D(), ed.ToGeometRIVector3D());
            var intersectionPoint = l.IntersectionWith(l2);

            if (intersectionPoint is geo.Point3d interPoint)
            {
               quadraticCurvedPosition= interPoint.ToPoint3D();// new Point3D(interPoint.X, interPoint.Y, interPoint.Z); 

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