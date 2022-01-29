import gameMethods from "./ServerRestApiGameMethods.js";
import Rendering from "./Rendering.js";
import {BtnState, Input, KeyboardIdentifier, KeyboardIdentifierList, KeyboardKey} from "./model/Input.js";
import GameView from "./model/GameView.js";
import GameData from "./model/GameData.js";
import dataSet from "./TestPerformanceUsingLocalData.js";

export default class GameViewController {
    
    public Gamedata : GameData;
    public IsEventListenerDisabled: boolean = false;
    public Rendering: Rendering;
    public FrameCount: number = 0;
    
    public constructor(gamedata: GameData, rendering: Rendering) {
        this.Gamedata = gamedata;
        this.Rendering = rendering;
    }
    
    public async runGame() {
        this.Gamedata = await this.gameLoop();
        this.registerListeners();
        while (true) {
            if (! this.IsEventListenerDisabled) {
                this.Gamedata.Input = KeyboardIdentifierList.getDefaultInput();
                this.Gamedata = await this.gameLoop();
                if (this.Gamedata.ElapsedTime > 9000) {
                    break;
                }
            }
            await GameViewController.sleep(1);
        }
    }

    static sleep(ms: number) {
        return new Promise(resolve => setTimeout(resolve, ms));
    }
    

    public registerListeners() {
        // prevent scrolling
        window.addEventListener("keydown", function(e) {
            if(["Space","ArrowUp","ArrowDown","ArrowLeft","ArrowRight"].indexOf(e.code) > -1) {
                e.preventDefault();
            }
        }, false);
        
        let keysToListen = [
            "KeyR",
            "KeyX",
            "Escape",
            "KeyC",
            "KeyZ",
            
            "Digit1",
            "Digit2",
            "Digit3",
            
            "ArrowUp",
            "ArrowDown",
            "ArrowLeft",
            "ArrowRight",
            
            "KeyJ",
            "KeyI",
            "KeyL",
            "KeyK",
            
            "Slash",
            "Period",
            "Comma",
        ];
        
        document.addEventListener("keydown", async (event) => {
            if (this.IsEventListenerDisabled) return;
            if (keysToListen.indexOf(event.code) === -1) {
                console.log(`Unregistered key ${event.code} pressed!`);
                return ;
            }
            this.IsEventListenerDisabled = true;
            console.log("Pressed key:");
            console.log(event.code);
            console.log(event);

            let input : Input = KeyboardIdentifierList.getDefaultInput();
            
            switch (event.code) {
                case "KeyR":
                    this.setInputToPressed(input, KeyboardIdentifierList.KeyR);
                    break;
                case "KeyX":
                    this.setInputToPressed(input, KeyboardIdentifierList.KeyX);
                    break;
                case "Escape":
                    this.setInputToPressed(input, KeyboardIdentifierList.Escape);
                    break;
                case "KeyC":
                    this.setInputToPressed(input, KeyboardIdentifierList.KeyC);
                    break;
                case "KeyZ":
                    this.setInputToPressed(input, KeyboardIdentifierList.KeyZ);

                    break;
                    
                case "Digit1":
                    this.setInputToPressed(input, KeyboardIdentifierList.Digit1);
                    break;
                case "Digit2":
                    this.setInputToPressed(input, KeyboardIdentifierList.Digit2);
                    break;
                case "Digit3":
                    this.setInputToPressed(input, KeyboardIdentifierList.Digit3);

                    break;
                    
                case "ArrowUp":
                    this.setInputToPressed(input, KeyboardIdentifierList.ArrowUp);
                    break;
                case "ArrowDown":
                    this.setInputToPressed(input, KeyboardIdentifierList.ArrowDown);
                    break;
                case "ArrowLeft":
                    this.setInputToPressed(input, KeyboardIdentifierList.ArrowLeft);
                    break;
                case "ArrowRight":
                    this.setInputToPressed(input, KeyboardIdentifierList.ArrowRight);
                    break;

                case "KeyJ":
                    this.setInputToPressed(input, KeyboardIdentifierList.KeyJ);
                    break;
                case "KeyI":
                    this.setInputToPressed(input, KeyboardIdentifierList.KeyI);
                    break;
                case "KeyL":
                    this.setInputToPressed(input, KeyboardIdentifierList.KeyL);
                    break;
                case "KeyK":
                    this.setInputToPressed(input, KeyboardIdentifierList.KeyK);
                    break;

                case "Slash":
                    this.setInputToPressed(input, KeyboardIdentifierList.Slash);
                    break;
                case "Period":
                    this.setInputToPressed(input, KeyboardIdentifierList.Period);
                    break;
                case "Comma":
                    this.setInputToPressed(input, KeyboardIdentifierList.Comma);

                    break;
            }
            this.Gamedata.Input = input;
            this.Gamedata = await this.gameLoop();
        
            
            this.IsEventListenerDisabled = false;
        })
    }
    
    public setInputToPressed(input : Input, key : KeyboardIdentifier): void {
        let t : KeyboardKey | undefined = input.Keyboard.KeyboardState.find(x => x.Identifier === key);
        if (t === undefined) throw new Error(`Key: ${key} should not have been undefined!`);
        t.Values = [BtnState.Pressed];
    }

    public async gameLoop(): Promise<GameData> {
        let gameView: GameView | null = null;
        let isTestingLocalData = false;
        if (isTestingLocalData) {
            gameView = JSON.parse(dataSet);
            this.FrameCount++;
            gameView!.GameData.FrameCount = this.FrameCount;
        } else {
            gameView = await gameMethods.DoGame(this.Gamedata);
        }
        console.log(gameView);
        await this.Rendering.renderByName("GameView", gameView!);
        return gameView!.GameData;
    };
}
