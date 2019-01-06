# [Url Shortener](https://url-shortener-app.azurewebsites.net/main)

[![Build Status](https://dev.azure.com/danothom10/UrlShortener/_apis/build/status/url-shortener-app%20-%20CI?branchName=master)](https://dev.azure.com/danothom10/UrlShortener/_build/latest?definitionId=2?branchName=master)

#### An example project for a url shortening service.

* Hosted on an [Azure](portal.azure.com) Web App using an Azure SQL Database

#### Dependencies
* Requires [.Net Core 2.2](https://dotnet.microsoft.com/download/dotnet-core/2.2)

#### Notes
* To work around the fact of not having 2 hosted service domains, navigate to <domain>/Main in order to use the website.
That way generated shortUrls can still use the shorter index route.
* CI/CD  on [Visual Studio Team Services](https://dev.azure.com/danothom10/UrlShortener/) 
  * Pipeline:
      1. Push project to Github.
      2. VSTS Pulls Project.
      3. Builds, tests and deploys to Web App Service in Azure. (Schema changes are auto-migrated during deploy)

