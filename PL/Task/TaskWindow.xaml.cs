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

namespace PL.Task;

/// <summary>
/// Interaction logic for TaskWindow.xaml
/// </summary>
public partial class TaskWindow : Window
{
    static readonly BlApi.IBl s_bl = BlApi.Factory.Get();
    public BO.Status Status { get; set; } = BO.Status.None;

    public BO.Task? CurrentTask
    {
        get { return (BO.Task)GetValue(CurrentTaskProperty); }
        set { SetValue(CurrentTaskProperty, value); }
    }
    public static readonly DependencyProperty CurrentTaskProperty =
     DependencyProperty.Register("CurrentTask", typeof(BO.Task),
    typeof(TaskWindow), new PropertyMetadata(null));

    public IEnumerable<BO.TaskInList> TaskList
    {
        get { return (IEnumerable<BO.TaskInList>)GetValue(TaskListProperty); }
        set { SetValue(TaskListProperty, value); }
    }
    public static readonly DependencyProperty TaskListProperty =
     DependencyProperty.Register("TaskList", typeof(IEnumerable<BO.TaskInList>),
    typeof(TaskWindow), new PropertyMetadata(null));

    public IEnumerable<BO.TaskInList> TaskDependencies
    {
        get { return (IEnumerable<BO.TaskInList>)GetValue(TaskDependenciesProperty); }
        set { SetValue(TaskDependenciesProperty, value); }
    }

    public static readonly DependencyProperty TaskDependenciesProperty =
        DependencyProperty.Register("TaskDependencies", typeof(IEnumerable<BO.TaskInList>), typeof(TaskWindow), new PropertyMetadata(null));

    public int DepTask { get; set; } = 0;

    public TaskWindow(int id = 0)
    {

       try
       {
            if (id != 0)
            {
                CurrentTask = s_bl!.Task.Read(id)!;      
            }
        else
        {
            CurrentTask = new BO.Task()
            {
                Id = 0,
                Description = "",
                Alias = "",
                Milestone = null,
                Status = null,               
                CreateAt = DateTime.Now,
                BaselineStartDate = null,
                StartDate = null,
                ForecastDate=null,
                DeadlineDate=null,
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
                IsActive=false
            };
        }
            TaskDependencies = CurrentTask.Dependencies != null ? new ObservableCollection<BO.TaskInList>(CurrentTask.Dependencies) : new ObservableCollection<BO.TaskInList>();
            var temp = s_bl?.Task.ReadAll().Select(x => new BO.TaskInList()
            {
                Id = x.Id,
                Alias = x.Alias,
                Description = x.Description,
                Status = x.Status
            }).ToList();

            TaskList = temp != null ? new ObservableCollection<BO.TaskInList>(temp) : new ObservableCollection<BO.TaskInList>();

       }
       catch (Exception ex)
       {
            throw new Exception($"{ex.Message}");
       }



        InitializeComponent();
    }

    private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
    {

        if ((sender as Button)!.Content.ToString() == "Add")
        {
            try
            {
                s_bl.Task.Create(CurrentTask!);
                MessageBox.Show("The addition was made successfully", "Confirmation", MessageBoxButton.OK);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Confirmation", MessageBoxButton.OK);
            }
        }
        else
        {
            try
            {
                s_bl.Task.Update(CurrentTask!);
                MessageBox.Show("The update was successful", "Confirmation", MessageBoxButton.OK);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Confirmation", MessageBoxButton.OK);

            }
        }
    }

    private void ComboBoxDepTasks_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        try
        {
            if (DepTask != 0)
            {
                MessageBoxResult result = MessageBox.Show("Do you want to add the selected item?", "Confirmation", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    BO.Task dep = s_bl.Task.Read(DepTask)!;
                    if (CurrentTask!.Dependencies == null)
                        CurrentTask.Dependencies = new List<TaskInList>();

                    CurrentTask.Dependencies.Add(new BO.TaskInList()
                    {
                        Id = dep.Id,
                        Alias = dep.Alias,
                        Description = dep.Description,
                        Status = dep.Status
                    });


                    TaskDependencies = new ObservableCollection<BO.TaskInList>(CurrentTask.Dependencies);
                }
                DepTask = 0;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"{ex}", "Confirmation", MessageBoxButton.OK);
        }
    }

   
   
}
