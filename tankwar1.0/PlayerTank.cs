using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
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
            }
        }
    }
}
