using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class TalentWheel : MonoBehaviour
{
    [SerializeField] private List<TalentItem> Items;
    [SerializeField] private AnimationCurve curve;
    
    private Keyframe frame;
    private Keyframe startKeyframe = new Keyframe(0,0.1f);
    private int counter;

    public System.Action<TalentItem> OnTalentToUpgradeDefined;

    [ContextMenu("Talents/StartWheel")]
    public void StartWheel()
    {
        var choose = Random.Range(0, TalentSlotAmount.maxSlotAmount);
        var circles = Random.Range(2, 4);
        Debug.Log($"Talent #{choose} and {circles} circles");
        StartCoroutine(WheelRotating(choose,circles));
    }

    public IEnumerator WheelRotating(int choose,int circles)
    {
        frame.time=  circles*TalentSlotAmount.maxSlotAmount -(TalentSlotAmount.maxSlotAmount-choose);
        Debug.Log($"Estimated iterations: { circles*TalentSlotAmount.maxSlotAmount -(TalentSlotAmount.maxSlotAmount-choose)}" );
        frame.value = 0.5f;
        curve.MoveKey(0, startKeyframe);
        curve.MoveKey(1, frame);

       
        counter = 0;
      
        for (int i = 0; i < circles; i++)
        {
            for (int j = 0; j < TalentSlotAmount.maxSlotAmount; j++)
            {
                Image image = Items[j].GetComponent<Image>();
                image.color = Color.blue;
                var delay = curve.Evaluate(counter);
                
                yield return new WaitForSecondsRealtime(delay);
                Debug.Log($"evaluationParameter: {counter} delay :{delay}");
               
                    image.color = Color.white;
                    if(i==circles-1&& j ==choose)
                    {
                        Items[j].GetComponent<Image>().color = Color.cyan;

                        OnTalentToUpgradeDefined?.Invoke(Items[j]);

                        break;
                    }

                    counter++;
            }
        }
    }
}
