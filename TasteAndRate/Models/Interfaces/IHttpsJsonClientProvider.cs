using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TasteAndRate.DTO;


namespace TasteAndRate.Interfaces
{
    public interface IHttpsJsonClientProvider<T>
    {
        Task<IEnumerable<T>> GetAsync(string api_url);
        Task<T?> GetByIdAsync(string path, string id);
        Task<T?> PostAsync(string path, T data);
        Task<T?> PutAsync(string path, T data);
        Task<T?> PatchAsync(string path, T data);

        Task<bool> DeleteAsync(string path, string id);

        Task Authenticate(string path, HttpClient httpClient, HttpResponseMessage request);
        Task<T?> LoginPostAsync(string path, LoginDTO data);
        Task<T?> RegisterPostAsync(string path, RegisterDTO data);
    }
}
