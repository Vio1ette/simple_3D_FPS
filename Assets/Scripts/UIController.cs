using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIController : MonoBehaviour
{
/*    [SerializeField] private Text scoreLabel;
*/    [SerializeField] private Text healthLabel;
    [SerializeField] private SettingsPopup settingsPopup;
/*    private int _score;
*/    private int _health;

    private void Awake()
    {
        /*        Messenger.AddListener(GameEvent.ENEMY_HIT, OnEnemyHit); 
        */        //ENEMY_HIT是一个事件，监听者监听这个事件是否发生，如果收到这个事件的消息，就会调用OnEnemyHit函数（第二个参数）
        Messenger<int>.AddListener(GameEvent.PLAYER_HIT, OnPlayerHit);
    }

    private void OnDestroy()
    {
/*        Messenger.RemoveListener(GameEvent.ENEMY_HIT, OnEnemyHit);
*/        Messenger<int>.RemoveListener(GameEvent.PLAYER_HIT, OnPlayerHit);
    }

    // Start is called before the first frame update
    void Start()
    {
    /*    _score = 0;*/
        _health = 5;
/*        scoreLabel.text = _score.ToString();   
*/        healthLabel.text = _health.ToString();

        settingsPopup.Close();
    }

    // Update is called once per frame
/*    void Update()
    {
        scoreLabel.text = Time.realtimeSinceStartup.ToString();

    }*/



    public void OnOpenSettings()
    {
        Debug.Log("open setting");
    }

    public void OnpointerDown()
    {
        settingsPopup.Open();
    }

/*    public void OnEnemyHit()
    {
        _score += 1;
        scoreLabel.text = _score.ToString();
    }*/
    public void OnPlayerHit(int health)
    {
        healthLabel.text = health.ToString();
    }
}
