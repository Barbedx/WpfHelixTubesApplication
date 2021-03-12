using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;

namespace WpfAppDatagridGroupingHeader.Models
{
    public class CurvedItemModel : ItemModel
    {
        public CurvedItemModel(int iD) : base(iD)
        {
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
    internal class FakePillarModel : ArrowItemModel
    {
        public FakePillarModel(int iD) : base(iD)
        {
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
        private double middlePipeDiameter;

        public Vector3D MiddlePipeDirection
        {
            get { return middlePipeDirection; }
            set { this.SetValue(ref middlePipeDirection, value); }
        }

        public double MiddlePipeDiameter
        {
            
            get { return middlePipeDiameter; }
            set { this.SetValue(ref middlePipeDiameter, value); }
            
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
    
    public class SquareStubModel : ItemModel
    {
        public SquareStubModel(int iD) : base(iD)
        {
        }
        private int xLength;

        public int XLength
        {
            get { return xLength; }
            set { this.SetValue(ref xLength, value); }
        }

        private int yLength;

        public int YLength
        {
            get { return yLength; }
            set { this.SetValue(ref yLength, value); }
        }

        private int zLength;

        public int ZLength
        {
            get { return zLength; }
            set { this.SetValue(ref zLength, value); }
        }


        private Vector3D direction;

        public Vector3D Direction
        {
            get { return direction; }
            set { this.SetValue(ref direction, value); }
        }
 
        private BoxFaces faces;
 

        public BoxFaces Faces {    get { return faces; }
            set { this.SetValue(ref faces, value); }
        }
        

    }
   

}