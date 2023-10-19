using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Painter : MonoBehaviour
{
    public LayerMask PaintableLayer;

    [Header("Painting")]
    [SerializeField] private int paintSpread;
    [SerializeField] private float paintDistance;
    [SerializeField] private float paintCooldown;

    private float remainingCooldown = 0;

    private Transform playerCamera;

    [Header("Color Selection")]
    [SerializeField] private Color[] validColors;
    [SerializeField] private Renderer brushRenderer;
    private int selectedColorIndex = 0;

    void Start()
    {
        playerCamera = Camera.main.transform;
    }

    void Update()
    {
        ShootLePaint();
        ChangeColor();
    }

    private void ShootLePaint()
    {
        // Shoot le paint
        if (Input.GetMouseButton(0) && remainingCooldown <= 0)
        {
            ThrowPaint(validColors[selectedColorIndex]);
            remainingCooldown = paintCooldown;
            GameManager.Instance.OnPaint.Invoke();
        }
        else
            remainingCooldown -= Time.deltaTime;
    }

    private void ChangeColor()
    {
        int delta = 0;
        if (Input.GetKeyDown(KeyCode.UpArrow))
            delta += 1;
        if (Input.GetKeyDown(KeyCode.DownArrow))
            delta -= 1;

        if (delta == 0)
            return;

        selectedColorIndex += delta;
        selectedColorIndex = (validColors.Length + selectedColorIndex) % validColors.Length;

        brushRenderer.material.SetColor("_BaseColor", validColors[selectedColorIndex]);
    }

    Vector3 paintDirection => Quaternion.AngleAxis(-(paintSpread / 2), Vector3.up) * playerCamera.forward;
    public void ThrowPaint(Color color)
    {
        HashSet<RaycastHit> hits = new HashSet<RaycastHit>();

        for (int i = 0; i < paintSpread; i++)
        {
            RaycastHit hit;
            Vector3 castDirection = Quaternion.AngleAxis(i, Vector3.up) * paintDirection;

            if (Physics.Raycast(playerCamera.position, castDirection, out hit, paintDistance, PaintableLayer.value))
            {
                hits.Add(hit);
            }

            Debug.DrawLine(playerCamera.position,
                playerCamera.position + paintDistance * castDirection,
                Color.green,
                2);
        }

        foreach (RaycastHit hit in hits)
        {
            hit.transform.GetComponent<PaintableObject>().Paint(color);
        }
    }
}
