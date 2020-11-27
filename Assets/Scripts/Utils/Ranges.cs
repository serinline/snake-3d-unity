using System.Collections;
using UnityEngine;

public class Ranges : MonoBehaviour
{

    public static Ranges ranges;
    public GameObject new_fruit;

    void Start()
    {
        Invoke(nameof(StartCreatingNewFruit), 1f);
    }

    void StartCreatingNewFruit()
    {
        StartCoroutine(NewFrits());
    }

    IEnumerator NewFrits()
    {
        yield return new WaitForSeconds(1f);
        Instantiate(new_fruit, new Vector3(Random.Range(-6.78f, 6.7f), Random.Range(-4.5f, 0.86f), 0f), Quaternion.identity);
        Invoke(nameof(StartCreatingNewFruit), 0f);
    }
}
