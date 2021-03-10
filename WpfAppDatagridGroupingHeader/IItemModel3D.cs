using System.ComponentModel;
using System.Windows.Media.Media3D;

namespace WpfAppDatagridGroupingHeader
{
    public   interface IItemModel3D<out T> where T: ItemModel
    {
        T InnerModel { get; }

        GeometryModel3D GeometryModel3D { get; }

    }
}