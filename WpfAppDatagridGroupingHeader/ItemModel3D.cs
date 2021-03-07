using System.Windows;
using System.Windows.Media.Media3D;

using HelixToolkit.Wpf;

using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace WpfAppDatagridGroupingHeader
{
    public  abstract  class ItemModel3D<T> : UIElement3D where T : class
    {
        public ItemModel3D(T innerValue)
        {
            InnerValue = innerValue;
            GeometryModel3D.Material = MaterialHelper.CreateMaterial(GradientBrushes.BlueWhiteRed);
            this.Visual3DModel = GeometryModel3D;
            AppearanceChanged();
        }

        [ExpandableObject]
        public T InnerValue { get; set; }
        public GeometryModel3D GeometryModel3D { get; set; } = new GeometryModel3D();
        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        //Using a DependencyProperty as the backing store for Caption.This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CaptionProperty =
        DependencyPropertyEx.Register<string, ItemModel3D<T>>(nameof(Caption), "noname", (s, e) => s.AppearanceChanged());

        protected abstract void AppearanceChanged();


        public int ThetaDiv
        {
            get { return (int)GetValue(ThetaDivProperty); }
            set { SetValue(ThetaDivProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ThetaDiv.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ThetaDivProperty =
            DependencyPropertyEx.Register<int, PipeModel3D>(nameof(ThetaDiv), 19, (s, e) => s.AppearanceChanged());


    }
}