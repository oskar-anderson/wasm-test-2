using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Domain.Model.Api;
using System.Text.Json;
using Domain;
using Domain.Tile;
using Domain.Model;
using Game;
using WebApp.ApiControllers.Models;

namespace WebApp.ApiControllers
{
	// API return values are serialized manually, because automatic return value
	// serialization turns Json keys to lowercase to match js conventions.
	// Maintaining the same naming convention in front- and backend is clearer.
	[Route("api/[controller]")]
	[ApiController]
	public class GameController : ControllerBase
	{
		[HttpPost("CheckValidGameSettings")]
		[Consumes("application/json")]
		[Produces("application/json")]
		public ActionResult<string> CheckValidGameSettings(ValidGameSettingsInRuleSet settings)
		{
			string errorMsgText = "";
			bool areSettingsValid = Utils.TryBtnStart(settings.Ships, settings.BoardWidth, settings.BoardHeight, settings.AllowedPlacementType, ref errorMsgText, out _);
			var result = new ValidGameSettingsOut()
			{
				AreSettingsValid = areSettingsValid,
				ErrorMessage = errorMsgText
			};
			return TurnApiReturnValueToJson(result);
		}


		[HttpPost("StartGame")]
		[Consumes("application/json")]
		[Produces("application/json")]
		public ActionResult<string> StartGame(ValidGameSettingsInRuleSet settings)
		{
			Console.WriteLine(settings);
			BaseBattleship game = new WebBattle(
				settings.BoardHeight,
				settings.BoardWidth,
				settings.Ships,
				settings.AllowedPlacementType,
				-1,
				-1,
				new WebInput(Input.GetDefaultInput())
			);
			GameViewDTO gameViewDto = GetGameViewDto(game);
			return TurnApiReturnValueToJson(gameViewDto);
		}
		

		[HttpPost("DoGame")]
		[Consumes("application/json")]
		[Produces("application/json")]
		// Unfortunately API input has to be string otherwise automatic deserialization to the class fails
		// if the class has a property with a JSONIgnore tag
		public ActionResult<string> DoGame(GameDataSerializable gameDataSerializable)
		{
			// GameDataSerializable gameDataSerializable = JsonSerializer.Deserialize<GameDataSerializable>(gameDataSerializableString)!;
			GameData gameData = GameDataSerializable.ToGameModelSerializable(gameDataSerializable);
			BaseBattleship game = new WebBattle(gameData, new WebInput(gameData.Input));
			
			BaseBattleship.Update(1d, game);
			game.GameData.FrameCount++;

			GameViewDTO gameViewDto = GetGameViewDto(game);
			return TurnApiReturnValueToJson(gameViewDto);
		}
		
		[HttpPost("GetDrawArea")]
		[Consumes("application/json")]
		[Produces("application/json")]
		public ActionResult<string> GetDrawArea(GameDataSerializable gameDataSerializable)
		{
			GameData gameData = GameDataSerializable.ToGameModelSerializable(gameDataSerializable);
			BaseBattleship game = new WebBattle(gameData, new WebInput(gameData.Input));
			TileData.CharInfo[][] drawArea = GetDrawArea(game.GameData);
			return TurnApiReturnValueToJson(drawArea);
		}

		private ActionResult<string> TurnApiReturnValueToJson(object o)
		{
			string json = JsonSerializer.Serialize(o, new JsonSerializerOptions() { WriteIndented = true });
			return Ok(json);
		}

		private GameViewDTO GetGameViewDto(BaseBattleship game)
		{
			GameDataSerializable gameDataSerializable = new GameDataSerializable(game.GameData);
			
			TileData.CharInfo[][] board = GetDrawArea(game.GameData);
			UpdateLogic.ShipPlacementStatus shipPlacementStatus = UpdateLogic.GetShipPlacementStatus(game.GameData);

			List<byte> boardPixelsRgbaSerialized = SfmlApp.ConsoleDrawLogic.GetBoardAsImage(game.GameData);
			
			GameViewDTO result = new GameViewDTO(gameDataSerializable, boardPixelsRgbaSerialized, "todo");
			return result;
		}
		
		private TileData.CharInfo[][] GetDrawArea(GameData gameData)
		{
			TileData.CharInfo[,] map = new TileData.CharInfo[40, 40];
			BaseDraw.GetDrawArea(gameData, ref map);
			return map.ToJaggedArray();
		}
	}
}
