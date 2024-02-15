using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine.TextCore.Text;

public class NewBehaviourScript : MonoBehaviour
{
    public float health = 75f;
    public float maxHealth = 100f;

    public int _duration;

    public Image healthBarImage;
    public Image healthBarImage1;

    //public TextMeshProUGUI healthText;

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0f, maxHealth);
    }

    public void DamageButton(int damageAmount)
    {
        health -= damageAmount;
        truc();
    }
    void truc()
    {
        float targetFillAmount = Mathf.InverseLerp(0, maxHealth, health);

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
