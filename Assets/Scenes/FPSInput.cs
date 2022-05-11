using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]  //打磨已完成的脚本
[AddComponentMenu("Control Script/FPS Input")]
public class FPSInput : MonoBehaviour
{
    private CharacterController _charController;
    public static float speed = 6.0f;
    public float gravity = -9.8f;
    public const float baseSpeed = 6.0f;

    //引用游戏中的UI对象
    [SerializeField] private Text scoreLabel;


    private void Awake()
    {
        Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged); //监听速度改变事件，如果事件发生了（收到了消息），则调用OnSpeedChanged函数，改变玩家的速度
    }

    private void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }

    // Start is called before the first frame update
    void Start()
    {
        _charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed;
        float deltaZ = Input.GetAxis("Vertical") * speed;
        Vector3 movement =new Vector3 (deltaX, 0, deltaZ);
        movement = Vector3.ClampMagnitude(movement, speed); //将movement向量的模限制在一个值（这里是speed） 
        movement.y = gravity;


        movement *= Time.deltaTime;
        movement = transform.TransformDirection(movement);

        _charController.Move(movement);

    }
    private void OnSpeedChanged(float value)
    {
        speed = baseSpeed * value;
    }
}
