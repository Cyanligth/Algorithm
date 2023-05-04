using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_TextRPG
{
    internal class BattleScene : Scene
    {
        private Monster monster;
        public BattleScene(Game game) : base(game) { }

        public override void Render()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("크아앙아ㅏ아아앙 투명드래곤이 울부지져따");
            sb.AppendLine();
            sb.AppendLine("1. 공격");
            sb.AppendLine("2. 가방");
            sb.AppendLine("3. 도망");
            sb.AppendLine();
            sb.Append("메뉴를 선택하세요 : ");

            Console.Write(sb.ToString());
        }

        public override void Update()
        {
            string input = Console.ReadLine();
        }

        public void BattleStart(Monster monster)
        {
            this.monster = monster;
            Data.monsters.Remove(monster);

            Console.Clear();
            Console.WriteLine("대충 웅장한 전투 시작 브금");
            Thread.Sleep(1000);
        }
    }
}
