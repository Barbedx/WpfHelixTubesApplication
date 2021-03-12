using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Media3D;
using System.Numerics;
using HelixToolkit.Wpf;
using WpfAppDatagridGroupingHeader.Helpers;
using System.Linq;
using System.Windows.Media;
using System;
using WpfAppDatagridGroupingHeader.Extensions;

namespace WpfAppDatagridGroupingHeader
{
    public enum TubeStabs
    {
        None,
        FrontCap,
        BackCap,
        All
    }

    internal class PipeModel3D<T> : ItemModel3D<T> where T:ItemModel
    {

        #region properties 
        public Point3D StartPosition => InnerModel.StartPosition;
 
        public Point3D EndPosition => InnerModel.EndPosition;
 
        public double Diameter => InnerModel.Diameter;

        public TubeStabs TubeStabs => InnerModel.TubeStabs;// TubeStabs.None;
  
        public PipeModel3D(T model) : base(model)
        {
     
        } 
        #endregion

        public override void AppearanceChanged(string caller = null)
        {
            var gb = new MeshBuilder();

            gb.AddTube(path: new Point3D[] { StartPosition, EndPosition },
                   diameter: this.Diameter,
                   thetaDiv: ThetaDiv,
                   isTubeClosed: false,
                   TubeStabs == WpfAppDatagridGroupingHeader.TubeStabs.FrontCap|| TubeStabs == WpfAppDatagridGroupingHeader.TubeStabs.All,
                   TubeStabs == WpfAppDatagridGroupingHeader.TubeStabs.BackCap|| TubeStabs == WpfAppDatagridGroupingHeader.TubeStabs.All
                   );
            GeometryModel3D.Geometry = gb.ToMesh();
        }


    }
}