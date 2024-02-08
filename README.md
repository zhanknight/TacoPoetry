![Deploy API](https://github.com/zhanknight/TacoPoetry/actions/workflows/main_tacopoetryapi.yml/badge.svg)
![Deploy API](https://github.com/zhanknight/TacoPoetry/actions/workflows/main_tacopoetry.yml/badge.svg)

# TacoPoetry
A taco themed .NET Blazor demo app (feat. Web API, SQL, Azure, and more).

## What is this?
Taco Poetry is a website with poems about tacos. All of the content, including the poems AND the authors, are fake and were created by an LLM. 
**Taco Poetry is a learning & practice project**. 
The content is all taco poetry. The poems are categorized by poem type, and there is support for tags. 

## The Stack
- Taco Poetry uses Blazor Server communicating over SignalR for the front end. 
- It uses Tailwind (via the CDN rather than real integration. This is bad practice, but we're not in production here).
- It has a .NET Web API backend that communicates with a SQL Server database instance in Azure.
- Everything is deployed in an Azure App Service (Free and cheap tiers, may take a minute to wake up). 
