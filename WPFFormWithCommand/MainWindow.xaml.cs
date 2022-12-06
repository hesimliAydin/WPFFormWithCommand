using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
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
using WPFFormWithCommand.Command;
using WPFFormWithCommand.FakeRepo;
using WPFFormWithCommand.Model;

namespace WPFFormWithCommand
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window,INotifyPropertyChanged
    {
        public RelayCommand FirstNameEnterCommand { get; set; }
        public RelayCommand LastNameEnterCommand { get; set; }
        public RelayCommand PhoneEnterCommand { get; set; }
        public RelayCommand EmailEnterCommand { get; set; }
        public RelayCommand SubmitButtonCommand { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null!)
        {
            PropertyChangedEventHandler handler = PropertyChanged!;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private List<Person> person;

        public List<Person> Person
        {
            get { return person; }
            set { person = value; OnPropertyChanged(); }
        }

        Regex PhoneNumberRegex= new("^\\+?\\d{1,4}?[-.\\s]?\\(?\\d{1,3}?\\)?[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,4}[-.\\s]?\\d{1,9}$");
        Regex EmailRegex = new("^[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?$");



        public MainWindow()
        {
            InitializeComponent();


            Person = new List<Person>();

            Person=FakeRepository.GetPerson();

            DataContext = this;


            FirstNameEnterCommand = new RelayCommand((o) =>
            {
                var mystackPanel = o as StackPanel;
                var firstNameTextBox = mystackPanel!.Children[1] as TextBox;
                var button = (mystackPanel.Children[8] as Border)!.Child as Button;

                if (string.IsNullOrEmpty(firstNameTextBox!.Text) || firstNameTextBox.Text.Length < 4)
                {
                    firstNameTextBox!.BorderBrush = new SolidColorBrush(Colors.Red);
                    firstNameTextBox.BorderThickness = new Thickness(0, 0, 0, 5);
                    firstNameTextBox.Focus();
                    button!.IsEnabled = false;

                }
                else
                {
                    firstNameTextBox!.BorderBrush = new SolidColorBrush(Colors.SpringGreen);
                    firstNameTextBox.BorderThickness = new Thickness(0, 0, 0, 5);

                    var textBox2 = mystackPanel.Children[3] as TextBox;
                    textBox2!.Focus();
                    button!.IsEnabled = true;
                }

            });

            LastNameEnterCommand = new RelayCommand((o) =>
            {
                var mystackPanel = o as StackPanel;
                var lastNameTextBox = mystackPanel!.Children[3] as TextBox;
                var button = (mystackPanel.Children[8] as Border)!.Child as Button;

                if (string.IsNullOrEmpty(lastNameTextBox!.Text) || lastNameTextBox.Text.Length<4)
                {
                    lastNameTextBox!.BorderBrush = new SolidColorBrush(Colors.Red);
                    lastNameTextBox.BorderThickness = new Thickness(0, 0, 0, 5);
                    lastNameTextBox.Focus();
                    button!.IsEnabled = false;

                }
                else
                {
                    lastNameTextBox!.BorderBrush = new SolidColorBrush(Colors.SpringGreen);
                    lastNameTextBox.BorderThickness = new Thickness(0, 0, 0, 5);

                    var PhonetextBox = mystackPanel.Children[3] as TextBox;
                    PhonetextBox!.Focus();
                    button!.IsEnabled = true;
                }

            });

            PhoneEnterCommand = new RelayCommand((o) =>
            {
                var stackPanel = o as StackPanel;
                var phoneTextBox = stackPanel!.Children[5] as TextBox;
                var button = (stackPanel!.Children[8] as Border)!.Child as Button;
                if (PhoneNumberRegex.IsMatch(phoneTextBox!.Text) &&
                (phoneTextBox.Text.StartsWith("+994 51") || phoneTextBox.Text.StartsWith("+994 50") || phoneTextBox.Text.StartsWith("+994 55")
                || phoneTextBox.Text.StartsWith("+994 70") || phoneTextBox.Text.StartsWith("+994 77")))
                {
                    phoneTextBox.BorderBrush = new SolidColorBrush(Colors.SpringGreen);
                    phoneTextBox.BorderThickness = new Thickness(0, 0, 0, 5);

                    var emailTextbox = stackPanel.Children[7] as TextBox;
                    emailTextbox!.Focus();
                    button!.IsEnabled = true;
                }
                else
                {
                    phoneTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
                    phoneTextBox.BorderThickness = new Thickness(0, 0, 0, 5);
                    phoneTextBox.Focus();
                    button!.IsEnabled = false;
                }
            });

            EmailEnterCommand = new RelayCommand((o) =>
            {
                var stackPanel = o as StackPanel;
                var EmailTextBox = stackPanel!.Children[7] as TextBox;
                var border = (stackPanel!.Children[8] as Border);
                var button = border!.Child as Button;

                if (EmailRegex.IsMatch(EmailTextBox!.Text)
                && (EmailTextBox.Text.EndsWith("@gmail.com") || EmailTextBox.Text.EndsWith("@mail.ru")))
                {
                    EmailTextBox.BorderBrush = new SolidColorBrush(Colors.SpringGreen);
                    EmailTextBox.BorderThickness = new Thickness(0, 0, 0, 5);

                    button!.Foreground = new SolidColorBrush(Colors.SpringGreen);
                    button!.Focus();
                    button!.IsEnabled = true;
                }
                else
                {
                    EmailTextBox.BorderBrush = new SolidColorBrush(Colors.Red);
                    EmailTextBox.BorderThickness = new Thickness(0, 0, 0, 5);

                    button!.Foreground = new SolidColorBrush(Colors.Red);
                    EmailTextBox.Focus();
                    button!.IsEnabled = false;
                }
            });


            SubmitButtonCommand = new RelayCommand((o) =>
            {
                var stackPanel = o as StackPanel;
                var firstName = stackPanel!.Children[1] as TextBox;
                var lastName = stackPanel!.Children[3] as TextBox;
                var Phone = stackPanel!.Children[5] as TextBox;
                var Email = stackPanel!.Children[7] as TextBox;
                var button = (stackPanel!.Children[8] as Border)!.Child as Button;

                button!.IsEnabled = true;

                foreach (var human in Person)
                {
                    if (human.FirstName == firstName!.Text && lastName!.Text == human.LastName &&
                    Phone!.Text == human.PhoneNumber && Email!.Text == human.EmailAddress)
                    {
                        MessageBox.Show($"Welcome {firstName.Text} {lastName.Text}", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    else if (firstName!.Text == string.Empty)
                    {
                        MessageBox.Show($"Enter FirstName ", "Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }
                    else if (lastName!.Text == string.Empty)
                    {
                        MessageBox.Show($"Enter LastName ", "Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }
                    else if (Phone!.Text == string.Empty)
                    {
                        MessageBox.Show($"Enter Phone Number ", "Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }
                    else if (Email!.Text == string.Empty)
                    {
                        MessageBox.Show($"Enter Email address ", "Information", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }
                }
                MessageBox.Show("No such person was found!!! ", "Information", MessageBoxButton.OK, MessageBoxImage.Error);


            });







        }

        
    }
}
