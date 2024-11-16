using NUnit.Framework.Constraints;
using System.Collections;
using UnityEngine;

public class HoveringTrophy : MonoBehaviour
{
    void Start()
    {
        float origin = this.gameObject.transform.localPosition.y;

        LeanTween.moveLocalY(this.gameObject, origin +0.3f, 1f).setLoopPingPong().setEase(LeanTweenType.easeInOutSine);
    }
}
