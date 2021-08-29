using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model.Api;
using Game;
using Domain.Model;
using System.Text.Json;

namespace WebApp.ApiControllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GameController : ControllerBase
	{
		[HttpPost("CheckValidGameSettings")]
		[Consumes("application/json")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ValidGameSettingsOut))]
		[ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ValidGameSettingsOut))]
		public ActionResult<ValidGameSettingsOut> CheckValidGameSettings(ValidGameSettingsInRuleSet settings)
		{
			System.Diagnostics.Debug.WriteLine("In CheckValidGameSettings");
			string errorMsgText = "";
			bool areSettingsValid = Game.Utils.TryBtnStart(settings.Ships, settings.BoardWidth, settings.BoardHeight, settings.AllowedPlacementType, ref errorMsgText, out _);
			return Ok(
				new ValidGameSettingsOut()
				{
					AreSettingsValid = areSettingsValid,
					ErrorMessage = errorMsgText
				}
			);
		}


		[HttpPost("StartGame")]
		[Consumes("application/json")]
		[Produces("application/json")]
		[ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ValidGameSettingsOut))]
		public ActionResult<ValidGameSettingsOut> StartGame(ValidGameSettingsInRuleSet settings)
		{
			BaseBattleship game = new WebBattle(
				settings.BoardHeight,
				settings.BoardWidth,
				settings.Ships,
				settings.AllowedPlacementType,
				-1,
				-1
			);
			GameDataSerializable gameDataSerializableSave = new GameDataSerializable(game.GameData);
			var gameDataSerialized = JsonSerializer.Serialize(gameDataSerializableSave, new JsonSerializerOptions() { WriteIndented = true });
			return Ok(gameDataSerialized);
		}
	}
}
