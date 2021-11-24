using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;

namespace MvcClient
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            // turn off the JWT claim type mapping to allow well-known claims (e.g. ‘sub’ and ‘idp’) to flow through unmolested
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>               // adds the authentication services to DI
                {
                    options.DefaultScheme = "Cookies";          // to issue a cookie using the cookie handler once the OpenID Connect protocol is complete
                    options.DefaultChallengeScheme = "oidc";    // OpenID Connect
                })
                .AddCookie("Cookies")                           // add the handler that can process cookies
                .AddOpenIdConnect("oidc", options =>            // configure the handler that perform the OpenID Connect protocol
                {
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;

                    options.ClientId = "mvc";
                    options.SaveTokens = true;                  // to persist the tokens from IdentityServer in the cookie (as they will be needed later)
                });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            // The authentication middleware should be added before the MVC in the pipeline
            app.UseAuthentication();

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}