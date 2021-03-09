using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media.Media3D;

using HelixToolkit.Wpf;

using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace WpfAppDatagridGroupingHeader
{
    public abstract class ItemModel3D : UIElement3D // where T : class
    {
        public ItemModel3D()
        {
            GeometryModel3D.Material =  MaterialHelper.CreateMaterial(GradientBrushes.BlueWhiteRed);
            this.Visual3DModel = GeometryModel3D;
            AppearanceChanged();
        }

        public GeometryModel3D GeometryModel3D { get; set; } = new GeometryModel3D();

        protected abstract void AppearanceChanged([CallerMemberName] string caller = null);
         
        private int thetaDiv= 19;

        public int ThetaDiv
        {
            get { return thetaDiv; }
            set
            {
                if (thetaDiv != value)
                {
                    thetaDiv = value;
                    this.AppearanceChanged();
                };
            }
        }


    }
}