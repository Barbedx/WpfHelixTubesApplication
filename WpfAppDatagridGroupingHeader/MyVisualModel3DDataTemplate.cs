using System.ComponentModel;
using System.Windows.Media.Media3D;
using WpfAppDatagridGroupingHeader.Models;
using WpfAppDatagridGroupingHeader.Models3D;

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

            ItemModel3D<ItemModel> itemModel3D = null;
            if (dataContext is ItemModel model)
            {
                switch (model)
                {
                    case CurvedItemModel itemModel:
                         return new CurvedPipeModel3D(itemModel);
                        break;
                    
                    case TeePipeItemModel itemModel:
                        return new TeePipeModel3D(itemModel);
                    
                    case ThreeArrowItemModel itemModel:
                        return  new ThreeArrowModel3D(itemModel);
                    
                    case FakePillarModel itemModel:
                        return  new FakePillarModel3D(itemModel);
                    
                    case ArrowItemModel itemModel:
                        return  new ArrowModel3D<ArrowItemModel>(itemModel);
                    
                    case ValveItemModel itemModel:
                        return  new ValveModel3D(itemModel);

                    case CircleStubModel itemModel:
                        return new CircleStubModel3D(itemModel);

                    case SquareStubModel itemModel:
                        return new SquareStubModel3d(itemModel);
                 
                    default:
                        return  new PipeModel3D<ItemModel> (model);
                        break;
                        // throw  new InvalidEnumArgumentException($"Template for type {model.GetType()}  not found");
                }
                itemModel3D.AppearanceChanged();
            }
            else
                throw  new InvalidEnumArgumentException($@"Object {dataContext.GetType()} has no datatemlate");
            return itemModel3D;
        }
    }
}