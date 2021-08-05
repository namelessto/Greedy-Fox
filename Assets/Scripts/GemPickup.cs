using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemPickup : MonoBehaviour
{
    [SerializeField] int gemValue = 100;
    [SerializeField] Animator animator;
    [SerializeField] GameObject gem;


    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (ContactPoint2D point in collision.contacts)
            {
                if (point.normal.x >= 0.95f)
                {
                    gem.GetComponent<Collider2D>().enabled = false;
                    ScoreManager.instance.ChangeScore(gemValue);
                    Destroy(this.gameObject, 0.225f);
                    animator.SetBool("IsPick", true);
                }
            }
        }
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player_Collect")
        {
            AudioManager.instance.Play("GemPickUp");
            gem.GetComponent<Collider2D>().enabled = false;
            ScoreManager.instance.ChangeScore(gemValue);
            Destroy(this.gameObject, 0.225f);
            animator.SetBool("IsPick", true);
        }
    }
}
