using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skynet
{
    class Graphe
    {

        public Dictionary<int, Node> nodes { get; set; }
        public int nbNodes { get; set; }
        public int nbPasserelles { get; set; }

        public Graphe()
        {
            nodes = new Dictionary<int, Node>();
            nbNodes = 0;
            nbPasserelles = 0;
        }

        public void addLink(int depart, int arrivee)
        {
            if (!nodes.ContainsKey(depart))
            {
                nodes.Add(depart, new Node(depart));
                nbNodes++;
            }
            if (!nodes.ContainsKey(arrivee))
            {
                nodes.Add(arrivee, new Node(arrivee));
                nbNodes++;
            }
            nodes[depart].addChild(nodes[arrivee]);
            nodes[arrivee].addChild(nodes[depart]);
        }

        public void addPasserelle(int position)
        {
            nbPasserelles++;
            nodes[position].isPasserelle = true;
        }

        public void removeLink(int depart, int arrivee)
        {
            nodes[depart].removeChild(nodes[arrivee]);
            nodes[arrivee].removeChild(nodes[depart]);
            if (nodes[depart].nbLink == 0)
                nodes.Remove(depart);
            if (nodes[arrivee].nbLink == 0)
                nodes.Remove(arrivee);
        }

        public void afficherGraphe()
        {
            foreach (int val in nodes.Keys)
            {
                Console.Error.WriteLine("Noeud " + val);
                nodes[val].link.ForEach(x => Console.Error.WriteLine("Lien : " + x.id));
            }
        }

        public Tuple<int, int> supprimerNoeud(int skynet)
        {
            Tuple<int, int> noeud = null;
            var tremplin = new List<int>();
            foreach (var n in nodes)
            {
                if (!n.Value.isPasserelle && n.Value.countTremplin() != 0)
                {
                    tremplin.Add(n.Key);
                }
            }
            //On a récupéré tout les tremplins
            //Si skynet est sur un tremplin, on doit absolument détruire le lien 
            if (tremplin.Contains(skynet))
            {

                noeud = new Tuple<int, int>(skynet, nodes[skynet].getFirstPasserelle().id);
            }
            else
            //Si skynet n'est pas sur un tremplin, on detruit un lien du tremplin qui possède le plus de passerelle
            //Si 2 tremplin ont le meme nombre maximum de passerelle, on regarde le plus proche
            {
                var passerelleByTremplin = new Dictionary<int, int>();
                foreach (int i in tremplin)
                {
                    passerelleByTremplin[i] = nodes[i].countTremplin();
                }
                int max = passerelleByTremplin.Values.Max();
                //On a récupéré le max, on enlève toutes les tremplins qui ne sont pas égales au max
                var temp = new Dictionary<int, int>();
                foreach (var v in passerelleByTremplin)
                {
                    if (v.Value == max)
                    {
                        temp.Add(v.Key, v.Value);
                    }
                }
                passerelleByTremplin = new Dictionary<int, int>(temp);
                //On a un ou plusieurs tremplins contenant le maximum de lien
                //Si on en a qu'un, on le supprime
                if (passerelleByTremplin.Count == 1)
                {
                    int result = passerelleByTremplin.Keys.First();
                    noeud = new Tuple<int, int>(result, nodes[result].getFirstPasserelle().id);
                }
                else
                {

                    List<int> tremplin_max = passerelleByTremplin.Keys.ToList();
                    int min = int.MaxValue;
                    int node = -1;
                    foreach (int val in tremplin_max)
                    {
                        int distance = distBetweenNode(skynet, val);
                        Console.Error.WriteLine("Distance de " + distance + "entre " + skynet + " et " + val);
                        if (distance < min)
                        {
                            min = distance;
                            node = val;
                        }
                    }
                    noeud = new Tuple<int, int>(node, nodes[node].getFirstPasserelle().id);

                }



            }
            return noeud;
        }

        public int distBetweenNode(int skynet, int node)
        {
            var queue = new Queue<Tuple<int, int>>();
            var vu = new List<int>();
            vu.Add(skynet);
            queue.Enqueue(new Tuple<int, int>(skynet, 0));
            Tuple<int, int> temp;
            while ((temp = queue.Dequeue()).Item1 != node)
            {
                vu.Add(temp.Item1);
                foreach (Node n in nodes[temp.Item1].link)
                {
                    if (!vu.Contains(n.id) && !n.isPasserelle)
                    {
                        if (nodes[temp.Item1].isTremplin())
                        {
                            queue.Enqueue(new Tuple<int, int>(n.id, temp.Item2));
                        }
                        else
                        {
                            queue.Enqueue(new Tuple<int, int>(n.id, temp.Item2 + 1));
                        }

                    }

                }
            }
            return temp.Item2;


        }


    }
}
