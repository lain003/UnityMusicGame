using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class BaseSheetPresenter : MonoBehaviour {
	public BaseSheet baseSheet;
	public BGMAudioSource_Editor bgmAudioSource;

	// Use this for initialization
	void Start () {
		bgmAudioSource.detailMusicTime.Subscribe (x => baseSheet.move (x));
		//bgmAudioSource.aboutMusicTime.Subscribe (x => baseSheet.move (x));
	}
	
	// Update is called once per frame
	void Update () {
	}
}
