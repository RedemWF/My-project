using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    /// <summary>
    /// 管理器基类
    /// </summary>
    public class BaseManager
    {
        protected GameFacade facade;
        public BaseManager(GameFacade facade)
        {
            this.facade = facade;
        }
        public virtual void Init() { }
        public virtual void Destroy() { }

        public virtual void Update()
        {

        }
    }
}
