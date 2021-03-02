using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;

using HelixToolkit.Wpf;

using Xceed.Words.NET;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace WpfAppDatagridGroupingHeader
{
    public class Person
    {
        [Category("Information")]
        [DisplayName("First Name")]
        [Description("This property uses a TextBox as the default editor.")]
        public string FirstName { get; set; }

        [Category("Information")]
        [DisplayName("Фамилия")]
        [Description("This property uses a TextBox as the default editor.")]
        public string LastName { get; internal set; }
        [Category("свойства")]
        [DisplayName("Фамилия")]
        [Description("This property uses a TextBox as the default editor.")]
        public double Weight { get; internal set; } = 200;
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>


    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<ItemModel3D<ItemModel>> Items { get; set; }
        //public ObservableCollection<TubeModel> Items { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            var res = new ObservableCollection<ItemModel>()

            {
                new ItemModel(1)
                {
                    c1 = "lol",
                    c2 = "kek",
                    c5 = new List<double>{1,2,3},
                    Spouse = new Person{FirstName="lol",LastName = "kek"},
                    Diametr  = 10,
                    StartPosition=new Point3D(0,0,0),
                    EndPosition=new Point3D(10,0,0),
                    Radius = 0,
                    Type = TubeTypes.Regular
                },
                new ItemModel(2)
                {
                    c1 = "2lol",
                    c2 = "2kek",
                    StartPosition=new Point3D(10,0,0),
                    EndPosition=new Point3D(20,0,0),
                    Type = TubeTypes.Regular

                },
                new ItemModel(3)
                {
                    c1 = "3lol",
                    c2 = "3kek",
                    StartPosition=new Point3D(20,0,0),
                    EndPosition=new Point3D(20,30,0),
                    Type = TubeTypes.curved,
                    Radius =30
                },
                new ItemModel(4)
                {
                    c1 = "4lol",
                    c2 = "4kek",
                    StartPosition=new Point3D(20,30,0),
                    EndPosition=new Point3D(20,40,0),
                    Type = TubeTypes.Regular,
                    Radius =30
                }
            };
            var allItmes = new List<ItemModel3D<ItemModel>>(res.Where(x => x.Type == TubeTypes.Regular).Select(x => new Tube3D(x)));
            allItmes.AddRange(res.Where(x => x.Type == TubeTypes.Regular).Select(x => new PipeModel3D(x)));
            //allItmes.Add()
            //allItmes.AddRange()
            //Items = new ObservableCollection<Tube3D>();
            //Items.Add()
            Items = new ObservableCollection<ItemModel3D<ItemModel>>(allItmes);
            this.DataContext = this;
             
             

            //for (int i = 0; i < 100; i++)
            //{
            //    Items.Add(new Tube3D(i)
            //    {
            //        c1 = "lol",
            //        c2 = "kek",
            //        c3 = "www",
            //        c4 = 0.04,
            //        c5 = new List<double> { 1, 2, 3 },
            //        Spouse = new Person { FirstName = "lol", LastName = "kek" },
            //        StartPosition = new Point3D(10 * i, 0, 0),
            //        EndPosition = new Point3D(10 * i, 0, 5)
            //    }
            //        )
            //}

        }


        private bool godMode = false;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            godMode = !godMode;
            var v = godMode ? Visibility.Visible : Visibility.Collapsed;
            for (int i = 2; i < 4; i++)
            {
                datagrid1.Columns[i].Visibility = v;
            }
        }

        // private void datagrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        // {
        //     if (e.AddedItems.Count > 0)
        //     {
        //         _propGrid.SelectedObject = e.AddedItems[0];
        //
        //     }
        // }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            using (var document = DocX.Create("docname.docx"))
            {
                var temlplate = "DocumentWithTemplateTable.docx";
                document.ApplyTemplate(temlplate);

                document.ReplaceText("<$My grocery list$>", "wtf");

                var dt = document.Tables.FirstOrDefault(x => x.TableCaption == "GROCERY_LIST");
                var pattern = dt.Rows[1];
                for (int i = 0; i < 20; i++)
                {
                    var newrow = dt.InsertRow(pattern, dt.RowCount - 1);
                    newrow.ReplaceText("%PRODUCT_NAME%", i.ToString());
                }


                var pt = document.Tables.FirstOrDefault(x => x.TableCaption == "table with columns");
                //var temlplateColumnNumber = 2; //номер колонки где переменные 
                var temlplateColumnNumber = pt.ColumnCount - 1; //впринципе так как она последняя в таблице - мжно брать последнюю. 
                foreach (var item in Items) // my list - наш список из которого будем генерировать столбцы
                {
                    pt.InsertColumn();
                    foreach (var row in pt.Rows)//обрабатываем построчно
                    {
                        var templateCellValue = row.Cells[temlplateColumnNumber].Xml.Value; //копируем значение из темплейт  
                        var newCell = row.Cells[row.Cells.Count - 1];                       //столбца в  новый
                        newCell.Paragraphs.First().Append(templateCellValue);               //для удобства дальнейшей обработки


                        newCell.ReplaceText(templateCellValue, GetValueByTemplate(templateCellValue, item.InnerValue)); // заменяем текст в новом столбце

                    }
                }
                //pt.RemoveColumn(temlplateColumnNumber);
                //var tamplaterow = pt.Rows.FirstOrDefault();
                //tamplaterow.
                //pt.


                //                Name
                //    price       First<first>

                //    last<last>
                //count<price>
                document.Save();
            }
        }

        private string GetValueByTemplate(string templateCellValue, ItemModel item)
        {
            switch (templateCellValue)
            {
                case "<first>":
                    return item.c1;
                case "<last>":
                    return item.c2;
                default:
                    return string.Empty;
            }
        }
        private object selectedObject;
        public object SelectedObject
        {
            get
            {
                return this.selectedObject;
            }

            set
            {
                this.SetValue(ref this.selectedObject, value, nameof(this.SelectedObject));
            }
        }

        public void Select(Visual3D visual)
        {
            this.SelectedObject = visual;
        }
        private void HelixViewport3D_MouseDown(object sender, MouseButtonEventArgs e)
        {

            var viewport = (HelixViewport3D)sender;
            var firstHit = viewport.Viewport.FindHits(e.GetPosition(viewport)).FirstOrDefault();
            if (firstHit != null)
            {
                if (firstHit.Visual is ItemModel3D<ItemModel>  model)
                {
                    var gModel = model.GeometryModel3D;
                    gModel.Material = gModel.Material == Materials.Green ? Materials.Gray : Materials.Green;
                    e.Handled = true;
                    this.Select(model);
                }
                if (firstHit.Visual is BillboardTextVisual3D bmodel)
                {


                    //var gModel = bmodel.geometryModel;
                    //gModel.Material = gModel.Material == Materials.Green ? Materials.Gray : Materials.Green;
                    //e.Handled = true;
                    //this.Select(model);
                }

            }
            else
            {
                this.Select(null);
            }

        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName, object oldValue, object newValue)
        {
            RaisePropertyChanged(propertyName);
        }

        protected void RaisePropertyChanged(string propertyName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetValue<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            if (object.Equals(field, value))
            {
                return false;
            }

            T val = field;
            field = value;
            OnPropertyChanged(propertyName, val, value);
            return true;
        }

        [Conditional("DEBUG")]
        private void VerifyProperty(string propertyName)
        {
            GetType().GetTypeInfo().GetDeclaredProperty(propertyName);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Items.Add(new PipeModel3D(
            new ItemModel(3)
            {
                c1 = "3lol",
                c2 = "3kek",
                StartPosition = new Point3D(20, 0, 0),
                EndPosition = new Point3D(20, 30, 0),
                Type = TubeTypes.curved,
                Radius = 30
            }));

            //var lastItems = Items.Last();
            //Items.Add(new Tube3D(
            //    new ItemModel(Items.Max(x => x.InnerValue.ID) + 1)
            //    {
            //        StartPosition = new Point3D(lastItems.InnerValue.StartPosition.X + 10, lastItems.InnerValue.StartPosition.Y, lastItems.InnerValue.StartPosition.Z),
            //        EndPosition = new Point3D(lastItems.InnerValue.EndPosition.X + 10, lastItems.InnerValue.EndPosition.Y, lastItems.InnerValue.EndPosition.Z)
            //    }));
        }

        private void RemoveClick(object sender, RoutedEventArgs e)
        {
            Items.RemoveAt(Items.Count - 1);
        }

        //private Xceed.Document.NET.Cell GetValueBytemplatName(Xceed.Document.NET.Cell cell, MyClass item)
        //{
        //    switch (cell)
        //    {
        //        default:
        //            break;
        //    }
        //}
        private GeometryModel3D GetModel(double radius, Vector3D normal, Point3D center, int resolution, double StartAngle, double EndAngle)
        {
            var mod = new GeometryModel3D();
            var geo = new MeshGeometry3D();

            // Generate the circle in the XZ-plane
            // Add the center first
            geo.Positions.Add(new Point3D(0, 0, 0));

            // Iterate from angle 0 to 2*PI
            double dev = (2 * Math.PI) / resolution;
            double thik = 0.02;
            //float spaceangle = StartAngle + 1;
            if (StartAngle != EndAngle)
            {
                for (double i = StartAngle; i < EndAngle; i += dev)
                {
                    geo.Positions.Add(new Point3D(radius * Math.Cos(i), 0, -radius * Math.Sin(i)));
                    geo.Positions.Add(new Point3D((radius - thik) * Math.Cos(i), 0, (-(radius - thik)) * Math.Sin(i)));
                }


                for (int i = 3; i < geo.Positions.Count; i += 1)
                {
                    geo.TriangleIndices.Add(i - 3);
                    geo.TriangleIndices.Add(i - 1);
                    geo.TriangleIndices.Add(i - 2);

                    geo.TriangleIndices.Add(i - 1);
                    geo.TriangleIndices.Add(i);
                    geo.TriangleIndices.Add(i - 2);
                }
            }


            mod.Geometry = geo;
            // Create transforms
            var trn = new Transform3DGroup();
            // Up Vector (normal for XZ-plane)
            var up = new Vector3D(0, 1, 0);
            // Set normal length to 1
            normal.Normalize();
            var axis = Vector3D.CrossProduct(up, normal); // Cross product is rotation axis
            var angle = Vector3D.AngleBetween(up, normal); // Angle to rotate
            trn.Children.Add(new RotateTransform3D(new AxisAngleRotation3D(axis, angle)));
            trn.Children.Add(new TranslateTransform3D(new Vector3D(center.X, center.Y, center.Z)));

            mod.Transform = trn;
            return mod;


        }

    }
}