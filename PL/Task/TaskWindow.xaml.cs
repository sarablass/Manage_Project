using BO;
using PL.Engineer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
    /// Interaction logic for TaskWindow.xaml
    /// </summary>
    public partial class TaskWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get(); // Static field to hold a reference to the bl instance.

        public BO.Status Status { get; set; } = BO.Status.None; // Property to hold the status for tasks.

        // Dependency property to bind the current task to the UI.
        public BO.Task? CurrentTask
        {
            get { return (BO.Task)GetValue(CurrentTaskProperty); }
            set { SetValue(CurrentTaskProperty, value); }
        }

        public static readonly DependencyProperty CurrentTaskProperty =
            DependencyProperty.Register("CurrentTask", typeof(BO.Task),
            typeof(TaskWindow), new PropertyMetadata(null));

        // Dependency property to bind the task list to the UI.
        public IEnumerable<BO.TaskInList> TaskList
        {
            get { return (IEnumerable<BO.TaskInList>)GetValue(TaskListProperty); }
            set { SetValue(TaskListProperty, value); }
        }

        public static readonly DependencyProperty TaskListProperty =
            DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.TaskInList>),
            typeof(TaskWindow), new PropertyMetadata(null));

        // Dependency property to bind the task dependencies to the UI.
        public IEnumerable<BO.TaskInList> TaskDependencies
        {
            get { return (IEnumerable<BO.TaskInList>)GetValue(TaskDependenciesProperty); }
            set { SetValue(TaskDependenciesProperty, value); }
        }

        public static readonly DependencyProperty TaskDependenciesProperty =
            DependencyProperty.Register("TaskDependencies", typeof(IEnumerable<BO.TaskInList>), typeof(TaskWindow), new PropertyMetadata(null));

        public int CheckedDependTask { get; set; } = 0; // Property to hold the ID of the selected dependency task.

        // Constructor for the TaskWindow class.
        public TaskWindow(int id = 0)
        {
            try
            {
                // Initializing the window with the data of the specified task or with empty data if no ID is provided.
                if (id != 0)
                {
                    CurrentTask = s_bl!.Task.Read(id)!;
                }
                else
                {
                    CurrentTask = new BO.Task()
                    {
                        // Initializing a new task object with default values.
                        Id = 0,
                        Description = "",
                        Alias = "",
                        Milestone = null,
                        Status = null,
                        CreateAt = DateTime.Now,
                        BaselineStartDate = null,
                        StartDate = null,
                        ForecastDate = null,
                        DeadlineDate = null,
                        CompleteDate = null,
                        Deliverables = "",
                        Remarks = "",
                        Engineer = new EngineerInTask()
                        {
                            Id = 0,
                            Name = ""
                        },
                        ComplexityLevel = null,
                        Dependencies = null,
                        IsActive = false
                    };
                }

                // Initializing the task dependencies collection based on the current task's dependencies.
                TaskDependencies = CurrentTask.Dependencies != null ? new ObservableCollection<BO.TaskInList>(CurrentTask.Dependencies) : new ObservableCollection<BO.TaskInList>();

                // Retrieving and initializing the task list for dependency selection.
                var task = s_bl?.Task.ReadAll().Select(x => new BO.TaskInList()
                {
                    Id = x.Id,
                    Alias = x.Alias,
                    Description = x.Description,
                    Status = x.Status
                }).ToList();

                TaskList = task != null ? new ObservableCollection<BO.TaskInList>(task) : new ObservableCollection<BO.TaskInList>();
            }
            catch (Exception ex)
            {
                // Throwing any exceptions that occur during initialization.
                throw new Exception($"{ex.Message}");
            }

            // Initializing the window components.
            InitializeComponent();
        }

        // Event handler for the "Add/Update" button click.
        private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Retrieving the engineer associated with the current task.
                BO.Engineer? eng = s_bl.Engineer.Read(CurrentTask!.Engineer!.Id)!;
            }
            catch (Exception ex)
            {
                // Displaying any exceptions that occur.
                MessageBox.Show($"{ex.Message}", "Exception", MessageBoxButton.OK);
            }

            // Checking whether the button content is "Add" or "Update".
            if ((sender as Button)!.Content.ToString() == "Add")
            {
                try
                {
                    // Adding the current task to the bl.
                    s_bl.Task.Create(CurrentTask!);
                    // Displaying a success message.
                    MessageBox.Show("The addition was made successfully", "Confirmation", MessageBoxButton.OK);
                    // Closing the window.
                    this.Close();
                }
                catch (Exception ex)
                {
                    // Displaying any exceptions that occur.
                    MessageBox.Show($"{ex.Message}", "Exception", MessageBoxButton.OK);
                }
            }
            else
            {
                try
                {
                    // Updating the current task in the bl.
                    s_bl.Task.Update(CurrentTask!);
                    // Displaying a success message.
                    MessageBox.Show("The update was successful", "Confirmation", MessageBoxButton.OK);
                    // Closing the window.
                    this.Close();
                }
                catch (Exception ex)
                {
                    // Displaying any exceptions that occur.
                    MessageBox.Show($"{ex.Message}", "Confirmation", MessageBoxButton.OK);
                }
            }
        }

        // Event handler for the selection change in the task dependency combo box.
        private void cbCheckedDependTask_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Checking if a task is selected for dependency.
                if (CheckedDependTask != 0)
                {
                    // Prompting the user for confirmation.
                    MessageBoxResult ans = MessageBox.Show("Are you sure you want to add the dependency?", "Confirmation", MessageBoxButton.YesNo);

                    // Proceeding if the user confirms.
                    if (ans == MessageBoxResult.Yes)
                    {
                        // Retrieving the selected dependency task.
                        BO.Task dependency = s_bl.Task.Read(CheckedDependTask)!;
                        // Initializing the task's dependencies list if not already initialized.
                        if (CurrentTask!.Dependencies == null)
                            CurrentTask.Dependencies = new List<TaskInList>();

                        // Adding the selected task to the current task's dependencies.
                        CurrentTask.Dependencies.Add(new BO.TaskInList()
                        {
                            Id = dependency.Id,
                            Alias = dependency.Alias,
                            Description = dependency.Description,
                            Status = dependency.Status
                        });

                        // Refreshing the UI with the updated dependencies list.
                        TaskDependencies = new ObservableCollection<BO.TaskInList>(CurrentTask.Dependencies);
                    }
                    CheckedDependTask = 0;
                }
            }
            catch (Exception ex)
            {
                // Displaying any exceptions that occur.
                MessageBox.Show($"{ex}", "Confirmation", MessageBoxButton.OK);
            }
        }
    }
}