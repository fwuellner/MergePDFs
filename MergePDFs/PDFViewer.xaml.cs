using System.Windows;

namespace MergePDFs
{
    /// <summary>
    /// Interaktionslogik für PDFViewer.xaml
    /// </summary>
    public partial class PDFViewer : Window
    {
        public PDFViewer()
        {
            InitializeComponent();
        }

        internal void SetPDFPath(string _filepath)
        {
            webBrowser1.Navigate(_filepath);
        }
    }
}
