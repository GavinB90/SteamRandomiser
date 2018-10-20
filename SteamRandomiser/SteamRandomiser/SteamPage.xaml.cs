using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SteamRandomiser
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SteamPage : ContentPage
	{

        public SteamPage ()
		{
			InitializeComponent ();
            BindingContext = new SteamGame();
		}

        private void GetAnySteamGameBtn_Clicked(object sender, EventArgs e)
        {
            setBindingContext(0);
        }
        
        private void GetUnplayedSteamGamesBtn_Clicked(object sender, EventArgs e)
        {
            setBindingContext(-1);
        }
        
        private void GetPlayedSteamGamesBtn_Clicked(object sender, EventArgs e)
        {
            setBindingContext(1);
        }

        private async void setBindingContext(int gamePlayedTime)
        {
            // get the list of games owned by the user and filtered by game played time
            List<SteamGame> steamGames = await GetSteamGames.GetGames(gamePlayedTime);
            // get a random number from 0 to size of games
            int steamGameNumber = randomNumber(steamGames.Count);
            // get the steam game out of the list
            SteamGame steamGame = steamGames[steamGameNumber];
            // get the steam game name
            string gameName = await GetSteamGames.GetGameName(steamGame.AppId);
            // set the steam game title
            steamGame.Title = gameName;
            // set the binding context
            BindingContext = steamGame;
        }

        private static int randomNumber(int listSize)
        {
            Random rnd = new Random();
            return rnd.Next(0, listSize);
        }

    }
}