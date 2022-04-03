using System.Collections.Generic;
using System.Linq;
using Domain.Model;
using Game;
using RogueSharp;
using SFML.Graphics;
using SFML.Window;

namespace Sdl2App
{
    public class ConsoleInput
    {
        private readonly RenderWindow window;
        public ConsoleInput(RenderWindow window)
        {
            this.window = window;
        }

        public List<Input.BtnState> GetKeyState(Keyboard.Key key, string identifierKey, Input? oldInput)
        {
            var keyValues = new List<Input.BtnState>();
            if (GetKey(key))
            {
                keyValues.Add(Input.BtnState.Pressed);
                if (oldInput != null && 
                    oldInput.Keyboard.KeyboardState
                        .Any(x => x.Identifier.Key == identifierKey && 
                                  x.Values.Contains(Input.BtnState.Pressed))
                    )
                {
                    keyValues.Add(Input.BtnState.Echo);
                }
            }
            else
            {
                keyValues.Add(Input.BtnState.Released);
            }

            return keyValues;
        }

        public Input.MouseInput GetMouseState(Input? oldInput)
        {
            List<Input.BtnState> GetMouseButtonState(bool isPressed, bool isEcho)
            {
                var btn = new List<Input.BtnState>();
                if (isPressed)
                {
                    btn.Add(Input.BtnState.Pressed);
                    if (isEcho)
                    {
                        btn.Add(Input.BtnState.Echo);
                    }
                }
                else
                {
                    btn.Add(Input.BtnState.Released);
                }

                return btn;
            };
            
            var leftButton = GetMouseButtonState(
                SFML.Window.Mouse.IsButtonPressed(Mouse.Button.Left),
                oldInput != null && oldInput.Mouse.LeftButton.Contains(Input.BtnState.Pressed)
            );
            var middleButton = GetMouseButtonState(
                SFML.Window.Mouse.IsButtonPressed(Mouse.Button.Middle),
                oldInput != null && oldInput.Mouse.LeftButton.Contains(Input.BtnState.Pressed)
            );
            var rightButton = GetMouseButtonState(
                SFML.Window.Mouse.IsButtonPressed(Mouse.Button.Right),
                oldInput != null && oldInput.Mouse.LeftButton.Contains(Input.BtnState.Pressed)
            );

            Point p = new Point()
            {
                X = SFML.Window.Mouse.GetPosition(window).X,
                Y = SFML.Window.Mouse.GetPosition(window).Y
            };
            var mousePos = new RogueSharp.Point(p.X, p.Y);
            
            
            
            return new Input.MouseInput()
            {
                LeftButton = leftButton,
                MiddleButton = middleButton,
                RightButton = rightButton,
                ScrollWheel = 0,
                X = mousePos.X,
                Y = mousePos.Y
            };
        }

        public Input UpdateInput(Input? oldInput)
        {
            var result = new Input()
            {
                Keyboard = new Input.KeyboardInput()
                {
                    KeyboardState = new List<Input.KeyboardInput.KeyboardKey>()
                    {
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.KeyR,
                            Values = GetKeyState(Keyboard.Key.R, Input.KeyboardInput.KeyboardIdentifierList.KeyR.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.KeyX,
                            Values = GetKeyState(Keyboard.Key.X, Input.KeyboardInput.KeyboardIdentifierList.KeyX.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.Escape,
                            Values = GetKeyState(Keyboard.Key.Escape, Input.KeyboardInput.KeyboardIdentifierList.Escape.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.KeyC,
                            Values = GetKeyState(Keyboard.Key.C, Input.KeyboardInput.KeyboardIdentifierList.KeyC.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.KeyZ,
                            Values = GetKeyState(Keyboard.Key.Z, Input.KeyboardInput.KeyboardIdentifierList.KeyZ.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.Digit1,
                            Values = GetKeyState(Keyboard.Key.Num1, Input.KeyboardInput.KeyboardIdentifierList.Digit1.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.Digit2,
                            Values = GetKeyState(Keyboard.Key.Num2, Input.KeyboardInput.KeyboardIdentifierList.Digit2.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.Digit3,
                            Values = GetKeyState(Keyboard.Key.Num3, Input.KeyboardInput.KeyboardIdentifierList.Digit3.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.KeyA,
                            Values = GetKeyState(Keyboard.Key.A, Input.KeyboardInput.KeyboardIdentifierList.KeyA.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.KeyW,
                            Values = GetKeyState(Keyboard.Key.W, Input.KeyboardInput.KeyboardIdentifierList.KeyW.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.KeyD,
                            Values = GetKeyState(Keyboard.Key.D, Input.KeyboardInput.KeyboardIdentifierList.KeyD.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.KeyS,
                            Values = GetKeyState(Keyboard.Key.S, Input.KeyboardInput.KeyboardIdentifierList.KeyS.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.ArrowLeft,
                            Values = GetKeyState(Keyboard.Key.Left, Input.KeyboardInput.KeyboardIdentifierList.ArrowLeft.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.ArrowUp,
                            Values = GetKeyState(Keyboard.Key.Up, Input.KeyboardInput.KeyboardIdentifierList.ArrowUp.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.ArrowRight,
                            Values = GetKeyState(Keyboard.Key.Right, Input.KeyboardInput.KeyboardIdentifierList.ArrowRight.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.ArrowDown,
                            Values = GetKeyState(Keyboard.Key.Down, Input.KeyboardInput.KeyboardIdentifierList.ArrowDown.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.KeyJ,
                            Values = GetKeyState(Keyboard.Key.J, Input.KeyboardInput.KeyboardIdentifierList.KeyJ.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.KeyI,
                            Values = GetKeyState(Keyboard.Key.I, Input.KeyboardInput.KeyboardIdentifierList.KeyI.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.KeyL,
                            Values = GetKeyState(Keyboard.Key.L, Input.KeyboardInput.KeyboardIdentifierList.KeyL.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.KeyK,
                            Values = GetKeyState(Keyboard.Key.K, Input.KeyboardInput.KeyboardIdentifierList.KeyK.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.Slash,
                            Values = GetKeyState(Keyboard.Key.Slash, Input.KeyboardInput.KeyboardIdentifierList.Slash.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.Period,
                            Values = GetKeyState(Keyboard.Key.Period, Input.KeyboardInput.KeyboardIdentifierList.Period.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.Comma,
                            Values = GetKeyState(Keyboard.Key.Comma, Input.KeyboardInput.KeyboardIdentifierList.Comma.Key, oldInput),
                        },
                    }
                },
                Mouse = GetMouseState(oldInput)
            };
            return result;
        }

        private bool GetKey(Keyboard.Key key)
        {
            return SFML.Window.Keyboard.IsKeyPressed(key);
        }
    }
}