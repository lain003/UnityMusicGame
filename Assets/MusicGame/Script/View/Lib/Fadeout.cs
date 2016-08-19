using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;

public class Fadeout : MonoBehaviour {
	public Image image;
	public float count_time;
	public float delay_time;

	void Start()
	{
		const int ALPHA_RANGE = 100;//徐々に値を減らすために、Alphaの1を100として計算する。
		const float REFRESH_RATE = 0.1f;

		float alpha_per_second = ALPHA_RANGE / count_time;
		Observable.Timer (TimeSpan.FromSeconds (delay_time), TimeSpan.FromSeconds (REFRESH_RATE))
			.Select (x => (ALPHA_RANGE - alpha_per_second * x * REFRESH_RATE) / 100f)
			.TakeWhile (x => x > 0)
			.Subscribe (x => {
				Color color = image.color;
				color.a = x;
				image.color = color;
			}, () => UnityEngine.Object.Destroy (this.gameObject))
			.AddTo (this);
	}
}