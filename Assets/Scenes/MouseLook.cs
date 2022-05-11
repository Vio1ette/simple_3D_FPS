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

    //有限的垂直视野
    public float minimumVert = -45.0f;
    public float maximumVert = 45.0f;

    //平视，垂直旋转的上下绝对角度值
    private float _rotationX = 0;

    // Start is called before the first frame update
    void Start()
    {
        //玩家的旋转由鼠标单独控制，人物物理仿真不会影响旋转
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
            
            //这里为什么要提到绕y轴的旋转呢？因为这里对localEulerAngles进行了赋值，localEulerAngles是只读的
            //所以要new一个Vector3给它，为了不改变原来的y的值，那就要事先获取一下y的值，再原模原样地写回去
            float rotationY = transform.localEulerAngles.y;
            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
        }
        else  //同时水平旋转和垂直旋转
        {
            _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
            _rotationX = Mathf.Clamp(_rotationX, minimumVert, maximumVert);

            //为什么垂直是-=，而水平是delta？
            //因为垂直旋转是有一个公认初始值的：平视，角度为0，用-=直接设置角度值，而不是相对于以前的值的增量。用绝对角度值就可以限制角度范围了
            //而水平旋转每次都是相对于之前的值进行递增或递减，这样是没有上下限的
            float delta = Input.GetAxis("Mouse X") * sensitivityHor;
            float rotationY = transform.localEulerAngles.y+delta;

            transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);

        }

    }
}
