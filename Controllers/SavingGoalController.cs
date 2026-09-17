using FinSteady_API.Models;
using FinSteady_API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace FinSteady_API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SavingGoalController : ControllerBase
    {
        private readonly ISavingGoalRepository savingGoalRepository;
        protected APIResponse _response;
        public SavingGoalController(ISavingGoalRepository savingGoal)
        {
            this.savingGoalRepository = savingGoal;
            _response = new();
        }


    }

}
