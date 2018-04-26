using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MaterialDesignThemes.Wpf;
using PseudoLocalizeThis.Wpf.Services;
using TransformLib.Services;
using TransformLib.Transforms;

namespace PseudoLocalizeThis.Wpf.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IDialogService _dialogService;
        private readonly IResourceFileService _resourceFileService;
        private readonly SelectResourceFileViewModel.Factory _selectResourceFileViewModelFactory;

        private RelayCommand _addResourceFileCommand;
        private RelayCommand _applyTransformCommand;
        private ObservableCollection<DataElement> _elementsCollection;
        private RelayCommand _saveTransformCommand;
        private string _selectedResourceFile;
        private ObservableCollection<DataElement> _transformedElementsCollection;
        private TransformSettings _transformSettings;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="resourceFileService">
        /// The resource file service used to interact with resource files.
        /// </param>
        /// <param name="selectResourceFileViewModelFactory">
        /// Factory for creating a new view model for selecting a file.
        /// </param>
        /// <param name="dialogService">The dialog service for dislaying dialogs.</param>
        /// <param name="messageQueue">The snackbar message queue.</param>
        public MainViewModel(
            IResourceFileService resourceFileService,
            SelectResourceFileViewModel.Factory selectResourceFileViewModelFactory,
            IDialogService dialogService,
            ISnackbarMessageQueue messageQueue)
        {
            _resourceFileService = resourceFileService;
            _selectResourceFileViewModelFactory = selectResourceFileViewModelFactory;
            _dialogService = dialogService;
            MessageQueue = messageQueue;

            _transformSettings = new TransformSettings();
        }

        /// <summary>
        /// Gets a command which asks the user to select a resource file.
        /// </summary>
        public RelayCommand AddResourceFileCommand =>
            _addResourceFileCommand ?? (_addResourceFileCommand = new RelayCommand(AddResourceFile));

        /// <summary>
        /// Gets a command for applying the given transform to the selected file.
        /// </summary>
        public RelayCommand ApplyTransformCommand =>
                    _applyTransformCommand ?? (_applyTransformCommand = new RelayCommand(ApplyTransform));

        /// <summary>
        /// Gets or sets the collection of elements from the resource file.
        /// </summary>
        public ObservableCollection<DataElement> ElementsCollection
        {
            get { return _elementsCollection; }
            set { Set(() => ElementsCollection, ref _elementsCollection, value); }
        }

        /// <summary>
        /// The snack bar message queue.
        /// </summary>
        public ISnackbarMessageQueue MessageQueue { get; }

        /// <summary>
        /// Gets the command used to save the transformed resource file.
        /// </summary>
        public RelayCommand SaveTransformCommand =>
            _saveTransformCommand ?? (_saveTransformCommand = new RelayCommand(SaveTransform, () => _transformedElementsCollection != null && _transformedElementsCollection.Any()));

        /// <summary>
        /// Gets or sets the selected resource file
        /// </summary>
        public string SelectedResourceFile
        {
            get { return _selectedResourceFile; }
            private set { Set(() => SelectedResourceFile, ref _selectedResourceFile, value); }
        }

        /// <summary>
        /// Gets or sets the collection of the transformed elements after the transform has been applied.
        /// </summary>
        public ObservableCollection<DataElement> TransformedElementsCollection
        {
            get { return _transformedElementsCollection; }
            set { Set(() => TransformedElementsCollection, ref _transformedElementsCollection, value); }
        }

        /// <summary>
        /// Gets or sets the transform settings to apply.
        /// </summary>
        public TransformSettings TransformSettings
        {
            get { return _transformSettings; }
            set { Set(() => _transformSettings, ref _transformSettings, value); }
        }

        /// <summary>
        /// Shows a dialog to select a resource file.
        /// </summary>
        private async void AddResourceFile()
        {
            var selectFileVm = _selectResourceFileViewModelFactory(_dialogService.ShowResourceFilePicker);
            var path = (await DialogHost.Show(selectFileVm)) as string;

            if (path == null)
            {
                return;
            }

            try
            {
                _resourceFileService.Read(path);

                SelectedResourceFile = path;

                ElementsCollection = new ObservableCollection<DataElement>(_resourceFileService.GetOriginalElements());

                MessageQueue.Enqueue($"'{SelectedResourceFile}' successfully added!");
            }
            catch (FileNotFoundException)
            {
                MessageQueue.Enqueue(
                    $"'{SelectedResourceFile}' does not exists. Please select an existing .resx file.");
            }
        }

        /// <summary>
        /// Applies the transform to the given resource file.
        /// </summary>
        private void ApplyTransform()
        {
            try
            {
                TransformedElementsCollection = new ObservableCollection<DataElement>(
                    _resourceFileService.GetTransformedElements(_transformSettings));

                MessageQueue.Enqueue(
                    $"Successfully transformed {_selectedResourceFile}");
            }
            catch (Exception ex)
            {
                MessageQueue.Enqueue(
                    $"Failed to transform resource file: {ex.Message}");
            }
        }

        /// <summary>
        /// Saves the transformed resource file.
        /// </summary>
        private async void SaveTransform()
        {
            var saveFileVm = _selectResourceFileViewModelFactory(_dialogService.ShowSaveTransformedFileDilaog);
            var path = (await DialogHost.Show(saveFileVm)) as string;

            if (path == null)
            {
                return;
            }

            try
            {
                _resourceFileService.Save(path);

                MessageQueue.Enqueue($"Successfully saved file '{path}'");
            }
            catch (Exception)
            {
                MessageQueue.Enqueue($"Faile to save '{path}'");
            }
        }
    }
}