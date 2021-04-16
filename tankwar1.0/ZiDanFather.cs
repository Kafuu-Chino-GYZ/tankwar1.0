using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tankwar1._0
{
    class ZiDanFather:GameObject
    {
        //存储子弹图片
        private Image image;

        public Image Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
            }
        }

        //存储子弹威力
        public int power
        {
            get;
            set;
        }

        public ZiDanFather(TankFather tf,int speed,int life,int power,Image image)
            :base(tf.X+tf.Width/2-6,tf.Y+tf.Height/2-6,image.Width,image.Height,speed,life,tf.direction)
        {
            this.image = image;
            this.power = power;
        }

        public override void Draw(Graphics g)
        {
            switch (this.direction)
            {
                case Direction.Up:
                    this.Y -= this.Speed;
                    break;
                case Direction.Down:
                    this.Y += this.Speed;
                    break;
                case Direction.Left:
                    this.X -= this.Speed;
                    break;
                case Direction.Right:
                    this.X += this.Speed;
                    break;
            }
            //在游戏对象完成后，判断当前游戏对象是否超出当前窗体
            if (this.X <= 0)
            {
                this.X = -100;
            }
            if (this.Y <= 0)
            {
                this.Y = -100;
            }
            if (this.X >= 1000)
            {
                this.X = 1000;
            }
            if (this.Y >= 750)
            {
                this.Y = 1000;
            }
            g.DrawImage(image, this.X, this.Y);
        }
    }
}
