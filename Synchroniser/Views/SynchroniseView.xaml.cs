using Synchroniser.Libraries;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Synchroniser.Views
{
    /// <summary>
    /// Interaction logic for SynchroniseView.xaml
    /// </summary>
    public partial class SynchroniseView
    {
        /// <summary>
        /// Creates a new SynchroniseView
        /// </summary>
        /// <param name="synchroniser">Synchroniser</param>
        public SynchroniseView(ISynchroniser synchroniser)
        {
            synchroniser_ = synchroniser;
            working_ = false;

            InitializeComponent();
        }

        /// <summary>
        /// Called when the synchronise button is clicked
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Arguments</param>
        private async void OnSynchroniseButtonClicked(object sender, RoutedEventArgs e)
        {
            // Now working
            working_ = true;

            // Hide button
            SynchroniseButton.Visibility = Visibility.Hidden;

            // Show progress bar and label
            SynchroniseProgress.Value = 0;
            SynchroniseProgress.Visibility = Visibility.Visible;
            SynchroniseLabel.Content = "Please wait...";
            SynchroniseLabel.Visibility = Visibility.Visible;

            // Start the synchroniser
            await synchroniser_.Synchronise(OnSynchroniseProgress);

            // Hide label and progress bar
            SynchroniseLabel.Visibility = Visibility.Hidden;
            SynchroniseProgress.Visibility = Visibility.Hidden;

            // Show button
            SynchroniseButton.Visibility = Visibility.Visible;

            // Finished working
            working_ = false;

            // Close the window
            Close();
        }

        /// <summary>
        /// Called when the synchroniser wants to pass us current progress
        /// </summary>
        /// <param name="progress">Progress</param>
        /// <param name="message">Message</param>
        private void OnSynchroniseProgress(float progress, string message)
        {
            SynchroniseProgress.Dispatcher.Invoke(() => SynchroniseProgress.Value = progress);
            SynchroniseLabel.Dispatcher.Invoke(() => SynchroniseLabel.Content = message);
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
        private void OnWindowInitialized(object sender, EventArgs e)
        {
            UpdateChanges();
        }

        /// <summary>
        /// Updates the changes tree of the window
        /// </summary>
        private void UpdateChanges()
        {
            var changes = synchroniser_.Changes;
            var playlists = new TreeViewItem();
            var tracks = new TreeViewItem();

            foreach (var change in changes)
            {
                var item = new TreeViewItem();
                item.Header = change.ToString();

                switch (change.Type)
                {
                    case ChangeTypes.kAddPlaylist:
                    case ChangeTypes.kAddTrackToPlaylist:
                        item.Foreground = Brushes.ForestGreen;
                        playlists.Items.Add(item);
                        break;

                    case ChangeTypes.kRemovePlaylist:
                    case ChangeTypes.kRemoveTrackFromPlaylist:
                        item.Foreground = Brushes.OrangeRed;
                        playlists.Items.Add(item);
                        break;

                    case ChangeTypes.kAddTrack:
                        item.Foreground = Brushes.ForestGreen;
                        tracks.Items.Add(item);
                        break;

                    case ChangeTypes.kRemoveTrack:
                        item.Foreground = Brushes.OrangeRed;
                        tracks.Items.Add(item);
                        break;

                    default:
                        break;
                }
            }

            playlists.Header = "Playlists (" + playlists.Items.Count + " changes)";
            tracks.Header = "Tracks (" + tracks.Items.Count + " changes)";

            ChangesTree.Items.Add(playlists);
            ChangesTree.Items.Add(tracks);
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
