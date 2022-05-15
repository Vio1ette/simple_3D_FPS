using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(CharacterController))]

public class RelativeMovement : MonoBehaviour
{
    [SerializeField] private Transform target; //����ű������������ң���target�������

    private ControllerColliderHit _contact; //��ײ����

    //��Ծ
    public float jumpSpeed = 15.0f;
    public float gravity = -9.8f;
    public float termicalVelocity = -10.0f;
    public float minFall = -1.5f;
    private float _vertSpeed;


    //��ת���ƶ�
    public float rotSpeed = 15.0f;
    public float moveSpeed = 6.0f;

    private CharacterController _charController;

    // Start is called before the first frame update
    void Start()
    {
        _charController=GetComponent<CharacterController>();
        _vertSpeed = minFall; //����ֱ�ٶȳ�ʼ��Ϊ��С�����ٶ�
    }
    private void Update()
    {
        float horInput = Input.GetAxis("Horizontal");
        float vertInput = Input.GetAxis("Vertical");

        Vector3 movement = Vector3.zero;


        bool hitGround = false;
        RaycastHit hit;

        if (_vertSpeed < 0&&
            Physics.Raycast(transform.position,Vector3.down,out hit)) {  //downӦ����ȫ�ֵġ��¡�����ע�������Ǵ������������ķ����
            float check =
                (_charController.height + _charController.radius) / 1.9f; // ǰ�ߵĸ߶�Ϊ��ɫ�ĸ߶ȣ�������������ǶΣ�������רָ�Ƕ�����İ뾶��
            //��������ӳ���2���պ����������ĵ��ŵ׵ĳ��ȣ�����������1.9f������check����ʹ���ﲻ�����վ�ڵ����ϣ���ʹ��һ��������գ�Ҳ�ᱻ�ж�Ϊվ�ڵ�����
            // ��� hit.distance > check��˵�������Ǹ��յģ�
            hitGround = hit.distance <= check;
        }


/*        if (_charController.isGrounded)//����ڵ�����
*/      if(hitGround)  
        {
            if (Input.GetButtonDown("Jump"))
            {
                _vertSpeed = jumpSpeed; //_vertSpeed������
            }
            else _vertSpeed = minFall;
        }
        else
        {
            _vertSpeed += gravity * 5 * Time.deltaTime;  ////_vertSpeed�Ǹ��ģ�ԽС��ʾ�����ٶ�Խ��Խ��
            if (_vertSpeed < termicalVelocity)  //���������ٶȵĳ�������
            {
                _vertSpeed = termicalVelocity;
            }

            if (_charController.isGrounded)  //�����������������Ͷ��û�м�⵽���棬��������Ӵ����˵���
            {
                if (Vector3.Dot(movement, _contact.normal) < 0) { 
                
                    movement = _contact.normal * moveSpeed;  //С��0˵���ƶ���������ײ��ķ�����֮��ļн� > 90�㣨���� 
                }
                else
                {
                    movement += _contact.normal * moveSpeed; //����0˵���ƶ���������ײ��ķ�����֮��ļн� < 90�㣨ͬ�� , ��ʱ movement ���ܵ���ײ����ƶ�����
                }
            }

        }

        if (horInput != 0 || vertInput != 0)
        {
            movement.x = horInput * moveSpeed;
            movement.z = vertInput * moveSpeed;
            movement = Vector3.ClampMagnitude(movement, moveSpeed); //���ƶԽ��ߵ��ƶ��ٶȣ�ʹ�����������ƶ����ٶ�һ��


            //����̫��⣬��ʵ��  
            //��δ���ʵ���ˣ����ƶ��������������������簴'A'������ɫӦ������ת���������ԭ�����������ҵģ���δ����á���ʼ�������������������
            Quaternion tmp = target.rotation;  //����������ĳ�ʼ��ת
            target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);  //ʹ���������y����ת����������ȫ����������ת�����ע�͵����У���W��S�ᵼ������ᵹ��
            movement = target.TransformDirection(movement); //��movement�������������ռ�ת��Ϊ��������ռ�
            //ΪʲôҪת��������ռ䣿������Ϊ����LookRotation�Ĳ���Ҫ����
            target.rotation = tmp; // �����Ҫ�ָ�ת�򣿣� ������û�и���������������ת������x�ᣬ���Լ�ʹ���ָ�Ҳ������������

            // LookRotation��ʸ��ת��Ϊ��Ԫ������ȡһ����������ʾ�ķ��򣨽�һ������תΪ����������Ӧ�ķ���
            Quaternion direction = Quaternion.LookRotation(movement); //���ýű�������ң���ת��Ϊmovement�ķ���
            transform.rotation = Quaternion.LookRotation(movement); //���ýű�������ң���ת��Ϊmovement�ķ���

            // transform.rotation ��ԭ����ת��direction�Ǳ仯���ת��������֮������ֵ�������� rotSpeed*Time.deltaTime
            transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotSpeed * Time.deltaTime);  //Time.deltaTimeΪ֡���ʱ�䣬��̫�������������ô�ò�������ֵ�ģ�������Ӧ������[0.1]��
            //rotSpeed*Time.deltaTime�Ľ��������[0,1]��

        }


        movement.y = _vertSpeed;  //����yֵ����˲�����Ϊɶ���ǰ��¿ո�������ͳ����ڿ��У������и�����Ĺ��̣�ʵ��������Ҫ��ֵ���ѵ�Move�ڲ��Ѿ������˲�ֵ����
        movement *= Time.deltaTime; // ʹmovement������֡��
        _charController.Move(movement);

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)  //�����ײʱ������ײ���ݱ����ڻص���
    {
        _contact = hit;
    }

}
