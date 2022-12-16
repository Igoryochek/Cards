using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Card _cardPrefab;
    [SerializeField] private int _minCardsCount = 4;
    [SerializeField] private int _maxCardsCount = 6;
    [SerializeField] private TextMeshProUGUI _center;
    [SerializeField] private TextMeshProUGUI _spawningPlace;
    [SerializeField] private Canvas _canvas;
    [SerializeField] private float _spawningSpeed = 0.5f;
    [SerializeField] private float _angleBeetweenCards;
    [SerializeField] private float _arcRadius;
    [SerializeField] private float _arcAngle;
    [SerializeField] private int _minHitForceValue;
    [SerializeField] private int _maxHitForceValue;

    private float _startArcAngle;
    private List<Card> _cards = new List<Card>();

    private void Start()
    {
        _startArcAngle = _arcAngle;
        HandOutCards();
    }

    private void HandOutCards()
    {
        StartCoroutine(HandingOutCards());
    }

    private IEnumerator HandingOutCards()
    {
        int randomCount = Random.Range(_minCardsCount, _maxCardsCount + 1);

        for (int i = 0; i < randomCount; i++)
        {
            Card card = Instantiate(_cardPrefab, _spawningPlace.transform.position, Quaternion.identity);
            card.transform.SetParent(_canvas.transform, true);
            _cards.Add(card);
            card.transform.position = _spawningPlace.transform.position;
            yield return new WaitForSeconds(_spawningSpeed);
        }
        PlaceCards();
    }

    private void PlaceCards()
    {
        float increment = _angleBeetweenCards / _cards.Count;
        for (int i = 0; i < _cards.Count; i++)
        {
            Vector2 position;
            position.x = _center.transform.position.x + _arcRadius * Mathf.Sin(_arcAngle * Mathf.Deg2Rad);
            position.y = _center.transform.position.y + _arcRadius * Mathf.Cos(_arcAngle * Mathf.Deg2Rad);
            _cards[i].MoveTo(position, -_arcAngle);
            _arcAngle += increment;
        }
        _arcAngle = _startArcAngle;
    }

    private Card NonHitedCard()
    {
        Card card = _cards.FirstOrDefault(card => card.IsHited == false);
        if (card == null)
        {
            foreach (var item in _cards)
            {
                item.ResetHitStatus();
            }
            return _cards[0];
        }
        return card;
    }

    public void TakeHit()
    {
        int randomHitForceValue = Random.Range(_minHitForceValue, _maxHitForceValue);
        Card card = NonHitedCard();
        card.TakeDamage(randomHitForceValue);
        if (card.Health <= 0)
        {
            _cards.Remove(card);
            Destroy(card.gameObject);
            if (_cards.Count == 0)
            {
                HandOutCards();
            }
            else
            {
                PlaceCards();
            }
        }
    }
}
