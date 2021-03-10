using System.Windows.Media.Media3D;

using HelixToolkit.Wpf;

using WpfAppDatagridGroupingHeader.Extensions;

namespace WpfAppDatagridGroupingHeader
{
    internal class TeePipeModel3D : PipeModel3D<TeePipeItemModel>
    {
 
        public Vector3D MiddlePipeDirection => InnerModel.MiddlePipeDirection;
        
         
        public Point3D MiddlePipeEndPoint => InnerModel.MiddlePipeEndPosition;
         

        public override void AppearanceChanged(string caller = null)
        {
            var gb = new MeshBuilder();

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

        public TeePipeModel3D(TeePipeItemModel model) : base(model)
        {
        }
    }
}