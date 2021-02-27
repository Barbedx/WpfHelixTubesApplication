using System.Collections.Generic;
using System.Windows.Media.Media3D;

namespace WpfAppDatagridGroupingHeader
{
    public class PipeModel :ModelBase
    {
        internal string c3;
 

        public PipeModel(int v)
        {
            this.ID = v;
        }
        public Point3D StartPosition { get; internal set; }
        public Point3D EndPosition { get; internal set; }
    }

    public class TubeModel: ModelBase
    {
        public Point3DCollection Points { get; set; }   
    }
}