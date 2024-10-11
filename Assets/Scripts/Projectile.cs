using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] GameObject[] bullet;

    [Space]
    [SerializeField] float lifetime;
    [SerializeField] float projectileSpeed;
    private Vector3 direction;
    int currentColorIndex; 
    private void Start() {
        Destroy(gameObject, lifetime);
        CurrentColor currentColor = Player.Instance.GetCurrentColor();
        currentColorIndex = currentColor.colorIndex;
        bullet[currentColorIndex].SetActive(true);
    }

    private void Update() {
        transform.Translate(direction * projectileSpeed * Time.deltaTime, Space.World);
    }
    private void OnTriggerEnter(Collider other) {
        if(other.TryGetComponent(out Enemy enemy)) 
        {
            enemy.colorChecker(currentColorIndex);
            Destroy(gameObject);
        }
    }

    public void SetDirection(Vector3 forwardDirection) {
        direction = forwardDirection.normalized; 
    }
}
