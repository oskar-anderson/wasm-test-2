export default class GameSettings {

    constructor(_boardHeight, _boardWidth, _allowedPlacementType, _ships) {
        this.BoardHeight = _boardHeight;
        this.BoardWidth = _boardWidth;
        this.AllowedPlacementType = _allowedPlacementType;
        this.Ships = _ships;
    }
}