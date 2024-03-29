﻿using MongoDB.Bson;

namespace Torty.Web.Apps.CurrentSpotifySong.Data.Entities.Users;

public record UnauthenticatedUser
{
    /// <summary>
    /// The unique MongoDb ID for this Unauthenticated user
    /// </summary>
    public ObjectId Id;
    
    /// <summary>
    /// How long this Unauthenticated User entry is valid for
    /// </summary>
    /// <remarks>
    /// If the current date time exceeds this expiry date
    /// time this user cannot authorize themselves with
    /// our app without restarting the registration workflow
    /// </remarks>
    public DateTime ExpireDateTime;
}