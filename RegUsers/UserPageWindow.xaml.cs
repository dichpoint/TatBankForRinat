using System.Windows;
using System.ComponentModel;
using RegUsers.Models;
using RegUsers.Services;
using System;

namespace RegUsers
{
    /// <summary>
    /// Логика взаимодействия для UserPageWindow.xaml
    /// </summary>

    // Кабинет пользователя
    public partial class UserPageWindow : Window
    {
        private readonly string PATH = $"{Environment.CurrentDirectory}\\ToDoDataList.json";
        private BindingList<ToDoModel> _toDoDataList;
        private FileIOService _fileIOService;

        // конструктор
        public UserPageWindow()
        {
            InitializeComponent();
        }

        private void ToDoList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _fileIOService = new FileIOService(PATH);

            try
            {
                _toDoDataList = _fileIOService.LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Close();
            }

            ToDoList.ItemsSource = _toDoDataList;
            _toDoDataList.ListChanged += _toDoDataList_ListChanged;
        }

        private void _toDoDataList_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemDeleted || e.ListChangedType == ListChangedType.ItemChanged)
            {
                try
                {
                    _fileIOService.SaveData(sender);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Close();
                }
            }
        }
    }
}