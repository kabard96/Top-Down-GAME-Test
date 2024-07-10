using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
    bool inventoryactive;
    [SerializeField] GameObject _InventoryImage;
    [SerializeField] Button _APTECHKA;
    [SerializeField] Button _BODYARMOR;
    [SerializeField] Button _GRANATE;
    Collider2D _GRANATECollider;
    [SerializeField] Text _APTECHKAtext;
    [SerializeField] Text _BODYARMORtext;
    [SerializeField] Text _GRANATEtext;
    public static int _ApteckaCount;
    public static int _GRANATECount;
    public static int _BodyArmorCount;
    float _VariableButtonReload = 100;
    bool BoolButtonReload1;
     bool BoolButtonReload2;

    [SerializeField] Image ButtonReloadFillAmount1;
    [SerializeField] Image ButtonReloadFillAmount2;

    private void FixedUpdate()
    {
        

        if (gameObject.transform.CompareTag("button"))
        {
            if (_ApteckaCount > 0)
            {
                _APTECHKA.interactable = true;

            }
            else if (_BodyArmorCount > 0)
            {
                _BODYARMOR.interactable = true;
            }
            else if (_GRANATECount > 0)
            {
                _GRANATE.interactable = true;
            }


            if (_ApteckaCount > 1)
            {
                _APTECHKAtext.text = "X" + _ApteckaCount.ToString();
            }
            else if (_GRANATECount > 1)
            {
                _GRANATEtext.text = "X" + _GRANATECount.ToString();
            }
            else if (_BodyArmorCount > 1)
            {
                _BODYARMORtext.text = "X" + _BodyArmorCount.ToString();
            }

        }
        if (BoolButtonReload1 == true)
        {

            if (_VariableButtonReload < 100)
            {
                _VariableButtonReload += 2;
                ButtonReloadFillAmount1.fillAmount = _VariableButtonReload/100;
            }else if(_VariableButtonReload>=100)
            {
                BoolButtonReload1 = false;
            }

        }
        else if (BoolButtonReload2 == true)
        {
            if (_VariableButtonReload < 100)
            {
                _VariableButtonReload += 2;
                ButtonReloadFillAmount2.fillAmount = _VariableButtonReload /100;
            }
            else if (_VariableButtonReload >= 100)
            {
                BoolButtonReload2 = false;
            }

        }

        Debug.Log(_VariableButtonReload);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (gameObject.transform.CompareTag("Aptechka"))
        {
            _ApteckaCount++;
            float dfsfg = collision.GetComponent<PlayerControl>().healt;
            dfsfg += 30;


            Destroy(gameObject);

        }else if (gameObject.transform.CompareTag("Granate"))
        {
            _GRANATECount++;
        }else if (gameObject.transform.CompareTag("BodyArmor"))
        {
            _BodyArmorCount++;
        }
    }
    public void InventorySetActiveTrue()
    {
        if (inventoryactive == true)
        {
            _InventoryImage.SetActive(false);
            inventoryactive = false;
        }
        else
        {
            _InventoryImage.SetActive(true);
            inventoryactive=true;
        }
    }

    public void _BuTTonRELOAD1()
    {
        if (_VariableButtonReload >= 100)
        {
            BoolButtonReload1 = true;
            _VariableButtonReload = 0;
        }

    }
    public void _ButtonReload2()
    {
        if (_VariableButtonReload >= 100)
        {
            BoolButtonReload2 = true;
            _VariableButtonReload = 0;
        }
     
    }
}
