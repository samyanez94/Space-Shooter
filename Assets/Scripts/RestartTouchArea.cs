using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RestartTouchArea : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
	public GameController gameController;

	private bool touched;

	void Awake ()
	{
		touched = false;
	}

	public void OnPointerDown (PointerEventData data)
	{
		// Set our start point
		if (!touched) {
			touched = true;
			gameController.RestartGame ();
		}
	}

	public void OnPointerUp (PointerEventData data)
	{
	}

}
