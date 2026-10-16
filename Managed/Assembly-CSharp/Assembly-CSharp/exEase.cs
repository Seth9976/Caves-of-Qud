using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000F0 RID: 240
public class exEase
{
	// Token: 0x060008FD RID: 2301 RVA: 0x00039A9C File Offset: 0x00037C9C
	public static void Init()
	{
		if (!exEase.initialized)
		{
			exEase.initialized = true;
			exEase.easeFunctions = new Dictionary<exEase.Type, Func<float, float>>();
			exEase.easeFunctions[exEase.Type.Linear] = new Func<float, float>(exEase.Linear);
			exEase.easeFunctions[exEase.Type.QuadIn] = new Func<float, float>(exEase.QuadIn);
			exEase.easeFunctions[exEase.Type.QuadOut] = new Func<float, float>(exEase.QuadOut);
			exEase.easeFunctions[exEase.Type.QuadInOut] = new Func<float, float>(exEase.QuadInOut);
			exEase.easeFunctions[exEase.Type.QuadOutIn] = new Func<float, float>(exEase.QuadOutIn);
			exEase.easeFunctions[exEase.Type.CubicIn] = new Func<float, float>(exEase.CubicIn);
			exEase.easeFunctions[exEase.Type.CubicOut] = new Func<float, float>(exEase.CubicOut);
			exEase.easeFunctions[exEase.Type.CubicInOut] = new Func<float, float>(exEase.CubicInOut);
			exEase.easeFunctions[exEase.Type.CubicOutIn] = new Func<float, float>(exEase.CubicOutIn);
			exEase.easeFunctions[exEase.Type.QuartIn] = new Func<float, float>(exEase.QuartIn);
			exEase.easeFunctions[exEase.Type.QuartOut] = new Func<float, float>(exEase.QuartOut);
			exEase.easeFunctions[exEase.Type.QuartInOut] = new Func<float, float>(exEase.QuartInOut);
			exEase.easeFunctions[exEase.Type.QuartOutIn] = new Func<float, float>(exEase.QuartOutIn);
			exEase.easeFunctions[exEase.Type.QuintIn] = new Func<float, float>(exEase.QuintIn);
			exEase.easeFunctions[exEase.Type.QuintOut] = new Func<float, float>(exEase.QuintOut);
			exEase.easeFunctions[exEase.Type.QuintInOut] = new Func<float, float>(exEase.QuintInOut);
			exEase.easeFunctions[exEase.Type.QuintOutIn] = new Func<float, float>(exEase.QuintOutIn);
			exEase.easeFunctions[exEase.Type.SineIn] = new Func<float, float>(exEase.SineIn);
			exEase.easeFunctions[exEase.Type.SineOut] = new Func<float, float>(exEase.SineOut);
			exEase.easeFunctions[exEase.Type.SineInOut] = new Func<float, float>(exEase.SineInOut);
			exEase.easeFunctions[exEase.Type.SineOutIn] = new Func<float, float>(exEase.SineOutIn);
			exEase.easeFunctions[exEase.Type.ExpoIn] = new Func<float, float>(exEase.ExpoIn);
			exEase.easeFunctions[exEase.Type.ExpoOut] = new Func<float, float>(exEase.ExpoOut);
			exEase.easeFunctions[exEase.Type.ExpoInOut] = new Func<float, float>(exEase.ExpoInOut);
			exEase.easeFunctions[exEase.Type.ExpoOutIn] = new Func<float, float>(exEase.ExpoOutIn);
			exEase.easeFunctions[exEase.Type.CircIn] = new Func<float, float>(exEase.CircIn);
			exEase.easeFunctions[exEase.Type.CircOut] = new Func<float, float>(exEase.CircOut);
			exEase.easeFunctions[exEase.Type.CircInOut] = new Func<float, float>(exEase.CircInOut);
			exEase.easeFunctions[exEase.Type.CircOutIn] = new Func<float, float>(exEase.CircOutIn);
			exEase.easeFunctions[exEase.Type.ElasticIn] = new Func<float, float>(exEase.ElasticIn_Simple);
			exEase.easeFunctions[exEase.Type.ElasticOut] = new Func<float, float>(exEase.ElasticOut_Simple);
			exEase.easeFunctions[exEase.Type.ElasticInOut] = new Func<float, float>(exEase.ElasticInOut_Simple);
			exEase.easeFunctions[exEase.Type.ElasticOutIn] = new Func<float, float>(exEase.ElasticOutIn_Simple);
			exEase.easeFunctions[exEase.Type.BackIn] = new Func<float, float>(exEase.BackIn_Simple);
			exEase.easeFunctions[exEase.Type.BackOut] = new Func<float, float>(exEase.BackOut_Simple);
			exEase.easeFunctions[exEase.Type.BackInOut] = new Func<float, float>(exEase.BackInOut_Simple);
			exEase.easeFunctions[exEase.Type.BackOutIn] = new Func<float, float>(exEase.BackOutIn_Simple);
			exEase.easeFunctions[exEase.Type.BounceIn] = new Func<float, float>(exEase.BounceIn_Simple);
			exEase.easeFunctions[exEase.Type.BounceOut] = new Func<float, float>(exEase.BounceOut_Simple);
			exEase.easeFunctions[exEase.Type.BounceInOut] = new Func<float, float>(exEase.BounceInOut_Simple);
			exEase.easeFunctions[exEase.Type.BounceOutIn] = new Func<float, float>(exEase.BounceOutIn_Simple);
			exEase.easeFunctions[exEase.Type.Smooth] = new Func<float, float>(exEase.Smooth);
			exEase.easeFunctions[exEase.Type.Fade] = new Func<float, float>(exEase.Fade);
			exEase.easeFunctions[exEase.Type.Spring] = new Func<float, float>(exEase.Spring);
		}
	}

