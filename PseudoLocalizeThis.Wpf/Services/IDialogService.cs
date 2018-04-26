namespace PseudoLocalizeThis.Wpf.Services
{
    /// <summary>
    /// Displays different dialogs.
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Displays a file picker dialog to select a resource file.
        /// </summary>
        /// <returns>The selected file path.</returns>
        string ShowResourceFilePicker();

        /// <summary>
        /// Displays a save file dialog for saving the transformed resource file.
        /// </summary>
        /// <returns></returns>
        string ShowSaveTransformedFileDilaog();
    }
}
