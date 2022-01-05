using System.Collections.Generic;

namespace Domain.Model
{
    public class Input
    {
        public KeyboardInput Keyboard { get; set; } = null!;
        public MouseInput Mouse { get; set; } = null!;

        public static Input GetDefaultInput()
        {
            return new Input()
            {
                Keyboard = new KeyboardInput()
                {
                    KeyboardState = new List<KeyboardInput.KeyboardKey>()
                    {
                        new KeyboardInput.KeyboardKey()
                        {
                            Identifier = KeyboardInput.KeyboardIdentifierList.KeyR,
                            Values = new List<BtnState>()
                        },
                        new KeyboardInput.KeyboardKey()
                        {
                            Identifier = KeyboardInput.KeyboardIdentifierList.KeyX,
                            Values = new List<BtnState>()
                        },
                        new KeyboardInput.KeyboardKey()
                        {
                            Identifier = KeyboardInput.KeyboardIdentifierList.Escape,
                            Values = new List<BtnState>()
                        },
                        new KeyboardInput.KeyboardKey()
                        {
                            Identifier = KeyboardInput.KeyboardIdentifierList.KeyC,
                            Values = new List<BtnState>()
                        },
                        new KeyboardInput.KeyboardKey()
                        {
                            Identifier = KeyboardInput.KeyboardIdentifierList.KeyZ,
                            Values = new List<BtnState>()
                        },
                        new KeyboardInput.KeyboardKey()
                        {
                            Identifier = KeyboardInput.KeyboardIdentifierList.Digit1,
                            Values = new List<BtnState>()
                        },
                        new KeyboardInput.KeyboardKey()
                        {
                            Identifier = KeyboardInput.KeyboardIdentifierList.Digit2,
                            Values = new List<BtnState>()
                        },
                        new KeyboardInput.KeyboardKey()
                        {
                            Identifier = KeyboardInput.KeyboardIdentifierList.Digit3,
                            Values = new List<BtnState>()
                        },
                        new KeyboardInput.KeyboardKey()
                        {
                            Identifier = KeyboardInput.KeyboardIdentifierList.KeyA,
                            Values = new List<BtnState>()
                        },
                        new KeyboardInput.KeyboardKey()
                        {
                            Identifier = KeyboardInput.KeyboardIdentifierList.KeyW,
                            Values = new List<BtnState>()
                        },
                        new KeyboardInput.KeyboardKey()
                        {
                            Identifier = KeyboardInput.KeyboardIdentifierList.KeyD,
                            Values = new List<BtnState>()
                        },
                        new KeyboardInput.KeyboardKey()
                        {
                            Identifier = KeyboardInput.KeyboardIdentifierList.KeyS,
                            Values = new List<BtnState>()
                        },
                        new KeyboardInput.KeyboardKey()
                        {
                            Identifier = KeyboardInput.KeyboardIdentifierList.ArrowLeft,
                            Values = new List<BtnState>()
                        },
                        new KeyboardInput.KeyboardKey()
                        {
                            Identifier = KeyboardInput.KeyboardIdentifierList.ArrowUp,
                            Values = new List<BtnState>()
                        },
                        new KeyboardInput.KeyboardKey()
                        {
                            Identifier = KeyboardInput.KeyboardIdentifierList.ArrowRight,
                            Values = new List<BtnState>()
                        },
                        new KeyboardInput.KeyboardKey()
                        {
                            Identifier = KeyboardInput.KeyboardIdentifierList.ArrowDown,
                            Values = new List<BtnState>()
                        },
                        new KeyboardInput.KeyboardKey()
                        {
                            Identifier = KeyboardInput.KeyboardIdentifierList.KeyJ,
                            Values = new List<BtnState>()
                        },
                        new KeyboardInput.KeyboardKey()
                        {
                            Identifier = KeyboardInput.KeyboardIdentifierList.KeyI,
                            Values = new List<BtnState>()
                        },
                        new KeyboardInput.KeyboardKey()
                        {
                            Identifier = KeyboardInput.KeyboardIdentifierList.KeyL,
                            Values = new List<BtnState>()
                        },
                        new KeyboardInput.KeyboardKey()
                        {
                            Identifier = KeyboardInput.KeyboardIdentifierList.KeyK,
                            Values = new List<BtnState>()
                        },
                        new KeyboardInput.KeyboardKey()
                        {
                            Identifier = KeyboardInput.KeyboardIdentifierList.Slash,
                            Values = new List<BtnState>()
                        },
                        new KeyboardInput.KeyboardKey()
                        {
                            Identifier = KeyboardInput.KeyboardIdentifierList.Period,
                            Values = new List<BtnState>()
                        },
                        new KeyboardInput.KeyboardKey()
                        {
                            Identifier = KeyboardInput.KeyboardIdentifierList.Comma,
                            Values = new List<BtnState>()
                        },
                    }
                },
                Mouse =
                {
                    LeftButton = new List<BtnState>(),
                    MiddleButton = new List<BtnState>(),
                    RightButton = new List<BtnState>(),
                    ScrollWheel = 0,
                    X = 0,
                    Y = 0
                }
            };

        }
        
        public class KeyboardInput
        {
            public List<KeyboardKey> KeyboardState { get; set; } = null!;
            
