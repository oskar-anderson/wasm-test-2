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
using Domain.Tile;
using Domain;
using WebApp.ApiControllers.Models;

namespace WebApp.ApiControllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class GameController : ControllerBase
	{
		[HttpPost("CheckValidGameSettings")]
		[Consumes("application/json")]
		[Produces("application/json")]
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
		public ActionResult<string> StartGame(ValidGameSettingsInRuleSet settings)
		{
			BaseBattleship game = new WebBattle(
				settings.BoardHeight,
				settings.BoardWidth,
				settings.Ships,
				settings.AllowedPlacementType,
				-1,
				-1
			);
			GameDataSerializable gameDataSerializable = new GameDataSerializable(game.GameData);
			
			TileData.CharInfo[][] board = GetDrawArea(game.GameData);
			UpdateLogic.ShipPlacementStatus shipPlacementStatus = UpdateLogic.GetShipPlacementStatus(game.GameData);
			
			GameViewDTO result = new GameViewDTO(gameDataSerializable, board, "todo");
			string serializedResult = JsonSerializer.Serialize(result, new JsonSerializerOptions() { WriteIndented = true });
			
			return Ok(serializedResult);
		}
		

		[HttpPost("DoGame")]
		[Consumes("application/json")]
		[Produces("application/json")]
		public ActionResult<GameViewDTO> DoGame(GameDataSerializable gameDataSerializable)
		{
			GameData gameData = GameDataSerializable.ToGameModelSerializable(gameDataSerializable);
			BaseBattleship game = new WebBattle(gameData);
            
			game.Initialize();
			game.Update(1d, game.GameData);
			game.GameData.FrameCount++;
			
			GameDataSerializable gameDataSerializableSave = new GameDataSerializable(game.GameData);

			TileData.CharInfo[][] board = GetDrawArea(game.GameData);
			UpdateLogic.ShipPlacementStatus shipPlacementStatus = UpdateLogic.GetShipPlacementStatus(game.GameData);
			
			GameViewDTO result = new GameViewDTO(gameDataSerializableSave, board, "todo");
			
			return Ok(result);
		}
		
		[HttpPost("GetDrawArea")]
		[Consumes("application/json")]
		[Produces("application/json")]
		public ActionResult<TileData.CharInfo[][]> GetDrawArea(GameDataSerializable gameDataSerializable)
		{
			GameData gameData = GameDataSerializable.ToGameModelSerializable(gameDataSerializable);
			BaseBattleship game = new WebBattle(gameData);
            
			game.Initialize();

			return Ok(GetDrawArea(game.GameData));
		}
		
		private TileData.CharInfo[][] GetDrawArea(GameData gameData)
		{
			TileData.CharInfo[,] map = new TileData.CharInfo[40, 40];
			BaseDraw.GetDrawArea(gameData, ref map);
			return map.ToJaggedArray();
		}
	}
}
