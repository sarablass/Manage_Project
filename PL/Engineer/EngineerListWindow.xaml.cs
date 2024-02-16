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

namespace PL.Engineer
{
    /// <summary>
    /// Interaction logic for EngineerListWindow.xaml
    /// </summary>
    public partial class EngineerListWindow : Window
    {
        static readonly BlApi.IBl s_bl = BlApi.Factory.Get();

        // Property to store the engineer's level of experience
        public BO.EngineerExperience Level { get; set; } = BO.EngineerExperience.None;

        // Property to bind the list of engineers to UI elements
        public IEnumerable<BO.Engineer> EngineerList
        {
            get { return (IEnumerable<BO.Engineer>)GetValue(EngineerListProperty); }
            set { SetValue(EngineerListProperty, value); }
        }
        public static readonly DependencyProperty EngineerListProperty =
         DependencyProperty.Register("EngineerList", typeof(IEnumerable<BO.Engineer>),
        typeof(EngineerListWindow), new PropertyMetadata(null));

        // Constructor for EngineerListWindow
        public EngineerListWindow()
        {
            InitializeComponent();  
            EngineerList = s_bl?.Engineer.ReadAll()!; // Populate the engineer list from the business logic layer
        }

        // Event handler for level selector change
        private void cbLevelSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Update the engineer list based on the selected level
            EngineerList = ((Level == BO.EngineerExperience.None) ?
                s_bl?.Engineer.ReadAll()! : s_bl?.Engineer.ReadAll(item => item.Level == Level))!;
        }

        // Event handler for Add button click
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Open the EngineerWindow to add a new engineer
            new EngineerWindow().ShowDialog();
        }

        // Event handler for double-clicking an item in the engineer list to updating
        private void lvUpdate_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                // Retrieve the selected engineer and open EngineerWindow for updating
                BO.Engineer? engineer = (sender as ListView)?.SelectedItem as BO.Engineer;
                new EngineerWindow(engineer!.Id).ShowDialog();                      
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}", "Confirmation", MessageBoxButton.OK);
            }
        }

        // Event handler for window activity (refreshing engineer list)
        private void Window_activity(object sender, EventArgs e)
        {
            EngineerList = s_bl.Engineer.ReadAll()!;
        }
    }
}
