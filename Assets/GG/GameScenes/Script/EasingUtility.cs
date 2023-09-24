using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasingUtility : MonoBehaviour
{
	public enum EASING_TYPE { LINEAR , QUAD_IN , QUAD_OUT, QUAD_INOUT, CUBIC_IN, CUBIC_OUT, CUBIC_INOUT , QUART_IN ,
		QUART_OUT, QUART_INOUT, QUINT_IN, QUINT_OUT, QUINT_INOUT, SINE_IN, SINE_OUT, SINE_INOUT, EXPO_IN, EXPO_OUT,
		EXPO_INOUT, CIRC_IN, CIRC_OUT, CIRC_INOUT, ELASTIC_IN, ELASTIC_OUT, ELASTIC_INOUT, BOUNCE_IN, BOUNCE_OUT,
		OUT_BACK, INOUT_BACK, TYPE_END
	};

	static public float PI = 3.141592f;

	static public Vector4  LerpToType(Vector4 vStartPoint, Vector4 vTargetPoint, float fPassedTime, float fTotalTime, EASING_TYPE eType)
	{
		switch (eType)
		{
			case EASING_TYPE.LINEAR:
				return Linear(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);
				break;
			case EASING_TYPE.QUAD_IN:
				return QuadIn(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);

				break;
			case EASING_TYPE.QUAD_OUT:
				return QuadInOut(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);
				break;
			case EASING_TYPE.QUAD_INOUT:
				return QuadInOut(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);
				break;
			case EASING_TYPE.CUBIC_IN:
				return CubicIn(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);
				break;
			case EASING_TYPE.CUBIC_OUT:
				return CubicOut(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);
				break;
			case EASING_TYPE.CUBIC_INOUT:
				return CubicInOut(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);
				break;
			case EASING_TYPE.QUART_IN:
				return QuartIn(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);

				break;
			case EASING_TYPE.QUART_OUT:
				return QuartOut(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);

				break;
			case EASING_TYPE.QUART_INOUT:
				return QuartInOut(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);

				break;
			case EASING_TYPE.QUINT_IN:
				return QuintIn(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);

				break;
			case EASING_TYPE.QUINT_OUT:
				return QuintOut(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);

				break;
			case EASING_TYPE.QUINT_INOUT:
				return QuintInOut(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);

				break;
			case EASING_TYPE.SINE_IN:
				return SineIn(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);

				break;
			case EASING_TYPE.SINE_OUT:
				return SineOut(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);

				break;
			case EASING_TYPE.SINE_INOUT:
				return SineInOut(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);
				break;
			case EASING_TYPE.EXPO_IN:
				return ExpoIn(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);

				break;
			case EASING_TYPE.EXPO_OUT:
				return ExpoOut(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);

				break;
			case EASING_TYPE.EXPO_INOUT:
				return ExpoInOut(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);
				break;
			case EASING_TYPE.CIRC_IN:
				return CircIn(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);
				break;
			case EASING_TYPE.CIRC_OUT:
				return CircOut(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);
				break;
			case EASING_TYPE.CIRC_INOUT:
				return CircInOut(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);
				break;
			case EASING_TYPE.ELASTIC_IN:
				return ElasticIn(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);
				break;
			case EASING_TYPE.ELASTIC_OUT:
				return ElasticOut(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);
				break;
			case EASING_TYPE.ELASTIC_INOUT:
				return ElasticInOut(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);
				break;
			case EASING_TYPE.BOUNCE_IN:
				return BounceIn(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);
				break;
			case EASING_TYPE.BOUNCE_OUT:
				return BounceOut(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);
				break;
			case EASING_TYPE.OUT_BACK:
				return OutBack(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);
				break;
			case EASING_TYPE.INOUT_BACK:
				return InOutBack(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);
				break;
			case EASING_TYPE.TYPE_END:
				break;
			default:
				break;
		}

		return Linear(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);
	}

	static public float LerpToType(float vStartPoint, float vTargetPoint, float fPassedTime, float fTotalTime, EASING_TYPE eType)
	{
		switch (eType)
		{
			case EASING_TYPE.LINEAR:
				return Linear(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);
				break;
			case EASING_TYPE.QUAD_IN:
				return QuadIn(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);

				break;
			case EASING_TYPE.QUAD_OUT:
				return QuadInOut(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);
				break;
			case EASING_TYPE.QUAD_INOUT:
				return QuadInOut(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);
				break;
			case EASING_TYPE.CUBIC_IN:
				return CubicIn(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);
				break;
			case EASING_TYPE.CUBIC_OUT:
				return CubicOut(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);
				break;
			case EASING_TYPE.CUBIC_INOUT:
				return CubicInOut(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);
				break;
			case EASING_TYPE.QUART_IN:
				return QuartIn(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);

				break;
			case EASING_TYPE.QUART_OUT:
				return QuartOut(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);

				break;
			case EASING_TYPE.QUART_INOUT:
				return QuartInOut(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);

				break;
			case EASING_TYPE.QUINT_IN:
				return QuintIn(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);

				break;
			case EASING_TYPE.QUINT_OUT:
				return QuintOut(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);

				break;
			case EASING_TYPE.QUINT_INOUT:
				return QuintInOut(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);

				break;
			case EASING_TYPE.SINE_IN:
				return SineIn(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);

				break;
			case EASING_TYPE.SINE_OUT:
				return SineOut(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);

				break;
			case EASING_TYPE.SINE_INOUT:
				return SineInOut(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);
				break;
			case EASING_TYPE.EXPO_IN:
				return ExpoIn(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);

				break;
			case EASING_TYPE.EXPO_OUT:
				return ExpoOut(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);

				break;
			case EASING_TYPE.EXPO_INOUT:
				return ExpoInOut(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);
				break;
			case EASING_TYPE.CIRC_IN:
				return CircIn(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);
				break;
			case EASING_TYPE.CIRC_OUT:
				return CircOut(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);
				break;
			case EASING_TYPE.CIRC_INOUT:
				return CircInOut(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);
				break;
			case EASING_TYPE.ELASTIC_IN:
				return ElasticIn(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);
				break;
			case EASING_TYPE.ELASTIC_OUT:
				return ElasticOut(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);
				break;
			case EASING_TYPE.ELASTIC_INOUT:
				return ElasticInOut(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);
				break;
			case EASING_TYPE.BOUNCE_IN:
				return BounceIn(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);
				break;
			case EASING_TYPE.BOUNCE_OUT:
				return BounceOut(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);
				break;
			case EASING_TYPE.OUT_BACK:
				return OutBack(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);
				break;
			case EASING_TYPE.INOUT_BACK:
				return InOutBack(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);
				break;
			case EASING_TYPE.TYPE_END:
				break;
			default:
				break;
		}

		return Linear(vStartPoint, vTargetPoint, fPassedTime, fTotalTime);
	}

	static public Vector4  Linear(Vector4 vStartPoint, Vector4 vTargetPoint, float fPassedTime, float fTotalTime)
	{
		return (Vector4)((vTargetPoint - vStartPoint) * fPassedTime / fTotalTime + vStartPoint);
	}

	static public float Linear(float fStartPoint, float fTargetPoint, float fPassedTime, float fTotalTime)
	{
		return (float)((fTargetPoint - fStartPoint) * fPassedTime / fTotalTime + fStartPoint);
	}

	static public Vector4 QuadIn(Vector4 vStartPoint, Vector4 vTargetPoint, float fPassedTime, float fTotalTime)
	{
		fPassedTime /= fTotalTime;
		return (Vector4)((vTargetPoint - vStartPoint) * fPassedTime * fPassedTime + vStartPoint);
	}

	static public float QuadIn(float fStartPoint, float fTargetPoint, float fPassedTime, float fTotalTime)
	{
		fPassedTime /= fTotalTime;
		return (float)((fTargetPoint - fStartPoint) * fPassedTime * fPassedTime + fStartPoint);
	}


	static public Vector4 QuadOut(Vector4 vStartPoint, Vector4 vTargetPoint, float fPassedTime, float fTotalTime)
	{
		fPassedTime /= fTotalTime;
		return (Vector4)(-(vTargetPoint - vStartPoint) * fPassedTime * (fPassedTime - 2f) + vStartPoint);
	}

	static public float QuadOut(float fStartPoint, float fTargetPoint, float fPassedTime, float fTotalTime)
	{
		fPassedTime /= fTotalTime;
		return (float)(-(fTargetPoint - fStartPoint) * fPassedTime * (fPassedTime - 2f) + fStartPoint);
	}

	static public Vector4 QuadInOut(Vector4 vStartPoint, Vector4 vTargetPoint, float fPassedTime, float fTotalTime)
	{
		fPassedTime /= fTotalTime / 2f;
		if (fPassedTime < 1f)
			return (Vector4)((vTargetPoint - vStartPoint) / 2f * fPassedTime * fPassedTime + vStartPoint);

		fPassedTime--;
		return (Vector4)(-(vTargetPoint - vStartPoint) / 2f * (fPassedTime * (fPassedTime - 2f) - 1f) + vStartPoint);
	}

	static public float QuadInOut(float fStartPoint, float fTargetPoint, float fPassedTime, float fTotalTime)
	{
		fPassedTime /= fTotalTime / 2f;
		if (fPassedTime < 1f)
			return (float)((fTargetPoint - fStartPoint) / 2f * fPassedTime * fPassedTime + fStartPoint);

		fPassedTime--;
		return (float)(-(fTargetPoint - fStartPoint) / 2f * (fPassedTime * (fPassedTime - 2f) - 1f) + fStartPoint);
	}

	static public Vector4 CubicIn(Vector4 vStartPoint, Vector4 vTargetPoint, float fPassedTime, float fTotalTime)
	{
		fPassedTime /= fTotalTime;
		return (Vector4)((vTargetPoint - vStartPoint) * fPassedTime * fPassedTime * fPassedTime + vStartPoint);
	}

	static public float CubicIn(float fStartPoint, float fTargetPoint, float fPassedTime, float fTotalTime)
	{
		fPassedTime /= fTotalTime;
		return (float)((fTotalTime - fStartPoint) * fPassedTime * fPassedTime * fPassedTime + fStartPoint);
	}

	static public Vector4 CubicOut(Vector4 vStartPoint, Vector4 vTargetPoint, float fPassedTime, float fTotalTime)
	{
		fPassedTime /= fTotalTime;
		fPassedTime--;
		return (Vector4)((vTargetPoint - vStartPoint) * (fPassedTime * fPassedTime * fPassedTime + 1f) + vStartPoint);
	}

	static public float CubicOut(float fStartPoint, float fTargetPoint, float fPassedTime, float fTotalTime)
	{
		fPassedTime /= fTotalTime;
		fPassedTime--;
		return (float)((fTargetPoint - fStartPoint) * (fPassedTime * fPassedTime * fPassedTime + 1f) + fStartPoint);
	}

	static public Vector4 CubicInOut(Vector4 vStartPoint, Vector4 vTargetPoint, float fPassedTime, float fTotalTime)
	{
		fPassedTime /= fTotalTime / 2f;
		if (fPassedTime < 1f) return (Vector4)((vTargetPoint - vStartPoint) / 2f * fPassedTime * fPassedTime * fPassedTime + vStartPoint);
		fPassedTime -= 2f;
		return (Vector4)((vTargetPoint - vStartPoint) / 2f * (fPassedTime * fPassedTime * fPassedTime + 2f) + vStartPoint);
	}

	static public float CubicInOut(float fStartPoint, float fTargetPoint, float fPassedTime, float fTotalTime)
	{
		fPassedTime /= fTotalTime / 2f;
		if (fPassedTime < 1f) return (float)((fTargetPoint - fStartPoint) / 2f * fPassedTime * fPassedTime * fPassedTime + fStartPoint);
		fPassedTime -= 2f;
		return (float)((fTargetPoint - fStartPoint) / 2f * (fPassedTime * fPassedTime * fPassedTime + 2f) + fStartPoint);
	}

	static public Vector4 QuartIn(Vector4 vStartPoint, Vector4 vTargetPoint, float fPassedTime, float fTotalTime)
	{
		fPassedTime /= fTotalTime;
		return (Vector4)((vTargetPoint - vStartPoint) * fPassedTime * fPassedTime * fPassedTime * fPassedTime + vStartPoint);
	}

	static public float QuartIn(float fStartPoint, float fTargetPoint, float fPassedTime, float fTotalTime)
	{
		fPassedTime /= fTotalTime;
		return (float)((fTargetPoint - fStartPoint) * fPassedTime * fPassedTime * fPassedTime * fPassedTime + fStartPoint);
	}

	static public Vector4 QuartOut(Vector4 vStartPoint, Vector4 vTargetPoint, float fPassedTime, float fTotalTime)
	{
		fPassedTime /= fTotalTime;
		fPassedTime--;
		return (Vector4)(-(vTargetPoint - vStartPoint) * (fPassedTime * fPassedTime * fPassedTime * fPassedTime - 1f) + vStartPoint);
	}

	static public float QuartOut(float fStartPoint, float fTargetPoint, float fPassedTime, float fTotalTime)
	{
		fPassedTime /= fTotalTime;
		fPassedTime--;
		return (float)(-(fTargetPoint - fStartPoint) * (fPassedTime * fPassedTime * fPassedTime * fPassedTime - 1f) + fStartPoint);
	}

	static public Vector4 QuartInOut(Vector4 vStartPoint, Vector4 vTargetPoint, float fPassedTime, float fTotalTime)
	{
		fPassedTime /= fTotalTime / 2f;
		if (fPassedTime < 1f) return (Vector4)((vTargetPoint - vStartPoint) / 2f * fPassedTime * fPassedTime * fPassedTime * fPassedTime + vStartPoint);
		fPassedTime -= 2f;
		return (Vector4)(-(vTargetPoint - vStartPoint) / 2f * (fPassedTime * fPassedTime * fPassedTime * fPassedTime - 2f) + vStartPoint);
	}

	static public float QuartInOut(float fStartPoint, float fTargetPoint, float fPassedTime, float fTotalTime)
	{
		fPassedTime /= fTotalTime / 2f;
		if (fPassedTime < 1f) return (float)((fTargetPoint - fStartPoint) / 2f * fPassedTime * fPassedTime * fPassedTime * fPassedTime + fStartPoint);
		fPassedTime -= 2f;
		return (float)(-(fTargetPoint - fStartPoint) / 2f * (fPassedTime * fPassedTime * fPassedTime * fPassedTime - 2f) + fStartPoint);
	}

	static public Vector4 QuintIn(Vector4 vStartPoint, Vector4 vTargetPoint, float fPassedTime, float fTotalTime)
	{
		fPassedTime /= fTotalTime;
		return (Vector4)((vTargetPoint - vStartPoint) * fPassedTime * fPassedTime * fPassedTime * fPassedTime * fPassedTime + vStartPoint);
	}

	static public float QuintIn(float fStartPoint, float fTargetPoint, float fPassedTime, float fTotalTime)
	{
		fPassedTime /= fTotalTime;
		return (float)((fTargetPoint - fStartPoint) * fPassedTime * fPassedTime * fPassedTime * fPassedTime * fPassedTime + fStartPoint);
	}

	static public Vector4 QuintOut(Vector4 vStartPoint, Vector4 vTargetPoint, float fPassedTime, float fTotalTime)
	{
		fPassedTime /= fTotalTime;
		fPassedTime--;
		return (Vector4)((vTargetPoint - vStartPoint) * (fPassedTime * fPassedTime * fPassedTime * fPassedTime * fPassedTime + 1f) + vStartPoint);
	}

	static public float QuintOut(float fStartPoint, float fTargetPoint, float fPassedTime, float fTotalTime)
	{
		fPassedTime /= fTotalTime;
		fPassedTime--;
		return (float)((fTargetPoint - fStartPoint) * (fPassedTime * fPassedTime * fPassedTime * fPassedTime * fPassedTime + 1f) + fStartPoint);
	}

	static public Vector4 QuintInOut(Vector4 vStartPoint, Vector4 vTargetPoint, float fPassedTime, float fTotalTime)
	{
		fPassedTime /= fTotalTime / 2f;
		if (fPassedTime < 1f) return (Vector4)((vTargetPoint - vStartPoint) / 2f * fPassedTime * fPassedTime * fPassedTime * fPassedTime * fPassedTime + vStartPoint);
		fPassedTime -= 2f;
		return (Vector4)((vTargetPoint - vStartPoint) / 2f * (fPassedTime * fPassedTime * fPassedTime * fPassedTime * fPassedTime + 2f) + vStartPoint);
	}

	static public float QuintInOut(float fStartPoint, float fTargetPoint, float fPassedTime, float fTotalTime)
	{
		fPassedTime /= fTotalTime / 2f;
		if (fPassedTime < 1f) return (float)((fTargetPoint - fStartPoint) / 2f * fPassedTime * fPassedTime * fPassedTime * fPassedTime * fPassedTime + fStartPoint);
		fPassedTime -= 2f;
		return (float)((fTargetPoint - fStartPoint) / 2f * (fPassedTime * fPassedTime * fPassedTime * fPassedTime * fPassedTime + 2f) + fStartPoint);
	}

	static public Vector4 SineIn(Vector4 vStartPoint, Vector4 vTargetPoint, float fPassedTime, float fTotalTime)
	{
		return (Vector4)(-(vTargetPoint - vStartPoint) * Mathf.Cos(fPassedTime / fTotalTime * (PI / 2f)) + (vTargetPoint - vStartPoint) + vStartPoint);
	}

	static public float SineIn(float fStartPoint, float fTargetPoint, float fPassedTime, float fTotalTime)
	{
		return (float)(-(fTargetPoint - fStartPoint) * Mathf.Cos(fPassedTime / fTotalTime * (PI / 2f)) + (fTargetPoint - fStartPoint) + fStartPoint);
	}

	static public Vector4 SineOut(Vector4 vStartPoint, Vector4 vTargetPoint, float fPassedTime, float fTotalTime)
	{
		return (Vector4)((vTargetPoint - vStartPoint) * Mathf.Sin(fPassedTime / fTotalTime * (PI / 2f)) + vStartPoint);
	}

	static public float SineOut(float fStartPoint, float fTargetPoint, float fPassedTime, float fTotalTime)
	{
		return (float)((fTargetPoint - fStartPoint) * Mathf.Sin(fPassedTime / fTotalTime * (PI / 2f)) + fStartPoint);
	}

	static public Vector4 SineInOut(Vector4 vStartPoint, Vector4 vTargetPoint, float fPassedTime, float fTotalTime)
	{
		return (Vector4)(-(vTargetPoint - vStartPoint) / 2f * (Mathf.Cos(PI * fPassedTime / fTotalTime) - 1f) + vStartPoint);
	}

	static public float SineInOut(float fStartPoint, float fTargetPoint, float fPassedTime, float fTotalTime)
	{
		return (float)(-(fTargetPoint - fStartPoint) / 2f * (Mathf.Cos(PI * fPassedTime / fTotalTime) - 1f) + fStartPoint);
	}

	static public Vector4 ExpoIn(Vector4 vStartPoint, Vector4 vTargetPoint, float fPassedTime, float fTotalTime)
	{
		return (Vector4)((vTargetPoint - vStartPoint) * Mathf.Pow(2f, 10.0f * (fPassedTime / fTotalTime - 1f)) + vStartPoint);
	}

	static public float ExpoIn(float fStartPoint, float fTargetPoint, float fPassedTime, float fTotalTime)
	{
		return (float)((fTargetPoint - fStartPoint) * Mathf.Pow(2f, 10.0f * (fPassedTime / fTotalTime - 1f)) + fStartPoint);
	}

	static public Vector4 ExpoOut(Vector4 vStartPoint, Vector4 vTargetPoint, float fPassedTime, float fTotalTime)
	{
		return (Vector4)((vTargetPoint - vStartPoint) * (-Mathf.Pow(2f, -10f * fPassedTime / fTotalTime) + 1f) + vStartPoint);
	}

	static public float ExpoOut(float fStartPoint, float fTargetPoint, float fPassedTime, float fTotalTime)
	{
		return (float)((fTargetPoint - fStartPoint) * (-Mathf.Pow(2f, -10f * fPassedTime / fTotalTime) + 1f) + fStartPoint);
	}

	static public Vector4 ExpoInOut(Vector4 vStartPoint, Vector4 vTargetPoint, float fPassedTime, float fTotalTime)
	{
		fPassedTime /= fTotalTime / 2f;
		if (fPassedTime < 1f) return (Vector4)((vTargetPoint - vStartPoint) / 2f * Mathf.Pow(2f, 10f * (fPassedTime - 1f)) + vStartPoint);
		fPassedTime--;
		return (Vector4)((vTargetPoint - vStartPoint) / 2f * (-Mathf.Pow(2f, -10f * fPassedTime) + 2f) + vStartPoint);
	}

	static public float ExpoInOut(float fStartPoint, float fTargetPoint, float fPassedTime, float fTotalTime)
	{
		fPassedTime /= fTotalTime / 2f;
		if (fPassedTime < 1f) return (float)((fTargetPoint - fStartPoint) / 2f * Mathf.Pow(2f, 10f * (fPassedTime - 1f)) + fStartPoint);
		fPassedTime--;
		return (float)((fTargetPoint - fStartPoint) / 2f * (-Mathf.Pow(2f, -10f * fPassedTime) + 2f) + fStartPoint);
	}

	static public Vector4 CircIn(Vector4 vStartPoint, Vector4 vTargetPoint, float fPassedTime, float fTotalTime)
	{
		fPassedTime /= fTotalTime;
		return (Vector4)(-(vTargetPoint - vStartPoint) * (Mathf.Sqrt(1f - fPassedTime * fPassedTime) - 1f) + vStartPoint);
	}

	static public float CircIn(float fStartPoint, float fTargetPoint, float fPassedTime, float fTotalTime)
	{
		fPassedTime /= fTotalTime;
		return (float)(-(fTargetPoint - fStartPoint) * (Mathf.Sqrt(1f - fPassedTime * fPassedTime) - 1f) + fStartPoint);
	}

	static public Vector4 CircOut(Vector4 vStartPoint, Vector4 vTargetPoint, float fPassedTime, float fTotalTime)
	{
		fPassedTime /= fTotalTime;
		fPassedTime--;
		return (Vector4)((vTargetPoint - vStartPoint) * Mathf.Sqrt(1f - fPassedTime * fPassedTime) + vStartPoint);
	}

	static public float CircOut(float fStartPoint, float fTargetPoint, float fPassedTime, float fTotalTime)
	{
		fPassedTime /= fTotalTime;
		fPassedTime--;
		return (float)((fTargetPoint - fStartPoint) * Mathf.Sqrt(1f - fPassedTime * fPassedTime) + fStartPoint);
	}

	static public Vector4 CircInOut(Vector4 vStartPoint, Vector4 vTargetPoint, float fPassedTime, float fTotalTime)
	{
		fPassedTime /= fTotalTime / 2f;
		if (fPassedTime < 1f) return (Vector4)(-(vTargetPoint - vStartPoint) / 2f * (Mathf.Sqrt(1f - fPassedTime * fPassedTime) - 1f) + vStartPoint);
		fPassedTime -= 2f;
		return (Vector4)((vTargetPoint - vStartPoint) / 2f * (Mathf.Sqrt(1f - fPassedTime * fPassedTime) + 1f) + vStartPoint);
	}

	static public float CircInOut(float fStartPoint, float fTargetPoint, float fPassedTime, float fTotalTime)
	{
		fPassedTime /= fTotalTime / 2f;
		if (fPassedTime < 1f) return (float)(-(fTargetPoint - fStartPoint) / 2f * (Mathf.Sqrt(1f - fPassedTime * fPassedTime) - 1f) + fStartPoint);
		fPassedTime -= 2f;
		return (float)((fTargetPoint - fStartPoint) / 2f * (Mathf.Sqrt(1f - fPassedTime * fPassedTime) + 1f) + fStartPoint);
	}

	static public Vector4 ElasticOut(Vector4 vStartPoint, Vector4 vTargetPoint, float fPassedTime, float fTotalTime)
	{
		if ((fPassedTime /= fTotalTime) == 1f)
			return (Vector4)(vStartPoint + (vTargetPoint - vStartPoint));

		float p=fTotalTime* .3f;
		float s=p / 4f;

		return (Vector4)((vTargetPoint - vStartPoint) * Mathf.Pow(2f, -10f * fPassedTime) * Mathf.Sin((fPassedTime * fTotalTime - s) * (2f * PI) / p) + (vTargetPoint - vStartPoint) + vStartPoint);
	}

	static public float ElasticOut(float fStartPoint, float fTargetPoint, float fPassedTime, float fTotalTime)
	{
		if ((fPassedTime /= fTotalTime) == 1f)
			return (float)(fStartPoint + (fTargetPoint - fStartPoint));

		float p=fTotalTime* .3f;
		float s=p / 4f;

		return (float)((fTargetPoint - fStartPoint) * Mathf.Pow(2f, -10f * fPassedTime) * Mathf.Sin((fPassedTime * fTotalTime - s) * (2f * PI) / p) + (fTargetPoint - fStartPoint) + fStartPoint);
	}

	static public Vector4 ElasticIn(Vector4 vStartPoint, Vector4 vTargetPoint, float fPassedTime, float fTotalTime)
	{
		if ((fPassedTime /= fTotalTime) == 1f)
			return (Vector4)(vStartPoint + (vTargetPoint - vStartPoint));

		float p=fTotalTime* .3f;
		float s=p / 4f;

		return (Vector4)(-((vTargetPoint - vStartPoint) * Mathf.Pow(2f, 10f * (fPassedTime -= 1f)) * Mathf.Sin((fPassedTime * fTotalTime - s) * (2f * PI) / p)) + vStartPoint);
	}

	static public float ElasticIn(float fStartPoint, float fTargetPoint, float fPassedTime, float fTotalTime)
	{
		if ((fPassedTime /= fTotalTime) == 1f)
			return (float)(fStartPoint + (fTargetPoint - fStartPoint));

		float p=fTotalTime* .3f;
		float s=p / 4f;

		return (float)(-((fTargetPoint - fStartPoint) * Mathf.Pow(2f, 10f * (fPassedTime -= 1f)) * Mathf.Sin((fPassedTime * fTotalTime - s) * (2f * PI) / p)) + fStartPoint);
	}

	static public Vector4 ElasticInOut(Vector4 vStartPoint, Vector4 vTargetPoint, float fPassedTime, float fTotalTime)
	{
		if ((fPassedTime /= fTotalTime / 2f) == 2f)
			return (Vector4)(vStartPoint + (vTargetPoint - vStartPoint));

		float p = fTotalTime* (.3f * 1.5f);
		float s = p / 4f;

		if (fPassedTime < 1f)
			return (Vector4)(-.5f * ((vTargetPoint - vStartPoint) * Mathf.Pow(2f, 10f * (fPassedTime -= 1f)) * Mathf.Sin((fPassedTime * fTotalTime - s) * (2f * PI) / p)) + vStartPoint);
		return (Vector4)((vTargetPoint - vStartPoint) * Mathf.Pow(2f, -10f * (fPassedTime -= 1f)) * Mathf.Sin((fPassedTime * fTotalTime - s) * (2f * PI) / p) * .5f + (vTargetPoint - vStartPoint) + vStartPoint);
	}

	static public float ElasticInOut(float fStartPoint, float fTargetPoint, float fPassedTime, float fTotalTime)
	{
		if ((fPassedTime /= fTotalTime / 2f) == 2f)
			return (float)(fStartPoint + (fTargetPoint - fStartPoint));

		float p=fTotalTime* (.3f * 1.5f);
		float s=p / 4f;

		if (fPassedTime < 1f)
			return (float)(-.5f * ((fTargetPoint - fStartPoint) * Mathf.Pow(2f, 10f * (fPassedTime -= 1f)) * Mathf.Sin((fPassedTime * fTotalTime - s) * (2f * PI) / p)) + fStartPoint);
		return (float)((fTargetPoint - fStartPoint) * Mathf.Pow(2f, -10f * (fPassedTime -= 1f)) * Mathf.Sin((fPassedTime * fTotalTime - s) * (2f * PI) / p) * .5f + (fTargetPoint - fStartPoint) + fStartPoint);
	}

	static public Vector4 BounceOut(Vector4 vStartPoint, Vector4 vTargetPoint, float fPassedTime, float fTotalTime)
	{
		if ((fPassedTime /= fTotalTime) < (1 / 2.75f))
			return (Vector4)((vTargetPoint - vStartPoint) * (7.5625f * fPassedTime * fPassedTime) + vStartPoint);
		else if (fPassedTime < (2 / 2.75f))
			return (Vector4)((vTargetPoint - vStartPoint) * (7.5625f * (fPassedTime -= (1.5f / 2.75f)) * fPassedTime + .75f) + vStartPoint);
		else if (fPassedTime < (2.5f / 2.75f))
			return (Vector4)((vTargetPoint - vStartPoint) * (7.5625f * (fPassedTime -= (2.25f / 2.75f)) * fPassedTime + .9375f) + vStartPoint);
		else
			return (Vector4)((vTargetPoint - vStartPoint) * (7.5625f * (fPassedTime -= (2.625f / 2.75f)) * fPassedTime + .984375f) + vStartPoint);
	}

	static public float BounceOut(float fStartPoint, float fTargetPoint, float fPassedTime, float fTotalTime)
	{
		if ((fPassedTime /= fTotalTime) < (1 / 2.75f))
			return (float)((fTargetPoint - fStartPoint) * (7.5625f * fPassedTime * fPassedTime) + fStartPoint);
		else if (fPassedTime < (2 / 2.75f))
			return (float)((fTargetPoint - fStartPoint) * (7.5625f * (fPassedTime -= (1.5f / 2.75f)) * fPassedTime + .75f) + fStartPoint);
		else if (fPassedTime < (2.5f / 2.75f))
			return (float)((fTargetPoint - fStartPoint) * (7.5625f * (fPassedTime -= (2.25f / 2.75f)) * fPassedTime + .9375f) + fStartPoint);
		else
			return (float)((fTargetPoint - fStartPoint) * (7.5625f * (fPassedTime -= (2.625f / 2.75f)) * fPassedTime + .984375f) + fStartPoint);
	}

	static public Vector4 OutBack(Vector4 vStartPoint, Vector4 vTargetPoint, float fPassedTime, float fTotalTime)
	{
		const float c1 = 1.70158f;
		const float c3 = c1 + 1f;

		return (vTargetPoint - vStartPoint) * (1f + c3 * Mathf.Pow(fPassedTime / fTotalTime - 1f, 3f) + c1 * Mathf.Pow(fPassedTime / fTotalTime - 1f, 2f)) + vStartPoint;
	}

	static public float OutBack(float vStartPoint, float vTargetPoint, float fPassedTime, float fTotalTime)
	{
		const float c1 = 1.70158f;
		const float c3 = c1 + 1f;

		return (vTargetPoint - vStartPoint) * (1f + c3 * Mathf.Pow(fPassedTime / fTotalTime - 1f, 3f) + c1 * Mathf.Pow(fPassedTime / fTotalTime - 1f, 2f)) + vStartPoint;
	}

	static public Vector4 InOutBack(Vector4 vStartPoint, Vector4 vTargetPoint, float fPassedTime, float fTotalTime)
	{
		const float c1 = 1.70158f;
		const float c2 = c1 * 1.525f;

		float x = fPassedTime / fTotalTime;

		return x < 0.5f
			? (vTargetPoint - vStartPoint) * ((Mathf.Pow(2f * x, 2f) * ((c2 + 1f) * 2f * x - c2)) / 2f) + vStartPoint
			: (vTargetPoint - vStartPoint) * ((Mathf.Pow(2f * x - 2f, 2f) * ((c2 + 1f) * (x * 2f - 2f) + c2) + 2f) / 2f) + vStartPoint;

	}
	static public float InOutBack(float vStartPoint, float vTargetPoint, float fPassedTime, float fTotalTime)
	{
		const float c1 = 1.70158f;
		const float c2 = c1 * 1.525f;

		float x = fPassedTime / fTotalTime;

		return x < 0.5f
			? (vTargetPoint - vStartPoint) * ((Mathf.Pow(2f * x, 2f) * ((c2 + 1f) * 2f * x - c2)) / 2f) + vStartPoint
			: (vTargetPoint - vStartPoint) * ((Mathf.Pow(2f * x - 2f, 2f) * ((c2 + 1f) * (x * 2f - 2f) + c2) + 2f) / 2f) + vStartPoint;

	}

	static public Vector4 BounceIn(Vector4 vStartPoint, Vector4 vTargetPoint, float fPassedTime, float fTotalTime)
	{
		return (Vector4)(BounceOut(vTargetPoint, vStartPoint, fTotalTime - fPassedTime, fTotalTime));
	}
	static public float BounceIn(float fStartPoint, float fTargetPoint, float fPassedTime, float fTotalTime)
	{
		return (float)(BounceOut(fTargetPoint, fStartPoint, fTotalTime - fPassedTime, fTotalTime));
	}
	//float  BounceEaseInOut(Vector4 vStartPoint, Vector4 vTargetPoint, float fPassedTime, float fTotalTime)
	//{
	//	if (dPassedTime < dTotalTime * 0.5f)
	//		return BounceEaseIn(vStartPoint, vTargetPoint * 0.5f, dPassedTime , dTotalTime * 0.5f);
	//	else
	//		return BounceEaseOut(vTargetPoint * 0.5f, vTargetPoint , dPassedTime-(dTotalTime * 0.5f) , dTotalTime * 0.5f);
	//}
	//
	//float  BounceEaseOutIn(Vector4 vStartPoint, Vector4 vTargetPoint, float fPassedTime, float fTotalTime)
	//{
	//
	//	if (dPassedTime < dTotalTime * 0.5f )
	//		return BounceEaseOut(vStartPoint, vTargetPoint * 0.5f, dPassedTime * 0.5f, dTotalTime * 0.5f);
	//	return BounceEaseIn(vTargetPoint * 0.5f, vTargetPoint, dPassedTime * 0.5f, dTotalTime * 0.5f);
	//}
}
