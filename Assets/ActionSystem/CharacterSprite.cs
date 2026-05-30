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



        return _default;
    }
    
}

public enum Emotion
{
    None,
    Happy
}