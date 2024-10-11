using System;
using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    public static event Action OnEnemyDeath;

    [SerializeField] private Player player;
    [SerializeField] private UnityEngine.AI.NavMeshAgent navMeshAgent;
    [Space]
    [SerializeField] private List<SkinnedMeshRenderer> meshRenderer;
    [SerializeField] private Shader shader;

    [Space]
    private int currentColor;
    private Color chosenColor;

    private void Awake() {
        player = FindAnyObjectByType<Player>();
    }

    private void Start() {
        SetRandomColor();
        navMeshAgent.SetDestination(player.transform.position);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.TryGetComponent(out Player player))
        {
            OnEnemyDeath = null;
            GameManager.Instance.GameOver();
        }
    }

    private void SetRandomColor() {
        for (int i = 0; i < meshRenderer.Count; i++) {
            meshRenderer[i].material = new Material(shader);
        }

        currentColor = UnityEngine.Random.Range(0, 3);
        switch (currentColor) {
            case 0:
                chosenColor = Color.red;

                break;

            case 1:
                chosenColor = Color.green;

                break;

            case 2:
                chosenColor = Color.blue;

                break;

            default:
                break;
        }

        for (int i = 0; i < meshRenderer.Count; i++) {
            meshRenderer[i].material.color = chosenColor;
        }
    }

    public void colorChecker(int projectileColor) {
        if (projectileColor == currentColor) {
            Player.Instance.RemoveFromList(this);
            OnEnemyDeath?.Invoke();
            Destroy(gameObject);
        }
    }

    public void SetSpeed(float speed) {
        navMeshAgent.speed = speed;
    }
}