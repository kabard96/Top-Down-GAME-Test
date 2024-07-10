using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.SceneManagement;
using System;

public class PlayerControl : MonoBehaviour
{
    public float _speed;
    private float _moveInputx;
    private float _moveInputy;
    private Rigidbody2D _rb;
    [SerializeField] Joystick _joystic;
    Animator _animator;
    public  float healt = 100;
    bool _dethplayer;
    bool _facingRigth;

    float radius = 3;
    [SerializeField] GameObject _bullet;

    [SerializeField]Transform _gunTransform;
    [SerializeField] GameObject _fire;



    [SerializeField] Image _imagehealtUp;
    [SerializeField] Image _imagehealtButton;

    [SerializeField]Text _TextButtonreloadCountBULLET;
    [SerializeField]Text _TextOnPLAYERCOUNTBULLET;
    private float timeBTWshorts;
    public float _starttimeBTWshorts;
    int _buletcount=50;

    Collider2D _Zombie;
    // Start is called before the first frame update
    private void Start()
    {
       //��� ���������� ������ ���������� ���������� ��������� ���� �� ������� �����.

        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {


        
        if (_dethplayer == false)
        {

            if (gameObject.CompareTag("NEWPLAYER"))
            {
                _TextOnPLAYERCOUNTBULLET.text = _buletcount.ToString();
                _imagehealtUp.fillAmount = healt / 100;  //��� ����������� ����� ��� �������
            }
            else
            {
                _TextButtonreloadCountBULLET.text = _buletcount.ToString() + "/50";
                _TextOnPLAYERCOUNTBULLET.text = _buletcount.ToString();
                _imagehealtButton.fillAmount = healt / 100;  //��� ����������� ����� ����� � ������ ������ ����
                _imagehealtUp.fillAmount = healt / 100;  //��� ����������� ����� ��� �������
            }
           

            if (_Zombie == null)
            {
                _fire.SetActive(false);
                Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radius);//���� ��� ��������� ������� ����� � ������ ������ ���

                foreach (var el in hitColliders)//���������� ������ ����������
                {
                    if (el.transform.CompareTag("zombie"))
                    {
                        _Zombie = el; //�������� ����� ����� � ���������� ������� ������� �������
                    }



                }
            }
            else
            {
                float distance = Vector2.Distance(transform.position, _Zombie.transform.position);

                if (distance <= 3)
                {
                    if (timeBTWshorts <= 0)
                    {
                        Instantiate(_bullet, _gunTransform.position, _gunTransform.rotation);
                        _buletcount -= 1;
                        timeBTWshorts = _starttimeBTWshorts;
                    }
                    else
                    {
                        timeBTWshorts -= Time.deltaTime;
                    }
                    _fire.SetActive(true);
                }
                else
                {
                    _fire.SetActive(false);
                }


            }



            if (healt <= 0)//���� ����� ������ ���� 
            {
                _dethplayer = true;
                //��� ���������� ������ ���������� ���������� ��������� ���� �� ����� �������� ����.
                try
                {
                    savesData.instance.SaveProgress();
                }
                catch (Exception ex)
                {
                    Debug.LogError("��� ���������� ������ ���������� ���������� ��������� ���� �� ����� �������� ����: " + ex.Message);
                   
                }
               
               

            }
            _moveInputx = _joystic.Horizontal;
            _moveInputy = _joystic.Vertical;
            _rb.velocity = new Vector2(_moveInputx * _speed, _moveInputy * _speed);
           
            
            
            
            
            if (_Zombie != null)
            {
                if (_facingRigth == false  && _Zombie.transform.position.x < transform.position.x)
                {
                    Flip();
                }
               else if (_facingRigth == true && _Zombie.transform.position.x > transform.position.x)
                {
                    Flip();
                }
            }
            else
            {
                if (_facingRigth == false && _moveInputx < 0 )
                {
                    Flip();

                }
                else if (_facingRigth == true && _moveInputx > 0 )
                {
                    Flip();

                }
            }
          






                if (_moveInputx == 0 && _moveInputy == 0)
                {
                
                if (gameObject.CompareTag("NEWPLAYER"))
                {
                    _animator.SetBool("IDLE", true);
                    _animator.SetBool("RUN", false);
                }
                else
                {
                    _animator.speed = 0;
                }
                 }
            else
            {
                
              
                if (gameObject.CompareTag("NEWPLAYER"))
                {
                    _animator.SetBool("IDLE", false);
                    _animator.SetBool("RUN", true);
                }
                else
                {
                    _animator.speed = 0.5f;
                }
            }
        }


        else
        {
            _animator.speed = 0.5f;
            if (transform.CompareTag("red"))
            {
                _animator.SetBool("diered", true);
            }else if (transform.CompareTag("blue"))
            {
                _animator.SetBool("bluedie", true);
            }
           
            
        }
    }
    public void _oNEshut()
    {
            Instantiate(_bullet, _gunTransform.position, _gunTransform.rotation);
            _buletcount -= 1;
           
    }
    void Flip()
    {
        _facingRigth=!_facingRigth;
        Vector3 scaler=transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
  
}
