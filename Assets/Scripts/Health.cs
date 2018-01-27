using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public float HP = 10;
    public float MaxHP = 10;

    public float Speed = 1;

    public Transform PointBar;
    
	// Use this for initialization
	void Start ()
	{
	    RecalculateBar();

	}

    public void AddDamage(float ammount)
    {
        HP -= ammount;
        if(HP>0)
            RecalculateBar();
    }

    private void RecalculateBar()
    {
        var percentage = (HP / MaxHP);
        PointBar.transform.localScale = new Vector3(percentage * 0.95f, PointBar.transform.localScale.y, PointBar.transform.localScale.z);
    }

    void FixedUpdate()
    {
        var initialRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);

        transform.LookAt(Camera.main.transform);
        var targetRoation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y, transform.localRotation.eulerAngles.z);

        transform.localRotation = Quaternion.Lerp(initialRotation, targetRoation, 0.1f);
    }
}
