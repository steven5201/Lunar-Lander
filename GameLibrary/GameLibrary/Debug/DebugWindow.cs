using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameLibrary.Debug
{
    public partial class DebugWindow : Form
    {
        public DebugWindow()
        {
            InitializeComponent();
        }

        public void UpdateValues(List<Tuple<string, string>> debugVarsNameValue)
        {
            listBox1.Items.Clear();

            foreach (Tuple<string, string> item in debugVarsNameValue)
            {
                listBox1.Items.Add(item.Item1 + ": " + item.Item2);
            }
        }
    }
}
