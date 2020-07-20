using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary; 
using System.Collections;
using System.Collections.Generic;

public static class SaveProgression
{
    public static void SaveProg(List<int> progression){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath+"/player.progression";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, progression);
        stream.Close();
    }

    public static void SaveTime(Dictionary<string,string> times){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath+"/player.time";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, times);
        stream.Close();
    }

    public static List<int> LoadProgression(){
        string path = Application.persistentDataPath+"/player.progression";
        if (File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            List<int> progression = (List<int>)formatter.Deserialize(stream);
            stream.Close();
            return progression;
        } else {
            List<int> a = new List<int> {0, 0, 0, 0};
            return a;
        }
    }

    public static Dictionary<string,string> LoadTimes(){
        string path = Application.persistentDataPath+"/player.time";
        if (File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            Dictionary<string,string> times = (Dictionary<string,string>)formatter.Deserialize(stream);
            stream.Close();
            return times;
        } else {
            Dictionary<string,string> a = new Dictionary<string,string>();
            return a;
        }
    }
}
