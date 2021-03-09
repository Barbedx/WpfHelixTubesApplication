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

        //public double Size
        //{
        //    get { return (double)GetValue(SizeProperty); }
        //    set { SetValue(SizeProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for Size.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty SizeProperty =
        // DependencyPropertyEx.Register<double, CircleStubModel3D>(nameof(Size), new double(), (s, e) => s.AppearanceChanged(e));

        private double _size;

        public double Size
        {
            get { return _size; }
            set
            {
                _size = value;
                this.AppearanceChanged(new DependencyPropertyChangedEventArgs());
             }
        }


        public Vector3D Direction
        {
            get { return (Vector3D)GetValue(DirectionProperty); }
            set { SetValue(DirectionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Direction.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DirectionProperty =
         DependencyPropertyEx.Register<Vector3D, CircleStubModel3D>(nameof(Direction), new Vector3D(), (s, e) => s.AppearanceChanged(e));



        public Point3D Position
        {
            get { return (Point3D)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Position.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PositionProperty =
         DependencyPropertyEx.Register<Point3D, CircleStubModel3D>(nameof(Position), new Point3D(), (s, e) => s.AppearanceChanged(e));



        public Vector3D DirectionY
        {
            get { return (Vector3D)GetValue(DirectionYProperty); }
            set { SetValue(DirectionYProperty, value); }
        }

        // Using a DependencyProperty as the backing store for DirectionY.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DirectionYProperty =
         DependencyPropertyEx.Register<Vector3D, CircleStubModel3D>(nameof(DirectionY), new Vector3D(), (s, e) => s.AppearanceChanged(e));



        protected override void AppearanceChanged(DependencyPropertyChangedEventArgs e)
        {

            var gb = new MeshBuilder();

            var vector = new Vector3D(Direction.X / Direction.Length, Direction.Y / Direction.Length, Direction.Z / Direction.Length);

            gb.AddCone(Position, vector, Size, Size, 1, true, true, this.ThetaDiv);
            GeometryModel3D.Geometry = gb.ToMesh();
        }

        private Random random = new Random();
        Vector3D RandomTangent(Vector3D normal)
        {
            normal.Normalize();
            var tangent = Vector3D.CrossProduct(normal, new Vector3D(-normal.Z, normal.X, normal.Y));
            var bitangent = Vector3D.CrossProduct(normal, tangent);
            var angle = random.NextDouble() * (Math.PI * 2) - Math.PI; // .Range(-Math.PI, Math.PI);
            return tangent * Math.Sin(angle) + bitangent * Math.Cos(angle);
        }
    }
}