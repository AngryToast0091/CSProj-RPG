using ClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Activity_System
{
    public interface IActivity
    {
        string Name { get; }
        void Execute(Character character);
    }
}
