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

namespace MethodMinder.WPF.Examples
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DebouncedDispatcher _debouncer;

        public MainWindow()
        {
            InitializeComponent();
            _debouncer = new DebouncedDispatcher(MyAction)
            {
                DebounceInterval = TimeSpan.FromSeconds(1)
            };
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PART_Text.Text = "Waiting...";
            _debouncer.Debounce();
        }

        private void MyAction()
        {
            PART_Text.Text = "Done!";
        }
    }
}
