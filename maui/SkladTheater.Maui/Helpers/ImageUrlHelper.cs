namespace SkladTheater.Maui.Helpers;

public static class ImageUrlHelper
{
    public static string Resolve(string? relativeOrAbsoluteUrl)
    {
        if (string.IsNullOrWhiteSpace(relativeOrAbsoluteUrl))
            return string.Empty;

        if (relativeOrAbsoluteUrl.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
            relativeOrAbsoluteUrl.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
            return relativeOrAbsoluteUrl;

        var baseUrl = AppConfig.BaseUrl.TrimEnd('/');
        var path = relativeOrAbsoluteUrl.StartsWith("/") ? relativeOrAbsoluteUrl : "/" + relativeOrAbsoluteUrl;
        return baseUrl + path;
    }
}
