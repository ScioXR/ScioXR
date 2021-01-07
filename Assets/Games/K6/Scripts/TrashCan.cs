using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashCan : MonoBehaviour
{
    public string goodTag;
    public bool isRecyclingCan;

    public RecyclingGame recyclingGame;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isRecyclingCan)
        {
            Debug.Log("Trigger enter " + name + ": " + other.gameObject);
            recyclingGame.ConsumeTrash(other.gameObject, other.gameObject.tag == goodTag);
        }
        else
        {
            recyclingGame.SendTrash(other.gameObject);
        }
    }
}
