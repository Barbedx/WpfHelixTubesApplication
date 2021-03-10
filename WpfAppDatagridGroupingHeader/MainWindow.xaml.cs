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
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public ObservableCollection<ItemModel> Items { get; set; }

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
                    c5 = new List<double> {1, 2, 3},
                    Diameter = 10,
                    StartPosition = new Point3D(0, 0, 0),
                    EndPosition = new Point3D(10, 0, 0),
                    Radius = 0,
                    Type = TubeTypes.Regular
                }
                ,

                new ItemModel(2)
                {
                    c1 = "2lol",
                    c2 = "2kek",
                    StartPosition = new Point3D(10, 0, 0),
                    EndPosition = new Point3D(20, 0, 0),
                    Type = TubeTypes.Regular
                },
                new CurvedItemModel(3)
                {
                    c1 = "3lol",
                    c2 = "3kek",
                    StartPosition = new Point3D(20, 0, 0), 
                    Length = 10,
                    StartDirection = new Vector3D(100,0,0),
                    EndDirection = new Vector3D(0,100,0),
                    TubeStabs = TubeStabs.All,
                    Type = TubeTypes.curved,
                    Radius = 10,
                    Diameter =  15
                },
                new CircleStubModel(4)
                {
                    c1 = "4lol",
                    c2 = "4kek",
                    StartPosition = new Point3D(10, 10, 0),
                    Direction = new Vector3D(0,1,0)
                },
                new TeePipeItemModel(5)
                {
                    StartPosition = new Point3D(0,0,0),
                    EndPosition = new Point3D(-10,0,0),
                    MiddlePipeEndPoint =  new Point3D(5,0,5)
                },
                new ArrowItemModel(6)
                {
                    StartPosition = new Point3D(0,0,0),
                    Offset =  5,
                    Diameter =  0.5,
                    Height = 5
                },
                new ThreeArrowItemModel(7)
                {
                    StartPosition = new Point3D(5,0,5),
                    Offset =  5,
                    Diameter =  0.5,
                    Height = 5,
                    Direction = new Vector3D(0,0,10)
                },
                new ValveItemModel(8)
                {
                    StartPosition =  new Point3D(-10,0,0) ,
                    EndPosition = new Point3D(-20,0,0),
                    Diameter = 5
                }

            }; 
            
            Items = res;// new ObservableCollection<ItemModel>(allItems);
            this.DataContext = this;
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

        private ItemModel selectedObject;
        public ItemModel SelectedObject
        {
            get { return this.selectedObject; }

            set { this.SetValue(ref this.selectedObject, value, nameof(this.SelectedObject)); }
        }

        public void Select(ItemModel visual)
        {
            this.SelectedObject = visual;
        }

        private Visual3D selectedObject3D;
        public Visual3D SelectedObject3D
        {
            get { return this.selectedObject3D; }

            set { this.SetValue(ref this.selectedObject3D, value, nameof(this.selectedObject3D)); }
        }

        public void Select3D(Visual3D visual)
        {
            this.SelectedObject3D = visual;
        }

        private void HelixViewport3D_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var viewport = (HelixViewport3D) sender;
            var firstHit = viewport.Viewport.FindHits(e.GetPosition(viewport)).FirstOrDefault();
            if (firstHit != null)
            {
                if (firstHit.Visual is ItemModel3D<ItemModel> model)
                {
                    var gModel = model.GeometryModel3D;
                    gModel.Material = gModel.Material == Materials.Green ? Materials.Gray : Materials.Green;
                    e.Handled = true;
                    this.Select(model.InnerModel);
                    this.Select3D(model);
                }

                if (firstHit.Visual is BillboardTextVisual3D bmodel)
                {
                  this.Select3D(bmodel);
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
           Items.Add(  new ItemModel(2)
           {
               c1 = "2lol",
               c2 = "2kek",
               StartPosition = new Point3D(10, 0, 0),
               EndPosition = new Point3D(20, 0, 0),
               Type = TubeTypes.Regular
           });
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
             Items.Add(    new ValveItemModel(8)
             {
                 StartPosition =  new Point3D(-10,0,0) ,
                 EndPosition = new Point3D(-20,0,0),
                 Diameter = 5
             });
        }

        private void Add_Arrow_btn(object sender, RoutedEventArgs e)
        {
            Items.Add(new ArrowItemModel(6)
            {
                StartPosition = new Point3D(0, 0, 0),
                Offset = 5,
                Diameter = 0.5,
                Height = 5
            });
            //  Items.Add(new ArrowModel3D(new Point3D(0, 0, 0), 5));
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            viewport.ZoomExtents(500);
        }

        private void updown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
 
        }

        private void Add_arrows_btn(object sender, RoutedEventArgs e)
        {
           Items.Add(new ThreeArrowItemModel(7)
               {
                   StartPosition = new Point3D(5,0,5),
                   Offset =  5,
                   Diameter =  0.5,
                   Height = 5,
                   Direction = new Vector3D(0,0,10)
               }
               );
        }

        private void Add_Curved_pipe(object sender, RoutedEventArgs e)
        {
            Items.Add(
                new CurvedItemModel(3)
                {
                    c1 = "3lol",
                    c2 = "3kek",
                    StartPosition = new Point3D(20, 0, 0),
                    Length = 10,
                    StartDirection = new Vector3D(100, 0, 0),
                    EndDirection = new Vector3D(0, 100, 0),
                    TubeStabs = TubeStabs.All,
                    Type = TubeTypes.curved,
                    Radius = 10,
                    Diameter = 15
                });
        }

        private void Add_squere_stub(object sender, RoutedEventArgs e)
        {
             Items.Add( new CircleStubModel(4)
             {
                 c1 = "4lol",
                 c2 = "4kek",
                 StartPosition = new Point3D(10, 10, 0),
                 Direction = new Vector3D(0,1,0)
             });
        }

        private void Add_tee_pipe(object sender, RoutedEventArgs e)
        {
            Items.Add( new TeePipeItemModel(5)
            {
                StartPosition = new Point3D(0,0,0),
                EndPosition = new Point3D(-10,0,0),
                MiddlePipeEndPoint =  new Point3D(5,0,5)
            });
        }
    }
}