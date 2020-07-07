using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary; 

public static class SaveProgression
{
    public static void SaveProg(int progression){
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath+"/player.progression";
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, progression);
        stream.Close();
    }

    public static int LoadProgression(){
        string path = Application.persistentDataPath+"/player.progression";
        if (File.Exists(path)){
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            int progression = (int)formatter.Deserialize(stream);
            stream.Close();
            return progression;
        } else {
            return 0;
        }
    }
}
