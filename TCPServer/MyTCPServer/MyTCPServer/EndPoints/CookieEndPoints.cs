using MyTCPServer.Factory;
using MyTCPServer.Http;

namespace MyTCPServer.EndPoints;

public class CookieEndPoints
{
    private static readonly List<string> CookieFacts =
    [
        "The chocolate chip cookie was invented by Ruth Wakefield in 1938 by accident",
        "The word \"cookie\" comes from the Dutch word \"koekje\" meaning \"little cake\"",
        "Americans consume over 2 billion cookies per year",
        "The most popular cookie in the US is the chocolate chip cookie",
        "Fortune cookies were invented in California, not China",
        "Oreo is the best-selling cookie brand in the world",
        "The first Oreo cookie was sold on March 6, 1912",
        "Girl Scouts have been selling cookies since 1917",
        "A standard chocolate chip cookie contains around 150 calories",
        "The world's largest cookie weighed over 40,000 pounds and was made in 2003"
    ];
    
    private string GetCookieFact()
    {
        int randomIndex = Random.Shared.Next(CookieFacts.Count);
        return CookieFacts[randomIndex];
    }

    public Response? Handle(HttpRequest request)
    {
        return request is { Path: "/cookie", Method: "COOKIE" }
            ? ResponseFactory.Ok(GetCookieFact()) 
            : null;
    }
}