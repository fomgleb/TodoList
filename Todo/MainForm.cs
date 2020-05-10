using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Todo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Button[] buttonsArray = { editButton, addButton, deleteButton };
            foreach (var button in buttonsArray)//Ставлю для всех кнопок свои настройки, так как их нету в свойствах
            {
                button.FlatAppearance.BorderSize = 0;
                button.FlatStyle = FlatStyle.Flat;
            }

            Save.CreateMainDirectory("TodoSaves");

            LoadDataTo(todoListBox);
        }

        private void LoadDataTo(ListBox listBox)
        {
            List<string> AllTodoNames = new List<string>(Save.GetAllTodoLists());

            listBox.Items.Clear();
            foreach (var todoName in AllTodoNames)
                listBox.Items.Add(todoName);
        }

        #region ButtonsClick
        private void addButton_Click(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm();
            addForm.ShowDialog();
            LoadDataTo(todoListBox);
        }
        #endregion

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (todoListBox.SelectedItem != null) //Если что-то выбрано в todoListBox
            {
                Save.DeleteTodoList(todoListBox.SelectedItem.ToString());//Удаляем тудулист который выбраный в todoListBox
                LoadDataTo(todoListBox);
            }
        }
    }
}
