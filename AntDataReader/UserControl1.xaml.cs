using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AntDataReader
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
            Render();
        }

        public void RotateX(double angle)
        {
            rotX.Angle = angle;
        }

        public void RotateY(double angle)
        {
            rotY.Angle = angle;
        }

        public void RotateZ(double angle)
        {
            rotZ.Angle = angle;
        }

        public void UpdateDisplay(double x, double y, double z)
        {
            mainViewport.Children.Clear();
            CubeBuilder cubeBuilder = new CubeBuilder(Color.FromRgb(0, 0, 255));
            mainViewport.Children.Add(cubeBuilder.Create(CubeBuilder.Direction.X, x));
            cubeBuilder.CubeColor = Color.FromRgb(0, 255, 0);
            mainViewport.Children.Add(cubeBuilder.Create(CubeBuilder.Direction.Y, y));
            cubeBuilder.CubeColor = Color.FromRgb(255, 0, 0);
            mainViewport.Children.Add(cubeBuilder.Create(CubeBuilder.Direction.Z, z));
        }

        private void Render()
        {
            CubeBuilder cubeBuilder = new CubeBuilder(Color.FromRgb(0,0,255));
            mainViewport.Children.Add(cubeBuilder.Create(CubeBuilder.Direction.X, 8.2));
            cubeBuilder.CubeColor = Color.FromRgb(0,255,0);
            mainViewport.Children.Add(cubeBuilder.Create(CubeBuilder.Direction.Y, -6.5));
            cubeBuilder.CubeColor = Color.FromRgb(255,0,0);
            mainViewport.Children.Add(cubeBuilder.Create(CubeBuilder.Direction.Z, 6));
        }
    }

}
