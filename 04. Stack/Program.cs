using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace _04._Stack
{
    internal class Program
    {
        /******************************************************
		 * 스택 (Stack)
		 * 
		 * 선입후출(FILO), 후입선출(LIFO) 방식의 자료구조
		 * 가장 최신 입력된 순서대로 처리해야 하는 상황에 이용
		 ******************************************************/

        void Stack()
        {
            Stack<int> stack = new Stack<int>();

            for (int i = 0; i < 5; i++) stack.Push(i);  // 입력순서 : 0 1 2 3 4

            Console.WriteLine(stack.Peek());            // 최상단 : 4

            while (stack.Count > 0)
            {
                Console.WriteLine(stack.Pop());         // 출력순서 : 4 3 2 1 0
            }
        }


        static void Main(string[] args)
        {
            Stack<char> stack = new Stack<char>();
            /*
            for (int i = 0; i < 5; i++) stack.Push(i);  // 입력순서 : 0 1 2 3 4

            Console.WriteLine(stack.Peek());            // 최상단 : 4
            while (stack.Count() > 0)
            {
                Console.WriteLine(stack.Pop());         // 출력순서 : 4 3 2 1 0
            }
            */
            
            Console.Write("괄호 입력 : ");
            string s = Console.ReadLine();
            foreach (char a in s) { stack.Push(a); }
            Console.WriteLine(Parenthesis(stack));
            // 괄호 성공

            Console.Write("계산기 입력 : ");
            
            string input = Console.ReadLine();
            // "1+2*3+(1+3)/4"
            List<string> list = new List<string>();
            List<string> list2 = new List<string>();
            list = GetArray(input);
            foreach (string i in list) { Console.Write(i+" "); }
            Console.WriteLine();
            list2 = Change(list);
            foreach (string s in list2) { Console.Write(s+" "); }
            Console.WriteLine();
            Console.WriteLine(Calculater(list2));

        }

        static double Calculater(List<string> list)
        {
            double result = 0;
            Stack<double> stack = new Stack<double>();
            foreach(string i in list)
            {
                if(i != "+" && i != "-" && i != "*" && i != "/")
                    stack.Push(double.Parse(i));
                else
                {
                    double num1 = stack.Pop();
                    double num2 = stack.Pop();
                    switch(i)
                    {
                        case "+": stack.Push(num1+num2); break;
                        case "-": stack.Push(num1-num2); break;
                        case "*": stack.Push(num1*num2); break;
                        case "/": stack.Push(num1/num2); break;
                    }
                }
            }
            result = stack.Pop();
            return result;
        }

        static List<string> Change(List<string> input) 
        {
            Stack<string> stack = new Stack<string>();
            List<string> list = new List<string>();
            foreach(string s in input)
            {
                if(s == "*" || s == "/" || s == "(")
                    stack.Push(s);
                else if(s == "+" || s == "-")
                {
                    if(stack.Count == 0)
                        stack.Push(s);
                    else
                    {
                        int a = stack.Count();
                        for(int i = 0; i < a; i++)
                        {
                            if(stack.Peek() == "*" || stack.Peek() == "/")
                            {
                                list.Add(stack.Pop());
                                break;
                            }
                        }
                        stack.Push(s);
                    }
                }
                else if(s == ")")
                {
                    while(stack.Peek() != "(")
                    {
                        list.Add(stack.Pop());
                    }
                    stack.Pop();
                }
                else { list.Add(s); }
            }
            while(stack.Count > 0)
                list.Add(stack.Pop());
            return list;
        }

        static List<string> GetArray(string input) 
        {
            List<string> list = new List<string>();
            List<string> ect = new List<string>();
            List<string> charList = new List<string>();
            string num = "";
            foreach(char a in input)
            {
                charList.Add(a.ToString());
            }
            foreach (string a in charList)
            {
                if(a == "+" || a == "-" || a == "*" || a == "/" || a == "(" || a == ")")
                {
                    foreach (string s in ect)
                        num += s;
                    if (num != "")
                    {
                        list.Add(num);
                    }
                    list.Add(a);
                    ect = new List<string>();
                    num = "";
                }
                else 
                    ect.Add(a);
            }
            foreach (string s in ect)
                num += s;
            list.Add(num);
            return list;
        }


        static bool Parenthesis(Stack<char> stack)
        {
            int[] arr = new int[3];
            int e = 0;
            foreach (char s in stack) 
            {
                switch(s)
                {
                    case ')':
                        arr[0]++;
                        break;
                    case '}':
                        arr[1]++;
                        break;
                    case ']':
                        arr[2]++;
                        break;
                    case '(':
                        if (arr[0] < 1)
                            e++;
                        else
                            arr[0]--;
                        break;
                    case '{':
                        if (arr[1] < 1)
                            e++;
                        else
                            arr[1]--;
                        break;
                    case '[':
                        if (arr[2] < 1)
                            e++;
                        else
                            arr[2]--;
                        break;
                    default:
                        Console.WriteLine("괄호가 아님");
                        throw new Exception();
                }
            }
            if (e>0 || arr[0]>0 || arr[1]>0 || arr[2]>0)
            {
                return false;
            }
            else return true;
        }
    }
}