using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mendeley
{
    public static class Prompt
    {
        public static string ShowDialog(string text, string caption, string textBoxVal) {
            Form prompt = new Form()
            {
                Width = 500,
                Height = 150,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };

            Label textLabel = new Label() { Left=50, Top=20, Text=text };
            TextBox textBox = new TextBox() { Left = 50, Top = 50, Width = 400, Text=textBoxVal };
            Button confirmButton = new Button() { Text = "Ok", Left = 350, Width = 100, Top = 70, DialogResult = DialogResult.OK };
            confirmButton.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmButton);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmButton;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }
    }
}
