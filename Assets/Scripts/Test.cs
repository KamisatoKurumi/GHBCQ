using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Update()
    {
        // 检测鼠标左键点击
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Left Button Clicked");
            // 获取鼠标点击位置
            Vector2 mousePosition = Input.mousePosition;

            // 将屏幕位置转换为世界坐标
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // 发射射线检测点击的UI
            RaycastHit2D hit = Physics2D.Raycast(worldPosition, Vector2.zero);

            // 如果点击到UI，输出调试信息
            if (hit.collider != null)
            {
                Debug.Log("Clicked on UI: " + hit.collider.gameObject.name);
            }
        }
    }
}
