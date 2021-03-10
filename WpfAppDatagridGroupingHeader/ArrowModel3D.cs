using System;
using System.Windows;
using System.Windows.Media.Media3D;

using HelixToolkit.Wpf;

namespace WpfAppDatagridGroupingHeader
{
    internal class ArrowModel3D : ItemModel3D
    {
 
        private double diameter = 10;

        public ArrowModel3D(ItemModel model, double diameter, Point3D position, double height, double offset) : base(model)
        {
            this.diameter = diameter;
            this.position = position;
            this.height = height;
            this.offset = offset;
        }

        // public ArrowModel3D(Point3D p, double zOffset, double height = 5, double diameter = 1)
        // {
        //     this.position = new Point3D(p.X, p.Y, p.Z);
        //     this.height = height;
        //     this.offset = zOffset;
        //     this.diameter = diameter;
        //     AppearanceChanged();
        // }
        protected double Diameter
        {
            get => diameter;
            set
            {
                if (diameter != value)
                {
                    diameter = value;
                    this.AppearanceChanged();
                };
            }
        }



        //public static readonly DependencyProperty PositionProperty = DependencyPropertyEx.Register<Point3D, ArrowModel3D>(nameof(Position), new Point3D(0, 0, 0), (s, e) => s.AppearanceChanged(e)); 

        private Point3D position;

        public Point3D Position
        {
            get { return position; }
            set
            {
                if (position != value)
                {
                    position = value;
                    this.AppearanceChanged();
                };
            }
        }

        private double height = 5;

        public double Height
        {
            get { return height; }
            set
            {
                if (height != value)
                {
                    height = value;
                    this.AppearanceChanged();
                };
            }
        }

 
        private double offset = 5;

        public double Offset
        {
            get { return offset; }
            set
            {
                if (offset != value)
                {
                    offset = value;
                    this.AppearanceChanged();
                };
            }
        }
 

        public override void AppearanceChanged(string caller = null)
        {

            var ep = new Point3D(Position.X, Position.Y, Position.Z);
            var sp = new Point3D(Position.X, Position.Y, Position.Z - Offset - Height);
            var gb = new MeshBuilder();

            gb.AddArrow(sp, ep, Diameter, thetaDiv: this.ThetaDiv);
            gb.AddBox(sp, Diameter * 3, Diameter * 2, Diameter / 4);
            GeometryModel3D.Geometry = gb.ToMesh();

        }
 
    }
}