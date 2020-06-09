using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveElement : MonoBehaviour
{
    public List<ActiveElement> activateOther = new List<ActiveElement>();
    public bool active;

    public bool canBeActivated = true;
    public bool canBeDeactivated = true;

    public Animator animator;

    public void Activate()
    {
        if (canBeActivated)
        {
            foreach (ActiveElement element in activateOther)
            {
                element.Activate();
            }
            active = true;
            Action();
            if (animator) animator.SetBool("Activate", true);
        }
    }

    public void Deactivate()
    {
        if (canBeDeactivated)
        {
            foreach (ActiveElement element in activateOther)
            {
                element.Deactivate();
            }
            active = false;
            Action();
            if (animator) animator.SetBool("Activate", false);
        }
    }

    public virtual void Action() { }
}
