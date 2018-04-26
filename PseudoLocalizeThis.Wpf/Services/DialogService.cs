using System.Windows;
using Microsoft.Win32;

namespace PseudoLocalizeThis.Wpf.Services
{
    /// <inheritdoc/>
    public class DialogService : IDialogService
    {
        private Window _owner;

        /// <summary>
        /// Initializes a new instance of the <see cref="DialogService"/> class.
        /// </summary>
        /// <param name="owner">The window owner of the dialogs.</param>
        public DialogService(Window owner)
        {
            _owner = owner;
        }

        /// <inheritdoc/>
        public string ShowResourceFilePicker()
        {
            var dialog = new OpenFileDialog
            {
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = ".resx",
                Filter = "Resource(.resx)| *.resx"
            };

            var result = dialog.ShowDialog(_owner);
            if (!result.HasValue || !result.Value)
            {
                return null;
            }

            return dialog.FileName;
        }

        /// <inheritdoc/>
        public string ShowSaveTransformedFileDilaog()
        {
            var dialog = new SaveFileDialog
            {
                DefaultExt = ".resx",
                Filter = "Resource(.resx)| *.resx",
                Title = "Save Transformed Resource File"
            };

            var result = dialog.ShowDialog(_owner);
            if (!result.HasValue || !result.Value)
            {
                return null;
            }

            return dialog.FileName;
        }
    }
}