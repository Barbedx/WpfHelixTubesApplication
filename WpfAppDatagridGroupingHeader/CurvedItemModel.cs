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
        private Vector3D startDirection;

        public Vector3D StartDirection
        {
            get { return startDirection; }
            set { this.SetValue(ref startDirection, value); }
        }
        private double length;

        public double Length
        {
            get { return length; }
            set { this.SetValue(ref length, value); }
        }

        private Vector3D endDirection;

        public Vector3D EndDirection
        {
            get { return endDirection; }
            set { this.SetValue(ref endDirection, value); }
        }

         
    }

    public class ArrowItemModel : ItemModel
    {
        public ArrowItemModel(int iD) : base(iD)
        {
        }
        private double height;

        public double Height
        {
            get { return height; }
            set { this.SetValue(ref height, value); }
        }

        private double offset;

        public double Offset
        {
            get { return offset; }
            set { this.SetValue(ref offset, value); }
        }
         
    }
    public class CircleStubModel : ItemModel
    {
        public CircleStubModel(int iD) : base(iD)
        {
        }
        private Vector3D direction;

        public Vector3D Direction
        {
            get { return direction; }
            set { this.SetValue(ref direction, value); }
        }
         
    }
    public class TeePipeItemModel : ItemModel
    {
        public TeePipeItemModel(int iD) : base(iD)
        {
        }

        private Point3D middlePipePosition;

        public Point3D MiddlePipeEndPosition
        {
            get { return middlePipePosition; }
            set { this.SetValue(ref middlePipePosition, value); }
        }

        private Vector3D middlePipeDirection;

        public Vector3D MiddlePipeDirection
        {
            get { return middlePipeDirection; }
            set { this.SetValue(ref middlePipeDirection, value); }
        }
         
    }
    public class ThreeArrowItemModel : ArrowItemModel
    {
        public ThreeArrowItemModel(int iD) : base(iD)
        {
        }

        private Vector3D direction;

        public Vector3D Direction
        {
            get { return direction; }
            set { this.SetValue(ref direction, value); }
        }

    }

    public class ValveItemModel : ItemModel
    {
        public ValveItemModel(int iD) : base(iD)
        {
        }
    }
    

}