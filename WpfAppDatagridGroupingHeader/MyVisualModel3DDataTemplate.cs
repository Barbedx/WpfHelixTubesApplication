using System.Windows.Media.Media3D;

namespace WpfAppDatagridGroupingHeader
{
    public class MyVisualModel3DDataTemplate : DataTemplate3D
    {
        private object item;

        public MyVisualModel3DDataTemplate(object item)
        {
            this.item = item;
        }
        public override Visual3D CreateItem(object dataContext)
        {
            ItemModel3D itemModel3D = null;
            if (dataContext is ItemModel model)
            {
                switch (model)
                {
                    case CurvedItemModel curvedItemModel:
                        itemModel3D = new CurvedPipeModel3D(curvedItemModel);
                        break;
                    case ItemModel itemModel:
                        itemModel3D = new PipeModel3D(itemModel);
                        break;
                    default:
                        break;
                }
                itemModel3D.AppearanceChanged();
            }
            return itemModel3D;
        }
    }
}