using System.Windows.Media.Media3D;

using HelixToolkit.Wpf;

using WpfAppDatagridGroupingHeader.Extensions;
using WpfAppDatagridGroupingHeader.Models;
using WpfAppDatagridGroupingHeader.Models3D;

namespace WpfAppDatagridGroupingHeader
{
    internal class TeePipeModel3D : PipeModel3D<TeePipeItemModel>
    {
 
        public Vector3D MiddlePipeDirection => InnerModel.MiddlePipeDirection;
        private double MiddlePipeDiameter => InnerModel.MiddlePipeDiameter;

        private Point3D MiddlePipeEndPoint => InnerModel.MiddlePipeEndPosition;
         

        public override void AppearanceChanged(string caller = null)
        {
            var gb = new MeshBuilder();

            gb.AddTube(path: new Point3D[] { StartPosition, EndPosition },
                   diameter: this.Diameter,
                   thetaDiv: ThetaDiv,
                   isTubeClosed: false,
                   TubeStabs == TubeStabs.FrontCap || TubeStabs == TubeStabs.All,
                   TubeStabs == TubeStabs.BackCap || TubeStabs == TubeStabs.All
                   );

            var middlePosition = StartPosition.GetMidPointTo(EndPosition);
           
           gb.AddTube(path: new Point3D[] { middlePosition, MiddlePipeEndPoint },
                diameter: this.MiddlePipeDiameter,
                thetaDiv: ThetaDiv,
                isTubeClosed: false);
             
            GeometryModel3D.Geometry = gb.ToMesh(); 
        }

        public TeePipeModel3D(TeePipeItemModel model) : base(model)
        {
        }
    }
}