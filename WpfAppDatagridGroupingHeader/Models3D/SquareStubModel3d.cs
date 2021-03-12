using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using WpfAppDatagridGroupingHeader.Extensions;

namespace WpfAppDatagridGroupingHeader
{
    public class SquareStubModel3d : ItemModel3D<SquareStubModel>
    {
        public SquareStubModel3d(SquareStubModel model) : base(model)
        {
        }
        private  Point3D Position => InnerModel.StartPosition;
        public Vector3D Direction => InnerModel.Direction;
        public Vector3D DirectionY => InnerModel.DirectionY;
        public double XLength => InnerModel.XLength;
        public double YLength => InnerModel.YLength;
        public double ZLength => InnerModel.ZLength;
        public BoxFaces Faces => InnerModel.Faces;
        public override void AppearanceChanged(string caller = null)
        {
            var gb = new MeshBuilder(); 
            var vector = Direction.GetNormalized();
            // var vectorY = Vector3D.CrossProduct(vector, new Vector3D(0, 0, 1).GetNormalized());// DirectionY.GetNormalized();
            Vector3D vectorOpposite = new Vector3D(0,0,1);
            if (vector.Z==0)
            {
                vectorOpposite=new Vector3D(0,0,1);
            }
            if (vector.X==0)
            {
                vectorOpposite = new Vector3D(1,0,0);
            }
            if (vector.Y==0)
            {
                vectorOpposite = new Vector3D(0,1,0);
            } 
            //var vectorOpposte = vector.Z != 0 ? new Vector3D(0, 1, 0) : new Vector3D(0, 0, 1);
            var vectorY = Vector3D.CrossProduct(vector, vectorOpposite.GetNormalized());// DirectionY.GetNormalized();

            gb.AddBox(Position, vector, vectorY, XLength, YLength, ZLength,Faces);
            GeometryModel3D.Geometry = gb.ToMesh();
        }
    }
}