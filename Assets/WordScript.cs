using UnityEngine;

public class WordScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Rigidbody2D rb;
    private bool isDragging = false;
    private bool isSlotted = false;
    private Vector3 offset;

    public WordData wordData; // Reference to the ScriptableObject data
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(Random.insideUnitCircle * 50f, ForceMode2D.Impulse); // Initial random push
    }
    void Update()
    {
        if (isDragging)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos + offset;
        }
    }

    void FixedUpdate()
    {
        // Gentle floating behavior if not being held or slotted
        if (!isDragging && !isSlotted)
        {
            rb.AddForce(Random.insideUnitCircle * 2f);
        }
    }
    private void OnMouseDown()
    {
        isDragging = true;
        rb.simulated = false; // Stop physics while dragging

        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset.z = 0;
    }
    void OnMouseDrag()
    {
        isDragging = true;

        // Convert mouse position to world position
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // Keep it on the 2D plane

        transform.position = Vector3.Lerp(transform.position, mousePos, Time.deltaTime * 20f);
    }

    private void OnMouseUp()
    {
        isDragging = false;
        rb.simulated = true; // Re-enable physics so it can snap or float
        CheckForSlot();
    }

    void CheckForSlot()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (var hit in hits)
        {
            if (!hit.CompareTag("Slot")) return;
            // SlotScript slot = hit.GetComponent<SlotScript>();
            // if (slot != null && slot.wordType == wordData.type && !slot.isOccupied)
            // {
            //     transform.position = slot.transform.position; // Snap to slot
            //     rb.simulated = false; // Stop physics once slotted
            //     slot.PlaceWord(this); // Inform the slot that this word is now occupying it
            //     isSlotted = true;
            //     return;
            // }
        }
    }
}
