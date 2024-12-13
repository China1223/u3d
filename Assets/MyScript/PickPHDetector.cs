using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickPHDetector : MonoBehaviour
{
    public GameObject detector;
    public GameObject biochar;
    public GameObject Netbag;
    // Start is called before the first frame update
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    void OnMouseEnter()
    {
        Debug.Log("PickPHDetector Mouse Enter");

        detector.SetActive(true);
        biochar.SetActive(false);
        Netbag.SetActive(false);

    }
}
