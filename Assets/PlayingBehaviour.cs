using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingBehaviour : MonoBehaviour
{
    private float cardSpeed = 0;

    private GameObject pileOfCardsRef;

    private Vector3 pileOffset = new Vector3(0,0,2);

    // Start is called before the first frame update
    void Start()
    {
        this.transform.SetParent(null);
        this.GetComponent<CardBehaviour>().enabled = false;
        this.GetComponents<BoxCollider>()[0].enabled = false;
        this.GetComponents<BoxCollider>()[1].enabled = false;

        pileOfCardsRef = GameObject.FindGameObjectWithTag("PileOfCards");

        this.transform.LookAt(pileOfCardsRef.transform);

        cardSpeed = 4;

        StartCoroutine(SelfDestruct());
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(pileOffset * Time.deltaTime * cardSpeed);
    }

    IEnumerator SelfDestruct()
    {
        yield return new WaitForSeconds(0.2f);

        Destroy(this.gameObject);
    }
}
