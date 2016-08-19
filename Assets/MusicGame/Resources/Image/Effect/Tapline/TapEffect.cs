using UnityEngine;
using System.Collections;

public class TapEffect : MonoBehaviour {
	void OnAnimationFinish (){
		Destroy (gameObject);
	}
}
