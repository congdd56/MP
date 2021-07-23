using VAS.Dealer.Models.CRM;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VAS.Dealer.Provider
{
    public class CCEvents : Hub
    {
        private static readonly ConcurrentDictionary<string, User> Users =
               new ConcurrentDictionary<string, User>(StringComparer.InvariantCultureIgnoreCase);

        private readonly MM_Context _MM_Context;
        private readonly ILogger<CCEvents> _logger;
        public CCEvents(MM_Context MM_Context, ILogger<CCEvents> logger)
        {
            _MM_Context = MM_Context;
            _logger = logger;
        }
        #region Methods
        /// <summary>
        /// Provides the handler for SignalR OnConnected event
        /// supports async threading
        /// </summary>
        /// <returns></returns>
        public override Task OnConnectedAsync()
        {
            try
            {
                var claimsIdentity = Context.User.Identity as System.Security.Claims.ClaimsIdentity;
                var currentUser = JsonConvert.DeserializeObject<UserLogonModel>(claimsIdentity.FindFirst("UserData").Value);

                string connectionId = Context.ConnectionId;
                var user = Users.GetOrAdd(currentUser.UserName, _ => new User
                {
                    ProfileId = currentUser.UserName,
                    ConnectionIds = new HashSet<string>(),
                    DateConnect = DateTime.Now
                });

                lock (user.ConnectionIds)
                {
                    user.ConnectionIds.Add(connectionId);

                    Groups.AddToGroupAsync(connectionId, currentUser.UserName);

                    #region + Gắn group role
                    if (currentUser.RoleCodes != null)
                    {
                        currentUser.RoleCodes.ForEach(x =>
                        {
                            Groups.AddToGroupAsync(connectionId, x);
                        });
                    }
                    #endregion

                    #region + Thêm bản ghi Active user
                    _MM_Context.ActiveUser.Add(new MM_ActiveUser()
                    {
                        UserName = currentUser.UserName
                    });
                    _MM_Context.SaveChanges();
                    #endregion

                    return base.OnConnectedAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message, null);
            }
            return base.OnConnectedAsync();
        }

        /// <summary>
        /// Provides the handler for SignalR OnDisconnected event
        /// supports async threading
        /// </summary>
        /// <returns></returns>
        public override Task OnDisconnectedAsync(Exception exception)
        {
            try
            {
                string profileId = Context.User == null ? "111" : Context.User.Identity.Name;

                var claimsIdentity = Context.User.Identity as System.Security.Claims.ClaimsIdentity;
                var currentUser = JsonConvert.DeserializeObject<UserLogonModel>(claimsIdentity.FindFirst("UserData").Value);

                string connectionId = Context.ConnectionId;
                DateTime DateCurrent = DateTime.Now;
                Users.TryGetValue(profileId, out User user);
                if (user != null)
                {
                    user.DateConnect = DateCurrent;
                    lock (user.ConnectionIds)
                    {
                        user.ConnectionIds.RemoveWhere(cid => cid.Equals(connectionId));
                        Groups.RemoveFromGroupAsync(connectionId, user.ProfileId);
                        if (!user.ConnectionIds.Any())
                        {
                            Users.TryRemove(profileId, out User removedUser);
                        }

                        #region + Remove group role
                        if (currentUser.RoleCodes != null)
                        {
                            currentUser.RoleCodes.ForEach(x =>
                            {
                                Groups.RemoveFromGroupAsync(connectionId, x);
                            });
                        }
                        #endregion

                        #region + Xóa bản ghi Active user
                        var itemActive = _MM_Context.ActiveUser.Where(x => x.UserName == currentUser.UserName).ToList();
                        if (itemActive != null && itemActive.Count > 0)
                        {
                            _MM_Context.ActiveUser.RemoveRange(itemActive);
                            _MM_Context.SaveChanges();
                        }
                        #endregion
                    }
                }
            }
            catch (Exception)
            {
            }

            return base.OnDisconnectedAsync(exception);
        }

        /// <summary>
        /// Provides the facility to send individual user notification message
        /// </summary>
        /// <param name="profileId">
        /// Set to the ProfileId of user who will receive the notification
        /// </param>
        /// <param name="message">
        /// set to the notification message
        /// </param>
        public void Send(string profileId, string message)
        {
            Clients.User(profileId).SendAsync("ReceiveMessage", message);
        }


        /// <summary>
        /// Provides the facility to send group notification message
        /// set to the user groupd name who will receive the message
        /// </summary>
        /// <param name="groupName"></param>
        /// <param name="message"></param>
        public void SendUserMessage(String groupName, String message)
        {
            Clients.Group(groupName).SendAsync(message);
        }

        public void RecieveAgentInCall(String groupName, String message)
        {
            Clients.Group(groupName).SendAsync("ReceiveMessage", message);
        }

        /// <summary>
        /// Provides the ability to get User from the dictionary for passed in profileId
        /// </summary>
        /// <param name="profileId">
        /// set to the profileId of user that need to be fetched from the dictionary
        /// </param>
        /// <returns>
        /// return User object if found otherwise returns null
        /// </returns>
        public User GetUser(string profileId)
        {
            Users.TryGetValue(profileId, out User user);
            return user;
        }

        /// <summary>
        /// Provide theability to get currently connected user
        /// </summary>
        /// <returns>
        /// profileId of user based on current connectionId
        /// </returns>
        public IEnumerable<string> GetConnectedUser()
        {
            return Users.Where(x =>
            {
                lock (x.Value.ConnectionIds)
                {
                    return !x.Value.ConnectionIds.Contains(Context.ConnectionId, StringComparer.InvariantCultureIgnoreCase);
                }
            }).Select(x => x.Key);
        }

        public IEnumerable<string> GetAllConnectedUser()
        {
            return Users.Where(u => !string.IsNullOrEmpty(u.Key)).GroupBy(g => g.Key).Select(x => x.Key);
        }

        #endregion

        //string UserId = string.Empty;
        //string WorkGroup = string.Empty;
        //string UserIdService = string.Empty;
        //string WorkGroupService = string.Empty;
    }

    public class User
    {
        #region Constructor
        public User()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #endregion


        #region Properties

        /// <summary>
        /// Property to get/set ProfileId
        /// </summary>
        public string ProfileId
        {
            get;
            set;
        }
        public DateTime DateConnect
        {
            get;
            set;
        }
        /// <summary>
        /// Propoerty to get/set multiple ConnectionId
        /// </summary>
        public HashSet<string> ConnectionIds
        {
            get;
            set;
        }
        public string Extension { get; set; }

        #endregion
    }
}
