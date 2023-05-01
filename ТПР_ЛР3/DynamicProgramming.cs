using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ТПР_ЛР3
{
    public class DynamicProgramming
    {
        const int size = 8;
        private List<string> steps = new List<string>();
        private string path = "";
        private int[] weight = new int[size];
        private int[,] dist = new int[size, size];

        public DynamicProgramming()
        {
            for (int el = 0; el < size; el++)
                weight[el] = 999;
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                    dist[i, j] = 999;
                steps.Add("");
            }
            steps[0] = 0.ToString();
        }
        public void CreationDistMatrix()
        {

            Random random = new Random();
            int r = random.Next() % 2;
            for (int i = 0; i < size; ++i)
                for (int j = 0; j < size; ++j)
                    if ((j - 4 - r) <= i && j - r >= i && j != i)
                        dist[i, j] = random.Next(1, 15);
            for (int i = 0; i < size; ++i)
            {
                for (int j = 0; j < size; ++j)
                {
                    if (dist[i, j] == 999)
                        Console.Write($"{0,3} ");
                    else
                        Console.Write($"{dist[i, j],3} ");
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        private void SearchMinDist()
        {
            List<string> tempWeight = new List<string>();
            weight[0] = 0;
            int i = 0;
            int n = 0;
            while (i < size )
            {
                List<int> temp = new List<int>(size);
                List<int> temp2 = new List<int>(size);
                steps[i].Split(' ').ToList().ForEach(x =>
                {
                    if (x != null && x != "")
                        temp.Add(Convert.ToInt32(x));
                });
                if (i < size - 1)
                {
                    Steps(i + 1, temp);
                    steps[i + 1].Split(' ').ToList().ForEach(x =>
                    {
                        if (x != null && x != "")
                            temp2.Add(Convert.ToInt32(x));
                    });
                }
                int t = 0;
                Console.Write($"\nFunction {n++}: ");
                foreach (int step in temp)
                {
                    t = step;
                    if (weight[step] != 999 && t >= 0)
                    {
                        Console.Write($"{weight[step].ToString()} ");
                        t--;
                    }
                }
                foreach (int step in temp)
                    foreach (int step2 in temp2)
                        if (weight[step2] > weight[step] + dist[step, step2])
                            weight[step2] = weight[step] + dist[step, step2];
                i++;
            }
            Console.WriteLine("\n");
            int k = 0;
            foreach (string step in steps)
            {

                string newStep = "";
                step.Split(' ').ToList().ForEach(x =>
                {
                    if (x != "")
                        newStep += (Convert.ToInt32(x) + 1).ToString() + " ";
                });
                Console.WriteLine($"Step {k++}: {newStep}");
            }

        }
        private void Steps(int step, List<int> lines)
        {
            foreach (int line in lines)
            {
                int j = 0;
                while (j < size)
                {
                    if (dist[line, j] != 999 && !steps[step].Contains(j.ToString()))
                        steps[step] += j.ToString() + ' ';
                    j++;
                }
            }
        }

        private void SearchPath()
        {
            CreationDistMatrix();
            SearchMinDist();
            for (int i = size - 1; i > 0;)
                for (int j = i - 1; j >= 0; j--)
                    if (dist[j, i] != 999 && weight[i] == weight[j] + dist[j, i] && !path.Contains((j + 1).ToString()))
                    {
                        path += (j + 1).ToString() + '-';
                        i = j;
                    }
        }
        public void GetPath()
        {
            SearchPath();
            Console.WriteLine($"Minpath: {weight[size - 1].ToString()}");
            Console.WriteLine($"Path: 8-{path.Remove(path.Length - 1)}");
        }
    }
}
