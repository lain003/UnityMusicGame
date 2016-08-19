using System.Collections;
using UniRx;
using System;

public class BPM {
	public ReactiveProperty<int> positioning { get; private set; }
	public ReactiveProperty<int> value { get; private set; }

	public BPM(){
		positioning = new ReactiveProperty<int>(0);
		value = new ReactiveProperty<int>(30);
	}

	public BPM(int _bpm, int _position){
		positioning = new ReactiveProperty<int>(_position);
		value = new ReactiveProperty<int>(_bpm);
	}

	public float calculate_interval(){
		return MyMath.ToRoundDown_MyEffectiveDigit(60.0f / this.value.Value);
	}

	public float caluculate_nearTime(float musicTime){
		float interval = this.calculate_interval ();
		float interval_w = interval / 2;
		float positioningTime = caluculateTime_positioning ();
		int count = (int)((musicTime - positioningTime) / interval_w);
		float before_time = MyMath.ToRoundDown_MyEffectiveDigit(count * interval_w + positioningTime);
		float next_time = MyMath.ToRoundDown_MyEffectiveDigit((count + 1) * interval_w + positioningTime);
		if (Math.Abs (musicTime - before_time) < Math.Abs (musicTime - next_time)) {
			return before_time;
		} else {
			return next_time;
		}
	}

	public float caluculate_positioningTime(){
		return MyMath.ToRoundDown_MyEffectiveDigit(this.calculate_interval() * (this.positioning.Value / 100.0f));
	}

	private float caluculateTime_positioning(){
		return positioning.Value / 100.0f * this.calculate_interval ();
	}
}
