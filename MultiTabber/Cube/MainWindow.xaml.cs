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
using System.Windows.Media.Animation;

namespace Cube
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool down = false;

        public MainWindow()
        {
            InitializeComponent();
        }

        public void HitTest(object sender, System.Windows.Input.MouseButtonEventArgs args)
        {
            Point mouseposition = args.GetPosition(myViewport);
            Point3D testpoint3D = new Point3D(mouseposition.X, mouseposition.Y, 0);
            Vector3D testdirection = new Vector3D(mouseposition.X, mouseposition.Y, 1);
            PointHitTestParameters pointparams = new PointHitTestParameters(mouseposition);
            RayHitTestParameters rayparams = new RayHitTestParameters(testpoint3D, testdirection);

            //test for a result in the Viewport3D
            VisualTreeHelper.HitTest(myViewport, null, HTResult, pointparams);            
        }

        public HitTestResultBehavior HTResult(System.Windows.Media.HitTestResult rawresult)
        {
            //MessageBox.Show(rawresult.ToString());
            RayHitTestResult rayResult = rawresult as RayHitTestResult;

            if (rayResult != null)
            {
                RayMeshGeometry3DHitTestResult rayMeshResult = rayResult as RayMeshGeometry3DHitTestResult;

                if (rayMeshResult != null)
                {
                    GeometryModel3D hitgeo = rayMeshResult.ModelHit as GeometryModel3D;

                    UpdateResultInfo(rayMeshResult);
                    UpdateMaterial(hitgeo, (side1GeometryModel3D.Material as MaterialGroup));
                }
            }

            return HitTestResultBehavior.Continue;
        }

        public void UpdateMaterial(GeometryModel3D hitgeo, MaterialGroup changegroup)
        {
            MaterialGroup priorMaterial = hitgeo.Material as MaterialGroup;

            if (hitgeo.Geometry == (MeshGeometry3D)Application.Current.Resources["CubeSide01"])
            {
                //HitFaceInfo.Text = "CubeSide01";
                if (priorMaterial == (MaterialGroup)Application.Current.Resources["HitMaterial"])
                {
                    hitgeo.Material = (MaterialGroup)Application.Current.Resources["BranchesMaterial"];
                }
                else
                {
                    hitgeo.Material = (MaterialGroup)Application.Current.Resources["HitMaterial"];
                }
            }
            if (hitgeo.Geometry == (MeshGeometry3D)Application.Current.Resources["CubeSide02"])
            {
                //HitFaceInfo.Text = "CubeSide02";
                if (priorMaterial == (MaterialGroup)Application.Current.Resources["HitMaterial"])
                {
                    hitgeo.Material = (MaterialGroup)Application.Current.Resources["FlowersMaterial"];
                }
                else
                {
                    hitgeo.Material = (MaterialGroup)Application.Current.Resources["HitMaterial"];
                }
            }
            if (hitgeo.Geometry == (MeshGeometry3D)Application.Current.Resources["CubeSide03"])
            {
                //HitFaceInfo.Text = "CubeSide03";
                if (priorMaterial == (MaterialGroup)Application.Current.Resources["HitMaterial"])
                {
                    hitgeo.Material = (MaterialGroup)Application.Current.Resources["BerriesMaterial"];
                }
                else
                {
                    hitgeo.Material = (MaterialGroup)Application.Current.Resources["HitMaterial"];
                }
            }
            if (hitgeo.Geometry == (MeshGeometry3D)Application.Current.Resources["CubeSide04"])
            {
                //HitFaceInfo.Text = "CubeSide04";
                if (priorMaterial == (MaterialGroup)Application.Current.Resources["HitMaterial"])
                {
                    hitgeo.Material = (MaterialGroup)Application.Current.Resources["LeavesMaterial"];
                }
                else
                {
                    hitgeo.Material = (MaterialGroup)Application.Current.Resources["HitMaterial"];
                }
            }
            if (hitgeo.Geometry == (MeshGeometry3D)Application.Current.Resources["CubeSide05"])
            {
                //HitFaceInfo.Text = "CubeSide05";
                if (priorMaterial == (MaterialGroup)Application.Current.Resources["HitMaterial"])
                {
                    hitgeo.Material = (MaterialGroup)Application.Current.Resources["RocksMaterial"];
                }
                else
                {
                    hitgeo.Material = (MaterialGroup)Application.Current.Resources["HitMaterial"];
                }
            }
            if (hitgeo.Geometry == (MeshGeometry3D)Application.Current.Resources["CubeSide06"])
            {
                //HitFaceInfo.Text = "CubeSide06";
                if (priorMaterial == (MaterialGroup)Application.Current.Resources["HitMaterial"])
                {
                    hitgeo.Material = (MaterialGroup)Application.Current.Resources["SunsetMaterial"];
                }
                else
                {
                    hitgeo.Material = (MaterialGroup)Application.Current.Resources["HitMaterial"];
                }
            }

        }

        public void UpdateResultInfo(RayMeshGeometry3DHitTestResult rayMeshResult)
        {
            //HitVisualInfo.Text = rayMeshResult.VisualHit.ToString();
            //HitModelInfo.Text = rayMeshResult.ModelHit.ToString();
            //HitMeshInfo.Text = rayMeshResult.MeshHit.ToString();

            ////HitMaterialInfo.Text = (rayMeshResult.ModelHit as GeometryModel3D).Material.GetType().Name;
            ////HitMaterialBrushInfo.Text = ((rayMeshResult.ModelHit as GeometryModel3D).Material as DiffuseMaterial).Brush.ToString();

            //HitDistanceInfo.Text = rayMeshResult.DistanceToRayOrigin.ToString();
            //Vertex1Info.Text = (rayMeshResult.VertexWeight1 * 100) + "%";
            //Vertex2Info.Text = (rayMeshResult.VertexWeight2 * 100) + "%";
            //Vertex3Info.Text = (rayMeshResult.VertexWeight3 * 100) + "%";
        }

        private void DockPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (down)
            {
               //myHorizontalRotation.
            }
        }

        private void DockPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            down = true;

            // Specify where in the 3D scene the camera is.
            myPerspectiveCamera.Position = new Point3D(0, 0, 2);

            // Specify the direction that the camera is pointing.
            myPerspectiveCamera.LookDirection = new Vector3D(1, 0, -1);

            // Define camera's horizontal field of view in degrees.
            myPerspectiveCamera.FieldOfView = 60;

            myViewport.Camera = myPerspectiveCamera;
 
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            // Specify where in the 3D scene the camera is.
            myPerspectiveCamera.Position = new Point3D(Convert.ToDouble(textBox1.Text), Convert.ToDouble(textBox2.Text), Convert.ToDouble(textBox3.Text));

            // Specify the direction that the camera is pointing.
            myPerspectiveCamera.LookDirection = new Vector3D(Convert.ToDouble(textBox4.Text), Convert.ToDouble(textBox5.Text), Convert.ToDouble(textBox6.Text));

            // Define camera's horizontal field of view in degrees.
            myPerspectiveCamera.FieldOfView = Convert.ToDouble(textBox7.Text);

            myViewport.Camera = myPerspectiveCamera;
 
        }       
    }
}
