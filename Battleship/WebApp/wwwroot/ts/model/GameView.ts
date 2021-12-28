import CharInfo from "./CharInfo.js";

export default class GameView {
    GameData: object;
    Board: CharInfo[][];
    ShipPlacementStatus: string;
    
    constructor(_gameData: object | undefined, 
                _board: CharInfo[][] | undefined, 
                _shipPlacementStatus: string | undefined) {
        if ([_gameData, _board, _shipPlacementStatus].includes(undefined)) {
            throw Error("Object has undefined fields!");
        }
        this.GameData = _gameData!;
        this.Board = _board!;
        this.ShipPlacementStatus = _shipPlacementStatus!;
    }

    public static mapJsonToObject(json: string): GameView {
        console.log("mapJsonToObject");
        console.log(json);
        let obj = JSON.parse(json);
        return new GameView(obj.GameData, obj.Board, obj.ShipPlacementStatus);
    }
}