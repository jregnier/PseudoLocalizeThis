using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace PseudoLocalizeThis.Wpf.ViewModel
{
    /// <inheritdoc />
    public class SelectResourceFileViewModel : ViewModelBase, IFileSelect
    {
        private readonly Func<string> _dialogAction;
        private string _filePath;
        private RelayCommand _selectectFileCommand;

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectResourceFileViewModel"/> class.
        /// </summary>
        /// <param name="dialogAction"></param>
        public SelectResourceFileViewModel(
            Func<string> dialogAction)
        {
            _dialogAction = dialogAction;
        }

        /// <summary>
        /// Factory method for creating a new <see cref="SelectResourceFileViewModel"/> class.
        /// </summary>
        /// <param name="dialogAction">A function which displays a dialog.</param>
        /// <returns>A new <see cref="SelectResourceFileViewModel"/> class.</returns>
        public delegate SelectResourceFileViewModel Factory(Func<string> dialogAction);

        /// <inheritdoc />
        public string FilePath
        {
            get { return _filePath; }
            set { Set(() => FilePath, ref _filePath, value); }
        }

        /// <summary>
        /// Gets a command used to display a select file dialog.
        /// </summary>
        public RelayCommand SelectectFileCommand =>
            _selectectFileCommand ?? (_selectectFileCommand = new RelayCommand(ShowSelectFileDialog));

        /// <summary>
        /// Shows the dialog used to select a file and returns the selected file path.
        /// </summary>
        private void ShowSelectFileDialog()
        {
            FilePath = _dialogAction();
        }
    }
}