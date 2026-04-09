using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Carochitos/New Item")]
public class Item : ScriptableObject
{
    [SerializeField] string _name;
    [SerializeField] string _namePlural;
    [SerializeField] string _namePortion;
    [SerializeField] string _namePortionPlural;
    [SerializeField] string _description;

    [SerializeField] Pocket _pocket;
    [SerializeField] int _price;

    public string Name { get { return _name; } }
    public string NamePlural { get { return _namePlural; } }
    public string NamePortion { get { return _namePortion; } }
    public string NamePortionPlural { get { return _namePortionPlural; } }
    public string Description { get { return _description; } }
    public Pocket Pocket { get { return _pocket; } }
    public int Price { get { return _price; } }
}
