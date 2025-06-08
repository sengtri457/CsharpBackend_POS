using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Group1_POS.models.Interface
{
    internal abstract class Action : IAction
    {

        public virtual void createRole()
        {
            throw new NotImplementedException();
        }

        public virtual void deleted(DataGridView dg)
        {
            throw new NotImplementedException();
        }

        public virtual void getDataGrid(DataGridView dg)
        {
            throw new NotImplementedException();
        }

        public virtual void update(DataGridView dg)
        {
            throw new NotImplementedException();
        }
    }
}
