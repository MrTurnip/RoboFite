using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionsCrescent : MonoBehaviour
{
    public Actor targetActor;
    public List<GameObject> subjects;

    // Use this for initialization
    void Start()
    {
        foreach (GameObject subject in subjects)
        {
            Animator animator = subject.GetComponent<Animator>();
            animator.speed = 0;

            Collider2D collider = subject.GetComponent<Collider2D>();
            collider.enabled = false;
        }
    }

    public void RevealOptions()
    {
        foreach (GameObject subject in subjects)
        {
            Animator animator = subject.GetComponent<Animator>();
            animator.speed = 1.0f;

            Collider2D collider2D = subject.GetComponent<Collider2D>();
            collider2D.enabled = true;
        }
    }

    public void HideOptions()
    {
        foreach (GameObject subject in subjects)
        {
            Animator animator = subject.GetComponent<Animator>();
            animator.Play(0, 0, 0);
            animator.speed = 0;

           
            List<Animator> animatorChildren = new List<Animator>(subject.GetComponentsInChildren<Animator>());
            foreach (Animator animatorlette in animatorChildren)
            {
                animatorlette.Play(0, 0, 0);
                animatorlette.speed = 0;
            }

            List<Collider2D> colliderChildren = new List<Collider2D>(subject.GetComponentsInChildren<Collider2D>());
            foreach (Collider2D colliderette in colliderChildren)
            {
                colliderette.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMouseDown()
    {

    }
}
