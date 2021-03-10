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

    internal class PipeModel3D : ItemModel3D
    {

        #region properties
        private Point3D startPosition ;

        public Point3D StartPosition
        {
            get { return startPosition; }
            set
            {
                if (startPosition != value)
                {
                    startPosition = value;
                    this.AppearanceChanged();
                };
            }
        }

        private Point3D endPosition  ;

        public Point3D EndPosition
        {
            get { return endPosition; }
            set
            {
                if (endPosition != value)
                {
                    endPosition = value;
                    this.AppearanceChanged();
                };
            }
        }

        private double diameter;

        public double Diameter
        {
            get { return diameter; }
            set
            {
                if (diameter != value)
                {
                    diameter = value;
                    this.AppearanceChanged();
                };
            }
        }

        private TubeStabs tubeStabs = TubeStabs.None;
 

        public PipeModel3D(Point3D startPosition, Point3D endPosition, double diameter, TubeStabs tubeStabs = TubeStabs.None)
        {
            this.startPosition = startPosition;
            this.endPosition = endPosition;
            this.diameter = diameter;
            this.tubeStabs = tubeStabs;
        }

        public PipeModel3D(ItemModel model)
        {
            this.startPosition = model.StartPosition;
            this.endPosition = model.EndPosition;
            this.diameter = model.Diametr;
            this.TubeStabs = TubeStabs.None;
        }

        public TubeStabs TubeStabs
        {
            get { return tubeStabs; }
            set
            {
                if (tubeStabs != value)
                {
                    tubeStabs = value;
                    this.AppearanceChanged();
                };
            }
        }
         
        #endregion

        public override void AppearanceChanged(string caller = null)
        {
            var gb = new MeshBuilder();

            gb.AddTube(path: new Point3D[] { StartPosition, EndPosition },
                   diameter: this.Diameter,
                   thetaDiv: ThetaDiv,
                   isTubeClosed: false,
                   TubeStabs == TubeStabs.FrontCap|| TubeStabs == TubeStabs.All,
                   TubeStabs == TubeStabs.BackCap|| TubeStabs == TubeStabs.All
                   );
            //GeometryModel3D.Material = this.Material;// MaterialHelper.CreateMaterial(Brushes.Red);
            //GeometryModel3D.BackMaterial = this.BackMaterial;
            GeometryModel3D.Geometry = gb.ToMesh();
        }


    }
}