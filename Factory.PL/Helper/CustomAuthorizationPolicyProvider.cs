using Microsoft.AspNetCore.Authorization;


namespace Factory.PL.Helper
{
    public class CustomAuthorizationPolicyProvider : IAuthorizationPolicyProvider
    {
        private readonly Dictionary<string, AuthorizationPolicy> _policies = new Dictionary<string, AuthorizationPolicy>();

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => Task.FromResult(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build());

        public Task<AuthorizationPolicy> GetFallbackPolicyAsync() => Task.FromResult<AuthorizationPolicy>(null);

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (_policies.TryGetValue(policyName, out var policy))
            {
                return Task.FromResult(policy);
            }

            return Task.FromResult<AuthorizationPolicy>(null);
        }

        public void AddPolicy(string policyName, AuthorizationPolicy policy)
        {
            _policies[policyName] = policy;
        }
    }
}