	// Token: 0x060008FE RID: 2302 RVA: 0x00039EDA File Offset: 0x000380DA
	public static Func<float, float> GetEaseFunc(exEase.Type _type)
	{
		exEase.Init();
		return exEase.easeFunctions[_type];
	}

	// Token: 0x060008FF RID: 2303 RVA: 0x00039EEC File Offset: 0x000380EC
	public static float Linear(float _t)
	{
		return _t;
	}

	// Token: 0x06000900 RID: 2304 RVA: 0x00039EEF File Offset: 0x000380EF
	public static float QuadIn(float _t)
	{
		return _t * _t;
	}

	// Token: 0x06000901 RID: 2305 RVA: 0x00039EF4 File Offset: 0x000380F4
	public static float QuadOut(float _t)
	{
		return -_t * (_t - 2f);
	}

	// Token: 0x06000902 RID: 2306 RVA: 0x00039F00 File Offset: 0x00038100
	public static float QuadInOut(float _t)
	{
		_t *= 2f;
		if (_t < 1f)
		{
			return _t * _t / 2f;
		}
		_t -= 1f;
		return -0.5f * (_t * (_t - 2f) - 1f);
	}

	// Token: 0x06000903 RID: 2307 RVA: 0x00039F3B File Offset: 0x0003813B
	public static float QuadOutIn(float _t)
	{
		if (_t < 0.5f)
		{
			return exEase.QuadOut(_t * 2f) / 2f;
		}
		return exEase.QuadIn(2f * _t - 1f) / 2f + 0.5f;
	}

	// Token: 0x06000904 RID: 2308 RVA: 0x00039F76 File Offset: 0x00038176
	public static float CubicIn(float _t)
	{
		return _t * _t * _t;
	}

	// Token: 0x06000905 RID: 2309 RVA: 0x00039F7D File Offset: 0x0003817D
	public static float CubicOut(float _t)
	{
		_t -= 1f;
		return _t * _t * _t + 1f;
	}

	// Token: 0x06000906 RID: 2310 RVA: 0x00039F93 File Offset: 0x00038193
	public static float CubicInOut(float _t)
	{
		_t *= 2f;
		if (_t < 1f)
		{
			return 0.5f * _t * _t * _t;
		}
		_t -= 2f;
		return 0.5f * (_t * _t * _t + 2f);
	}

	// Token: 0x06000907 RID: 2311 RVA: 0x00039FCC File Offset: 0x000381CC
	public static float CubicOutIn(float _t)
	{
		if (_t < 0.5f)
		{
			return exEase.CubicOut(2f * _t) / 2f;
		}
		return exEase.CubicIn(2f * _t - 1f) / 2f + 0.5f;
	}

