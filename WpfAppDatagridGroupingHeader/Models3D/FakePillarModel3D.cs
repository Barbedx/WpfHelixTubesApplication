using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using WpfAppDatagridGroupingHeader.Models;

namespace WpfAppDatagridGroupingHeader.Models3D
{
    internal class FakePillarModel3D : ItemModel3D<FakePillarModel> 
    {
        private double Diameter => InnerModel.Diameter;
        private Point3D Position => InnerModel.StartPosition;
        private double Height => InnerModel.Height;
        private double Offset => InnerModel.Offset; 
        
        public FakePillarModel3D(FakePillarModel model) : base(model)
        {
          
        }

        public override void AppearanceChanged(string caller = null)
        {
            var ep = new Point3D(Position.X, Position.Y, Position.Z - Offset);
            var sp = new Point3D(Position.X, Position.Y, Position.Z - Offset - Height);
            var gb = new MeshBuilder();
            gb.AddCone(sp, ep, Diameter, true, this.ThetaDiv);
            gb.AddBox(sp, Diameter * 3, Diameter * 2, Diameter / 4);
            GeometryModel3D.Geometry = gb.ToMesh();
        }
    }
}