using System;
using System.Windows;
using System.Windows.Media.Media3D;

using HelixToolkit.Wpf;

namespace WpfAppDatagridGroupingHeader
{
    internal class ArrowModel3D : ItemModel3D
    {

        public double Diametr
        {
            get { return (double)GetValue(DiametrProperty); }
            set { SetValue(DiametrProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Diametr.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DiametrProperty =
            DependencyPropertyEx.Register<double, ArrowModel3D>(nameof(Diametr), 10, (s, e) => s.AppearanceChanged(e));



        public static readonly DependencyProperty PositionProperty = DependencyPropertyEx.Register<Point3D, ArrowModel3D>(nameof(Position), new Point3D(0, 0, 0), (s, e) => s.AppearanceChanged(e)); 


        public Point3D Position
        {
            get { return (Point3D)this.GetValue(PositionProperty); }
            set { this.SetValue(PositionProperty, value); }
        }


        public double Height
        {
            get { return (double)GetValue(HeightProperty); }
            set { SetValue(HeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Height.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeightProperty =
        DependencyPropertyEx.Register<double, ArrowModel3D>(nameof(Height), 5, (s, e) => s.AppearanceChanged(e));



        public double Offset
        {

            get { return (double)GetValue(OffsetProperty); }
            set { SetValue(OffsetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Offset.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OffsetProperty =
        DependencyPropertyEx.Register<double, ArrowModel3D>(nameof(Offset), 5, (s, e) => s.AppearanceChanged(e));



        public ArrowModel3D(Point3D p, double zOffset, double height = 5, double diameter = 1)
        {
            this.Position = new Point3D(p.X, p.Y, p.Z);
            this.Height = height;
            this.Offset = zOffset;
            this.Diametr = diameter;
        }

        protected override void AppearanceChanged(DependencyPropertyChangedEventArgs e)
        {
            try
            {

                var ep = new Point3D(Position.X, Position.Y, Position.Z);
                var sp = new Point3D(Position.X, Position.Y, Position.Z - Offset - Height);
                var gb = new MeshBuilder();

                gb.AddArrow(sp, ep, Diametr, thetaDiv: this.ThetaDiv);
                gb.AddBox(sp, Diametr * 3, Diametr * 2, Diametr / 4);
                GeometryModel3D.Geometry = gb.ToMesh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error with"+e.Property.Name+"message: "+ex.Message);
            }
        }
    }
}