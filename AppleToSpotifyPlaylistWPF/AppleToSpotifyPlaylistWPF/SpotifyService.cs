using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppleToSpotifyPlaylistWPF
{
    class SpotifyService
    {
        //get it from dashboard for now
        string accessToken = "BQBd1b0ChlCfEq5YgFRHDgLacASSEjR8rzSKpDmmcScYuefxfuAnTu-k0FkbQiJRZzDUyIOSSI8ZbfI4W90X1VJ7cyCvUscgNj3EvuppNUOwM2uMiO_8gxdvdleEuFvfjxvcvpiiMcRV774UYWOSo1zpN3XDDKPZDGm4qbEWk82c0ib0Pd1HfdpmPnfpZn85-ZwvccOgYqgvJQfZ9-LVqFVSjR6de5sUlEc0AdcEXQ9LpChY0cbQIKQwgzUhQqxYXPg";
        public async Task AddTracksToSpotify(List<Track> tracks, string playlistName)
        {
            var spotify = new SpotifyClient(accessToken);
            var spotifyTracks = new List<string>();
            var user = await spotify.UserProfile.Current();
            PlaylistCreateRequest pcr = new PlaylistCreateRequest(playlistName);
            var newPlaylist = await spotify.Playlists.Create(user.Id, pcr);

            foreach (var track in tracks)
            {
                try
                {
                    SearchRequest req = new SearchRequest(SearchRequest.Types.Track, $"{track.TrackName} {track.ArtistName} {track.AlbumName}");
                    var searchresp = await spotify.Search.Item(req);
                    if (searchresp.Tracks.Items.Count < 1) continue;
                    spotifyTracks.Add($"spotify:track:{searchresp.Tracks.Items[0].Id}");
                }
                catch
                {
                    continue;
                }
            }

            List<string> reqStrings = new List<string>();
            foreach(var trackstring in spotifyTracks)
            {
                reqStrings.Add(trackstring);

                if (reqStrings.Count >= 50)
                {
                    PlaylistAddItemsRequest pair = new PlaylistAddItemsRequest(reqStrings);
                    pair.BuildQueryParams();
                    pair.BuildBodyParams();
                    await spotify.Playlists.AddItems(newPlaylist.Id, pair);
                    reqStrings = new List<string>();
                }

            }
                
            
            

        }
    }
}
