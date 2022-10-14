using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public Player player;

    [SerializeField]
    private GameObject playerPrefab;

    public RoomDataManager roomManager;

    [SerializeField]
    private Vector3 playerStartPos, playerStartRot;

    [SerializeField]
    private string startCam = "";

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        //PlayerSpawn();
    }

    public void ChangeScene(string newScene, Vector3 pos, Vector3 rot, string cam)
    {
        playerStartPos = pos;
        playerStartRot = rot;

        startCam = cam;

        SceneManager.LoadScene(newScene);
    }

    public void SetRoomManager(RoomDataManager roomDataManager)
    {
        roomManager = roomDataManager;
        roomManager.LoadData();
    }

    public void OnPlayerSpawned(Player temp)
    {
        player = temp.GetComponent<Player>();

        SetPlayerSpawnValues();

    }

    private void SetPlayerSpawnValues()
    {
        player.transform.position = playerStartPos;
        player.transform.rotation = Quaternion.Euler(playerStartRot);
        if (startCam != "")
            player.GetComponent<CameraSystem>().ChangeMainCamera(GameObject.Find(startCam).GetComponent<Camera>());
    }

}
