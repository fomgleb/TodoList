using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Todo
{
    public partial class MainForm : Form
    {
        List<DoingControl> doingControls = new List<DoingControl>();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            Button[] buttonsArray = { editButton, addTodoButton, deleteButton, addDoingButton };
            foreach (var button in buttonsArray)//Ставлю для всех кнопок свои настройки, так как их нету в свойствах
            {
                button.FlatAppearance.BorderSize = 0;
                button.FlatStyle = FlatStyle.Flat;
            }

            foreach (var doingControl in doingControls)
            {
                doingControl.Width = panel.Width - 20;
            }

            Save.CreateMainDirectory("TodoSaves");

            LoadTodoListsToListBox(todoListBox);
        }

        private void LoadTodoListsToListBox(ListBox listBox)
        {
            List<string> AllTodoNames = new List<string>(Save.GetAllTodoLists());

            listBox.Items.Clear();
            foreach (var todoName in AllTodoNames)
                listBox.Items.Add(todoName);
        }

        private void LoadDoingToPanel(Panel panel, string todoListName)
        {
            List<string> doingList = new List<string>(Save.GetAllDoing(todoListName));

            panel.Controls.Clear();

            int top = 10;
            int left = 10;
            int i = 0;
            foreach (var doing in doingList)
            {
                DoingControl doingControl  = new DoingControl(todoListName, i);
                doingControl.ChangeTextLabel(doing);
                panel.Controls.Add(doingControl);
                doingControl.Top = top;
                doingControl.Left = left;

                doingControls.Add(doingControl);
                foreach (var doingControll in doingControls)
                {
                    doingControll.Width = panel.Width - 20;
                }

                top += 65;
                i++;
            }
        }

        #region ButtonsClick
        private void addTodoButton_Click(object sender, EventArgs e)
        {
            AddForm addForm = new AddForm();
            addForm.ShowDialog();
            LoadTodoListsToListBox(todoListBox);
            todoListBox.SelectedItem = addForm.GetCreatedTodo();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (todoListBox.SelectedItem != null) //Если что-то выбрано в todoListBox
            {
                Save.DeleteTodoList(todoListBox.SelectedItem.ToString());//Удаляем тудулист который выбраный в todoListBox
                LoadTodoListsToListBox(todoListBox);
                if(todoListBox.Items.Count != 0)
                    todoListBox.SelectedIndex = 0;
            }
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (todoListBox.SelectedItem != null)
            {
                EditForm editForm = new EditForm(todoListBox.SelectedItem.ToString());
                editForm.ShowDialog();
                LoadTodoListsToListBox(todoListBox);
                todoListBox.SelectedItem = editForm.GetEditedTodo();
            }
        }

        private void todoListBox_DoubleClick(object sender, EventArgs e)
        {
            editButton_Click(editButton, null);
        }

        private void todoListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (todoListBox.SelectedItem != null)
            {
                addDoingButton.Visible = true;
                LoadDoingToPanel(panel, todoListBox.SelectedItem.ToString());
                Save.DeleteDoingByText("Deleted"); // При закрытии формы удаляем все doing в которых текст: Deleted
            }
        }

        private void addDoingButton_Click(object sender, EventArgs e)
        {
            Save.CreateDoing("Text", todoListBox.SelectedItem.ToString());

            Save.DeleteDoingByText("Deleted"); // При закрытии формы удаляем все doing в которых текст: Deleted

            LoadDoingToPanel(panel, todoListBox.SelectedItem.ToString());
        }

        #endregion

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Save.DeleteDoingByText("Deleted"); // При закрытии формы удаляем все doing в которых текст: Deleted
        }

        private void panel_SizeChanged(object sender, EventArgs e)
        {
            foreach (var doingControl in doingControls)
            {
                doingControl.Width = panel.Width - 20;
            }
        }
    }
}
