using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

using geo = GeometRi;

using HelixToolkit.Wpf;

using WpfAppDatagridGroupingHeader.Extensions;
using WpfAppDatagridGroupingHeader.Helpers;

namespace WpfAppDatagridGroupingHeader
{

    internal class TeePipeModel3D : PipeModel3D
    {


        public Vector3D MiddlePipeDirection
        {
            get { return (Vector3D)GetValue(MiddlePipeDirectionProperty); }
            set { SetValue(MiddlePipeDirectionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MiddlePipeDirection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MiddlePipeDirectionProperty =
         DependencyPropertyEx.Register<Vector3D, TeePipeModel3D>(nameof(MiddlePipeDirection), new Vector3D(), (s, e) => s.AppearanceChanged(e));




        public Point3D MiddlePipeEndPoint
        {
            get { return (Point3D)GetValue(MiddlePipeEndPointProperty); }
            set { SetValue(MiddlePipeEndPointProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MiddlePipeEndPoint.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MiddlePipeEndPointProperty =
         DependencyPropertyEx.Register<Point3D, TeePipeModel3D>(nameof(MiddlePipeEndPoint), new Point3D(), (s, e) => s.AppearanceChanged(e));


        protected override void AppearanceChanged(DependencyPropertyChangedEventArgs e)
        {
            var gb = new MeshBuilder();

            gb.AddTube(path: new Point3D[] { StartPosition, EndPosition },
                   diameter: this.Diametr,
                   thetaDiv: ThetaDiv,
                   isTubeClosed: false,
                   TubeStabs == TubeStabs.FrontCap || TubeStabs == TubeStabs.All,
                   TubeStabs == TubeStabs.BackCap || TubeStabs == TubeStabs.All
                   );

           var  middleposition = (EndPosition - StartPosition) / 2;

            gb.AddTube(path: new Point3D[] { StartPosition, EndPosition },
                   diameter: this.Diametr,
                   thetaDiv: ThetaDiv,
                   isTubeClosed: false,
                   TubeStabs == TubeStabs.FrontCap || TubeStabs == TubeStabs.All,
                   TubeStabs == TubeStabs.BackCap || TubeStabs == TubeStabs.All
                   );
            gb.AddTube(path: new Point3D[] { middleposition.ToPoint3D(), MiddlePipeEndPoint },
                diameter: this.Diametr,
                thetaDiv: ThetaDiv,
                isTubeClosed: false);

            GeometryModel3D.Material = this.Material;// MaterialHelper.CreateMaterial(Brushes.Red);
            GeometryModel3D.BackMaterial = this.BackMaterial;
            GeometryModel3D.Geometry = gb.ToMesh(); 
        }

    }
    internal class CurvedPipeModel3D : PipeModel3D
    {




        public Vector3D StartDirection
        {
            get { return (Vector3D)GetValue(StartDirectionProperty); }
            set { SetValue(StartDirectionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for StartDirection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StartDirectionProperty =
        DependencyPropertyEx.Register<Vector3D, CurvedPipeModel3D>(nameof(StartDirection), new Vector3D(0, 0, 0), (s, e) => s.AppearanceChanged(e));




        public Vector3D EndDirection
        {
            get { return (Vector3D)GetValue(EndDirectionProperty); }
            set { SetValue(EndDirectionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EndDirection.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EndDirectionProperty =
        //DependencyProperty.Register("EndDirection", typeof(Vector3D), typeof(CurvedPipeModel3D), new PropertyMetadata(0));
        DependencyPropertyEx.Register<Vector3D, CurvedPipeModel3D>(nameof(EndDirection), new Vector3D(0, 0, 0), (s, e) => s.AppearanceChanged(e));



        public double Length
        {
            get { return (double)GetValue(LengthProperty); }
            set { SetValue(LengthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Length.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LengthProperty =
        DependencyPropertyEx.Register<double, CurvedPipeModel3D>(nameof(Length), 0, (s, e) => s.AppearanceChanged(e));

        public Point3D QuadraticCurvedPosition
        {
            get { return (Point3D)GetValue(QuadraticCurvedPositionProperty); }
            set { SetValue(QuadraticCurvedPositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Position2.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty QuadraticCurvedPositionProperty =
            DependencyPropertyEx.Register<Point3D, CurvedPipeModel3D>(nameof(QuadraticCurvedPosition), new Point3D(10, 0, 0), (s, e) => s.AppearanceChanged(e));

        public CurvedPipeModel3D(Point3D startPosition, Vector3D startDirection, Vector3D endDirection, int length)
        {
            StartPosition = startPosition;
            StartDirection = startDirection;
            EndDirection = endDirection;
            Length = length;
        }

        private Bezier Bezier { get; set; }
        protected override void AppearanceChanged(DependencyPropertyChangedEventArgs e)
        {

            if (e.Property != null && (
                e.Property?.Name == nameof(EndPosition) ||
                e.Property?.Name == nameof(QuadraticCurvedPosition)))
            {
                return;
            }

            var s = new Ray3D(StartPosition, StartDirection);

            StartDirection.Normalize();
            EndDirection.Normalize();
            var vector = StartDirection + EndDirection;
            vector *= Length;
            var ep = StartPosition + vector;
            EndPosition = ep;

            var l = new geo.Line3d(StartPosition.ToGeometRIPoint3D(), StartDirection.ToGeometRIVector3D());
            var l2 = new geo.Line3d(EndPosition.ToGeometRIPoint3D(), EndDirection.ToGeometRIVector3D());
            var intersectionPoint = l.IntersectionWith(l2);

            if (intersectionPoint is geo.Point3d interPoint)
            {
                QuadraticCurvedPosition = interPoint.ToPoint3D();// new Point3D(interPoint.X, interPoint.Y, interPoint.Z); 

                Bezier = new Bezier(StartPosition, QuadraticCurvedPosition, ep, 100);

                var pts = Bezier.points.Select(x => x.ToPoint3D()).ToArray();

                var gb = new MeshBuilder();
                gb.AddTube(path: pts,
                  diameter: this.Diametr,
                  thetaDiv: ThetaDiv,
                  isTubeClosed: false
                  );

                GeometryModel3D.Material = this.Material;
                GeometryModel3D.BackMaterial = this.Material;
                GeometryModel3D.Geometry = gb.ToMesh();

            }

        }
        internal void Rotate(double angle)
        {
            var vector = this.Bezier.points[1] - this.Bezier.points[0];
            var rotation = new AxisAngleRotation3D(vector.ToVector3D(), angle);
            this.Transform = new RotateTransform3D(rotation);
            var newEndPosition = this.Transform.Transform(EndPosition);
        }
    }
}