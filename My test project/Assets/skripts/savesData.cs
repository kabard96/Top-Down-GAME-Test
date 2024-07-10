using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class savesData : MonoBehaviour
{
    public int _levelNumber = 1;
    public int _maxHealt = 100;
    public int _numberofzombieskilled = 0;
    string filePath;

    public static savesData instance;
   

    private void Awake()
    {
        //ƒÀﬂ  Œ––≈ “ÕŒ… –¿¡Œ“€ —Œ’–¿Õ≈Õ»… Õ≈Œ¡’Œƒ»ÃŒ «¿√–”∆¿“‹ »√–” »« —÷≈Õ€ √À¿¬ÕŒ√Œ Ã≈Õﬁ.
        //—≈…◊¿— –¿¡Œ“¿ﬁ“ “ŒÀ‹ Œ —Œ’–¿Õ≈Õ»ﬂ ◊»—À¿ œŒ¡≈∆ƒ≈ÕÕ€’ «ŒÃ¡»
        
        if (instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            instance = this;

        }
        else
        {
            Destroy(gameObject);
        }

    }
    private void Start()
    {
        filePath = Application.persistentDataPath + "/save.gamesave";
        LoadProgress();
    }
    public void LoadProgress()
    {

        if (!File.Exists(filePath))
            return;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream fs = new FileStream(filePath, FileMode.Open);
        Save save = (Save)bf.Deserialize(fs);
        _levelNumber = save.LevelNumber;
        _maxHealt = save.MaxHealt;
        _numberofzombieskilled = save.Numberofzombieskilled;
        fs.Close();
    }
    public void SaveProgress()
    {
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        FileStream fs=new FileStream(filePath,FileMode.Create);
        Save save=new Save();
        save.LevelNumber = _levelNumber;
        save.MaxHealt = _maxHealt;
        save.Numberofzombieskilled= _numberofzombieskilled;
        binaryFormatter.Serialize(fs,save);
        fs.Close();
     
    }
    [System.Serializable]
    public class Save
    {
        
        public int LevelNumber;
        public int MaxHealt;
        public int Numberofzombieskilled;

    }
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
}
