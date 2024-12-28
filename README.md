<h1 align="center">API Aggregator</h1>

This API Aggregator consolidates data from the following external APIs<br>
* News API: https://newsapi.org/
* OpenWeatherMap API: https://openweathermap.org/api
* RestCountries Api: https://restcountries.com/

and provides a unified endpoint to access the aggregated information.

<hr />

<strong>Tech Stack</strong>

* .NET 9.0
* ASP.NET Core
* xUnit
* Swagger

<hr />

<strong>NuGet Packages</strong>

* Newtonsoft.Json (version 13.0.3)
* Swashbuckle.AspNetCore (version 7.2.0)
* Asp.Versioning.Mvc (version 8.1.0)
* Asp.Versioning.Mvc.ApiExplorer (version 8.1.0)

<hr />

<strong>Solution Structure</strong>
<p align="center">
  <img src="https://github.com/fanis-kar/ApiAggregator/blob/master/Images/solution-structure.png" />
</p>

<hr />

<strong>API Documentation</strong>
<br><br>
On the Main Page you can see all the available endpoints <br><br>

<p align="center">
  <img src="https://github.com/fanis-kar/ApiAggregator/blob/master/Images/swagger-main.png" width="780" />
</p>

<br><br>
The main operation is the Aggregator. The Aggregator consolidates data from the following external APIs:
* News API - example: https://newsapi.org/v2/everything?q=bitcoin&apiKey=[YourApiKey]
* OpenWeatherMap API - example: https://api.openweathermap.org/data/2.5/weather?q=London,uk&appid=[YourApiKey]
* RestCountries Api - example: https://restcountries.com/v3.1/currency/euro
<br><br>
  
<p align="center">  
  <img src="https://github.com/fanis-kar/ApiAggregator/blob/master/Images/swagger-aggregator-response.png" width="780" />
</p>

<br><br>
In addition, there is the Statistics operation which is responsible for recording statistics.
<br><br>
  
<p align="center">  
  <img src="https://github.com/fanis-kar/ApiAggregator/blob/master/Images/swagger-stats-response.png" width="780" />
</p>

<hr />

<strong>Acknowledgements</strong>

I would like to thank you for your time. I am at your disposal for any clarification.
