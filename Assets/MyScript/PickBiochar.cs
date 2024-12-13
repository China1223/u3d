using UnityEngine;

public class Pickbiochar  : MonoBehaviour
{

    public GameObject detector;
    public GameObject biochar;
    public GameObject Netbag;

    void Start()
    {
    }
        void OnMouseEnter()
    {
        Debug.Log("Pickbiochar Mouse Enter");

        detector.SetActive(false);
        biochar.SetActive(true);
        Netbag.SetActive(false);
    }
   
    void Update()
    {
        
    }
}
