using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalGeometry
{
    using System.Runtime.CompilerServices;

    public class Point : IEquatable<Point>
    {
        private const int X = 0;
        private const int Y = 1;
        private const int DIM = 2;

        public int[] Dim { get; set; }

        public Point()
        {
            Dim = new int[DIM];
        }

        public int this[int index]
        {
            get
            {
                return Dim[index];
            }
            set
            {
                Dim[index] = value;
            }
        }

        public bool Equals(Point other)
        {
            for(int i = 0; i < DIM; i++) if (other[i] != this[i]) return false;
            return true;
        }
    }
}
