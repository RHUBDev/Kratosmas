using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
    private float speedMultiplier = 1f;
    private float gametimer = 60f;
    private float gametime = 60f;
    public TMP_Text gameTimeText;
    public TMP_Text endText;
    public Transform hand;
    public bool ended = false;
    private float starttimer = 0f;
    private float starttime = 2f;
    private bool doneonce = false;

    // Start is called before the first frame update
    void Start()
    {
        gametimer = gametime;
        ended = false;
        StartCoroutine(GetAnimator());
        transform.localPosition = new Vector3(-0.927f, 0.389f, 0.391f);
    }

    #region Update
    // Update is called once per frame
    void Update()
    {
        if (starttimer < starttime)
        {
            starttimer += Time.deltaTime;
        }
        else
        {
            if (!doneonce)
            {
                doneonce = true;
                transform.SetParent(null);
                GetComponent<CapsuleCollider>().isTrigger = false;
            }
            if (gametimer > 0)
            {
                gametimer -= Time.deltaTime;
                transform.Translate(Vector3.forward * moveSpeed * speedMultiplier * Time.deltaTime);

                float horiz = Input.GetAxis("Horizontal");
                float vert = Input.GetAxis("Vertical");

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if (speedMultiplier == 1)
                    {
                        speedMultiplier = 3;
                    }
                    else
                    {
                        speedMultiplier = 1;
                    }
                }

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
                        else if (angle >= 90)
                        {
                            animator.SetBool("Flap", false);
                            animator.SetBool("EffortfulFlap", false);
                        }
                    }
                    else if (vert > 0)
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
                else if (horiz < 0 && yaw > -maxYaw)
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
                transform.rotation = Quaternion.Euler(transform.eulerAngles.x + (vert * turnSpeed * Time.deltaTime), transform.eulerAngles.y + ((yaw / maxYaw) * turnSpeed * Time.deltaTime), -yaw);
                if (transform.eulerAngles.x > 88 && transform.eulerAngles.x < 100)
                {
                    transform.rotation = Quaternion.Euler(88, transform.eulerAngles.y, transform.eulerAngles.z);
                }
                else if (transform.eulerAngles.x < 272 && transform.eulerAngles.x > 260)
                {
                    transform.rotation = Quaternion.Euler(272, transform.eulerAngles.y, transform.eulerAngles.z);
                }
            }
            else
            {
                gametimer = 0;
                if (!ended)
                {
                    ended = true;
                    DoEndGame();
                }
            }
        }
        gameTimeText.text = "" + Mathf.CeilToInt(gametimer).ToString("n0");
    }
    #endregion

    IEnumerator GetAnimator()
    {
        while (transform.childCount < 1)
        {
            yield return null;
        }
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    #region CatchFish
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
        fishParent.position = new Vector3(Random.Range(-fishRange, fishRange), 5f, 70f + Random.Range(-fishRange, fishRange));
    }
    #endregion

    #region EndGame
    void DoEndGame()
    {
        if (hand.childCount == 0)
        {
            endText.text = "Another day with no food...\n(0 Fish)";
        }
        else if (hand.childCount == 1)
        {
            endText.text = "Give a man a fish, and you will feed him for a day...\n(1 Fish)";
        }
        else if (hand.childCount > 1 && hand.childCount < 4)
        {
            endText.text = "Give a man a helpful pet seagull, and you will feed him forever!\n(2-3 Fish)";
        }
        else if (hand.childCount >= 4)
        {
            endText.text = "Give a man the most awesome pet seagull ever, and...\n(4+ Fish)";
        }
    }

    public void LoadWinScene()
    {
        if (hand.childCount >= 4)
        {
            SceneManager.LoadScene("WinScene");
        }
        else
        {
            SceneManager.LoadScene("MenuScene");
        }
    }
    #endregion
}
