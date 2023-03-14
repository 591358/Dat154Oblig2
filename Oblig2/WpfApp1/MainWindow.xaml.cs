using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using SpaceSim;
using Color = System.Windows.Media.Color;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        List<Ellipse> ellipses = new List<Ellipse>();
        int loopCounter;
        private System.Windows.Threading.DispatcherTimer timer;
        private System.Windows.Controls.Canvas canvas;

        Random rand = new Random();

        double newLeft = 0;
        double newTop = 0;
        int i = 0;
        int j = 0;
        static int scale = 10;
        List<SpaceObject> solarSystem = new List<SpaceObject> {
            new Star("Sun", 696342 /scale, "Yellow",0),
            new Planet("Mercury", 2440, "Grey", 57910, 87.97, 59),
            new Planet("Venus", 6052, "Brown", 108200, 224.7, 243),
            new Planet("Terra", 6371, "Blue", 149600, 365.26, 1),
            new Planet("Mars", 3389, "Red", 227940, 686.98, 1.025),
            new Planet("Jupiter", 69911 / scale, "Brown", 778330/scale, 4332.71, 0.417),
            new Planet("Saturn", 58232 / scale, "Brown", 1429400/scale, 10759.5, 0.442),
            new Planet("Uranus", 25362 / scale, "AliceBlue", 2870990/ scale + 5, 30685, 0.708),
            new Planet("Neptune", 24764 / scale, "Blue", 4504300 / scale+ 10, 60190, 0.671),
            new DwarfPlanet("Pluto", 1185, "Brown", 5913520/scale + 20, 90550, 6.39)
           /* new Moon("The Moon", 1737, "Grey", 384, 27.32, 29.5),*/
        };



        public MainWindow()
        {

            InitializeComponent();

            //Initialize the timer class


            PaintCanvas.MouseWheel += new MouseWheelEventHandler(Canvas_MouseWheel);


            PaintCanvas.MouseDown += new MouseButtonEventHandler(Canvas_MouseButton);
            /* timer = new System.Windows.Threading.DispatcherTimer();
             timer.Interval = TimeSpan.FromSeconds(1); //Set the interval period here.
             timer.Tick += timer1_Tick;


             loopCounter = 5000;

             timer.Start();*/


        }
        private void myCanvas_Loaded(object sender, RoutedEventArgs e) => displaySolarSystem();
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e) => displaySolarSystem();

        const double ScaleRate = 1.1;
        private void Canvas_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            /* if (e.Delta > 0)
             {
                 st.ScaleX *= ScaleRate;
                 st.ScaleY *= ScaleRate;
             }
             else
             {
                 st.ScaleX /= ScaleRate;
                 st.ScaleY /= ScaleRate;
             }*/

        }

        double newscale = 1.0;
        private void Canvas_MouseButton(object sender, MouseButtonEventArgs e)
        {
            Ellipse el = (Ellipse)e.OriginalSource;

            /*  if (e.LeftButton > 0)
              {
                  st.ScaleX *= ScaleRate;
                  st.ScaleY *= ScaleRate;
              }
              else if (e.RightButton > 0)
              {
                  st.ScaleX /= ScaleRate;
                  st.ScaleY /= ScaleRate;
              }*/
            PaintCanvas.RenderTransform = null;

            var position = e.MouseDevice.GetPosition(PaintCanvas);

            if (e.LeftButton > 0)
            {

                newscale += 0.2;
            }
            else if (e.RightButton > 0)
            {
                newscale -= 0.2;

            }

            /* y = Canvas.GetTop(el);
             double x = Canvas.GetLeft(el);*/
            /*Canvas.GetTop(el)*/



            double distance = (PaintCanvas.ActualWidth / 2) - Canvas.GetLeft(el);

            for (int i = PaintCanvas.Children.Count - 1; i >= 0; i += -1)
            {
                UIElement Child = PaintCanvas.Children[i];

                Canvas.SetLeft(Child, Canvas.GetLeft(Child) + distance);


            }
            PaintCanvas.RenderTransform = new ScaleTransform(newscale, newscale, Canvas.GetLeft(el), Canvas.GetTop(el));


            /*PaintCanvas.RenderTransform = new ScaleTransform(newscale, newscale, X, Y);*/

            /*PaintCanvas.RenderTransformOrigin = new System.Windows.Point(0.5, 0.5);*/

        }


        private void displaySolarSystem()
        {

            for (int i = PaintCanvas.Children.Count - 1; i >= 0; i += -1)
            {
                UIElement Child = PaintCanvas.Children[i];

                PaintCanvas.Children.Remove(Child);
            }

            newTop = PaintCanvas.ActualHeight / 2;
            newLeft = PaintCanvas.ActualWidth / 2;
            solarSystem.ForEach(a =>
            {

                Ellipse ellipse = CreateAnEllipse(a.ObjRad / 350, a.ObjRad / 350);
                ellipses.Add(ellipse);
                if (a.Name.Equals("Sun"))
                {
                    Canvas.SetTop(ellipse, newTop - (ellipse.Height / 2));
                    Canvas.SetLeft(ellipse, newLeft - (ellipse.Width / 2));
                    newLeft = newLeft + (ellipse.Width / 2);
                }
                else
                {
                    if (a.GetType().Equals(typeof(Planet)) || a.GetType().Equals(typeof(DwarfPlanet)))
                    {
                        Planet p = (Planet)a;
                        newLeft += p.OrbRad / 4000;
                        Canvas.SetTop(ellipse, newTop - (ellipse.Height / 2));
                        Canvas.SetLeft(ellipse, newLeft - (ellipse.Width / 2));

                    };
                }

            });

        }



        private void timer1_Tick(object sender, EventArgs e)
        {
            //Remove the previous ellipse from the paint canvas.


            for (int i = PaintCanvas.Children.Count - 1; i >= 0; i += -1)
            {
                UIElement Child = PaintCanvas.Children[i];

                PaintCanvas.Children.Remove(Child);
            }

            newTop = PaintCanvas.ActualHeight / 2;
            newLeft = PaintCanvas.ActualWidth / 2;
            solarSystem.ForEach(a =>
            {

                Ellipse ellipse = CreateAnEllipse(a.ObjRad / 350, a.ObjRad / 350);
                ellipses.Add(ellipse);
                if (a.Name.Equals("Sun"))
                {
                    Canvas.SetTop(ellipse, newTop - (ellipse.Height / 2));
                    Canvas.SetLeft(ellipse, newLeft - (ellipse.Width / 2));
                    newLeft = newLeft + (ellipse.Width / 2);
                }
                else
                {
                    if (a.GetType().Equals(typeof(Planet)) || a.GetType().Equals(typeof(DwarfPlanet)))
                    {
                        Planet p = (Planet)a;

                        if (loopCounter != 5000)
                        {

                            newLeft += p.OrbRad / 4000;

                            Canvas.SetLeft(ellipse, (int)(newLeft * Math.Cos(i++ * (Math.PI / 180))));
                            Canvas.SetTop(ellipse, (int)((newTop - (ellipse.Height / 2)) * Math.Sin(j++ * (Math.PI / 180))));
                        }

                    };
                }

            });




            /* ellipse = CreateAnEllipse(20, 20);
             ellipse2 = CreateAnEllipse(30, 30);
             PaintCanvas.Children.Add(ellipse);
             if (loopCounter != 5000)
             {   
                 *//* newLeft =  (int)(p.Orbital_radius * Math.Cos(i++ * (Math.PI / 180)));
                 newTop =  (int)(p.Orbital_radius * Math.Sin(j++ * (Math.PI / 180)));
                 Canvas.SetLeft(ellipse, newLeft);*//*
                 Canvas.SetLeft(ellipse2, (int)(p.OrbRad * Math.Cos(i++ * (Math.PI / 180))) + newLeft + 200);
                 Canvas.SetTop(ellipse2, (int)(p.OrbRad * Math.Sin(j++ * (Math.PI / 180))) + newTop + 200);



                 Canvas.SetLeft(ellipse, (int)(p.OrbRad * Math.Cos(i++ * (Math.PI / 180))) + newLeft);
                 Canvas.SetTop(ellipse, (int)(p.OrbRad * Math.Sin(j++ * (Math.PI / 180))) + newTop);
             }   
             else
             {   
                 newTop = PaintCanvas.ActualHeight / 2;
                 newLeft = PaintCanvas.ActualWidth / 2;
                 Canvas.SetLeft(ellipse, newLeft);
                 Canvas.SetTop(ellipse, newTop / 2);

                 Canvas.SetLeft(ellipse2, newLeft + 200);
                 Canvas.SetTop(ellipse2, newTop / 2 + 200);
             }


             if (--loopCounter == 0)
             {

                 timer.Stop();
             }*/







        }


        //Add the ellipse to the canvas
        /*ellipse = CreateAnEllipse(20, 20);
        PaintCanvas.Children.Add(ellipse);
        double newLeft = Canvas.GetLeft(ellipse);
        Canvas.SetLeft(ellipse,newLeft-=5);
        Canvas.SetTop(ellipse, Canvas.GetTop(ellipse));*/


        // Customize your ellipse in this method
        public Ellipse CreateAnEllipse(double height, double width)
        {

            SolidColorBrush fillBrush = new SolidColorBrush() { Color = Colors.Red };
            SolidColorBrush borderBrush = new SolidColorBrush() { Color = Colors.Black };
            Ellipse el = new Ellipse()
            {
                Height = height,
                Width = width,
                StrokeThickness = 1,
                Stroke = borderBrush,
                Fill = fillBrush
            };
            PaintCanvas.Children.Add(el);
            return el;

        }
    }
}