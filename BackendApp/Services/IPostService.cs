using BackendApp.DTOs;

namespace BackendApp.Services
{
    public interface IPostService
    {
        public Task<IEnumerable<PostDto>> Get();
    }
}
