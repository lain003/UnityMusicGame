using UnityEngine;
using System.Collections;

public class MyMath : MonoBehaviour
{
	/// ------------------------------------------------------------------------
	/// <summary>
	///     指定した精度の数値に切り捨てします。</summary>
	/// <param name="fValue">
	///     丸め対象の倍精度浮動小数点数。</param>
	/// <param name="iDigits">
	///     戻り値の有効桁数の精度。</param>
	/// <returns>
	///     iDigits に等しい精度の数値に切り捨てられた数値。</returns>
	/// ------------------------------------------------------------------------
	public static double ToRoundDown(float fValue, int iDigits) {
		double dCoef = System.Math.Pow(10, iDigits);
		decimal dValue = (decimal)fValue;//floatからdoubleへの変換時の、丸め誤差対策
		return System.Math.Floor  ((double)dValue * dCoef) / dCoef;
	}

	public static float ToRoundDown_MyEffectiveDigit(float value){
		return (float)MyMath.ToRoundDown (value, MyConfig.Editor.EFFECTIVE_DIGIT);
	}
}

