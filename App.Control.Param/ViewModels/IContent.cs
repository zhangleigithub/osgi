using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace App.Control.Param.ViewModels
{
    public interface IContent
    {
        string Title { get; }
        bool CanClose { get; }
    }
}
