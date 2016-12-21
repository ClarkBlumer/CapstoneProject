using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class BarScript : MonoBehaviour {

    [SerializeField] private float fillAmount;
    [SerializeField] private Image content;
    public float yellowBarThreshold = 0.49f;
    public float redBarThreshold = 0.2f;
    public byte r = 0, g = 0, b = 0, a=0;
    public float MaxValue { get; set; }
   
	
	// Update is called once per frame
	void Update ()
    {
        HandleBar();
        //content.fillAmount -= 0.001f;
	}

    private void HandleBar()
    {
        if(content.fillAmount != fillAmount)
        {
            if(fillAmount <= yellowBarThreshold && fillAmount > redBarThreshold)
            {
                content.color = new Color32(r, g, b, a);
            }
            else if(fillAmount <= redBarThreshold)
            {
                content.color = Color.red;
            }
            else
            {
                content.color = Color.green;
            }

            content.fillAmount = fillAmount;
            
        }
        //Debug.Log("AfterUpdate:HandleBar.fill=" + content.fillAmount);

    }

    public void UpdateValue(float value)
    {
        fillAmount = Map(value, 0, 100, 0, 1);
    }

    /// <summary>
    /// Takes current health and takes it into a value the scale can understand
    /// </summary>
    /// <param name="value">Value that is the current health</param>
    /// <param name="inMin">Minimum value to use -- 0</param>
    /// <param name="inMax">Maximum value of health</param>
    /// <param name="outMin">Minimum value to return</param>
    /// <param name="outMax">Maximum value to return</param>
    /// <returns></returns>
    private float Map(float value, float inMin, float inMax, float outMin, float outMax)
    {
        //    CurrHealth - 0   * (   1   -   0   ) / (MaxHealth - 0) + 0
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
        // Simple example. This calculation will allow for easy adjustments to max health
        // (80 - 0) * (1 - 0) / (100 - 0) + 0
        // 80 / 100
        // 0.8
    }
}
