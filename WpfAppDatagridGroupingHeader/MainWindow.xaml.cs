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
            var allItmes = new List<ItemModel3D<ItemModel>> (res.Where(x => x.Type == TubeTypes.Regular).Select(x => new PipeModel3D(x)));
            
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

                    this.Select(bmodel);
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
        private void Add_valve_btn(object sender, RoutedEventArgs e)
        {

            Items.Add(new ValveModel3D(
               new ItemModel(4)
               {
                   c1 = "3lol",
                   c2 = "3kek",
                   StartPosition = new Point3D(20, 0, 0),
                   EndPosition = new Point3D(20, 30, 0)
               }));
        }

        private void Add_Arrow_btn(object sender, RoutedEventArgs e)
        {
            Items.Add(new ArrowModel3D(
               new ItemModel(4)
               {
                   c1 = "3lol",
                   c2 = "3kek",
                   StartPosition = new Point3D(20, 0, 0),
                   EndPosition = new Point3D(20, 30, 0)
               }));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            viewport.ZoomExtents(500);

        }

        private void updown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (selectedObject is PipeModel3D pipe)
            {

                 pipe.Rotate(decimal.ToDouble(txt_updown.Value.Value));// = new RotateTransform3D(new AxisAngleRotation3D())
            }
        }
    }
}