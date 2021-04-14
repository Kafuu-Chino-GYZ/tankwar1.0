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
        //添加游戏对象
        public void AddGameObject(GameObject go)
        {
            if(go is PlayerTank)   //is 类型转换，转换成功返回true
            {
                PT = go as PlayerTank;   //as类型转换，转换成功返回对应的对象，否则返回null
            }
        }
        //绘制游戏对象
        public void Draw(Graphics g)
        {
            PT.Draw(g);
        }
    }
}
