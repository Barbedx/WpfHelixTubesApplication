using System.Windows.Media.Media3D;

namespace WpfAppDatagridGroupingHeader
{
    public class CurvedItemModel : ItemModel
    {
        public CurvedItemModel(int iD) : base(iD)
        {
        }
        public CurvedItemModel(int id, Vector3D startDirection, double length, Vector3D endDirection):base(id)
        {
            this.StartDirection = startDirection;
            this.Length = length;
            this.EndDirection = endDirection;
        } 
        public Vector3D StartDirection { get;   set; }
        public double Length { get;   set; }
   
        public Vector3D EndDirection { get;   set; }
    }

    public class ArrowItemModel : ItemModel
    {
        public ArrowItemModel(int iD) : base(iD)
        {
        }

        public double Height { get; set; }
        public double Offset { get; set; }
    }
    public class CircleStubModel : ItemModel
    {
        public CircleStubModel(int iD) : base(iD)
        {
        }

        public Vector3D Direction { get; set; }
    }
    public class TeePipeItemModel : ItemModel
    {
        public TeePipeItemModel(int iD) : base(iD)
        {
        }

        public Point3D MiddlePipeEndPoint { get; set; }
        public Vector3D MiddlePipeDirection { get; set; }
    }
    public class ThreeArrowItemModel : ArrowItemModel
    {
        public ThreeArrowItemModel(int iD) : base(iD)
        {
        }

        public Vector3D Direction { get; set; }
    }

    public class ValveItemModel : ItemModel
    {
        public ValveItemModel(int iD) : base(iD)
        {
        }
    }
    

}