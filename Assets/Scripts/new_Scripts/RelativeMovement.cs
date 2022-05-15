using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]

public class RelativeMovement : MonoBehaviour
{
    [SerializeField] private Transform target; //这个脚本自身对象是玩家，而target是摄像机

    private ControllerColliderHit _contact; //碰撞数据

    //跳跃
    public float jumpSpeed = 15.0f;
    public float gravity = -9.8f;
    public float termicalVelocity = -10.0f;
    public float minFall = -1.5f;
    private float _vertSpeed;


    //旋转、移动
    public float rotSpeed = 15.0f;
    public float moveSpeed = 6.0f;

    private CharacterController _charController;

    // Start is called before the first frame update
    void Start()
    {
        _charController=GetComponent<CharacterController>();
        _vertSpeed = minFall; //将垂直速度初始化为最小下落速度
    }
    private void Update()
    {
        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");

        Vector3 movement = Vector3.zero;


        bool hitGround = false;
        RaycastHit hit;

        if (_vertSpeed < 0&&
            Physics.Raycast(transform.position,Vector3.down,out hit)) {  //down应该是全局的“下”方向，注意射线是从人物身体中心发射的
            float check =
                (_charController.height + _charController.radius) / 1.9f; // 前者的高度为角色的高度，不包含球面的那段；而后者专指那段球面的半径；
            //这两项相加除以2，刚好是人物中心到脚底的长度，这里故意除以1.9f，增大check，即使人物不是真的站在地面上，即使有一丁点儿浮空，也会被判定为站在地面上
            // 如果 hit.distance > check，说明人物是浮空的，
            hitGround = hit.distance <= check;
        }


/*        if (_charController.isGrounded)//如果在地面上
*/      if(hitGround)  
        {
            if (Input.GetButtonDown("Jump"))
            {
                _vertSpeed = jumpSpeed; //_vertSpeed是正的
            }
            else _vertSpeed = minFall;
        }
        else
        {
            _vertSpeed += gravity * 5 * Time.deltaTime;  ////_vertSpeed是负的，越小表示下落速度越来越快
            if (_vertSpeed < termicalVelocity)  //限制下落速度的持续增大
            {
                _vertSpeed = termicalVelocity;
            }

            if (_charController.isGrounded)  //处理特殊情况，光线投射没有检测到地面，但胶囊体接触到了地面
            {
                if (Vector3.Dot(movement, _contact.normal) < 0) { 
                
                    movement = _contact.normal * moveSpeed;  //小于0说明移动向量和碰撞点的法向量之间的夹角 > 90°（反向） 
                }
                else
                {
                    movement += _contact.normal * moveSpeed; //大于0说明移动向量和碰撞点的法向量之间的夹角 < 90°（同向） , 此时 movement 会受到碰撞体的移动助力
                }
            }

        }

        if (horInput != 0 || vertInput != 0)
        {
            movement.x = horInput * moveSpeed;
            movement.z = vertInput * moveSpeed;
            movement = Vector3.ClampMagnitude(movement, moveSpeed); //限制对角线的移动速度，使它和沿着轴移动的速度一样


            //【不太理解，其实】  
            //这段代码实现了，让移动向量相对于摄像机，比如按'A'键，角色应该向左转，这个“左”原本是相对于玩家的，这段代码让“左”始终是相对于摄像机的左边
            Quaternion tmp = target.rotation;  //保存摄像机的初始旋转
            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);  //使摄像机仅绕y轴旋转，而不是绕全部三根轴旋转。如果注释掉这行，按W或S会导致人物会倒下
            movement = target.TransformDirection(movement); //将movement从摄像机的坐标空间转换为世界坐标空间
            //为什么要转换到世界空间？，是因为下面LookRotation的参数要求吗
            target.rotation = tmp; // 摄像机要恢复转向？？ ，这里没有给摄像机添加上下旋转，即绕x轴，所以即使不恢复也看不出来错误

            // LookRotation将矢量转换为四元数，获取一个向量所表示的方向（将一个向量转为该向量所对应的方向）
            Quaternion direction = Quaternion.LookRotation(movement); //设置脚本对象（玩家）的转向为movement的方向
            transform.rotation = Quaternion.LookRotation(movement); //设置脚本对象（玩家）的转向为movement的方向

            // transform.rotation 是原来的转向，direction是变化后的转向，在它们之间做插值，参数用 rotSpeed*Time.deltaTime
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);  //Time.deltaTime为帧间隔时间，不太清楚这里这里怎么用参数来插值的，参数不应该属于[0.1]吗
            //rotSpeed*Time.deltaTime的结果是属于[0,1]吗

        }


        movement.y = _vertSpeed;  //设置y值不是瞬间的吗，为啥不是按下空格，人立马就出现在空中，而是有个跳起的过程？实现跳起不需要插值，难道Move内部已经进行了插值？？
        movement *= Time.deltaTime; // 使movement独立于帧率
        _charController.Move(movement);

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)  //检测碰撞时，将碰撞数据保存在回调中
    {
        _contact = hit;
    }

}
