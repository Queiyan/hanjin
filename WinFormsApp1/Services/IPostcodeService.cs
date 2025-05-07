using System.Threading.Tasks;

namespace WinFormsApp1.Services
{
    public interface IPostcodeService
    {
        Task<(string zipCode, string address)> SearchAddressAsync(string query);
        bool ValidateSearchQuery(string query, out string sanitizedQuery);
    }
} 