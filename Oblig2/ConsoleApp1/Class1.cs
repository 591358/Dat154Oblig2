
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting;
using System.Security.Cryptography.X509Certificates;
using SpaceSim;

class Astronomy
{
    public static void Main()
    {
        List<SpaceObject> solarSystem = new List<SpaceObject>
        {

            new Planet("Mercury",57910,87.97,24),
            new Planet("Venus",108200,224.70,24),
            new Planet("Earth", 149600, 365.26,24),
            new Planet("Mars", 227940,686.96, 24),
            new Planet("Jupiter", 778330,4332.81, 24),
            new Planet("Saturn",1429400,10759.50, 24),
            new Planet("Uranus",2870990,30685.00 , 24),
            new Planet("Neptune",4504300,60190.00, 24),

           /* new Moon("Moon", 384, 27.32),
            new Moon("Phobos", 9,0.32),*/


        };

        /* Console.WriteLine("Planet name: ");

         String? name = Console.ReadLine();

         Console.WriteLine("Time: ");

         double? time = Convert.ToDouble(Console.ReadLine());

         var p1 = solarSystem.Find(x => x.Name.Equals(name));*/

        List<Planet> planets = new List<Planet>();
        solarSystem.ForEach(a =>
        {
            if (a.GetType() == typeof(Planet))
            {
                planets.Add((Planet)a);
            }
            if (a.GetType() == typeof(Moon))
            {
                planets.Add((Moon)a);
            }

        });

        planets.ForEach(a => Console.WriteLine(a.ToString()));


        /* solarSystem.ForEach(x => Console.WriteLine(x.Name));
         Planet p = new("Mercury", 5000, 88.75);
         Console.WriteLine("name : " + p.Name);*/

        /*    Coordinates x = p.calculateOrbit(33.33);

            Console.WriteLine(x.ToString());*/


        /*  Planet venus = new Planet("Venus");
          venus.Orbital_radius = 200;
          venus.Draw();
          venus.Object_color = "Gray";
          Console.Write("Orbital radius:" + venus.Orbital_radius +"\n");
          Console.Write("Orbital Color:" + venus.Object_color +"\n");
          venus.Orbital_radius = 150;
          Console.Write("Orbital radius:" + venus.Orbital_radius + "\n");
          foreach (SpaceObject obj in solarSystem)
          {
              obj.Draw();
          }
          Console.ReadLine();*/




        /*Coordinates coord = new Coordinates(50, 0);

        DwarfPlanet t =  new DwarfPlanet("Terra", 35.3, 20, 29.5, "gray", new Coordinates(2, 3), 45);

        t.Coordinates = coord;
        t.Object_color = "Mørk&Hvit";
        double x = t.Rotational_period;

         Console.Write(t.ToString());
        //p.simulateOrbit();

        Coordinates test = t.calculateOrbit(20);

        Console.Write("\nnew Coords: " + test.ToString());*/





    }
}