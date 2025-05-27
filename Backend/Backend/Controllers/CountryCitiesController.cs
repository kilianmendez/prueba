using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class LocationController : ControllerBase
{
    private readonly CountriesNowService _countriesNowService;

    public LocationController(CountriesNowService countriesNowService)
    {
        _countriesNowService = countriesNowService;
    }

    [HttpGet("CountriesSearch")]
    public async Task<IActionResult> SearchCountries([FromQuery] string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return BadRequest("El parámetro 'query' es obligatorio.");

        var filteredCountries = await _countriesNowService.SearchCountriesAsync(query);
        return Ok(filteredCountries);
    }

    [HttpGet("CitiesSearch")]
    public async Task<IActionResult> SearchCities([FromQuery] string country, [FromQuery] string query)
    {
        if (string.IsNullOrWhiteSpace(country))
            return BadRequest("El parámetro 'country' es obligatorio.");
        if (string.IsNullOrWhiteSpace(query))
            return BadRequest("El parámetro 'query' es obligatorio.");

        var filteredCities = await _countriesNowService.SearchCitiesAsync(country, query);
        return Ok(filteredCities);
    }


}
