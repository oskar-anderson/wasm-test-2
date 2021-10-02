using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using DAL;
using Domain;
using Domain.Model;
using Domain.Tile;
using Game;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using RogueSharp;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace WebApp.Pages.Battleship
{
    public class GameViewJs : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }
    }
}