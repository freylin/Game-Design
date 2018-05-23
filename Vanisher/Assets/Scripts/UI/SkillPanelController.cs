using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanelController : MonoBehaviour {
    public GameObject stonePlaceHolder, smokePlaceHolder, runPlaceHolder;

    private Image stoneImage, smokeImage, runImage;
    private CharacterAbilityController abilityController;
    private Ability[] abilities;
    private int selected;
	Text stoneText;
	Text smokeText;
	Text runText;

    private Vector4 colorSelected;
    private Vector4 colorUnselected;

    private void Awake()
    {
        abilityController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterAbilityController>();
        if(abilityController == null)
        {
            Debug.Log("No ability controller");
        }
        abilities = abilityController.abilities;
        if(abilities == null)
        {
            Debug.Log("No abilities");
        }
        stoneImage = stonePlaceHolder.transform.Find("StoneCD").Find("Stone").GetComponent<Image>();
        smokeImage = smokePlaceHolder.transform.Find("SmokeCD").Find("Smoke").GetComponent<Image>();
        runImage = runPlaceHolder.transform.Find("RunCD").Find("Run").GetComponent<Image>();

		stoneText = stonePlaceHolder.transform.Find ("Count").GetComponent<Text> ();
		smokeText = smokePlaceHolder.transform.Find ("Count").GetComponent<Text> ();
		runText = runPlaceHolder.transform.Find ("Count").GetComponent<Text> ();

		//for (int i = 0; i < abilities.Length; i++) {
		//	//Debug.Log (abilities [i].name);
		//	//Debug.Log(abilities[i].currentNumber);
		//	switch (abilities[i].name) {
				
		//	case "Distract":
		//		stoneText.text = (abilities[i].currentNumber).ToString();
		//		break;
		//	case "Smoke Screen":
		//		smokeText.text = (abilities[i].currentNumber).ToString();
		//		break;
		//	case "Accelerate":
		//		runText.text = (abilities[i].currentNumber).ToString();
		//		break;
		//	}
		//}
    }


    // Use this for initialization
    void Start () {
        colorUnselected = stonePlaceHolder.GetComponent<Image>().color;
		colorSelected = Color.gray;
	}
	
	// Update is called once per frame
	void Update () {
        // show current number of each skill
        ShowAbilityCounts();

        // skill selecting effect
        selected = abilityController.abilitySelected;
		if(selected >= 0 && abilities[selected]._isInCooldown == false)
        {
            switch (abilities[selected].name)
            {
				case "Distract":
					stonePlaceHolder.GetComponent<Image> ().color = colorSelected;
					smokePlaceHolder.GetComponent<Image> ().color = colorUnselected;
					runPlaceHolder.GetComponent<Image> ().color = colorUnselected;
					stoneText.text = "" + abilities [selected].currentNumber;
                    break;

                case "Smoke Screen":
                    smokePlaceHolder.GetComponent<Image>().color = colorSelected;
                    stonePlaceHolder.GetComponent<Image>().color = colorUnselected;
                    runPlaceHolder.GetComponent<Image>().color = colorUnselected;
					smokeText.text = "" + abilities [selected].currentNumber;
					Debug.Log ("enter Smoke");
                    break;

                case "Accelerate":
                    runPlaceHolder.GetComponent<Image>().color = colorSelected;
                    stonePlaceHolder.GetComponent<Image>().color = colorUnselected;
                    smokePlaceHolder.GetComponent<Image>().color = colorUnselected;
					runText.text = "" + abilities [selected].currentNumber;
                    break;
            }
        }
        else
        {
            stonePlaceHolder.GetComponent<Image>().color = colorUnselected;
            smokePlaceHolder.GetComponent<Image>().color = colorUnselected;
            runPlaceHolder.GetComponent<Image>().color = colorUnselected;
        }

        // skill cooling down effect
        for(int i = 0; i < abilities.Length; ++i)
        {
            float t = abilities[i].inCooldownTime / abilities[i].cooldown;

            t = Mathf.Clamp(t, 0f, 1f);

            switch (abilities[i].name)
            {
                case "Distract":
                    stoneImage.fillAmount = t;
                    break;
                case "Smoke Screen":
                    smokeImage.fillAmount = t;
                    break;
                case "Accelerate":
                    runImage.fillAmount = t;
                    break;
            }
        }
	}


    private void ShowAbilityCounts()
    {
        Debug.Log("show ability counts");
        for (int i = 0; i < abilities.Length; i++)
        {
            Debug.Log("number = " + abilities[i].currentNumber);
            switch (abilities[i].name)
            {
                case "Distract":
                    stoneText.text = (abilities[i].currentNumber).ToString();
                    break;
                case "Smoke Screen":
                    smokeText.text = (abilities[i].currentNumber).ToString();
                    break;
                case "Accelerate":
                    runText.text = (abilities[i].currentNumber).ToString();
                    break;
            }
        }
    }
}
