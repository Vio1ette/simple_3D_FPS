using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SettingsPopup : MonoBehaviour
{

    [SerializeField] private Slider speedSlider;

    public void Open()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        //OnSpeedValue方法将浮点数保存到speed变量当中
        speedSlider.value = PlayerPrefs.GetFloat("speed", 1); // 保存速度设置，GetFloat获取speed的值，若speed没有设定过值，则GetFloat使用默认值1
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSubmitName(string name) //用户在输入域输入完成按Enter时，触发该方法
    {
        Debug.Log(name);
    }

    public void OnSpeedValue(float speed) //当用户调整滑动条时触发该方法，speed是滑动条所在的位置[0,2]
    {
        /*        Debug.Log("Speed: " + speed);
        */
        Messenger<float>.Broadcast(GameEvent.SPEED_CHANGED, speed);
    }  


}
