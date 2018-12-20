using System.Collections.Generic;
using Dapplo.ActiveDirectory.Entities;
using Dapplo.Config;

namespace Dapplo.ActiveDirectory.Finder.Entities.Impl
{
    public class User : ConfigurationBase<IUser>, IUser
    {
        #region Implementation of IAdObject

        public string Id { get; set; }

        #endregion

        #region Implementation of IUser

        public string AgentId { get; set; }
        public string Displayname { get; set; }
        public string Firstname { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string Department { get; set; }
        public string TeamKz { get; set; }
        public byte[] ThumbnailBytes { get; set; }

        #endregion
    }
}
