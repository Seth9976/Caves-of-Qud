using System;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x0200071B RID: 1819
	public interface ITransitionAnimations
	{
		// Token: 0x060034B2 RID: 13490
		ValueAnimation<float> Start(float from, float to, int durationMs, Action<VisualElement, float> onValueChanged);

		// Token: 0x060034B3 RID: 13491
		ValueAnimation<Rect> Start(Rect from, Rect to, int durationMs, Action<VisualElement, Rect> onValueChanged);

		// Token: 0x060034B4 RID: 13492
		ValueAnimation<Color> Start(Color from, Color to, int durationMs, Action<VisualElement, Color> onValueChanged);

		// Token: 0x060034B5 RID: 13493
		ValueAnimation<Vector3> Start(Vector3 from, Vector3 to, int durationMs, Action<VisualElement, Vector3> onValueChanged);

		// Token: 0x060034B6 RID: 13494
		ValueAnimation<Vector2> Start(Vector2 from, Vector2 to, int durationMs, Action<VisualElement, Vector2> onValueChanged);

		// Token: 0x060034B7 RID: 13495
		ValueAnimation<Quaternion> Start(Quaternion from, Quaternion to, int durationMs, Action<VisualElement, Quaternion> onValueChanged);

		// Token: 0x060034B8 RID: 13496
		ValueAnimation<StyleValues> Start(StyleValues from, StyleValues to, int durationMs);

		// Token: 0x060034B9 RID: 13497
		ValueAnimation<StyleValues> Start(StyleValues to, int durationMs);

		// Token: 0x060034BA RID: 13498
		ValueAnimation<float> Start(Func<VisualElement, float> fromValueGetter, float to, int durationMs, Action<VisualElement, float> onValueChanged);

		// Token: 0x060034BB RID: 13499
		ValueAnimation<Rect> Start(Func<VisualElement, Rect> fromValueGetter, Rect to, int durationMs, Action<VisualElement, Rect> onValueChanged);

		// Token: 0x060034BC RID: 13500
		ValueAnimation<Color> Start(Func<VisualElement, Color> fromValueGetter, Color to, int durationMs, Action<VisualElement, Color> onValueChanged);

		// Token: 0x060034BD RID: 13501
		ValueAnimation<Vector3> Start(Func<VisualElement, Vector3> fromValueGetter, Vector3 to, int durationMs, Action<VisualElement, Vector3> onValueChanged);

		// Token: 0x060034BE RID: 13502
		ValueAnimation<Vector2> Start(Func<VisualElement, Vector2> fromValueGetter, Vector2 to, int durationMs, Action<VisualElement, Vector2> onValueChanged);

		// Token: 0x060034BF RID: 13503
		ValueAnimation<Quaternion> Start(Func<VisualElement, Quaternion> fromValueGetter, Quaternion to, int durationMs, Action<VisualElement, Quaternion> onValueChanged);

		// Token: 0x060034C0 RID: 13504
		ValueAnimation<Rect> Layout(Rect to, int durationMs);

		// Token: 0x060034C1 RID: 13505
		ValueAnimation<Vector2> TopLeft(Vector2 to, int durationMs);

		// Token: 0x060034C2 RID: 13506
		ValueAnimation<Vector2> Size(Vector2 to, int durationMs);

		// Token: 0x060034C3 RID: 13507
		ValueAnimation<float> Scale(float to, int duration);

		// Token: 0x060034C4 RID: 13508
		ValueAnimation<Vector3> Position(Vector3 to, int duration);

		// Token: 0x060034C5 RID: 13509
		ValueAnimation<Quaternion> Rotation(Quaternion to, int duration);
	}
}
