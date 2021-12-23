export default class GameView {
    constructor(_gameData, _board, _shipPlacementStatus) {
        if ([_gameData, _board, _shipPlacementStatus].includes(undefined)) {
            throw Error("Object has undefined fields!");
        }
        this.GameData = _gameData;
        this.Board = _board;
        this.ShipPlacementStatus = _shipPlacementStatus;
    }
    static mapJsonToObject(json) {
        let obj = JSON.parse(json);
        return new GameView(obj.GameData, obj.Board, obj.ShipPlacementStatus);
    }
}
//# sourceMappingURL=GameView.js.map