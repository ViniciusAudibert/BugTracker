using BugTracker.Domain.Service;
using BugTracker.Infra.Repository.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Interface.Presentation.Services
{
    public static class ActivationServiceInjection
    {
        public static ActivationService Create()
        {
            return new ActivationService(new ActivationRepository());
        }
    }
}