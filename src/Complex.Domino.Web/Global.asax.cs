using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.UI;
using System.Reflection;
using Complex.Domino.Plugins;

namespace Complex.Domino.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            ScriptManager.ScriptResourceMapping.AddDefinition("jquery",
                new ScriptResourceDefinition
                {
                    Path = "~/Scripts/jquery-1.8.0.min.js",
                    DebugPath = "~/Scripts/jquery-1.8.0.js",
                    CdnPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.0.min.js",
                    CdnDebugPath = "http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.8.0.js"
                });

            ScriptManager.ScriptResourceMapping.AddDefinition("jquery-validation",
                new ScriptResourceDefinition
                {
                    Path = "~/Scripts/jquery.validate.min.js",
                    DebugPath = "~/Scripts/jquery.validate.js",
                    CdnPath = "http://ajax.aspnetcdn.com/ajax/jquery.validate/1.13.1/jquery.validate.min.js",
                    CdnDebugPath = "http://ajax.aspnetcdn.com/ajax/jquery.validate/1.13.1/jquery.validate.js"
                });

            ScriptManager.ScriptResourceMapping.AddDefinition("jquery-validation-unobtrusive",
                new ScriptResourceDefinition
                {
                    Path = "~/Scripts/jquery.validate.unobtrusive.min.js",
                    DebugPath = "~/Scripts/jquery.validate.unobtrusive.js",
                });

            ScriptManager.ScriptResourceMapping.AddDefinition("jquery-ui",
                new ScriptResourceDefinition
                {
                    Path = "~/Scripts/jquery-ui-1.11.2/jquery-ui.min.js",
                    DebugPath = "~/Scripts/jquery-ui-1.11.2/jquery-ui.js",
                    CdnPath = "http://ajax.aspnetcdn.com/ajax/jquery.ui/1.11.2/jquery-ui.min.js",
                    CdnDebugPath = "http://ajax.aspnetcdn.com/ajax/jquery.ui/1.11.2/jquery-ui.js",
                });

            var pf = PluginManager.Create(null);
            pf.RegisterPlugins();

            CleanUpScratch(null);

            Application[Constants.ApplicationDominoVersion] = typeof(Lib.Course).Assembly.GetName().Version.ToString();
            Application[Constants.ApplicationCopyright] = typeof(Lib.Course).Assembly.GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright;
            
            var git = new Complex.Domino.Git.Git();
            Application[Constants.ApplicationGitVersion] = git.GetVersion();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session[Constants.SessionGuid] = Guid.NewGuid().ToString("D");
            CleanUpScratch((string)Session[Constants.SessionGuid]);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {
            CleanUpScratch((string)Session[Constants.SessionGuid]);
        }

        protected void Application_End(object sender, EventArgs e)
        {
            CleanUpScratch(null);
        }

        private void CleanUpScratch(string subdir)
        {
            // Clean up scratch repos
            var scratchdir = Lib.DominoConfiguration.Instance.ScratchPath;

            if (subdir != null)
            {
                scratchdir = System.IO.Path.Combine(scratchdir, subdir);
            }

            try
            {
                Complex.Domino.Util.IO.ForceEmptyDirectory(scratchdir);
            }
            catch (Exception)
            {
                // TODO!
            }
        }
    }
}