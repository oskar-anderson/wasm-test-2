using Domain.Model;
using Game;
using SfmlApp;

namespace WebApp;

public class WebUpdateLogic : UpdateLogic
{
    private readonly Input Input;

    public WebUpdateLogic(Input input)
    {
        Input = input;
    }
    
    public override bool Update(double deltaTime, BaseBattleship basegame)
    {
        basegame.GameData.Input = Input;
        return base.Update(deltaTime, basegame);
    }
}
