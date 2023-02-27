using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneController : MonoBehaviour
{
    [SerializeField] private MemoryCard _originalCard;
    [SerializeField] private Sprite[] _frontCards;

    private const int gridRows = 2;
    private const int gridCols = 4;
    private const float offsetX = 1.5f;
    private const float offsetY = 2f;         // высьраиваем сетку значений (на этой сетке будут распологаться карты)
    private MemoryCard _firstRevealed;
    private MemoryCard _secondRevealed;
    private int _score = 0;

    public int Score { get { return _score; } }
    public bool canReveal { get { return _secondRevealed == null; } }

    
    private void Start()
    {
        Vector3 startPos = _originalCard.transform.position;

        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3 };
        numbers = ShuffleArray(numbers);

        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MemoryCard card = new MemoryCard();
                card = Instantiate(_originalCard);

                int index = j * gridCols + i;
                int id = numbers[index];
                card.SetCard(id, _frontCards[id]);     // вызов метода из скрипта MemoryCard

                float posX = (offsetX * i) + startPos.x;
                float posY = (offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }

    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r]= tmp;
        }
        return newArray;
    }
    public void CardRevealed(MemoryCard memoryCard)
    {
        if (_firstRevealed == null)
        {
            _firstRevealed = memoryCard;
        }
        else
        {
            _secondRevealed = memoryCard;
            StartCoroutine(CheckMatch());
        }
    }

    private IEnumerator CheckMatch()
    {
        if (_firstRevealed.id == _secondRevealed.id)
        {
            _score++;
            DestoyCard(_firstRevealed);
            DestoyCard(_secondRevealed);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);

            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
        }

        _firstRevealed = null;
        _secondRevealed = null;
    }

    public void Restart()
    {
        SceneManager.LoadScene("SampleScene");
    }

    private void DestoyCard(MemoryCard memoryCard)
    {
        float duration = 1;

        DOTween.Sequence()
            .Append(memoryCard.transform.DOScale(new Vector3(0.6f, 0.6f, 0), duration))
            .AppendInterval(0.5f)
            .Append(memoryCard.transform.DOScale(Vector3.zero, duration))
            .OnComplete(() => Destroy(memoryCard.gameObject));
    }
}
