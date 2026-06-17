using UnityEngine;

public class EightDSpriteScript : MonoBehaviour
{
    [SerializeField]
    bool rearView;
    private Transform player;

    private Vector3 targetPos;

    private Vector3 targetDir;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    public Animator animator;

    [SerializeField]
    private float angle;

    public int lastIndex;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        //animator = GetComponentInChildren<Animator>();

        if(!rearView)
        {
            player = Camera.main.transform;
        }
        else
        {
            player = FindFirstObjectByType<MirrorCam>().transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        targetPos = new Vector3(player.position.x, transform.position.y, player.position.z);

        targetDir = targetPos - transform.position;

        angle = Vector3.SignedAngle(targetDir, transform.forward, Vector3.up);

        Vector3 tempScale = Vector3.one;
       /* if (angle > 4)
        {
            tempScale.x *= -1;
        }*/

        spriteRenderer.transform.localScale = tempScale;

        lastIndex = GetIndex(angle);

        animator.SetFloat("Angle", lastIndex);

        Vector3 modTarget = player.position;
        modTarget.y = transform.position.y;

        spriteRenderer.transform.LookAt(modTarget);
    }


    private int GetIndex(float angle)
    {
        //front
        if (angle > -22.5f && angle < 22.6f)
            return 0;
        if (angle >= 22.5f && angle < 45f)
            return 7;
        if (angle >= 45f && angle < 112.5f)
            return 6;
        if (angle >= 112.5f && angle < 135.4f)
            return 5;
        //back
        if (angle <= -135.5f || angle >= 135.5f)
            return 4;
        if (angle >= -135.4f && angle < -112.5f)
            return 3;
        if (angle >= -112.5f && angle < -41.5f)
            return 2;
        if (angle >= -41.5f && angle <= -22.5f)
            return 1;

        return lastIndex;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, targetPos);
    }
}