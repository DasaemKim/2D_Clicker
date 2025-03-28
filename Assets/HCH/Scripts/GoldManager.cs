using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoldManager : MonoBehaviour
{
    
    [SerializeField] public int plusGold;    //È¹µæ °ñµå
    [SerializeField] public int useGold;     //»ç¿ë °ñµå
    [SerializeField] public int currentGold; //ÇöÀç °ñµå

    public GameObject errorPopup;
    public float popupDuration = 1.0f;


    public void AddGold()
    {
        currentGold += plusGold;
    }
    public void UseGold()
    {
        if (currentGold >= useGold)
        {
            currentGold -= useGold;
        }
        else
        {
            StartCoroutine(ShowErrorPopup());
        }
    }
    private IEnumerator ShowErrorPopup()
    {
        errorPopup.SetActive(true);
        yield return new WaitForSeconds(popupDuration);
        errorPopup.SetActive(false);
    }
}
