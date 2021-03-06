using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using WpfAppDatagridGroupingHeader.Models;

namespace WpfAppDatagridGroupingHeader.Models3D
{
    internal class ValveModel3D : PipeModel3D<ValveItemModel>
    {
  
        public override void AppearanceChanged(string caller = null)
        {
            var mb = new MeshBuilder();
            var centerX = (StartPosition.X  + EndPosition.X) / 2;
            var centerY = (StartPosition.Y  + EndPosition.Y ) / 2;
            var centerZ = (StartPosition.Z +  EndPosition.Z ) / 2;
            var centerPoint = new Point3D(centerX, centerY, centerZ); 
          

            mb.AddCone(StartPosition, centerPoint, this.Diameter,
                TubeStabs == TubeStabs.FrontCap|| TubeStabs == TubeStabs.All
                , ThetaDiv);
            mb.AddCone(EndPosition, centerPoint, Diameter, 
                TubeStabs == TubeStabs.BackCap|| TubeStabs == TubeStabs.All,
                ThetaDiv);
            this.GeometryModel3D.Geometry = mb.ToMesh();
        }

        public ValveModel3D(ValveItemModel model) : base(model)
        {
        }
    }
}