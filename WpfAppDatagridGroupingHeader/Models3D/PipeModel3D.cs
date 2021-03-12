using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

namespace WpfAppDatagridGroupingHeader.Models3D
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

        protected Point3D StartPosition => InnerModel.StartPosition;
 
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
                   TubeStabs == TubeStabs.FrontCap|| TubeStabs == TubeStabs.All,
                   TubeStabs == TubeStabs.BackCap|| TubeStabs == TubeStabs.All
                   );
            GeometryModel3D.Geometry = gb.ToMesh();
        }


    }
}