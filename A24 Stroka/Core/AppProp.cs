using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A24_Stroka.Core
{
    public class AppProp : IAppProp
    {
        private string _status;
        public string Status
        {
            get { return _status; }
            private set
            {
                _status = value;
                RaiseStatusChanged();
            }
        }
        public event EventHandler StatusChanged;
        protected virtual void RaiseStatusChanged()
        {
            StatusChanged?.Invoke(this, EventArgs.Empty);
        }
        public void ChangeStatus(string status)
        {
            if (Status == status)
            {
                return;
            }

            if (status == null)
            {
                return;
            }

            Status = status;
        }
    }
}
