using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class screenmanager : MonoBehaviour {

    public Animator initiallyOpen;
    private Animator mCurrent;

    private int mStateID;

    private const string kIdleStateName = "idle";
        private const string kOnStateName = "on";
        private const string kOffStateName = "off";

    public void OnEnable()
    {
        //We cache the Hash to the "Open" Parameter, so we can feed to Animator.SetBool.
            mStateID = Animator.StringToHash ("State");

        //If set, open the initial Screen now.
//        if (initiallyOpen == null)
//            return;
            OpenPanel(initiallyOpen);
    }

    //Closes the currently open panel and opens the provided one.
    //It also takes care of handling the navigation, setting the new Selected element.
    public void OpenPanel (Animator anim)
    {
        if (mCurrent == anim)
            return;

        //Activate the new Screen hierarchy so we can animate it.
//        anim.gameObject.SetActive(true);
        //Save the currently selected button that was used to open this Screen. (CloseCurrent will modify it)
//        var newPreviouslySelected = EventSystem.current.currentSelectedGameObject;
        //Move the Screen to front.
//        anim.transform.SetAsLastSibling();

        CloseCurrent();

//        m_PreviouslySelected = newPreviouslySelected;

        //Set the new Screen as then open one.
        mCurrent = anim;
        //Start the open animation

            mCurrent.SetInteger(mStateID,1);

        //Set an element in the new screen as the new Selected one.
//        GameObject go = FindFirstEnabledSelectable(anim.gameObject);
//        SetSelected(go);
    }

        public void CloseCurrent()
    {
        if (mCurrent == null)
            return;

        //Start the close animation.
            mCurrent.SetInteger(mStateID,2);

        //Reverting selection to the Selectable used before opening the current screen.
//        SetSelected(m_PreviouslySelected);
        //Start Coroutine to disable the hierarchy when closing animation finishes.
        StartCoroutine(DisablePanelDeleyed(mCurrent));
        //No screen open.
        mCurrent = null;
    }

        IEnumerator DisablePanelDeleyed(Animator anim)
    {
        bool closedStateReached = false;
        bool wantToClose = true;
        while (!closedStateReached && wantToClose)
        {
            if (!anim.IsInTransition(0))
                    closedStateReached = anim.GetCurrentAnimatorStateInfo(0).IsName(kOffStateName);

            wantToClose = anim.GetInteger(mStateID) == 2; //anim.GetBool(m_OpenParameterId);

            yield return new WaitForEndOfFrame();
        }

        if (wantToClose)
                mCurrent.SetInteger(mStateID,0);
            //anim.gameObject.SetActive(false);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
