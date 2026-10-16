using System;
using UnityEngine;

namespace RedShadow.Tween
{
	// Token: 0x020001E4 RID: 484
	public static class TweenExtensions
	{
		// Token: 0x0600117D RID: 4477 RVA: 0x000696B0 File Offset: 0x000678B0
		public static Tween<Vector3> moveTo(this Transform t, Vector3 endValue)
		{
			Tween<Vector3> tween = new Tween<Vector3>(t.position, delegate(object value)
			{
				t.position = (Vector3)value;
			}, endValue);
			TweenManager.Instance.StartCoroutine(tween.start());
			return tween;
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x000696FC File Offset: 0x000678FC
		public static Tween<Vector3> moveFromTo(this Transform t, Vector3 startValue, Vector3 endValue)
		{
			Tween<Vector3> tween = new Tween<Vector3>(startValue, delegate(object value)
			{
				t.position = (Vector3)value;
			}, endValue);
			TweenManager.Instance.StartCoroutine(tween.start());
			return tween;
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x0006973C File Offset: 0x0006793C
		public static Tween<Vector3> rotateTo(this Transform t, Vector3 endValue)
		{
			Tween<Vector3> tween = new Tween<Vector3>(t.eulerAngles, delegate(object value)
			{
				t.eulerAngles = (Vector3)value;
			}, endValue);
			TweenManager.Instance.StartCoroutine(tween.start());
			return tween;
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x00069788 File Offset: 0x00067988
		public static Tween<Color> colorTo(this Color t, Color endValue)
		{
			Tween<Color> tween = new Tween<Color>(t, delegate(object value)
			{
				t = (Color)value;
			}, endValue);
			TweenManager.Instance.StartCoroutine(tween.start());
			return tween;
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x000697D0 File Offset: 0x000679D0
		public static Tween<float> fadeTo(this Color t, float endValue)
		{
			Tween<float> tween = new Tween<float>(t.a, delegate(object value)
			{
				t.a = (float)value;
			}, endValue);
			TweenManager.Instance.StartCoroutine(tween.start());
			return tween;
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x0006981C File Offset: 0x00067A1C
		public static Tween<float> floatPropertyTo(this Material m, string propertyName, float endValue)
		{
			Tween<float> tween = new Tween<float>(m.GetFloat(propertyName), delegate(object value)
			{
				m.SetFloat(propertyName, (float)value);
			}, endValue);
			TweenManager.Instance.StartCoroutine(tween.start());
			return tween;
		}
	}
}
