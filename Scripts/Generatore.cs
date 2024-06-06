using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Generatore : MonoBehaviour
{
    public int nDadi;
    public int nFacce;
    public int nTiri;
    public float maxTreshold;
    public float idleDeltaTime;
    public bool idle;

    [SerializeField] List<GameObject> instantiatedSliders = new List<GameObject>();

    [SerializeField] TextMeshProUGUI idleIndicator;
    [SerializeField] TMP_InputField inputDadi;
    [SerializeField] TMP_InputField inputFacce;
    [SerializeField] TMP_InputField inputTiri;
    [SerializeField] GameObject sliderParent;
    [SerializeField] GameObject sliderPrefab;
    [SerializeField] int[] debugInt;

    void Start()
    {
        
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            idle = !idle;
            if (idle)
            {
                StartCoroutine(IdleState());
            }
            else
            {
                StopCoroutine(IdleState());
            }
            idleIndicator.gameObject.SetActive(idle);
        }

        nDadi = int.Parse(inputDadi.text);
        nFacce = int.Parse(inputFacce.text);
        nTiri = int.Parse(inputTiri.text);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            TiraTuttiIDadi();
        }
        
        
    }


    public int TiraDado(int n)
    {
        return Random.Range(1, n + 1);
    }

    public void TiraTuttiIDadi()
    {
        int[] numeriUsciti = new int[nDadi * nFacce - nDadi + 1];

        for (int i = 0; i < nTiri; i++)
        {
            int tot = 0;
            for (int j = 0; j < nDadi; j++)
            {
                tot += TiraDado(nFacce);
            }

            numeriUsciti[tot - nDadi]++;
        }

        foreach (GameObject item in instantiatedSliders)
        {
            Destroy(item);
        }

        instantiatedSliders.RemoveRange(0, instantiatedSliders.Count);

        int maxValue = maxValueInArray(numeriUsciti);

        for (int i = 0; i < numeriUsciti.Length; i++)
        {
            instantiatedSliders.Add(Instantiate(sliderPrefab, sliderParent.transform));
            instantiatedSliders[i].GetComponent<Slider>().maxValue = maxValue * maxTreshold;
            instantiatedSliders[i].GetComponent<Slider>().value = numeriUsciti[i];
            instantiatedSliders[i].GetComponent<TextRef>().textValore.text = (i + nDadi).ToString();
            instantiatedSliders[i].GetComponent<TextRef>().textPercentuale.text = (Mathf.Floor((float)numeriUsciti[i] * 10000 / nTiri)/100).ToString() + "%";
        }
    }

    public int maxValueInArray(int[] array)
    {
        int maxValue = 0;
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] > maxValue)
            {
                maxValue = array[i];
            }
        }
        return maxValue;
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator IdleState()
    {
        while (idle)
        {
            nDadi = Random.Range(2, 11);
            inputDadi.text = nDadi.ToString();
            nFacce = 6;
            inputFacce.text = nFacce.ToString();
            nTiri = (int)Mathf.Pow(10, Random.Range(5, 7));
            inputTiri.text = nTiri.ToString();
            TiraTuttiIDadi();
            yield return new WaitForSeconds(idleDeltaTime);
        }
    }
}
