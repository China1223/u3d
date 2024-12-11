using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
// -0.5 1.6 -1.6
// 0.2 0 0
// 0.05
public class FieldAct : MonoBehaviour
{
    // public TMP_Text uiLabel;
    // public TextMeshProUGUI textMeshProUGUI;
    // public GameObject quadObject;
    public GameObject textBoard;
    public TextMeshPro textMeshPro;
    // public GameObject obj2;

    // Start is called before the first frame update
    void Start()
    {
        // 确保组件已绑定
        if (textMeshPro != null)
        {
            // 修改文字内容
            textMeshPro.text = "新文字内容";
        }
        else
        {
            Debug.LogError("未设置TextMeshPro组件！");
        }
    }
    // 当其他Collider进入触发器时调用
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Enter: ");
        if (other.gameObject.tag == "detector")
        {
            textBoard.SetActive(true);
            float newValue = Random.Range(10, 20);
            textMeshPro.text = "ph : "+newValue+" ";
            Debug.Log("detector === Trigger Enter: detector ");
        }
        if (other.gameObject.tag == "BocharBox")
        {
            
            Debug.Log("BocharBox === Trigger Enter:BocharBox " );
            float newValue = Random.Range(10, 20);
            textMeshPro.text = "ph : "+newValue+" ";
            // TextMeshProUGUI textMeshProUGUI = uiTextPh.GetComponent<TextMeshProUGUI>();
            
        }
    }

    // // 当其他Collider停留在触发器内部时调用
    // private void OnTriggerStay(Collider other)
    // {
    //     Debug.Log("Trigger Stay: " + other.gameObject.name);
    // }

    // // 当其他Collider离开触发器时调用
    // private void OnTriggerExit(Collider other)
    // {
    //     Debug.Log("Trigger Exit: " + other.gameObject.name);
    // }

    // Update is called once per frame
    void Update()
    {
        
    }
}
