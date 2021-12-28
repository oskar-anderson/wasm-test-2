export default class GameSettings {
    BoardHeight: number;
    BoardWidth: number;
    AllowedPlacementType: number;
    Ships: string;
    
    constructor(_boardHeight: number, _boardWidth: number, _allowedPlacementType: number, _ships: string) {
        this.BoardHeight = _boardHeight;
        this.BoardWidth = _boardWidth;
        this.AllowedPlacementType = _allowedPlacementType;
        this.Ships = _ships;
    }
}