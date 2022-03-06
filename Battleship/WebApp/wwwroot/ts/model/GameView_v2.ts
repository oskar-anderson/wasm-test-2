import CharInfo from "./CharInfo.js";
import GameData from "./GameData.js"

export default class GameView_v2 {
    GameData: GameData;
    Board: CharInfo[][];
    ShipPlacementStatus: string;
    
    constructor(_gameData: object | undefined, 
                _board: CharInfo[][] | undefined, 
                _shipPlacementStatus: string | undefined) {
        if ([_gameData, _board, _shipPlacementStatus].includes(undefined)) {
            throw Error("Object has undefined fields!");
        }
        // @ts-ignore
        this.GameData = _gameData!;
        this.Board = _board!;
        this.ShipPlacementStatus = _shipPlacementStatus!;
    }

    public static mapJsonToObject(json: string): GameView_v2 {
        console.log("mapJsonToObject");
        console.log(json);
        let obj = JSON.parse(json);
        return new GameView_v2(obj.GameData, obj.Board, obj.ShipPlacementStatus);
    }
}