using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using Domain.Model.Api;
using System.Text.Json;
using Domain;
using Domain.Tile;
using Domain.Model;
using Game;

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
			bool areSettingsValid = Game.Utils.TryBtnStart(settings.Ships, settings.BoardWidth, settings.BoardHeight, settings.AllowedPlacementType, ref errorMsgText, out _);
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
			BaseBattleship game = new BaseBattleship(
				settings.BoardHeight,
				settings.BoardWidth,
				settings.Ships,
				settings.AllowedPlacementType,
				-1,
				-1
			);
			GameViewDTO_v2 gameViewDTO_v2 = new GameViewDTO_v2(
				new GameDataSerializable(game.GameData),
				GetDrawArea(game.GameData), 
				"Todo"
			);

			return TurnApiReturnValueToJson(gameViewDTO_v2);
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
			BaseBattleship game = new WebBattle(gameData);
			
			// TODO! Input is actually not being modified
			new WebUpdateLogic(gameData.Input).Update(1d, game);
			game.GameData.FrameCount++;

			GameViewDTO gameViewDto = GetGameViewDto(game);
			return TurnApiReturnValueToJson(gameViewDto);
		}
		
		[HttpPost("DoGame_v1")]
		[Consumes("application/json")]
		[Produces("application/json")]
		// Unfortunately API input has to be string otherwise automatic deserialization to the class fails
		// if the class has a property with a JSONIgnore tag
		public ActionResult<string> DoGame_v1(GameDataSerializable gameDataSerializable)
		{
			// GameDataSerializable gameDataSerializable = JsonSerializer.Deserialize<GameDataSerializable>(gameDataSerializableString)!;
			GameData gameData = GameDataSerializable.ToGameModelSerializable(gameDataSerializable);
			BaseBattleship game = new WebBattle(gameData);
			
			new WebUpdateLogic(gameData.Input).Update(1d, game);
			game.GameData.FrameCount++;

			GameViewDTO_v1 gameViewDto = GetGameViewDto_v1(game);
			return TurnApiReturnValueToJson(gameViewDto);
		}

		[HttpPost("DoGame_v2")]
		[Consumes("application/json")]
		[Produces("application/json")]
		public ActionResult<string> DoGame_v2(GameDataSerializable gameDataSerializable)
		{
			GameData gameData = GameDataSerializable.ToGameModelSerializable(gameDataSerializable);
			BaseBattleship game = new WebBattle(gameData);
			
			new WebUpdateLogic(gameData.Input).Update(1d, game);
			game.GameData.FrameCount++;

			GameViewDTO_v2 gameViewDTO_v2 = new GameViewDTO_v2(
				new GameDataSerializable(game.GameData),
				GetDrawArea(game.GameData), 
				"Todo"
				);
			
			return TurnApiReturnValueToJson(gameViewDTO_v2);
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

			string base64Picture = SfmlApp.ConsoleDrawLogic.GetDrawAreaAsPicture(game.GameData);
			
			GameViewDTO result = new GameViewDTO(gameDataSerializable, base64Picture, "todo");
			return result;
		}
		
		private GameViewDTO_v1 GetGameViewDto_v1(BaseBattleship game)
		{
			GameDataSerializable gameDataSerializable = new GameDataSerializable(game.GameData);
			
			TileData.CharInfo[][] board = GetDrawArea(game.GameData);
			UpdateLogic.ShipPlacementStatus shipPlacementStatus = UpdateLogic.GetShipPlacementStatus(game.GameData);

			List<byte> boardPixelsRgbaSerialized = SfmlApp.ConsoleDrawLogic.GetBoardAsImage(game.GameData);

			GameViewDTO_v1 result = new GameViewDTO_v1(gameDataSerializable, boardPixelsRgbaSerialized, "todo");
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
