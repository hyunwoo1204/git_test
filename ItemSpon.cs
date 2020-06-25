using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSpon : MonoBehaviour
{
    public Transform Pos;
    public GameObject ItemEffect;
    public GameObject ItemEffect2;
    public GameObject ItemEffect3;
    public GameObject Item;
   // public GameObject _sponPos;


    public GameObject[] _sponPos = new GameObject[9];
    public List<GameObject> sponPosList=new List<GameObject>();
    public Image FindItem;

    public GameObject m_finditem;
    public GameObject m_finditempos;


    void Start()
    {
      
        InvokeRepeating("Spawnitem", 1, 10);
        DataManager.Instance.TntHad = false;
        m_finditem.transform.position = Camera.main.WorldToScreenPoint(m_finditempos.transform.position);
        FindItem.gameObject.SetActive(false);
        PosInit();

    }

    void PosInit()
    {
        for (int i=0;i<9;i++)
        {
            sponPosList.Add(_sponPos[i]);
        }
    }


    void Spawnitem()
    {
       AudioSource sponsound = GetComponent<AudioSource>();
        sponsound.Play();
        int random = Random.Range(0, 8);
        float randomX = Random.Range(-2f, 1f);
        float randomZ = Random.Range(-2f, 1f);


        GameObject effect = Instantiate(ItemEffect, sponPosList[random].transform.position, Quaternion.identity);
        GameObject effect2 = Instantiate(ItemEffect2, sponPosList[random].transform.position, Quaternion.Euler(-90,0,0));
        m_finditem.transform.position = Camera.main.WorldToScreenPoint(m_finditempos.transform.position);

        if (DataManager.Instance.SizeUp==false)
        {

            //GameObject effect3 = Instantiate(ItemEffect3, Pos.position, Quaternion.Euler(-90, 0, 0));
            //FindItem.gameObject.SetActive(true);
            StartCoroutine("FindItemEffect");
        }


        if (true)
        {
            if (DataManager.Instance.obj_col == false)
            {
                if (sponPosList[random].gameObject.activeSelf == true)
                {
                    Item = (GameObject)Instantiate(Item, sponPosList[random].transform.position, Quaternion.identity);
                    Destroy(Item, 10);
                }
               
            }
        } 

    }

    IEnumerator FindItemEffect()
    {
        if (DataManager.Instance.SpeedUpItemTimeCurrent >= 4.9)
        {
            float count = 0.5f;
            while (count < 1.2f)
            {
                FindItem.transform.localScale = new Vector3(count, count, count);
                FindItem.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.1f);
         
                count+=0.15f;
            }
            FindItem.gameObject.SetActive(false);
        }
    }

    
}
