using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ResourceOwnerClient
{
    public class Program
    {
        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();

        private static async Task MainAsync()
        {
            // discover endpoints from metadata
            var disco = await Step1DiscoverEndpoint();
            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }

            // request token
            var tokenResponse = await Step2RequestToken(disco.TokenEndpoint);
            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }
            Console.WriteLine(tokenResponse.Json);
            Console.WriteLine("\n\n");

            // call api
            var apiClient = new HttpClient();
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await apiClient.GetAsync("http://localhost:9000/identity");
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine(JArray.Parse(content));
            }
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
        private static async Task<DiscoveryResponse> Step1DiscoverEndpoint()
        {
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync("http://localhost:5000");
            return disco;
        }

        /* It will make HTTP request:
         * POST: http://localhost:5000/connect/token
         * request body: username=alice&password=password&scope=api1&grant_type=password&client_id=ro.client&client_secret=secret
         * Response JSON ======================================================================
{
    "access_token": "eyJhbGciOiJSUzI1NiIsImtpZCI6IjJlZjMyZTQ2MGQzMmU2YzUwZTI5NDYzOTc2NDZmNGJjIiwidHlwIjoiSldUIn0.eyJuYmYiOjE1OTExMTEyNjksImV4cCI6MTU5MTExNDg2OSwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDo1MDAwIiwiYXVkIjpbImh0dHA6Ly9sb2NhbGhvc3Q6NTAwMC9yZXNvdXJjZXMiLCJhcGkxIl0sImNsaWVudF9pZCI6InJvLmNsaWVudCIsInN1YiI6IjEiLCJhdXRoX3RpbWUiOjE1OTExMTEyNjksImlkcCI6ImxvY2FsIiwic2NvcGUiOlsiYXBpMSJdLCJhbXIiOlsicHdkIl19.CqezI4GsNO3paReo57exLQ2WxdrLc8CdptsZmvh75m3sQtz5qeIeBTkXjRecRg-TRvu77IY2oKja914z3JcnGBE8ilay7XONEW5-GwoNHS27DKJACSeCPLEKMfdiWsmv5LM0zg4u7mKnovL7TJ64_b50vLJUhq7N5EPalAaH5u8_r3WlM_wEBX_a9QJJ2uOnUuz6lfiP_oSBbwqc4zFNrnC-xzlYAvdBJtzVFalr6ADYxf5u4L_mxElQmGWjgPpTXqMVIlHJ8ibdAN1TNpwdLkuPnB8QKMTzcC--6v9j8ZzxM50dVOv1tJttATJN3HcsKTrSb9_CXaqovfM7jOYZXA",
    "expires_in": 3600,
    "token_type": "Bearer"
}
         *
         * 
         * 
         * And the header & payload of the access_token:
{
    "alg": "RS256",
    "kid": "2ef32e460d32e6c50e2946397646f4bc",
    "typ": "JWT"
}
{
    "nbf": 1591111269,
    "exp": 1591114869,
    "iss": "http://localhost:5000",
    "aud": [
        "http://localhost:5000/resources",
        "api1"
    ],
    "client_id": "ro.client",
    "sub": "1",
    "auth_time": 1591111269,
    "idp": "local",
    "scope": [
        "api1"
    ],
    "amr": [
        "pwd"
    ]
}
         *
         */
        private static async Task<TokenResponse> Step2RequestToken(string tokenEndpoint)
        {
            var client = new HttpClient();
            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = tokenEndpoint,
                ClientId = "ro.client",
                ClientSecret = "secret",

                UserName = "alice",
                Password = "password",
                Scope = "api1"
            });
            return tokenResponse;
        }
    }
}