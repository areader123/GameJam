using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
     private SaveManager instance;
     [SerializeField]private string fileName;

     private GameData gameData;
     public List<ISaveManager> saveManagers;

    


     private void Start() {
         
     }

     private void Awake() 
     {
         // fileDataHandler = new FileDataHandler(Application.persistentDataPath,fileName);
          saveManagers = FindAllSaveManagers();
          LoadGame();
          if( instance != null)
          {
               Destroy(instance.gameObject);
          }
          else
          {
               instance = this;
          }
     }


     private void LoadGame()
     {
          foreach(var saveManager in saveManagers) 
          {
               saveManager.LoadData(gameData);
               //Debug.Log("LoadGame");
          }
        
     }

     private void SaveGame()
     {
          foreach(var saveManager in saveManagers) 
          {
              //  Debug.Log("SaveData");
               saveManager.SaveData(ref gameData);
          }
     }

     private void OnApplicationQuit() 
     {
          SaveGame();
     }

     private void OnDisable() {
          SaveGame();
     }

     private List<ISaveManager> FindAllSaveManagers()
     {
          IEnumerable<ISaveManager> saveManagers = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>();
          return new List<ISaveManager>(saveManagers);
     }

}