using UnityEngine;

[CreateAssetMenu]
public class CardInfo : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private int _mana;
    [SerializeField] private int _health;
    [SerializeField] private int _damage;
    [SerializeField] private Sprite _icon;

    public string Name => _name;
    public string Description => _description;
    public int Mana => _mana;
    public int Health => _health;
    public int Damage => _damage;
    public Sprite Icon => _icon;
}
