using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TalentWheel : MonoBehaviour
{
    [SerializeField] private List<TalentItem> Items;
    [SerializeField] private AnimationCurve curve;

    private AudioManager _audioManager;

    private Keyframe frame;
    private Keyframe startKeyframe = new Keyframe(0,0.1f);
    private int counter;

    public System.Action<TalentItem> OnTalentToUpgradeDefined;

    #region Zenject
    [Inject]
    private void Construct(AudioManager audioManager)
    {
        _audioManager = audioManager;
    }
    #endregion Zenject

    [ContextMenu("Talents/StartWheel")]
    public void StartWheel()
    {
        ResetTalentItemsImages();
        var choose = Random.Range(0, TalentSlotAmount.maxSlotAmount);
        var circles = Random.Range(2, 4);
        //Debug.Log($"Talent #{choose} and {circles} circles");
        StartCoroutine(WheelRotating(choose,circles));
    }

    private void ResetTalentItemsImages()
    {
        for(int i = 0; i < Items.Count; i++)
        {
            Items[i].ChangeTalentSelectionState(false);
        }
    }

    private IEnumerator WheelRotating(int choose,int circles)
    {
        frame.time=  circles*TalentSlotAmount.maxSlotAmount -(TalentSlotAmount.maxSlotAmount-choose);
        //Debug.Log($"Estimated iterations: { circles*TalentSlotAmount.maxSlotAmount -(TalentSlotAmount.maxSlotAmount-choose)}" );
        frame.value = 0.5f;
        curve.MoveKey(0, startKeyframe);
        curve.MoveKey(1, frame);

       
        counter = 0;
      
        for (int i = 0; i < circles; i++)
        {
            for (int j = 0; j < TalentSlotAmount.maxSlotAmount; j++)
            {
                Items[j].ChangeTalentSelectionState(true);
                _audioManager.PlaySFXSound_TalentSlotChange();
                var delay = curve.Evaluate(counter);
                
                yield return new WaitForSecondsRealtime(delay);
                //Debug.Log($"evaluationParameter: {counter} delay :{delay}");

                    Items[j].ChangeTalentSelectionState(false);
                    if (i==circles-1&& j ==choose)
                    {
                        Items[j].ChangeTalentSelectionState(true);

                        OnTalentToUpgradeDefined?.Invoke(Items[j]);

                        break;
                    }

                    counter++;
            }
        }

        _audioManager.PlaySFXSound_TalentGranted();
    }
}
