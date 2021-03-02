using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Media3D;
using System.Numerics;
using HelixToolkit.Wpf;
using WpfAppDatagridGroupingHeader.Helpers;
using System.Linq;
using System.Windows.Media;

namespace WpfAppDatagridGroupingHeader
{
    internal class PipeModel3D : ItemModel3D<ItemModel>
    {
        public PipeModel3D(ItemModel innerValue): base(innerValue)
        {
        }

        #region DP Properties
        public static readonly DependencyProperty StartPositionProperty = DependencyPropertyEx.Register<Point3D, PipeModel3D>(nameof(StartPosition), new Point3D(0, 0, 0), (s, e) => s.AppearanceChanged());
        public static readonly DependencyProperty EndPositionProperty = DependencyPropertyEx.Register<Point3D, PipeModel3D>(nameof(EndPosition), new Point3D(10, 0, 0), (s, e) => s.AppearanceChanged());

     
        public double Diametr
        {
            get { return (double)GetValue(DiametrProperty); }
            set { SetValue(DiametrProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Diametr.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DiametrProperty =
            DependencyPropertyEx.Register<double, PipeModel3D>(nameof(Diametr), 10, (s, e) => s.AppearanceChanged());



        public bool IsCurved
        {
            get { return (bool)GetValue(IsCurvedProperty); }
            set { SetValue(IsCurvedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsCurved.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCurvedProperty =
            DependencyPropertyEx.Register<bool, PipeModel3D>(nameof(IsCurved), false, (s, e) => s.AppearanceChanged()); 

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


        public Point3D Position2
        {
            get { return (Point3D)GetValue(Position2Property); }
            set { SetValue(Position2Property, value); }
        }

        // Using a DependencyProperty as the backing store for Position2.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Position2Property =
            DependencyPropertyEx.Register<Point3D, PipeModel3D>(nameof(Position2), new Point3D(10, 0, 0), (s, e) => s.AppearanceChanged());
 
        public float Length
        {
            get { return (float)GetValue(LengthProperty); }
            set { SetValue(LengthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Length.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LengthProperty =
        DependencyPropertyEx.Register<float, PipeModel3D>(nameof(Length), 0, (s, e) => s.AppearanceChanged());
 
        public Point3D Position3
        {
            get { return (Point3D)GetValue(Position3Property); }
            set { SetValue(Position3Property, value); }
        }

        // Using a DependencyProperty as the backing store for Position3.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Position3Property = 
            DependencyPropertyEx.Register<Point3D, PipeModel3D>(nameof(Position3), new Point3D(10, 0, 0), (s, e) => s.AppearanceChanged());

        #endregion

        protected override void AppearanceChanged()
        {
            var gb = new MeshBuilder();

            
            var bazier = new Bezier(StartPosition, Position2, Position3, EndPosition,100);
            Point3D[] pts = bazier.points.Select(x => new Point3D(x.X, x.Y, x.Z)).ToArray();


            gb.AddTube(path: pts,
                diameter: this.Diametr,
                thetaDiv: 19,
                isTubeClosed: false
                ) ;
             
            GeometryModel3D.Material = MaterialHelper.CreateMaterial(Brushes.Red);
            GeometryModel3D.BackMaterial = MaterialHelper.CreateMaterial(Brushes.Yellow);
            GeometryModel3D.Geometry = gb.ToMesh();
            //.Geometry = gb.ToMesh();
            //gb.arc
        }

   

    }
}