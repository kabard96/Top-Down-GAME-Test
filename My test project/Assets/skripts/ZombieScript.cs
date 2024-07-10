using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class ZombieScript : MonoBehaviour
{
    float radius = 3.5f;
    [SerializeField] Transform _Vitya;
   Animator _animator;
    bool _destroyzombie;
    Collider2D _player;
    float _zombiehealt=100f;

    [SerializeField]GameObject _medecinekit;
    [SerializeField] Image _imagehealtUp;

    PlayerControl _playerControl;
    bool APTECHKA;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }


    private void FixedUpdate()
    {
        if (_destroyzombie == false)
        {
            _imagehealtUp.fillAmount =_zombiehealt / 100;
            if (_player == null)
            {
                _animator.SetBool("run", true);
                Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius);//èùåì âñå êîëàéäåğû êîòîğûå âîøëè â ñâõåğó âîêğóã íàñ
               
                    foreach (var el in hitColliders)//ïåğåáèğàåì ñïèñîê êîëàéäåğîâ
                    {
                    if (el.transform.CompareTag("blue") || el.transform.CompareTag("red")|| el.transform.CompareTag("NEWPLAYER"))
                    {
                        _player = el; //ïîìåùàåì ıòîãî âğàãà â ïåğåìåííóş êîòîğóş ñîçäàëè âíà÷àëå
                        _playerControl=_player.GetComponent<PlayerControl>();
                    }
                           
                          

                    }
                
            }
            if (_player != null)
            {

                float distance = Vector2.Distance(transform.position, _player.transform.position);
                Debug.Log(distance);
                if (distance > 0.9f)
                {
                    transform.position = Vector2.MoveTowards(transform.position, _player.transform.position, 0.02f);
                }



                if (distance <= 1)
                {

                    _animator.SetBool("atack", true);
                    _playerControl.healt -= 0.2f;
                    _animator.SetBool("run", false);
                }
                else
                {
                    _animator.SetBool("run", true);
                    _animator.SetBool("atack", false);
                }
            }

            if (_zombiehealt <= 0)
            {
                _animator.SetBool("die", true);
              
                if (APTECHKA == false)
                {
                    //ÄËß ÊÎĞĞÅÊÒÍÎÉ ĞÀÁÎÒÛ ÑÎÕĞÀÍÅÍÈÉ ÍÅÎÁÕÎÄÈÌÎ ÇÀÃĞÓÆÀÒÜ ÈÃĞÓ ÈÇ ÑÖÅÍÛ ÃËÀÂÍÎÃÎ ÌÅÍŞ.
                    try
                    {
                        savesData.instance._numberofzombieskilled++;
                    }
                    catch (Exception ex)
                    {
                        Debug.LogError("ÄËß ÊÎĞĞÅÊÒÍÎÉ ĞÀÁÎÒÛ ÑÎÕĞÀÍÅÍÈÉ ÍÅÎÁÕÎÄÈÌÎ ÇÀÃĞÓÆÀÒÜ ÈÃĞÓ ÈÇ ÑÖÅÍÛ ÃËÀÂÍÎÃÎ ÌÅÍŞ: " + ex.Message);
                       
                    }
                    
                    savesData.instance._numberofzombieskilled++;
                    Instantiate(_medecinekit, transform.position, Quaternion.identity);
                    APTECHKA = true;

                }
               
            }
          
        }
        else
        {
            _animator.SetBool("die", true);
        }
    }
    public void TakeDamage(int damage)
    {
        _zombiehealt -= damage;
    }
    

}
