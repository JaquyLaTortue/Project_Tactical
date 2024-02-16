using DG.Tweening;
using UnityEngine;

public class UIPlayerScale : MonoBehaviour
{
    private Vector3 _orignalScale;
    private Vector3 _scaleTo;

    private int truc;

    [SerializeField]
    private GameObject _player1;
    [SerializeField]
    private GameObject _player2;
    [SerializeField]
    private GameObject _player3;
    [SerializeField]
    private GameObject _player4;

    public void Player1()
    {
        /*_orignalScale = transform.localScale;
        _scaleTo = _orignalScale * 1.5f;

        transform.DOScale(_scaleTo, 0.5f)
            .SetEase(Ease.InOutSine);    */

        _player1.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f);
        _player2.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);
        _player3.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);
        _player4.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);


        _player1.transform.position = new Vector3(70, 1010, 0);
        _player2.transform.position = new Vector3(45, 925, 0);
        _player3.transform.position = new Vector3(45, 850, 0);
        _player4.transform.position = new Vector3(45, 775, 0);

        /* _player1.transform.localScale = new Vector3(1.5f, 1.5f, 0);
         _player2.transform.localScale = new Vector3(1, 1, 0);
         _player3.transform.localScale = new Vector3(1, 1, 0);
         _player4.transform.localScale = new Vector3(1, 1, 0);

         _player1.transform.position = new Vector3(70, 1010, 0);
         _player2.transform.position = new Vector3(45, 925, 0);
         _player3.transform.position = new Vector3(45, 850, 0);
         _player4.transform.position = new Vector3(45, 775, 0);*/
    }

    public void Player2()
    {
        _player1.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);
        _player2.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f);
        _player3.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);
        _player4.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);


        _player1.transform.position = new Vector3(45, 1030, 0);
        _player2.transform.position = new Vector3(70, 930, 0);
        _player3.transform.position = new Vector3(45, 845, 0);
        _player4.transform.position = new Vector3(45, 770, 0);

        /* _player1.transform.localScale = new Vector3(1, 1, 0);
         _player2.transform.localScale = new Vector3(1.5f, 1.5f, 0);
         _player3.transform.localScale = new Vector3(1, 1, 0);
         _player4.transform.localScale = new Vector3(1, 1, 0);*/
    }

    public void Player3()
    {
        _player1.transform.position = new Vector3(45, 1030, 0);
        _player2.transform.position = new Vector3(45, 955, 0);
        _player3.transform.position = new Vector3(70, 855, 0);
        _player4.transform.position = new Vector3(45, 775, 0);

        _player1.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);
        _player3.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f);
        _player2.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);
        _player4.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);

        /*_player1.transform.localScale = new Vector3(1, 1, 0);
        _player2.transform.localScale = new Vector3(1, 1, 0);
        _player3.transform.localScale = new Vector3(1.5f, 1.5f, 0);
        _player4.transform.localScale = new Vector3(1, 1, 0);*/
    }

    public void Player4()
    {
        _player1.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);
        _player4.transform.DOScale(new Vector3(1.5f, 1.5f, 1.5f), 0.5f);
        _player2.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);
        _player3.transform.DOScale(new Vector3(1f, 1f, 1f), 0.5f);

        _player1.transform.position = new Vector3(45, 1030, 0);
        _player2.transform.position = new Vector3(45, 955, 0);
        _player3.transform.position = new Vector3(45, 880, 0);
        _player4.transform.position = new Vector3(70, 780, 0);

        /* _player1.transform.localScale = new Vector3(1, 1, 0);
         _player2.transform.localScale = new Vector3(1, 1, 0);
         _player3.transform.localScale = new Vector3(1, 1, 0);
         _player4.transform.localScale = new Vector3(1.5f, 1.5f, 0);

         */
    }

}
