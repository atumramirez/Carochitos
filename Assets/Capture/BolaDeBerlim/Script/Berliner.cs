using UnityEngine;

[CreateAssetMenu(fileName = "Bola", menuName = "Carochito/Criar novo Item/Berlim")]
public class Berliner : ItemBase
{
    [Header("Model")]
    public GameObject _model;

    [Header("Flavour")]
    public Flavour _flavour;
}

public enum Flavour
{
    Sweet,
    Spicy,
    Sour,
    Salty,
    Fresh
}
