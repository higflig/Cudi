using System.Linq;
using System.Collections.Generic;
using Discord.WebSocket;

namespace Cudi.Profiles
{
    public class GuildProfiles
    {
        private static List<GuildProfile> profiles;
        private static readonly string filePath = "Resources/guild_profiles.json";

        static GuildProfiles()
        {
            if (ProfileStorage.SaveExists(filePath))
            {
                profiles = ProfileStorage.LoadProfiles<GuildProfile>(filePath).ToList();
            }
            else
            {
                profiles = new List<GuildProfile>();
                Save();
            }
        }

        public static void Save()
        {
            ProfileStorage.SaveProfiles<GuildProfile>(profiles, filePath);
        }

        public static GuildProfile Get(SocketGuild guild)
        {
            var profile = profiles.SingleOrDefault(x => x.GuildID == x.GuildID);
            if (profile == null)
            {
                profile = Create(guild);
            }
            return profile;
        }

        private static GuildProfile Create(SocketGuild guild)
        {
            var newProfile = new GuildProfile()
            {
                GuildID = guild.Id,
                Prefix = "."
            };

            profiles.Add(newProfile);
            Save();
            return newProfile;
        }

        public static IList<GuildProfile> GetAllProfiles()
        {
            return profiles;
        }
    }

    public class GuildProfile
    {
        public ulong GuildID { get; set; }

        public string Prefix { get; set; }
    }
}