using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project_TextRPG.Util;

namespace Project_TextRPG
{
    public class Dragon : Monster
    {
        int a = 0;
        public override void MoveAction()
        {
            if (a++ % 2 != 0)
                return;

            List<Point> path;
            bool result = AStar.PathFinding(Data.map, new Point(pos.x, pos.y),
                new Point(Data.player.pos.x, Data.player.pos.y), out path);

            if (!result)
                return;

            if (path[1].y == pos.y - 1)
                Move(Direction.Up);
            if (path[1].y == pos.y + 1)
                Move(Direction.Down);
            if (path[1].x == pos.x + 1)
                Move(Direction.Right);
            if (path[1].x == pos.x - 1)
                Move(Direction.Left);
        }
    }
}
