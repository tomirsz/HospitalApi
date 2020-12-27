using Microsoft.AspNetCore.Authorization;


namespace HospitalApi.Models
{
    public class Policies
    {
        public const string ADMIN = "ADMIN";
        public const string NURSE = "NURSE";
        public const string DOCTOR = "DOCTOR";
        public const string USER = "USER";

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
                .RequireRole(NURSE, DOCTOR, USER)
                .Build();
        }
    }
}