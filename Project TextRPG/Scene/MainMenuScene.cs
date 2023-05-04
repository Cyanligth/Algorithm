using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TextRPG
{
    internal class MainMenuScene : Scene
    {
        public MainMenuScene(Game game) : base(game) { }

        public override void Render()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("1. 게임시작");
            sb.AppendLine("2. 게임종료");
            sb.AppendLine();
            sb.Append("메뉴를 선택하세요 : ");

            Console.Write(sb.ToString());
        }

        public override void Update()
        {
            string input = Console.ReadLine();

            int command;
            if(!int.TryParse(input, out command))
            {
                Console.WriteLine(" E R R O R ");
                Thread.Sleep(2000);
                return;
            }

            switch(command)
            {
                case 1:
                    game.GameStart();
                    Console.WriteLine("시작");
                    Thread.Sleep(1000);
                    break;
                case 2:
                    game.GameOver();
                    Console.WriteLine("종료");
                    Thread.Sleep(1000);
                    break;
                default:
                    Console.WriteLine("그런건 업따....");
                    Thread.Sleep(1000);
                    break;
            }
        }
    }
}
