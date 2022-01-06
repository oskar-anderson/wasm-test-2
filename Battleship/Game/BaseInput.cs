using Domain.Model;

namespace Game
{
    public interface BaseInput
    {
        Input UpdateInput(Input oldInput);

    }
}