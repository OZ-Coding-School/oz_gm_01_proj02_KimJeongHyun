using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision
{
    private readonly PlayerController controller;
    private readonly BoxCollider2D footCol;
    private readonly BoxCollider2D bodyCol;
    private readonly BoxCollider2D hitboxCol;
    private readonly Rigidbody2D rb;
    private readonly LayerMask groundAndPlatform;
    private readonly float checkDist = 0.02f;
    private readonly int platformLayer;

    private readonly RaycastHit2D[] hitResult = new RaycastHit2D[5];
    public bool IsGround { get; private set; }
    public bool IsDropping { get; private set; }
    public bool CanParry { get; private set; }
    public Collider2D CurrentPlatform { get; private set; }

    public PlayerCollision(PlayerController controller, Rigidbody2D rb,
       PlayerCollisions col)
    {
        this.controller = controller;
        this.rb = rb;
        this.footCol = col.footCol;
        this.bodyCol = col.bodyCol;
        this.hitboxCol = col.hitboxCol;
        this.groundAndPlatform = col.groundAndPlatform;
        this.platformLayer = LayerMask.NameToLayer("platform");
    }

    public void CheckGround()
    {
        int hitCount = Physics2D.BoxCastNonAlloc(
            footCol.bounds.center,
            footCol.bounds.size,
            0f,
            Vector2.down,
            hitResult,
            checkDist,
            groundAndPlatform);

        IsGround = false;
        CurrentPlatform = null;

        if (hitCount > 0)
        {
            float minDis = float.MaxValue;

            for (int i = 0;  i < hitCount; i++)
            {
                RaycastHit2D hit = hitResult[i];
                Collider2D hitCol = hitResult[i].collider;

                if (hitCol == null || hitCol.isTrigger) continue;
                if (Physics2D.GetIgnoreCollision(footCol, hitCol)) continue;

                if (hit.distance < minDis)
                {
                    minDis = hit.distance;
                    CurrentPlatform = hitCol;
                    IsGround = true;
                }
            }
        }
    }

    public void DropDown()
    {
        if (IsDropping) return;
        if (CurrentPlatform == null) return;

        if (CurrentPlatform.gameObject.layer == platformLayer)
        {
            Physics2D.IgnoreCollision(bodyCol, CurrentPlatform, true);
            controller.StartCoroutine(DropDownCo(CurrentPlatform));
        }
    }

    public IEnumerator DropDownCo(Collider2D platform)
    {
        if (platform == null) yield break;
        IsDropping = true;

        Physics2D.IgnoreCollision(footCol, platform, true);
        rb.velocity = new Vector2(rb.velocity.x, -5f);

        yield return new WaitForFixedUpdate();
        float timeout = 0.3f;
        float timer = 0f;

        while ((platform != null && footCol.bounds.Intersects(platform.bounds)))
        {
            timer += Time.deltaTime;
            if (timer > timeout) break;
            yield return null;
        }        

        if (platform != null)
        {
            Physics2D.IgnoreCollision(footCol, platform, false);
        }
        IsDropping = false;
    } 

    public void DrawGizmos()
    {
        if (footCol == null) return;
        Gizmos.color = IsGround ? Color.green : Color.red;
        Gizmos.DrawWireCube((Vector2)footCol.bounds.center + Vector2.down * checkDist, footCol.bounds.size);
    }
}
