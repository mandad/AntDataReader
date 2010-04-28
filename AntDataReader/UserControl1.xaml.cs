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
using System.Windows.Media.Media3D;

namespace AntDataReader
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class UserControl1 : UserControl
    {
        /// <summary>
        /// Initialized the control and renders the inital drawing
        /// </summary>
        public UserControl1()
        {
            InitializeComponent();
            Render();
        }

        /// <summary>
        /// Rotates the object around the X axis
        /// </summary>
        /// <param name="angle">The angle by which to rotate (Degrees)</param>
        public void RotateX(double angle)
        {
            rotX.Angle = angle;
        }

        /// <summary>
        /// Rotates the object around the Y axis
        /// </summary>
        /// <param name="angle">The angle by which to rotate (degrees)</param>
        public void RotateY(double angle)
        {
            rotY.Angle = angle;
        }

        /// <summary>
        /// Rotates the object around the Z axis
        /// </summary>
        /// <param name="angle">The angle by which to rotate (degrees)</param>
        public void RotateZ(double angle)
        {
            rotZ.Angle = angle;
        }

        /// <summary>
        /// Updates the positions
        /// </summary>
        /// <param name="x">The X direction acceleration</param>
        /// <param name="y">The Y direction acceleration</param>
        /// <param name="z">The Z direction acceleration</param>
        public void UpdateDisplay(double x, double y, double z)
        {
            mainViewport.Children.Clear();
            CubeBuilder cubeBuilder = new CubeBuilder(Color.FromRgb(0, 0, 255));
            mainViewport.Children.Add(cubeBuilder.Create(CubeBuilder.Direction.X, x * 3));
            cubeBuilder.CubeColor = Color.FromRgb(0, 255, 0);
            mainViewport.Children.Add(cubeBuilder.Create(CubeBuilder.Direction.Y, y * 3));
            cubeBuilder.CubeColor = Color.FromRgb(255, 0, 0);
            mainViewport.Children.Add(cubeBuilder.Create(CubeBuilder.Direction.Z, z * 3));
            ModelVisual3D lightSource = new ModelVisual3D();
            AmbientLight light = new AmbientLight(Color.FromRgb(255,255,255));
            lightSource.Content = light;
            mainViewport.Children.Add(lightSource);
        }

        /// <summary>
        /// Performs the initial rendering
        /// </summary>
        private void Render()
        {
            CubeBuilder cubeBuilder = new CubeBuilder(Color.FromRgb(0,0,255));
            mainViewport.Children.Add(cubeBuilder.Create(CubeBuilder.Direction.X, 6));
            cubeBuilder.CubeColor = Color.FromRgb(0,255,0);
            mainViewport.Children.Add(cubeBuilder.Create(CubeBuilder.Direction.Y, 6));
            cubeBuilder.CubeColor = Color.FromRgb(255,0,0);
            mainViewport.Children.Add(cubeBuilder.Create(CubeBuilder.Direction.Z, 6));
        }
    }

}
