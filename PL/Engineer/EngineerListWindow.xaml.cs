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
        public BO.EngineerExperience Level { get; set; } = BO.EngineerExperience.None;
        public IEnumerable<BO.Engineer> EngineerList
        {
            get { return (IEnumerable<BO.Engineer>)GetValue(EngineerListProperty); }
            set { SetValue(EngineerListProperty, value); }
        }
        public static readonly DependencyProperty EngineerListProperty =
         DependencyProperty.Register("EngineerList", typeof(IEnumerable<BO.Engineer>),
        typeof(EngineerListWindow), new PropertyMetadata(null));


        public EngineerListWindow()
        {
            InitializeComponent();  
            EngineerList = s_bl?.Engineer.ReadAll()!;
        }

        private void cbLevelSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EngineerList = ((Level == BO.EngineerExperience.None) ?
                s_bl?.Engineer.ReadAll()! : s_bl?.Engineer.ReadAll(item => item.Level == Level))!;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            new EngineerWindow().ShowDialog();
        }

        private void lvUpdate_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                BO.Engineer? engineer = (sender as ListView)?.SelectedItem as BO.Engineer;
                new EngineerWindow(engineer!.Id).ShowDialog();                      
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}", "Confirmation", MessageBoxButton.OK);
            }
        }
        private void Window_activity(object sender, EventArgs e)
        {
            EngineerList = s_bl.Engineer.ReadAll()!;
        }
    }
}
