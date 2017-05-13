using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SimpleTouchAreaButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	private bool touched;
	private bool canFire;
	private int pointerID;

	void Awake ()
	{
		touched = false;
	}

	public void OnPointerDown (PointerEventData data)
	{
		// Set our start point
		if (!touched) {
			touched = true;
			pointerID = data.pointerId;
			canFire = true;
		}
	}

	public void OnPointerUp (PointerEventData data)
	{
		// Reset everything
		if (data.pointerId == pointerID) {
			canFire = false;
			touched = false;
		}
	}

	public bool CanFire ()
	{
		return canFire;
	}

}