	// Token: 0x06000908 RID: 2312 RVA: 0x0003A007 File Offset: 0x00038207
	public static float QuartIn(float _t)
	{
		return _t * _t * _t * _t;
	}

	// Token: 0x06000909 RID: 2313 RVA: 0x0003A010 File Offset: 0x00038210
	public static float QuartOut(float _t)
	{
		_t -= 1f;
		return -(_t * _t * _t * _t - 1f);
	}

	// Token: 0x0600090A RID: 2314 RVA: 0x0003A029 File Offset: 0x00038229
	public static float QuartInOut(float _t)
	{
		_t *= 2f;
		if (_t < 1f)
		{
			return 0.5f * _t * _t * _t * _t;
		}
		_t -= 2f;
		return -0.5f * (_t * _t * _t * _t - 2f);
	}

	// Token: 0x0600090B RID: 2315 RVA: 0x0003A066 File Offset: 0x00038266
	public static float QuartOutIn(float _t)
	{
		if (_t < 0.5f)
		{
			return exEase.QuartOut(2f * _t) / 2f;
		}
		return exEase.QuartIn(2f * _t - 1f) / 2f + 0.5f;
	}

	// Token: 0x0600090C RID: 2316 RVA: 0x0003A0A1 File Offset: 0x000382A1
	public static float QuintIn(float _t)
	{
		return _t * _t * _t * _t * _t;
	}

	// Token: 0x0600090D RID: 2317 RVA: 0x0003A0AC File Offset: 0x000382AC
	public static float QuintOut(float _t)
	{
		_t -= 1f;
		return _t * _t * _t * _t * _t + 1f;
	}

	// Token: 0x0600090E RID: 2318 RVA: 0x0003A0C8 File Offset: 0x000382C8
	public static float QuintInOut(float _t)
	{
		_t *= 2f;
		if (_t < 1f)
		{
			return 0.5f * _t * _t * _t * _t * _t;
		}
		_t -= 2f;
		return 0.5f * (_t * _t * _t * _t * _t + 2f);
	}

	// Token: 0x0600090F RID: 2319 RVA: 0x0003A114 File Offset: 0x00038314
	public static float QuintOutIn(float _t)
	{
		if (_t < 0.5f)
		{
			return exEase.QuintOut(2f * _t) / 2f;
		}
		return exEase.QuintIn(2f * _t - 1f) / 2f + 0.5f;
	}

	// Token: 0x06000910 RID: 2320 RVA: 0x0003A14F File Offset: 0x0003834F
	public static float SineIn(float _t)
	{
		if (_t != 1f)
		{
			return -Mathf.Cos(_t * 3.1415927f / 2f) + 1f;
		}
		return 1f;
	}

	// Token: 0x06000911 RID: 2321 RVA: 0x0003A178 File Offset: 0x00038378
	public static float SineOut(float _t)
	{
		return Mathf.Sin(_t * 3.1415927f / 2f);
	}

	// Token: 0x06000912 RID: 2322 RVA: 0x0003A18C File Offset: 0x0003838C
	public static float SineInOut(float _t)
	{
		return -0.5f * (Mathf.Cos(3.1415927f * _t) - 1f);
	}

	// Token: 0x06000913 RID: 2323 RVA: 0x0003A1A6 File Offset: 0x000383A6
	public static float SineOutIn(float _t)
	{
		if (_t < 0.5f)
		{
			return exEase.SineOut(2f * _t) / 2f;
		}
		return exEase.SineIn(2f * _t - 1f) / 2f + 0.5f;
	}

	// Token: 0x06000914 RID: 2324 RVA: 0x0003A1E1 File Offset: 0x000383E1
	public static float ExpoIn(float _t)
	{
		if (_t != 0f && _t != 1f)
		{
			return Mathf.Pow(2f, 10f * (_t - 1f)) - 0.001f;
		}
		return _t;
	}

	// Token: 0x06000915 RID: 2325 RVA: 0x0003A212 File Offset: 0x00038412
	public static float ExpoOut(float _t)
	{
		if (_t != 1f)
		{
			return 1.001f * (-Mathf.Pow(2f, -10f * _t) + 1f);
		}
		return 1f;
	}

