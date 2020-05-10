using System;
using System.Drawing;
using System.Windows.Forms;

namespace Todo
{
    public partial class DoingControl : UserControl
    {
        string _todoListName;

        public DoingControl(string todoListName)
        {
            InitializeComponent();

            deleteButton.FlatAppearance.BorderSize = 0;
            deleteButton.FlatStyle = FlatStyle.Flat;

            _todoListName = todoListName;

        }


        private void deleteButton_Click(object sender, EventArgs e)
        {
            BackColor = Color.Green;
            textLabel.Text = "Deleted";
            deleteButton.Visible = false;
        }

        private void DoingControl_MouseClick(object sender, MouseEventArgs e)
        {
            DoingEditForm doingEditForm = new DoingEditForm(textLabel.Text, _todoListName);
            doingEditForm.ShowDialog();
        }
    }
}
