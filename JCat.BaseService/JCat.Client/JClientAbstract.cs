using JCat.BaseService;
using JCat.BaseService.Extensions.BaseType;
using JCat.Client.Const;
using JCat.Client.Extensions;
using JCat.Client.Interface;
using JCat.Client.Model;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Web;

namespace JCat.Client;
public abstract class JClientAbstract : IJClient
{
    private readonly HttpClient _client;
    private readonly IAppKeyEncoder _encoder;
    private readonly IJClientSettings _settings;
    private readonly string _userTokenHeader;
    private readonly string _url;
    public JClientAbstract(
        IHttpClientFactory httpClientFactory,
        IAppKeyEncoder encoder,
        IJClientSettings settings)
    {
        _client = httpClientFactory.CreateClient();
        _encoder = encoder;
        _settings = settings;
        EnsureSettingsHasValues();
        SetApplicationKey();
        _userTokenHeader = "Authorization";
        _url = _settings.BaseUrl;
    }

    #region Validate
    private void EnsureSettingsHasValues()
    {
        if (string.IsNullOrWhiteSpace(_settings.BaseUrl))
        {
            throw new ArgumentNullException(nameof(_settings.BaseUrl));
        }
        if (string.IsNullOrWhiteSpace(_settings.ApplicationKey))
        {
            throw new ArgumentNullException(nameof(_settings.ApplicationKey));
        }
    }

