using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Group1_POS.models.Interface
{
    internal interface IAction
    {
        void createRole();
        void update(DataGridView dg);
        void deleted(DataGridView dg);
        void getDataGrid(DataGridView dg);
    }
}
