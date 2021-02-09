using System;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

namespace warmup_project_teama_web_app.Models.Session
{
    /// <summary>
    /// Extensions of the Sessions class to handle saving objects.
    /// </summary>
    public static class SessionExtensions
    {
        /// <summary>
        /// Saves object in session.
        /// </summary>
        /// <typeparam name="T">Object type to save</typeparam>
        /// <param name="session">Session variable of the HTTP context</param>
        /// <param name="key">Name of the variable to save</param>
        /// <param name="value">Actual object to save</param>
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        /// <summary>
        /// Gets object from session.
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="session">Session variable of the HTTP context</param>
        /// <param name="key">Name of the variable to get</param>
        /// <returns>The object saved at Session[key], or default otherwise</returns>
        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}
