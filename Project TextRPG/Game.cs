using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Project_TextRPG
{
    public class Game
    {
        private bool running;
        // private Dictionary<string, Scene> sceneDic;
        private Scene curruntScene;
        private MainMenuScene mainMenuScene;
        private MapScene mapScene;
        private InventoryScene inventoryScene;
        private BattleScene battleScene;

        public void Run()
        {
            // 초기화
            Init();

            // 반복
            while (running) 
            {
                // 출력
                Render();
                // 갱신  <<안에서 입력까지 처리
                Update();
            }

            // 마무리
            Release();
        }

        private void Init()
        {
            Console.CursorVisible = false;
            running = true;

            Data.Init();
            mainMenuScene = new MainMenuScene(this);
            mapScene = new MapScene(this);
            inventoryScene = new InventoryScene(this);
            battleScene = new BattleScene(this);

            curruntScene = mainMenuScene;
        }

        public void GameStart()
        {
            curruntScene = mapScene;
            Data.LoadLevel();
        }

        public void GameOver() 
        {
            Console.Clear();

            StringBuilder sb = new StringBuilder();

            sb.AppendLine();
            sb.AppendLine("  ***    *   *   * *****       ***  *   * ***** ****  ");
            sb.AppendLine(" *      * *  ** ** *          *   * *   * *     *   * ");
            sb.AppendLine(" * *** ***** * * * *****      *   * *   * ***** ****  ");
            sb.AppendLine(" *   * *   * *   * *          *   *  * *  *     *  *  ");
            sb.AppendLine("  ***  *   * *   * *****       ***    *   ***** *   * ");
            sb.AppendLine();

            sb.AppendLine();

            Console.WriteLine(sb.ToString());

            running = false;
        }

        public void BattleStart(Monster monster)
        {
            curruntScene = battleScene;
            battleScene.BattleStart(monster);
        }

        private void Render()
        {
            Console.Clear();
            curruntScene.Render();
        }

        private void Update()
        {
            curruntScene.Update();
        }

        

        private void Release()
        {
            Data.Release();

        }


    }
}
