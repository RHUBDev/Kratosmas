using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public Animator animator;
    public int animnum = 9;

    /*private void Start()
    {
        Screen.SetResolution(960, 600, false);
    }*/

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("Animation_int", animnum);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
}
