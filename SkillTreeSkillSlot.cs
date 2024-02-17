using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillTreeSkillSlot : MonoBehaviour
{
    public Skill skill;

    public Image myImage;
    public TextMeshProUGUI characterPoints;
    public TextMeshProUGUI skillPoints;

    public GameObject GameMenu;

    public void RefreshSkill()
    {
        myImage.sprite = skill.skillIcon;
        characterPoints.text = "SP:" + skill.CharacterSPCost;
        skillPoints.text = "CP:" + skill.OverallSPCost;
    }

    public void BuySkill()
    {

        var x = 0;

        for (int i = 0; i < GameMenu.GetComponent<GameMenu>().CharacterSkillPointNames.Count; i++) //Finds the current character name and matches it to the id in the list
        {
            if (GameMenu.GetComponent<GameMenu>().CharacterSkillPointNames[i] == GameMenu.GetComponent<GameMenu>().currentCharacter)
            {
                x = i;
            }
        }

        if (GameMenu.GetComponent<GameMenu>().overallSkillPoints >= skill.OverallSPCost && GameMenu.GetComponent<GameMenu>().CharacterSkillPoints[x] >= skill.CharacterSPCost) //costs
        {
            for (int i = 0; i < GameMenu.GetComponent<GameMenu>().CharacterSkillsBoughten.Count; i++)
            {
                if (GameMenu.GetComponent<GameMenu>().CharacterSkillsBoughten[i] == skill.skillName)
                {
                    if (GameMenu.GetComponent<GameMenu>().CharacterSkillsBoughtenBool[i] == false)
                    {
                        var evolutionNumberRequirement = 0;
                        for (int j = 0; j < skill.evolutionRequirements.Count; j++)
                        {
                            if (GameMenu.GetComponent<GameMenu>().CharacterSkillsBoughten.Contains(skill.evolutionRequirements[j].skillName))
                            {
                                if (GameMenu.GetComponent<GameMenu>().CharacterSkillsBoughtenBool[j] == true) 
                                {
                                    evolutionNumberRequirement += 1;
                                }
                            }
                        }
                        if (evolutionNumberRequirement == skill.evolutionRequirements.Count)
                        {
                            GameMenu.GetComponent<GameMenu>().CharacterSkillsBoughtenBool[i] = true;
                            GameMenu.GetComponent<GameMenu>().CharacterSkillPoints[x] -= skill.CharacterSPCost;
                            GameMenu.GetComponent<GameMenu>().overallSkillPoints -= skill.OverallSPCost;
                            GameMenu.GetComponent<GameMenu>().UpdateSkillValues();
                        }else
                        {
                            print("don't have all the skills required to unlock this skill.");
                        }
                    }
                    else
                    {
                        print("already boughten");
                    }
                }
            }
        } else
        {
            print("don't have enough points");
        }
    }
}
