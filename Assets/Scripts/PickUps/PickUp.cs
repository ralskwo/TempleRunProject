using UnityEngine;

public abstract class PickUp : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 100f;

    const string playerString = "Player";

    private void Update()
    {
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag(playerString))
        {
            OnPickUp();
            Destroy(gameObject); 
        }
    }

    protected abstract void OnPickUp();
}
