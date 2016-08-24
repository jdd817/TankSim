using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Tank.Web.App_Start.BundleSetup), "Start")]

namespace Tank.Web.App_Start
{
    public class BundleSetup
    {
        public static void Start()
        {
            BundleTable.Bundles.Add(new ScriptBundle("~/jslibs")
                .Include("~/Scripts/jquery-1.10.2.min.js")
                .Include("~/Scripts/angular.js")
                .Include("~/Scripts/flot/jquery.flot.js")
                .Include("~/Scripts/angular-flot.js")
                .Include("~/Scripts/angular-ui/ui-bootstrap-tpls.min.js")
                .Include("~/Scripts/modernizr-2.6.2.js")
                .Include("~/Scripts/angular-cookies.min.js")
                .Include("~/Scripts/angularLocalStorage.js")
                );

            BundleTable.Bundles.Add(new ScriptBundle("~/jsapp")
                .IncludeDirectory("~/app", "*.js", true)
                );

            BundleTable.EnableOptimizations = false;

        }
    }
}