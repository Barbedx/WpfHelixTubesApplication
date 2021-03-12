using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using WpfAppDatagridGroupingHeader.Models;

namespace WpfAppDatagridGroupingHeader.Models3D
{
    internal class ArrowModel3D<T> : ItemModel3D<T> where T : ArrowItemModel
    {
        public ArrowModel3D(T model) : base(model)
        {
        }

        protected double Diameter => InnerModel.Diameter;
        protected Point3D Position => InnerModel.StartPosition;
        protected double Height => InnerModel.Height;
        protected double Offset => InnerModel.Offset; 
        public override void AppearanceChanged(string caller = null)
        {
            var ep = new Point3D(Position.X, Position.Y, Position.Z);
            var sp = new Point3D(Position.X, Position.Y, Position.Z - Offset - Height);
            var gb = new MeshBuilder();

            gb.AddArrow(sp, ep, Diameter, thetaDiv: this.ThetaDiv);
            gb.AddBox(sp, Diameter * 3, Diameter * 2, Diameter / 4);
            GeometryModel3D.Geometry = gb.ToMesh();
        }
    }
}