using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Xunit;

namespace CardLearningAPI.Tests;

public sealed class ApiSmokeTests : IClassFixture<TestWebApplicationFactory>
{
    private readonly HttpClient _client;

    public ApiSmokeTests(TestWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task Healthz_returns_healthy_status()
    {
        var response = await _client.GetAsync("/healthz");

        response.EnsureSuccessStatusCode();
        var payload = await response.Content.ReadFromJsonAsync<HealthResponse>();

        Assert.Equal("Healthy", payload?.Status);
    }

    [Fact]
    public async Task WeatherForecast_returns_forecasts()
    {
        var forecasts = await _client.GetFromJsonAsync<WeatherForecast[]>("/WeatherForecast");

        Assert.NotNull(forecasts);
        Assert.Equal(5, forecasts.Length);
        Assert.All(forecasts, forecast =>
        {
            Assert.InRange(forecast.TemperatureC, -20, 54);
            Assert.Equal(32 + (int)(forecast.TemperatureC / 0.5556), forecast.TemperatureF);
        });
    }

    [Fact]
    public async Task CreateDeck_with_invalid_payload_returns_bad_request_with_validation_text()
    {
        var response = await _client.PostAsJsonAsync("/Deck", new
        {
            Name = "",
            Description = "test"
        });

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

        await using var responseStream = await response.Content.ReadAsStreamAsync();
        using var document = await JsonDocument.ParseAsync(responseStream);

        Assert.Equal("Validation failed.", document.RootElement.GetProperty("title").GetString());
        Assert.Equal("One or more validation errors occurred.", document.RootElement.GetProperty("detail").GetString());
        Assert.Contains(
            document.RootElement.GetProperty("errors").EnumerateArray().Select(x => x.GetString()),
            error => error is not null && error.Contains("Name", StringComparison.OrdinalIgnoreCase));
    }

    private sealed record HealthResponse(string Status);

    private sealed record WeatherForecast(
        DateOnly Date,
        int TemperatureC,
        int TemperatureF,
        string? Summary);
}
