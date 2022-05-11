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

    private void Awake() //如果在Awake中设定监听器，岂不是只会被调用一次？然而每次Update都有可能生成新敌人，都要改变其速度
    {
    }

    private void OnDestroy()
    {
        Messenger<float>.RemoveListener(GameEvent.SPEED_CHANGED, OnSpeedChanged);
    }*/

    // Start is called before the first frame update
    void Start()
    {
        speedSlider.value = PlayerPrefs.GetFloat("speed", 1); // 保存速度设置，GetFloat获取speed的值，若speed没有设定过值，
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
           // Messenger<float>.AddListener(GameEvent.SPEED_CHANGED, OnSpeedChanged); //监听速度改变事件，如果事件发生了（收到了消息），

        }

    }

/*    private void OnSpeedChanged(float value)
    {
        enemy_temp = _enemy.GetComponent<WanderingAI>();
    }*/
}
