﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MagicMirror.Common.MVVM
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync();
        bool CanExecute();
    }

    public interface IAsyncParameterCommand<T> : ICommand
    {
        Task ExecuteAsync(T param);
        bool CanExecute();
    }
}
