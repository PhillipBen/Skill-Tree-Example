using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeBGLine : MonoBehaviour
{
    public GameObject skill1;
    public GameObject skill2;


    Vector3 size;
    public void Move()
    {
        var skillOne = skill1.GetComponent<RectTransform>().localPosition; //These two points are your original two points that you want the line to connect to
        var skillTwo = skill2.GetComponent<RectTransform>().localPosition;


        size.x = (float)(Mathf.Abs((skillOne.x - skillTwo.x) / 190f) + 1); // Line settings -- The Length of the line
        size.y = .2f;
        size.z = 1f;


        this.GetComponent<RectTransform>().localScale = size; //width, height
        this.GetComponent<RectTransform>().localPosition = new Vector3(((skillOne.x + skillTwo.x) / 2), ((skillOne.y + skillTwo.y) / 2), 0f); //x,y,z

        var yAxisPoint = skillOne; //These variables have to be initialized outside the if statement, so these are just temporary settings, they may change in the if statement below
        var otherPoint = skillTwo;

        //Need an if statement for if the xs or ys are equal
        if (Mathf.Abs(skillOne.x) < Mathf.Abs(skillTwo.x)) //This determines which of the two points is closer to the yAxis
        {
            yAxisPoint = skillOne;
            otherPoint = skillTwo;
        } else
        {
            yAxisPoint = skillTwo;
            otherPoint = skillOne;
        }

        var rightAnglePoint = new Vector2(yAxisPoint.x, otherPoint.y);


        var hypotenuse = Mathf.Sqrt(Mathf.Pow(skillTwo.x - skillOne.x, 2) + Mathf.Pow(skillTwo.y - skillOne.y, 2));
        var adjacent = Mathf.Sqrt(Mathf.Pow(yAxisPoint.x - rightAnglePoint.x, 2) + Mathf.Pow(yAxisPoint.y - rightAnglePoint.y, 2));
        //var opposite = Mathf.Sqrt(Mathf.Pow(otherPoint.x - rightAnglePoint.x, 2) + Mathf.Pow(otherPoint.y - rightAnglePoint.x, 2));

        var pointInsideAngle = Mathf.Acos(adjacent / hypotenuse);
        pointInsideAngle = pointInsideAngle * (180f / Mathf.PI);

        var pointOutsideAngle = (90 - pointInsideAngle);

        if (otherPoint.x < 0) //The rotation needs to be flipped if the angle is being flipped towards the left side (negative side of x axis)
        {
            pointOutsideAngle = pointOutsideAngle * -1f;

        }
        if (otherPoint.y < 0) //The rotation needs to be flipped if the angle is being flipped towards the bottom side (negative side of y axis)
        {
            pointOutsideAngle = pointOutsideAngle * -1f;
        }else if (otherPoint.y < yAxisPoint.y)
        {
            pointOutsideAngle = pointOutsideAngle * -1f;
        }


        this.GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, pointOutsideAngle);

        //Took me 10.25 hours!!!! And 1 hour thinking about it at work!
        //https://www.triangle-calculator.com/?what=vc This amazing website helped me a ton!
        //And this one too https://www.calculatorsoup.com/calculators/geometry-plane/distance-two-points.php

    }

}
