using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tankwar1._0.Properties;

namespace tankwar1._0
{
    class EnemyTank:TankFather
    {
        //三个数组分别存储三种不同类型的敌人，每个数组的4个元素存储四种不同的状态
        private static Image[] images1 =
        {
            Resources.enemy1_up,
            Resources.enemy1_down,
            Resources.enemy1_left,
            Resources.enemy1_right
        };
        private static Image[] images2 =
        {
            Resources.enemy2_up,
            Resources.enemy2_down,
            Resources.enemy2_left,
            Resources.enemy2_right
        };
        private static Image[] images3 =
        {
            Resources.enemy3_up,
            Resources.enemy3_down,
            Resources.enemy3_left,
            Resources.enemy3_right
        };

        //存储敌人坦克的速度
        private static int _speed;

        //存储敌人坦克的生命
        private static int _life;

        public int EnemyTankType
        {
            get;
            set;
        }

        /*
         通过一个静态方法设置敌人坦克的速度，方便直接调用
         通过传入不同坦克类型设定不同的速度
         */
         public static int setspeed(int type)
        {
            switch(type)
            {
                case 1:
                    _speed = 5;
                    break;
                case 2:
                    _speed = 8;
                    break;
                case 3:
                    _speed = 10;
                    break;
            }
            return _speed;
        }

        /*
         通过一个静态方法设置敌人坦克的生命，方便直接调用
         通过传入不同坦克类型设定不同的生命
         */
         public static int setlife(int type)
        {
            switch(type)
            {
                case 1:
                    _life = 1;
                    break;
                case 2:
                    _life = 2;
                    break;
                case 3:
                    _life = 3;
                    break;
            }
            return _life;
        }


        public EnemyTank(int x,int y,int type,Direction direction):base(x,y,images1,setspeed(type),setlife(type),direction)
        {
            this.EnemyTankType = type;
        }

        //向窗体当中绘制敌人坦克
        public override void Draw(Graphics g)
        {
            //绘制后立即让坦克开始移动
            Move();
            switch(EnemyTankType)
            {
                case 0:
                    switch(this.direction)
                    {
                        case Direction.Up:
                            g.DrawImage(images1[0], this.X, this.Y);
                            break;
                        case Direction.Down:
                            g.DrawImage(images1[1], this.X, this.Y);
                            break;
                        case Direction.Left:
                            g.DrawImage(images1[2], this.X, this.Y);
                            break;
                        case Direction.Right:
                            g.DrawImage(images1[3], this.X, this.Y);
                            break;
                    }
                    break;
                case 1:
                    switch (this.direction)
                    {
                        case Direction.Up:
                            g.DrawImage(images2[0], this.X, this.Y);
                            break;
                        case Direction.Down:
                            g.DrawImage(images2[1], this.X, this.Y);
                            break;
                        case Direction.Left:
                            g.DrawImage(images2[2], this.X, this.Y);
                            break;
                        case Direction.Right:
                            g.DrawImage(images2[3], this.X, this.Y);
                            break;
                    }
                    break;
                case 2:
                    switch (this.direction)
                    {
                        case Direction.Up:
                            g.DrawImage(images3[0], this.X, this.Y);
                            break;
                        case Direction.Down:
                            g.DrawImage(images3[1], this.X, this.Y);
                            break;
                        case Direction.Left:
                            g.DrawImage(images3[2], this.X, this.Y);
                            break;
                        case Direction.Right:
                            g.DrawImage(images3[3], this.X, this.Y);
                            break;
                    }
                    break;
            }
            if(r.Next(0,100)<2)
            {
                Fire();
            }
        }

        //将此随机数对象设置为静态，让所有敌方坦克对象公用一个对象，避免所有坦克转向同一方向
        static Random r = new Random();
        
        public override void Move()
        {
            base.Move();
            //当移动到窗体边缘时，换个方向继续移动

            //给一个很小的概率产生随机数，避免敌方坦克鬼畜
            if(r.Next(0,100)<5)
            {
                switch (r.Next(0, 4))
                {
                    case 0:
                        this.direction = Direction.Up;
                        break;
                    case 1:
                        this.direction = Direction.Down;
                        break;
                    case 3:
                        this.direction = Direction.Left;
                        break;
                    case 4:
                        this.direction = Direction.Right;
                        break;
                }
            }
            

        }
         //敌人发射子弹
        public override void Fire()
        {
            SingleObject.GetSingle().AddGameObject(new EnemyZiDan(this, 10, 10, 1));
        }

        public override void IsOver()
        {
            SingleObject.GetSingle().AddGameObject(new boom(this.X - 25, this.Y - 25));
            SingleObject.GetSingle().RemoveGameObject(this);
        }
    }
}
