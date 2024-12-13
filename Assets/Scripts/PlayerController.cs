using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float detectionRange = 2f;
    private bool hasNetBag = false;
    public GameObject Netbag;
    public GameObject detector;
    public GameObject biochar;

    void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRange);

        foreach (Collider collider in colliders)
        {
            // Debug.Log("collider.gameObject.name");
            // Debug.Log(collider.gameObject.name);
            // Debug.Log(collider.gameObject.tag);
            if (collider.CompareTag("NetBag1"))
            {
                // Destroy(collider.gameObject);
                // collider.gameObject.SetActive(false);
                hasNetBag = true;
                Netbag.SetActive(true);
                detector.SetActive(false);
                biochar.SetActive(false);
            }
            else if (collider.CompareTag("Snail") && hasNetBag)
            {
                // Destroy(collider.gameObject);
                collider.gameObject.SetActive(false);
                //hasNetBag = false;
                                FindObjectOfType<ProgressBar>().woniu += 1;
                //Netbag.SetActive(false);

            }
        }
    }
}