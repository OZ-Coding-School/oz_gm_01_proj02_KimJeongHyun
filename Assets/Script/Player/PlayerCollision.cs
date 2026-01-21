using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using UnityEngine;

public class PlayerCollision
{
    private readonly PlayerController controller;
    private BoxCollider2D footCol;
    private BoxCollider2D bodyCol;
    private BoxCollider2D hitBoxCol;
    private readonly Transform centerTrs;
    private readonly Rigidbody2D rb;
    private readonly FlashEffect playerFlash;
    private readonly LayerMask groundAndPlatform;
    private readonly LayerMask parryLayer;
    private readonly float checkDist = 0.02f;
    private readonly Vector2 parryBoxSize = new Vector2(1.3f, 1.3f);
    private readonly Collider2D[] parryHitResult = new Collider2D[5]; 
    private readonly int platformLayer;
    private readonly RaycastHit2D[] hitResult = new RaycastHit2D[5];
    public Vector2 ParryPoint { get; private set; }
    public bool IsGround { get; private set; }
    public bool IsDropping { get; private set; }    
    public bool CanParry { get; private set; }
    public Collider2D CurrentPlatform { get; private set; }

    public PlayerCollision(PlayerController controller, Rigidbody2D rb,
       PlayerCollisions col, FlashEffect playerFlash)
    {
        this.controller = controller;
        this.rb = rb;
        this.footCol = col.footCol;
        this.bodyCol = col.bodyCol;
        this.hitBoxCol = col.hitBoxCol;
        this.groundAndPlatform = col.groundAndPlatform;
        this.platformLayer = LayerMask.NameToLayer("platform");
        this.parryLayer = LayerMask.GetMask("parryable");
        this.playerFlash = playerFlash;
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

    public Collider2D CheckParry()
    {
        int hitCount = Physics2D.OverlapBoxNonAlloc(
            bodyCol.bounds.center,
            parryBoxSize,
            0,
            parryHitResult,
            parryLayer
        );

        if (hitCount > 0)
        {
            ParryPoint = parryHitResult[0].ClosestPoint(bodyCol.bounds.center);
            CanParry = true;
            return parryHitResult[0];
        }
        CanParry = false;
        return null;
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

    public void SetJumpColSize()
    {
        bodyCol.size = new Vector2(0.7f, 0.6f);
        bodyCol.offset = new Vector2(0f, -0.1f);
        hitBoxCol.size = new Vector2(0.4f, 0.6f);
        hitBoxCol.offset = new Vector2(0f, 0.6f);
    }

    public void SetGroundColSize()
    {
        bodyCol.size = new Vector2(0.7f, 1.2f);
        bodyCol.offset = new Vector2(0f, -0.01f);
        hitBoxCol.size = new Vector2(0.4f, 0.8f);
        hitBoxCol.offset = new Vector2(0f, 0.7f);
    }

    public void SetDuckColSize()
    {
        bodyCol.size = new Vector2(0.8f, 0.5f);
        bodyCol.offset = new Vector2(0, -0.3f);
        hitBoxCol.size = new Vector2(0.8f, 0.5f);
        hitBoxCol.offset = new Vector2(0, 0.3f);
    }

    public void DrawGizmos()
    {
        if (footCol != null)
        {
            Gizmos.color = IsGround ? Color.green : Color.red;
            Gizmos.DrawWireCube((Vector2)footCol.bounds.center + Vector2.down * checkDist, footCol.bounds.size);
        }
        if (bodyCol != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(bodyCol.bounds.center, parryBoxSize);
        }
    }
}
