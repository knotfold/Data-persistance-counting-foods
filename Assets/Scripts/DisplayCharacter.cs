using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DisplayCharacter : MonoBehaviour
{
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown() {
        MainManager.Instance.skinnedMeshRenderer = gameObject.GetComponent<SkinnedMeshRenderer>();
        MainManager.Instance.character = index;
        if(MainManager.Instance.character != 5){
            Debug.Log("character swapped");
        }
        
    }

 
}
