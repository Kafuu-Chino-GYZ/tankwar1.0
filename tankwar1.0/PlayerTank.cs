using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tankwar1._0.Properties;

namespace tankwar1._0
{
    class PlayerTank:TankFather
    {
        //使用static的原因：确保images被加载到内存从而可以被后面调用
        private static Image[] images = {
                                    Resources.mytank_up,
                                    Resources.mytank_down,
                                    Resources.mytank_left,
                                    Resources.mytank_right
        };
        public PlayerTank(int x,int y,int speed,int life,Direction direction)
            :base(x,y,images,speed,life,direction)
        {
            Born();
        }

        //此类用于记录玩家子弹的等级
        public int ZDLevel
        {
            get;
            set;
        }
        public void KeyDown(KeyEventArgs e)
        {
            switch(e.KeyCode)
            {
                case Keys.W:
                    this.direction = Direction.Up;
                    base.Move();
                    break;
                case Keys.S:
                    this.direction = Direction.Down;
                    base.Move();
                    break;
                case Keys.A:
                    this.direction = Direction.Left;
                    base.Move();
                    break;
                case Keys.D:
                    this.direction = Direction.Right;
                    base.Move();
                    break;
                case Keys.J:
                    Fire();
                    break;
            }
        }

        public override void Fire()
        {
            switch(ZDLevel)
            {
                case 0:
                    SingleObject.GetSingle().AddGameObject(new PlayerZiDan(this, 15, 15, 1));
                    break;
                case 1:
                    SingleObject.GetSingle().AddGameObject(new PlayerZiDan(this, 25, 15, 1));
                    break;
                case 2:
                    SingleObject.GetSingle().AddGameObject(new PlayerZiDan(this, 30, 15, 1));
                    break;
            }
            
        }

        public override void IsOver()
        {
            SoundPlayer sp = new SoundPlayer(Resources.hit);
            sp.Play();
            SingleObject.GetSingle().AddGameObject(new boom(this.X - 25, this.Y - 25));
        }

        public override void Born()
        {
            SingleObject.GetSingle().AddGameObject(new TankBorn(this.X, this.Y));
        }
    }
}
