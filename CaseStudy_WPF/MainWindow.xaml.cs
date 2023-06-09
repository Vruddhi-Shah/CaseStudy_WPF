﻿using CaseStudy_WPF.Configuration;
using CaseStudy_WPF.Model;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CaseStudy_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<EmployeeDetails> employeeDetails = new List<EmployeeDetails>();
        long totalPages = 0;
        long currentPage = 1;
        long totalRecords = 0;
        long pageLimit = 10;

        public MainWindow()
        {
            InitializeComponent();
            BindEmployeeGrid(1);
        }

        private void BindEmployeeGrid(long pageNo)
        {
            try
            {
                ClearAll();
                APIResponseModel emp = EmployeeService.GetAllEmployee(pageNo);
                totalRecords = emp.Meta.pagination.Total;
                totalPages = emp.Meta.pagination.Pages;
                currentPage = emp.Meta.pagination.Page;
                employeeDetails = emp.data;
                EmployeeGrid.ItemsSource = emp.data;
                EmployeeGridPagination();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Due to some issue not able to fetch employee details");
            }
        }

        private void EmployeeGridPagination()
        {
            var records = currentPage == totalPages ? totalRecords : currentPage * pageLimit;
            //lblpageInformation.Content = records + " of " + totalRecords;
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EmployeeService.DeleteEmployee(Convert.ToInt32(id_txt.Text));
                BindEmployeeGrid(1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Due to some issue not able to delete the employee detail");
            }
        }

        public void ClearAll()
        {
            id_txt.Clear();
            name_txt.Clear();
            email_txt.Clear();
            male_radio.IsChecked = false;
            female_radio.IsChecked = false;
            status_check.IsChecked = false;           
        }
        private void clear_btn_Click(object sender, RoutedEventArgs e)
        {
            ClearAll();
        }

        private void addNewbtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EmployeeDetails empDetail = new EmployeeDetails();
                empDetail.Name = name_txt.Text;
                empDetail.Email = email_txt.Text;
                empDetail.Gender = male_radio.IsChecked == true ? Common.male : Common.female;
                empDetail.Status = status_check.IsChecked == true ? Common.active : Common.inactive;
                var response = EmployeeService.AddEmployee(empDetail);
                if (response.IsSuccessStatusCode)
                {
                    BindEmployeeGrid(1);
                    response.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Due to some issue not able to add employee detail.");
            }
        }

        private void update_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                EmployeeDetails empDetail = new EmployeeDetails();
                empDetail.Id = Convert.ToInt64(id_txt.Text);
                empDetail.Name = name_txt.Text;
                empDetail.Email = email_txt.Text;
                empDetail.Gender = male_radio.IsChecked == true ? Common.male : Common.female;
                empDetail.Status = status_check.IsChecked == true ? Common.active : Common.inactive;
                EmployeeService.UpdateEmployeeAsync(empDetail);
                BindEmployeeGrid(1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Due to some issue not able to update employee detail");
            }
        }

        private void search_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(id_txt.Text))
                {
                    var employeeDetail = EmployeeService.GetEmployeeDetails(Convert.ToInt32(id_txt.Text));
                    if (!string.IsNullOrWhiteSpace(employeeDetail.Email))
                    {
                        FillEmployeeData(employeeDetail);
                    }

                }
                else
                {
                    MessageBox.Show("Id is required.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Due to some issue not able to find the employee detail");
            }
        }

        private void FillEmployeeData(EmployeeDetails employeeDetail)
        {
            email_txt.Text = employeeDetail.Email;
            name_txt.Text = employeeDetail.Name;
            male_radio.IsChecked = employeeDetail.Gender == Common.male;
            female_radio.IsChecked = employeeDetail.Gender == Common.female;
            status_check.IsChecked = employeeDetail.Status == Common.active;

            delete_btn.IsEnabled = true;
            update_btn.IsEnabled = true;
            addNewbtn.IsEnabled = false;
        }
    }
}
