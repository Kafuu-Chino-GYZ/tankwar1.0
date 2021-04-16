using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tankwar1._0.Properties;

namespace tankwar1._0
{
    class PlayerZiDan:ZiDanFather
    {
        //导入玩家子弹照片
        private static Image image = Resources.tankmissile;

        //玩家子弹构造
        public PlayerZiDan(TankFather tf,int speed,int life,int power)
            :base(tf,speed,life,power,image)
        {

        }
    }
}
