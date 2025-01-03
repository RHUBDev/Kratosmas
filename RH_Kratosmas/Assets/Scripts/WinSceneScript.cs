using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WinSceneScript : MonoBehaviour
{
    public GameObject[] cameras;
    public TMP_Text endtext;
    public TMP_Text screentext;
    public TMP_Text wintext;
    public Transform turningCharacter;
    private float lerpTime = 1f;
    private float lerpTimer = 0f;
    private float colorLerpTime = 1.5f;
    private float colorLerpTimer = 0f;
    private Quaternion startrot;
    private Quaternion endrot = Quaternion.Euler(0, -93, 0);
    private Color startcolor = new Color(1, 1, 1, 1);
    private Color endcolor = new Color(1, 1, 1, 0);
    public Animator animator;
    private int animnum = 0;
    private int animnum2 = 6;
    private bool cutSceneEnded = false;

    // Start is called before the first frame update
    void Start()
    {
        startrot = turningCharacter.rotation;
        endrot *= startrot;
        StartCoroutine(DoScene());
    }

    // Update is called once per frame
    void Update()
    {
        //animator.SetInteger("Animation_int", animnum);
        if (cutSceneEnded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("MenuScene");
            }
        }
    }

    IEnumerator DoScene()
    {
        yield return new WaitForSeconds(1);
        
        while (colorLerpTimer < 1f)
        {
            //Debug.Log("ColorLerp = " + colorLerpTimer);
            colorLerpTimer += (1 / colorLerpTime) * Time.deltaTime;
            endtext.color = Color.Lerp(startcolor, endcolor, colorLerpTimer);
            yield return null;
        }

        cameras[1].SetActive(true);
        cameras[0].SetActive(false);
        endtext.text = "";
        yield return new WaitForSeconds(0.5f);
        screentext.text = "'Is that a giant pile of fish?'";
        yield return new WaitForSeconds(0.5f);

        while (lerpTimer < 1f)
        {
            lerpTimer += (1 / lerpTime) * Time.deltaTime;
            turningCharacter.rotation = Quaternion.Lerp(startrot, endrot, lerpTimer);
            yield return null;
        }

        yield return new WaitForSeconds(2f);
        screentext.text = "";
        cameras[2].SetActive(true);
        cameras[1].SetActive(false);
        animator.SetInteger("Animation_int", animnum2);
        yield return new WaitForSeconds(1f);
        animator.SetInteger("Animation_int", animnum);
        wintext.text = "Give a man the most awesome pet seagull ever, and he will live life to the fullest!\n(4+ Fish)";
        yield return new WaitForSeconds(2f);
        screentext.fontSize = 40f;
        screentext.text = "Press Spacebar To Return To Menu";
        cutSceneEnded = true;
    }
}
