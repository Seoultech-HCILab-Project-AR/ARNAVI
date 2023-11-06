using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class JoyStick : MonoBehaviour
{
    [SerializeField] private RectTransform joyStickRectTransform;
    [SerializeField] private RectTransform joyStickBackGroundRectTransform;
    [Space] [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform ARCameraTransform;
    public float mul = 0f;
    private bool m_bTouch = false;
    
    [SerializeField] private Dragable dragable;

    public Vector2 startPos;       // 시작점 위치
    public Vector2 endPos;         // 끝점 위치

    public Vector2 direction;      // 계산한 방향
    public Vector2 joyVec2;
    private void Awake()
    {
        joyVec2 = new Vector2(100, 100); 
        dragable = GetComponentInChildren<Dragable>();
        dragable.OnDragStart += PosMark;
        dragable.OnWhileDrag += CharacterMove;
        dragable.OnDragDone += AfterInteraction;
    }
    void Update()
    {
        
    }

    private void SynchronizeJoyStickCamToPlayerRotation()
    {
        // 카메라로 보는 방향  -> 월드좌표 방향 추정, XZ 평면
        // ARCamera.transform.forward가 가리키는 방향을 world좌표 내 x, -x, z, -z방향 으로 결정
        //playerTransform.position - ARCameraTransform.position
        
    }

    private float RotationApproximate()
    {
        /*if (ARCameraTransform.localRotation.eulerAngles.y >= -45f && ARCameraTransform.localRotation.eulerAngles.y < 45f) return Vector3.forward;
        if (ARCameraTransform.localRotation.eulerAngles.y >= 45f && ARCameraTransform.localRotation.eulerAngles.y < 135f) return Vector3.right;
        if (ARCameraTransform.localRotation.eulerAngles.y >= 135f && ARCameraTransform.localRotation.eulerAngles.y < 225f) return Vector3.back;
        if (ARCameraTransform.localRotation.eulerAngles.y >= 225f && ARCameraTransform.localRotation.eulerAngles.y > -45f) return Vector3.left;*/
        if (ARCameraTransform.localRotation.eulerAngles.y >= -45f && ARCameraTransform.localRotation.eulerAngles.y < 45f) return 0f;
        if (ARCameraTransform.localRotation.eulerAngles.y >= 45f && ARCameraTransform.localRotation.eulerAngles.y < 135f) return 90f;
        if (ARCameraTransform.localRotation.eulerAngles.y >= 135f && ARCameraTransform.localRotation.eulerAngles.y < 225f) return 180f;
        if (ARCameraTransform.localRotation.eulerAngles.y >= 225f && ARCameraTransform.localRotation.eulerAngles.y > -45f) return 270f;
        else
        {
            Debug.Log("error");
            return 0;
        }
    }
    private void PosMark(PointerEventData eventData)
    {
        // 시작점 기억
        Debug.Log($"PosMark");
        startPos = eventData.position;
        
    }
    
    private void CharacterMove(PointerEventData eventData)
    {
        // 플레이어가 이동할 "방향" 업데이트
        //Debug.Log($"CharacterMove");
        endPos = eventData.position;
        direction = (endPos - startPos).normalized;
        joyStickRectTransform.anchoredPosition = joyVec2 + direction * mul;
        Debug.Log(direction);
        // Debug.Log(RotationApproximate());
        
        //playerTransform.forward = RotationApproximate();
        //playerTransform.forward += Vector3.right;
        
        // Cam 방향 + JoyStick 방향 
        
        //playerTransform.rotation = Quaternion.Euler(0, RotationApproximate() + 90f,0);
        
        

        if (direction.x >=0.707 && (direction.y < 0.707 || direction.y > -0.707))
        {
            playerTransform.rotation = Quaternion.Euler(0, RotationApproximate() + 90f,0);
        }else if (direction.y >=0.707 && (direction.x > -0.707 || direction.x < 0.707))
        {
            playerTransform.rotation = Quaternion.Euler(0, RotationApproximate(),0);
        }else if (direction.x <= -0.707 && (direction.y < 0.707 || direction.y > -0.707))
        {
            playerTransform.rotation = Quaternion.Euler(0, RotationApproximate() + 270f,0);
        }else if (direction.y <= -0.707 && (direction.x > -0.707 || direction.x < 0.707))
        {
            playerTransform.rotation = Quaternion.Euler(0, RotationApproximate() + 180f,0);
        }
        // x >=0.707 and y < 0.707 or y > -0.707 보는 방향 기준 오른쪽
        // y >= 0.707 and x > -0.707 or x < 0.707 위쪽
        // x <= -0.707 and y < 0.707 or y > -0.707 왼쪽
        // y <= -0.707 and x > -0.707 or x < 0.707 아래쪽
        // ARCamera.forward 더하기 조이스틱 입력하는 방향을 더 회전시킨다.
    }

    private void AfterInteraction(PointerEventData eventData)
    {
        joyStickRectTransform.anchoredPosition = new Vector2(100,100);
        // 이동 종료시 direction 제거
        Debug.Log($"AfterInteraction");
        //_playerController.direction = Vector2.zero;
        //playerController.playerAnimator.SetBool("IsWalking",false);
    }
   
}
