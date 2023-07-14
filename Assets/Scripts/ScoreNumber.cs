using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreNumber : MonoBehaviour
{
    public void Initialize(int n)
    {
        StartCoroutine(Activating(n));
    }

    IEnumerator Activating(int n)
    {
        TMPro.TMP_Text Label = GetComponentInChildren<TMPro.TMP_Text>();
        Label.text = n.ToString();
        yield return new WaitForSeconds(0.5f);

        Color col = Label.color;

        while(col.a > 0.0f)
        {
            transform.Translate(Vector3.up * Time.deltaTime * 50.0f); // 점점 올라감
            col.a -= Time.deltaTime;
            Label.color = col; // 점점 투명하게 바뀜
            yield return null;
        }
        Destroy(gameObject);
    }
}
