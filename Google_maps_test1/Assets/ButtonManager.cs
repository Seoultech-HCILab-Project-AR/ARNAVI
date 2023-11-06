using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    #region Button
    
    [SerializeField] private Button startButton;
    [SerializeField] private Button showMapButton;
    [SerializeField] private Button hideMapButton;
    [SerializeField] private GameObject mapImage;
    [Space]
    [SerializeField] private Button showUnivButton;
    [SerializeField] private Button hideUnivButton;
    [SerializeField] private Button searchButton;
    [SerializeField] private Button searchSmokingRoomButton;
    

    // Building Info
    [SerializeField] private GameObject buildingInfo; 
    [SerializeField] private Button goBackButton;
    [SerializeField] private Button naviOnButton;
    #endregion

    public void LoginSuccess()
    {
        // 로그인 성공 후 로그인 UI 지우기
        startButton.transform.parent.gameObject.SetActive(false);
        showMapButton.gameObject.SetActive(true);
        showUnivButton.gameObject.SetActive(true);
    }

    public void ShowMap()
    {
        mapImage.SetActive(true);
        showMapButton.gameObject.SetActive(false);
        hideMapButton.gameObject.SetActive(true);
        // mapImage.GetComponent<RawImage>().texture = 불러온 맵 텍스쳐 
    }

    public void HideMap()
    {
        // 투명도만 낮추는 건 어떨까? 다시 ShowMap했을 때 동기화되는 데 까지 시간이 오래걸릴 것 같음. 
        mapImage.SetActive(false);
        showMapButton.gameObject.SetActive(true);
        hideMapButton.gameObject.SetActive(false);
    }

    public void HideUniv()
    {
        showUnivButton.gameObject.SetActive(true);
        hideUnivButton.gameObject.SetActive(false);
        searchButton.gameObject.SetActive(false);
        searchSmokingRoomButton.gameObject.SetActive(false);
    }
    public void ShowUnivButton()
    {
        showUnivButton.gameObject.SetActive(false);
        hideUnivButton.gameObject.SetActive(true);
        searchButton.gameObject.SetActive(true);
        searchSmokingRoomButton.gameObject.SetActive(true);
    }

    public void ShowBuildingInfo()
    {
        buildingInfo.SetActive(true);
        hideUnivButton.gameObject.SetActive(false);
        searchButton.gameObject.SetActive(false);
        searchSmokingRoomButton.gameObject.SetActive(false);
    }

    public void GoBack()
    {
        buildingInfo.SetActive(false);
        showUnivButton.gameObject.SetActive(true);
        showMapButton.gameObject.SetActive(true);
    }

    public void NaviOn()
    {
        naviOnButton.gameObject.SetActive(false);
    }
}
