using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DSheldon {
    public partial class Main : Form {
        public Main() {
            InitializeComponent();
        }

        private void Convert_Click(object sender, EventArgs e) {
            try {
                XmlToConvert.Text = BullHornXmlConverter.ToSimplyHiredXml(XmlToConvert.Text);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
