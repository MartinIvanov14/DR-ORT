using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Myproject
{
    public partial class Form1 : Form
    {

        public class Node
        {
            public int Id { get; set; }
            public List<Edge> Edges { get; set; }
            
        }

        public class Edge
        {
            public Node To { get; set; }
            public int Cost { get; set; }
        }

        public class PriorityQueue<T>
        {
            private SortedDictionary<int, Queue<T>> list = new SortedDictionary<int, Queue<T>>();

            public void Enqueue(T item, int priority)
            {

                if (!list.ContainsKey(priority))
                {
                    list[priority] = new Queue<T>();
                }
                list[priority].Enqueue(item);

            }

            public T Dequeue()
            {

                var firstPair = list.OrderBy(p => p.Key).First();
                var dequeuedItem = firstPair.Value.Dequeue();
                if (firstPair.Value.Count == 0)
                {
                    list.Remove(firstPair.Key);
                }
                return dequeuedItem;

            }

            public bool IsEmpty
            {
                get { return list.Count == 0; }
            }

        }

        public List<TextBox> textBoxes;

        List<TextBox> rebtextBoxes;
        public Form1()
        {
            InitializeComponent();

            textBoxes = new List<TextBox>() { rebtextBox12, rebtextBox13, rebtextBox23, rebtextBox24, rebtextBox25, rebtextBox35,
            rebtextBox45, rebtextBox46, rebtextBox56, rebtextBox57, rebtextBox67, goaltextBox};
            rebtextBoxes = new List<TextBox>() { rebtextBox12, rebtextBox13, rebtextBox23, rebtextBox24, rebtextBox25, rebtextBox35,
            rebtextBox45, rebtextBox46, rebtextBox56, rebtextBox57, rebtextBox67};
        }

        public static (List<Node>, Dictionary<Node, int>) UniformCostSearch(Node start, Node goal)
        {
            var visited = new HashSet<Node>();
            var queue = new PriorityQueue<Node>();
            var cameFrom = new Dictionary<Node, Node>();
            var costSoFar = new Dictionary<Node, int>();

            queue.Enqueue(start, 0);
            cameFrom[start] = null;
            costSoFar[start] = 0;

            while (!queue.IsEmpty)
            {
                var current = queue.Dequeue();

                if (current == goal)
                {
                    var path = new List<Node>();
                    while (current != null)
                    {
                        path.Add(current);
                        current = cameFrom[current];
                    }
                    path.Reverse();
                    return (path, costSoFar);
                }

                visited.Add(current);

                foreach (var edge in current.Edges)
                {
                    var next = edge.To;
                    var newCost = costSoFar[current] + edge.Cost;

                    if (!costSoFar.ContainsKey(next) || newCost < costSoFar[next])
                    {
                        if (!visited.Contains(next))
                        {
                            costSoFar[next] = newCost;
                            queue.Enqueue(next, newCost);
                            cameFrom[next] = current;
                        }
                    }
                }
            }

            return (null, new Dictionary<Node, int>());
        }

        private void Startbutton_Click(object sender, EventArgs e)
        {
            var nodes = new Node[7];
            for (int i = 0; i < 7; i++)
                nodes[i] = new Node { Id = i + 1, Edges = new List<Edge>() };


            string costText12 = rebtextBox12.Text;
            string costText13 = rebtextBox13.Text;
            string costText23 = rebtextBox23.Text;
            string costText24 = rebtextBox24.Text;
            string costText25 = rebtextBox25.Text;
            string costText35 = rebtextBox35.Text;
            string costText45 = rebtextBox45.Text;
            string costText46 = rebtextBox46.Text;
            string costText56 = rebtextBox56.Text;
            string costText57 = rebtextBox57.Text;
            string costText67 = rebtextBox67.Text;
            foreach (TextBox textBox in textBoxes)
            {
                if (string.IsNullOrWhiteSpace(textBox.Text))
                {
                    MessageBox.Show("Моля, попълнете празните полета.");
                    textBox.Focus();
                    return;
                }
            }
            if (!int.TryParse(costText12, out int cost12) || !int.TryParse(costText13, out int cost13)|| !int.TryParse(costText23, out int cost23)||
                !int.TryParse(costText24, out int cost24)|| !int.TryParse(costText25, out int cost25)|| !int.TryParse(costText35, out int cost35)||
                !int.TryParse(costText45, out int cost45)|| !int.TryParse(costText46, out int cost46)|| !int.TryParse(costText56, out int cost56)||
                !int.TryParse(costText57, out int cost57)|| !int.TryParse(costText67, out int cost67))
            {
                MessageBox.Show("Моля, въвеждайте само цели числа в полетата за ребрата.");
                return;
            }

            nodes[0].Edges.Add(new Edge { To = nodes[1], Cost = cost12 }); nodes[0].Edges.Add(new Edge { To = nodes[2], Cost = cost13 });
            nodes[1].Edges.Add(new Edge { To = nodes[0], Cost = cost12 }); nodes[1].Edges.Add(new Edge { To = nodes[2], Cost = cost23 });
            nodes[1].Edges.Add(new Edge { To = nodes[3], Cost = cost24 }); nodes[1].Edges.Add(new Edge { To = nodes[4], Cost = cost25 });
            nodes[2].Edges.Add(new Edge { To = nodes[0], Cost = cost13 }); nodes[2].Edges.Add(new Edge { To = nodes[1], Cost = cost23 });
            nodes[2].Edges.Add(new Edge { To = nodes[4], Cost = cost35 });
            nodes[3].Edges.Add(new Edge { To = nodes[1], Cost = cost24 }); nodes[3].Edges.Add(new Edge { To = nodes[4], Cost = cost45 });
            nodes[3].Edges.Add(new Edge { To = nodes[5], Cost = cost46 });
            nodes[4].Edges.Add(new Edge { To = nodes[1], Cost = cost25 }); nodes[4].Edges.Add(new Edge { To = nodes[2], Cost = cost35 });
            nodes[4].Edges.Add(new Edge { To = nodes[3], Cost = cost45 }); nodes[4].Edges.Add(new Edge { To = nodes[5], Cost = cost56 });
            nodes[4].Edges.Add(new Edge { To = nodes[6], Cost = cost57 });
            nodes[5].Edges.Add(new Edge { To = nodes[3], Cost = cost46 }); nodes[5].Edges.Add(new Edge { To = nodes[4], Cost = cost56 });
            nodes[5].Edges.Add(new Edge { To = nodes[6], Cost = cost67 });
            nodes[6].Edges.Add(new Edge { To = nodes[4], Cost = cost57 }); nodes[6].Edges.Add(new Edge { To = nodes[5], Cost = cost67 });

            string goalText = goaltextBox.Text;
            int goal;
            if (!int.TryParse(goaltextBox.Text, out goal) || goal < 2 || goal > 7)
            {
                MessageBox.Show("Моля, въведете цяло число между две и седем в полето за целевия възел.");
                goaltextBox.Focus();
                return;
            }

            int cost;

            foreach (TextBox textBox in rebtextBoxes)
            {
                if (!int.TryParse(textBox.Text, out cost) || cost <= 0)
                {
                    MessageBox.Show("Моля, въвеждайте положителни цели числа в полетата за ребрата.");
                    textBox.Focus();
                    return;
                }
            }

            var (pathToNode, costSoFar) = UniformCostSearch(nodes[0], nodes[goal-1]);
            if (pathToNode != null)
            {
                var pathCost = costSoFar[pathToNode.Last()];
                string path = "Пътят от възел 1 до възел " +  goalText + " е: " + string.Join(" -> ", pathToNode.Select(n => n.Id))
                            + " с обща стойност: " + pathCost;
                rezlabel.Text = path;
            }
            else
            {
                Console.WriteLine("Няма път от възел 1 до възел"+ goalText);
            }

            foreach (TextBox textBox in textBoxes)
            {
                textBox.Enabled = false;
            }
        }

        private void tryAgainbutton_Click(object sender, EventArgs e)
        {
            foreach (TextBox textBox in textBoxes)
            {
                textBox.Clear();
            }

            rezlabel.Text = " ";

            foreach (TextBox textBox in textBoxes)
            {
                textBox.Enabled = true;
            }
        }
    }
}