	// Token: 0x06000916 RID: 2326 RVA: 0x0003A240 File Offset: 0x00038440
	public static float ExpoInOut(float _t)
	{
		if (_t == 0f)
		{
			return 0f;
		}
		if (_t == 1f)
		{
			return 1f;
		}
		_t *= 2f;
		if (_t < 1f)
		{
			return 0.5f * Mathf.Pow(2f, 10f * (_t - 1f)) - 0.005f;
		}
		return 0.5025f * (-Mathf.Pow(2f, -10f * (_t - 1f)) + 2f);
	}

	// Token: 0x06000917 RID: 2327 RVA: 0x0003A2C2 File Offset: 0x000384C2
	public static float ExpoOutIn(float _t)
	{
		if (_t < 0.5f)
		{
			return exEase.ExpoOut(2f * _t) / 2f;
		}
		return exEase.ExpoIn(2f * _t - 1f) / 2f + 0.5f;
	}

	// Token: 0x06000918 RID: 2328 RVA: 0x0003A2FD File Offset: 0x000384FD
	public static float CircIn(float _t)
	{
		return -(Mathf.Sqrt(1f - _t * _t) - 1f);
	}

	// Token: 0x06000919 RID: 2329 RVA: 0x0003A314 File Offset: 0x00038514
	public static float CircOut(float _t)
	{
		_t -= 1f;
		return Mathf.Sqrt(1f - _t * _t);
	}

	// Token: 0x0600091A RID: 2330 RVA: 0x0003A330 File Offset: 0x00038530
	public static float CircInOut(float _t)
	{
		_t *= 2f;
		if (_t < 1f)
		{
			return -0.5f * (Mathf.Sqrt(1f - _t * _t) - 1f);
		}
		_t -= 2f;
		return 0.5f * (Mathf.Sqrt(1f - _t * _t) + 1f);
	}

	// Token: 0x0600091B RID: 2331 RVA: 0x0003A38C File Offset: 0x0003858C
	public static float CircOutIn(float _t)
	{
		if (_t < 0.5f)
		{
			return exEase.CircOut(2f * _t) / 2f;
		}
		return exEase.CircIn(2f * _t - 1f) / 2f + 0.5f;
	}

	// Token: 0x0600091C RID: 2332 RVA: 0x0003A3C8 File Offset: 0x000385C8
	private static float ElasticInHelper(float _t, float _b, float _c, float _d, float _a, float _p)
	{
		if (_t == 0f)
		{
			return _b;
		}
		float num = _t / _d;
		if (num == 1f)
		{
			return _b + _c;
		}
		float num2;
		if (_a < Mathf.Abs(_c))
		{
			_a = _c;
			num2 = _p / 4f;
		}
		else
		{
			num2 = _p / 2f * 3.1415927f * Mathf.Asin(_c / _a);
		}
		num -= 1f;
		return -(_a * Mathf.Pow(2f, 10f * num) * Mathf.Sin((num * _d - num2) * 2f * 3.1415927f / _p)) + _b;
	}

	// Token: 0x0600091D RID: 2333 RVA: 0x0003A45C File Offset: 0x0003865C
	private static float ElasticOutHelper(float _t, float _b, float _c, float _d, float _a, float _p)
	{
		if (_t == 0f)
		{
			return 0f;
		}
		if (_t == 1f)
		{
			return _c;
		}
		float num;
		if (_a < _c)
		{
			_a = _c;
			num = _p / 4f;
		}
		else
		{
			num = _p / 2f * 3.1415927f * Mathf.Asin(_c / _a);
		}
		return _a * Mathf.Pow(2f, -10f * _t) * Mathf.Sin((_t - num) * 2f * 3.1415927f / _p) + _c;
	}

	// Token: 0x0600091E RID: 2334 RVA: 0x0003A4DB File Offset: 0x000386DB
	public static float ElasticIn(float _t, float _a, float _p)
	{
		return exEase.ElasticInHelper(_t, 0f, 1f, 1f, _a, _p);
	}

	// Token: 0x0600091F RID: 2335 RVA: 0x0003A4F4 File Offset: 0x000386F4
	public static float ElasticOut(float _t, float _a, float _p)
	{
		return exEase.ElasticOutHelper(_t, 0f, 1f, 1f, _a, _p);
	}

