using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Media3D;
using System.Numerics;
using HelixToolkit.Wpf;
using WpfAppDatagridGroupingHeader.Helpers;
using System.Linq;
using System.Windows.Media;
using System;
using WpfAppDatagridGroupingHeader.Extensions;

namespace WpfAppDatagridGroupingHeader
{
    public enum TubeStabs
    {
        None,
        FrontCap,
        BackCap,
        All
    }

    internal class PipeModel3D : ItemModel3D
    {
        #region DP Properties
        public static readonly DependencyProperty StartPositionProperty = DependencyPropertyEx.Register<Point3D, PipeModel3D>(nameof(StartPosition), new Point3D(0, 0, 0), (s, e) => s.AppearanceChanged(e));
        public static readonly DependencyProperty EndPositionProperty = DependencyPropertyEx.Register<Point3D, PipeModel3D>(nameof(EndPosition), new Point3D(10, 0, 0), (s, e) => s.AppearanceChanged(e));


        public double Diametr
        {
            get { return (double)GetValue(DiametrProperty); }
            set { SetValue(DiametrProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Diametr.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DiametrProperty =
            DependencyPropertyEx.Register<double, PipeModel3D>(nameof(Diametr), 10, (s, e) => s.AppearanceChanged(e));





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



        public TubeStabs TubeStabs
        {
            get { return (TubeStabs)GetValue(TubeStabsProperty); }
            set { SetValue(TubeStabsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TubeStabs.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TubeStabsProperty =
         DependencyPropertyEx.Register<TubeStabs, PipeModel3D>(nameof(TubeStabs), new TubeStabs(), (s, e) => s.AppearanceChanged(e));



        #endregion

        protected override void AppearanceChanged(DependencyPropertyChangedEventArgs e)
        {
            var gb = new MeshBuilder();

            gb.AddTube(path: new Point3D[] { StartPosition, EndPosition },
                   diameter: this.Diametr,
                   thetaDiv: ThetaDiv,
                   isTubeClosed: false,
                   TubeStabs == TubeStabs.FrontCap|| TubeStabs == TubeStabs.All,
                   TubeStabs == TubeStabs.BackCap|| TubeStabs == TubeStabs.All
                   );

            GeometryModel3D.Material = this.Material;// MaterialHelper.CreateMaterial(Brushes.Red);
            GeometryModel3D.BackMaterial = this.BackMaterial;
            GeometryModel3D.Geometry = gb.ToMesh();
        }


    }
}