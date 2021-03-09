using System;
using System.Windows;
using System.Windows.Media.Media3D;

using HelixToolkit.Wpf;

namespace WpfAppDatagridGroupingHeader
{
    internal class ValveModel3D : ItemModel3D
    {
        public double Diametr
        {
            get { return (double)GetValue(DiametrProperty); }
            set { SetValue(DiametrProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Diametr.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DiametrProperty =
            DependencyPropertyEx.Register<double, ValveModel3D>(nameof(Diametr), 10, (s, e) => s.AppearanceChanged(e));



        public static readonly DependencyProperty StartPositionProperty = DependencyPropertyEx.Register<Point3D, ValveModel3D>(nameof(StartPosition), new Point3D(0, 0, 0), (s, e) => s.AppearanceChanged(e));
        public static readonly DependencyProperty EndPositionProperty = DependencyPropertyEx.Register<Point3D, ValveModel3D>(nameof(EndPosition), new Point3D(10, 0, 0), (s, e) => s.AppearanceChanged(e));

        public ValveModel3D(ItemModel innerValue)
        {
        }

        public Point3D StartPosition
        {
            get { return (Point3D)this.GetValue(StartPositionProperty); }
            set { this.SetValue(StartPositionProperty, value); }
        }
        public Point3D EndPosition
        {
            get { return (Point3D)this.GetValue(EndPositionProperty); }
            set { this.SetValue(EndPositionProperty, value); }
        }



        public bool Cap
        {
            get { return (bool)GetValue(CapProperty); }
            set { SetValue(CapProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cap.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CapProperty = 
        DependencyPropertyEx.Register<bool, ValveModel3D>(nameof(Cap), false, (s, e) => s.AppearanceChanged(e));


        protected override void AppearanceChanged(DependencyPropertyChangedEventArgs e)
        {
            var mb = new MeshBuilder();
            var centerX = (StartPosition.X  + EndPosition.X) / 2;
            var centerY = (StartPosition.Y  + EndPosition.Y ) / 2;
            var centerZ = (StartPosition.Z +  EndPosition.Z ) / 2;
            var centerPoint = new Point3D(centerX, centerY, centerZ);

            mb.AddCone(StartPosition, centerPoint, Diametr, Cap, ThetaDiv);
            mb.AddCone(EndPosition, centerPoint, Diametr, Cap, ThetaDiv);
            this.GeometryModel3D.Geometry = mb.ToMesh();
        }
    }
}