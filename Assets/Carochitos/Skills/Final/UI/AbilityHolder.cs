using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public GameObject AbilityIconPrefab;

    public void SetUp(Carochito carochito)
    {
        // Destroy all Children
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        // Create Icons
        foreach (Skill skill in carochito.Skill)
        {
            GameObject Icon = Instantiate(AbilityIconPrefab, transform);

            Icon.GetComponent<IconAbility>().SetUp(skill);
        }
    }
}
