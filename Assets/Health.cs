using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Text healthText;
    public Image healthBar;
    public float health = 100f;
    Transform camTransform;
    public Transform UI;
    public float moveSpeed = 10f;


    // Start is called before the first frame update
    void Start()
    {
        camTransform = Camera.main.transform;
    }

    void Update()
    {
        Vector3 lookDirection = (UI.position - camTransform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(lookDirection);

        UI.rotation = Quaternion.Lerp(UI.rotation, lookRotation, Time.deltaTime * moveSpeed);
    }
    public void TakeDemage(int amount)
    {
        if (health < amount) return;
        StartCoroutine(TakeDemageSomoothly(amount));

    }

    private IEnumerator TakeDemageSomoothly(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            health--;
            healthText.text = health.ToString();
            healthBar.fillAmount = health / 100f;
            yield return null;
        }
    }
}
