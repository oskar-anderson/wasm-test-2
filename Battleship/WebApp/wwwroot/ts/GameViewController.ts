import gameMethods from "./ServerRestApiGameMethods.js";
import Rendering from "./Rendering.js";

export default class GameViewController {
    
    static async RunGame(gameData: any) {
        let gameLoop = async function(gameData: any) {
            gameData = await gameMethods.DoGame(gameData);
            console.log("reached renderByName");
            await Rendering.renderByName("GameView", gameData);
        }
        await gameLoop(gameData);
        GameViewController.registerListeners();
    }
    

    static registerListeners() {
        let keyDict = {
            "ArrowUp" : "ArrowUp",
            "ArrowDown" : "ArrowDown",
            "ArrowLeft" : "ArrowLeft",
            "ArrowRight" : "ArrowRight",
            "KeyZ" : "KeyZ",
            "KeyX" : "KeyX",
            "Digit1" : "Digit1",
            "Digit2" : "Digit2",
            "Digit3" : "Digit3"
        };
        
        document.addEventListener("keydown", function onEvent(event) {
            console.log("Pressed key:");
            console.log(event.code);
            console.log(event);

            for (let key in keyDict) {
                if (event.code === key) {
                    // @ts-ignore
                    let action = keyDict[key];
                    console.log(action);
                    if (action === "ArrowUp") {
                        // game.Input.KeyStatuses[UsedKeyKeys.A] = new KeyStatus(true, true);
                        // gameViewDto.GameData.Game.Input.KeyStatuses[]
                    }
                    if (action === "ArrowDown") {
                        
                    }
                    if (action === "ArrowLeft") {
                        
                    }
                    if (action === "ArrowRight") {
                        
                    }
                    // move
                }
            }
        })
    }
}
