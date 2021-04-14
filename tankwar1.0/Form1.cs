using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tankwar1._0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //对游戏进行初始化
            InitialGame();
        }

        //初始化游戏
        private void InitialGame()
        {
            SingleObject.GetSingle().AddGameObject(new PlayerTank(200, 200, 72, 72, Direction.Up));

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            SingleObject.GetSingle().Draw(e.Graphics);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            SingleObject.GetSingle().PT.KeyDown(e);
        }

        private void timer_refresh_Tick(object sender, EventArgs e)
        {
            //每隔50ms对窗口进行更新
            this.Invalidate();
        }
    }
}
