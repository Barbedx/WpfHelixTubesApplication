using System.Windows.Media.Media3D;
using WpfAppDatagridGroupingHeader.Models3D;

namespace WpfAppDatagridGroupingHeader
{
    public class ItemModel : ViewModelBase
    {
        public int ID { get; set; }

        public ItemModel(int iD)
        {
            ID = iD;
        }

        private double diameter;

        public double Diameter
        {
            get { return diameter; }
            set { this.SetValue(ref diameter, value); }
        }

        private Point3D startPosition;

        public Point3D StartPosition
        {
            get { return startPosition; }
            set { this.SetValue(ref startPosition, value); }
        }

        private Point3D endPosition;

        public Point3D EndPosition
        {
            get { return endPosition; }
            set { this.SetValue(ref endPosition, value); }
        }

        private TubeStabs tubeStabs;

        public TubeStabs TubeStabs
        {
            get { return tubeStabs; }
            set { this.SetValue(ref tubeStabs, value); }
        }

        public IItemModel3D<ItemModel> ItemModel3D { get; set; }
    }
}