# Quickstart #5: OpenID Connect Hybrid Flow Authentication and API Access Tokens

This quickstart we combine what we learned in the previous quickstarts and we explored both API access and user authentication. 

## Tutorial

The tutorial that goes along with this sample can be found here [Switching to Hybrid Flow and adding API Access back](http://docs.identityserver.io/en/release/quickstarts/5_hybrid_and_api_access.html)

This section consist of 3 projects, and each project has its own VS solution.
- IdentityServer
- Api
- MvcClient

Start IdentityServer project. Please note, its applicationUrl is http://localhost:5000
![](doc_images/1_LaunchIdentityServer.jpg)

Start Api project. Please note, its applicationUrl is http://localhost:9000
![](doc_images/2_LaunchApi.jpg)

Start MvcClient project. Please note, its applicationUrl is http://localhost:7000
![](doc_images/3_LaunchMvcClient.jpg)

Here is the default page of MvcClient project.
![](doc_images/4_MvcClientWebPage.jpg)

So far the use has not logged-in. So if click the link of "Call Api Using Access Token", it will return 401 error as below
![](doc_images/5_CallApiGet401.jpg)

Get back to the home page of MvcClient, and click "View Secure Data".
![](doc_images/6_ClickViewSecureData.jpg)

Then the browser is redirected to the IdentityServer page, asking user to login. Use "alice" to login. In fact, the username and password is hard-coded in the IdentityServer project.
![](doc_images/7_LoginIdentityServer.jpg)

After login the IdentityServer, the browser will be redirected back to MvcClient page. And now we can view the secure data.
![](doc_images/8_ViewSecureData.jpg)

Get back to the home page of MvcClient, and click the link of "Call Api Using Access Token" again. This time we get the result from the API.
![](doc_images/9_CallApiAndViewResult.jpg)

