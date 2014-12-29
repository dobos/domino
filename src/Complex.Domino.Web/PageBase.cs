﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Complex.Domino.Lib;

namespace Complex.Domino.Web
{
    public class PageBase : Page
    {
        private bool bypassAuthentication;
        private Context databaseContext;

        protected Context DatabaseContext
        {
            get
            {
                EnsureContextExists();

                return databaseContext;
            }
        }

        public string OriginalReferer
        {
            get { return (string)(ViewState[Constants.OriginalReferer] ?? String.Empty); }
            private set { ViewState[Constants.OriginalReferer] = value; }
        }

        /// <summary>
        /// Gets the root URL of the current web application
        /// </summary>
        public string BaseUrl
        {
            get { return Util.UrlFormatter.ToBaseUrl(Request.Url.AbsoluteUri, Request.ApplicationPath); }
        }

        /// <summary>
        /// Gets the return url from the query string of the request
        /// </summary>
        public string ReturnUrl
        {
            get { return Request.QueryString[Constants.ReturnUrl] ?? ""; }
        }

        protected void BypassAuthentication()
        {
            this.bypassAuthentication = true;
        }

        protected void SetUser(User u)
        {
            DatabaseContext.User = u;

            Session[Constants.SessionUserID] = u.ID;
            Session[Constants.SessionUsername] = u.Username;
        }

        protected void GetUser()
        {
            // TODO
            throw new NotImplementedException();
        }

        protected void ResetUser()
        {
            DatabaseContext.User = null;

            Session[Constants.SessionUserID] = null;
            Session[Constants.SessionUsername] = null;
        }

        private void EnsureContextExists()
        {
            if (databaseContext == null)
            {
                databaseContext = Complex.Domino.Lib.Context.Create();

                databaseContext.User = new User(databaseContext)
                {
                    ID = (int)(Session[Constants.SessionUserID] ?? -1),
                    Username = (string)Session[Constants.SessionUsername]
                };
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (!IsPostBack && Request.UrlReferrer != null)
            {
                OriginalReferer = Request.UrlReferrer.ToString();
            }

            if (!bypassAuthentication)
            {
                // If the user hold a valid cookie but the session is new
                // we need to look up user details from the database
                if (this.User.Identity.IsAuthenticated && Session[Constants.SessionUserID] == null)
                {
                    var u = new User(DatabaseContext);
                    u.Load(this.User.Identity.Name);

                    if (!u.Enabled)
                    {
                        throw Lib.Error.InvalidUsernameOrPassword();
                    }

                    SetUser(u);
                }
            }

            base.OnLoad(e);
        }

        protected override void OnError(EventArgs e)
        {
            var ex = Server.GetLastError();

#if DEBUG
            System.Diagnostics.Debugger.Break();
#endif

            // Save exception to session for future use
            Session[Constants.SessionException] = ex;

            if (databaseContext != null)
            {
                databaseContext.RollbackTransaction();
            }

            // Server.ClearError();

            base.OnError(e);
        }

        protected override void OnUnload(EventArgs e)
        {
            if (databaseContext != null)
            {
                databaseContext.CommitTransaction();
                databaseContext.Dispose();
                databaseContext = null;
            }

            base.OnUnload(e);
        }
    }
}