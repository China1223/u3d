using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Netcode; // 使用Netcode进行网络同步

public class FieldAct : NetworkBehaviour
{
    public GameObject textBoard;
    public TextMeshPro textMeshPro;

    // 网络同步变量，初始值为1-5的随机整数
    private NetworkVariable<int> phValue = new NetworkVariable<int>(
        default,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server // 仅服务器端可写
    );

    void Start()
    {
        if (IsServer)
        {
            // 服务器端初始化phValue
            phValue.Value = Random.Range(1, 6);
            UpdateText(); 
            textBoard.SetActive(false);
        }

        // 监听phValue变化
        phValue.OnValueChanged += OnPhValueChanged;
    }

    private void OnDestroy()
    {
        // 移除事件监听器
        phValue.OnValueChanged -= OnPhValueChanged;
    }

    // 当其他Collider进入触发器时调用
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("detector"))
        {
            if (!textBoard.activeSelf)
            {
                textBoard.SetActive(true); // 如果未激活，激活textBoard
                UpdateText();             // 更新文字内容
            }
            else
            {
                Debug.Log("textBoard 已经激活，无需重复设置内容。");
            }
        }

        if (other.CompareTag("BocharBox"))
        {
            Debug.Log("BocharBox === Trigger Enter: BocharBox");
            // 调用 BiocharManager 的 Work 方法，仅当是本客户端的对象时执行
            if (IsOwner)
            {
                BiocharManager biocharManager = other.GetComponent<BiocharManager>();
                if (biocharManager != null)
                {
                    biocharManager.Work();
                }
                else
                {
                    Debug.LogError("未找到 BiocharManager 组件！");
                }
            }
            else
            {
                Debug.Log("不是本客户端的对象，忽略。");
            }
        }
    }

    // 更新文字内容
    private void UpdateText()
    {
        if (textMeshPro != null)
        {
            textMeshPro.text = $"ph : {phValue.Value}";
        }
        else
        {
            Debug.LogError("未设置TextMeshPro组件！");
        }
    }

    // 当phValue发生变化时调用
    private void OnPhValueChanged(int oldValue, int newValue)
    {
        Debug.Log($"phValue 发生变化: {oldValue} -> {newValue}");
        UpdateText(); // 当值变化时更新文字内容
    }
}


// public class FieldAct : MonoBehaviour
// {

//     public GameObject textBoard;
//     public TextMeshPro textMeshPro;


//     void Start()
//     {
//         // 确保组件已绑定
//         if (textMeshPro != null)
//         {
//             // 修改文字内容
//             textMeshPro.text = "新文字内容";
//         }
//         else
//         {
//             Debug.LogError("未设置TextMeshPro组件！");
//         }
//     }
//     // 当其他Collider进入触发器时调用
//     private void OnTriggerEnter(Collider other)
//     {
//         Debug.Log("Trigger Enter: ");
//         if (other.gameObject.tag == "detector")
//         {
//             textBoard.SetActive(true);
//             float newValue = Random.Range(10, 20);
//             textMeshPro.text = "ph : "+newValue+" ";
//             Debug.Log("detector === Trigger Enter: detector ");
//         }

//     }


//     void Update()
//     {
        
//     }
// }
