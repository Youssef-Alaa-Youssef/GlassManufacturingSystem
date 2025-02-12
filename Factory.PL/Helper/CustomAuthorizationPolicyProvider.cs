using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace Factory.PL.Helper
{
    public class CustomAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        private readonly Dictionary<string, AuthorizationPolicy> _policies = new Dictionary<string, AuthorizationPolicy>();

        public CustomAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
        {
        }

        public void AddPolicy(string name, AuthorizationPolicy policy)
        {
            _policies[name] = policy;
        }

        public override Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (_policies.TryGetValue(policyName, out var policy))
            {
                return Task.FromResult(policy);
            }

            return base.GetPolicyAsync(policyName);
        }
    }
}