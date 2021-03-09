using System;
using System.Windows;
using System.Windows.Media.Media3D;

using HelixToolkit.Wpf;

namespace WpfAppDatagridGroupingHeader
{
    internal class ThreeArrowModel3D : ArrowModel3D
    {


        public ThreeArrowModel3D(Point3D p, double zOffset = 5, double angleX = 0, double angleY = 0, double angleZ = 0, double height = 5, double diameter = 1) : this(p, new Vector3D(0, 1, 0), zOffset, height, diameter)
        {
            this.Transform = new TranslateTransform3D(p.ToVector3D());

            this.Transform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(1, 0, 0), angleX), center: p);
            this.Transform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 1, 0), angleY), center: p);
            this.Transform = new RotateTransform3D(new AxisAngleRotation3D(new Vector3D(0, 0, 1), angleZ), center: p);


        }

        public ThreeArrowModel3D(Point3D p, Vector3D vDirection, double zOffset, double height = 5, double diameter = 1) : base(p, zOffset, height, diameter)
        {
            this.VectorOfCenter = vDirection;
            this.Position = p;
            this.Height = height;
            this.Diametr = diameter;
            this.Offset = zOffset;

        }



        public Vector3D VectorOfCenter
        {
            get { return (Vector3D)GetValue(VectorOfCenterProperty); }
            set { SetValue(VectorOfCenterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for VectorOfCenter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty VectorOfCenterProperty =
            DependencyPropertyEx.Register<Vector3D, ThreeArrowModel3D>(nameof(VectorOfCenter), new Vector3D(0, 0, 0), (s, e) => s.AppearanceChanged(e));



        protected override void AppearanceChanged( DependencyPropertyChangedEventArgs e)
        {

            try
            {

                var ep = new Point3D(Position.X, Position.Y, Position.Z-Offset);
                var sp = new Point3D(Position.X, Position.Y, Position.Z - Offset - Height);


                var rotation = new AxisAngleRotation3D(VectorOfCenter, 90);
                var transformation = new RotateTransform3D(rotation, Position);
                var rotation2 = new AxisAngleRotation3D(VectorOfCenter, -90);
                var transformation2 = new RotateTransform3D(rotation2, Position);

                var point1Start = transformation.Transform(sp);
                var point1End = transformation.Transform(ep);
                var point2Start = transformation2.Transform(sp);
                var point2End = transformation2.Transform(ep);

                var gb = new MeshBuilder();

                gb.AddArrow(sp, ep, Diametr, thetaDiv: this.ThetaDiv);
                gb.AddArrow(point1Start, point1End, Diametr, thetaDiv: this.ThetaDiv);
                gb.AddArrow(point2Start, point2End, Diametr, thetaDiv: this.ThetaDiv);

                gb.AddBox(sp, Diametr * 3, Diametr * 2, Diametr / 4);
                //gb.AddBox(point1Start, Diametr / 4, Diametr * 3, Diametr * 2);//TODO:Write by vector
                //gb.AddBox(point2Start, Diametr / 4, Diametr * 3, Diametr * 2);//TODO:Write by vector
                GeometryModel3D.Geometry = gb.ToMesh(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}