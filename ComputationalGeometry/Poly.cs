using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComputationalGeometry
{
    public class Poly
    {
        public LinkedList<Vertex> Verticies = new LinkedList<Vertex>();

        public void AddVertex(Vertex v)
        {
            Verticies.AddLast(v);
        }
    }
}
