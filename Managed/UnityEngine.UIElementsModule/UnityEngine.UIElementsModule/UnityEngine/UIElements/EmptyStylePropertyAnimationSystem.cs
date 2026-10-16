using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.Bindings;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x020004BF RID: 1215
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal class EmptyStylePropertyAnimationSystem : IStylePropertyAnimationSystem
	{
		// Token: 0x0600255A RID: 9562 RVA: 0x00088060 File Offset: 0x00086260
		public bool StartTransition(VisualElement owner, StylePropertyId prop, float startValue, float endValue, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x00088074 File Offset: 0x00086274
		public bool StartTransition(VisualElement owner, StylePropertyId prop, int startValue, int endValue, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x0600255C RID: 9564 RVA: 0x00088088 File Offset: 0x00086288
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Length startValue, Length endValue, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x0600255D RID: 9565 RVA: 0x0008809C File Offset: 0x0008629C
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Color startValue, Color endValue, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x000880B0 File Offset: 0x000862B0
		public bool StartTransitionEnum(VisualElement owner, StylePropertyId prop, int startValue, int endValue, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x000880C4 File Offset: 0x000862C4
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Background startValue, Background endValue, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x000880D8 File Offset: 0x000862D8
		public bool StartTransition(VisualElement owner, StylePropertyId prop, FontDefinition startValue, FontDefinition endValue, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x000880EC File Offset: 0x000862EC
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Font startValue, Font endValue, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x00088100 File Offset: 0x00086300
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Cursor startValue, Cursor endValue, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x00088114 File Offset: 0x00086314
		public bool StartTransition(VisualElement owner, StylePropertyId prop, TextShadow startValue, TextShadow endValue, int durationMs, int delayMs, Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x00088128 File Offset: 0x00086328
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Scale startValue, Scale endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x06002565 RID: 9573 RVA: 0x0008813C File Offset: 0x0008633C
		public bool StartTransition(VisualElement owner, StylePropertyId prop, TransformOrigin startValue, TransformOrigin endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x06002566 RID: 9574 RVA: 0x00088150 File Offset: 0x00086350
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Translate startValue, Translate endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x06002567 RID: 9575 RVA: 0x00088164 File Offset: 0x00086364
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Rotate startValue, Rotate endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x00088178 File Offset: 0x00086378
		public bool StartTransition(VisualElement owner, StylePropertyId prop, BackgroundPosition startValue, BackgroundPosition endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x06002569 RID: 9577 RVA: 0x0008818C File Offset: 0x0008638C
		public bool StartTransition(VisualElement owner, StylePropertyId prop, BackgroundRepeat startValue, BackgroundRepeat endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x0600256A RID: 9578 RVA: 0x000881A0 File Offset: 0x000863A0
		public bool StartTransition(VisualElement owner, StylePropertyId prop, BackgroundSize startValue, BackgroundSize endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve)
		{
			return false;
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x00002152 File Offset: 0x00000352
		public void CancelAllAnimations()
		{
		}

		// Token: 0x0600256C RID: 9580 RVA: 0x00002152 File Offset: 0x00000352
		public void CancelAllAnimations(VisualElement owner)
		{
		}

		// Token: 0x0600256D RID: 9581 RVA: 0x00002152 File Offset: 0x00000352
		public void CancelAnimation(VisualElement owner, StylePropertyId id)
		{
		}

		// Token: 0x0600256E RID: 9582 RVA: 0x000881B4 File Offset: 0x000863B4
		public bool HasRunningAnimation(VisualElement owner, StylePropertyId id)
		{
			return false;
		}

		// Token: 0x0600256F RID: 9583 RVA: 0x00002152 File Offset: 0x00000352
		public void UpdateAnimation(VisualElement owner, StylePropertyId id)
		{
		}

		// Token: 0x06002570 RID: 9584 RVA: 0x00002152 File Offset: 0x00000352
		public void GetAllAnimations(VisualElement owner, List<StylePropertyId> propertyIds)
		{
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x00002152 File Offset: 0x00000352
		public void Update()
		{
		}
	}
}
