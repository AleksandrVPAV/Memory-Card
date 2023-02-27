using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryCard : MonoBehaviour
{

    [SerializeField] private GameObject _cardBack;
    private SceneController _controller;

    private int _id;    
    public int id  {  get  { return _id;} }

    
    private void Start()
    {
        _controller = FindObjectOfType<SceneController>();
    }

    public void SetCard(int id, Sprite imageFrontCard)      //��������� ��������� id �� ������� �������
    {
        _id = id;                                           
        GetComponent<SpriteRenderer>().sprite = imageFrontCard;     //���������� � SpriteRenderer ������� ������� � ���������� � ���� sprite ���� ����� �� ���� 
    }
    public void OnMouseDown()
    {
       if (_cardBack.activeSelf && _controller.canReveal)            // ������ ������� �� ��������� � �������� ��������, ������� ������ ����  = null
        {
            _cardBack.SetActive(false);     // ������ ������ ����������
            _controller.CardRevealed(this);
        }
    }

    public void Unreveal()
    {
        _cardBack?.SetActive(true);
    }
}
