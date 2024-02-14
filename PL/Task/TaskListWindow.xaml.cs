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
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
        public BO.Status Status { get; set; } = BO.Status.None;
        public IEnumerable<BO.Task> TaskList
        {
            get { return (IEnumerable<BO.Task>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }
        public static readonly DependencyProperty TaskListProperty =
         DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.Task>),
        typeof(TaskListWindow), new PropertyMetadata(null));
        public TaskListWindow()
        {
            InitializeComponent();
            TaskList = s_bl?.Task.ReadAll()!;
        }

        private void cbLevelSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TaskList = ((Status == BO.Status.None) ?
                s_bl?.Task.ReadAll()! : s_bl?.Task.ReadAll(item => item.Status == Status))!;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            new TaskWindow().ShowDialog();
        }

        private void lvUpdate_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                BO.Task? task = (sender as ListView)?.SelectedItem as BO.Task;
                new TaskWindow(task!.Id).ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Exception", MessageBoxButton.OK);
            }
        }

        private void Window_activity(object sender, EventArgs e)
        {
            TaskList = s_bl.Task.ReadAll()!;
        }
    }
}
