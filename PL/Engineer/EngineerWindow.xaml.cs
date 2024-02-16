using BO;
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

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerWindow.xaml
    /// </summary>
    public partial class EngineerWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        // Property to store the engineer's level of experience
        public BO.EngineerExperience Level { get; set; } = BO.EngineerExperience.None;

        // Dependency property to bind the current engineer
        public BO.Engineer CurrentEngineer
        {
            get { return (BO.Engineer)GetValue(CurrentEngineerProperty); }
            set { SetValue(CurrentEngineerProperty, value); }
        }

        public static readonly DependencyProperty CurrentEngineerProperty =
         DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer),
        typeof(EngineerWindow), new PropertyMetadata(null));

        // Constructor for EngineerWindow
        public EngineerWindow(int id = 0)
        {
            try
            {
                if (id != 0)
                {
                    // If ID is provided, retrieve the engineer with the given ID from the business logic layer
                    CurrentEngineer = s_bl!.Engineer.Read(id)!;
                }
                else
                {
                    // If ID is not provided, create a new engineer object with default values
                    CurrentEngineer = new BO.Engineer()
                    {
                        Id = 0,
                        Name="",
                        IsActive = false,
                        Email="",
                        Level=null,
                        Cost=0,
                        Task = new TaskInEngineer()
                        {
                           Id = 0,
                           Alias = ""
                        }
                    };
                }
            }        
             catch (Exception ex)
            {   
                throw new Exception($"{ex.Message}");
            }
            InitializeComponent();
        }

        // Event handler for Add/Update button click
        private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
        {
            // Determine whether to add or update the engineer based on the button content
            if ((sender as Button)!.Content.ToString() == "Add")
            {
                try
                {
                    // Call the business logic layer to create the engineer
                    s_bl.Engineer.Create(CurrentEngineer);
                    MessageBox.Show("The addition was made successfully", "Confirmation", MessageBoxButton.OK);
                    this.Close(); // Close the window after successful addition
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
                    // Call the business logic layer to update the engineer
                    s_bl.Engineer.Update(CurrentEngineer);
                    MessageBox.Show("The update was successful", "Confirmation", MessageBoxButton.OK);
                    this.Close(); // Close the window after successful update
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}", "Exception", MessageBoxButton.OK);

                }
            }
        }
    }
}
