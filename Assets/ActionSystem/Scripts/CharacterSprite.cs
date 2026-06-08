using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Carochito/Criar novo Personagem")]
public class CharacterSprite : ScriptableObject
{
    [Header("Base Infor")]
    public Sprite _baseSprite;
    public string _name;

    [Header("Emotions")]
    public Sprite _default;
    public Sprite _happy;
    public Sprite _sad;
    public Sprite _angry;
    public Sprite _surprised;

    public Sprite SelectEmotion(Emotion emotion)
    {
        if (emotion == Emotion.None)
        {
            return _default;
        }

        if (emotion == Emotion.Happy)
        {
            return _happy;
        }

        if (emotion == Emotion.Angry)
        {
            return _angry;
        }

        if (emotion == Emotion.Sad)
        {
            return _sad;
        }

        if (emotion == Emotion.Surprised)
        {
            return _surprised;
        }


        return _default;
    }
    
}

public enum Emotion
{
    None,
    Happy,
    Angry,
    Sad,
    Surprised
}

public enum Side
{
    Left,
    Right
}

