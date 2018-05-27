# aglpets
Implementation of AGLs programming challenge available [here](http://agl-developer-test.azurewebsites.net/)

Uses C#, ASP.NET Core, xUnit, Mock, Bootstrap, and React to show a list of sorted cat names grouped by their owners gender.

Consists of three projects:
* `AglPets` Model and Service classes
* `Cats` Web front end consuming services from `AglPets`
* `AglPetsTests` Unit tests for services in `AglPets`

The source for the data is currently the web service available at [http://agl-developer-test.azurewebsites.net/people.json](http://agl-developer-test.azurewebsites.net/people.json) however this could be changed to a database source or flat file source by implementing the `IPetsDataProvider` interface.