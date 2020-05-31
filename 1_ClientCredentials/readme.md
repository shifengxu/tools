# Quickstart #1: Securing an API using Client Credentials

The doc and code here is derived from the tutorial [Client Credentials](http://docs.identityserver.io/en/latest/quickstarts/1_client_credentials.html). But I removed some unnecessary code and tried it on Visual Studio 2019.

For quick trying and experience, you can download the Git code and run it. But if you want to set up the project from scratch, here is the step by step guide.

- 1.1: Setup [IdentityServer](./IdentityServer) project

- 1.2: Setup [Api](./Api) project

- 1.3: Setup [Client](./Client) project


The 3 projects are separated in 3 solutions. This is to ease the debugging process.

In the communication perspective, the client should discover the IdentityServer, get a token from it, and then make the API call with the token. I recorded such process by Fiddler, and the related files are present in the Git

- Fiddler_ClientRequests.saz

- Fiddler_ClientRequests.txt
