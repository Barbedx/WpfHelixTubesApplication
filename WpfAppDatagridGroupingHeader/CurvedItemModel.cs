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
            this.startDirection = startDirection;
            this.length = length;
            this.endDirection = endDirection;
        }

        public Vector3D startDirection { get; private set; }
        public double length { get; private set; }

        public Vector3D endDirection { get; private set; }
    }
}