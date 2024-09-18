using ExploreAPIs.API.Modals.Domain;
using System.Net;

namespace ExploreAPIs.API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
