using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalGeometry
{
    public class Vertex : IEquatable<Vertex>
    {
        public int Index;

        public Point Point;

        public bool IsEar;

        public bool Equals(Vertex other)
        {
            return other.Point == this.Point;
        }
    }


}
