using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UniRx;

public class ConfigButtonPresenter : MonoBehaviour {
	public Button button;
	public GameObject configUI;
	// Use this for initialization
	void Start () {
		button.OnClickAsObservable().Subscribe(_ => configUI.SetActive(!configUI.activeSelf));
	}

	// Update is called once per frame
	void Update () {

	}
}
