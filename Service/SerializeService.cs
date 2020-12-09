using System.Collections.Generic;
using System.Text.Json;
using System.IO;

public class SerializeService
    {

    private static SerializeService instance;

    protected SerializeService()
    { }

    public static SerializeService Instance() {
        if (instance == null) {
            instance = new SerializeService();
        }

        return instance;
    }

            public void Serialize(List<User> users) {
                string jsonString = JsonSerializer.Serialize(users);
                File.WriteAllText("/Users/tomirszulc/temp/temp.json", jsonString);
            }

    public List<User> deserialize() {
        string jsonString = File.ReadAllText("/Users/tomirszulc/temp/temp.json");
        return JsonSerializer.Deserialize<List<User>>(jsonString);
    }

}