    private void EnsureRequestHasValues(JClientRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(JClientRequest));
        }
    }
    #endregion

    #region PreSend
    private void SetApplicationKey()
    {
        var key = _encoder.EncodeApplicationKey(_settings.ApplicationKey);
        _client.DefaultRequestHeaders.Add(nameof(_settings.ApplicationKey), key);
    }

    private void AddMediaTypeJson()
    {
        var mediaType = new MediaTypeWithQualityHeaderValue(ContentTypeConst.json);
        if (!_client.DefaultRequestHeaders.Accept.Contains(mediaType))
        {
            _client.DefaultRequestHeaders.Accept.Add(mediaType);
        }
    }

    private void AddMediaTypeFormData()
    {
        var mediaType = new MediaTypeWithQualityHeaderValue(ContentTypeConst.formdata);
        if (!_client.DefaultRequestHeaders.Accept.Contains(mediaType))
        {
            _client.DefaultRequestHeaders.Accept.Remove(mediaType);
        }
    }

    private void AddUserToken(JClientRequest request)
    {
        request.Headers.TryGetValue(_userTokenHeader, out string token);
        if (token.IsNullOrWhiteSpace())
        {
            return;
        }

        if (_client.DefaultRequestHeaders.Contains(_userTokenHeader))
        {
            return;
        }

        _client.DefaultRequestHeaders.TryAddWithoutValidation(_userTokenHeader, token);
    }

    private void AddFromApplication()
    {
        var isFromAppPropName = nameof(JHeader.D_IsFromApplication).GetDisplayName<JHeader>();
        if (_client.DefaultRequestHeaders.Contains(isFromAppPropName))
        {
            _client.DefaultRequestHeaders.Remove(isFromAppPropName);
        }
        _client.DefaultRequestHeaders.TryAddWithoutValidation(isFromAppPropName, "True");
    }

    private async Task RequestHeader(JClientRequest request)
    {
        EnsureRequestHasValues(request);

        _client.DefaultRequestHeaders.Accept.Clear();
        AddMediaTypeJson();

        if (request.Headers != null)
        {
            foreach (var header in request.Headers)
            {
                if (header.Key == _userTokenHeader)
                {
                    continue;
                }

                if (!_client.DefaultRequestHeaders.Contains(header.Key) && header.Value != null)
                {
                    _client.DefaultRequestHeaders.TryAddWithoutValidation(header.Key, header.Value);
                }
            }

            AddUserToken(request);
        }

        AddFromApplication();
        await Task.CompletedTask;
    }

    private async Task RequestFromForm()
    {
        _client.DefaultRequestHeaders.Accept.Clear();
        AddMediaTypeFormData();
        await Task.CompletedTask;
    }

    private async Task<string> RequestQueryString(Uri uri, JClientRequest request)
    {
        if (request?.QueryString == null)
        {
            return uri.ToString();
        }

        var uriBuilder = new UriBuilder(uri);
        var query = HttpUtility.ParseQueryString(uriBuilder.Query);

        foreach (var queryItem in request.QueryString)
        {
            query[queryItem.Key] = queryItem.Value;
        }
        uriBuilder.Query = query.ToString();
        return await Task.FromResult(uriBuilder.ToString());
    }
    #endregion

    #region Base
    private async Task<HttpResponseMessage> SendByBody(Uri uri, HttpMethod httpMethod, object body)
    {
        HttpRequestMessage request = new HttpRequestMessage
        {
            Content = body.ToStringContent(),
            Method = httpMethod,
            RequestUri = uri
        };
        return await _client.SendAsync(request);
    }

    private async Task<JClientResponse<T>> BaseResponse<T>(HttpResponseMessage response)
    {
        var result = new JClientResponse<T>()
        {
            IsSuccessStatusCode = response.IsSuccessStatusCode,
            StatusCode = response.StatusCode,
            EventId = response.Headers.GetEventId(),
            Version = response.Headers.GetVersion(),
            IsFromCache = response.Headers.GetIsFromCache(),
            CacheKey = response.Headers.GetCacheKey()
        };
        var content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            result.ErrorResponse = JsonSerializer.Deserialize<ErrorServiceClientResponse>(content, _settings.JsonSerializerOptions);
            return result;
        }
        result.Data = JsonSerializer.Deserialize<T>(content, _settings.JsonSerializerOptions);
        return result;
    }
    #endregion

    public virtual async Task<JClientResponse<T>> GetAsync<T>(JClientRequest request)
    {
        await RequestHeader(request);
        var uri = new Uri($"{_url}{request.AdditionalUrl}/{request.MethodName}");
        var url = await RequestQueryString(uri, request);
        var response = await _client.GetAsync(url);
        var result = await BaseResponse<T>(response);
        return result;
    }

    public virtual async Task<JClientResponse<T>> PostAsync<T>(JClientRequest request)
    {
        await RequestHeader(request);
        var uri = new Uri($"{_url}{request.AdditionalUrl}/{request.MethodName}");
        var requestContent = request?.Body?.ToStringContent();
        var response = await _client.PostAsync(uri, requestContent);
        var result = await BaseResponse<T>(response);
        return result;
    }

    public virtual async Task<JClientResponse<T>> PostFileAsync<T>(JClientRequest request)
    {
        await RequestHeader(request);
        await RequestFromForm();
        var uri = new Uri($"{_url}{request.AdditionalUrl}/{request.MethodName}");
        using var requestContent = new MultipartFormDataContent();
        requestContent.FormContent(request.FormValues);
        var memoryStreams = requestContent.FileToContent(request.FileCollection);
        var response = await _client.PostAsync(uri, requestContent);
        memoryStreams.MemoryStreamDispose();
        var result = await BaseResponse<T>(response);
        return result;
    }

    public virtual async Task<JClientResponse<T>> PostFileAsync<T>(JClientRequest request, (string, IFormFile) file)
    {
        await RequestHeader(request);
        await RequestFromForm();
        var uri = new Uri($"{_url}{request.AdditionalUrl}/{request.MethodName}");
        using var requestContent = new MultipartFormDataContent();
        requestContent.FormContent(request.FormValues);
        var memoryStream = requestContent.FileToContent(file.Item1, file.Item2);
        var response = await _client.PostAsync(uri, requestContent);
        memoryStream.MemoryStreamDispose();
        var result = await BaseResponse<T>(response);
        return result;
    }

    public virtual async Task<JClientResponse<T>> PutFileAsync<T>(JClientRequest request)
    {
        await RequestHeader(request);
        await RequestFromForm();
        var uri = new Uri($"{_url}{request.AdditionalUrl}/{request.MethodName}");
        using var requestContent = new MultipartFormDataContent();
        requestContent.FormContent(request.FormValues);
        var memoryStreams = requestContent.FileToContent(request.FileCollection);
        var response = await _client.PutAsync(uri, requestContent);
        memoryStreams.MemoryStreamDispose();
        var result = await BaseResponse<T>(response);
        return result;
    }

    public virtual async Task<JClientResponse<T>> PutFileAsync<T>(JClientRequest request, (string, IFormFile) file)
    {
        await RequestHeader(request);
        await RequestFromForm();
        var uri = new Uri($"{_url}{request.AdditionalUrl}/{request.MethodName}");
        using var requestContent = new MultipartFormDataContent();
        requestContent.FormContent(request.FormValues);
        var memoryStream = requestContent.FileToContent(file.Item1, file.Item2);
        var response = await _client.PutAsync(uri, requestContent);
        memoryStream.MemoryStreamDispose();
        var result = await BaseResponse<T>(response);
        return result;
    }

    public virtual async Task<JClientResponse<T>> PutAsync<T>(JClientRequest request)
    {
        await RequestHeader(request);
        var uri = new Uri($"{_url}{request.AdditionalUrl}/{request.MethodName}");
        var requestContent = request?.Body?.ToStringContent();
        var response = await _client.PutAsync(uri, requestContent);
        var result = await BaseResponse<T>(response);
        return result;
    }

    public virtual async Task<JClientResponse<T>> DeleteAsync<T>(JClientRequest request)
    {
        await RequestHeader(request);
        var uri = new Uri($"{_url}{request.AdditionalUrl}/{request.MethodName}");
        var response = await SendByBody(uri, HttpMethod.Delete, request.Body);
        var result = await BaseResponse<T>(response);
        return result;
    }

    public virtual async Task<JClientResponse<T>> SendAsync<T>(JClientRequest request)
    {
        await RequestHeader(request);
        var uri = new Uri($"{_url}{request.AdditionalUrl}/{request.MethodName}");
        var url = await RequestQueryString(uri, request);
        var response = await SendByBody(new Uri(url), request.HttpMethod, request.Body);
        var result = await BaseResponse<T>(response);
        return result;
    }
}