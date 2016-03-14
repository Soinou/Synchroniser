using Synchroniser.Libraries;
using System.ComponentModel;
using System.Windows;

namespace Synchroniser.Views
{
    /// <summary>
    /// Interaction logic for LoadingView.xaml
    /// </summary>
    public partial class LoadingView : Window
    {
        /// <summary>
        /// Creates a new LoadingView
        /// </summary>
        /// <param name="synchroniser">Synchroniser</param>
        public LoadingView(ISynchroniser synchroniser)
        {
            synchroniser_ = synchroniser;
            working_ = false;

            InitializeComponent();
        }

        /// <summary>
        /// Called when the synchroniser needs to send us the current progress
        /// </summary>
        /// <param name="progress">Progress</param>
        /// <param name="message">Message</param>
        private void OnSynchroniserProgress(float progress, string message)
        {
            Dispatcher.Invoke(() => LoadingProgress.Value = progress);
            Dispatcher.Invoke(() => LoadingLabel.Content = message);
        }

        /// <summary>
        /// Called when the window is about to be closed
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments</param>
        private void OnWindowClosing(object sender, CancelEventArgs e)
        {
            e.Cancel = working_;
        }

        /// <summary>
        /// Called when the window is initialized
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments</param>
        private async void OnWindowInitialized(object sender, System.EventArgs e)
        {
            working_ = true;
            await synchroniser_.Load(OnSynchroniserProgress);
            working_ = false;
            Close();
        }

        /// <summary>
        /// Synchroniser
        /// </summary>
        private ISynchroniser synchroniser_;

        /// <summary>
        /// If the synchroniser is working
        /// </summary>
        private bool working_;
    }
}
