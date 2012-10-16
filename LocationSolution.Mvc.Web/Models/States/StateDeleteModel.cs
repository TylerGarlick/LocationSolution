using LocationSolution.Data.Core;

namespace LocationSolution.Mvc.Web.Models.States
{
    public class StateDeleteModel
    {
        public State State { get; set; }

        public StateDeleteModel()
        {

        }

        public StateDeleteModel(State state)
        {
            State = state;
        }
    }
}