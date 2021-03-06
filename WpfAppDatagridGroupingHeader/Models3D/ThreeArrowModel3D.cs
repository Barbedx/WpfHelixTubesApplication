using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using WpfAppDatagridGroupingHeader.Models;

namespace WpfAppDatagridGroupingHeader.Models3D
{
    internal class ThreeArrowModel3D :  ArrowModel3D<ThreeArrowItemModel>
    {
        private Vector3D Direction => InnerModel.Direction; 
         

        public override void AppearanceChanged(string caller = null)
        {

           
                var ep = new Point3D(Position.X, Position.Y, Position.Z-Offset);
                var sp = new Point3D(Position.X, Position.Y, Position.Z - Offset - Height);
             
                var rotation = new AxisAngleRotation3D(Direction, 90);
                var transformation = new RotateTransform3D(rotation, Position);
                var rotation2 = new AxisAngleRotation3D(Direction, -90);
                var transformation2 = new RotateTransform3D(rotation2, Position);

                var point1Start = transformation.Transform(sp);
                var point1End = transformation.Transform(ep);
                var point2Start = transformation2.Transform(sp);
                var point2End = transformation2.Transform(ep);

                var gb = new MeshBuilder();

                gb.AddArrow(sp, ep, Diameter, thetaDiv: this.ThetaDiv);
                gb.AddArrow(point1Start, point1End, Diameter, thetaDiv: this.ThetaDiv);
                gb.AddArrow(point2Start, point2End, Diameter, thetaDiv: this.ThetaDiv);

                gb.AddBox(sp, Diameter * 3, Diameter * 2, Diameter / 4); 
                GeometryModel3D.Geometry = gb.ToMesh(); 
         
        }

        public ThreeArrowModel3D(ThreeArrowItemModel model) : base(model)
        {
        }
    }
}