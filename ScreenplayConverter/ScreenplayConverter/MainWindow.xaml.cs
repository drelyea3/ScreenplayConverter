using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

using Word = Microsoft.Office.Interop.Word;

namespace ScreenplayConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ScriptModel script;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = script = new ScriptModel();

        }

        private void OnOpenExecute(object sender, ExecutedRoutedEventArgs e)
        {
            if (!LoseChanges())
            {
                return;
            }

            var dialog = new OpenFileDialog()
            {
                Title = "Select a script to open",
                Filter = "Word documents (*.docx)|*.docx|Text files (*.txt)|*.txt",
                CheckFileExists = true,
                CheckPathExists = true,
                FilterIndex=2,
            };

            var result = dialog.ShowDialog();
            if (result.HasValue && result.Value)
            {
                var name = dialog.FileName;
                if (name.EndsWith(".txt", StringComparison.InvariantCultureIgnoreCase))
                {
                    script.ImportText(File.ReadAllLines(name));
                }
                else if (name.EndsWith(".docx", StringComparison.InvariantCultureIgnoreCase))
                {
                    script.ImportText(ImportWordDoc(name));
                }
            }
        }

        private void CanOpenExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void OnSaveExecute(object sender, ExecutedRoutedEventArgs e)
        {

        }

        private void CanSaveExecute(object sender, CanExecuteRoutedEventArgs e)
        {

        }

        private void OnCloseExecute(object sender, ExecutedRoutedEventArgs e)
        {
            if (LoseChanges())
            {
                Close();
            }
        }

        private void OnDeleteExecute(object sender, ExecutedRoutedEventArgs e)
        {
            script.Delete((ScriptItem)e.Parameter);
        }

        private bool LoseChanges()
        {
            if (script.HasChanges)
            {
                var result = MessageBox.Show(this, "Do you want to save changes?", "Script Converter", MessageBoxButton.YesNoCancel, MessageBoxImage.Stop, MessageBoxResult.Cancel);
                if (result == MessageBoxResult.No)
                {
                    return true;
                }
            }

            return true;
        }

        private IEnumerable<string> ImportWordDoc(string name)
        {
            var paragraphs = new List<string>();

            Word.Application app = new Word.Application() { WindowState = Word.WdWindowState.wdWindowStateMinimize };

            var doc = app.Documents.Open(name, ReadOnly: true);

            foreach (Word.Paragraph para in doc.Paragraphs)
            {
                string text = para.Range.Text.Trim();
                if (!string.IsNullOrWhiteSpace(text))
                {
                    paragraphs.Add(text);
                }
            }

            app.Quit(SaveChanges: false);

            return paragraphs;
        }
    }
}
