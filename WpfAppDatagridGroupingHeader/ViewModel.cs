using System.Collections.ObjectModel;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using WpfAppDatagridGroupingHeader.Models3D;

namespace WpfAppDatagridGroupingHeader
{
    public class ViewModel : ViewModelBase
    {

        public ObservableCollection<ItemModel>    Items{ get; } = new ObservableCollection<ItemModel>();

        private ItemModel selectedObject;
        public ItemModel SelectedObject
        {
            get { return this.selectedObject; }

            set
            {
                if (this.SetValue(ref this.selectedObject, value, nameof(this.SelectedObject)))
                {
                    this.Select3D(selectedObject?.ItemModel3D);
                }
            }
        }

        public void Select(ItemModel visual)
        {
            this.SelectedObject = visual;
        }

        private IItemModel3D<ItemModel> selectedObject3D;
        private Material previusMaterial;

        // ReSharper disable once MemberCanBePrivate.Global --Used in xaml binding
        public IItemModel3D<ItemModel> SelectedObject3D
        {
            get { return this.selectedObject3D; }

            set { this.SetValue(ref this.selectedObject3D, value, nameof(this.selectedObject3D)); }
        }


        private void Select3D(IItemModel3D<ItemModel> model)
        {
            if (model != SelectedObject3D)
            {

                if (selectedObject3D != null && previusMaterial != null)
                {
                    selectedObject3D.GeometryModel3D.Material = previusMaterial;
                }
                if (model != null)
                {
                    var gModel = model.GeometryModel3D;
                    previusMaterial = gModel.Material;
                    gModel.Material = Materials.Green;
                }
                this.SelectedObject3D = model;
            }
        }
    }
}