using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderingAI : MonoBehaviour
{
    //改成static的了，这样，每个新生成的游戏对象的速度都会自动改变了
    public static float speed = 3.0f;
    public float obstacleRange = 5.0f;
    public const float baseSpeed = 3.0f;

    private bool _alive;
    
    [SerializeField] private GameObject fireballPrefab;  //序列化变量，用于链接预设对象
    private GameObject _fireball;  //一个私有变量，跟踪场景中敌人的实例

    private void Awake()
    {
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged); //监听速度改变事件，如果事件发生了（收到了消息），则调用OnSpeedChanged函数
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
            //NPC移动
            transform.Translate(0, 0, speed * Time.deltaTime);
            
            //射线检测
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            //0.75是射线半径
            if (Physics.SphereCast(ray, 0.75f, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                if (hitObject.GetComponent<PlayerCharacter>())
                {
                    if (_fireball == null) //生成火球
                    {
                        _fireball = Instantiate(fireballPrefab) as GameObject;  //复制了预设对象，Instantiate() 默认返回Object对象类型，所以需要进行类型转换
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
