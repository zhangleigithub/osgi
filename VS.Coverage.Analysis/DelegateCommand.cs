using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace VS.Coverage.Analysis
{
    public class DelegateCommand : ICommand
    {
        #region 字段

        /// <summary>
        /// 能否执行
        /// </summary>
        private bool canExecuted = true;

        #endregion

        #region 事件

        /// <summary>
        /// 能否执行更改后
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// 执行
        /// </summary>
        public Action OnExecuted;

        #endregion

        #region 外部接口

        /// <summary>
        /// 能否执行
        /// </summary>
        /// <param name="parameter">参数</param>
        /// <returns>能否执行</returns>
        public bool CanExecute(object parameter)
        {
            return canExecuted;
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="parameter">参数</param>
        public void Execute(object parameter)
        {
            if (OnExecuted != null)
            {
                OnExecuted();
            }
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="value">值</param>
        public void SetCanExecuted(bool value)
        {
            if (value != canExecuted)
            {
                canExecuted = value;

                if (CanExecuteChanged != null)
                {
                    CanExecuteChanged(this, new EventArgs());
                }
            }
        }

        #endregion
    }
}
