using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [Header("Model Data")]
    [Space]
    [SerializeField] private PlayerClasses modelType;

    private Animator animatorComponent;

    public PlayerClasses ModelType { get => modelType; }
    public Animator AnimatorComponent { get => animatorComponent; }

    private void Awake()
    {
        animatorComponent = GetComponent<Animator>();
    }
}
