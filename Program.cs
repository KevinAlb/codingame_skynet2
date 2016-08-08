using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skynet
{
    class Program
    {
        static void Main(string[] args)
        {

            Graphe graph = new Graphe();
            string[] inputs;
            inputs = Console.ReadLine().Split(' ');
            int N = int.Parse(inputs[0]); // the total number of nodes in the level, including the gateways
            int L = int.Parse(inputs[1]); // the number of links
            int E = int.Parse(inputs[2]); // the number of exit gateways

            for (int i = 0; i < L; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int N1 = int.Parse(inputs[0]); // N1 and N2 defines a link between these nodes
                int N2 = int.Parse(inputs[1]);
                graph.addLink(N1, N2);

            }
            for (int i = 0; i < E; i++)
            {
                int EI = int.Parse(Console.ReadLine()); // the index of a gateway node
                graph.addPasserelle(EI);

            }
            // graph.afficherGraphe();
            // game loop
            while (true)
            {
                int SI = int.Parse(Console.ReadLine()); // The index of the node on which the Skynet agent is positioned this turn
                var v = graph.supprimerNoeud(SI);
                Console.WriteLine(v.Item1 + " " + v.Item2);
                graph.removeLink(v.Item1, v.Item2);

            }

        }
    }
}



