using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    [SerializeField] Transform _Rocket;
    [SerializeField] Vector3 _Position;
    [SerializeField] float speed = 5;
    bool _t;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame

    private void FixedUpdate()
    {
            
        if(_Rocket != null)
        {
            Vector3 newcampos = new Vector3(_Rocket.position.x, _Rocket.position.y, _Rocket.position.z) + _Position;
            transform.position = Vector3.Lerp(transform.position, newcampos, speed * Time.deltaTime);
        }
            
        
    }
    
}
