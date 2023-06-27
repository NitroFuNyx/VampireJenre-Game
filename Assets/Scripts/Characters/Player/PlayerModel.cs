using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    [Header("Model Data")]
    [Space]
    [SerializeField] private PlayersCharactersTypes modelType;

    private Animator animatorComponent;

    public PlayersCharactersTypes ModelType { get => modelType; }
    public Animator AnimatorComponent { get => animatorComponent; }

    private void Awake()
    {
        animatorComponent = GetComponent<Animator>();
    }
}
