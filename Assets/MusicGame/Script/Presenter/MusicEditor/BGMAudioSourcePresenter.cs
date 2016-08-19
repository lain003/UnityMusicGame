using UnityEngine;
using UniRx;

public class BGMAudioSourcePresenter : PresenterBase<MyServerTrack> {
	public BGMAudioSource_Base bgmAudioSource;
	[HideInInspector]
	public Subject<AudioClip> finishMusicDownLoad_Stream = new Subject<AudioClip>();

	protected override IPresenter[] Children{
		get{
			return EmptyChildren;
		}
	}
	protected override void BeforeInitialize(MyServerTrack myServerTrack){
	}
	protected override void Initialize(MyServerTrack myServerTrack){
		Observable.FromCoroutine<AudioClip> (observer => SoundCloud.start_downloadMusic (observer, myServerTrack))
			.Subscribe (audioClip => {
				this.bgmAudioSource.audioSource.clip = audioClip;
				this.bgmAudioSource.play ();
				this.finishMusicDownLoad_Stream.OnNext(audioClip);
				this.finishMusicDownLoad_Stream.OnCompleted();
			}
		);
	}
}
