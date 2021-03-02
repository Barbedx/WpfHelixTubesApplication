using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;

using HelixToolkit.Wpf;

using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace WpfAppDatagridGroupingHeader
{
    public class Tube3D : ItemModel3D<ItemModel>
    {
        public static readonly DependencyProperty StartPositionProperty = DependencyPropertyEx.Register<Point3D, Tube3D>(nameof(StartPosition), new Point3D(0, 0, 0), (s, e) => s.AppearanceChanged());
        public static readonly DependencyProperty EndPositionProperty = DependencyPropertyEx.Register<Point3D, Tube3D>(nameof(EndPosition), new Point3D(10, 0, 0), (s, e) => s.AppearanceChanged());
         
        
        public double InnerDiametr
        {
            get { return (double)GetValue(InnerDiametrProperty); }
            set
            {
                SetValue(InnerDiametrProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for InnerDiametr.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InnerDiametrProperty =
            DependencyPropertyEx.Register<double, Tube3D>(nameof(InnerDiametr), 8, (s, e) => s.AppearanceChanged());

         
        public double Diametr
        {
            get { return (double)GetValue(DiametrProperty); }
            set { SetValue(DiametrProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Diametr.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DiametrProperty =
            DependencyPropertyEx.Register<double, Tube3D>(nameof(Diametr), 10, (s, e) => s.AppearanceChanged());
         
        protected override void AppearanceChanged()
        {

            var gb = new MeshBuilder();

            gb.AddPipe(
                point1: this.StartPosition,
                point2: this.EndPosition,
                diameter: this.Diametr,
                innerDiameter: this.InnerDiametr,
                thetaDiv:0
                );
            //geometryModel = GetModel(Diametr, new Vector3D(0, 0, 0), StartPosition, 10, 45, 45);
            //.Geometry = gb.ToMesh();
            geometryModel.Geometry = gb.ToMesh();


        }
         
        public readonly GeometryModel3D geometryModel = new GeometryModel3D();
       
        public Tube3D(ItemModel innerValue) :base(innerValue)
        {
            this.StartPosition = innerValue.StartPosition;
            this.EndPosition = innerValue.EndPosition;
            
            var gb = new MeshBuilder();

            gb.AddPipe(
                            point1: this.StartPosition,
                point2: this.EndPosition,
                diameter: this.Diametr,
                innerDiameter: this.InnerDiametr,
                thetaDiv: 10
                ); 
            geometryModel.Geometry = gb.ToMesh();
            geometryModel.Material = MaterialHelper.CreateMaterial(Brushes.Blue, freeze: false);
              
            this.Visual3DModel = geometryModel;// group;
 
        }
         
         



        //[Category("Conections")]
        //[Description("This property is a complex property and has no default editor.")]
        //[ExpandableObject]
        //public Person Spouse { get; set; }

        public Point3D StartPosition
        {
            get { return (Point3D)this.GetValue(StartPositionProperty); }
            set { this.SetValue(StartPositionProperty, value); }
        }
        public Point3D EndPosition
        {
            get { return (Point3D)this.GetValue(EndPositionProperty); }
            set { this.SetValue(EndPositionProperty, value); }
        }
         
    }
}
