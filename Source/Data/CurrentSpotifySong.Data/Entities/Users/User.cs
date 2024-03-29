﻿using MongoDB.Bson;

namespace Torty.Web.Apps.CurrentSpotifySong.Data.Entities.Users;

public record User
{
    /// <summary>
    /// The unique MongoDb ID for this User
    /// </summary>
    public ObjectId Id;

    /// <summary>
    /// An Access Token that should be provided in API calls
    /// made to Spotify when accessing data about this user
    /// </summary>
    public string AccessToken;

    /// <summary>
    /// The token that should be sent to the Spotify Api to
    /// acquire a new AccessToken when the current one expired
    /// </summary>
    public string RefreshToken;

    /// <summary>
    /// The DateTime the user last accessed their Spotify Information
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This is not exposed beyond the Domain layer
    ///     </para>
    ///     <para>
    ///         This is retained purely for administrative purposes
    ///     </para>
    /// </remarks>
    public DateTime LastAccessedDateTime;
}