using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.Bindings;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x020004A1 RID: 1185
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal interface IStylePropertyAnimationSystem
	{
		// Token: 0x06002486 RID: 9350
		bool StartTransition(VisualElement owner, StylePropertyId prop, float startValue, float endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve);

		// Token: 0x06002487 RID: 9351
		bool StartTransition(VisualElement owner, StylePropertyId prop, int startValue, int endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve);

		// Token: 0x06002488 RID: 9352
		bool StartTransition(VisualElement owner, StylePropertyId prop, Length startValue, Length endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve);

		// Token: 0x06002489 RID: 9353
		bool StartTransition(VisualElement owner, StylePropertyId prop, Color startValue, Color endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve);

		// Token: 0x0600248A RID: 9354
		bool StartTransitionEnum(VisualElement owner, StylePropertyId prop, int startValue, int endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve);

		// Token: 0x0600248B RID: 9355
		bool StartTransition(VisualElement owner, StylePropertyId prop, Background startValue, Background endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve);

		// Token: 0x0600248C RID: 9356
		bool StartTransition(VisualElement owner, StylePropertyId prop, FontDefinition startValue, FontDefinition endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve);

		// Token: 0x0600248D RID: 9357
		bool StartTransition(VisualElement owner, StylePropertyId prop, Font startValue, Font endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve);

		// Token: 0x0600248E RID: 9358
		bool StartTransition(VisualElement owner, StylePropertyId prop, TextShadow startValue, TextShadow endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve);

		// Token: 0x0600248F RID: 9359
		bool StartTransition(VisualElement owner, StylePropertyId prop, Scale startValue, Scale endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve);

		// Token: 0x06002490 RID: 9360
		bool StartTransition(VisualElement owner, StylePropertyId prop, TransformOrigin startValue, TransformOrigin endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve);

		// Token: 0x06002491 RID: 9361
		bool StartTransition(VisualElement owner, StylePropertyId prop, Translate startValue, Translate endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve);

		// Token: 0x06002492 RID: 9362
		bool StartTransition(VisualElement owner, StylePropertyId prop, Rotate startValue, Rotate endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve);

		// Token: 0x06002493 RID: 9363
		bool StartTransition(VisualElement owner, StylePropertyId prop, BackgroundPosition startValue, BackgroundPosition endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve);

		// Token: 0x06002494 RID: 9364
		bool StartTransition(VisualElement owner, StylePropertyId prop, BackgroundRepeat startValue, BackgroundRepeat endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve);

		// Token: 0x06002495 RID: 9365
		bool StartTransition(VisualElement owner, StylePropertyId prop, BackgroundSize startValue, BackgroundSize endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve);

		// Token: 0x06002496 RID: 9366
		void CancelAllAnimations();

		// Token: 0x06002497 RID: 9367
		void CancelAllAnimations(VisualElement owner);

		// Token: 0x06002498 RID: 9368
		void CancelAnimation(VisualElement owner, StylePropertyId id);

		// Token: 0x06002499 RID: 9369
		bool HasRunningAnimation(VisualElement owner, StylePropertyId id);

		// Token: 0x0600249A RID: 9370
		void UpdateAnimation(VisualElement owner, StylePropertyId id);

		// Token: 0x0600249B RID: 9371
		void GetAllAnimations(VisualElement owner, List<StylePropertyId> propertyIds);

		// Token: 0x0600249C RID: 9372
		void Update();
	}
}
