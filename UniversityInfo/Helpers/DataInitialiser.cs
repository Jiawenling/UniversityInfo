using Newtonsoft.Json;
using UniversityInfo.Helpers;
using UniversityInfo.Models;

public class DataInitialiser{
    public static async Task SeedData(DataContext context)
    {
        if(context.Universities.Any()) return;
        string file = File.ReadAllText(Path.Join("SeedData", "Universities.json"));
        List<University> universities = JsonConvert.DeserializeObject<List<University>>(file);
        await context.AddRangeAsync(universities);
        await context.SaveChangesAsync();
    }
}