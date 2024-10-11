using System;
using System.Collections.Generic;
using UnityEngine;

public class CurrentColor
{
    public Color color;
    public int colorIndex;
}

public class Player : MonoBehaviour
{
    public event Action<int> OnColorChange;

    public static Player Instance;

    [SerializeField] private Shader shader;
    [SerializeField] private List<MeshRenderer> meshRendererList;
    [SerializeField] private List<Enemy> enemyList;
    [SerializeField] private SpriteRenderer magicCircle;
    [SerializeField] private float sphereRadius = 1.0f; 
    [SerializeField] private float maxDistance = 10.0f; 
    [SerializeField] private float rotationSpeed = 5f; 

    private CurrentColor currentColor = new CurrentColor();
    private int currentColorIndex = 0; 

    private bool isGameStart = false;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        for (int i = 0; i < meshRendererList.Count; i++)
        {
            meshRendererList[i].material = new Material(shader);
        }

        GameManager.Instance.OnGameStart += GameManager_OnGameStart;
        currentColor.color = Color.red;
        currentColor.colorIndex = 0;

        for (int i = 0; i < meshRendererList.Count; i++)
        {
            meshRendererList[i].material.color = currentColor.color;
        }
        magicCircle.color = currentColor.color;
    }

    private void GameManager_OnGameStart() {
        isGameStart = true;
    }

    private void Update() {
        enemyList.RemoveAll(enemy => enemy == null);

        enemyDetector();
        rotationHandler();
        HandleChangeColor();
    }

    public CurrentColor GetCurrentColor() {
        return currentColor;
    }

    public Enemy GetCurrentTarget() {
        if (enemyList.Count <= 0) return null;
        return enemyList[0];
    }

    private void rotationHandler() {
        if (enemyList.Count > 0)
        {
            Vector3 directionToEnemy = enemyList[0].transform.position - transform.position;

            Quaternion targetRotation = Quaternion.LookRotation(directionToEnemy);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }

    private void enemyDetector() {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, maxDistance, LayerMask.GetMask("Enemy"));

        foreach (var hitCollider in hitColliders)
        {
            Enemy enemyHit = hitCollider.GetComponent<Enemy>(); 

            if (enemyHit != null && !enemyList.Contains(enemyHit)) 
            {
                enemyList.Add(enemyHit);
            }
        }
    }

    private void HandleChangeColor() {
        if (!isGameStart) return;
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            currentColorIndex++;
            switch (currentColorIndex)
            {
                case 0:
                    currentColor.color = Color.red;
                    currentColor.colorIndex = 0;
                    break;

                case 1:
                    currentColor.color = Color.green;
                    currentColor.colorIndex = 1;

                    break;

                case 2:
                    currentColor.color = Color.blue;
                    currentColor.colorIndex = 2;

                    break;

                default:
                    currentColorIndex = 0;
                    currentColor.colorIndex = 0;
                    currentColor.color = Color.red;

                    break;
            }

            for (int i = 0; i < meshRendererList.Count; i++)
            {
                meshRendererList[i].material.color = currentColor.color;
            }
            magicCircle.color = currentColor.color;
            OnColorChange?.Invoke(currentColorIndex);
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sphereRadius);
    }

    public void RemoveFromList(Enemy enemy) {
        if (enemyList.Contains(enemy))
        {
            enemyList.Remove(enemy);
        }
    }
}