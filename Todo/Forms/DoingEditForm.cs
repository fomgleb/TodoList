using System;
using System.Windows.Forms;

namespace Todo
{
    public partial class DoingEditForm : Form
    {
        string _doingText, _todoListName;

        public DoingEditForm(string doingText, string _todoListName)
        {
            InitializeComponent();

            _doingText = doingText;
            editTextBox.Text = doingText;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Save.RenameDoing(_doingText, editTextBox.Text, _todoListName);
        }
    }
}
