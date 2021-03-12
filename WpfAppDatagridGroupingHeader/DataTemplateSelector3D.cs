using System.Windows;

namespace WpfAppDatagridGroupingHeader
{
    public abstract class DataTemplateSelector3D 
    {
        public  virtual DataTemplate3D SelectTemplate(object item, DependencyObject container)
        {
            return null;
        }
    }


    public  class MyVisualModel3dDataTemplateSelector3D: DataTemplateSelector3D
    {
        public override DataTemplate3D SelectTemplate(object item, DependencyObject container)
        {
            return new MyVisualModel3DDataTemplate(item);
        }
    }



}