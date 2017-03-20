using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

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
               //new System.IO.StreamReader("../../test_Data/john.txt");
               new System.IO.StreamReader("../../test_Data/T15I7N0.5KD1K.txt");

            int value;
            List<List<string>> king_list= new List<List<string>>();;
            while ((line = file.ReadLine()) != null)
            {
                var tokens = new List<string>(line.Split(new string[] { ", " }, StringSplitOptions.RemoveEmptyEntries));
                for (int i = 0; i < tokens.Count(); i++)
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
                king_list.Add(tokens);
            }
            file.Close();
            L1 = GetL(C1,5);
            BuildC2(L1,king_list);
            L2 = GetL(C2,5);
            Print(L1);
            Print(L2);

            GetC3(L2,king_list);
            L3 = GetL(C3,5);
            Print(L3);
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
        public static void BuildC2(Dictionary<string, int> l1,List<List<string>> List)
        {
            int count = 0;

            ArrayList elements = new ArrayList();
            foreach (string obj in l1.Keys)
            {
                elements.Add(obj);
            }

            StringBuilder temp;
            for (int i = 0; i < elements.Count; i++)
            {
                string s1 = elements[i].ToString();
                temp = new StringBuilder(s1);
                for (int j = i + 1; j < elements.Count; j++)
                {
                    string s2 = elements[j].ToString();
                    temp.Append(" "+s2);
                    foreach (List<string> T in List)
                    {

                        if (T.Contains(s1) && T.Contains(s2))
                        {
                            count++;
                        }
                    }

                    C2.Add(temp.ToString(), count);
                    count = 0;
                    temp = new StringBuilder(s1);
                }
            }
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
        public static void GetC3(Dictionary<string, int> l2, List<List<string>> List)
        {
            ArrayList elements = new ArrayList();
            foreach (string obj in l2.Keys)
            {

                elements.Add(obj);
            }
            ArrayList elementC3 = new ArrayList();
            StringBuilder temp;

            for (int i = 0; i < elements.Count; i++)
            {
                string s1 = elements[i].ToString();
                temp = new StringBuilder(s1);
                for (int j = i + 1; j < elements.Count; j++)
                {
                    string s2 = elements[j].ToString();
                    temp.Append(" "+s2);

                    if (IsExistRepeatElement(temp,out temp))
                    {
                        int count = 0;
                        if (!C3.ContainsKey(temp.ToString()))
                        {                          
                            ArrayList temps = new ArrayList();
                            String[] myArray = temp.ToString().Split(' ');
                            for (int k = 0; k< myArray.Length; k++)
                            {
                                temps.Add(myArray[k]);
                            }

                            string add = "true";
                            ArrayList al = new ArrayList();  
                            for (int b = 0; b < temps.Count; b++)
                            {
                                al =(ArrayList) temps.Clone();
                                al.RemoveAt(b);
                                if (!IsInL2(al))
                                {
                                    add = "false";
                                } 
                             }

                            foreach (List<string>  T in List)
                            {
                                if (T.Contains(temps[0].ToString()) && T.Contains(temps[1].ToString()) && T.Contains(temps[2].ToString()))
                                {
                                    count++;
                                }
                            } 

                            if (add == "true")
                            {
                                C3.Add(temp.ToString(), count); 
                            }
                            count = 0;
                        }
                    }
                    temp = new StringBuilder(s1);
                }
            }
        }





        public static bool IsExistRepeatElement(StringBuilder sbs,out StringBuilder temp)
        {
            ArrayList sb = new ArrayList();
            String[] myArray = sbs.ToString().Split(' ');
            for (int i = 0; i < myArray.Length; i++)
            {
                 sb.Add(myArray[i]);
            }
            for (int i = 0; i < sb.Count; i++)
            {
                for (int j = 3; j > i; j--)
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
        public static bool IsInL2(ArrayList al)
        {
            for (int i = 0; i < L2.Count; i++)
            {
                if (L2.ContainsKey(al[0].ToString() +" "+ al[1].ToString()) || L2.ContainsKey(al[1].ToString() +" "+ al[0].ToString()))
                {
                    return true;
                }
                else 
                    return false;
            }
            return false;
        }
    }
}
