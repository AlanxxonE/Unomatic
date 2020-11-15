using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PileOfCardsBehaviour : MonoBehaviour
{
    private GameObject cardRef;
    private List<GameObject> pileCardsList = new List<GameObject>();

    public List<GameObject> GetPileCard()
    {
        return pileCardsList;
    }
    public GameObject GetCardRef()
    {
        return cardRef;
    }

    public GameObject SetCardRef(GameObject card)
    {
        cardRef = card;
        return cardRef;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
