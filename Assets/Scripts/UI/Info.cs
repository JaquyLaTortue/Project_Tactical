using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Info : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _text1;
    [SerializeField]
    private TextMeshProUGUI _text2;

    [SerializeField]
    private GameObject _spaceText1;
    [SerializeField]
    private GameObject _spaceText2;

    private void Start()
    {
        StartCoroutine(Text1());

    }

    IEnumerator Text1()
    {
        _text1.text = "" + _spaceText1;
        yield return new WaitForSeconds(2f);
        StopCoroutine(Text1());
    }

    
    IEnumerator Text2()
    {
        _text2.text = "" + _spaceText2;
        yield return new WaitForSeconds(2f);
        StopCoroutine(Text2());

    }

    IEnumerator Text3()
    {
        _text2.text = "" + _spaceText2;
        yield return new WaitForSeconds(2f);
        StopCoroutine(Text3());
    }

    public void AfficheText2()
    {
        StopCoroutine(Text2());
    }

    public void AfficheText3()
    {
        StopCoroutine(Text3());
    }

}
