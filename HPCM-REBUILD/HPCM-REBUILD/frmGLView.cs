using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tao.OpenGl;
using Tao.Platform.Windows;
using Tao.FreeGlut;

namespace HPCM_REBUILD
{
    public partial class Visualizer : Form
    {
        int height = 500;
        int width = 1000;

        
        public Visualizer()
        {
            InitializeComponent();
            this.OGLControl.InitializeContexts();

            Gl.glEnable(Gl.GL_DEPTH_TEST);
            Gl.glEnable(Gl.GL_COLOR_MATERIAL);
            Gl.glEnable(Gl.GL_LIGHTING);
            Gl.glEnable(Gl.GL_LIGHT0);
            Gl.glEnable(Gl.GL_NORMALIZE);
            Gl.glShadeModel(Gl.GL_SMOOTH);

            Gl.glViewport(100, 100, width, height);
            Gl.glMatrixMode(Gl.GL_PROJECTION);

            Gl.glLoadIdentity();
            Glu.gluPerspective(45,(double)width / (double)height, 0.01,5000);

            LoadTerrain(1);
            ComputeNormals();



        }

        private void timer3dVisualizer_Tick(object sender, EventArgs e)
        {
            angle += 2;

            if (angle > 360) {

                angle =angle-360;
            }

            this.OGLControl.Invalidate();
        }

        int angle = 30;
        

        double[,] terrainHeights = new double[Meteorology.NumberOfMetCellsY, Meteorology.NumberOfMetCellsX];

        int maxJ=0;
        int maxI = 0;


        private void LoadTerrain(float heightScale) {

            CalculateMinimumTerrainHeight();
      
            maxJ = Meteorology.NumberOfMetCellsY;
            maxI = Meteorology.NumberOfMetCellsX;

            int rowCount = maxJ;
            for (int j = 0; j < maxJ; j++)
            {

                rowCount -= 1;
                for (int i = 0; i < maxI; i++)
                {
                    terrainHeights[rowCount, i] = (Geography.terrainHeights[j, i] - minimumTerrainHeight) / minimumTerrainHeight * heightScale;
                }

         
            }

        }

        double minimumTerrainHeight = 5000;



        private void CalculateMinimumTerrainHeight() {


            for (int j = 0; j < Meteorology.NumberOfMetCellsY; j++)
            {

                for (int i = 0; i < Meteorology.NumberOfMetCellsX; i++)
                {

                    if (minimumTerrainHeight > terrainHeights[j, i])
                    {

                        minimumTerrainHeight = terrainHeights[j, i];
                    }

                }
            }

            if (minimumTerrainHeight == 0) {

                minimumTerrainHeight = 1;
            }
        
        }


