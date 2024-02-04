using System.Collections;
using System.Collections.Generic;
using Farm.Dialogue;
using UnityEngine;

public class LevelScene_HasShow : LevelScene
{
    public DialogueController dialogue;

    [Header("记录点")]
    public Transform cameraPointTrans;
    public Transform playerPointTrans;

    [Header("相机参数")]
    public float moveSpeed = 1f;


    private Transform[] cameraPoints;
    private Transform[] playerPoints;

    private GameObject playerA;
    private GameObject playerB;
    void Awake()
    {
        cameraPoints = new Transform[cameraPointTrans.childCount];
        playerPoints = new Transform[playerPointTrans.childCount];
        for(int i = 0;i < cameraPointTrans.childCount; ++i)
        {
            cameraPoints[i] = cameraPointTrans.GetChild(i);
        }
        for(int i = 0;i < playerPointTrans.childCount; ++i)
        {
            playerPoints[i] = playerPointTrans.GetChild(i);
        }

        playerA = GameObject.Find("Player_A");
        playerB = GameObject.Find("Player_B");
    }

    public override void Init()
    {
        Time.timeScale = 1;
        GameFlowManager.instance.InitLevel();
        OnShowStart();
        finalUI.SetActive(false);
        stopUI.SetActive(false);
    }

    public void OnShowStart()
    {
        playerA.SetActive(true);
        playerB.SetActive(true);
        playerA.GetComponent<PlayerController>()._active = false;
        playerB.GetComponent<PlayerController>()._active = false;
        playerA.transform.position = playerPoints[0].position;
        playerB.transform.position = playerPoints[1].position;

        Camera.main.transform.position = cameraPoints[0].position;

        dialogue.canTalk = true;
        dialogue.canActivate = true;
        dialogue.SetDialogueList();
        StartCoroutine(dialogue.DialogueRoutine());

    }

    public void MoveCameraToNextPoint(int index)
    {
        Camera.main.transform.position = cameraPoints[index].position;
    }

    public void OnShowEnd()
    {
        Debug.Log("OnShowEnd");
        StartCoroutine(MoveToTarget(cameraPoints[1]));

        playerA.GetComponent<PlayerController>()._active = true;
        playerB.GetComponent<PlayerController>()._active = true;
        GameFlowManager.instance.SetPlayerInStartPoint();
        GameFlowManager.instance.UnlockPlayers();
    }

    IEnumerator MoveToTarget(Transform target)
    {
        // 缓动函数，可以根据需要选择不同的缓动函数
        AnimationCurve curve = AnimationCurve.EaseInOut(0, 0, 1, 1);

        float elapsedTime = 0f;
        Vector3 startingPosition = Camera.main.transform.position;

        while (elapsedTime < 1f)
        {
            // 使用缓动函数计算插值
            float t = curve.Evaluate(elapsedTime);
            Camera.main.transform.position = Vector3.Lerp(startingPosition, target.position, t);

            // 更新时间
            elapsedTime += Time.deltaTime * moveSpeed;

            // 等待一帧
            yield return null;
        }

        // 确保最终位置准确
        Camera.main.transform.position = target.position;
    }
}
