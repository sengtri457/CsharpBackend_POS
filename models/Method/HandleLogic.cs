using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Group1_POS.models.Method
{
    internal class HandleLogic
    {
        public static bool EmptytextBox(params TextBox[] textBoxes)
        {

            foreach (var textBox in textBoxes)
            {
                if (textBox.Text.Equals(""))
                {
                    textBox.Focus();
                    return true;
                }
            }
            return false;
        }
        public static void ClearTextBox(params TextBox[] textBoxes)
        {
            foreach (var textBox in textBoxes)
            {
                if (!textBox.Equals(""))
                {
                    textBox.Clear();
                }
            }

        }
    }
}
