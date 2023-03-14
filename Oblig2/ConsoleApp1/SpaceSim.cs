using System;

namespace SpaceSim
{
    public class SpaceObject
    {
        public String Name { get; set; }
        public double ObjRad { get; set; } //object radius, in km
        public String ObjCol { get; set; } //object colour




        public SpaceObject(String name, double objRad, String objCol)
        {
            this.Name = name;
            this.ObjRad = objRad;
            this.ObjCol = objCol;
        }
        public SpaceObject(String name, double objRad)
        {
            this.Name = name;
            this.ObjRad = objRad;

        }


        public virtual void Draw()
        {
            Console.WriteLine(Name);
        }
    }
    public class Star : SpaceObject
    {
        public double OrbRad { get; set; } //orbital radius, in km
        public Star(String name, double objRad, String objCol, double orbRad) : base(name, objRad, objCol) { }
        public override void Draw()
        {
            Console.Write("Star : ");
            base.Draw();
        }
    }
    public class Planet : SpaceObject
    {
        public double OrbRad { get; set; } //orbital radius, in km
        public double OrbPer { get; set; }  //orbital period, in days
        public double RotPer { get; set; }  //rotational period, in days
        public Planet(String name, double objRad, String objCol, double orbRad, double orbPer, double rotPer)
            : base(name, objRad, objCol)
        {
            this.OrbRad = orbRad;
            this.OrbPer = orbPer;
            this.RotPer = rotPer;
        }


        public Planet(String name, double objRad, double orbRad, double orbPer)
            : base(name, objRad)
        {
            this.OrbRad = orbRad;
            this.OrbPer = orbPer;

        }

        public override void Draw()
        {
            Console.Write("Planet: ");
            base.Draw();
        }
        public Coordinates CalculateOrbit(double time)
        {
            double PercentageOfOrbit = time / OrbPer;
            double ArcLength = PercentageOfOrbit * 2 * Math.PI * OrbRad;
            double Angle = ArcLength / OrbRad;

            return new Coordinates((int)(OrbRad * Math.Cos(Angle)), (int)(OrbRad * Math.Sin(Angle)));
        }

    }
    public class DwarfPlanet : Planet
    {
        public DwarfPlanet(String name, double objRad, String objCol, double orbRad, double orbPer, double rotPer)
            : base(name, objRad, objCol, orbRad, orbPer, rotPer) { }
        public override void Draw()
        {
            Console.Write("Dwarf planet : ");
            base.Draw();
        }
    }
    public class Moon : Planet
    {
        public Moon(String name, double objRad, String objCol, double orbRad, double orbPer, double rotPer)
            : base(name, objRad, objCol, orbRad, orbPer, rotPer) { }
        public override void Draw()
        {
            Console.Write("Moon : ");
            base.Draw();
        }
    }
    public class Comet : SpaceObject
    {
        public Comet(String name, double objRad, String objCol) : base(name, objRad, objCol) { }
        public override void Draw()
        {
            Console.Write("Comet : ");
            base.Draw();
        }
    }
    public class Asteroid : SpaceObject
    {
        public Asteroid(String name, double objRad, String objCol) : base(name, objRad, objCol) { }
        public override void Draw()
        {
            Console.Write("Asteroid : ");
            base.Draw();
        }
    }
    public class Asteroidbelt : SpaceObject
    {
        public Asteroidbelt(String name, double objRad, String objCol) : base(name, objRad, objCol) { }
        public override void Draw()
        {
            Console.Write("Asteroidbelt : ");
            base.Draw();
        }
    }
    public class Coordinates
    {
        public int Posx { get; set; }
        public int Posy { get; set; }

        public Coordinates(int posx, int posy)
        {
            this.Posx = posx;
            this.Posy = posy;
        }
        override
        public String ToString()
        {
            return "Pos x: " + Posx + " pos y: " + Posy;
        }
    }

}