	// Token: 0x06000920 RID: 2336 RVA: 0x0003A510 File Offset: 0x00038710
	public static float ElasticInOut(float _t, float _a, float _p)
	{
		if (_t == 0f)
		{
			return 0f;
		}
		_t *= 2f;
		if (_t == 2f)
		{
			return 1f;
		}
		float num;
		if (_a < 1f)
		{
			_a = 1f;
			num = _p / 4f;
		}
		else
		{
			num = _p / 2f * 3.1415927f * Mathf.Asin(1f / _a);
		}
		if (_t < 1f)
		{
			return -0.5f * (_a * Mathf.Pow(2f, 10f * (_t - 1f)) * Mathf.Sin((_t - 1f - num) * 2f * 3.1415927f / _p));
		}
		return _a * Mathf.Pow(2f, -10f * (_t - 1f)) * Mathf.Sin((_t - 1f - num) * 2f * 3.1415927f / _p) * 0.5f + 1f;
	}

	// Token: 0x06000921 RID: 2337 RVA: 0x0003A600 File Offset: 0x00038800
	public static float ElasticOutIn(float _t, float _a, float _p)
	{
		if (_t < 0.5f)
		{
			return exEase.ElasticOutHelper(_t * 2f, 0f, 0.5f, 1f, _a, _p);
		}
		return exEase.ElasticInHelper(2f * _t - 1f, 0.5f, 0.5f, 1f, _a, _p);
	}

	// Token: 0x06000922 RID: 2338 RVA: 0x0003A656 File Offset: 0x00038856
	public static float ElasticIn_Simple(float _t)
	{
		return exEase.ElasticIn(_t, 0.1f, 0.05f);
	}

	// Token: 0x06000923 RID: 2339 RVA: 0x0003A668 File Offset: 0x00038868
	public static float ElasticOut_Simple(float _t)
	{
		return exEase.ElasticOut(_t, 0.1f, 0.05f);
	}

	// Token: 0x06000924 RID: 2340 RVA: 0x0003A67A File Offset: 0x0003887A
	public static float ElasticInOut_Simple(float _t)
	{
		return exEase.ElasticInOut(_t, 0.1f, 0.05f);
	}

	// Token: 0x06000925 RID: 2341 RVA: 0x0003A68C File Offset: 0x0003888C
	public static float ElasticOutIn_Simple(float _t)
	{
		return exEase.ElasticOutIn(_t, 0.1f, 0.05f);
	}

	// Token: 0x06000926 RID: 2342 RVA: 0x0003A69E File Offset: 0x0003889E
	public static float BackIn(float _t, float _s)
	{
		return _t * _t * ((_s + 1f) * _t - _s);
	}

	// Token: 0x06000927 RID: 2343 RVA: 0x0003A6AF File Offset: 0x000388AF
	public static float BackOut(float _t, float _s)
	{
		_t -= 1f;
		return _t * _t * ((_s + 1f) * _t + _s) + 1f;
	}

	// Token: 0x06000928 RID: 2344 RVA: 0x0003A6D0 File Offset: 0x000388D0
	public static float BackInOut(float _t, float _s)
	{
		_t *= 2f;
		if (_t < 1f)
		{
			_s *= 1.55f;
			return 0.5f * (_t * _t * ((_s + 1f) * _t - _s));
		}
		_t -= 2f;
		_s *= 1.55f;
		return 0.5f * (_t * _t * ((_s + 1f) * _t + _s) + 2f);
	}

	// Token: 0x06000929 RID: 2345 RVA: 0x0003A73A File Offset: 0x0003893A
	public static float BackOutIn(float _t, float _s)
	{
		if (_t < 0.5f)
		{
			return exEase.BackOut(2f * _t, _s) / 2f;
		}
		return exEase.BackIn(2f * _t - 1f, _s) / 2f + 0.5f;
	}

	// Token: 0x0600092A RID: 2346 RVA: 0x0003A777 File Offset: 0x00038977
	public static float BackIn_Simple(float _t)
	{
		return exEase.BackIn(_t, 2f);
	}

	// Token: 0x0600092B RID: 2347 RVA: 0x0003A784 File Offset: 0x00038984
	public static float BackOut_Simple(float _t)
	{
		return exEase.BackOut(_t, 2f);
	}

	// Token: 0x0600092C RID: 2348 RVA: 0x0003A791 File Offset: 0x00038991
	public static float BackInOut_Simple(float _t)
	{
		return exEase.BackInOut(_t, 2f);
	}

