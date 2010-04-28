using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Media3D;
using System.Windows.Media;

namespace AntDataReader
{
    /// <summary>
    /// Builds a 3D WPF rectangle
    /// </summary>
    public class CubeBuilder
    {
        private Color _color;

        /// <summary>
        /// The color of the rectangle
        /// </summary>
        public Color CubeColor
        {
            get { return _color; }
            set { _color = value; }
        }

        /// <summary>
        /// Sets the color of the cube
        /// </summary>
        /// <param name="color">The color to set</param>
        public CubeBuilder(Color color)
        {
            _color = color;
        }

        /// <summary>
        /// Represents a direction to draw the rod
        /// </summary>
        public enum Direction {
            X,
            Y,
            Z
        }

        /// <summary>
        /// Creates the cube
        /// </summary>
        /// <returns>The cube object</returns>
        public ModelVisual3D Create(Direction dir, double length)
        {
            Model3DGroup cube = new Model3DGroup();

            double xVal = 0;
            double yVal = 0;
            double zVal = 0;

            //Set the side lengths
            switch (dir)
            {
                case Direction.X:
                    xVal = length;
                    yVal = 2;
                    zVal = 2;
                    break;
                case Direction.Z:
                    xVal = 2;
                    yVal = length;
                    zVal = 2;
                    break;
                case Direction.Y:
                    xVal = 2;
                    yVal = 2;
                    zVal = length;
                    break;
            }

            //create the rectangular prism side points
            Point3D p0 = new Point3D(0, 0, 0);
            Point3D p1 = new Point3D(xVal, 0, 0);
            Point3D p2 = new Point3D(xVal, 0, zVal);
            Point3D p3 = new Point3D(0, 0, zVal);
            Point3D p4 = new Point3D(0, yVal, 0);
            Point3D p5 = new Point3D(xVal, yVal, 0);
            Point3D p6 = new Point3D(xVal, yVal, zVal);
            Point3D p7 = new Point3D(0, yVal, zVal);

            //front
            cube.Children.Add(CreateTriangle(p3, p2, p6));
            cube.Children.Add(CreateTriangle(p3, p6, p7));

            //right
            cube.Children.Add(CreateTriangle(p2, p1, p5));
            cube.Children.Add(CreateTriangle(p2, p5, p6));

            //back
            cube.Children.Add(CreateTriangle(p1, p0, p4));
            cube.Children.Add(CreateTriangle(p1, p4, p5));

            //left
            cube.Children.Add(CreateTriangle(p0, p3, p7));
            cube.Children.Add(CreateTriangle(p0, p7, p4));

            //top
            cube.Children.Add(CreateTriangle(p7, p6, p5));
            cube.Children.Add(CreateTriangle(p7, p5, p4));

            //bottom
            cube.Children.Add(CreateTriangle(p2, p3, p0));
            cube.Children.Add(CreateTriangle(p2, p0, p1));

            ModelVisual3D model = new ModelVisual3D();
            model.Content = cube;
            return model;
        }

        /// <summary>
        /// Creates triangles given corner points
        /// </summary>
        /// <param name="p0">The first point</param>
        /// <param name="p1">The second point</param>
        /// <param name="p2">The third point</param>
        /// <returns>The triangle as a model</returns>
        public Model3DGroup CreateTriangle(Point3D p0, Point3D p1, Point3D p2)
        {
            MeshGeometry3D mesh = new MeshGeometry3D();
            mesh.Positions.Add(p0);
            mesh.Positions.Add(p1);
            mesh.Positions.Add(p2);
            mesh.TriangleIndices.Add(0);
            mesh.TriangleIndices.Add(1);
            mesh.TriangleIndices.Add(2);

            Vector3D normal = VectorHelper.CalcNormal(p0, p1, p2);
            mesh.Normals.Add(normal);
            mesh.Normals.Add(normal);
            mesh.Normals.Add(normal);

            Material material = new DiffuseMaterial(
                new SolidColorBrush(_color));
            GeometryModel3D model = new GeometryModel3D(
                mesh, material);
            Model3DGroup group = new Model3DGroup();
            group.Children.Add(model);
            return group;
        }
    }

    /// <summary>
    /// Contains vector manipulation functions
    /// </summary>
    public class VectorHelper
    {
        /// <summary>
        /// Calculates the normal vector to a point
        /// </summary>
        /// <param name="p0">The first point</param>
        /// <param name="p1">The second point</param>
        /// <param name="p2">The third point</param>
        /// <returns>The normal vector</returns>
        public static Vector3D CalcNormal(Point3D p0, Point3D p1, Point3D p2)
        {
            Vector3D v0 = new Vector3D(p1.X - p0.X, p1.Y - p0.Y, p1.Z - p0.Z);
            Vector3D v1 = new Vector3D(p2.X - p1.X, p2.Y - p1.Y, p2.Z - p1.Z);
            return Vector3D.CrossProduct(v0, v1);
        }
    }

}
