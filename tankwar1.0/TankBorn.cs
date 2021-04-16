using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tankwar1._0.Properties;

namespace tankwar1._0
{
    /*
     此类用于实现坦克出生时的闪烁效果
         */
    class TankBorn:GameObject
    {
        //导入闪烁的图片
        private Image[] images =
        {
            Resources.born1,
            Resources.born2,
            Resources.born3,
            Resources.born4
        };

        public TankBorn(int x,int y):base(x,y)
        {

        }

        //此变量用于控制闪烁时间
        int time = 0;
        public override void Draw(Graphics g)
        {
            time++;
            for (int i=0;i<images.Length;i++)
            {
                switch(time%10)
                {
                    case 1:
                        g.DrawImage(images[0], this.X, this.Y);
                        break;
                    case 3:
                        g.DrawImage(images[1], this.X, this.Y);
                        break;
                    case 5:
                        g.DrawImage(images[2], this.X, this.Y);
                        break;
                    case 7:
                        g.DrawImage(images[3], this.X, this.Y);
                        break;
                }
            }

            //闪烁完成后应该将闪烁的图片移除
            if(time%50==0)
            {
                SingleObject.GetSingle().RemoveGameObject(this);
            }

            
        }
    }
}
