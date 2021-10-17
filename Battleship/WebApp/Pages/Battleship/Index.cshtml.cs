using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;

namespace WebApp.Pages.Battleship
{
    public class Index : PageModel
    {
      private static string StartMenuPartialPath = System.AppContext.BaseDirectory + "Pages\\Shared\\_StartMenuPartial.html"; 
      private static string NewGamePartialPath = System.AppContext.BaseDirectory + "Pages\\Shared\\_NewGamePartial.html"; 
      public string StartMenuPartial { get; set; } = System.IO.File.ReadAllText(StartMenuPartialPath); 
      public string NewGamePartial { get; set; } = System.IO.File.ReadAllText(NewGamePartialPath); 
        void OnGet()
        {
         System.Console.WriteLine(System.AppContext.BaseDirectory);
            // System.Diagnostics.Debug.WriteLine("In Page Index, Function OnGet");
        }
    }
}