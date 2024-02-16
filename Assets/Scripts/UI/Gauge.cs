using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    [SerializeField]
    private int _duration;

    [SerializeField]
    private Image _healthBarFill;

    [SerializeField]
    private Image _healthBarEmpty;

    public void ChangeGauge(int hpMax, int hpCurrent)
    {
        Debug.Log(hpCurrent + " " + hpMax);
        float targetFillAmount = Mathf.InverseLerp(0, hpMax, hpCurrent);
        //_healthBarFill.DOFillAmount(targetFillAmount, _duration).SetEase(Ease.Linear);
        DOTween.Sequence()
            .Append(
                _healthBarFill.DOFillAmount(targetFillAmount, _duration / 2f)
                    .SetEase(Ease.OutQuad))

            .AppendInterval(0.5f)
            .Append(
                _healthBarEmpty.DOFillAmount(targetFillAmount, _duration)
                    .SetEase(Ease.OutQuad));
    }
}
