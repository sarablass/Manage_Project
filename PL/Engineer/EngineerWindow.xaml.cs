﻿using System;
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
        public BO.EngineerExperience Level { get; set; } = BO.EngineerExperience.None;

        public BO.Engineer CurrentEngineer
        {
            get { return (BO.Engineer)GetValue(CurrentEngineerProperty); }
            set { SetValue(CurrentEngineerProperty, value); }
        }

        public static readonly DependencyProperty CurrentEngineerProperty =
         DependencyProperty.Register("CurrentEngineer", typeof(BO.Engineer),
        typeof(EngineerWindow), new PropertyMetadata(null));


        public EngineerWindow(int id = 0)
        {
            InitializeComponent();

            if (id != 0)
            {
                try
                {
                    CurrentEngineer = s_bl!.Engineer.Read(id)!;
                }
                catch (Exception ex)
                {
                    throw new Exception($"{ex}");
                }
            }
            else
            {
                CurrentEngineer = new BO.Engineer();
            }

        }

        private void btnAddUpdate_Click(object sender, RoutedEventArgs e)
        {

            if ((sender as Button)!.Content.ToString() == "Add")
            {
                try
                {
                    s_bl.Engineer.Create(CurrentEngineer);
                    MessageBox.Show("The addition was made successfully", "Confirmation", MessageBoxButton.OK);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex}", "Confirmation", MessageBoxButton.OK);
                }
            }
            else
            {
                try
                {
                    s_bl.Engineer.Update(CurrentEngineer);
                    MessageBox.Show("The update was successful", "Confirmation", MessageBoxButton.OK);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex}", "Confirmation", MessageBoxButton.OK);

                }
            }
        }
    }
}
