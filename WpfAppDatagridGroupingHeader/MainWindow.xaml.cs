using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;
using HelixToolkit.Wpf;
using WpfAppDatagridGroupingHeader.Models;
using WpfAppDatagridGroupingHeader.Models3D;

namespace WpfAppDatagridGroupingHeader
{
    public partial class MainWindow
    {
        private ViewModel ViewModel { get; }
 
        public MainWindow()
        {
            InitializeComponent(); 
            this.DataContext = this.ViewModel = new ViewModel();
        }
 
 
        private void HelixViewport3D_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var helixViewport3D = (HelixViewport3D)sender;
 
            var firstHit = helixViewport3D.Viewport.FindHits(e.GetPosition(helixViewport3D)).FirstOrDefault();
            if (firstHit != null)
            {
                switch (firstHit.Visual)
                {
                    case IItemModel3D<ItemModel> model:
                        e.Handled = true; 
                        ViewModel.Select(model.InnerModel);
                        break;
                    case BillboardTextVisual3D _:
                        break;
                }
            }
            else
            {
                ViewModel.Select(null);
            }
        }

 

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
             ViewModel.Items.Add(new ItemModel(2)
            {
                StartPosition = new Point3D(10, 0, 0),
                EndPosition = new Point3D(20, 0, 0)
            });
        }
  
        private void Add_valve_btn(object sender, RoutedEventArgs e)
        {
            ViewModel.Items.Add(new ValveItemModel(8)
            {
                StartPosition = new Point3D(-10, 0, 0),
                EndPosition = new Point3D(-20, 0, 0),
                Diameter = 5
            });
        }

        private void Add_Arrow_btn(object sender, RoutedEventArgs e)
        {
            ViewModel.Items.Add(new ArrowItemModel(6)
            {
                StartPosition = new Point3D(0, 0, 0),
                Offset = 5,
                Diameter = 0.5,
                Height = 5
            }); 
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            viewport.ZoomExtents(500);
        }
 
        private void Add_arrows_btn(object sender, RoutedEventArgs e)
        {
            ViewModel.Items.Add(new ThreeArrowItemModel(7)
            {
                StartPosition = new Point3D(5, 0, 5),
                Offset = 5,
                Diameter = 0.5,
                Height = 5,
                Direction = new Vector3D(0, 0, 10)
            }
                );
        }

        private void Add_Curved_pipe(object sender, RoutedEventArgs e)
        {
            ViewModel.Items.Add(
                new CurvedItemModel(3)
                {
                    StartPosition = new Point3D(20, 0, 0),
                    Length = 10,
                    StartDirection = new Vector3D(100, 0, 0),
                    EndDirection = new Vector3D(0, 100, 0),
                    TubeStabs = TubeStabs.All,
                    Diameter = 15
                });
        }

        private void Add_squere_stub(object sender, RoutedEventArgs e)
        { 
            ViewModel.Items.Add(new SquareStubModel(4)
            {
                StartPosition = new Point3D(10, 10, 0),
                XLength = 5,
                YLength = 5,
                ZLength = 5
            });
        }

        private void Add_tee_pipe(object sender, RoutedEventArgs e)
        {
            ViewModel.Items.Add(new TeePipeItemModel(5)
            {
                StartPosition = new Point3D(0, 0, 0),
                EndPosition = new Point3D(-10, 0, 0),
                MiddlePipeEndPosition = new Point3D(5, 0, 5)
            });
            ViewModel.Items.Clear();
        }
        private void Add_FakePillow(object sender, RoutedEventArgs e)
        { 
            DataTransfer.transferFromCore(ViewModel.Items);
        }
    }


    public static class DataTransfer {
        public static void transferFromCore(IList<ItemModel> list)
        {
            list.Add(new FakePillarModel(5)
            {
                StartPosition = new Point3D(0, 0, 0),
                Diameter = 1,
                Height = 5,
                Offset = 4
            });
        }

    }
}