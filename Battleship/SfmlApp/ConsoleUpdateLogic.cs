using Game;
using SFML.Graphics;

namespace SfmlApp;

public class ConsoleUpdateLogic : UpdateLogic
{
    private readonly RenderWindow Window;

    public ConsoleUpdateLogic(RenderWindow window)
    {
        Window = window;
    }
    
    public override bool Update(double deltaTime, BaseBattleship basegame)
    {
        basegame.GameData.Input = new ConsoleInput(Window).UpdateInput(basegame.GameData.Input);
        return base.Update(deltaTime, basegame);
    }
}