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

        public Point3D StartPosition { get; set; }
        public Point3D EndPosition { get; set; }
        public int Radius { get; set; } = 0;
        public TubeTypes Type { get; set; }
        public TubeStabs TubeStabs { get; set; }

    }

    public enum TubeTypes
    {
        Regular,
        curved
    }
}