using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary; 
using System.Collections;
using System.Collections.Generic;

public static class SaveProgression
{
    public static void SaveProg(Dictionary<string, string> progression){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath+"/player.progression";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, progression);
        stream.Close();
    }

    public static Dictionary<string,string> LoadProgression(){
        string path = Application.persistentDataPath+"/player.progression";
        if (File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            Dictionary<string, string> progression = (Dictionary<string, string>)formatter.Deserialize(stream);
            stream.Close();
            return progression;
        } else {
            Dictionary<string, string> a = new Dictionary<string, string>();
            return a;
        }
    }
}
