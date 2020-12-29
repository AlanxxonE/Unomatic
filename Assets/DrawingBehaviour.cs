using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingBehaviour : MonoBehaviour
{
    private float cardSpeed = 0;

    private Transform cardTarget;

    public Transform SetTarget(Transform position)
    {
        cardTarget = position;
        return cardTarget;
    }

    // Start is called before the first frame update
    void Start()
    {
        this.transform.SetParent(null);
        this.GetComponent<CardBehaviour>().enabled = false;
        this.GetComponents<BoxCollider>()[0].enabled = false;
        this.GetComponents<BoxCollider>()[1].enabled = false;

        this.transform.LookAt(cardTarget);

        cardSpeed = 4;

        this.GetComponent<AudioSource>().Play();

        StartCoroutine(SelfDestruct());
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(Vector3.forward * Time.deltaTime * cardSpeed);
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(0.5f);

        Destroy(this.gameObject);
    }
}
