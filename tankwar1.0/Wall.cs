using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tankwar1._0.Properties;

namespace tankwar1._0
{
    //此类用于建围墙
    class Wall:GameObject
    {
        private static Image image = Resources.wall;
        public Wall(int x,int y):base(x,y,image.Width,image.Height)
        {

        }

        public override void Draw(Graphics g)
        {
            g.DrawImage(image, this.X, this.Y);
        }
    }
}
