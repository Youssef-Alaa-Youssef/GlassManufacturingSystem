using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using System.Collections.Concurrent;

namespace Factory.PL.Helper
{
    public class CustomAuthorizationPolicyProvider : IAuthorizationPolicyProvider
    {
        private readonly ConcurrentDictionary<string, AuthorizationPolicy> _policies = new();
        private readonly DefaultAuthorizationPolicyProvider _defaultProvider;

        public CustomAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            _defaultProvider = new DefaultAuthorizationPolicyProvider(options);
        }

        /// <summary>
        /// Gets the default authorization policy.
        /// </summary>
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            return _defaultProvider.GetDefaultPolicyAsync();
        }

        /// <summary>
        /// Gets the fallback authorization policy.
        /// </summary>
        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
        {
            return _defaultProvider.GetFallbackPolicyAsync();
        }

        /// <summary>
        /// Gets the authorization policy for the specified policy name.
        /// </summary>
        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            // Check if the policy exists in the custom dictionary
            if (_policies.TryGetValue(policyName, out var policy))
            {
                return Task.FromResult<AuthorizationPolicy?>(policy);
            }

            // Fall back to the default provider if the policy is not found
            return _defaultProvider.GetPolicyAsync(policyName);
        }

        /// <summary>
        /// Adds a custom authorization policy to the provider.
        /// </summary>
        public void AddPolicy(string policyName, AuthorizationPolicy policy)
        {
            _policies[policyName] = policy;
        }
    }
}
