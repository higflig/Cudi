using System;
using System.Linq;
using System.Collections.Generic;
using Discord.WebSocket;

namespace Cudi.Profiles
{
    public class UserProfiles
    {
        private static List<UserProfile> profiles;
        private static readonly string filePath = "Resources/user_profiles.json";

        static UserProfiles()
        {
            if (ProfileStorage.SaveExists(filePath))
            {
                profiles = ProfileStorage.LoadProfiles<UserProfile>(filePath).ToList();
            }
            else
            {
                profiles = new List<UserProfile>();
                Save();
            }
        }

        public static void Save()
        {
            ProfileStorage.SaveProfiles<UserProfile>(profiles, filePath);
        }

        public static UserProfile Get(SocketGuildUser user)
        {
            if (user.IsBot || user.IsWebhook) return null;
            
            var profile = profiles.SingleOrDefault(x => x.UserID == user.Id && x.GuildID == x.GuildID);
            if (profile == null)
            {
                profile = Create(user);
            }
            return profile;
        }

        private static UserProfile Create(SocketGuildUser user)
        {
            var newProfile = new UserProfile()
            {
                UserID = user.Id,
                GuildID = user.Guild.Id
            };

            profiles.Add(newProfile);
            Save();
            return newProfile;
        }

        public static IList<UserProfile> GetAllProfiles()
        {
            return profiles;
        }
    }

    public class UserProfile
    {
        public ulong UserID { get; set; }

        public ulong GuildID { get; set; }

        public int Checks { get; set; }

        public DateTime LastMessage { get; set; } = DateTime.UtcNow;
    }
}