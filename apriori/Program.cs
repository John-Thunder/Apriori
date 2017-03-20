using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace apriori
{   
    class Program
    {   static List<ItemSet> transactions; 
        static List<ItemSet> freqitemsets; 
        static void Main(string[] args)
        {
            transactions = new List<ItemSet>();
            freqitemsets = new List<ItemSet>();

            addTransactions();
            // Suspend the screen.
            Console.ReadLine();
        }
        /*
        public static Dictionary<string, int> GetL(Dictionary<string, int> c, double d)
        {
            Dictionary<string, int> l = new Dictionary<string, int>();
            foreach (string obj in c.Keys)
            {
                if (c[obj]>=d)
                {
                    l.Add(obj, c[obj]);
                }
            } 
            return l;
        }
        public static int Print(Dictionary<string, int> Dic)
        {   int sum=0;
            foreach (string s in Dic.Keys)
            {
                sum=sum+Dic[s];
            }
            return sum;
        }
        public static List<List<String>> NoRepeat(List<List<String>> T)
        {
            List<List<String>> L = new List<List<String>>();
            for (int i = 0; i < T.Count; i++)
            {
                if (L.Contains(T[i])) { }
                else if (!L.Contains(T[i]))
                {
                    L.Add(T[i]);
                }
            }
            T = L;
            return T;
        }
        public static bool IsExistRepeatElement(StringBuilder sbs,out StringBuilder temp)
        {
            ArrayList sb = new ArrayList();
            String[] myArray = sbs.ToString().Split(' ');
            for (int i = 0; i < myArray.Length; i++)
            {
                 if (myArray[i]!="")
                 {
                    sb.Add(myArray[i]);
                 }
            }
            for (int i = 0; i < sb.Count; i++)
            {
                for (int j = sb.Count-1; j > i; j--)
                {
                    if (sb[i].Equals(sb[j]))
                    {
                        sb.Remove(sb[i]);
                        sb.Sort();
                        temp = new StringBuilder();
                        for (int k = 0; k < sb.Count; k++)
                        {
                            temp.Append(sb[k]+" "); 
                        }
                        return true;
                    }
                }
            }
            temp = new StringBuilder();
            return false;
        }
        public static bool IsInL2(Dictionary<string, int> L_n,ArrayList al)
        {
            for (int i = 0; i < L_n.Count; i++)
            {
                if (L_n.ContainsKey(al[0].ToString() +" "+ al[1].ToString()) || L_n.ContainsKey(al[1].ToString() +" "+ al[0].ToString()))
                {
                    return true;
                }
                else 
                    return false;
            }
            return false;
        }
        */
        static List<ItemSet> l1scan(List<ItemSet> candidateItemsets, int patternNum)
        {
            List<ItemSet> newcandidateItemsets = new List<ItemSet>();

            foreach (ItemSet iset in candidateItemsets)
            {
                ItemSet newcandidate = new ItemSet();// = iset;

                iset.support = GetSupport(iset);
                iset.clone(newcandidate);
                if (newcandidate.support >= 5)
                {
                    newcandidateItemsets.Add(newcandidate);
                }
            }
            //Console.WriteLine("Frequent Pattern for " + patternNum + " - Itemset and min support");
            //foreach (ItemSet i in newcandidateItemsets)
            //Console.WriteLine(i.stringRepresentationWithSpace() +  " " + i.support);
            return newcandidateItemsets;


        }
        static void addTransactions()
        {
            //   int lineNum = 0;
            string line;
            using (StreamReader sr = new StreamReader("../../test_Data/john.txt"))
            {

                while ((line = sr.ReadLine()) != null)
                {
                    List<string> data = line.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    ItemSet set = new ItemSet();
                    foreach (string d in data)
                    {
                        if (!d.Equals(""))
                        {
                            Item it = new Item(d);
                            set.add(it);
                        }

                    }
                    set.sort();
                    transactions.Add(set);

                    //  lineNum++;
                }
            }
        }
        static double GetSupport(ItemSet i1)
        {
            double support = 0;

            foreach (ItemSet transaction in transactions)
            {
                if (transaction.contains(i1))
                {
                    support++;
                }
            }

            return support;
        }

    }

}
