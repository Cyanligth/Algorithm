namespace TT2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            // Console.WriteLine(Tree());
            Console.WriteLine(Molu());

        }
        
        static int Tree()
        {
            int a = int.Parse(Console.ReadLine());
            int[,] box = new int[a,a];
            for(int i = 0; i < a; i++)
            {
                string s = Console.ReadLine();
                int[] ect = new int[s.Length];
                int count = 0;
                foreach(char x in s)
                {
                    if (x != ' ')
                        ect[count++] = int.Parse(x.ToString());
                }
                for(int j = 0; j < i+1; j++)
                    box[i,j] = ect[j];
            }

            return Begyo(box, a, 0);
        }
        static int Begyo(int[,] box, int a, int b)
        {
            int result = 0;
            for(int i = 0; i < a; i ++)
            {
                if (box[i,b] > box[i,b+1])
                {
                    result += box[i,b];
                }
                else
                {
                    result += box[i, b+1];
                    b++;
                }
            }
            return result;
        }

        static int Molu()
        {
            string a = Console.ReadLine();
            string[] ns = a.Split(new char[]{'+', '-'});
            string[] pn = a.Split(new char[] { '0','1','2','3','4','5','6','7','8','9',' ' });
            int[] n = new int[ns.Length];
            string[] p = new string[pn.Length];
            int result = 0;
            int count = 0;
            foreach(string s in ns)
                n[count++] = int.Parse(s);
            count = 0;
            foreach (string s in pn)
            {
                if (s != " " && s != "" && s != null && s != default)
                    p[count++] = s;
            }
            count = 0;
            for(int i = 0; i < n.Length; i++)
            {
                if(i == 0) result += n[i++];
                if (p[count] == "-")
                {
                    do
                    {
                        if (i >= n.Length) break;
                        result -= n[i++];
                        count++;
                    } while (p[count] != "-") ;
                }
                else if (p[count] == "+")
                {
                    result += n[i];
                    count++;
                }
            }
            return result;
        }
    }
}