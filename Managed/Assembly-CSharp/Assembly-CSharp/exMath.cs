using System;
using UnityEngine;

// Token: 0x020000F3 RID: 243
public static class exMath
{
	// Token: 0x06000952 RID: 2386 RVA: 0x0003B094 File Offset: 0x00039294
	public static float Wrap(float _value, float _length, WrapMode _wrapMode)
	{
		float num = Mathf.Abs(_value);
		if (_wrapMode == WrapMode.Loop)
		{
			num %= _length;
		}
		else if (_wrapMode == WrapMode.PingPong)
		{
			int num2 = (int)(num / _length);
			num %= _length;
			if (num2 % 2 == 1)
			{
				num = _length - num;
			}
		}
		else
		{
			num = Mathf.Clamp(num, 0f, _length);
		}
		return num;
	}

	// Token: 0x06000953 RID: 2387 RVA: 0x0003B0D7 File Offset: 0x000392D7
	public static int Wrap(int _value, int _maxValue, WrapMode _wrapMode)
	{
		if (_maxValue == 0)
		{
			return 0;
		}
		if (_value < 0)
		{
			_value = -_value;
		}
		if (_wrapMode == WrapMode.Loop)
		{
			return _value % (_maxValue + 1);
		}
		if (_wrapMode == WrapMode.PingPong)
		{
			int num = _value / _maxValue;
			_value %= _maxValue;
			if ((num & 1) == 1)
			{
				return _maxValue - _value;
			}
		}
		else
		{
			if (_value < 0)
			{
				return 0;
			}
			if (_value > _maxValue)
			{
				return _maxValue;
			}
		}
		return _value;
	}

	// Token: 0x06000954 RID: 2388 RVA: 0x0003B114 File Offset: 0x00039314
	public static float SpringLerp(float _strength, float _deltaTime)
	{
		if (_deltaTime > 1f)
		{
			_deltaTime = 1f;
		}
		int num = Mathf.RoundToInt(_deltaTime * 1000f);
		_deltaTime = 0.001f * _strength;
		float num2 = 0f;
		for (int i = 0; i < num; i++)
		{
			num2 = Mathf.Lerp(num2, 1f, _deltaTime);
		}
		return num2;
	}

	// Token: 0x06000955 RID: 2389 RVA: 0x0003B168 File Offset: 0x00039368
	public static float SpringLerp(float _from, float _to, float _strength, float _deltaTime)
	{
		if (_deltaTime > 1f)
		{
			_deltaTime = 1f;
		}
		int num = Mathf.RoundToInt(_deltaTime * 1000f);
		_deltaTime = 0.001f * _strength;
		float num2 = _from;
		for (int i = 0; i < num; i++)
		{
			num2 = Mathf.Lerp(num2, _to, _deltaTime);
		}
		return num2;
	}

	// Token: 0x06000956 RID: 2390 RVA: 0x0003B1B2 File Offset: 0x000393B2
	public static Vector2 SpringLerp(Vector2 _from, Vector2 _to, float _strength, float _deltaTime)
	{
		return Vector2.Lerp(_from, _to, exMath.SpringLerp(_strength, _deltaTime));
	}

	// Token: 0x06000957 RID: 2391 RVA: 0x0003B1C2 File Offset: 0x000393C2
	public static Vector3 SpringLerp(Vector3 _from, Vector3 _to, float _strength, float _deltaTime)
	{
		return Vector3.Lerp(_from, _to, exMath.SpringLerp(_strength, _deltaTime));
	}

	// Token: 0x06000958 RID: 2392 RVA: 0x0003B1D2 File Offset: 0x000393D2
	public static float Lerp(float _from, float _to, float _v)
	{
		return _from * (1f - _v) + _to * _v;
	}

	// Token: 0x06000959 RID: 2393 RVA: 0x0003B1E1 File Offset: 0x000393E1
	public static Vector2 Lerp(Vector2 _from, Vector2 _to, float _v)
	{
		return _from * (1f - _v) + _to * _v;
	}

	// Token: 0x0600095A RID: 2394 RVA: 0x0003B1FC File Offset: 0x000393FC
	public static Vector3 Lerp(Vector3 _from, Vector3 _to, float _v)
	{
		return _from * (1f - _v) + _to * _v;
	}

	// Token: 0x0600095B RID: 2395 RVA: 0x0003B218 File Offset: 0x00039418
	public static Color Lerp(Color _from, Color _to, float _v)
	{
		return new Color(_from.r + (_to.r - _from.r) * _v, _from.g + (_to.g - _from.g) * _v, _from.b + (_to.b - _from.b) * _v, _from.a + (_to.a - _from.a) * _v);
	}

	// Token: 0x0600095C RID: 2396 RVA: 0x0003B284 File Offset: 0x00039484
	public static Rect Lerp(Rect _from, Rect _to, float _v)
	{
		return new Rect
		{
			x = _from.x * (1f - _v) + _to.x * _v,
			y = _from.y * (1f - _v) + _to.y * _v,
			width = _from.width * (1f - _v) + _to.width * _v,
			height = _from.height * (1f - _v) + _to.height * _v
		};
	}
}
