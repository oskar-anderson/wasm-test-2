import {Input} from "./Input.js";

export default class GameData {
    public ActivePlayer: object;
    public InactivePlayer: object;
    public Sprites: object[];
    public AllowedPlacementType: number;
    public ShipSizes: object[];
    public State: object;
    public FrameCount: number;
    public DeltaTimes: number[];
    public ElapsedTime: number;
    public Input: Input;
    
    public constructor(
        activePlayer: object, 
        inactivePlayer: object, 
        sprites: object[], 
        allowedPlacementType: number, 
        ShipSizes: object[], 
        state: object, 
        frameCount: number, 
        deltaTimes: number[], 
        elapsedTime: number, 
        input: Input
    ) {
        this.ActivePlayer = activePlayer;
        this.InactivePlayer = inactivePlayer;
        this.Sprites = sprites;
        this.AllowedPlacementType = allowedPlacementType;
        this.ShipSizes = ShipSizes;
        this.State = state;
        this.FrameCount = frameCount;
        this.DeltaTimes = deltaTimes;
        this.ElapsedTime = elapsedTime;
        this.Input = input;
    }
}