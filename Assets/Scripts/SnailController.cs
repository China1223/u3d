using UnityEngine;
using Unity.Netcode;

public class SnailController : NetworkBehaviour
{
    public float moveSpeed = 1f;
    public float detectionRange = 3f;
    public Transform player;

    private void Update()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRange);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("cake"))
            {
                Debug.Log("Snail found cake!");
                loveCake(collider);
            }else if (collider.CompareTag("NetBag"))
            {
                Debug.Log("Snail found netBag!");
                runNetBag(collider);
            }
        }
    }
    private void runNetBag(Collider netBag)
    {
        
        // 计算蜗牛朝向玩家的方向
        Vector3 direction = (netBag.transform.position - transform.position);
        // direction.y = 0;  // 保持在水平面上
        direction.Normalize();

        // 旋转蜗牛背离玩家
        Vector3 targetDirection = new Vector3(direction.x, 0, direction.z);
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);

        // 移动蜗牛
        transform.position += -1*direction * moveSpeed * Time.deltaTime;
        AdjustHeightToGround();
        // 计算距离
        float distance = Vector3.Distance(transform.position, netBag.transform.position);
        if (distance < 0.3f)
        {
            // 蜗牛被捕捉
            Debug.Log("Snail is caught!");
            // 当前游戏对象
            GameObject currentObject = gameObject;
            // 销毁当前游戏对象
            currentObject.SetActive(false);
            // 将这个状态用netcode同步
            Destroy(gameObject);
        }

    }
    private void loveCake(Collider cake)
    {
        
        // 计算蜗牛朝向玩家的方向
        Vector3 direction = (cake.transform.position - transform.position);
        // direction.y = 0;  // 保持在水平面上
        direction.Normalize();

        // 旋转蜗牛朝向玩家
        Vector3 targetDirection = new Vector3(direction.x, 0, direction.z);
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y + 180, 0);

        // 移动蜗牛
        transform.position += direction * moveSpeed * Time.deltaTime;
        AdjustHeightToGround();
        // 吃掉蛋糕
        // cake.gameObject.SetActive(false);
        float distance = Vector3.Distance(transform.position, cake.transform.position);
        if (distance < 0.3f)
        {
            // 蜗牛被捕捉
            Debug.Log("Snail eats cake!");
            cake.gameObject.SetActive(false);
        }
    }
    private void MoveTowardsPlayer()
    {
        // 计算蜗牛朝向玩家的方向
        Vector3 direction = (player.position - transform.position);
        direction.y = 0;  // 保持在水平面上
        direction.Normalize();

        // 旋转蜗牛朝向玩家
        Vector3 targetDirection = new Vector3(direction.x, 0, direction.z);
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y + 180, 0);

        // 移动蜗牛
        transform.position += direction * moveSpeed * Time.deltaTime;
        AdjustHeightToGround();
    }

    private void AdjustHeightToGround()
    {
        // 使用射线检测来调整蜗牛高度
        RaycastHit hit;
        if (Physics.Raycast(transform.position + 5 * Vector3.up + Vector3.right, Vector3.down, out hit, 10f))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        }
    }
}
