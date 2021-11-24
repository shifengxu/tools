// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Api.Controllers
{
    [Route("identity")]
    [Authorize]
    public class IdentityController : ControllerBase
    {
        /*
         * Sample of the response:
[
  {
    "type": "nbf",
    "value": "1591112671"
  },
  {
    "type": "exp",
    "value": "1591116271"
  },
  {
    "type": "iss",
    "value": "http://localhost:5000"
  },
  {
    "type": "aud",
    "value": "http://localhost:5000/resources"
  },
  {
    "type": "aud",
    "value": "api1"
  },
  {
    "type": "client_id",
    "value": "client"
  },
  {
    "type": "scope",
    "value": "api1"
  }
]
         *
         * Another sample of the response:
         * 
[
  {
    "type": "nbf",
    "value": "1591112729"
  },
  {
    "type": "exp",
    "value": "1591116329"
  },
  {
    "type": "iss",
    "value": "http://localhost:5000"
  },
  {
    "type": "aud",
    "value": "http://localhost:5000/resources"
  },
  {
    "type": "aud",
    "value": "api1"
  },
  {
    "type": "client_id",
    "value": "ro.client"
  },
  {
    "type": "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
    "value": "1"
  },
  {
    "type": "auth_time",
    "value": "1591112729"
  },
  {
    "type": "http://schemas.microsoft.com/identity/claims/identityprovider",
    "value": "local"
  },
  {
    "type": "scope",
    "value": "api1"
  },
  {
    "type": "http://schemas.microsoft.com/claims/authnmethodsreferences",
    "value": "pwd"
  }
]
         * 
         */

        public IActionResult Get()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
    }
}