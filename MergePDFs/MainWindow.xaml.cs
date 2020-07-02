using System.Windows;

namespace MergePDFs
{
    public partial class MainWindow : Window
    {
        private ViewModel _viewModel = new ViewModel();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = _viewModel;
        }

        private void OnCloseClick(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Do you really want to quit this program?", 
                                                      "Question", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                e.Cancel = false;
                //App.Current.Shutdown();
            }
            else if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
