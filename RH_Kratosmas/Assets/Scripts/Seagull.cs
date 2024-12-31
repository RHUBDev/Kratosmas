using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seagull : MonoBehaviour
{
    public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetAnimator());
        //animator = transform.GetChild(0).GetComponent<Animator>();
        //animator.Play("fly");
    }

    // Update is called once per frame
    void Update()
    {
        if (animator)
        {
            //Debug.Log("1");
            animator.Play("fly");
        }
    }

    IEnumerator GetAnimator()
    {
        while(transform.childCount < 1)
        {
            yield return null;
        }
        animator = transform.GetChild(0).GetComponent<Animator>();
    }
}
