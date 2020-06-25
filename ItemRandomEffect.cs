using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemRandomEffect : MonoBehaviour
{
    public Transform EffectPos;
    public Transform EffectPos2;
    public Transform EffectPos3;




    public GameObject Effect;
    public GameObject Effect2;
    public GameObject Effect3;
    public GameObject Effect4;
    public GameObject Effect5;
    public GameObject Effect6;



    GameObject effect2;

    public Image img_SpeedUpSkill;
    public Image Get_img_SpeedUpSkill;


    public Image img_SlowSkill;
    public Image Get_img_SlowSkill;

    public Image img_ReverseKeySkill;
    public Image Get_img_ReverseKeySkill;

    public Image img_NonAttack;
    public Image Get_img_NonAttack;

    public Image img_SizeUp;
    public Image Get_img_SizeUp;

    public Image img_SizeDown;
    public Image Get_img_SizeDown;






    private Material Mate;
    public Transform body;
    public Text TimeText;
    public bool ItemTimeCount=false;

    public GameObject m_speedup2DUI;
    public GameObject m_speedup2DUIPos;

    public GameObject m_slow2DUI;
    public GameObject m_slow2DUIPos;

    public GameObject m_reversekey2DUI;
    public GameObject m_reversekey2DUIPos;

    public GameObject m_nonattack2DUI;
    public GameObject m_nonattack2DUIPos;

    public GameObject m_sizeup2DUI;
    public GameObject m_sizeup2DUIPos;

    public GameObject m_sizedown2DUI;
    public GameObject m_sizedown2DUIPos;

   

    //void Awake()
    //{
    //   Mate = body.GetComponent<Renderer>().material;
    //}

    void Start()
    {
        img_SpeedUpSkill.fillAmount = 0;
        img_SlowSkill.fillAmount = 0;
        img_ReverseKeySkill.fillAmount = 0;
        img_NonAttack.fillAmount = 0;
        img_SizeUp.fillAmount = 0;
        img_SizeDown.fillAmount = 0;

        Get_img_SpeedUpSkill.gameObject.SetActive(false);
        Get_img_SlowSkill.gameObject.SetActive(false);
        Get_img_ReverseKeySkill.gameObject.SetActive(false);
        Get_img_NonAttack.gameObject.SetActive(false);
        Get_img_SizeUp.gameObject.SetActive(false);
        Get_img_SizeDown.gameObject.SetActive(false);

        m_speedup2DUI.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        m_slow2DUI.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        m_nonattack2DUI.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        m_reversekey2DUI.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        m_sizeup2DUI.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        m_sizedown2DUI.transform.position = Camera.main.WorldToScreenPoint(transform.position);

    }
  
    void Update()
    {

       

        if (ItemTimeCount)
        {
            TimeText.CrossFadeAlpha(255, 1, true);
            TimeText.text = DataManager.Instance.SpeedUpItemTimeCurrent.ToString("N0");
        }

        if(DataManager.Instance.TestItem)
        {
            GameObject effect2 = Instantiate(Effect5, EffectPos3.position, Quaternion.identity);
            if(Time.deltaTime==5)
            //Destroy(effect2, effect2.GetComponent<ParticleSystem>().duration + 0.3f);
            Destroy(effect2, effect2.GetComponent<ParticleSystem>().duration + 0.3f);
        }

        if (DataManager.Instance.SpeedUpMode)
        {
            ItemTimeCount = true;


            DataManager.Instance.SpeedUpItemTimeCurrent -= 1 * Time.deltaTime;

            
             
            

            if (DataManager.Instance.rand < 0.3f)
            {
                img_SpeedUpSkill.fillAmount = (DataManager.Instance.SpeedUpItemTimeCurrent / 1.0f);
                //Get_img_SpeedUpSkill.gameObject.SetActive(true);
                //Get_img_SpeedUpSkill.fillAmount = 1;
             
                 StartCoroutine(SpeedUpEffect());
                GameObject effect = Instantiate(Effect, EffectPos.position, Quaternion.identity);
                Debug.Log("스피드업!!");
                DataManager.Instance.PlayerSpeed = 4.0f;
            }

            else if (DataManager.Instance.rand < 0.4f)
            {
                img_SlowSkill.fillAmount = (DataManager.Instance.SpeedUpItemTimeCurrent / 1.0f);
                StartCoroutine(SlowEffect());
                GameObject effect = Instantiate(Effect2, EffectPos.position, Quaternion.identity);
                StartCoroutine("SlowItemShineEffect");
                Debug.Log("슬로우..");
                DataManager.Instance.PlayerSpeed = 1.0f;
            }
            else if (DataManager.Instance.rand < 0.5f)
            {
                img_NonAttack.fillAmount = (DataManager.Instance.SpeedUpItemTimeCurrent / 1.0f);
                StartCoroutine(NoAttackEffect());
                GameObject effect = Instantiate(Effect4, EffectPos.position, Quaternion.identity);
                //StartCoroutine("NonAttackEffect");
                Debug.Log("공격불가");
                DataManager.Instance.NonAttack = true;
            }
            else if (DataManager.Instance.rand < 0.6f)
            {
                img_ReverseKeySkill.fillAmount = (DataManager.Instance.SpeedUpItemTimeCurrent / 1.0f);
                StartCoroutine(ReverseKeyEffect());
                GameObject effect = Instantiate(Effect3, EffectPos.position, Quaternion.identity);
                StartCoroutine("MaterialColorShine");
                Debug.Log("키반전..");
                DataManager.Instance.ReverseKey = true;

            }
           else if (DataManager.Instance.rand < 0.8f)
            {
                img_SizeDown.fillAmount = (DataManager.Instance.SpeedUpItemTimeCurrent / 1.0f);
                StartCoroutine(SizeDownEffect());
                Debug.Log("사이즈다운!");
                DataManager.Instance.SizeDown = true;

            }

            else
            {
                img_SizeUp.fillAmount = (DataManager.Instance.SpeedUpItemTimeCurrent / 1.0f);
                StartCoroutine(SizeUpEffect());
                Debug.Log("사이즈업!");
                DataManager.Instance.SizeUp = true;
            }
        }

        if (DataManager.Instance.SpeedUpItemTimeCurrent <= 0)
        {
            DataManager.Instance.SpeedUpMode = false;
            DataManager.Instance.SpeedUpItemTimeCurrent = 5.0f;
            DataManager.Instance.PlayerSpeed = 2.0f;
            DataManager.Instance.PlayerJumpSpeed = 7.0f;
            DataManager.Instance.ReverseKey = false;
            DataManager.Instance.NonAttack = false;
            DataManager.Instance.SizeUp = false;
            DataManager.Instance.SizeDown = false;
            DataManager.Instance.rand = Random.Range(0.0f, 1.0f);
            ItemTimeCount = false;
            TimeText.CrossFadeAlpha(0, 1, true);
           

        }
   
    }


    IEnumerator SpeedUpEffect()
    {
        if (DataManager.Instance.SpeedUpItemTimeCurrent >= 4.9)
        {
            int count = 0;
            while (count < 10)
            {
                Get_img_SpeedUpSkill.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.05f);
                Get_img_SpeedUpSkill.gameObject.SetActive(false);
                yield return new WaitForSeconds(0.05f);
                count++;
            }
        }
    }

    IEnumerator SlowEffect()
    {
        if (DataManager.Instance.SpeedUpItemTimeCurrent >= 4.9)
        {
            int count = 0;
            while (count < 10)
            {
                Get_img_SlowSkill.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.05f);
                Get_img_SlowSkill.gameObject.SetActive(false);
                yield return new WaitForSeconds(0.05f);
                count++;
            }
        }
    }

    IEnumerator  ReverseKeyEffect()
    {
        if (DataManager.Instance.SpeedUpItemTimeCurrent >= 4.9)
        {
            int count = 0;
            while (count < 10)
            {
                Get_img_ReverseKeySkill.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.05f);
                Get_img_ReverseKeySkill.gameObject.SetActive(false);
                yield return new WaitForSeconds(0.05f);
                count++;
            }
        }
    }

    IEnumerator NoAttackEffect()
    {
        if (DataManager.Instance.SpeedUpItemTimeCurrent >= 4.9)
        {
            int count = 0;
            while (count < 10)
            {
                Get_img_NonAttack.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.05f);
                Get_img_NonAttack.gameObject.SetActive(false);
                yield return new WaitForSeconds(0.05f);
                count++;
            }
        }
    }

    IEnumerator SizeUpEffect()
    {
        if (DataManager.Instance.SpeedUpItemTimeCurrent >= 4.9)
        {
            int count = 0;
            while (count < 10)
            {
                Get_img_SizeUp.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.05f);
                Get_img_SizeUp.gameObject.SetActive(false);
                yield return new WaitForSeconds(0.05f);
                count++;
            }
        }
    }

    IEnumerator SizeDownEffect()
    {
        if (DataManager.Instance.SpeedUpItemTimeCurrent >= 4.9)
        {
            int count = 0;
            while (count < 10)
            {
                Get_img_SizeDown.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.05f);
                Get_img_SizeDown.gameObject.SetActive(false);
                yield return new WaitForSeconds(0.05f);
                count++;
            }
        }
    }





    IEnumerator MaterialColorShine()
    {
       for(float i=0;i<0.5f;i+=0.1f)
        {
            Mate.color = Color.red;
            yield return new WaitForSeconds(0.05f);
            Mate.color = Color.yellow;
            yield return new WaitForSeconds(0.05f);
        }
        Mate.color = Color.white;
    }

    IEnumerator SlowItemShineEffect()
    {
        for (float i = 0; i < 0.5f; i += 0.1f)
        {
            Mate.color = Color.green;
            yield return new WaitForSeconds(0.05f);
            Mate.color = Color.yellow;
            yield return new WaitForSeconds(0.05f);
        }
        Mate.color = Color.white;
    }

    IEnumerator NonAttackEffect()
    {
        for (float i = 0; i < 0.5f; i += 0.1f)
        {
            Mate.color = Color.white;
            yield return new WaitForSeconds(0.05f);
            Mate.color = Color.yellow;
            yield return new WaitForSeconds(0.05f);
        }
        Mate.color = Color.white;
    }//반짝임 효과

    


}
