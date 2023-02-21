using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppleToSpotifyPlaylistWPF
{
    public class MainWindowViewModel : BaseViewModel
    {
        private string _playlistUrl;
        public string PlaylistUrl
        {
            get
            {
                return _playlistUrl;
            }
            set
            {
                _playlistUrl = value;
                OnPropertyChanged(nameof(PlaylistUrl));
            }
        }
        private string playlistName;
        public string PlaylistName
        {
            get
            {
                return playlistName;
            }
            set
            {
                playlistName = value;
                OnPropertyChanged(nameof(PlaylistName));
            }
        }

        private List<Track> _trackList;
        public List<Track> TrackList
        {
            get
            {
                return _trackList;
            }
            set
            {
                _trackList = value;
                OnPropertyChanged(nameof(TrackList));
            }
        }

        private bool isBusy;
        public bool IsBusy
        {
            get
            {
                return isBusy;
            }
            set
            {
                isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        public MainWindowViewModel()
        {
        }

        public async Task GetAppleTracks()
        {
            IsBusy = true;
            AppleService aps = new AppleService(PlaylistUrl);
            TrackList = aps.GetAppleTracks();
            IsBusy = false;

            //SpotifyService ss = new SpotifyService();
            //await ss.AddTracksToSpotify(TrackList, playlistName);
        }

        public async Task SendTracksToSpotify()
        {
            SpotifyService ss = new SpotifyService();
            await ss.AddTracksToSpotify(TrackList, PlaylistName);
        }
    }
}
