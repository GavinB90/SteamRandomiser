using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SteamRandomiser
{
    public class GetSteamGames
    {
        public static async Task<List<SteamGame>> GetGames(int playTimeFilter)
        {
            //Sign up for a free API key at https://steamcommunity.com/dev
            string key = "INSERT API KEY HERE";
            string steamId = "INSERT STEAM ID HERE";
            string queryString = "http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key="
                + key + "&steamid=" + steamId + "&format=json";

            dynamic results = await DataService.getDataFromService(queryString).ConfigureAwait(false);

            List<SteamGame> steamGames = new List<SteamGame>();
            if (results["response"] != null)
            {                
                var response = results.response;
                var games = response.games;
                foreach (var game in games)
                {
                    bool includeGame = playTimeFilter == -1 ? game.playtime_forever == 0 : game.playtime_forever >= playTimeFilter;
                    if (includeGame)
                    {
                        SteamGame steamGame = new SteamGame();
                        steamGame.AppId = game.appid;
                        steamGame.PlayTime = game.playtime_forever + "m";
                        steamGames.Add(steamGame);
                    }
                }

                return steamGames;
            }

            return null;
        }

        public static async Task<string> GetGameName(string appId)
        {
            //string queryString = "http://steamspy.com/api.php?request=appdetails&appid=" + appId + "&format=json";
            string queryString = "http://api.steampowered.com/ISteamApps/GetAppList/v2/";

            dynamic results = await DataService.getDataFromService(queryString).ConfigureAwait(false);

            List<SteamGame> steamGames = new List<SteamGame>();
            if (results["applist"] != null)
            {
                var response = results.applist;
                var apps = response.apps;
                foreach (var app in apps)
                {
                    if (((string) app.appid).Equals(appId))
                    {
                        return app.name;
                    }
                }
            }

            return null;
        }

    }
}