using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using ConsoleGameEngineCore;
using Domain;
using Domain.Model;
using Game;

namespace ConsoleApp
{
    public class ConsoleInput: BaseInput
    {
        private readonly ConsoleEngine engine;
        public ConsoleInput(ConsoleEngine engine)
        {
            this.engine = engine;
        }

        public List<Input.BtnState> GetKeyState(ConsoleKey ck, string identifierKey, Input? oldInput)
        {
            var keyValues = new List<Input.BtnState>();
            if (GetKey(ck))
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
                engine.GetMouseLeft(),
                oldInput != null && oldInput.Mouse.LeftButton.Contains(Input.BtnState.Pressed)
            );
            var middleButton = GetMouseButtonState(
                engine.GetMouseLeft(),
                oldInput != null && oldInput.Mouse.LeftButton.Contains(Input.BtnState.Pressed)
            );
            var rightButton = GetMouseButtonState(
                engine.GetMouseLeft(),
                oldInput != null && oldInput.Mouse.LeftButton.Contains(Input.BtnState.Pressed)
            );

            Point p = engine.GetMousePos();
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
                            Values = GetKeyState(ConsoleKey.R, Input.KeyboardInput.KeyboardIdentifierList.KeyR.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.KeyX,
                            Values = GetKeyState(ConsoleKey.X, Input.KeyboardInput.KeyboardIdentifierList.KeyX.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.Escape,
                            Values = GetKeyState(ConsoleKey.Escape, Input.KeyboardInput.KeyboardIdentifierList.Escape.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.KeyC,
                            Values = GetKeyState(ConsoleKey.C, Input.KeyboardInput.KeyboardIdentifierList.KeyC.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.KeyZ,
                            Values = GetKeyState(ConsoleKey.Z, Input.KeyboardInput.KeyboardIdentifierList.KeyZ.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.Digit1,
                            Values = GetKeyState(ConsoleKey.D1, Input.KeyboardInput.KeyboardIdentifierList.Digit1.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.Digit2,
                            Values = GetKeyState(ConsoleKey.D2, Input.KeyboardInput.KeyboardIdentifierList.Digit2.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.Digit3,
                            Values = GetKeyState(ConsoleKey.D3, Input.KeyboardInput.KeyboardIdentifierList.Digit3.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.KeyA,
                            Values = GetKeyState(ConsoleKey.A, Input.KeyboardInput.KeyboardIdentifierList.KeyA.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.KeyW,
                            Values = GetKeyState(ConsoleKey.W, Input.KeyboardInput.KeyboardIdentifierList.KeyW.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.KeyD,
                            Values = GetKeyState(ConsoleKey.D, Input.KeyboardInput.KeyboardIdentifierList.KeyD.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.KeyS,
                            Values = GetKeyState(ConsoleKey.S, Input.KeyboardInput.KeyboardIdentifierList.KeyS.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.ArrowLeft,
                            Values = GetKeyState(ConsoleKey.LeftArrow, Input.KeyboardInput.KeyboardIdentifierList.ArrowLeft.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.ArrowUp,
                            Values = GetKeyState(ConsoleKey.UpArrow, Input.KeyboardInput.KeyboardIdentifierList.ArrowUp.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.ArrowRight,
                            Values = GetKeyState(ConsoleKey.RightArrow, Input.KeyboardInput.KeyboardIdentifierList.ArrowRight.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.ArrowDown,
                            Values = GetKeyState(ConsoleKey.DownArrow, Input.KeyboardInput.KeyboardIdentifierList.ArrowDown.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.KeyJ,
                            Values = GetKeyState(ConsoleKey.J, Input.KeyboardInput.KeyboardIdentifierList.KeyJ.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.KeyI,
                            Values = GetKeyState(ConsoleKey.I, Input.KeyboardInput.KeyboardIdentifierList.KeyI.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.KeyL,
                            Values = GetKeyState(ConsoleKey.L, Input.KeyboardInput.KeyboardIdentifierList.KeyL.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.KeyK,
                            Values = GetKeyState(ConsoleKey.K, Input.KeyboardInput.KeyboardIdentifierList.KeyK.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.Slash,
                            Values = GetKeyState(ConsoleKey.OemMinus, Input.KeyboardInput.KeyboardIdentifierList.Slash.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.Period,
                            Values = GetKeyState(ConsoleKey.OemPeriod, Input.KeyboardInput.KeyboardIdentifierList.Period.Key, oldInput),
                        },
                        new Input.KeyboardInput.KeyboardKey()
                        {
                            Identifier = Input.KeyboardInput.KeyboardIdentifierList.Comma,
                            Values = GetKeyState(ConsoleKey.OemComma, Input.KeyboardInput.KeyboardIdentifierList.Comma.Key, oldInput),
                        },
                    }
                },
                Mouse = GetMouseState(oldInput)
            };
            return result;
        }
        
        [DllImport("user32.dll", SetLastError = true)]
        private static extern short GetAsyncKeyState(int vKey);

        private bool GetKey(ConsoleKey key)
        {
            if (false)
            {
                return engine.GetKey((ConsoleKey) (int) key);
            }
            short s = GetAsyncKeyState((int) key);
            return (s & 0x8000) > 0;
        }
    }
}