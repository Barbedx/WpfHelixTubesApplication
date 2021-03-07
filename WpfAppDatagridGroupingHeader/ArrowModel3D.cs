using System;
using System.Windows;
using System.Windows.Media.Media3D;

using HelixToolkit.Wpf;

namespace WpfAppDatagridGroupingHeader
{

    internal class ThreeArrowModel3D : ArrowModel3D
    {
        public ThreeArrowModel3D(ItemModel innerValue) : base(innerValue)
        {
        }
        protected override void AppearanceChanged()
        {

            var gb = new MeshBuilder();
            gb.AddArrow(StartPosition, EndPosition, Diametr, thetaDiv: this.ThetaDiv);
             
            gb.AddBox(StartPosition, Diametr * 3, Diametr * 2, Diametr / 4);
            GeometryModel3D.Geometry = gb.ToMesh();
        }
    }
    internal class ArrowModel3D : ItemModel3D<ItemModel>
    {

        public double Diametr
        {
            get { return (double)GetValue(DiametrProperty); }
            set { SetValue(DiametrProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Diametr.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DiametrProperty =
            DependencyPropertyEx.Register<double, ArrowModel3D>(nameof(Diametr), 10, (s, e) => s.AppearanceChanged());



        public static readonly DependencyProperty StartPositionProperty = DependencyPropertyEx.Register<Point3D, ArrowModel3D>(nameof(StartPosition), new Point3D(0, 0, 0), (s, e) => s.AppearanceChanged());
        public static readonly DependencyProperty EndPositionProperty = DependencyPropertyEx.Register<Point3D, ArrowModel3D>(nameof(EndPosition), new Point3D(10, 0, 0), (s, e) => s.AppearanceChanged());


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



        public int Height
        {
            get { return (int)GetValue(HeightProperty); }
            set { SetValue(HeightProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Height.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HeightProperty =
            DependencyProperty.Register("Height", typeof(int), typeof(ArrowModel3D), new PropertyMetadata(0));



        public ArrowModel3D(ItemModel innerValue) : base(innerValue)
        {
            this.StartPosition = innerValue.StartPosition;
            this.EndPosition = innerValue.EndPosition;

        }

        protected override void AppearanceChanged()
        {
            try
            {

                var gb = new MeshBuilder();
                gb.AddArrow(StartPosition, EndPosition, Diametr,thetaDiv:this.ThetaDiv);
                gb.AddBox(StartPosition,Diametr*3,Diametr*2,Diametr/4);
                GeometryModel3D.Geometry = gb.ToMesh();
            }
            catch (Exception ex)
            {
                 
            }
        }
    }
}