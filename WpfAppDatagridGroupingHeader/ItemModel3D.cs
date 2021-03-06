using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using WpfAppDatagridGroupingHeader.Models3D;

namespace WpfAppDatagridGroupingHeader
{
    public abstract class ItemModel3D<T> : UIElement3D ,IItemModel3D<T> where T : ItemModel, INotifyPropertyChanged
    {
        public T InnerModel { get; }

        public ItemModel3D(T model)
        {
            model.ItemModel3D = this;

            GeometryModel3D.Material = MaterialHelper.CreateMaterial(GradientBrushes.BlueWhiteRed);
            this.Visual3DModel = GeometryModel3D;
            this.InnerModel = model;
            model.PropertyChanged += Model_PropertyChanged; 
            
            // ReSharper disable once VirtualMemberCallInConstructor -- no one constructor else
            AppearanceChanged("base_ctor"); 
        }

        private void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            AppearanceChanged(e.PropertyName);
        }

        public GeometryModel3D GeometryModel3D { get; } = new GeometryModel3D();

        public abstract void AppearanceChanged([CallerMemberName] string caller = null);

        private int thetaDiv = 19;

        public int ThetaDiv
        {
            get => thetaDiv;
            private set
            {
                if (thetaDiv != value)
                {
                    thetaDiv = value;
                }

                ;
            }
        }
         
    }
}