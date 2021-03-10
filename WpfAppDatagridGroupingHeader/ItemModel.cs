using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace WpfAppDatagridGroupingHeader
{
    public class ItemModel
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
        public int Diametr { get; internal set; }
        public Point3D StartPosition { get; internal set; }
        public Point3D EndPosition { get; internal set; }
        public int Radius { get; internal set; } = 0;
        public TubeTypes Type { get; internal set; }
        public ItemModel3D ItemModel3D { get; set; }
    }

    public enum TubeTypes
    {
        Regular,
        curved
    }
}