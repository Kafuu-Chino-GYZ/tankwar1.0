using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using tankwar1._0.Properties;

namespace tankwar1._0
{
    //此类用于产生全局唯一的游戏对象
    class SingleObject
    {
        private SingleObject()
        {

        }
        public static SingleObject _singleObject = null;

        public static SingleObject GetSingle()
        {
            if(_singleObject==null)
            {
                _singleObject = new SingleObject();
            }
            return _singleObject;
        }

        public PlayerTank PT
        {
            get;
            set;
        }

        //将敌人存储在泛型集合中
        List<EnemyTank> listEnemyTank = new List<EnemyTank>();

        List<PlayerZiDan> listPlayerZiDan = new List<PlayerZiDan>();

        List<EnemyZiDan> listEnemyZiDan = new List<EnemyZiDan>();

        List<boom> listboom = new List<boom>();

        List<TankBorn> listbron = new List<TankBorn>();

        List<zhuangbei> listzhuangbei = new List<zhuangbei>();

        List<Wall> listWall = new List<Wall>();
        //添加游戏对象
        public void AddGameObject(GameObject go)
        {
            if(go is PlayerTank)   //is 类型转换，转换成功返回true
            {
                PT = go as PlayerTank;   //as类型转换，转换成功返回对应的对象，否则返回null
            }
            else if(go is EnemyTank)
            {
                listEnemyTank.Add(go as EnemyTank);
            }
            else if(go is PlayerZiDan)
            {
                listPlayerZiDan.Add(go as PlayerZiDan);
            }
            else if(go is EnemyZiDan)
            {
                listEnemyZiDan.Add(go as EnemyZiDan);
            }
            else if(go is boom)
            {
                listboom.Add(go as boom);
            }
            else if(go is TankBorn)
            {
                listbron.Add(go as TankBorn);
            }
            else if(go is zhuangbei)
            {
                listzhuangbei.Add(go as zhuangbei);
            }
            else if(go is Wall)
            {
                listWall.Add(go as Wall);
            }
        }
        //绘制游戏对象
        public void Draw(Graphics g)
        {
            PT.Draw(g);
            for(int i=0;i<listEnemyTank.Count;i++)
            {
                listEnemyTank[i].Draw(g);
            }
            for(int i=0;i<listPlayerZiDan.Count;i++)
            {
                listPlayerZiDan[i].Draw(g);
            }
            for(int i=0;i<listEnemyZiDan.Count;i++)
            {
                listEnemyZiDan[i].Draw(g);
            }
            for (int i = 0; i < listboom.Count; i++)
            {
                listboom[i].Draw(g);
            }
            for(int i=0;i<listbron.Count;i++)
            {
                listbron[i].Draw(g);
            }
            for(int i=0;i<listzhuangbei.Count;i++)
            {
                listzhuangbei[i].Draw(g);
            }
            for(int i=0;i<listWall.Count; i++)
            {
                listWall[i].Draw(g);
            }
        }

        //移除游戏对象
        public void RemoveGameObject(GameObject go)
        {
            if(go is boom)
            {
                listboom.Remove(go as boom);
            }
            if(go is PlayerZiDan)
            {
                listPlayerZiDan.Remove(go as PlayerZiDan);
            }
            if(go is EnemyZiDan)
            {
                listEnemyZiDan.Remove(go as EnemyZiDan);
            }
            if(go is EnemyTank)
            {
                listEnemyTank.Remove(go as EnemyTank);
            }
            if(go is TankBorn)
            {
                listbron.Remove(go as TankBorn);
            }
            if(go is zhuangbei)
            {
                listzhuangbei.Remove(go as zhuangbei);
            }
            if(go is Wall)
            {
                listWall.Remove(go as Wall);
            }
        }
        
