using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Slider speedSlider;


   // public const float baseSpeed = 3.0f;

    private GameObject _enemy;

/*    WanderingAI enemy_temp;

    private void Awake() //�����Awake���趨������������ֻ�ᱻ����һ�Σ�Ȼ��ÿ��Update���п��������µ��ˣ���Ҫ�ı����ٶ�
    {
    }

    private void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }*/

    // Start is called before the first frame update
    void Start()
    {
        speedSlider.value = PlayerPrefs.GetFloat("speed", 1); // �����ٶ����ã�GetFloat��ȡspeed��ֵ����speedû���趨��ֵ��
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemy == null)
        {
            _enemy = Instantiate(enemyPrefab) as GameObject;
            _enemy.transform.position = new Vector3(0, 1, 0);
            float angle = Random.Range(0, 360);
            _enemy.transform.Rotate(0, angle, 0);
           // Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged); //�����ٶȸı��¼�������¼������ˣ��յ�����Ϣ����

        }

    }

/*    private void OnSpeedChanged(float value)
    {
        enemy_temp = _enemy.GetComponent<WanderingAI>();
    }*/
}
