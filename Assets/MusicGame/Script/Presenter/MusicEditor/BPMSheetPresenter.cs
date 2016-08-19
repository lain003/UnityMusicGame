using UnityEngine;
using System;
using UniRx;

public class BPMSheetPresenter : PresenterBase<BPM> {
	public BPMSheet bpmSheet;
	public BGMAudioSourcePresenter bgmPresenter;
	private BPM bpm;

	protected override IPresenter[] Children{
		get{
			return EmptyChildren;
		}
	}

	protected override void BeforeInitialize(BPM _bpm){
	}

	protected override void Initialize(BPM _bpm){
		bpm = _bpm;
		bpm.positioning.Subscribe (x => this.reflect_positioning (x));

		bpm.value.Select(_ => bgmPresenter.bgmAudioSource.audioSource.clip)
			.Where(clip => clip != null)
			.Subscribe (clip => this.redraw_bpmSheet(clip.length));
		this.bgmPresenter.finishMusicDownLoad_Stream
			.Subscribe(clip => this.redraw_bpmSheet(clip.length));
	}

	void Update () {
	}

	private void reflect_positioning(int percentage){
		float distance_adjust = this.bpm.caluculate_positioningTime() * MyConfig.Editor.NOTE_SPEED;
		this.gameObject.transform.localPosition = new Vector3 (this.gameObject.transform.localPosition.x, distance_adjust, this.gameObject.transform.localPosition.z);
	}

	private void redraw_bpmSheet(float audioLength){
		this.bpmSheet.draw_dotLines (bpm.calculate_interval (), audioLength);
		this.reflect_positioning (bpm.positioning.Value);
	}
}
