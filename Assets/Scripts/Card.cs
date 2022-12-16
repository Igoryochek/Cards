using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class Card : MonoBehaviour
{
    [SerializeField] private List<CardInfo> _cardInfos;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private TextMeshProUGUI _manaText;
    [SerializeField] private TextMeshProUGUI _healthText;
    [SerializeField] private TextMeshProUGUI _damageText;
    [SerializeField] private Image _icon;
    [SerializeField] private float _speed = 40;

    private string _name;
    private string _description;
    private int _mana;
    private int _health;
    private int _damage;
    private Sprite _iconSprite;
    private bool _isHited = false;

    public bool IsHited => _isHited;
    public int Health => _health;

    private void Start()
    {
        int randomCardIndex = Random.Range(0, _cardInfos.Count - 1);
        CardInfo randomCard = _cardInfos[randomCardIndex];
        _name = randomCard.Name;
        _description = randomCard.Description;
        _mana = randomCard.Mana;
        _health = randomCard.Health;
        _damage = randomCard.Damage;
        _iconSprite = randomCard.Icon;
        DrawCard();
    }

    private void DrawCard()
    {
        _nameText.text = _name;
        _descriptionText.text = _description;
        _manaText.text = _mana.ToString();
        _healthText.text = _health.ToString();
        _damageText.text = _damage.ToString();
        _icon.sprite = _iconSprite;
    }

    public void MoveTo(Vector2 direction, float angle)
    {
        transform.DOMove(direction, _speed);
        transform.DORotate(new Vector3(0, 0, angle), _speed);
    }

    public void TakeDamage(int damage)
    {
        _isHited = true;
        _health -= damage;
        DrawChangedValue(_health, _healthText);
    }

    public void ResetHitStatus()
    {
        _isHited = false;
    }

    private void DrawChangedValue(int value, TextMeshProUGUI text)
    {
        text.text = value.ToString();
    }
}
