using UnityEngine;

public class TextBehaviour : MonoBehaviour
{
    private float elapsedTime;
    public float displayTimeOut = 1f;

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