	// Token: 0x0600092D RID: 2349 RVA: 0x0003A79E File Offset: 0x0003899E
	public static float BackOutIn_Simple(float _t)
	{
		return exEase.BackOutIn(_t, 2f);
	}

	// Token: 0x0600092E RID: 2350 RVA: 0x0003A7AC File Offset: 0x000389AC
	private static float BounceOutHelper(float _t, float _c, float _a)
	{
		if (_t == 1f)
		{
			return _c;
		}
		if (_t < 0.36363637f)
		{
			return _c * (7.565f * _t * _t);
		}
		if (_t < 0.72727275f)
		{
			_t -= 0.54545456f;
			return -_a * (1f - (7.565f * _t * _t + 0.5f)) + _c;
		}
		if (_t < 0.90909094f)
		{
			_t -= 0.8181818f;
			return -_a * (1f - (7.565f * _t * _t + 0.935f)) + _c;
		}
		_t -= 0.95454544f;
		return -_a * (1f - (7.565f * _t * _t + 0.98435f)) + _c;
	}

	// Token: 0x0600092F RID: 2351 RVA: 0x0003A852 File Offset: 0x00038A52
	public static float BounceIn(float _t, float _a)
	{
		return 1f - exEase.BounceOutHelper(1f - _t, 1f, _a);
	}

	// Token: 0x06000930 RID: 2352 RVA: 0x0003A86C File Offset: 0x00038A6C
	public static float BounceOut(float _t, float _a)
	{
		return exEase.BounceOutHelper(_t, 1f, _a);
	}

	// Token: 0x06000931 RID: 2353 RVA: 0x0003A87C File Offset: 0x00038A7C
	public static float BounceInOut(float _t, float _a)
	{
		if (_t < 0.5f)
		{
			return exEase.BounceIn(2f * _t, _a) / 2f;
		}
		if (_t != 1f)
		{
			return exEase.BounceOut(2f * _t - 1f, _a) / 2f + 0.5f;
		}
		return 1f;
	}

	// Token: 0x06000932 RID: 2354 RVA: 0x0003A8D2 File Offset: 0x00038AD2
	public static float BounceOutIn(float _t, float _a)
	{
		if (_t < 0.5f)
		{
			return exEase.BounceOutHelper(_t * 2f, 0.5f, _a);
		}
		return 1f - exEase.BounceOutHelper(2f - 2f * _t, 0.5f, _a);
	}

	// Token: 0x06000933 RID: 2355 RVA: 0x0003A90D File Offset: 0x00038B0D
	public static float BounceIn_Simple(float _t)
	{
		return exEase.BounceIn(_t, 2f);
	}

	// Token: 0x06000934 RID: 2356 RVA: 0x0003A91A File Offset: 0x00038B1A
	public static float BounceOut_Simple(float _t)
	{
		return exEase.BounceOut(_t, 2f);
	}

	// Token: 0x06000935 RID: 2357 RVA: 0x0003A927 File Offset: 0x00038B27
	public static float BounceInOut_Simple(float _t)
	{
		return exEase.BounceInOut(_t, 2f);
	}

	// Token: 0x06000936 RID: 2358 RVA: 0x0003A934 File Offset: 0x00038B34
	public static float BounceOutIn_Simple(float _t)
	{
		return exEase.BounceOutIn(_t, 2f);
	}

	// Token: 0x06000937 RID: 2359 RVA: 0x0003A941 File Offset: 0x00038B41
	public static float Smooth(float _t)
	{
		if (_t <= 0f)
		{
			return 0f;
		}
		if (_t >= 1f)
		{
			return 1f;
		}
		return _t * _t * (3f - 2f * _t);
	}

	// Token: 0x06000938 RID: 2360 RVA: 0x0003A970 File Offset: 0x00038B70
	public static float Fade(float _t)
	{
		if (_t <= 0f)
		{
			return 0f;
		}
		if (_t >= 1f)
		{
			return 1f;
		}
		return _t * _t * _t * (_t * (_t * 6f - 15f) + 10f);
	}

	// Token: 0x06000939 RID: 2361 RVA: 0x0003A9AC File Offset: 0x00038BAC
	public static float Spring(float _t)
	{
		_t = Mathf.Clamp01(_t);
		_t = (Mathf.Sin(_t * 3.1415927f * (0.2f + 2.5f * _t * _t * _t)) * Mathf.Pow(1f - _t, 2.2f) + _t) * (1f + 1.2f * (1f - _t));
		return _t;
	}

