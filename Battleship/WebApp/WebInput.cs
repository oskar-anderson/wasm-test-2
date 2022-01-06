using Domain.Model;
using Game;

namespace WebApp
{
    public class WebInput: BaseInput
    {
        public Input NewInput;

        public WebInput(Input newInput)
        {
            this.NewInput = newInput;
        }
        
        public Input UpdateInput(Input oldInput)
        {
            return this.NewInput;
        }
    }
}