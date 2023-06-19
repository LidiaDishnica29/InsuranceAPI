﻿// This file is used by Code Analysis to maintain SuppressMessage
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given
// a specific target and scoped to a namespace, type, member, etc.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "<Pending>", Scope = "member", Target = "~M:Insurance.Api.Controllers.InsuranceController.CalculateInsurance(System.Nullable{System.Int32})~System.Threading.Tasks.Task{Microsoft.AspNetCore.Mvc.ActionResult{Insurance.Api.DTOs.InsuranceDTO}}")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "<Pending>", Scope = "member", Target = "~F:Insurance.Api.Controllers.InsuranceController._iInsuranceService")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "<Pending>", Scope = "member", Target = "~M:Insurance.Api.Controllers.InsuranceController.#ctor(Insurance.Api.Interfaces.IInsuranceService)")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "<Pending>", Scope = "member", Target = "~M:Insurance.Api.Controllers.InsuranceController.CalculateOrderInsurance(System.Collections.Generic.List{System.Int32})~System.Threading.Tasks.Task{Microsoft.AspNetCore.Mvc.ActionResult{Insurance.Api.DTOs.TotalInsuranceCostOrderDTO}}")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "<Pending>", Scope = "member", Target = "~M:Insurance.Api.Automapper.Mapping.#ctor")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1633:File should have header", Justification = "<Pending>", Scope = "namespace", Target = "~N:Insurance.Api.DTOs")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1633:File should have header", Justification = "<Pending>")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "<Pending>", Scope = "member", Target = "~M:Insurance.Api.Exceptions.HttpResponseException.#ctor(System.Int32)")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1310:Field names should not contain underscore", Justification = "<Pending>", Scope = "member", Target = "~F:Insurance.Api.Helpers.ErrorMessages.UNEXPECTED_ERROR")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "<Pending>", Scope = "member", Target = "~F:Insurance.Api.Helpers.ErrorMessages.UNEXPECTED_ERROR")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1310:Field names should not contain underscore", Justification = "<Pending>", Scope = "member", Target = "~F:Insurance.Api.Helpers.ErrorMessages.ID_NULL")]
[assembly: SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1401:Fields should be private", Justification = "<Pending>", Scope = "member", Target = "~F:Insurance.Api.Helpers.ErrorMessages.ID_NULL")]
[assembly: SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "<Pending>", Scope = "member", Target = "~F:Insurance.Api.Helpers.Utils.GetProductsTypeConst")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "<Pending>", Scope = "member", Target = "~F:Insurance.Api.Middleware.ErrorHandlingMiddleware._next")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "<Pending>", Scope = "member", Target = "~F:Insurance.Api.Middleware.ErrorHandlingMiddleware._logger")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "<Pending>", Scope = "member", Target = "~M:Insurance.Api.Middleware.ErrorHandlingMiddleware.#ctor(Microsoft.AspNetCore.Http.RequestDelegate,Microsoft.Extensions.Logging.ILogger{Insurance.Api.Middleware.ErrorHandlingMiddleware})")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "<Pending>", Scope = "member", Target = "~M:Insurance.Api.Middleware.ErrorHandlingMiddleware.InvokeAsync(Microsoft.AspNetCore.Http.HttpContext)~System.Threading.Tasks.Task")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "<Pending>", Scope = "member", Target = "~M:Insurance.Api.Middleware.ErrorHandlingMiddleware.HandleExceptionAsync(Microsoft.AspNetCore.Http.HttpContext,System.Exception)~System.Threading.Tasks.Task")]
[assembly: SuppressMessage("Usage", "CA2254:Template should be a static expression", Justification = "<Pending>", Scope = "member", Target = "~M:Insurance.Api.Middleware.ErrorHandlingMiddleware.HandleExceptionAsync(Microsoft.AspNetCore.Http.HttpContext,System.Exception)~System.Threading.Tasks.Task")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "<Pending>", Scope = "member", Target = "~F:Insurance.Api.Services.InsuranceService._logger")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "<Pending>", Scope = "member", Target = "~F:Insurance.Api.Services.InsuranceService._configuration")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "<Pending>", Scope = "member", Target = "~F:Insurance.Api.Services.InsuranceService._httpWrapper")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "<Pending>", Scope = "member", Target = "~F:Insurance.Api.Services.InsuranceService._mapper")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "<Pending>", Scope = "member", Target = "~M:Insurance.Api.Services.InsuranceService.#ctor(Microsoft.Extensions.Logging.ILogger{Insurance.Api.Services.InsuranceService},Microsoft.Extensions.Configuration.IConfiguration,AutoMapper.IMapper,Insurance.Api.Wrapper.IHttpClientWrapper)")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "<Pending>", Scope = "member", Target = "~M:Insurance.Api.Services.InsuranceService.GetInsuranceByProduct(System.Int32)~System.Threading.Tasks.Task{Insurance.Api.DTOs.InsuranceDTO}")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "<Pending>", Scope = "member", Target = "~M:Insurance.Api.Services.InsuranceService.GetInsuranceForProductsInShoppingCart(System.Collections.Generic.List{System.Int32})~System.Threading.Tasks.Task{Insurance.Api.DTOs.TotalInsuranceCostOrderDTO}")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "<Pending>", Scope = "member", Target = "~M:Insurance.Api.Services.InsuranceService.GetProductTypes~System.Threading.Tasks.Task{System.Collections.Generic.List{Insurance.Api.Entities.ProductType}}")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "<Pending>", Scope = "member", Target = "~M:Insurance.Api.Services.InsuranceService.GetProductById(System.Nullable{System.Int32})~System.Threading.Tasks.Task{Insurance.Api.Entities.Product}")]
[assembly: SuppressMessage("Style", "IDE0059:Unnecessary assignment of a value", Justification = "<Pending>", Scope = "member", Target = "~M:Insurance.Api.Services.InsuranceService.GetProductById(System.Nullable{System.Int32})~System.Threading.Tasks.Task{Insurance.Api.Entities.Product}")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "<Pending>", Scope = "member", Target = "~M:Insurance.Api.Services.InsuranceService.GetProducts(System.Collections.Generic.List{System.Int32})~System.Threading.Tasks.Task{System.Collections.Generic.List{Insurance.Api.Entities.Product}}")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "<Pending>", Scope = "member", Target = "~F:Insurance.Api.API.HttpClientWrapper._httpClient")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "<Pending>", Scope = "member", Target = "~M:Insurance.Api.API.HttpClientWrapper.#ctor(System.Net.Http.HttpClient)")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "<Pending>", Scope = "member", Target = "~M:Insurance.Api.API.HttpClientWrapper.GetAsync(System.String)~System.Threading.Tasks.Task{System.Net.Http.HttpResponseMessage}")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "<Pending>", Scope = "member", Target = "~M:Insurance.Api.Startup.#ctor(Microsoft.AspNetCore.Hosting.IWebHostEnvironment)")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "<Pending>", Scope = "member", Target = "~F:Insurance.Api.Services.ProductAPIService._logger")]
[assembly: SuppressMessage("StyleCop.CSharp.NamingRules", "SA1309:Field names should not begin with underscore", Justification = "<Pending>", Scope = "member", Target = "~F:Insurance.Api.Services.InsuranceService._productAPIService")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "<Pending>", Scope = "member", Target = "~M:Insurance.Api.Services.InsuranceService.#ctor(Microsoft.Extensions.Logging.ILogger{Insurance.Api.Services.InsuranceService},AutoMapper.IMapper,Insurance.Api.Interfaces.IProductAPIService)")]
[assembly: SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1101:Prefix local calls with this", Justification = "<Pending>", Scope = "member", Target = "~M:Insurance.Api.Services.InsuranceService.GetSurcharge(System.Int32,System.Int32)~System.Threading.Tasks.Task{Insurance.Api.DTOs.InsuranceDTO}")]
