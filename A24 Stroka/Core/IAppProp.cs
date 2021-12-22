using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A24_Stroka.Core
{
    public interface IAppProp
    {
        string Status { get; }
        event EventHandler StatusChanged;
        void ChangeStatus(string name);
    }
}
