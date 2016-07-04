using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Interface.Presentation.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //SCRIPTS

            bundles.Add(new ScriptBundle("~/bundles/validations-form").Include(
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.unobtrusive.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/application").Include(
                        "~/Scripts/highcharts/4.2.0/highcharts.js",
                        "~/Scripts/Custom/model/bug-tracker-model.js",
                        "~/Scripts/Custom/view/bug-tracker-view.js"));

            bundles.Add(new ScriptBundle("~/bundles/home").Include(
                        "~/Scripts/Custom/application/delete.js"));

            bundles.Add(new ScriptBundle("~/bundles/account").Include(
                        "~/Scripts/Custom/User/account.js"));

            bundles.Add(new ScriptBundle("~/bundles/slider-sl").Include(
            "~/Scripts/modernizr-2.6.2.js",
            "~/Scripts/jquery.ba-cond.min.js",
            "~/Scripts/jquery.slitslider.js",
            "~/Scripts/Custom/SliderSL/SliderSL.js"));


            bundles.Add(new ScriptBundle("~/bundles/layout").Include(
                        "~/Scripts/jquery-1.10.2.js",
                        "~/Scripts/modernizr-2.6.2.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/Custom/layout/layout-nav.js"));

            bundles.Add(new ScriptBundle("~/bundles/layout-home").Include(
                        "~/Scripts/jquery-1.10.2.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/Custom/layout/layoutHome.js"));

            //CSS

            bundles.Add(new StyleBundle("~/Content/layout-home").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/Custom/layout-home.css"));

            bundles.Add(new StyleBundle("~/Content/slideSL").Include(
                        "~/Content/Custom/sl-slide.css"));

            bundles.Add(new StyleBundle("~/Content/documentation").Include(
                        "~/Content/Custom/documentation.css"));

            bundles.Add(new StyleBundle("~/Content/forms").Include(
                       "~/Content/Custom/forms.css"));

            bundles.Add(new StyleBundle("~/Content/layout").Include(
                        "~/Content/bootstrap-sandstone.css",
                        "~/Content/Custom/layout.css",
                        "~/Content/Custom/new-home.css"));
        }
    }
}