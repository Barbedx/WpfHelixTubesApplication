using System.ComponentModel;
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

            ItemModel3D<ItemModel> itemModel3D = null;
            if (dataContext is ItemModel model)
            {
                switch (model)
                {
                    case CurvedItemModel curvedItemModel:
                         return new CurvedPipeModel3D(curvedItemModel);
                        break;
                    
                    case TeePipeItemModel teePipeItemModel:
                        return new TeePipeModel3D(teePipeItemModel);
                    
                    case ThreeArrowItemModel threeArrowItemModel :
                        return  new ThreeArrowModel3D(threeArrowItemModel);
                    
                    case ArrowItemModel arrowModel3D:
                        return  new ArrowModel3D<ArrowItemModel>(arrowModel3D);
                    
                    case ValveItemModel valveItemModel:
                        return  new ValveModel3D(valveItemModel);
                    
                    case CircleStubModel circleStubModel:
                        return  new CircleStubModel3D(circleStubModel);
                    
                       
                    default:
                        return  new PipeModel3D<ItemModel> (model);
                        break;
                        // throw  new InvalidEnumArgumentException($"Template for type {model.GetType()}  not found");
                }
                itemModel3D.AppearanceChanged();
            }
            else
                throw  new InvalidEnumArgumentException($"Oject {dataContext.GetType()} has no datatamlate");
            return itemModel3D;
        }
    }
}