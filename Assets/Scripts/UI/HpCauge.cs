using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HpCauge : MonoBehaviour
{
    public Image _healthImage;
    public Image _healthImageFront;
    [SerializeField]
    private float _duration;
    public int a;
    public CharacterBase _character;

    void Update()
    {
        /* _healthImage.fillAmount = _character.HpCurrent / _character.HpMax;
         Debug.Log("aaaa");
         OnHealthChanged();*/

    }

    public void OnEnable()
    {
        //_character.HpCurrent += OnHealthChanged;

        /*_character.HpCurrent -= a;
        Debug.Log("zzz");*/
    }

    private void OnHealthChanged(int newHealth)
    {
        /*float targetFillAmount = Mathf.InverseLerp(0, _character.HpMax, newHealth);

        DOTween.Sequence()
            .Append(
                _healthImageFront.DOFillAmount(targetFillAmount, _duration / 2f)
                    .SetEase(Ease.OutQuad)
            )
            .AppendInterval(0.5f)
            .Append(
                _healthImage.DOFillAmount(targetFillAmount, _duration)
                    .SetEase(Ease.OutQuad)
            );*/
    }


    
}
