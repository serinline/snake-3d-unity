using UnityEngine;

public class FruitCreationStopper : MonoBehaviour
{
    void Start()
    {
        Invoke(nameof(StopCreation), Random.Range(5f, 8f));
    }

    void StopCreation()
    {
        gameObject.SetActive(false);
    }
}
