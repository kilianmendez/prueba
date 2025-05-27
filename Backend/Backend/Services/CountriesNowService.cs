

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Globalization;
using System.Threading.Tasks;
using F23.StringSimilarity;
using F23.StringSimilarity.Interfaces;

public class CountriesNowService
{
    private const double THRESHOLD = 0.80;
    private readonly INormalizedStringSimilarity _comparer;
    private readonly HttpClient _httpClient;

    public CountriesNowService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("CountriesNow");
        _comparer = new JaroWinkler();
    }

    public async Task<List<CountryData>> GetAllCountriesAsync()
    {
        var resp = await _httpClient.GetAsync("countries/flag/images");
        resp.EnsureSuccessStatusCode();
        var wrapper = await resp.Content.ReadFromJsonAsync<CountryImagesResponse>();
        return wrapper?.Data ?? new List<CountryData>();
    }

    public async Task<List<CountryData>> SearchCountriesAsync(string query)
    {
        var all = await GetAllCountriesAsync();
        if (string.IsNullOrWhiteSpace(query))
            return all;

        var qKeys = GetKeys(ClearText(query));
        var result = new List<CountryData>();

        foreach (var c in all)
        {
            var itemKeys = GetKeys(ClearText(c.Name));
            if (IsMatch(itemKeys, qKeys))
                result.Add(c);
        }

        return result;
    }

    public async Task<List<string>> GetCitiesByCountryAsync(string country)
    {
        var body = new { country };
        var resp = await _httpClient.PostAsJsonAsync("countries/cities", body);
        resp.EnsureSuccessStatusCode();
        var wrapper = await resp.Content.ReadFromJsonAsync<CitiesResponse>();
        return wrapper?.Data ?? new List<string>();
    }

    public async Task<List<string>> SearchCitiesAsync(string country, string query)
    {
        var cities = await GetCitiesByCountryAsync(country);
        if (string.IsNullOrWhiteSpace(query))
            return cities;

        var qKeys = GetKeys(ClearText(query));
        var result = new List<string>();

        foreach (var city in cities)
        {
            var itemKeys = GetKeys(ClearText(city));
            if (IsMatch(itemKeys, qKeys))
                result.Add(city);
        }

        return result;
    }


    private bool IsMatch(string[] itemKeys, string[] queryKeys)
    {
        foreach (var ik in itemKeys)
            foreach (var qk in queryKeys)
                if (IsMatch(ik, qk))
                    return true;
        return false;
    }

    private bool IsMatch(string itemKey, string queryKey)
    {
        return itemKey == queryKey
            || itemKey.Contains(queryKey, StringComparison.OrdinalIgnoreCase)
            || _comparer.Similarity(itemKey, queryKey) >= THRESHOLD;
    }

    private string[] GetKeys(string text)
    {
        return text
            .Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    }

    private string ClearText(string text)
    {
        var formD = text.ToLowerInvariant().Normalize(NormalizationForm.FormD);
        var sb = new StringBuilder();
        foreach (var c in formD)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                sb.Append(c);
        }
        return sb.ToString().Normalize(NormalizationForm.FormC);
    }
}
