using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
public class GameManager : MonoBehaviour
{
    [SerializeField] public UIDocument UIDocument;
    public float SceneSwipeFactor = 900;
    public static GameManager _instance;
    //public static GameManager Instance
    //{
    //    get
    //    {
    //        if (_instance == null)
    //        {
    //            _instance = this;
    //        }
    //        return _instance;
    //    }
    //}
    public static GameManager GetInstance()
    {
        if (_instance == null)
        {
            _instance = new GameObject("_GameManager").AddComponent<GameManager>();
        }

        return _instance;
    }
    public static float GetSceneSwipeFactor()
    {
        return GetInstance().SceneSwipeFactor;
    }


    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if(_instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
