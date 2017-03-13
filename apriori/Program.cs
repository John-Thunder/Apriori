using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace apriori
{   
    class Program
    {   static Dictionary<string, int> C1 = new Dictionary<string, int>();
        static Dictionary<string, int> C2 = new Dictionary<string, int>();
        static Dictionary<string, int> C3 = new Dictionary<string, int>();
        static Dictionary<string, int> L1 = new Dictionary<string, int>();
        static Dictionary<string, int> L2 = new Dictionary<string, int>();
        static Dictionary<string, int> L3 = new Dictionary<string, int>();
        static void Main(string[] args)
        {
            string line;

            //System.Collections.Generic.List<string> table = new System.Collections.Generic.List<string>();
            // Read the file and display it line by line.

            System.IO.StreamReader file =
               new System.IO.StreamReader("../../test_Data/T15I7N0.5KD1K.txt");
            int value;
            while ((line = file.ReadLine()) != null)
            {
                string[] tokens = line.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < tokens.Length; i++)
			    {
                    if (!C1.TryGetValue(tokens[i],out value))
	                {
                        C1.Add(tokens[i], 1);
	                }
                    else if (C1.TryGetValue(tokens[i],out value))
                    {
                        C1[tokens[i]] = C1[tokens[i]] + 1;
                    }
			    }

                //int[] convertedItems = Array.ConvertAll<string, int>(tokens, int.Parse);
            }
            L1=GetL(C1,5);
            file.Close();
            Print(L1);
            // Suspend the screen.
            Console.ReadLine();
        }
        public static Dictionary<string, int> GetL(Dictionary<string, int> c, double d)
        {
            Dictionary<string, int> l = new Dictionary<string, int>();
            foreach (string obj in c.Keys)
            {
                if (c[obj]>d)
                {
                    l.Add(obj, c[obj]);
                }
            } 
            return l;
        }
        public static void Print(Dictionary<string, int> Dic)
        {   int sum=0;
            foreach (string s in Dic.Keys)
            {
                sum=sum+Dic[s];
            }
            Console.WriteLine(sum);
        }
    }
}
