using System.Windows.Media.Media3D;

using HelixToolkit.Wpf;

using WpfAppDatagridGroupingHeader.Extensions;

namespace WpfAppDatagridGroupingHeader
{
    internal class TeePipeModel3D : PipeModel3D
    {

        private Vector3D middlePipeDirection  ;
        
        public TeePipeModel3D(Point3D startPosition,
            Point3D endPosition, double diameter, Vector3D middlePipeDirection, double middlePipeLength, 
            TubeStabs tubeStabs = TubeStabs.None) : base(startPosition,
            endPosition, diameter, tubeStabs)
        {
            this.middlePipeDirection = middlePipeDirection;
            this.middlePipeEndPoint =  ((endPosition - startPosition)/2 + middlePipeDirection).ToPoint3D();
            
        }
        public TeePipeModel3D(Point3D startPosition,
            Point3D endPosition, double diameter,  Point3D middlePipeEndPoint,
            TubeStabs tubeStabs = TubeStabs.None) : base(startPosition,
            endPosition, diameter, tubeStabs)
        {
            this.middlePipeDirection = middlePipeDirection;
            this.middlePipeEndPoint = middlePipeEndPoint;
        }

        public Vector3D MiddlePipeDirection
        {
            get { return middlePipeDirection; }
            set
            {
                if (middlePipeDirection != value)
                {
                    middlePipeDirection = value;
                    this.AppearanceChanged();
                };
            }
        }

        private Point3D middlePipeEndPoint;

        public Point3D MiddlePipeEndPoint
        {
            get { return middlePipeEndPoint; }
            set
            {
                if (middlePipeEndPoint != value)
                {
                    middlePipeEndPoint = value;
                    this.AppearanceChanged();
                };
            }
        }

         

        protected override void AppearanceChanged(string caller = null)
        {
            var gb = new MeshBuilder();

            gb.AddTube(path: new Point3D[] { StartPosition, EndPosition },
                   diameter: this.Diameter,
                   thetaDiv: ThetaDiv,
                   isTubeClosed: false,
                   TubeStabs == TubeStabs.FrontCap || TubeStabs == TubeStabs.All,
                   TubeStabs == TubeStabs.BackCap || TubeStabs == TubeStabs.All
                   );


           gb.AddTube(path: new Point3D[] { StartPosition, EndPosition },
                   diameter: this.Diameter,
                   thetaDiv: ThetaDiv,
                   isTubeClosed: false,
                   TubeStabs == TubeStabs.FrontCap || TubeStabs == TubeStabs.All,
                   TubeStabs == TubeStabs.BackCap || TubeStabs == TubeStabs.All
                   );

           
           var middlePosition = (EndPosition - StartPosition) / 2;
           
           gb.AddTube(path: new Point3D[] { middlePosition.ToPoint3D(), MiddlePipeEndPoint },
                diameter: this.Diameter,
                thetaDiv: ThetaDiv,
                isTubeClosed: false);
             
            GeometryModel3D.Geometry = gb.ToMesh(); 
        }

    }
}