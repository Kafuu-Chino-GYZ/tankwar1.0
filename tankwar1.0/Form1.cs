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
            SingleObject.GetSingle().AddGameObject(new PlayerTank(200, 200, 5, 72, Direction.Up));
            SetEnemyTanks();

        }

        //此随机数用于随机设置敌人坦克的坐标，防止重叠
        Random r = new Random();

        //初始化敌人坦克对象
        public void SetEnemyTanks()
        {
            for(int i=0;i<8;i++)
            {
                SingleObject.GetSingle().AddGameObject(new EnemyTank(r.Next(0, this.Width), r.Next(0, this.Height),
                                                        r.Next(0, 3), Direction.Down));
            }
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
            SingleObject.GetSingle().PZJC();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //让窗体不在闪烁
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.AllPaintingInWmPaint,
                true);
        }
    }
}
