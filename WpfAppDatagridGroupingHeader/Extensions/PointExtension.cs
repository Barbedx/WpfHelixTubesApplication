﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

using geo=  GeometRi;

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
        public static geo.Point3d ToGeometRIPoint3D(this Point3D v)
        {
            return new geo.Point3d(v.X, v.Y, v.Z);
        }
        public static Point3D ToPoint3D(this Vector3 v)
        {
            return new Point3D(v.X, v.Y, v.Z);
        }

        public static Point3D ToPoint3D(this geo.Point3d v)
        {
            return new Point3D(v.X, v.Y, v.Z);
        }

        public static Vector3D ToVector3D(this Vector3 v)
        {
            return new Vector3D(v.X, v.Y, v.Z);
        }
        public static geo.Vector3d ToGeometRIVector3D(this Vector3D v)
        {
            return new geo.Vector3d(v.X, v.Y, v.Z);
        }
    }
}
