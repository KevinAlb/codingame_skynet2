using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skynet
{
    class Node
    {

        public int id { get; set; }
        public List<Node> link { get; set; }
        public bool isPasserelle { get; set; }
        public int nbLink { get; set; }

        public Node(int id)
        {
            this.id = id;
            link = new List<Node>();
            isPasserelle = false;
            nbLink = 0;
        }


        public void addChild(Node child)
        {
            link.Add(child);
            nbLink++;
        }

        public void removeChild(Node child)
        {
            link.Remove(child);
            nbLink--;
        }

        public int countTremplin()
        {
            int nb = 0;
            foreach (Node n in link)
            {
                if (n.isPasserelle)
                    nb++;
            }
            return nb;
        }

        public bool isTremplin()
        {
            return countTremplin() > 0;
        }

        public Node getFirstPasserelle()
        {
            foreach (Node n in link)
            {
                if (n.isPasserelle)
                    return n;
            }
            return null;
        }


    }
}