            public class KeyboardKey
            {
                public KeyboardIdentifier Identifier { get; set; } = null!;
                public List<BtnState> Values { get; set; } = null!;
            }
    
            public class KeyboardIdentifier
            {
                public int Unicode { get; set; } = 0;
                public string Key { get; set; } = null!;
            }

            public static class KeyboardIdentifierList
            {
                public static readonly KeyboardIdentifier KeyR = new Input.KeyboardInput.KeyboardIdentifier()
                {
                    Key = "KeyR",
                    Unicode = 82,
                };
                public static readonly KeyboardIdentifier KeyX = new Input.KeyboardInput.KeyboardIdentifier()
                {
                    Key = "KeyX",
                    Unicode = 88,
                };
                public static readonly KeyboardIdentifier Escape = new Input.KeyboardInput.KeyboardIdentifier()
                {
                    Key = "Escape",
                    Unicode = 27,
                };
                public static readonly KeyboardIdentifier KeyC = new Input.KeyboardInput.KeyboardIdentifier()
                {
                    Key = "KeyC",
                    Unicode = 67,
                };
                public static readonly KeyboardIdentifier KeyZ = new Input.KeyboardInput.KeyboardIdentifier()
                {
                    Key = "KeyZ",
                    Unicode = 90,
                };
                
                public static readonly KeyboardIdentifier Digit1 = new Input.KeyboardInput.KeyboardIdentifier()
                {
                    Key = "Digit1",
                    Unicode = 49,
                };
                public static readonly KeyboardIdentifier Digit2 = new Input.KeyboardInput.KeyboardIdentifier()
                {
                    Key = "Digit2",
                    Unicode = 50,
                };
                public static readonly KeyboardIdentifier Digit3 = new Input.KeyboardInput.KeyboardIdentifier()
                {
                    Key = "Digit3",
                    Unicode = 51,
                };
                        
                public static readonly KeyboardIdentifier KeyA = new Input.KeyboardInput.KeyboardIdentifier()
                {
                    Key = "KeyA",
                    Unicode = 65,
                };
                public static readonly KeyboardIdentifier KeyW = new Input.KeyboardInput.KeyboardIdentifier()
                {
                    Key = "KeyW",
                    Unicode = 87,
                };
                public static readonly KeyboardIdentifier KeyD = new Input.KeyboardInput.KeyboardIdentifier()
                {
                    Key = "KeyD",
                    Unicode = 68,
                };
                public static readonly KeyboardIdentifier KeyS = new Input.KeyboardInput.KeyboardIdentifier()
                {
                    Key = "KeyS",
                    Unicode = 83,
                };
                
                public static readonly KeyboardIdentifier ArrowLeft = new Input.KeyboardInput.KeyboardIdentifier()
                {
                    Key = "ArrowLeft",
                    Unicode = 37,
                };
                public static readonly KeyboardIdentifier ArrowUp = new Input.KeyboardInput.KeyboardIdentifier()
                {
                    Key = "ArrowUp",
                    Unicode = 38,
                };
                public static readonly KeyboardIdentifier ArrowRight = new Input.KeyboardInput.KeyboardIdentifier()
                {
                    Key = "ArrowRight",
                    Unicode = 39,
                };
                public static readonly KeyboardIdentifier ArrowDown = new Input.KeyboardInput.KeyboardIdentifier()
                {
                    Key = "ArrowDown",
                    Unicode = 40,
                };

                public static readonly KeyboardIdentifier KeyJ = new Input.KeyboardInput.KeyboardIdentifier()
                {
                    Key = "KeyJ",
                    Unicode = 74,
                };
                public static readonly KeyboardIdentifier KeyI = new Input.KeyboardInput.KeyboardIdentifier()
                {
                    Key = "KeyI",
                    Unicode = 73,
                };
                public static readonly KeyboardIdentifier KeyL = new Input.KeyboardInput.KeyboardIdentifier()
                {
                    Key = "KeyL",
                    Unicode = 76,
                };
                public static readonly KeyboardIdentifier KeyK = new Input.KeyboardInput.KeyboardIdentifier()
                {
                    Key = "KeyK",
                    Unicode = 75,
                };
                   
                public static readonly KeyboardIdentifier Slash = new Input.KeyboardInput.KeyboardIdentifier()
                {
                    Key = "Slash",
                    Unicode = 173,
                };
                public static readonly KeyboardIdentifier Period = new Input.KeyboardInput.KeyboardIdentifier()
                {
                    Key = ".",
                    Unicode = 190,
                };
                public static readonly KeyboardIdentifier Comma = new Input.KeyboardInput.KeyboardIdentifier()
                {
                    Key = ",",
                    Unicode = 188,
                };

            }
        }

        public class MouseInput
        {
            public int X { get; set; } = 0;
            public int Y { get; set; } = 0;
            public int ScrollWheel { get; set; } = 0;
            public List<BtnState> LeftButton { get; set; } = null!;
            public List<BtnState> MiddleButton { get; set; } = null!;
            public List<BtnState> RightButton { get; set; } = null!;
        }

        public enum BtnState
        {
            Pressed,
            Echo,
            Released
        }
    }
}