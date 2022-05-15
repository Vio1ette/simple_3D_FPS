using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    public enum RotationAxes
    {
        MouseXAndY=0,
        MouseX=1,
        MouseY=2
    }

    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityHor = 9.0f;
    public float sensitivityVert = 9.0f;

    //���޵Ĵ�ֱ��Ұ
    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;

    //ƽ�ӣ���ֱ��ת�����¾��ԽǶ�ֵ
    private float _rotationX = 0;

    // Start is called before the first frame update
    void Start()
    {
        //��ҵ���ת����굥�����ƣ�����������治��Ӱ����ת
        Rigidbody body = GetComponent<Rigidbody>();
        if (body != null)
        {
            body.freezeRotation = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (axes == RotationAxes.MouseX)
        {
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
        }
        else if (axes == RotationAxes.MouseY)
        {
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);
            
            //����ΪʲôҪ�ᵽ��y�����ת�أ���Ϊ�����localEulerAngles�����˸�ֵ��localEulerAngles��ֻ����
            //����Ҫnewһ��Vector3������Ϊ�˲��ı�ԭ����y��ֵ���Ǿ�Ҫ���Ȼ�ȡһ��y��ֵ����ԭģԭ����д��ȥ
            float rotationY = transform.localEulerAngles.y;
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
        else  //ͬʱˮƽ��ת�ʹ�ֱ��ת
        {
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

            //Ϊʲô��ֱ��-=����ˮƽ��delta��
            //��Ϊ��ֱ��ת����һ�����ϳ�ʼֵ�ģ�ƽ�ӣ��Ƕ�Ϊ0����-=ֱ�����ýǶ�ֵ���������������ǰ��ֵ���������þ��ԽǶ�ֵ�Ϳ������ƽǶȷ�Χ��
            //��ˮƽ��תÿ�ζ��������֮ǰ��ֵ���е�����ݼ���������û�������޵�
            float delta = Input.GetAxis("Mouse X") * sensitivityHor;
            float rotationY = transform.localEulerAngles.y+delta;

            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);

        }

    }
}
