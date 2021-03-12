using System.Windows.Media.Media3D;

namespace WpfAppDatagridGroupingHeader.Models3D
{
    public   interface IItemModel3D<out T> where T: ItemModel
    {
        T InnerModel { get; }

        GeometryModel3D GeometryModel3D { get; }

    }
}