using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NameList : MonoBehaviour
{
    string[] names = { "Tact Haelstrom", "Amarra Bandarrion", "Baldrick Buntd", "Donald Whent",
        "Syrulliana Di’ Annte", "Quadina Redrook", "Jaq Le’quet", "Turnip Bellwater", "",
        "Corina Camberline", "Stewart Inkpot", "Jagod Di", "Bevel Left" };

    Queue<string> nameList = new Queue<string>();

    // Start is called before the first frame update
    void Start()
    {
        foreach(var item in names)
        {
            nameList.Enqueue(item);
        }
    }

    public string GetName()
    {
        if(names != null)
        {
            return nameList.Dequeue();

        }
        else
        {
            Debug.Log("namelist empty");
            return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
