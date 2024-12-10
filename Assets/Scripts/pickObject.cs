using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
 
    public class ObjectDestroyer : MonoBehaviour
    {
        // 当鼠标在该物体上按下时调用
        void OnMouseDown()
        {
            // 销毁该物体
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Enter");
        if(other.gameObject.tag == "Coin"){
            Debug.Log("Coin");
            // Destroy(other.gameObject);
            other.gameObject.SetActive(false);
        }
    }
}
