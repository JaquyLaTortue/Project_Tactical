using System.Collections;
using TMPro;
using UnityEngine;

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

    public void AfficheText2()
    {
        StopCoroutine(Text2());
    }

    public void AfficheText3()
    {
        StopCoroutine(Text3());
    }

    private void Start()
    {
        StartCoroutine(Text1());

    }

    private IEnumerator Text1()
    {
        _text1.text = "" + _spaceText1;
        yield return new WaitForSeconds(2f);
        StopCoroutine(Text1());
    }

    private IEnumerator Text2()
    {
        _text2.text = "" + _spaceText2;
        yield return new WaitForSeconds(2f);
        StopCoroutine(Text2());

    }

    private IEnumerator Text3()
    {
        _text2.text = "" + _spaceText2;
        yield return new WaitForSeconds(2f);
        StopCoroutine(Text3());
    }
}
