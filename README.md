# Sample of NuScien

This is a sample for [NuScien 6](https://github.com/nuscien/nuscien) framework. It targets .NET 6.0 and licensed by MIT. Sample uses Microsoft SQL Server as its database, but the framework supports any based on Entity Framework Core 6.0.

## Prepare

Before all, please start SSMS to connect local service and create a database `NuScien5`. Then open `Sql\NuScien.ssmssln` solution to execute `TablesCreation.sql` and `TestTablesCreation.sql` TSQL files to create the tables.

## Layers

We often define DAL, BLL and UX as the standard architecture layers for a system. For this, UX should be replaced by Web API because it is a web service. And don't forget we also need models for each layer. Plus, an HTTP SDK layer is especially useful for client reference.

- DAL (data access layer) is used to link database and business logic. This layer is implemented by the framework by default so we can skip it unless we have a special reason to override it.
- BLL (business logic layer) is based on DAL and ACL to provide the complete business logic.
- Web API is used to public the JSON APIs via HTTP service to access BLL only with advanced network processing.
- HTTP SDK is a JSON HTTP client used for console and GUI.

## Entity

You can inherit `BaseResourceEntity` class or its sub-class to implement the unique shared model for all of DAL, BLL and Web API. It also supports property changing notifier so it can be used as view model of UWP and WPF with databinding. Following are some tips you may need.

- Set an attribute `[Table]` on the class to map the table name in database.
- Set an attribute `[Column]` on the property to map the column name in the table of the database; or attribute `[NotMapped]` for the one without mapping.
- Set an attribute `[JsonPropertyName]` on the property to map the JSON property name for network transfer; or attribute `[JsonIgnore]` for the one that does not need serialize and deserialzie.
- The getter and setter of the property should be implemented by `GetCurrentProperty<T>` and `SetCurrentProperty` method to supports databinding feature except the ones you don't care about it or they are just route and convert to/from other properties.

## BLL

You can inherit `OnPremisesResourceEntityProvider<T>` class to implement the provider of the specific entity as its business logic layer. The base class contains some methods for basic accessing capabilities. And you can override it to add ACL and extend it to add further accessing way. Please make sure it contains the constructors with parameter types and orders as same as the one of base class. And, of course, you can add further constructors as you want.

Then inherit `OnPremisesResourceAccessContext` class to organize all the entity providers in your business logic. The way is very simple: define them as properties with public getter and setter. They will be filled automatically when the instance is initialized.

## Web API

Targets ASP.NET Core 6.0.

In Web API controllers, you need implement `ResourceAccessController` class with attribute `[ApiController]` and attribute `[Route]` with `api` and `nuscien5` input values. This is used for passport and settings.

Then inherit `OnPremisesResourceEntityController<TProvider, TEntity>` class to enable the controller of entity provider. You need implement `GetProviderAsync` member method and call it to get the entity provider in actions. The base controller has implememented the basic logic to map the entity provider and you need append others.

## HTTP SDK

You can just do as BLL to inherit `HttpResourceEntityProvider` class for entity provider and to inherit `HttpResourceAccessContext` class for organizing them.

## Projects

Recommended add 3 projects in the solution (`SampleProject` for example): `Core` for entity and HTTP SDK (named `SampleProject`); `Bll` for BLL (named `SampleProject.Bll`); and, `Web` for Web API (named `SampleProject.Web`). You can also merge BLL into `Web` if you do not have a plan to export the BLL as a reference in future.

In this sample, as a simple solution, we only setup 2 projects: `Bll` with entity, HTTP SDK and BLL; `Web` with Web API. And a separated `Sql` is a SMSS solution for necessary TSQL files and other database related files. The sample contains Customers and Goods for example.
