using UnityEngine;
using System.Collections;
using System.Linq.Expressions;

public class DiscCapture : MonoBehaviour
{
    public Carochito capturedCarochito;
    public GameObject modelCarochito;

    [Header("Capture Settings")]
    public float bounceDelay = 0.8f;

    [Header("Animator")]
    public Animator _animator;

    private bool captureSuccess;

    public void SetUp(Carochito pokemon, GameObject pokemonModel)
    {
        Debug.Log("Set Up");

        capturedCarochito = pokemon;
        modelCarochito = pokemonModel;

        // Calculate Catch Rate
        captureSuccess = true;

        int catchOpportunity = Random.Range(1, 100 + 1);
        Debug.Log("" + catchOpportunity);

        if (catchOpportunity > pokemon.CurrentCatchRate)
        {
            Debug.Log("Vai Falhar");
            captureSuccess = false;
        }

        StartCoroutine(CaptureRoutine());
    }

    public void StartCapture()
    {
        StartCoroutine(CaptureRoutine());
    }

    private IEnumerator CaptureRoutine()
    {
        int failBounce = -1;

        if (captureSuccess == false)
        {
            failBounce = Random.Range(1, 3); // 1 or 2
        }

        // Bounce 1
        yield return Bounce(1);

        if (failBounce == 1)
        {
            PokemonEscaped();
            yield break;
        }

        // Bounce 2
        yield return Bounce(2);

        if (failBounce == 2)
        {
            PokemonEscaped();
            yield break;
        }

        // Bounce 3
        yield return Bounce(3);

        // Capture Success
        PokemonCaptured();
    }

    private IEnumerator Bounce(int bounceNumber)
    {
        Debug.Log($"Bounce {bounceNumber}");

        _animator.SetTrigger("bounce");

        yield return new WaitForSeconds(bounceDelay);
    }

    private void PokemonCaptured()
    {
        string alert = capturedCarochito.Name + " foi capturado!";
        AlertManager.instance.AddAlert(capturedCarochito.Base.Sprite, alert);

        Party.Instance.AddCarochito(capturedCarochito);

        StartCoroutine(EndCapture());
    }

    private void PokemonEscaped()
    {
        string alert = capturedCarochito.Name + " escapou!";
        AlertManager.instance.AddAlert(capturedCarochito.Base.Sprite, alert);

        GameObject currentEnemy = Instantiate(capturedCarochito.Base.Model, transform.position, transform.rotation);
        currentEnemy.GetComponent<CarochitoBattler>().SetUp(capturedCarochito);

        StartCoroutine(EndCapture());
    }
    private IEnumerator EndCapture()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    private bool CalculateCaptureChance(Carochito pokemon)
    {

        return Random.value <= 0.5f;
    }
}  

