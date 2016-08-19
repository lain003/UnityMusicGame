using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScrollController : MonoBehaviour {
	void Start () 
	{
	}

	public void addButton(ServerTrackButton trackButton){
		trackButton.transform.SetParent (this.transform, false);
	}
}