	// Token: 0x0600093A RID: 2362 RVA: 0x0003AA0C File Offset: 0x00038C0C
	public static float Punch(float _amplitude, float _t)
	{
		if (_t == 0f)
		{
			return 0f;
		}
		if (_t == 1f)
		{
			return 0f;
		}
		float num = 0.3f;
		float num2 = num / 6.2831855f * Mathf.Asin(0f);
		return _amplitude * Mathf.Pow(2f, -10f * _t) * Mathf.Sin((_t * 1f - num2) * 6.2831855f / num);
	}

	// Token: 0x0600093B RID: 2363 RVA: 0x0003AA80 File Offset: 0x00038C80
	public static float PingPong(float _t, Func<float, float> _ease)
	{
		float num = Mathf.PingPong(_t, 0.5f);
		return _ease(num / 0.5f);
	}

	// Token: 0x0400061C RID: 1564
	private static Dictionary<exEase.Type, Func<float, float>> easeFunctions;

	// Token: 0x0400061D RID: 1565
	private static bool initialized;

	// Token: 0x020015DC RID: 5596
	public enum Type
	{
		// Token: 0x04005804 RID: 22532
		Linear,
		// Token: 0x04005805 RID: 22533
		QuadIn,
		// Token: 0x04005806 RID: 22534
		QuadOut,
		// Token: 0x04005807 RID: 22535
		QuadInOut,
		// Token: 0x04005808 RID: 22536
		QuadOutIn,
		// Token: 0x04005809 RID: 22537
		CubicIn,
		// Token: 0x0400580A RID: 22538
		CubicOut,
		// Token: 0x0400580B RID: 22539
		CubicInOut,
		// Token: 0x0400580C RID: 22540
		CubicOutIn,
		// Token: 0x0400580D RID: 22541
		QuartIn,
		// Token: 0x0400580E RID: 22542
		QuartOut,
		// Token: 0x0400580F RID: 22543
		QuartInOut,
		// Token: 0x04005810 RID: 22544
		QuartOutIn,
		// Token: 0x04005811 RID: 22545
		QuintIn,
		// Token: 0x04005812 RID: 22546
		QuintOut,
		// Token: 0x04005813 RID: 22547
		QuintInOut,
		// Token: 0x04005814 RID: 22548
		QuintOutIn,
		// Token: 0x04005815 RID: 22549
		SineIn,
		// Token: 0x04005816 RID: 22550
		SineOut,
		// Token: 0x04005817 RID: 22551
		SineInOut,
		// Token: 0x04005818 RID: 22552
		SineOutIn,
		// Token: 0x04005819 RID: 22553
		ExpoIn,
		// Token: 0x0400581A RID: 22554
		ExpoOut,
		// Token: 0x0400581B RID: 22555
		ExpoInOut,
		// Token: 0x0400581C RID: 22556
		ExpoOutIn,
		// Token: 0x0400581D RID: 22557
		CircIn,
		// Token: 0x0400581E RID: 22558
		CircOut,
		// Token: 0x0400581F RID: 22559
		CircInOut,
		// Token: 0x04005820 RID: 22560
		CircOutIn,
		// Token: 0x04005821 RID: 22561
		ElasticIn,
		// Token: 0x04005822 RID: 22562
		ElasticOut,
		// Token: 0x04005823 RID: 22563
		ElasticInOut,
		// Token: 0x04005824 RID: 22564
		ElasticOutIn,
		// Token: 0x04005825 RID: 22565
		BackIn,
		// Token: 0x04005826 RID: 22566
		BackOut,
		// Token: 0x04005827 RID: 22567
		BackInOut,
		// Token: 0x04005828 RID: 22568
		BackOutIn,
		// Token: 0x04005829 RID: 22569
		BounceIn,
		// Token: 0x0400582A RID: 22570
		BounceOut,
		// Token: 0x0400582B RID: 22571
		BounceInOut,
		// Token: 0x0400582C RID: 22572
		BounceOutIn,
		// Token: 0x0400582D RID: 22573
		Smooth,
		// Token: 0x0400582E RID: 22574
		Fade,
		// Token: 0x0400582F RID: 22575
		Spring
	}
}
