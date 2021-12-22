﻿

using TaskoMask.Application.Share.Helpers;

namespace TaskoMask.Presentation.Framework.Share.Services.Http
{
    public  interface IHttpClientServices
    {
        Task<Result<T>> PostAsync<T>(Uri uri, object input);
        Task<Result<T>> PutAsync<T>(Uri uri, object input);
        Task<Result<T>> PutAsync<T>(Uri uri);
        Task<Result<T>> GetAsync<T>(Uri uri);
        Uri GetBaseAddress();
        void SetBaseAddress(string httpClientBaseAddress);  

    }
}
