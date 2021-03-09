using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace WpfAppDatagridGroupingHeader
{
    static class ItemModel3DFactory
    {
       static public ItemModel3D CreateArcVisual3D(Point3D p, double zOffset, double height=5, double diametr = 1 )
        {
            return new ArrowModel3D(p, zOffset, height, diametr);
        }
        
    }
}
