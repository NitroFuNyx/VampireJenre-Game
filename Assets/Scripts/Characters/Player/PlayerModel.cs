using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [Header("Model Data")]
    [Space]
    [SerializeField] private PlayerModels modelType;

    private Animator animatorComponent;

    public PlayerModels ModelType { get => modelType; }
    public Animator AnimatorComponent { get => animatorComponent; }

    private void Awake()
    {
        animatorComponent = GetComponent<Animator>();
    }
}
