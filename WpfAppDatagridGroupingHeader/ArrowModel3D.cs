using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media.Media3D;

using HelixToolkit.Wpf;

namespace WpfAppDatagridGroupingHeader
{
    internal class ArrowModel3D<T> : ItemModel3D<T> where T : ArrowItemModel

    {
    protected double Diameter => InnerModel.Diameter;
    public Point3D Position => InnerModel.StartPosition;

    public double Height => InnerModel.Height;

    private double offset = 5;

    public double Offset => InnerModel.Offset;


    public override void AppearanceChanged(string caller = null)
    {

        var ep = new Point3D(Position.X, Position.Y, Position.Z);
        var sp = new Point3D(Position.X, Position.Y, Position.Z - Offset - Height);
        var gb = new MeshBuilder();

        gb.AddArrow(sp, ep, Diameter, thetaDiv: this.ThetaDiv);
        gb.AddBox(sp, Diameter * 3, Diameter * 2, Diameter / 4);
        GeometryModel3D.Geometry = gb.ToMesh();

    }

    public ArrowModel3D(T model) : base(model)
    {
    }
    }

}