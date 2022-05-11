using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class RayShooter : MonoBehaviour
{


    private Camera _camera;




    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
/*
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;*/
    }

    // Update is called once per frame
    void Update()
    {   //【鼠标拾取】
        if (Input.GetMouseButtonDown(0)&& 
            !EventSystem.current.IsPointerOverGameObject() //如果GUI未被使用
            )
        {
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                reactiveTarget target = hitObject.GetComponent<reactiveTarget>();
                if(target != null)
                {
                    target.ReactToHit();

                    Messenger.Broadcast(GameEvent.ENEMY_HIT); //广播事件
                }
                else StartCoroutine(SphereIndicator(hit.point));
            }
        }
        
    }
    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;
        yield return new WaitForSeconds(1);
        Destroy(sphere);
    }

    private void OnGUI()
    {
        int size = 12;
        float posX = _camera.pixelWidth / 2 - size / 4;
        float posY = _camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
    }
}


