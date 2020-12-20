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
        checkIfFileExists();
        string jsonString = JsonSerializer.Serialize(users);
        File.WriteAllText("/Users/tomirszulc/temp/temp.json", jsonString);
     }

    public List<User> deserialize() {
        checkIfFileExists();
        string jsonString = File.ReadAllText("/Users/tomirszulc/temp/temp.json");
        if (jsonString != "") {
            return JsonSerializer.Deserialize<List<User>>(jsonString);
        }
        return new List<User>();
    }

    private void checkIfFileExists() {
        string file = "/Users/tomirszulc/temp/temp.json";
        if (File.Exists(file))
        {

        }
        else {
            File.Create(file);
        }
    }

}
