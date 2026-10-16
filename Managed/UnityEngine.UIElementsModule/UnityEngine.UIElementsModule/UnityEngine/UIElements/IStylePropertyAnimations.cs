using System;
using System.Collections.Generic;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x020004A0 RID: 1184
	internal interface IStylePropertyAnimations
	{
		// Token: 0x0600246D RID: 9325
		bool Start(StylePropertyId id, float from, float to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x0600246E RID: 9326
		bool Start(StylePropertyId id, int from, int to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x0600246F RID: 9327
		bool Start(StylePropertyId id, Length from, Length to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06002470 RID: 9328
		bool Start(StylePropertyId id, Color from, Color to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06002471 RID: 9329
		bool StartEnum(StylePropertyId id, int from, int to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06002472 RID: 9330
		bool Start(StylePropertyId id, Background from, Background to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06002473 RID: 9331
		bool Start(StylePropertyId id, FontDefinition from, FontDefinition to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06002474 RID: 9332
		bool Start(StylePropertyId id, Font from, Font to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06002475 RID: 9333
		bool Start(StylePropertyId id, TextShadow from, TextShadow to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06002476 RID: 9334
		bool Start(StylePropertyId id, Scale from, Scale to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06002477 RID: 9335
		bool Start(StylePropertyId id, Translate from, Translate to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06002478 RID: 9336
		bool Start(StylePropertyId id, Rotate from, Rotate to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x06002479 RID: 9337
		bool Start(StylePropertyId id, TransformOrigin from, TransformOrigin to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x0600247A RID: 9338
		bool Start(StylePropertyId id, BackgroundPosition from, BackgroundPosition to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x0600247B RID: 9339
		bool Start(StylePropertyId id, BackgroundRepeat from, BackgroundRepeat to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x0600247C RID: 9340
		bool Start(StylePropertyId id, BackgroundSize from, BackgroundSize to, int durationMs, int delayMs, Func<float, float> easingCurve);

		// Token: 0x0600247D RID: 9341
		bool HasRunningAnimation(StylePropertyId id);

		// Token: 0x0600247E RID: 9342
		void UpdateAnimation(StylePropertyId id);

		// Token: 0x0600247F RID: 9343
		void GetAllAnimations(List<StylePropertyId> outPropertyIds);

		// Token: 0x06002480 RID: 9344
		void CancelAnimation(StylePropertyId id);

		// Token: 0x06002481 RID: 9345
		void CancelAllAnimations();

		// Token: 0x1700096E RID: 2414
		// (get) Token: 0x06002482 RID: 9346
		// (set) Token: 0x06002483 RID: 9347
		int runningAnimationCount { get; set; }

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06002484 RID: 9348
		// (set) Token: 0x06002485 RID: 9349
		int completedAnimationCount { get; set; }
	}
}
