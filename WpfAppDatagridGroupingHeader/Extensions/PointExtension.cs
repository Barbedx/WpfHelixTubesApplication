using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace WpfAppDatagridGroupingHeader.Extensions
{
public    static  class PointExtension
    {
       public static Vector3 ToVector3(this Point3D point3D)
        {
            var x = (float)point3D.X;
            var y = (float)point3D.Y;
            var z = (float)point3D.Z;
            return new Vector3(x, y, z);
        }
    }
}
