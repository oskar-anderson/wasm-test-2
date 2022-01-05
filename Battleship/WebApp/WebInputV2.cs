using Domain.Model;
using Game;

namespace WebApp
{
    public class WebInputV2: BaseInputV2
    {
        public Input NewInput;

        public WebInputV2(Input newInput)
        {
            this.NewInput = newInput;
        }
        
        public override Input UpdateInput(Input oldInput)
        {
            return this.NewInput;
        }
    }
}