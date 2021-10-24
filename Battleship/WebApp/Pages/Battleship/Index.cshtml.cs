using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;

namespace WebApp.Pages.Battleship
{
    public class Index : PageModel
    {
        private static readonly string StartMenuPartialPath = System.AppContext.BaseDirectory + "Pages\\Shared\\_StartMenuPartial.html"; 
        private static readonly string NewGamePartialPath = System.AppContext.BaseDirectory + "Pages\\Shared\\_NewGamePartial.html";
        private static readonly string GameViewPartialPath = System.AppContext.BaseDirectory + "Pages\\Shared\\_GameView.html";
        private static readonly string GameViewControllerPartialPath = System.AppContext.BaseDirectory + "Pages\\Shared\\_GameViewController.html";
        public string StartMenuPartial { get; set; } = System.IO.File.ReadAllText(StartMenuPartialPath); 
        public string NewGamePartial { get; set; } = System.IO.File.ReadAllText(NewGamePartialPath); 
        public string GameViewPartial { get; set; } = System.IO.File.ReadAllText(GameViewPartialPath); 
        public string GameViewControllerPartial { get; set; } = System.IO.File.ReadAllText(GameViewControllerPartialPath); 
        void OnGet()
        { 
            System.Console.WriteLine(System.AppContext.BaseDirectory);
            // System.Diagnostics.Debug.WriteLine("In Page Index, Function OnGet");
        }
    }
}