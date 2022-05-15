using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField] private Transform target; //target是摄像机进行跟随的对象


    public float rotSpeed = 1.5f;
    private float _rotY;
    private Vector3 _offset;  //设定好偏移，就可以维持摄像机与角色之间的距离，使摄像机就像一个月球，角色就是一个地球

    // Start is called before the first frame update
    void Start()
    {
        //当前脚本对象的欧拉角的y值，绕y轴的旋转值，也就是水平转向
        //rotY是摄像机的水平转向
        _rotY = transform.eulerAngles.y;  
        _offset = target.position - transform.position;  //摄像机和目标之间的起始位置偏移
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate() //每帧调用，在Update之后调用
    {
        float horInput = Input.GetAxis("Horizontal");
        if (horInput != 0) //使用键盘或鼠标旋转摄像机
        {
            _rotY += horInput * rotSpeed;  //_rotY是绝对角度
        }
        else
        {
            _rotY += Input.GetAxis("Mouse X") * rotSpeed * 3;
        }

        Quaternion rotation = Quaternion.Euler(0, _rotY, 0);


        // 旋转摄像机会影响摄像机的位置
        // offset是玩家位置-摄像机位置的一个向量，就是摄像机射向玩家的方向向量
        // 考虑(rotation * _offset)，这是用四元数rotation对“方向向量”的一次旋转
        // 而 target.position 没变，方向向量的头仍是玩家，尾部就是摄像机的位置，所以摄像机的位置根据“方向向量”的旋转而改变
        transform.position = target.position - (rotation * _offset);

        // 上面设定好了摄像机的位置，下面使相机看向target，即通过旋转，使摄像机的z轴指向target
        transform.LookAt(target);  //不管摄像机在目标的什么地方，摄像机总是面向目标

        
    }

}
