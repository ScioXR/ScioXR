using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK.Highlighters;

public class ZoomHighlighter : VRTK_BaseHighlighter
{

    public float scaleFactor = 1.2f;
    public float offset = 0.02f;
    public float transitionSpeed = 8;

    private Vector3 startScale;
    private Vector3 startPosition;

    private Vector3 targetScale;
    private Vector3 targetPosition;

    private float transitionFactor;

    public enum STATE
    {
        NONE,
        IN,
        OUT,
        ZOOMED,
    }

    private STATE state;

    public STATE GetState()
    {
        return state;
    }

    public override void Initialise(Color? color = null, GameObject affectObject = null, Dictionary<string, object> options = null)
    {
        
    }

    public override void ResetHighlighter()
    {

    }

    public void Start()
    {
        startPosition = gameObject.transform.localPosition;
        targetPosition = gameObject.transform.localPosition + new Vector3(0, 0, offset);

        startScale = gameObject.transform.localScale;
        targetScale = gameObject.transform.localScale * scaleFactor;
    }


    public override void Highlight(Color? color = null, float duration = 0)
    {
        if (state == STATE.NONE)
        {
            transitionFactor = 0;
        }
        state = STATE.IN;
    }

    public override void Unhighlight(Color? color = null, float duration = 0)
    {
        state = STATE.OUT;
    }

    public void Update()
    {
        if (state == STATE.IN || state == STATE.OUT)
        {
            if (state == STATE.IN)
            {
                transitionFactor += Time.deltaTime * transitionSpeed;
            }
            else if (state == STATE.OUT)
            {
                transitionFactor -= Time.deltaTime * transitionSpeed;
            }

            if (transitionFactor > 1)
            {
                transitionFactor = 1;
                state = STATE.ZOOMED;
            }

            if (transitionFactor < 0)
            {
                transitionFactor = 0;
                state = STATE.NONE;
            }

            gameObject.transform.localPosition = Vector3.Lerp(startPosition, targetPosition, transitionFactor);
            gameObject.transform.localScale = Vector3.Lerp(startScale, targetScale, transitionFactor);
        }
    }

}