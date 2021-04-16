using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }
    }
}
