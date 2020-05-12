using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Todo
{
    public partial class DoingEditForm : Form
    {
        string _doingText, _todoListName;
        int _index;

        public DoingEditForm(int index, string doingText , string todoListName)
        {
            InitializeComponent();

            _todoListName = todoListName;
            _doingText = doingText;
            editRichTextBox.Text = doingText;
            _index = index;
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            Save.RenameDoing(_index, editRichTextBox.Text.Replace('\n', ' '), _todoListName);
            Close();
        }

        public string GetDoingText()
        {
            return editRichTextBox.Text.Replace('\n', ' ');
        }
    }
}
