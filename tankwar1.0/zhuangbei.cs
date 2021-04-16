using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tankwar1._0.Properties;

namespace tankwar1._0
{
    //此类用于控制游戏中出现的各种装备
    class zhuangbei:GameObject
    {
        private static Image imageStar = Resources.star;
        private static Image imageBomb = Resources.bomb;
        private static Image imageTimer = Resources.timer;


        //装备的类型  0代表五角星  1代表地雷  2代表计时器
        public int ZBType
        {
            get;
            set;
        }

        public zhuangbei(int x,int y,int type):base(x,y,imageStar.Width,imageStar.Height)
        {
            this.ZBType = type;
        }
        public override void Draw(Graphics g)
        {
            switch(ZBType)
            {
                case 0:
                    g.DrawImage(imageStar, this.X, this.Y);
                    break;
                case 1:
                    g.DrawImage(imageBomb, this.X, this.Y);
                    break;
                case 2:
                    g.DrawImage(imageTimer, this.X, this.Y);
                    break;
            }
        }
    }
}
