using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class GroundedTest : MonoBehaviour
{
    // Attached collider
    Collider2D col;

    // Size of the box to test in.
    [SerializeField] Vector2 size;
    // Distance from the ground to be considered "grounded" (keep in mind with sizes that aren't the same as the collider, this won't literally be the distance from the bottom of the collider to the ground)
    [SerializeField] float distance;
    // Layers to check for
    [SerializeField] LayerMask layerMask;

    private void Awake() {
        col = GetComponent<Collider2D>();
    }

    public bool IsGrounded () {
        RaycastHit2D hit = Physics2D.BoxCast(col.bounds.center, size, 0, Vector2.down, distance, layerMask);
        return hit.collider != null;
    }

    public bool IsHittingCeiling () {
        RaycastHit2D hit = Physics2D.BoxCast(col.bounds.center, size, 0, Vector2.up, distance, layerMask);
        return hit.collider != null;
    }
}
