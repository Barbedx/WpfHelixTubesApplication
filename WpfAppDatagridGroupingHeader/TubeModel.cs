using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace WpfAppDatagridGroupingHeader
{
    public class TubeModel :ModelBase
    {
        internal string c3;
        public int Id;

        public TubeModel(int v)
        {
            this.Id = v;
        }

        public string c1 { get; internal set; }
        public string c2 { get; internal set; }
        public double c4 { get; internal set; }
        public List<double> c5 { get; internal set; }
        public Person Spouse { get; internal set; }

        public Point3D StartPosition { get; internal set; }
        public Point3D EndPosition { get; internal set; }

    }
}