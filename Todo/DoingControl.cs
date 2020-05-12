using System;
using System.Drawing;
using System.Windows.Forms;

namespace Todo
{
    public partial class DoingControl : UserControl
    {
        string _todoListName;
        int _index;

        public DoingControl(string todoListName, int index)
        {
            InitializeComponent();

            deleteButton.FlatAppearance.BorderSize = 0;
            deleteButton.FlatStyle = FlatStyle.Flat;

            _todoListName = todoListName;
            _index = index;
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            BackColor = Color.Green;
            textLabel.Text = "Deleted";
            deleteButton.Visible = false;

            Save.RenameDoing(_index, "Deleted", _todoListName);
        }

        private void DoingControl_MouseClick(object sender, MouseEventArgs e)
        {
            DoingEditForm doingEditForm = new DoingEditForm(_index, textLabel.Text, _todoListName);
            doingEditForm.ShowDialog();
            textLabel.Text = doingEditForm.GetDoingText();
        }

        public void ChangeTextLabel(string text)
        {
            textLabel.Text = text;
        }

        private void textLabel_MouseClick(object sender, MouseEventArgs e)
        {
            DoingEditForm doingEditForm = new DoingEditForm(_index, textLabel.Text, _todoListName);
            doingEditForm.ShowDialog();
            textLabel.Text = doingEditForm.GetDoingText();
        }
    }
}
