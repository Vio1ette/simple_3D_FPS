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
        //OnSpeedValue���������������浽speed��������
        speedSlider.value = PlayerPrefs.GetFloat("speed", 1); // �����ٶ����ã�GetFloat��ȡspeed��ֵ����speedû���趨��ֵ����GetFloatʹ��Ĭ��ֵ1
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSubmitName(string name) //�û���������������ɰ�Enterʱ�������÷���
    {
        Debug.Log(name);
    }

    public void OnSpeedValue(float speed) //���û�����������ʱ�����÷�����speed�ǻ��������ڵ�λ��[0,2]
    {
        /*        Debug.Log("Speed: " + speed);
        */
        Messenger<float>.Broadcast(GameEvent.SPEED_CHANGED, speed);
    }  


}
