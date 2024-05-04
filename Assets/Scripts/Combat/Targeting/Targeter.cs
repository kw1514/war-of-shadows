using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Targeter : MonoBehaviour
{
    [SerializeField] private CinemachineTargetGroup cineTargetGroup;

    private Camera mainCamera;
    private List<Target> targets = new List<Target>();
    public Target CurrentTarget { get; private set; }

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Target>(out Target target)) { return; }
        targets.Add(target);
        target.OnDestroyed += RemoveTarget;
    }

    private void OnTriggerExit(Collider other)
    {
        // same as Target target = other.GetComponent<Target>(); if (target == null) { return; }
        if (!other.TryGetComponent<Target>(out Target target)) { return; }
        
        RemoveTarget(target);
    }

    public bool SelectTarget()
    {
        if (targets.Count == 0) { return false; } // no targets exist

        Target closetTarget = null;
        float closetTargetDistance = Mathf.Infinity;

        foreach(Target target in targets) // runs through to find target closet on screen to the player
        {
            Vector2 viewPos = mainCamera.WorldToViewportPoint(target.transform.position);

            if(!target.GetComponentInChildren<Renderer>().isVisible)
            {
                // not on the screen continue to next target
                continue;
            }

            Vector2 toCenter = viewPos - new Vector2(0.5f, 0.5f); // distance from center of screen
            if(toCenter.sqrMagnitude < closetTargetDistance)
            {
                closetTarget = target;
                closetTargetDistance = toCenter.sqrMagnitude;
            }
        }

        if (closetTarget == null) { return false; } // no targets on screen

        CurrentTarget = closetTarget;
        cineTargetGroup.AddMember(CurrentTarget.transform, 1f, 2f);

        return true;
    }

    public void Cancel()
    {
        if (CurrentTarget == null) { return; }

        cineTargetGroup.RemoveMember(CurrentTarget.transform);
        CurrentTarget = null;
    }

    private void RemoveTarget(Target target)
    {
        if (CurrentTarget == target)
        {
            cineTargetGroup.RemoveMember(CurrentTarget.transform);
            CurrentTarget = null;
        }

        target.OnDestroyed -= RemoveTarget;
        targets.Remove(target);
    }
}