        //碰撞检测
        public void PZJC()
        {
            #region 判断玩家的子弹是否打在了敌人身上
            for(int i=0;i<listPlayerZiDan.Count;i++)
            {
                for(int j=0;j<listEnemyTank.Count;j++)
                {
                    //判断玩家的子弹是否达到了敌人身上
                    if(listPlayerZiDan[i].GetRectangle().IntersectsWith(listEnemyTank[j].GetRectangle()))
                    {
                        //敌人减少生命值
                        listEnemyTank[j].Life -= listPlayerZiDan[i].power;
                        listEnemyTank[j].IsOver();
                        //当玩家坦克的子弹击中敌方坦克，玩家子弹移除
                        listPlayerZiDan.Remove(listPlayerZiDan[i]);
                        break;
                    }
                }
            }
            #endregion

            #region 判断敌人的子弹是否打在了玩家的身上
            for(int i=0;i<listEnemyZiDan.Count;i++)
            {
                if(listEnemyZiDan[i].GetRectangle().IntersectsWith(PT.GetRectangle()))
                {
                    PT.IsOver();
                    listEnemyZiDan.Remove(listEnemyZiDan[i]);
                }
            }
            #endregion

            #region 玩家捡装备的判断
            for(int i=0;i<listzhuangbei.Count;i++)
            {
                //玩家吃到了装备
                if (listzhuangbei[i].GetRectangle().IntersectsWith(PT.GetRectangle()))
                {
                    //装备效果显现
                    JudgeZB(listzhuangbei[i].ZBType);
                    //吃到装备后应该将装备移除
                    listzhuangbei.Remove(listzhuangbei[i]);
                    
                    //添加吃装备的声音
                    SoundPlayer sp = new SoundPlayer(Resources.add);
                    sp.Play();

                }
            }
            #endregion

            #region 敌方坦克和地图的碰撞检测
            for(int i=0;i<listWall.Count;i++)
            {
                for(int j=0;j<listEnemyTank.Count;j++)
                {
                    if(listWall[i].GetRectangle().IntersectsWith
                        (listEnemyTank[j].GetRectangle()))
                    {
                        //敌人和墙体发生碰撞，让敌人的坐标固定到碰撞的位置
                        //判断敌人是从那个方向过来的
                        switch(listEnemyTank[j].direction)
                        {
                            case Direction.Up:
                                listEnemyTank[j].Y = listWall[i].Y + listWall[i].Height;
                                break;
                            case Direction.Down:
                                listEnemyTank[j].Y = listWall[i].Y - listWall[i].Height;
                                break;
                            case Direction.Left:
                                listEnemyTank[j].X = listWall[i].X + listWall[i].Width;
                                break;
                            case Direction.Right:
                                listEnemyTank[j].X= listWall[i].X - listWall[i].Width;
                                break;

                        }
                    }
                }
            }
            #endregion

            #region 判断玩家的子弹是否打到了墙体
            for(int i=0;i<listPlayerZiDan.Count;i++)
            {
                for(int j=0;j<listWall.Count;j++)
                {
                    if(listPlayerZiDan[i].GetRectangle().
                        IntersectsWith(listWall[j].GetRectangle()))
                    {
                        //子弹打到了墙上
                        listPlayerZiDan.Remove(listPlayerZiDan[i]);
                        listWall.Remove(listWall[j]);
                        break;
                    }
                }
            }
            #endregion
            #region 判断敌人的子弹与墙体的碰撞检测
            for (int i = 0; i < listEnemyZiDan.Count; i++)
            {
                for (int j = 0; j < listWall.Count; j++)
                {
                    if (listEnemyZiDan[i].GetRectangle().
                        IntersectsWith(listWall[j].GetRectangle()))
                    {
                        //子弹打到了墙上
                        listEnemyZiDan.Remove(listEnemyZiDan[i]);
                        listWall.Remove(listWall[j]);
                        break;
                    }
                }
            }
            #endregion

            #region 判断敌人子弹与我方子弹相撞
            for (int i = 0; i < listEnemyZiDan.Count; i++)
            {
                for (int j = 0; j < listPlayerZiDan.Count; j++)
                {
                    if (listEnemyZiDan[i].GetRectangle().
                        IntersectsWith(listPlayerZiDan[j].GetRectangle()))
                    {
                        listEnemyZiDan.Remove(listEnemyZiDan[i]);
                        listPlayerZiDan.Remove(listPlayerZiDan[j]);
                        break;
                    }
                }
            }
            #endregion
        }

        //判断装备类型
        public void JudgeZB(int type)
        {
            switch(type)
            {
                case 0: //吃到五角星，让玩家子弹速度变快
                    if(PT.ZDLevel<2)
                    {
                        PT.ZDLevel++;
                    }
                    break;
                case 1:  //吃到地雷，世界核平
                    for(int i=0;i<listEnemyTank.Count;i++)
                    {
                        listEnemyTank[i].Life = 0;
                        listEnemyTank[i].IsOver();
                    }
                    break;
                case 2://吃到计时器，时停~~~~！
                    for(int i=0;i<listEnemyTank.Count;i++)
                    {
                        listEnemyTank[i].canmove = false;
                    }

                    break;
            }
        }
    }
}
