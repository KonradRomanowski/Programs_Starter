using Programs_Starter.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Programs_Starter.ViewModels.Wrappers
{
    public abstract class BaseWrapper<TEntity> : BaseNotificator
    {
        protected TEntity entity;

        protected BaseWrapper(TEntity entity)
        {
            this.entity = entity;
        }
    }
}
