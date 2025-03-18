# covadis-azure-workshop

## Requirements
- .NET 8
  - ASP.NET and web development
  - Azure development

## Tools
- Visual Studio
- Azure Data Studio or SQL Server Management Studio (SSMS)
- Azurite
- ServiceBusExplorer
- Microsoft Azure Storage Explorer

## Azure resources
- Azure Resource Group _(rg-)_
- Azure App Service Plan _(asp-)_
- Azure App Service _(app-)_
- Azure Function App _(func-)_
- Azure Application Insights _(appi-)_
- Azure Log Analytics Workspace _(log-)_
- Azure Storage Account _(st)_
- Azure SQL Server _(sql-)_
- Azure SQL Database _(*)_
- Azure Service Bus Namespace _(sbns-)_

### Azure Sandbox
- Azure Portal: https://portal.azure.com/
- Microsoft Learn
  - [10 sandboxes per day, 4 hours per sandbox](https://community.dynamics.com/blogs/post/?postid=990c7a16-9426-427f-9a2c-a94df8dad1f5)
  - [AZ-900: Describe the core architectural components of Azure](https://learn.microsoft.com/en-us/training/modules/describe-core-architectural-components-of-azure/1-introduction) Exercise unit 4
  - [AZ-204: Implement Azure App Service web apps / Explore Azure App Service](https://learn.microsoft.com/en-us/training/modules/introduction-to-azure-app-service/1-introduction) Exercise unit 7
  - [Host a web application with Azure App Service](https://learn.microsoft.com/en-us/training/modules/host-a-web-app-with-azure-app-service/1-introduction) Exercise unit 7

Login with the same account where you activated the sandbox in Microsoft Learn and switch to the sandbox tenant in the Azure portal:
 ![image](https://github.com/user-attachments/assets/bfd5b860-974a-45d5-bde1-3541104883c4)

Search for subscriptions in the Azure portal and you will find your sandbox Azure subscription:
![image](https://github.com/user-attachments/assets/a4a72325-5f36-41de-9d91-7083bc5c1269)

Within this Azure subscription you are able to create Azure resources like Azure App Service, Azure SQL Server, etc. Every resource must be inside an Azure resource group.
![image](https://github.com/user-attachments/assets/2535889d-b5eb-4d75-9557-6cff4e066097)
