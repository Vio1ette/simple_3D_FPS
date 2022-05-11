using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    //�ĳ�static���ˣ�������ÿ�������ɵ���Ϸ������ٶȶ����Զ��ı���
    public static float speed = 3.0f;
    public float obstacleRange = 5.0f;
    public const float baseSpeed = 3.0f;

    private bool _alive;
    
    [SerializeField] private GameObject fireballPrefab;  //���л���������������Ԥ�����
    private GameObject _fireball;  //һ��˽�б��������ٳ����е��˵�ʵ��

    private void Awake()
    {
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged); //�����ٶȸı��¼�������¼������ˣ��յ�����Ϣ���������OnSpeedChanged����
    }

    private void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    // Start is called before the first frame update
    void Start()
    {
        _alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_alive)
        {   
            //NPC�ƶ�
            transform.Translate(0, 0, speed * Time.deltaTime);
            
            //���߼��
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            //0.75�����߰뾶
            if (Physics.SphereCast(ray, 0.75f, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<PlayerCharacter>())
                {
                    if (_fireball == null) //���ɻ���
                    {
                        _fireball = Instantiate(fireballPrefab) as GameObject;  //������Ԥ�����Instantiate() Ĭ�Ϸ���Object�������ͣ�������Ҫ��������ת��
                        _fireball.transform.position = transform.TransformPoint(Vector3.forward * 1.5f);
                        _fireball.transform.rotation = transform.rotation;
                    }
                }
                else if (hit.distance < obstacleRange)
                {
                    float angle = Random.Range(-110, 110);
                    transform.Rotate(0, angle, 0);
                }
            }
        }
    }

    public void SetAlive(bool alive)
    {
        _alive = alive;
    }

    private void OnSpeedChanged(float value)
    {
        speed = baseSpeed * value;
    }

}
