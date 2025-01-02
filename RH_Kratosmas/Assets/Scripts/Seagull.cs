using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seagull : MonoBehaviour
{
    public Animator animator;
    private float moveSpeed = 5f;
    private float turnSpeed = 60f;
    private float yaw = 0f;
    private float maxYaw = 60f;
    private float yawMultiplier = 80f;
    private bool doneFlap = false;
    public GameObject fishprefab;
    private int numfish = 0;
    private float fishRange = 30;
    private float fishOffset = 0.12f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetAnimator());
    }

    #region Update
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        float horiz = Input.GetAxis("Horizontal");
        float vert = Input.GetAxis("Vertical");

        if (animator)
        {
            animator.SetBool("InFlight", true);
            animator.SetBool("Flap", true);
           
            if (vert == 0)
            {
                float angle = Vector3.Angle(Vector3.up, transform.forward);
                if (angle < 90)
                {
                    animator.SetBool("Flap", true);
                    animator.SetBool("EffortfulFlap", true);
                }
                else if(angle >= 90)
                {
                    animator.SetBool("Flap", false);
                    animator.SetBool("EffortfulFlap", false);
                }
            }
            else if(vert > 0)
            {
                animator.SetBool("Flap", false);
                animator.SetBool("EffortfulFlap", false);
            }
            else if (vert < 0)
            {
                animator.SetBool("Flap", true);
                animator.SetBool("EffortfulFlap", true);
            }
        }

        if (horiz > 0 && yaw < maxYaw)
        {
            yaw += Time.deltaTime * yawMultiplier;
        }
        else if(horiz < 0 && yaw > -maxYaw)
        {
            yaw -= Time.deltaTime * yawMultiplier;
        }
        else
        {
            if (yaw > -1 && yaw < 1)
            {
                yaw = 0f;
            }
            else if (yaw > 0)
            {
                yaw -= Time.deltaTime * yawMultiplier;
            }
            else if (yaw < 0)
            {
                yaw += Time.deltaTime * yawMultiplier;
            }
        }
        //transform.Rotate(vert * turnSpeed * Time.deltaTime, horiz * turnSpeed * Time.deltaTime, 0);
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x + (vert * turnSpeed * Time.deltaTime), transform.eulerAngles.y + ((yaw/maxYaw) * turnSpeed * Time.deltaTime), -yaw);
        if(transform.eulerAngles.x > 88 && transform.eulerAngles.x < 100)
        {
            transform.rotation = Quaternion.Euler(88, transform.eulerAngles.y, transform.eulerAngles.z);
        }
        else if (transform.eulerAngles.x < 272 && transform.eulerAngles.x > 260)
        {
            transform.rotation = Quaternion.Euler(272, transform.eulerAngles.y, transform.eulerAngles.z);
        }
    }
    #endregion

    IEnumerator GetAnimator()
    {
        while(transform.childCount < 1)
        {
            yield return null;
        }
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.transform.CompareTag("Fish"))
        {
            RepositionFish(collision.collider.transform.parent);
            AddFish();
        }
    }

    void AddFish()
    {
        GameObject caughtfish = Instantiate(fishprefab, transform.position - transform.up * (0.27f + fishOffset * numfish) - transform.forward * 0.24f, transform.rotation * fishprefab.transform.rotation);
        caughtfish.transform.SetParent(transform);
        numfish++;
    }

    void RepositionFish(Transform fishParent)
    {
        fishParent.position = new Vector3(Random.Range(-fishRange, fishRange), 5f, 60f + Random.Range(-fishRange, fishRange));
    }
}
