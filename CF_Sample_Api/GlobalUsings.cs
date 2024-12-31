﻿global using System.Net;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Microsoft.OpenApi.Models;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
global using Microsoft.AspNetCore.Authentication.JwtBearer;
global using Microsoft.IdentityModel.Tokens;
global using System.Text;
global using AutoMapper;
global using FluentValidation;
global using CF_Sample_Api.Configs;
global using CF_Sample_Api.AppContext;
global using CF_Sample_Api.Contracts;
global using CF_Sample_Api.Constants;  
global using CF_Sample_Api.Middlewares;
global using CF_Sample_Api.Models.Account;
global using CF_Sample_Api.Services.Account;
global using CF_Sample_Api.Contracts.Account;
global using CF_Sample_Api.Endpoints.Account;
global using Microsoft.AspNetCore.Mvc;
global using CF_Sample_Api.Models.Author;
global using CF_Sample_Api.Models.Book;
global using CF_Sample_Api.Contracts.Author;
global using CF_Sample_Api.Services.Author;
global using CF_Sample_Api.Endpoints.Author;
global using App.Core.Util.Cryptography;
