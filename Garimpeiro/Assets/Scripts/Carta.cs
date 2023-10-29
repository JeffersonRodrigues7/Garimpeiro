using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carta : MonoBehaviour
{
    [Header("Set Dynamically")]

    public Sprite sprite;
    public string naipe;
    public string nome;
    public int valor;
    public GameObject back = null;
    SpriteRenderer _tSR = null;
    public SpriteRenderer[] spriteRenderers;

    private void Start()
    {
        SetSortOrder(1);
    }

    public bool faceUp{
        get {return (!back.activeSelf);}
        set { back.SetActive(!value);}
    }



    virtual public void OnMouseDown(){
        print(valor + naipe);
        //_tSR = GetComponent<SpriteRenderer>();
        //if(_tSR.sortingOrder==3) _tSR.sortingOrder=1;
        //else{
        //    Destroy(gameObject);
        //}
    }

    public void PopulateSpriteRenderers()
    {
        if(spriteRenderers == null || spriteRenderers.Length == 0)
        {
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        }
    }

    public void SetSortingLayerName(string tSLN)
    {
        PopulateSpriteRenderers();
        foreach(SpriteRenderer tSR in spriteRenderers)
        {
            tSR.sortingLayerName = tSLN;
        }
    }

    public void SetSortOrder(int SOrd)
    {
        PopulateSpriteRenderers();
        foreach(SpriteRenderer tSR in spriteRenderers)
        {
            switch (tSR.gameObject.name)
            {
                case "back":
                    tSR.sortingOrder = SOrd + 1;
                    break;
                case "face":
                default:
                    tSR.sortingOrder = SOrd;
                    break;
            }
        }
    }

}
