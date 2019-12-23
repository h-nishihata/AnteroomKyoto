using UnityEngine;

public class TextBehaviour : MonoBehaviour
{
    private float elapsedTime;
    private float displayTimeOut = 1f;

    void OnEnable()
    {
        elapsedTime = 0f;
    }

    void Update()
    {
        if (elapsedTime < displayTimeOut)
            elapsedTime += Time.deltaTime;
        else
            this.gameObject.SetActive(false);
    }
}
