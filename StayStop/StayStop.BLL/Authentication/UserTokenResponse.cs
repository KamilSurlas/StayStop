namespace StayStop.BLL.Authentication
{
    public record UserTokenResponse(string AccessToken, string RefreshToken)
    {
        public string AccessToken { get; set; } = AccessToken;
        public string RefreshToken { get; set; } = RefreshToken;
    }
}
