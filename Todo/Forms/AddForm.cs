using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Todo
{
    public partial class AddForm : Form
    {
        public AddForm()
        {
            InitializeComponent();
            addButton.FlatAppearance.BorderSize = 0;
            addButton.FlatStyle = FlatStyle.Flat;
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            List<string> todoListNames = new List<string>(Save.GetAllTodoLists());
            foreach (var name in todoListNames) // Проверяем, есть ли такое имя что мы вписали в TextBox, в списке туду листов
                if (name == addTextBox.Text) // Если такое имя есть
                {
                    MessageBox.Show("Такой Todo уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); // Пишем ошибку
                    addTextBox.Text = "";
                    return; //Прекращаем работу метода
                }
            try
            {
                if (addTextBox.Text != "")
                    Save.CreateTodoList(addTextBox.Text);
                Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Название содержит недопустимые знаки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                addTextBox.Text = "";
            }
        }

        public string GetCreatedTodo()
        {
            return addTextBox.Text;
        }
    }
}
