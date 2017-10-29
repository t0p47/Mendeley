using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mendeley
{
    public class Types
    {
        String[] typesName = {"Journal Article","Book","Film" };
        int comboBoxTypesWidth = 243;

        public TabPage JournalArticle() {
            TabPage tabPage1 = new TabPage("Details");
            Label lType = new Label();
            lType.Text = lType.Location.ToString();
            
            ComboBox comboBoxType = new ComboBox();
            comboBoxType.Items.AddRange(typesName);


            tabPage1.Controls.Add(lType);
            tabPage1.Controls.Add(comboBoxType);
            return tabPage1;
        }
    }
}
