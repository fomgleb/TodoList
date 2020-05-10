using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Todo
{
    public partial class EditForm : Form
    {
        string _todoListName;

        public EditForm(string todoListName)
        {
            InitializeComponent();

            _todoListName = todoListName;
            editTextBox.Text = todoListName;//Вписываем имя todoList, которое мы указали в todoListName, в ТекстБокс.
        }

        private void saveButton_Click(object sender, System.EventArgs e)
        {
            List<string> todoListNames = new List<string>();
            foreach (var name in todoListNames) // Проверяем, есть ли такое имя что мы вписали в TextBox, в списке туду листов
                if (name == editTextBox.Text) // Если такое имя есть
                {
                    MessageBox.Show("Такой Todo уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error); // Пишем ошибку
                    return; //Прекращаем работу метода
                }
            try
            {
                Save.RenameTodoList(_todoListName, editTextBox.Text);
                Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Название содержит недопустимые знаки.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                editTextBox.Text = _todoListName;
            }
        }
    }
}
