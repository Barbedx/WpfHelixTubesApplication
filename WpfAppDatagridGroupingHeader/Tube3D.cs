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
    public class Tube3D : UIElement3D
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





        public int ThetaDiv
        {
            get { return (int)GetValue(ThetaDivProperty); }
            set { SetValue(ThetaDivProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ThetaDiv.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ThetaDivProperty =
            DependencyPropertyEx.Register<int, Tube3D>(nameof(ThetaDiv), 90, (s, e) => s.AppearanceChanged());



        public Point3D TextPosition
        {
            get { return (Point3D)GetValue(TextPositionProperty); }
            set { SetValue(TextPositionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextPosition.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextPositionProperty =
            DependencyPropertyEx.Register<Point3D, Tube3D>(nameof(TextPosition), new Point3D(0, 0, 0), (s, e) => s.AppearanceChanged());





        public string Caption
        {
            get { return (string)GetValue(CaptionProperty); }
            set { SetValue(CaptionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Caption.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CaptionProperty =
        DependencyPropertyEx.Register<string, Tube3D>(nameof(Caption), "noname", (s, e) => s.AppearanceChanged());





        //public double Diametr { get; set; } = 8;
        //public Point3D StartPosition { get; set; }
        //public Point3D EndPosition { get; set; }
        //public double InnerDiametr { get; set; } = 10;
        //public int ThetaDiv { get; set; }
        //public string Caption { get; set; }
        //public Point3D TextPosition { get; set; }

        private void AppearanceChanged()
        {

            var gb = new MeshBuilder();

            gb.AddPipe(
                point1: this.StartPosition,
                point2: this.EndPosition,
                diameter: this.Diametr,
                innerDiameter: this.InnerDiametr,
                thetaDiv: this.ThetaDiv
                );
            geometryModel.Geometry = gb.ToMesh();

            //this.geometryModel.Point1 = this.StartPosition;
            //this.geometryModel.Point2 = this.EndPosition;
            //this.geometryModel.Diameter = this.Diametr;

            //geometryModel.Point1 = this.StartPosition;
            //geometryModel.Point2 = this.EndPosition;
            //geometryModel.Diameter = this.Diametr;
            //geometryModel.InnerDiameter = this.InnerDiametr;
            //geometryModel.ThetaDiv = this.ThetaDiv;

           // billboardText.Text = Caption;
           // billboardText.Position = TextPosition;

        }
        public readonly GeometryModel3D geometryModel = new GeometryModel3D(); 

        public Tube3D( )
        {
         
           
            var gb = new MeshBuilder();

            gb.AddPipe(
                            point1: this.StartPosition,
                point2: this.EndPosition,
                diameter: this.Diametr,
                innerDiameter: this.InnerDiametr,
                thetaDiv: this.ThetaDiv
                );


            geometryModel.Geometry = gb.ToMesh();
            geometryModel.Material = MaterialHelper.CreateMaterial(Brushes.Blue, freeze: false);

             

            this.Visual3DModel =geometryModel;// group;


        }
        public Tube3D(int id  = 0)
        {
             
         
            var gb = new MeshBuilder();

            gb.AddPipe(
                            point1: this.StartPosition,
                point2: this.EndPosition,
                diameter: this.Diametr,
                innerDiameter: this.InnerDiametr,
                thetaDiv: this.ThetaDiv
                );


            geometryModel.Geometry = gb.ToMesh();
            geometryModel.Material = MaterialHelper.CreateMaterial(Brushes.Blue, freeze: false);

            //geometryModel =  new PipeVisual3D()
            //{
            //    Point1 = this.StartPosition,
            //    Point2 = this.EndPosition,
            //    Diameter = this.Diametr,
            //    Material = MaterialHelper.CreateMaterial(Brushes.Blue),
            //    InnerDiameter = this.InnerDiametr,
            //    ThetaDiv = this.ThetaDiv
            //};

            //geometryModel.Children.Add(billboardText);
            //this.MouseDown += Tube3D_MouseDown;  
            this.Visual3DModel = geometryModel;// group;

        }

        public Tube3D(TubeModel x)
        {
            this.StartPosition = x.StartPosition;
            this.EndPosition = x.EndPosition;
            
            var gb = new MeshBuilder();

            gb.AddPipe(
                            point1: this.StartPosition,
                point2: this.EndPosition,
                diameter: this.Diametr,
                innerDiameter: this.InnerDiametr,
                thetaDiv: this.ThetaDiv
                );


            geometryModel.Geometry = gb.ToMesh();
            geometryModel.Material = MaterialHelper.CreateMaterial(Brushes.Blue, freeze: false);

            //geometryModel =  new PipeVisual3D()
            //{
            //    Point1 = this.StartPosition,
            //    Point2 = this.EndPosition,
            //    Diameter = this.Diametr,
            //    Material = MaterialHelper.CreateMaterial(Brushes.Blue),
            //    InnerDiameter = this.InnerDiametr,
            //    ThetaDiv = this.ThetaDiv
            //};

            //geometryModel.Children.Add(billboardText);
            //this.MouseDown += Tube3D_MouseDown;
          
            var group = new Model3DGroup();
            group.Children.Add(geometryModel);
           
            this.Visual3DModel = group;// group;
            TubeModel = x;
        }

        public int Id => TubeModel.Id;
        public string c1 => TubeModel.c1;
        public string c2 => TubeModel.c2;
        public string c3 => TubeModel.c3;
        public double c4 => TubeModel.c4;

        public List<double> c5 => TubeModel.c5;



        [Category("Conections")]
        [Description("This property is a complex property and has no default editor.")]
        [ExpandableObject]
        public Person Spouse { get; set; }

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

        public TubeModel TubeModel { get; }
    }
}
