
using System;
using System.Net;

namespace Torty.Web.Apps.CurrentSpotifySong.Infrastructure.Exceptions;

public class WebException : Exception
{
    public readonly HttpStatusCode ResponseCode;
    public new readonly string Message;

    public WebException(HttpStatusCode responseCode, string message)
    {
        ResponseCode = responseCode;
        Message = message;
    }
}