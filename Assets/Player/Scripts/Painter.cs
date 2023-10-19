using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Painter : MonoBehaviour
{
    [SerializeField] private Transform paintBrush;
    public LayerMask PaintableLayer;

    [Space]
    [SerializeField] private int paintSpread;
    [SerializeField] private float paintDistance;
    [SerializeField] private float paintCooldown;

    private float remainingCooldown = 0;

    void Update()
    {
        // Shoot le paint
        if (Input.GetMouseButton(0) && remainingCooldown <= 0)
        {
            ThrowPaint(Color.white);
            remainingCooldown = paintCooldown;
            GameManager.Instance.OnPaint.Invoke();
        }
        else
            remainingCooldown -= Time.deltaTime;
    }

    Vector3 paintDirection => Quaternion.AngleAxis(-(paintSpread / 2), Vector3.up) * paintBrush.forward;
    public void ThrowPaint(Color color)
    {
        HashSet<RaycastHit> hits = new HashSet<RaycastHit>();

        for (int i = 0; i < paintSpread; i++)
        {
            RaycastHit hit;
            Vector3 castDirection = Quaternion.AngleAxis(i, Vector3.up) * paintDirection;

            if (Physics.Raycast(paintBrush.position, castDirection, out hit, paintDistance, PaintableLayer.value))
            {
                hits.Add(hit);
            }

            Debug.DrawLine(paintBrush.position,
                paintBrush.position + paintDistance * castDirection,
                Color.green,
                2);
        }

        foreach (RaycastHit hit in hits)
        {
            hit.transform.GetComponent<PaintableObject>().Paint(color);
        }
    }
}
