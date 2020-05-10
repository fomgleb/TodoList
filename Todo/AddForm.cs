using System;
using System.Windows.Forms;

namespace Todo
{
    public partial class AddForm : Form
    {
        public AddForm()
        {
            InitializeComponent();
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            try
            {
                Save.CreateTodoList(addTextBox.Text);
                Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Название содержит недопустимые знаки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                addTextBox.Text = "";
            }
        }
    }
}
