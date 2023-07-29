using System;

namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Clients.SpotifyClient.Types.Exceptions;

public class BadOrExpiredTokenException : Exception
{
    public BadOrExpiredTokenException()
    {
    }
}