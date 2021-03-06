using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tankwar1._0
{
    //提供玩家坦克子类和敌人坦克子类所共有的成员
    abstract class TankFather :GameObject
    {
        //提供玩家坦克子类和敌人坦克子类所共有的成员
        private Image[] images = new Image[] { };

        //坦克出生时间
        public int bornTime = 0;

        //移动许可标记
        public bool isMove = false;

        public TankFather(int x,int y,Image[] images,int speed,int life,Direction direction)
            :base(x,y,images[0].Width,images[0].Height,speed,life,direction)
        {
            this.images = images;
        }

        public override void Draw(Graphics g)
        {
            bornTime++;
            if(bornTime%50==0)
            {
                isMove = true;
            }
            if (isMove)
            {
                switch (this.direction)
                {
                    case Direction.Up:
                        g.DrawImage(images[0], this.X, this.Y);
                        break;
                    case Direction.Down:
                        g.DrawImage(images[1], this.X, this.Y);
                        break;
                    case Direction.Left:
                        g.DrawImage(images[2], this.X, this.Y);
                        break;
                    case Direction.Right:
                        g.DrawImage(images[3], this.X, this.Y);
                        break;
                }
            }
        }

        //己方和敌方坦克开火方式不同，此处提供抽象方法由各子类重写
        public abstract void Fire();

        //坦克炸了
        public abstract void IsOver();

        //坦克出生
        public abstract void Born();
    }
}
