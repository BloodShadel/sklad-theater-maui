using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

#if ANDROID
using Xamarin.Android.Net;
#elif IOS || MACCATALYST
using System.Net.Http;
#endif

namespace SkladTheater.Maui.Services;

public static class HttpClientHandlerProvider
{
    public static HttpMessageHandler Create()
    {
#if ANDROID
        return new AndroidMessageHandler
        {
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true
        };
#elif IOS || MACCATALYST
        return new NSUrlSessionHandler
        {
            TrustOverrideForUrl = (handler, url, trust) => true
        };
#else
        return new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (sender, cert, chain, errors) => true
        };
#endif
    }
}
