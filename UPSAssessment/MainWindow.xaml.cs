using System.Windows;
using UPSAssessment.Business.DTOs;
using UPSAssessment.Business.Services;

namespace UPSAssessment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IEmployeeService _employeeService;

        public MainWindow(IEmployeeService employeeService)
        {
            InitializeComponent();
            _employeeService = employeeService;

            dataGridEmployee.ItemsSource = _employeeService.GetAll();
        }

        private void buttonSearch_Click(object sender, RoutedEventArgs e)
        {
            RefreshData();
        }

        private void buttonAdd_Click(object sender, RoutedEventArgs e)
        {
            EmployeeDto employee = new EmployeeDto()
            {
                name = textBoxName.Text,
                email = textBoxEmail.Text,
                gender = textBoxGender.Text,
                status = "active"
            };

            _employeeService.Add(employee);
            RefreshData();
        }

        private void RefreshData()
        {
            dataGridEmployee.ItemsSource = null;
            dataGridEmployee.ItemsSource = _employeeService.GetAll(textBoxSearchName.Text);
        }
    }
}
