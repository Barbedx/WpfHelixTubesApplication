using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Media.Media3D;

namespace WpfAppDatagridGroupingHeader
{


    public class ItemModel : ViewModelBase
    {
        public int ID { get; set; }

        public ItemModel(int iD)
        {
            ID = iD;
        }

        public string Caption { get; set; }
        public string c1 { get; set; }
        public string c2 { get; set; }
        public List<double> c5 { get; set; }

        private double diameter;

        public double Diameter
        {
            get { return diameter; }
            set
            {
                this.SetValue(ref diameter, value);
            }
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

        private double radius;

        public double Radius
        {
            get { return radius; }
            set { this.SetValue(ref radius, value); }
        }

        private TubeStabs tubeStabs;

        public TubeStabs TubeStabs
        {
            get { return tubeStabs; }
            set { this.SetValue(ref tubeStabs, value); }
        }
public        IItemModel3D<ItemModel> ItemModel3D { get; set; }
    }
     
}