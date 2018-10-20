using System;

public class GetSteamGames
{
    public static async Task<Weather> GetWeather(string zipCode)
    {
        //Sign up for a free API key at https://steamcommunity.com/dev
        string key = "INSERT API KEY HERE";
        string steamId = "INSERT STEAM ID HERE";
        string queryString = "http://api.steampowered.com/IPlayerService/GetOwnedGames/v0001/?key="
            + key + "&steamid=" + steamId + "&format=json";

        dynamic results = await DataService.getDataFromService(queryString).ConfigureAwait(false);

        if (results["response"] != null && results["response.games"] != null)
        {

            List<SteamGame> steamGames = new List<SteamGame>();
           foreach (string game in results["response.games"]) {
                SteamGame steamGame = new SteamGame();
                steamGame.AppId = (string)results["games.appid"];
                steamGame.PlayTime = (string)results["games.playtime_forever"];
            }

            return steamGames;
        }
        else
        {
            return null;
        }
    }
}
