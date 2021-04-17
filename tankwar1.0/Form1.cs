using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using tankwar1._0.Properties;
using System.Threading;

namespace tankwar1._0
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            //对游戏进行初始化
            InitialGame();
        }

        //初始化游戏
        private void InitialGame()
        {
            SingleObject.GetSingle().AddGameObject(new PlayerTank(200, 200, 5, 72, Direction.Up));
            //SetEnemyTanks();
            Thread thread_enemytank = new Thread(new ThreadStart(SetEnemyTanks));
            thread_enemytank.Start();
        }

        //此随机数用于随机设置敌人坦克的坐标，防止重叠
        Random r = new Random();

        //此变量用于标识Form1打开关闭状态
        private bool isopen = true;

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

        //此方法利用一个time_refresh进行窗口刷新以及碰撞检测，已被下方refreshForm方法替代
        //此替代处于多线程考虑
        private void timer_refresh_Tick(object sender, EventArgs e)
        {
            ////每隔30ms对窗口进行更新
            //this.Invalidate();
            //SingleObject.GetSingle().PZJC();
        }

        private void refreshForm()
        {
            while(true)
            {
                Thread.Sleep(30);
                this.Invalidate();
                SingleObject.GetSingle().PZJC();
                if(!isopen)
                {
                    break;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //通过c#提供的双缓冲方法让窗体不再闪烁
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.AllPaintingInWmPaint,
                true);

            //在程序加载的时候播放开始音乐
            SoundPlayer sp = new SoundPlayer(Resources.start);
            sp.Play();

            InitialMap();

            //这个线程用于进行碰撞检测
            Thread thread_PZJC = new Thread(new ThreadStart(refreshForm));
            thread_PZJC.Start();

            
        }

        //初始化地图
        public void InitialMap()
        {
            for (int i = 0; i < 10; i++)
            {
                SingleObject.GetSingle().AddGameObject(new Wall(i * 15 + 30, 100));
                SingleObject.GetSingle().AddGameObject(new Wall(95, 100 + 15 * i));

                SingleObject.GetSingle().AddGameObject(new Wall(245 - i * 7, 100 + 15 * i));
                SingleObject.GetSingle().AddGameObject(new Wall(245 + i * 7, 100 + 15 * i));
                SingleObject.GetSingle().AddGameObject(new Wall(215 + i * 15 / 2, 185));

                SingleObject.GetSingle().AddGameObject(new Wall(390 - i * 5, 100 + 15 * i));
                SingleObject.GetSingle().AddGameObject(new Wall(390 + i * 5, 100 + 15 * i));
                SingleObject.GetSingle().AddGameObject(new Wall(480 - i * 5, 100 + 15 * i));

                SingleObject.GetSingle().AddGameObject(new Wall(515, 100 + 15 * i));
                SingleObject.GetSingle().AddGameObject(new Wall(595 - i * 8, 100 + 15 * i / 2));
                SingleObject.GetSingle().AddGameObject(new Wall(530 + i * 8, 165 + 15 * i / 2));
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            isopen = false;
        }
    }
}
