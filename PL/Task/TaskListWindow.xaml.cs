using PL.Engineer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Task
{
    /// <summary>
    /// Interaction logic for TaskListWindow.xaml
    /// </summary>
    public partial class TaskListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get(); // Static field to hold a reference to the bl instance.

        public BO.Status Status { get; set; } = BO.Status.None; // Property to hold the status filter for tasks.

        // Dependency property to bind the task list to the UI.
        public IEnumerable<BO.Task> TaskList
        {
            get { return (IEnumerable<BO.Task>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }

        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.Task>),
            typeof(TaskListWindow), new PropertyMetadata(null));

        // Constructor for the TaskListWindow class.
        public TaskListWindow()
        {
            InitializeComponent();
            TaskList = s_bl?.Task.ReadAll()!; // Initializing the task list with all tasks from the bl.
        }

        // Event handler for the selection change in the status combo box.
        private void cbLevelSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Updating the task list based on the selected status.
            TaskList = ((Status == BO.Status.None) ?
                s_bl?.Task.ReadAll()! : s_bl?.Task.ReadAll(item => item.Status == Status))!;
        }

        // Event handler for the "Add" button click.
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Opening a new TaskWindow for adding a new task.
            new TaskWindow().ShowDialog();
        }

        // Event handler for double-clicking on a task in the ListView.
        private void lvUpdate_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                // Getting the selected task.
                BO.Task? task = (sender as ListView)?.SelectedItem as BO.Task;
                // Opening a TaskWindow for updating the selected task.
                new TaskWindow(task!.Id).ShowDialog();
            }
            catch (Exception ex)
            {
                //  Displaying any exceptions that occur.
                MessageBox.Show($"{ex.Message}", "Exception", MessageBoxButton.OK);
            }
        }

        // Event handler for the window's activity.
        private void Window_activity(object sender, EventArgs e)
        {
            // Refreshing the task list when the window is activated.
            TaskList = s_bl.Task.ReadAll()!;
        }
    }
}