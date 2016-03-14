# Synchroniser

MusicBee plugin aiming to synchronise playlists across music players

## How do I install it ?

Download one of the [Releases](https://github.com/Soinou/Synchroniser/releases), unzip it, and copy the .dll inside your MusicBee plugin directory (Typically `C:\Program Files\MusicBee\Plugins` or `C:\Program Files (x86)\MusicBee\Plugins` if you're on a x64 system).

## How do I build it myself ?

Open the `Synchroniser.sln` in Visual Studio, then build the `Synchroniser` project. It will automatically copy the resulting .dll in the MusicBee plugin directory, and you can then either start the project if your MusicBee installation is in `C:\Program Files (x86)\MusicBee` or start it yourself or change the path in the project Debug setting to point to your `MusicBee.exe`

## How do I use it ?

Usage is pretty straight forward. You setup your playlists in MusicBee, then once you have installed the plugin, you select `Tools > Synchronise with iTunes` in MusicBee:

![Start the plugin](http://puu.sh/nGjNj/53b2e32440.png)

You will then have a loading screen looking like this:

![Loading screen](http://puu.sh/nGk44/96f296878c.png)

The plugin will load your library, the iTunes library, will compute the changes that needs to be made and will output them in a window looking like this:

![Preview screen](http://puu.sh/nGlKX/afaaa766a9.png)

Once you press the Synchronise button, the synchronisation will start and you will have a progress bar indicating how much is done and how much is left.

After all the changes are finished, the window will close by itself and your iTunes library should have all the playlists of your MusicBee library.
