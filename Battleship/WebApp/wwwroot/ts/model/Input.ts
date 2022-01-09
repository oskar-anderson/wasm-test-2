export class Input {
    public Keyboard: KeyboardInput;
    public Mouse: MouseInput;
    
    constructor(keyboard: KeyboardInput, mouse: MouseInput) {
        this.Keyboard = keyboard;
        this.Mouse = mouse;
    }
}

export class KeyboardInput {
    KeyboardState: KeyboardKey[];

    constructor(KeyboardState: KeyboardKey[]) {
        this.KeyboardState = KeyboardState;
    }
}

export class MouseInput {
    LeftButton: BtnState[];
    MiddleButton: BtnState[];
    RightButton: BtnState[];
    ScrollWheel: number;
    X: number;
    Y: number;
    
    constructor(leftButton: BtnState[], middleButton: BtnState[], rightButton: BtnState[], scrollWheel: number, x: number, y: number) {
        this.LeftButton = leftButton;
        this.MiddleButton = middleButton;
        this.RightButton = rightButton;
        this.ScrollWheel = scrollWheel;
        this.X = x;
        this.Y = y;
    }
}

export enum BtnState {
    Pressed,
    Echo,
    Released
}

export class KeyboardIdentifier {
    Unicode: number;
    Key: string;

    constructor(unicode: number, key:string) {
        this.Unicode = unicode;
        this.Key = key;
    }
}

export class KeyboardKey {
    Identifier: KeyboardIdentifier;
    Values: BtnState[];

    constructor(identifier: KeyboardIdentifier, values:BtnState[]) {
        this.Identifier = identifier;
        this.Values = values;
    }
}

export class KeyboardIdentifierList {
    public static readonly KeyR: KeyboardIdentifier = new KeyboardIdentifier(82, "KeyR");
    public static readonly KeyX: KeyboardIdentifier = new KeyboardIdentifier(88, "KeyX");
    public static readonly Escape: KeyboardIdentifier = new KeyboardIdentifier(27, "Escape");
    public static readonly KeyC: KeyboardIdentifier = new KeyboardIdentifier(67, "KeyC");
    public static readonly KeyZ: KeyboardIdentifier = new KeyboardIdentifier(90, "KeyZ");
    public static readonly Digit1: KeyboardIdentifier = new KeyboardIdentifier(49, "Digit1");
    public static readonly Digit2: KeyboardIdentifier = new KeyboardIdentifier(50, "Digit2");
    public static readonly Digit3: KeyboardIdentifier = new KeyboardIdentifier(51, "Digit3");

    public static readonly KeyA: KeyboardIdentifier = new KeyboardIdentifier(65, "KeyA");
    public static readonly KeyW: KeyboardIdentifier = new KeyboardIdentifier(87, "KeyW");
    public static readonly KeyD: KeyboardIdentifier = new KeyboardIdentifier(68, "KeyD");
    public static readonly KeyS: KeyboardIdentifier = new KeyboardIdentifier(83, "KeyS");

    public static readonly ArrowLeft: KeyboardIdentifier = new KeyboardIdentifier(37, "ArrowLeft");
    public static readonly ArrowUp: KeyboardIdentifier = new KeyboardIdentifier(38, "ArrowUp");
    public static readonly ArrowRight: KeyboardIdentifier = new KeyboardIdentifier(39, "ArrowRight");
    public static readonly ArrowDown: KeyboardIdentifier = new KeyboardIdentifier(40, "ArrowDown");

    public static readonly KeyJ: KeyboardIdentifier = new KeyboardIdentifier(74, "KeyJ");
    public static readonly KeyI: KeyboardIdentifier = new KeyboardIdentifier(73, "KeyI");
    public static readonly KeyL: KeyboardIdentifier = new KeyboardIdentifier(76, "KeyL");
    public static readonly KeyK: KeyboardIdentifier = new KeyboardIdentifier(27, "KeyK");

    public static readonly Slash: KeyboardIdentifier = new KeyboardIdentifier(173, "Slash");
    public static readonly Period: KeyboardIdentifier = new KeyboardIdentifier(190, ".");
    public static readonly Comma: KeyboardIdentifier = new KeyboardIdentifier(188, ",");

    public static getDefaultInput() {
        return new Input(
            new KeyboardInput(
                [
                    new KeyboardKey(KeyboardIdentifierList.KeyR, []),
                    new KeyboardKey(KeyboardIdentifierList.KeyX, []),
                    new KeyboardKey(KeyboardIdentifierList.Escape, []),
                    new KeyboardKey(KeyboardIdentifierList.KeyC, []),
                    new KeyboardKey(KeyboardIdentifierList.KeyZ, []),

                    new KeyboardKey(KeyboardIdentifierList.Digit1, []),
                    new KeyboardKey(KeyboardIdentifierList.Digit2, []),
                    new KeyboardKey(KeyboardIdentifierList.Digit3, []),

                    new KeyboardKey(KeyboardIdentifierList.ArrowLeft, []),
                    new KeyboardKey(KeyboardIdentifierList.ArrowUp, []),
                    new KeyboardKey(KeyboardIdentifierList.ArrowRight, []),
                    new KeyboardKey(KeyboardIdentifierList.ArrowDown, []),

                    new KeyboardKey(KeyboardIdentifierList.KeyJ, []),
                    new KeyboardKey(KeyboardIdentifierList.KeyI, []),
                    new KeyboardKey(KeyboardIdentifierList.KeyL, []),
                    new KeyboardKey(KeyboardIdentifierList.KeyK, []),

                    new KeyboardKey(KeyboardIdentifierList.Slash, []),
                    new KeyboardKey(KeyboardIdentifierList.Period, []),
                    new KeyboardKey(KeyboardIdentifierList.Comma, []),

                ]),
            new MouseInput([], [], [], 0, 0, 0)
        );
    }
}