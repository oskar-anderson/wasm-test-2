import GameSettings from "../model/GameSettings.js";
import GameMethods from "../ServerRestApiGameMethods.js";
import Rendering from "../Rendering.js";
import {KeyboardIdentifierList} from "../model/Input.js";

export default class NewGamePartial {


    public static main(rendering: Rendering): void {
        let sliderHeight = document.getElementById("BoardHeight") as HTMLInputElement | null;
        let outputHeight = document.getElementById("BoardHeightIndicator");
        if (sliderHeight === null) throw Error('sliderHeight is null!');
        if (outputHeight === null) throw Error('outputHeight is null!');
        outputHeight.innerHTML = sliderHeight.value; // Display the default slider value

        // Update the current slider value (each time you drag the slider handle)
        sliderHeight.oninput = function () {
            outputHeight!.innerHTML = sliderHeight!.value;
        };

        let sliderWidth = document.getElementById("BoardWidth") as HTMLInputElement | null;
        let outputWidth = document.getElementById("BoardWidthIndicator");
        if (sliderWidth === null) throw Error('sliderWidth is null!');
        if (outputWidth === null) throw Error('outputWidth is null!');
        outputWidth.innerHTML = sliderWidth.value;

        sliderWidth.oninput = function () {
            outputWidth!.innerHTML = sliderWidth!.value;
        };
        let startBtn = document.querySelector("#Start");
        if (startBtn === null) throw Error('outputWidth is null!');
        startBtn.addEventListener("click", () => NewGamePartial.launchGame(rendering));
    }


    public static async launchGame(rendering: Rendering): Promise<void> {
        console.log("in launchGame");
        let settings = NewGamePartial.getGameSettings();
        console.log("after getGameSettings");
        console.log(settings);

        let gameSettingsValidity = await GameMethods.CheckGameSettingsValidity(settings);
        if (!gameSettingsValidity.areSettingsValid) {
            let errorField = document.getElementById("rulesetError");
            if (errorField === null) throw Error("Error field is null!");
            errorField.innerHTML = gameSettingsValidity.errorMessage;
            return;
        }
        let gameViewDTO = await GameMethods.StartGame(settings);
        // @ts-ignore
        gameViewDTO.GameData.Input = KeyboardIdentifierList.getDefaultInput();
        // let gameViewData = await GameMethods.DoGame(gameViewDTO.GameData);
        console.log("calling render");
        await rendering.renderByName("GameViewController", gameViewDTO.GameData);
    }

    public static getGameSettings(): GameSettings {
        let elBoardHeight = document.getElementById("BoardHeight") as HTMLInputElement | null;
        let elBoardWidth = document.getElementById("BoardWidth") as HTMLInputElement | null;
        let elAllowedPlacementType = document.getElementById("AllowedPlacementType") as HTMLInputElement | null;
        let elShips = document.getElementById("Ships") as HTMLInputElement | null;
        if (elBoardHeight === null || elBoardWidth === null || elAllowedPlacementType === null || elShips === null) throw Error('One or more inputs is null');
        
        let height = parseInt(elBoardHeight.value);
        let width = parseInt(elBoardWidth.value);
        let allowedPlacementType = parseInt(elAllowedPlacementType.value);
        let ships = elShips.value;

        let input = new GameSettings(height, width, allowedPlacementType, ships);
        return input;
    }


}