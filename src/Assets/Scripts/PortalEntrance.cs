﻿using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PortalEntrance : MonoBehaviour
{
    [SerializeField]
    private float portalTransferDelay = 0.3f;
    [SerializeField]
    private float portalCooldown = 2.0f;

    private bool portalIsActive = true;

    [SerializeField]
    private GameObject portalEntranceEffect;
    [SerializeField]
    private GameObject portalExitEffect;
    [SerializeField]
    private GameObject portalLight;

    [SerializeField]
    private Transform exit;

    private UnityEvent enterPortal;

    private void Awake()
    {
        if (exit == null)
        {
            Debug.Log("Warning:" + gameObject.name + " does not have an exit portal assigned.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (exit != null && portalIsActive)
        {
            StartCoroutine(ActivatePortal(other));
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (exit != null && portalIsActive)
        {
            StartCoroutine(ActivatePortal(other));
        }
    }

    private IEnumerator ActivatePortal(Collider other)
    {
        if (!portalIsActive)
        {
            yield break;
        }

        portalIsActive = false;
        portalEntranceEffect.SetActive(true);
        portalLight.SetActive(false);
        other.gameObject.SetActive(false);
        yield return new WaitForSeconds(portalTransferDelay);

        other.transform.position = exit.position;
        portalExitEffect.SetActive(true);
        other.gameObject.SetActive(true);
        yield return new WaitForSeconds(portalCooldown);

        enterPortal.Invoke();
        portalEntranceEffect.SetActive(false);
        portalExitEffect.SetActive(false);
        portalLight.SetActive(true);
        portalIsActive = true;
    }

    public UnityEvent EnterPortal
    {
        get
        {
            return enterPortal;
        }
    }
}
