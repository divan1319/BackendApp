using BackendApp.Controllers;

namespace BackendApp.Services
{
    public interface IPeopleService
    {
        bool Validate(People people);
    }
}
