using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField] private Transform target; //target����������и���Ķ���


    public float rotSpeed = 1.5f;
    private float _rotY;
    private Vector3 _offset;  //�趨��ƫ�ƣ��Ϳ���ά����������ɫ֮��ľ��룬ʹ���������һ�����򣬽�ɫ����һ������

    // Start is called before the first frame update
    void Start()
    {
        //��ǰ�ű������ŷ���ǵ�yֵ����y�����תֵ��Ҳ����ˮƽת��
        //rotY���������ˮƽת��
        _rotY = transform.eulerAngles.y;  
        _offset = target.position - transform.position;  //�������Ŀ��֮�����ʼλ��ƫ��
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate() //ÿ֡���ã���Update֮�����
    {
        float horInput = Input.GetAxis("Horizontal");
        if (horInput != 0) //ʹ�ü��̻������ת�����
        {
            _rotY += horInput * rotSpeed;  //_rotY�Ǿ��ԽǶ�
        }
        else
        {
            _rotY += Input.GetAxis("Mouse X") * rotSpeed * 3;
        }

        Quaternion rotation = Quaternion.Euler(0, _rotY, 0);


        // ��ת�������Ӱ���������λ��
        // offset�����λ��-�����λ�õ�һ�����������������������ҵķ�������
        // ����(rotation * _offset)����������Ԫ��rotation�ԡ�������������һ����ת
        // �� target.position û�䣬����������ͷ������ң�β�������������λ�ã������������λ�ø��ݡ���������������ת���ı�
        transform.position = target.position - (rotation * _offset);

        // �����趨�����������λ�ã�����ʹ�������target����ͨ����ת��ʹ�������z��ָ��target
        transform.LookAt(target);  //�����������Ŀ���ʲô�ط����������������Ŀ��

        
    }

}
