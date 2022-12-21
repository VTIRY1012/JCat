using JCat.Client.Model;
using Microsoft.AspNetCore.Http;

namespace JCat.Client;
public interface IJClient
{
    public Task<JClientResponse<T>> GetAsync<T>(JClientRequest request);
    public Task<JClientResponse<T>> PostAsync<T>(JClientRequest request);
    public Task<JClientResponse<T>> PutAsync<T>(JClientRequest request);
    public Task<JClientResponse<T>> DeleteAsync<T>(JClientRequest request);
    public Task<JClientResponse<T>> PostFileAsync<T>(JClientRequest request);
    public Task<JClientResponse<T>> PostFileAsync<T>(JClientRequest request, (string, IFormFile) file);
    public Task<JClientResponse<T>> PutFileAsync<T>(JClientRequest request);
    public Task<JClientResponse<T>> PutFileAsync<T>(JClientRequest request, (string, IFormFile) file);
    public Task<JClientResponse<T>> SendAsync<T>(JClientRequest request);
}