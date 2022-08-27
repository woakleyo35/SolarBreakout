using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Brick : MonoBehaviour
{
    [SerializeField]
    private int Hits = 1;

    [SerializeField]
    private int Points = 100;

    [SerializeField]
    private Vector3 rotator = Vector3.zero;

    [SerializeField]
    private Material hitMaterial;

    [SerializeField]
    private Material damageMaterial;

    private Material originalMaterial;
    private Renderer render;

    private void Start()
    {
        transform.Rotate(rotator * ((transform.position.x + transform.position.y) * -0.1f));
        this.render = GetComponent<Renderer>();
        this.originalMaterial = damageMaterial ?? render.sharedMaterial;
    }

    private void Update()
    {
        transform.Rotate(rotator * (Time.deltaTime));
    }

    private void OnCollisionEnter(Collision collision)
    {
        this.Hits--;
        // TODO: Score points;

        if(this.Hits <= 0)
        {
            GameManager.Instance.Score += Points;
            Destroy(gameObject);
            return;
        }

        this.render.sharedMaterial = this.hitMaterial;
        Invoke("RestoreMaterial", 0.05f);
    }

    private void RestoreMaterial()
    {
        this.render.sharedMaterial = this.originalMaterial;
    }
}
