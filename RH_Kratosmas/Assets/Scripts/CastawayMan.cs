using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CastawayMan : MonoBehaviour
{
    public Transform hand;
    public GameObject fishprefab;
    public TMP_Text fishText;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            if(other.transform.childCount > 1)
            {
                for (int i = 0; i < other.transform.childCount - 1; i++)
                {
                    GameObject caughtfish = Instantiate(fishprefab, hand.position, hand.rotation * Quaternion.Euler(0, Random.Range(0, 360), 0));
                    caughtfish.transform.SetParent(hand);
                    Destroy(other.transform.GetChild(i+1).gameObject);
                }
                fishText.text = "Fish Delivered: " + hand.childCount;
            }
        }
    }
}
