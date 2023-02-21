using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace AppleToSpotifyPlaylistWPF
{
    public class AppleService
    {
        ChromeDriver driver;
        string playlistUrl;

        IList<IWebElement> TrackRows => driver.FindElements(By.XPath("//*[contains(@class,'songs-list-row--preview')]"));

        public AppleService(string url)
        {
            playlistUrl = url;
            ChromeOptions co = new ChromeOptions();
            //co.AddArgument("--headless");
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        internal List<Track> GetAppleTracks()
        {
            GoToURL();
            return GetTracks();
        }

        private List<Track> GetTracks()
        {
            var TrackList = new List<Track>();
            for (int i = 0; i < TrackRows.Count; i++)
            {
                Console.WriteLine($"{i}/{TrackRows.Count}");
                var newTrack = new Track()
                {
                    TrackName = TrackRows[i].FindElement(By.XPath("./div[2]/div/div[2]/div/div[1]")).Text,
                    ArtistName = TrackRows[i].FindElement(By.XPath("./div[3]/div/span/a")).Text,
                    AlbumName = TrackRows[i].FindElement(By.XPath("./div[4]/div/span/a")).Text
                };
                TrackList.Add(newTrack);

            }
            driver.Quit();
            return TrackList;
        }

        private void GoToURL()
        {
            driver.Navigate().GoToUrl(playlistUrl);
            while (TrackRows.Count < 1)
            {
                Thread.Sleep(1000);
            }
        }
    }
}
