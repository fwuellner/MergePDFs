using Microsoft.Win32;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace MergePDFs
{
    public class ViewModel : INotifyPropertyChanged
    {
        // Interface for Data Binding

        public event PropertyChangedEventHandler PropertyChanged;
        protected internal void OnPropertyChanged(string propertyname)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyname));
        }

        /*

         Private Members

        */

        private PDFData _pdfData;
        private string _filename;

        private ObservableCollection<PDFData> _allPDFdata;

        /*

        Public Constructor

        */

        public ViewModel()
        {
            _pdfData = new PDFData();

            AddCommand = new PDFCommand(OnAddExecuted, OnAddCanExecute);
            MergeCommand = new PDFCommand(OnMergeExecuted, OnMergeCanExecute);
            MoveUpCommand = new PDFCommand(OnMoveUpExecuted, OnMoveUpCanExecute);
            MoveDownCommand = new PDFCommand(OnMoveDownExecuted, OnMoveDownCanExecute);
            LeftDoubleClickCommand = new PDFCommand(OnLeftDoubleClickExecuted, OnLeftDoubleClickCanExecute);
            DeleteCommand = new PDFCommand(OnDeleteExecuted, OnDeleteCanExecute);
            ResetCommand = new PDFCommand(OnResetExecuted, OnResetCanExecute);
        }

        /*

        Public Members

        */

        public string Filename
        {
            get
            {
                return _filename;
            }

            set
            {
                if (_filename == value)
                {
                    return;
                }

                _filename = value;
                OnPropertyChanged("Filename");
            }
        }

        public ObservableCollection<PDFData> AllPDFData
        {
            get { return _allPDFdata; }

            set
            {
                if (_allPDFdata == value)
                {
                    return;
                }

                _allPDFdata = value;
                OnPropertyChanged("AllPDFData");
            }
        }

        /*
         
        Public Commands

        */

        public ICommand AddCommand { get; }

        public void OnAddExecuted(object parameter)
        {
            if (AllPDFData == null)
            {
                AllPDFData = new ObservableCollection<PDFData>();
            }

            try
            {
                var dialog1 = new OpenFileDialog()
                {
                    Multiselect = true,
                    InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                    Filter = "PDF (*.pdf;*.PDF)|*.pdf;*.PDF",
                    Title = "Select single or multiple files for PDF merging"
                };

                var resultOfDialog = dialog1.ShowDialog();

                if (resultOfDialog == true)
                {
                    if (dialog1.FileNames.Length > 0)
                    {
                        foreach (string file in dialog1.FileNames)
                        {
                            PDFData data = new PDFData() { Filename = file };
                            AllPDFData.Add(data);
                        }
                    }
                }
                else
                {
                    if (AllPDFData.Count == 0)
                    {
                        AllPDFData = null;
                    }

                    MessageBox.Show("No files selected.", 
                                    "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error message:\n\n" + ex.Message, 
                                "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public bool OnAddCanExecute(object parameter)
        {
            return true;
        }

        public ICommand MergeCommand { get; }
        public void OnMergeExecuted(object parameter)
        {
            try
            {
                using (PdfDocument targetDoc = new PdfDocument())
                {
                    foreach (PDFData data in AllPDFData)
                    {
                        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                        using (PdfDocument pdfDoc = PdfReader.Open(data.Filename, PdfDocumentOpenMode.Import))
                        {
                            for (int i = 0; i < pdfDoc.PageCount; i++)
                            {
                                targetDoc.AddPage(pdfDoc.Pages[i]);
                            }
                        }
                    }

                    var sfd = new SaveFileDialog()
                    {
                        InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                        Filter = "PDF (*.pdf;*.PDF)|*.pdf;*.PDF",
                        Title = "Select save path and enter filename."
                    };

                    var result = sfd.ShowDialog();

                    if (result == true)
                    {
                        targetDoc.Save(sfd.FileName);

                        MessageBox.Show("PDF file saved to:\n\n" + sfd.FileName, 
                                        "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("Process aborted.", 
                                        "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error message:\n\n" + ex.Message,
                                "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        public bool OnMergeCanExecute(object parameter)
        {
            return (AllPDFData != null && AllPDFData.Count > 1);
        }

        public ICommand MoveUpCommand { get; }

        public void OnMoveUpExecuted(object parameter)
        {
            if (parameter != null)
            {
                int selectedIndex = (int)parameter;

                if (selectedIndex > 0)
                {
                    var observable = _allPDFdata;

                    observable.Move(selectedIndex, selectedIndex - 1);

                    _allPDFdata = observable;
                }
            }
        }

        public bool OnMoveUpCanExecute(object parameter)
        {
            int selectedIndex = (int)parameter;

            return (AllPDFData != null && selectedIndex > 0);
        }

        public ICommand MoveDownCommand { get; }

        public void OnMoveDownExecuted(object parameter)
        {
            if (parameter != null)
            {
                int selectedIndex = (int)parameter;

                if (selectedIndex < AllPDFData.Count -1)
                {
                    var observable = _allPDFdata;

                    observable.Move(selectedIndex, selectedIndex + 1);

                    _allPDFdata = observable;
                }
            }
        }

        public bool OnMoveDownCanExecute(object parameter)
        {
            int selectedIndex = (int)parameter;

            return AllPDFData != null && selectedIndex < AllPDFData.Count -1;
        }

        public ICommand LeftDoubleClickCommand { get; }

        public void OnLeftDoubleClickExecuted(object parameter)
        {
            if (parameter != null)
            {
                PDFData data = (PDFData)parameter;

                try
                {
                    PDFViewer pdfViewer = new PDFViewer();
                    pdfViewer.SetPDFPath(data.Filename);
                    pdfViewer.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error message:\n\n" + ex.Message, 
                                    "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        public bool OnLeftDoubleClickCanExecute(object parameter)
        {
            return AllPDFData != null && AllPDFData.Count > 0;
        }

        public ICommand DeleteCommand { get; }

        public void OnDeleteExecuted(object parameter)
        {
            if (parameter != null)
            {
                PDFData data = (PDFData)parameter;
                AllPDFData.Remove(data);
            }
        }

        public bool OnDeleteCanExecute(object parameter)
        {
            return AllPDFData != null && AllPDFData.Count > 0;
        }

        public ICommand ResetCommand { get; }

        public void OnResetExecuted(object parameter)
        {
            ResetPDFData();
        }

        public bool OnResetCanExecute(object parameter)
        {
            return AllPDFData != null;
        }

        /*

        Private Methods

        */

        private void ResetPDFData()
        {
            Filename = null;
            AllPDFData = null;
        }
    }
}
