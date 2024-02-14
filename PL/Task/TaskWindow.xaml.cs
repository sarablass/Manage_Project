using BO;
using PL.Engineer;
using System;
using System.Collections.Generic;
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

    public TaskWindow(int id = 0)
    {
        InitializeComponent();

        if (id != 0)
        {
            try
            {
                CurrentTask = s_bl!.Task.Read(id)!;
            }
            catch (Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }
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

    private void addDependency_Click(object sender, RoutedEventArgs e)
    {

    }
}
