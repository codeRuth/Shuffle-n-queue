﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq; 
using Shuffle_n_queue.Resources;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Media.PhoneExtensions; 
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;

namespace Shuffle_n_queue.ViewModels
{
    //public class SongItem
    //{
    //    public string Name { get; set; }
    //    public string Album { get; set; }
    //    public string Artist { get; set; }
    //    public WriteableBitmap AlbumArt { get; set; }
    //}

    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.SongItems = new ObservableCollection<Song>();
        }

        public ObservableCollection<Song> SongItems { get; private set; }
        public PlaylistCollection Playlists { get; set; }
        public SongCollection AllSongs { get; set; }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public void LoadData()
        {
            foreach (MediaSource source in MediaSource.GetAvailableMediaSources())
            {
                if (source.MediaSourceType == MediaSourceType.LocalDevice)
                {
                    var mediaLibrary = new MediaLibrary(source);

                    AllSongs = mediaLibrary.Songs;
                    foreach (var song in AllSongs)
                        SongItems.Add(song);

                    Playlists = mediaLibrary.Playlists;
                }
            }
            //SongItems.OrderBy(s => s.Artist + s.Name); 

            this.IsDataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}