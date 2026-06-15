using UnityEngine;
using System.Collections;

public class DiscCapture : MonoBehaviour
{
    public Carochito capturedCarochito;
    public GameObject modelCarochito;

    [Header("Capture Settings")]

    [Tooltip("Delay between each bounce.")]
    public float bounceDelay = 0.8f;

    [Header("Animator")]
    public Animator _animator;

    private bool captureSuccess;

    public void SetUp(Carochito pokemon, GameObject pokemonModel)
    {
        Debug.Log("Set Up");

        capturedCarochito = pokemon;
        modelCarochito = pokemonModel;

        // Adicionar calculos de captura
        captureSuccess = true;

        StartCoroutine(CaptureRoutine());
    }

    public void StartCapture()
    {
        StartCoroutine(CaptureRoutine());
    }

    private IEnumerator CaptureRoutine()
    {
        int failBounce = -1;

        if (!captureSuccess)
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
        AlertManager.instance.AddAlert(capturedCarochito, alert);

        Party.Instance.AddCarochito(capturedCarochito);
        Destroy(gameObject);
    }

    private void PokemonEscaped()
    {
        string alert = capturedCarochito.Name + " escapou!";
        AlertManager.instance.AddAlert(capturedCarochito, alert);

        Instantiate(modelCarochito, transform);
    }

    private bool CalculateCaptureChance(Carochito pokemon)
    {
        // ----------------------------------------
        // Insert pokemon catch-rate formula here.
        // HP
        // Status effects
        // Ball type
        // Legendary modifiers
        // etc.
        // ----------------------------------------

        return Random.value <= 0.5f;
    }
}  

