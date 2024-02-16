using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Gauge : MonoBehaviour
{
    public ManagerMain ManagerMain;
    public CharacterMain _characterMain;

    /*public float health = 75f;
    public float maxHealth = 100f;*/

    public int _duration;

    public Image healthBarImage;
    public Image healthBarImage1;

    //public TextMeshProUGUI healthText;

    // Update is called once per frame
    void Update()
    {
       // _characterMain.HpCurrent = Mathf.Clamp(_characterMain.HpCurrent, 0, _characterMain.HpMax);
    }

    public void DamageButton(int damageAmount)
    {
        _characterMain.HpCurrent -= damageAmount;
        ChangeGauge();
    }

    public void ChangeGauge()
    {
        _characterMain = ManagerMain.turnManager.Character;
        float targetFillAmount = Mathf.InverseLerp(0, _characterMain.HpMax, _characterMain.HpCurrent);

        DOTween.Sequence()
            .Append(
                healthBarImage.DOFillAmount(targetFillAmount, _duration / 2f)
                    .SetEase(Ease.OutQuad)
            )
            .AppendInterval(0.5f)
            .Append(
                healthBarImage1.DOFillAmount(targetFillAmount, _duration)
                    .SetEase(Ease.OutQuad)
            );

    }
}
