using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour
{
    [SerializeField] private NavigationFlow _navigationFlow;
    [SerializeField] private BoxMovement _boxMovement;
    [SerializeField] private BoxFloorColliderScript _boxFloorColliderScript;
    [SerializeField] private ModelSpawner _modelSpawner;
    [SerializeField] private StoneSpawner _stoneSpawner;
    [SerializeField] private BombSpawner _bombSpawner;
    [SerializeField] private GameObject boxGameObject;

    private short score = 0;
    private bool isPLaying = false, isGameOver = false;
    private byte bombSpawnerValue = 1;
    public UnityEvent OnGameOver { get; } = new UnityEvent();
    public UnityEvent<int> OnScoreValueChanged { get; } = new UnityEvent<int>();
    private void Start()
    {
        _navigationFlow.OnStart.AddListener(StartGame);
        _navigationFlow.OnPause.AddListener(PauseGame);
        _boxFloorColliderScript.OnBoxEntered.AddListener(ChangeScore);
        _navigationFlow.Init();
    }

    private void Update()
    {
        if (isPLaying)
        {
            _boxMovement.UpdateFrame();
            if (Input.GetKeyDown(KeyCode.RightArrow))
                ChangeScore(5);
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                ChangeScore(-5);
            return;
        }
        if(isGameOver)
        {
            score = 0;
            _stoneSpawner.DestroyStones();
            _modelSpawner.StopSpawn();
            boxGameObject.SetActive(false);
            OnGameOver?.Invoke();
            isGameOver = false;
        }
    }
    private void StartGame(bool isNewGame)
    {

        boxGameObject.SetActive(true);
        _modelSpawner.Init();
        _stoneSpawner.StartSpawn((byte)(7+bombSpawnerValue));
        _boxMovement.Init();    
        isPLaying = true;
        if (!isNewGame)
        {
            OnScoreValueChanged?.Invoke(score);
            return;
        }
        score = 0;
    }
    private void PauseGame()
    {
        _stoneSpawner.DestroyStones();
        _modelSpawner.StopSpawn();
        boxGameObject.SetActive(false);
        isPLaying = false;
    }
    private void ChangeScore(short value)
    {
        score = (short)(value < -5 ? -1 : score + value);
        OnScoreValueChanged?.Invoke(score);
        if (score < 0)
        {
            isPLaying = false;
            isGameOver = true;
            return;
        }
        ControlBadSpawn(score);
        _modelSpawner.ChangeWaitSeconds(score);
    }
    private void ControlBadSpawn(int value)
    {
        byte temp = (byte)Mathf.FloorToInt(value / 15);

        if (temp > bombSpawnerValue)
        {
            _stoneSpawner.StartSpawn(1);
            bombSpawnerValue = temp;
            _bombSpawner.OnSpawnBomb?.Invoke();
        }
        if (temp < bombSpawnerValue)
        {
            bombSpawnerValue = temp;
            _stoneSpawner.DestroyStone();
        }
    }
    
}