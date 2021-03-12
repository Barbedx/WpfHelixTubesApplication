using System.Windows;
using System.Windows.Media.Media3D;
using geo = GeometRi;

using HelixToolkit.Wpf;
using WpfAppDatagridGroupingHeader.Extensions;
using System.Numerics;
using System;

namespace WpfAppDatagridGroupingHeader
{
    internal class CircleStubModel3D : ItemModel3D<CircleStubModel>
    {
        private double Radius => InnerModel.Diameter/2;

        private Vector3D Direction => InnerModel.Direction;

        private Point3D Position => InnerModel.StartPosition; 
 



        public override void AppearanceChanged(string caller = null)
        {
            var gb = new MeshBuilder(); 
            var vector = Direction.GetNormalized(); 
            gb.AddCone(Position, vector, Radius,Radius, 1, true, true, this.ThetaDiv);
            GeometryModel3D.Geometry = gb.ToMesh();
        }

        public CircleStubModel3D(CircleStubModel model) : base(model)
        { 
        }
      
    }
}