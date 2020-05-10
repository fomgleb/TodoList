using System;
using System.Drawing;
using System.Windows.Forms;

namespace Todo
{
    public partial class TodoItem : UserControl
    {
        public TodoItem()
        {
            InitializeComponent();
            deleteButton.FlatAppearance.BorderSize = 0;
            deleteButton.FlatStyle = FlatStyle.Flat;
        }


        private void deleteButton_Click(object sender, EventArgs e)
        {
            BackColor = Color.Green;
            textLabel.Text = "Deleted";
            deleteButton.Visible = false;
        }
    }
}
