using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tankwar1._0.Properties;

namespace tankwar1._0
{
    class boom:GameObject
    {
        //导入图片资源
        private Image[] images =
        {
            Resources.blast1,
            Resources.blast2,
            Resources.blast3,
            Resources.blast4,
            Resources.blast5,
            Resources.blast6,
            Resources.blast7,
            Resources.blast8
        };

        public boom(int x,int y):base(x,y)
        {

        }

        public override void Draw(Graphics g)
        {
            for(int i=0;i<images.Length;i++)
            {
                g.DrawImage(images[i], this.X, this.Y);
            }

            //爆炸图片播放完成则销毁自己
            SingleObject.GetSingle().RemoveGameObject(this);
        }
    }
}
