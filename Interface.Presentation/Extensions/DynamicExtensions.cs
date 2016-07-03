using Interface.Presentation.Models.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Interface.Presentation.Extensions
{
    public static class DynamicExtensions
    {
        public static List<ApplicationAndBugsViewModel> toApplicationAndBugsViewModel(this IEnumerable<dynamic> apps)
        {
            var appViewModel = new List<ApplicationAndBugsViewModel>();

            foreach (var app in apps)
            {
                appViewModel.Add(
                    new ApplicationAndBugsViewModel(
                        app.GetType().GetProperty("AppId").GetValue(app, null),
                        app.GetType().GetProperty("AppName").GetValue(app, null),
                        app.GetType().GetProperty("AppImage").GetValue(app, null),
                        app.GetType().GetProperty("LastTrack").GetValue(app, null),
                        app.GetType().GetProperty("TracksCountError").GetValue(app, null),
                        app.GetType().GetProperty("TracksCountWarning").GetValue(app, null)
                    )
                 );
            }

            return appViewModel;
        }
    }
}