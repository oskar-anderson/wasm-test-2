using Microsoft.AspNetCore.Mvc.RazorPages;
using System.IO;

namespace WebApp.Pages.Battleship
{
    public class Index : PageModel
    {
        void OnGet()
        { 
            System.Console.WriteLine(System.AppContext.BaseDirectory);
            // System.Diagnostics.Debug.WriteLine("In Page Index, Function OnGet");
        }
    }
}