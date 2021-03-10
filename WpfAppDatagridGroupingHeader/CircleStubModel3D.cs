using System.Windows;
using System.Windows.Media.Media3D;
using geo = GeometRi;

using HelixToolkit.Wpf;
using WpfAppDatagridGroupingHeader.Extensions;
using System.Numerics;
using System;

namespace WpfAppDatagridGroupingHeader
{


    internal class CircleStubModel3D : ItemModel3D
    {

        private double radius;

        public double Radius
        {
            get { return radius; }
            set
            {
                if(radius != value){
                    radius = value;
                    this.AppearanceChanged();
                };
            }
        }

        private Vector3D direction ;

        public Vector3D Direction
        {
            get { return direction; }
            set
            {
                if (direction != value)
                {
                    direction = value;
                    this.AppearanceChanged();
                };
            }
        }

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

 



        public override void AppearanceChanged(string caller = null)
        {
            var gb = new MeshBuilder(); 
            var vector = Direction.GetNormalized(); 
            gb.AddCone(Position, vector, Radius,Radius, 1, true, true, this.ThetaDiv);
            GeometryModel3D.Geometry = gb.ToMesh();
        }

        public CircleStubModel3D(ItemModel model) : base(model)
        {
            Radius = model.Radius;
            Position = model.StartPosition;
        }
        // public CircleStubModel3D(Point3D position, Vector3D direction, double radius)
        // {
        //     Radius = radius;
        //     Direction = direction;
        //     Position = position;
        // } 
    }
}