        Vector[,] surfaceNormals;
        private void ComputeNormals() 
        {


            surfaceNormals = new Vector[maxJ, maxI];

            Vector left=new Vector();
            Vector right=new Vector();
            Vector up=new Vector();
            Vector down = new Vector() ;
         

            for (int j = 0; j < maxJ; j++) {

                for (int i = 0; i < maxI; i++) {
                    if (i > 0)
                    {
                        left = new Vector(-1, (terrainHeights[j, i - 1] - terrainHeights[j, i]), 0);
                    }
                    if (i < maxI-1)
                    {
                         right = new Vector(1, (terrainHeights[j, i + 1] - terrainHeights[j, i]), 0);
                    }
                    if (j < maxJ-1)
                    {
                        up = new Vector(0, (terrainHeights[j + 1, i] - terrainHeights[j, i]), 1);
                    }
                    if (j > 0)
                    {
                        down = new Vector(0, (terrainHeights[j - 1, i] - terrainHeights[j, i]), -1);
                    }


                    if (i == 0 && j == 0) {

                        surfaceNormals[j,i] = Vector.CrossProduct( up,right);

                    }
                    else if (i == 0) {

                        if (j == maxJ - 1)
                        {                    
                            surfaceNormals[j, i] = Vector.CrossProduct( right,down);

                        }
                        else {

                            Vector n1 = Vector.CrossProduct(up,right);
                            Vector n2 = Vector.CrossProduct(down,right);
                            surfaceNormals[j, i] = new Vector((n1.X + n2.X) / 2, (n1.Y + n2.Y) / 2, (n1.Z + n2.Z) / 2);
                        }

                    }
                    else if (j == maxJ - 1 && i == maxI - 1) {

                        surfaceNormals[j, i] = Vector.CrossProduct( down,left);
                    }
                    else if (j == 0) {

                        if (i == maxI - 1) {

                            surfaceNormals[j, i] = Vector.CrossProduct(left,up);
                        }
                        else {
                            Vector n1 = Vector.CrossProduct( up,right);
                            Vector n2 = Vector.CrossProduct(left,up);
                            surfaceNormals[j, i] = new Vector((n1.X + n2.X) / 2, (n1.Y + n2.Y) / 2, (n1.Z + n2.Z) / 2);
                        
                        }
                    }
                    else if (j == maxJ - 1) {

                        Vector n1 = Vector.CrossProduct(right,down);
                        Vector n2 = Vector.CrossProduct(down,left);
                        surfaceNormals[j, i] = new Vector((n1.X + n2.X) / 2, (n1.Y + n2.Y) / 2, (n1.Z + n2.Z) / 2);
                    }
                    else if (i == maxI - 1)
                    {

                        Vector n1 = Vector.CrossProduct(left,up);
                        Vector n2 = Vector.CrossProduct(down,left);

                        surfaceNormals[j, i] = new Vector((n1.X + n2.X) / 2, (n1.Y + n2.Y) / 2, (n1.Z + n2.Z) / 2);
                    }
                    else {

                        Vector n1 = Vector.CrossProduct(up,right);
                        Vector n2 = Vector.CrossProduct( left,up);
                        Vector n3 = Vector.CrossProduct(down,left);
                        Vector n4 = Vector.CrossProduct(right,down);

                        surfaceNormals[j, i] = new Vector((n1.X + n2.X + n3.X + n4.X) / 4, (n1.Y + n2.Y + n3.Y + n4.Y) / 4, (n1.Z + n2.Z + n3.Z + n4.Z) / 4);
                    
                    }
                  


                
                }
            
            
            
            }
        
        
        }

        private void OGLControl_Paint(object sender, PaintEventArgs e)
        {

            Gl.glClear(Gl.GL_COLOR_BUFFER_BIT | Gl.GL_DEPTH_BUFFER_BIT);
            Gl.glMatrixMode(Gl.GL_MODELVIEW);
            Gl.glLoadIdentity();

            Gl.glTranslatef(0.0f, 0.0f, -10.0f);
            Gl.glRotatef(30.0f, 30.0f, 0.0f, 0.0f);
            Gl.glRotatef(angle, 0.0f, 1.0f, 0.0f);

            float[] ambientColor = new float[] { 0.4f, 0.4f, 0.4f, 1.0f };
            Gl.glLightModelfv(Gl.GL_LIGHT_MODEL_AMBIENT, ambientColor);

            float[] lightColor0 = new float[] { 0.6f, 0.6f, 0.6f, 1.0f };
            float[] lightPos0 = new float[] { -0.5f, 0.8f, 0.1f, 0.0f };
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_DIFFUSE, lightColor0);
            Gl.glLightfv(Gl.GL_LIGHT0, Gl.GL_POSITION, lightPos0);

            float scale = 8f / (maxJ - 1);
            Gl.glScalef(scale, scale, scale);

            //Gl.glScalef(8f, 8f, 8f);
            Gl.glTranslatef(-(float)(maxI - 1) / 2, 0, -(float)(maxI - 1) / 2);

            Gl.glColor3f(0.3f, 0.9f, 0.0f);




            for (int j = 0; j < maxJ - 1; j++)
            {
                Gl.glBegin(Gl.GL_TRIANGLE_STRIP);
                for (int i = 0; i < maxI; i++)
                {

                    Gl.glNormal3d(surfaceNormals[j, i].X, surfaceNormals[j, i].Y, surfaceNormals[j, i].Z);
                    Gl.glVertex3d(i, terrainHeights[j, i], j);
                    Gl.glNormal3d(surfaceNormals[j + 1, i].X, surfaceNormals[j + 1, i].Y, surfaceNormals[j + 1, i].Z);
                    Gl.glVertex3d(i, terrainHeights[j + 1, i], j + 1);

                }
                Gl.glEnd();
            }
        }
    }
}
