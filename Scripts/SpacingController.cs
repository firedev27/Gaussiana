using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpacingController : MonoBehaviour
{
    VerticalLayoutGroup verticalLayoutGroup;
    [SerializeField] float spaceCostant;

    void Start()
    {
        verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();
    }

    void Update()
    {
        verticalLayoutGroup.spacing = spaceCostant / transform.hierarchyCount;
    }
}
