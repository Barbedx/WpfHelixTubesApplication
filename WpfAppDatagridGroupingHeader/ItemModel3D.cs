using System.Windows;
using System.Windows.Media.Media3D;

using HelixToolkit.Wpf;

using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace WpfAppDatagridGroupingHeader
{
    public  abstract  class ItemModel3D  : UIElement3D // where T : class
    {
        public ItemModel3D()
        {
            Material = MaterialHelper.CreateMaterial(GradientBrushes.RainbowStripes);
            GeometryModel3D.Material = Material;// MaterialHelper.CreateMaterial(GradientBrushes.BlueWhiteRed);
            this.Visual3DModel = GeometryModel3D;
            AppearanceChanged(new DependencyPropertyChangedEventArgs());
        }

        [ExpandableObject]
       
        public GeometryModel3D GeometryModel3D { get; set; } = new GeometryModel3D();
        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        //Using a DependencyProperty as the backing store for Caption.This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CaptionProperty =
        DependencyPropertyEx.Register<string, ItemModel3D>(nameof(Caption), "noname", (s, e) => s.AppearanceChanged(e));

        protected abstract void AppearanceChanged(DependencyPropertyChangedEventArgs e);



        public Material Material
        {
            get { return (Material)GetValue(MaterialProperty); }
            set { SetValue(MaterialProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Material.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MaterialProperty =
            DependencyPropertyEx.Register<Material, ItemModel3D>(nameof(Material), MaterialHelper.CreateMaterial(GradientBrushes.RainbowStripes), (s, e) => s.AppearanceChanged(e));


        public Material BackMaterial
        {
            get { return (Material)GetValue(BackMaterialProperty); }
            set { SetValue(BackMaterialProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Material.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BackMaterialProperty =
            DependencyPropertyEx.Register<Material, ItemModel3D>(nameof(BackMaterialProperty), MaterialHelper.CreateMaterial(GradientBrushes.HueStripes), (s, e) => s.AppearanceChanged(e));



        public int ThetaDiv
        {
            get { return (int)GetValue(ThetaDivProperty); }
            set { SetValue(ThetaDivProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ThetaDiv.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ThetaDivProperty =
            DependencyPropertyEx.Register<int, ItemModel3D>(nameof(ThetaDiv), 19, (s, e) => s.AppearanceChanged(e));


    }
}