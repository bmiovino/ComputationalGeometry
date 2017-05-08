using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalGeometry
{
    using System.Security.Policy;

    public class Operations
    {
        private const int X = 0;

        private const int Y = 1;

        public int Area(Point a, Point b, Point c)
        {
            return ((b[X] - a[X]) * (c[Y] - a[Y])) - ((c[X] - a[X]) * (b[Y] - a[Y]));
        }

        public int Area(Poly poly)
        {
            int sum = 0;

            LinkedListNode<Vertex> p = poly.Verticies.First;
            LinkedListNode<Vertex> a = p.Next;
            LinkedListNode<Vertex> b;

            do
            {
                if (a == poly.Verticies.Last) b = poly.Verticies.First;
                else b = a.Next;

                sum += Area(p.Value.Point, a.Value.Point, b.Value.Point);
            }
            while (a != poly.Verticies.Last);

            return sum;
        }

        public bool Left(Point a, Point b, Point c)
        {
            return Area(a, b, c) > 0;
        }

        public bool LeftOn(Point a, Point b, Point c)
        {
            return Area(a, b, c) >= 0;
        }

        public bool Collinear(Point a, Point b, Point c)
        {
            return Area(a, b, c) == 0;
        }

        public bool IntersectProp(Point a, Point b, Point c, Point d)
        {
            if (Collinear(a, b, c) || Collinear(a, b, d) || Collinear(c, d, a) || Collinear(c, d, b)) return false;

            return Xor(Left(a, b, c), Left(a, b, d)) && 
                Xor(Left(c,d,a), Left(c,d,b));
        }

        public bool Between(Point a, Point b, Point c)
        {
            if (!Collinear(a, b, c)) return false;

            if (a[X] != b[X]) return ((a[X] <= c[X]) && (c[X] <= b[X])) || ((a[X] >= c[X]) && (c[X] >= b[X]));
            else return ((a[Y] <= c[Y]) && (c[Y] <= b[Y])) || ((a[Y] >= c[Y]) && (c[Y] >= b[Y]));
        }

        public bool Intersect(Point a, Point b, Point c, Point d)
        {
            if (IntersectProp(a, b, c, d)) return true;
            else if (Between(a, b, c) || Between(a, b, d) || Between(c, d, a) || Between(c, d, b)) return true;
            else return false;
        }

        public bool Diagonalize(Poly poly, Vertex a, Vertex b)
        {
            LinkedListNode<Vertex> c, c1;

            c = poly.Verticies.First;

            bool last = false;

            do
            {
                c1 = c.Next;

                if ((c.Value != a) && (c1.Value != a) && (c.Value != b) && (c1.Value != b) && Intersect(
                        a.Point,
                        b.Point,
                        c.Value.Point,
                        c1.Value.Point)) return false;

                if (c == poly.Verticies.Last) last = true;
                else c = c.Next;
            }
            while (!last);

            return true;
        }

        public bool InCone(Poly poly, Vertex a, Vertex b)
        {
            LinkedListNode<Vertex> a0, a1;

            var t = poly.Verticies.Find(a);
            if (t == poly.Verticies.Last) a0 = poly.Verticies.First;
            else a0 = t.Next;
            if (t == poly.Verticies.First) a1 = poly.Verticies.Last;
            else a1 = t.Previous;

            if (LeftOn(a.Point, a1.Value.Point, a0.Value.Point))
                return Left(a.Point, b.Point, a0.Value.Point) && Left(b.Point, a.Point, a1.Value.Point);

            return !(Left(a.Point, b.Point, a1.Value.Point) && Left(b.Point, a.Point, a0.Value.Point));
        }

        public bool Diagonal(Poly poly, Vertex a, Vertex b)
        {
            return InCone(poly, a, b) && InCone(poly, b, a) && Diagonalize(poly, a, b);
        }

        private bool Xor(bool a, bool b)
        {
            return (a && !b) || (!a && b);
        }
    }
}
