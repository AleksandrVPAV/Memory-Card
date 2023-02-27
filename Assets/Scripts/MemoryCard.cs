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

    public void SetCard(int id, Sprite imageFrontCard)      //принимаем рандомный id из другого скрипта
    {
        _id = id;                                           
        GetComponent<SpriteRenderer>().sprite = imageFrontCard;     //обращаемся к SpriteRenderer данного обьекта и выставляем в поле sprite нашу карту из поля 
    }
    public void OnMouseDown()
    {
       if (_cardBack.activeSelf && _controller.canReveal)            // обьект активен по умолчанию и проверка свойства, которое должно быть  = null
        {
            _cardBack.SetActive(false);     // делаем обьект неактивным
            _controller.CardRevealed(this);
        }
    }

    public void Unreveal()
    {
        _cardBack?.SetActive(true);
    }
}
