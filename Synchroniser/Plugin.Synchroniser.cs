using Synchroniser.Libraries.Synchronisers;
using Synchroniser.Views;
using System;
using System.Reflection;

namespace MusicBeePlugin
{
    /// <summary>
    /// Plugin implementation
    /// </summary>
    public partial class Plugin
    {
        /// <summary>
        /// Closes the plugin
        /// </summary>
        /// <param name="reason"></param>
        public void Close(PluginCloseReason reason)
        {
        }

        /// <summary>
        /// Configures the plugin
        /// </summary>
        /// <param name="handle"></param>
        /// <returns></returns>
        public bool Configure(IntPtr handle)
        {
            return true;
        }

        /// <summary>
        /// Initializes the plugin
        /// </summary>
        /// <param name="api_ptr_">MusicBee api interface pointer</param>
        /// <returns>Plugin informations</returns>
        public PluginInfo Initialise(IntPtr api_ptr_)
        {
            api_ = new MusicBeeApiInterface();

            api_.Initialise(api_ptr_);

            api_.MB_AddMenuItem("mnuTools/Synchronise with iTunes",
                "Synchronises the MusicBee playlists with the iTunes playlists", OnSynchroniseITunes);

            info_.PluginInfoVersion = PluginInfoVersion;
            info_.Name = "Synchroniser";
            info_.Description = "Synchronizes your MusicBee playlists with other music players";
            info_.Author = "Abricot Soinou <abricot.soinou@gmail.com>";
            info_.TargetApplication = "";
            info_.Type = PluginType.General;

            var version = Assembly.GetExecutingAssembly().GetName().Version;

            info_.VersionMajor = (short)version.Major;
            info_.VersionMinor = (short)version.Minor;
            info_.Revision = (short)version.Revision;
            info_.MinInterfaceVersion = MinInterfaceVersion;
            info_.MinApiRevision = MinApiRevision;
            info_.ReceiveNotifications = ReceiveNotificationFlags.StartupOnly;
            info_.ConfigurationPanelHeight = 0;

            return info_;
        }

        /// <summary>
        /// Called when MusicBee has a notification to send to the plugin
        /// </summary>
        /// <param name="source">Source</param>
        /// <param name="type">Notification type</param>
        public void ReceiveNotification(string source, NotificationType type)
        {
            switch (type)
            {
                case NotificationType.PluginStartup:
                    break;
            }
        }

        /// <summary>
        /// Saves the plugin settings
        /// </summary>
        public void SaveSettings()
        {
            // Nothing to do here
        }

        /// <summary>
        /// Uninstalls the plugin
        /// </summary>
        public void Uninstall()
        {
            // Nothing to do here
        }

        /// <summary>
        /// Called when the Synchronise iTunes menu item is clicked
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="args">Arguments</param>
        private void OnSynchroniseITunes(object sender, EventArgs args)
        {
            using (var synchroniser = new ITunesSynchroniser(api_))
            {
                var loading = new LoadingView(synchroniser);

                loading.ShowDialog();

                var synchronise = new SynchroniseView(synchroniser);

                synchronise.ShowDialog();
            }
        }

        /// <summary>
        /// MusicBee api interface
        /// </summary>
        private MusicBeeApiInterface api_;

        /// <summary>
        /// Plugin informations
        /// </summary>
        private PluginInfo info_ = new PluginInfo();
    }
}
