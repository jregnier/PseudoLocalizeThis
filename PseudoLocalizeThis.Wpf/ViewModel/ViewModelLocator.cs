using System.Windows;
using Autofac;
using MaterialDesignThemes.Wpf;
using PseudoLocalizeThis.Wpf.Services;
using TransformLib.Services;

namespace PseudoLocalizeThis.Wpf.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        private static IContainer _container;

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ResourceFileService>().As<IResourceFileService>();
            builder.RegisterInstance(new DialogService(Application.Current.MainWindow)).As<IDialogService>();
            builder.RegisterType<SnackbarMessageQueue>().As<ISnackbarMessageQueue>();
            builder.RegisterType<SelectResourceFileViewModel>();
            builder.RegisterType<MainViewModel>();

            _container = builder.Build();
            _container.BeginLifetimeScope();
        }

        public MainViewModel Main => _container.Resolve<MainViewModel>();

        public static void Cleanup()
        {
            _container.Dispose();
        }
    }
}