using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tankwar1._0
{
    /*
    游戏对象的父类：GameObject
	成员：
		游戏对象的X,Y坐标、高度、宽度（用于碰撞检测）、方向、速度、以及生命值
		绘制游戏对象的抽象方法Draw（）
		游戏对象移动的方法Move（）
		返回矩形的方法，用于碰撞检测GetRectangle（）
     */

    //此枚举对象包含游戏对象运动的四个方向
    enum Direction
    {
        Up, Down, Left, Right
    }
    abstract class GameObject
    {
        /*
         自动属性写法，虽然未声明字段，编译时会自动生成字段
        */
        #region 游戏对象的属性
        //横坐标
        public int X
        {
            get;
            set;
        }
        //纵坐标
        public int Y
        {
            get;
            set;
        }
        //宽度
        public int Width
        {
            get;
            set;
        }
        //高度
        public int Height
        {
            get;
            set;
        }
        //速度
        public int Speed
        {
            get;
            set;
        }
        //生命值
        public int Life
        {
            get;
            set;
        }
        public Direction direction
        {
            get;
            set;
        }
        #endregion

        //初始化对象
        public GameObject(int x,int y,int width,int height,int speed,int life,Direction direction)
        {
            this.X = x;
            this.Y = y;
            this.Width = width;
            this.Height = height;
            this.Speed = speed;
            this.Life = life;
            this.direction = direction;
        }

        //绘制游戏对象的抽象方法
        public abstract void Draw(Graphics g);

        //游戏对象的移动方法,根据当前游戏对象的方向进行移动
        //使用虚类，子类可选择性重写
        public virtual void Move()
        {
            switch(this.direction)
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
            if(this.X<=0)
            {
                this.X = 0;
            }
            if(this.Y<=0)
            {
                this.Y = 0;
            }
            if (this.X >= 928)
            {
                this.X = 928;
            }
            if(this.Y>=690)
            {
                this.Y = 690;
            }
        }
        //此方法用于碰撞检测
        public Rectangle GetRectangle()
        {
            return new Rectangle(this.X, this.Y, this.Width, this.Height);
        }

        public GameObject(int x, int y) : this(x, y, 0, 0, 0, 0, 0)  //this 1.代表当前类的对象 2.显式地调用自己类当中的构造函数
        {

        }

        public GameObject(int x,int y,int width,int height):this(x,y,width,height,0,0,0)
        {

        }
    }
}
