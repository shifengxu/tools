using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Client
{
    public class Program
    {
        private static async Task Main()
        {
            // Step 1: discover endpoints from metadata
            var disco = await Step1Discover();
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            // Step 2: request token
            var tokenResponse = await Step2RequestToken(disco.TokenEndpoint);
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }
            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");

            // Step 3: call api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);
            // The tokenResponse.AccessToken is just the "access_token" in the JSON resposne of step 2.

            var response = await apiClient.GetAsync("http://localhost:9000/identity");
            /* It will make HTTP request to API server: 
             * GET: http://localhost:9000/identity
             * But, if the above request is the first request of the API server (namely the API server
             * has not received any request before), the API server will also make such request to IdentityServer:
             * GET: http://localhost:5000/.well-known/openid-configuration
             * GET: http://localhost:5000/.well-known/openid-configuration/jwks
             */
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
                return;
            }
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(JArray.Parse(content));
        }

        /* It will make 2 HTTP requests:
         * GET: http://localhost:5000/.well-known/openid-configuration
         * Response JSON ======================================================================
{
    "issuer": "http://localhost:5000",
    "jwks_uri": "http://localhost:5000/.well-known/openid-configuration/jwks",
    "authorization_endpoint": "http://localhost:5000/connect/authorize",
    "token_endpoint": "http://localhost:5000/connect/token",
    "userinfo_endpoint": "http://localhost:5000/connect/userinfo",
    "end_session_endpoint": "http://localhost:5000/connect/endsession",
    "check_session_iframe": "http://localhost:5000/connect/checksession",
    "revocation_endpoint": "http://localhost:5000/connect/revocation",
    "introspection_endpoint": "http://localhost:5000/connect/introspect",
    "device_authorization_endpoint": "http://localhost:5000/connect/deviceauthorization",
    "frontchannel_logout_supported": true,
    "frontchannel_logout_session_supported": true,
    "backchannel_logout_supported": true,
    "backchannel_logout_session_supported": true,
    "scopes_supported": [
        "openid",
        "api1",
        "offline_access"
    ],
    "claims_supported": [
        "sub"
    ],
    "grant_types_supported": [
        "authorization_code",
        "client_credentials",
        "refresh_token",
        "implicit",
        "password",
        "urn:ietf:params:oauth:grant-type:device_code"
    ],
    "response_types_supported": [
        "code",
        "token",
        "id_token",
        "id_token token",
        "code id_token",
        "code token",
        "code id_token token"
    ],
    "response_modes_supported": [
        "form_post",
        "query",
        "fragment"
    ],
    "token_endpoint_auth_methods_supported": [
        "client_secret_basic",
        "client_secret_post"
    ],
    "subject_types_supported": [
        "public"
    ],
    "id_token_signing_alg_values_supported": [
        "RS256"
    ],
    "code_challenge_methods_supported": [
        "plain",
        "S256"
    ]
}
         * 
         * GET: http://localhost:5000/.well-known/openid-configuration/jwks
         * Response JSON ======================================================================
{
    "keys": [
        {
            "kty": "RSA",
            "use": "sig",
            "kid": "2ef32e460d32e6c50e2946397646f4bc",
            "e": "AQAB",
            "n": "zS6yZvIiWCjhiVPMalv0AF57LrrcqAJSiujSGXu8GS-RcqymaUeqWrOIIoYGmu5bz-wtJ0VNb7j6WavHEZDfSGyVmzHIok1Nn--sCVoSsdDz6juZvnAHEi0RDcpfw47mBqJX3yMBSZGuJ-JfRj6T7UVmnyFLoO8fVQZu4DP2ax5-q7eUk8WmsjSyvWYzIrnHzzohc-eVo9wbD2qp0PxNtwqXw_oq8BN_2pM5OzSahn5LY-nEEmD-n_exDiwlTXOdfrm5UGJdzU8AgUl-qUYZFW40f0Zx4TaDGRJSKX7SHEt57-v7HeOxwW9dMYmIspupJGi-OVe9RJlxD_Ezf80p_Q",
            "alg": "RS256"
        }
    ]
}
         *
         */
        private static async Task<DiscoveryResponse> Step1Discover()
        {
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
            return disco;
        }

        /* It will make HTTP request:
         * POST: http://localhost:5000/connect/token
         * request body: scope=api1&grant_type=client_credentials&client_id=client&client_secret=secret
         * Response JSON ======================================================================
{
    "access_token": "eyJhbGciOiJSUzI1NiIsImtpZCI6IjJlZjMyZTQ2MGQzMmU2YzUwZTI5NDYzOTc2NDZmNGJjIiwidHlwIjoiSldUIn0.eyJuYmYiOjE1OTExMDgwMDksImV4cCI6MTU5MTExMTYwOSwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwIiwiYXVkIjpbImh0dHA6Ly9sb2NhbGhvc3Q6NTAwMC9yZXNvdXJjZXMiLCJhcGkxIl0sImNsaWVudF9pZCI6ImNsaWVudCIsInNjb3BlIjpbImFwaTEiXX0.hnNVWw036KaaKOMLkd7ktnJYMeg2SCqv1kPJFEqQA-SSbt2UqlraDjPVTlvBPm1rrqEsUX7Hj1uC3KdzBh6_co0W2CJSmQk50ntorV5c4rcRHQbmpOaNh6agza5YrlzeZruLG-Kzq8D3Z5oYOh9TDZCUlj1mzxT34KQ5iLQbDEn3kr4AHWddtZpZDeuQa31jV25P8acmeR9OJn8moil8ZZM5guSS82v36CSfWNLu17W1PUeJItL1ohmXpKVYBdF86zzCtC3pWAr-_iQxyePHGh8FtK8LwV8Of8MGkA0rDMDmSmfFS7krfS6FfDVofg2zNa2zJQzuAaCcENhF3XH4RA",
    "expires_in": 3600,
    "token_type": "Bearer"
}           
         *
         * And the token's header & payload
{
    "alg": "RS256",
    "kid": "2ef32e460d32e6c50e2946397646f4bc",
    "typ": "JWT"
}
{
    "nbf": 1591108009,
    "exp": 1591111609,
    "iss": "http://localhost:5000",
    "aud": [
        "http://localhost:5000/resources",
        "api1"
    ],
    "client_id": "client",
    "scope": [
        "api1"
    ]
}
         *
         */
        private static async Task<TokenResponse> Step2RequestToken(String tokenEndpoint)
        {
            var client = new HttpClient();
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                // disco.TokenEndpoint: http://localhost:5000/connect/token
                Address = tokenEndpoint,
                ClientId = "client",
                ClientSecret = "secret",

                Scope = "api1"
            });
            return tokenResponse;
        }

    }
}