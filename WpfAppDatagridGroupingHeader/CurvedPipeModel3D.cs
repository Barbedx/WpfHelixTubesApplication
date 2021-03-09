using System.Linq;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Media3D;

using geo = GeometRi;

using HelixToolkit.Wpf;

using WpfAppDatagridGroupingHeader.Extensions;
using WpfAppDatagridGroupingHeader.Helpers;
using System.Runtime.CompilerServices;

namespace WpfAppDatagridGroupingHeader
{
    internal class CurvedPipeModel3D : PipeModel3D
    { 

        private Vector3D startDirection;

        public CurvedPipeModel3D(Point3D startPosition, Vector3D startDirection
            ,Vector3D endDirection
            , double length,
            double diameter, TubeStabs tubeStabs = TubeStabs.None)
            : base(startPosition, new Point3D(), diameter, tubeStabs)
        {
            this.startDirection = startDirection;
            this.length = length;
            this.endDirection = endDirection;
        }

        public Vector3D StartDirection
        {
            get { return startDirection; }
            set
            {
                if (startDirection != value)
                {
                    startDirection = value;
                    this.AppearanceChanged();
                };
            }
        }

        private Vector3D endDirection;

        public Vector3D EndDirection
        {
            get { return endDirection; }
            set
            {
                if (endDirection != value)
                {
                    endDirection = value;
                    this.AppearanceChanged();
                };
            }
        }

        private double length = 5;

        public double Length
        {
            get { return length; }
            set
            {
                if (length != value)
                {
                    length = value;
                    this.AppearanceChanged();
                };
            }
        }

        private Point3D quadraticCurvedPosition;

        public Point3D QuadraticCurvedPosition
        {
            get { return quadraticCurvedPosition; }
            private set
            {
                quadraticCurvedPosition = value;
            }
        }
 
        //private Bezier Bezier { get; set; }
        protected override void AppearanceChanged([CallerMemberName] string caller = null)
        {
            if (caller == nameof(EndPosition))
            {
                return;
            }
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
               quadraticCurvedPosition= interPoint.ToPoint3D();// new Point3D(interPoint.X, interPoint.Y, interPoint.Z); 

                var Bezier = new Bezier(StartPosition, QuadraticCurvedPosition, ep, 100);

                var pts = Bezier.points.Select(x => x.ToPoint3D()).ToArray();

                var gb = new MeshBuilder();
                gb.AddTube(path: pts,
                  diameter: this.Diameter,
                  thetaDiv: ThetaDiv,
                  isTubeClosed: false
                  );

                GeometryModel3D.Geometry = gb.ToMesh();

            }

        }
        //internal void Rotate(double angle)
        //{
        //    var vector = this.Bezier.points[1] - this.Bezier.points[0];
        //    var rotation = new AxisAngleRotation3D(vector.ToVector3D(), angle);
        //    this.Transform = new RotateTransform3D(rotation);
        //    var newEndPosition = this.Transform.Transform(EndPosition);
        //}
    }
}