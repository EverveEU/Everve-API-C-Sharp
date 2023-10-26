using System;
using System.Net.Http;
using System.Threading.Tasks;

public class EverveAPI
{
    private string apiKey;
    private string baseUrl = "https://api.everve.net/v3/";

    public EverveAPI(string apiKey)
    {
        this.apiKey = apiKey;
    }

    public async Task<string> MakeRequest(string endpoint, string paramsString)
    {
        HttpClient client = new HttpClient();
        HttpResponseMessage response = await client.GetAsync($"{baseUrl}{endpoint}?api_key={apiKey}&format=json{paramsString}");
        return await response.Content.ReadAsStringAsync();
    }

    public Task<string> GetUser()
    {
        return MakeRequest("user", "");
    }

    public Task<string> GetSocials()
    {
        return MakeRequest("socials", "");
    }

    public Task<string> GetCategories(string id = null)
    {
        string endpoint = id != null ? $"categories/{id}" : "categories";
        return MakeRequest(endpoint, "");
    }

    public Task<string> CreateOrder(string paramsString)
    {
        return MakeRequest("orders", paramsString);
    }

    public Task<string> GetOrders(string id = null)
    {
        string endpoint = id != null ? $"orders/{id}" : "orders";
        return MakeRequest(endpoint, "");
    }

    public Task<string> UpdateOrder(string id, string paramsString)
    {
        return MakeRequest($"orders/{id}", paramsString);
    }

    public Task<string> DeleteOrder(string id)
    {
        return MakeRequest($"orders/{id}", "&_method=DELETE");
    }

// EXAMPLE:
    public static async Task Main(string[] args)
    {
        EverveAPI api = new EverveAPI("your_api_key_here");

        string userInfo = await api.GetUser();
        Console.WriteLine($"User Info: {userInfo}");

        string socials = await api.GetSocials();
        Console.WriteLine($"Socials: {socials}");

        string categories = await api.GetCategories();
        Console.WriteLine($"Categories: {categories}");

        // Add your params in the format "&param1=value1&param2=value2"
        string newOrder = await api.CreateOrder("&param1=value1");
        Console.WriteLine($"New Order: {newOrder}");

        string orders = await api.GetOrders();
        Console.WriteLine($"Orders: {orders}");

        string updatedOrder = await api.UpdateOrder("1", "&param1=newValue1");
        Console.WriteLine($"Updated Order: {updatedOrder}");

        string deletedOrder = await api.DeleteOrder("1");
        Console.WriteLine($"Deleted Order: {deletedOrder}");
    }
}
