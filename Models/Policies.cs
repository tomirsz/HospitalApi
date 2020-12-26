using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace HospitalApi.Models
{
    public class Policies
    {
        public const string ADMIN = "ADMIN";
        public const string NURSE = "NURSE";
        public const string DOCTOR = "DOCTOR";


        public static AuthorizationPolicy AdminPolicy()
        {
            return new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireRole(ADMIN)
                .Build();
        }

        public static AuthorizationPolicy UserPolicy()
        {
            return new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .RequireRole(NURSE, DOCTOR)
                .Build();
        }
    }
}