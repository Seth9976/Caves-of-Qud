using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine.Assertions;
using UnityEngine.Bindings;
using UnityEngine.Pool;
using UnityEngine.UIElements.StyleSheets;

namespace UnityEngine.UIElements
{
	// Token: 0x020004A2 RID: 1186
	[VisibleToOtherModules(new string[] { "UnityEditor.UIBuilderModule" })]
	internal class StylePropertyAnimationSystem : IStylePropertyAnimationSystem
	{
		// Token: 0x0600249D RID: 9373 RVA: 0x00084DF0 File Offset: 0x00082FF0
		public StylePropertyAnimationSystem()
		{
			this.m_CurrentTimeMs = Panel.TimeSinceStartupMs();
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x00084E24 File Offset: 0x00083024
		private T GetOrCreate<T>(ref T values) where T : new()
		{
			T t = values;
			return (t != null) ? t : (values = new T());
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x00084E58 File Offset: 0x00083058
		private bool StartTransition<T>(VisualElement owner, StylePropertyId prop, T startValue, T endValue, int durationMs, int delayMs, Func<float, float> easingCurve, StylePropertyAnimationSystem.Values<T> values)
		{
			this.m_PropertyToValues[prop] = values;
			bool flag = values.StartTransition(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.CurrentTimeMs());
			this.UpdateTracking<T>(values);
			return flag;
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x00084E9C File Offset: 0x0008309C
		public bool StartTransition(VisualElement owner, StylePropertyId prop, float startValue, float endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<float>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesFloat>(ref this.m_Floats));
		}

		// Token: 0x060024A1 RID: 9377 RVA: 0x00084ECC File Offset: 0x000830CC
		public bool StartTransition(VisualElement owner, StylePropertyId prop, int startValue, int endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<int>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesInt>(ref this.m_Ints));
		}

		// Token: 0x060024A2 RID: 9378 RVA: 0x00084EFC File Offset: 0x000830FC
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Length startValue, Length endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<Length>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesLength>(ref this.m_Lengths));
		}

		// Token: 0x060024A3 RID: 9379 RVA: 0x00084F2C File Offset: 0x0008312C
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Color startValue, Color endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<Color>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesColor>(ref this.m_Colors));
		}

		// Token: 0x060024A4 RID: 9380 RVA: 0x00084F5C File Offset: 0x0008315C
		public bool StartTransitionEnum(VisualElement owner, StylePropertyId prop, int startValue, int endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<int>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesEnum>(ref this.m_Enums));
		}

		// Token: 0x060024A5 RID: 9381 RVA: 0x00084F8C File Offset: 0x0008318C
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Background startValue, Background endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<Background>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesBackground>(ref this.m_Backgrounds));
		}

		// Token: 0x060024A6 RID: 9382 RVA: 0x00084FBC File Offset: 0x000831BC
		public bool StartTransition(VisualElement owner, StylePropertyId prop, FontDefinition startValue, FontDefinition endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<FontDefinition>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesFontDefinition>(ref this.m_FontDefinitions));
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x00084FEC File Offset: 0x000831EC
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Font startValue, Font endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<Font>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesFont>(ref this.m_Fonts));
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x0008501C File Offset: 0x0008321C
		public bool StartTransition(VisualElement owner, StylePropertyId prop, TextShadow startValue, TextShadow endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<TextShadow>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesTextShadow>(ref this.m_TextShadows));
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x0008504C File Offset: 0x0008324C
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Scale startValue, Scale endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<Scale>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesScale>(ref this.m_Scale));
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x0008507C File Offset: 0x0008327C
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Rotate startValue, Rotate endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<Rotate>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesRotate>(ref this.m_Rotate));
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x000850AC File Offset: 0x000832AC
		public bool StartTransition(VisualElement owner, StylePropertyId prop, Translate startValue, Translate endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<Translate>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesTranslate>(ref this.m_Translate));
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x000850DC File Offset: 0x000832DC
		public bool StartTransition(VisualElement owner, StylePropertyId prop, TransformOrigin startValue, TransformOrigin endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<TransformOrigin>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesTransformOrigin>(ref this.m_TransformOrigin));
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x0008510C File Offset: 0x0008330C
		public bool StartTransition(VisualElement owner, StylePropertyId prop, BackgroundPosition startValue, BackgroundPosition endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<BackgroundPosition>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesBackgroundPosition>(ref this.m_BackgroundPosition));
		}

		// Token: 0x060024AE RID: 9390 RVA: 0x0008513C File Offset: 0x0008333C
		public bool StartTransition(VisualElement owner, StylePropertyId prop, BackgroundRepeat startValue, BackgroundRepeat endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<BackgroundRepeat>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesBackgroundRepeat>(ref this.m_BackgroundRepeat));
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x0008516C File Offset: 0x0008336C
		public bool StartTransition(VisualElement owner, StylePropertyId prop, BackgroundSize startValue, BackgroundSize endValue, int durationMs, int delayMs, [JetBrains.Annotations.NotNull] Func<float, float> easingCurve)
		{
			return this.StartTransition<BackgroundSize>(owner, prop, startValue, endValue, durationMs, delayMs, easingCurve, this.GetOrCreate<StylePropertyAnimationSystem.ValuesBackgroundSize>(ref this.m_BackgroundSize));
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x0008519C File Offset: 0x0008339C
		public void CancelAllAnimations()
		{
			foreach (StylePropertyAnimationSystem.Values values in this.m_AllValues)
			{
				values.CancelAllAnimations();
			}
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x000851F4 File Offset: 0x000833F4
		public void CancelAllAnimations(VisualElement owner)
		{
			foreach (StylePropertyAnimationSystem.Values values in this.m_AllValues)
			{
				values.CancelAllAnimations(owner);
			}
			Assert.AreEqual(0, owner.styleAnimation.runningAnimationCount);
			Assert.AreEqual(0, owner.styleAnimation.completedAnimationCount);
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x00085274 File Offset: 0x00083474
		public void CancelAnimation(VisualElement owner, StylePropertyId id)
		{
			StylePropertyAnimationSystem.Values values;
			bool flag = this.m_PropertyToValues.TryGetValue(id, out values);
			if (flag)
			{
				values.CancelAnimation(owner, id);
			}
		}

		// Token: 0x060024B3 RID: 9395 RVA: 0x000852A0 File Offset: 0x000834A0
		public bool HasRunningAnimation(VisualElement owner, StylePropertyId id)
		{
			StylePropertyAnimationSystem.Values values;
			return this.m_PropertyToValues.TryGetValue(id, out values) && values.HasRunningAnimation(owner, id);
		}

		// Token: 0x060024B4 RID: 9396 RVA: 0x000852D0 File Offset: 0x000834D0
		public void UpdateAnimation(VisualElement owner, StylePropertyId id)
		{
			StylePropertyAnimationSystem.Values values;
			bool flag = this.m_PropertyToValues.TryGetValue(id, out values);
			if (flag)
			{
				values.UpdateAnimation(owner, id);
			}
		}

		// Token: 0x060024B5 RID: 9397 RVA: 0x000852FC File Offset: 0x000834FC
		public void GetAllAnimations(VisualElement owner, List<StylePropertyId> propertyIds)
		{
			foreach (StylePropertyAnimationSystem.Values values in this.m_AllValues)
			{
				values.GetAllAnimations(owner, propertyIds);
			}
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x00085354 File Offset: 0x00083554
		private void UpdateTracking<T>(StylePropertyAnimationSystem.Values<T> values)
		{
			bool flag = !values.isEmpty && !this.m_AllValues.Contains(values);
			if (flag)
			{
				this.m_AllValues.Add(values);
			}
		}

		// Token: 0x060024B7 RID: 9399 RVA: 0x00085390 File Offset: 0x00083590
		private long CurrentTimeMs()
		{
			return this.m_CurrentTimeMs;
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x000853A8 File Offset: 0x000835A8
		public void Update()
		{
			this.m_CurrentTimeMs = Panel.TimeSinceStartupMs();
			int count = this.m_AllValues.Count;
			for (int i = 0; i < count; i++)
			{
				this.m_AllValues[i].Update(this.m_CurrentTimeMs);
			}
		}

		// Token: 0x04001059 RID: 4185
		private long m_CurrentTimeMs = 0L;

		// Token: 0x0400105A RID: 4186
		private StylePropertyAnimationSystem.ValuesFloat m_Floats;

		// Token: 0x0400105B RID: 4187
		private StylePropertyAnimationSystem.ValuesInt m_Ints;

		// Token: 0x0400105C RID: 4188
		private StylePropertyAnimationSystem.ValuesLength m_Lengths;

		// Token: 0x0400105D RID: 4189
		private StylePropertyAnimationSystem.ValuesColor m_Colors;

		// Token: 0x0400105E RID: 4190
		private StylePropertyAnimationSystem.ValuesEnum m_Enums;

		// Token: 0x0400105F RID: 4191
		private StylePropertyAnimationSystem.ValuesBackground m_Backgrounds;

		// Token: 0x04001060 RID: 4192
		private StylePropertyAnimationSystem.ValuesFontDefinition m_FontDefinitions;

		// Token: 0x04001061 RID: 4193
		private StylePropertyAnimationSystem.ValuesFont m_Fonts;

		// Token: 0x04001062 RID: 4194
		private StylePropertyAnimationSystem.ValuesTextShadow m_TextShadows;

		// Token: 0x04001063 RID: 4195
		private StylePropertyAnimationSystem.ValuesScale m_Scale;

		// Token: 0x04001064 RID: 4196
		private StylePropertyAnimationSystem.ValuesRotate m_Rotate;

		// Token: 0x04001065 RID: 4197
		private StylePropertyAnimationSystem.ValuesTranslate m_Translate;

		// Token: 0x04001066 RID: 4198
		private StylePropertyAnimationSystem.ValuesTransformOrigin m_TransformOrigin;

		// Token: 0x04001067 RID: 4199
		private StylePropertyAnimationSystem.ValuesBackgroundPosition m_BackgroundPosition;

		// Token: 0x04001068 RID: 4200
		private StylePropertyAnimationSystem.ValuesBackgroundRepeat m_BackgroundRepeat;

		// Token: 0x04001069 RID: 4201
		private StylePropertyAnimationSystem.ValuesBackgroundSize m_BackgroundSize;

		// Token: 0x0400106A RID: 4202
		private readonly List<StylePropertyAnimationSystem.Values> m_AllValues = new List<StylePropertyAnimationSystem.Values>();

		// Token: 0x0400106B RID: 4203
		private readonly Dictionary<StylePropertyId, StylePropertyAnimationSystem.Values> m_PropertyToValues = new Dictionary<StylePropertyId, StylePropertyAnimationSystem.Values>();

		// Token: 0x020004A3 RID: 1187
		[Flags]
		private enum TransitionState
		{
			// Token: 0x0400106D RID: 4205
			None = 0,
			// Token: 0x0400106E RID: 4206
			Running = 1,
			// Token: 0x0400106F RID: 4207
			Started = 2,
			// Token: 0x04001070 RID: 4208
			Ended = 4,
			// Token: 0x04001071 RID: 4209
			Canceled = 8
		}

		// Token: 0x020004A4 RID: 1188
		private struct AnimationDataSet<TTimingData, TStyleData>
		{
			// Token: 0x17000970 RID: 2416
			// (get) Token: 0x060024B9 RID: 9401 RVA: 0x000853F7 File Offset: 0x000835F7
			// (set) Token: 0x060024BA RID: 9402 RVA: 0x00085401 File Offset: 0x00083601
			private int capacity
			{
				get
				{
					return this.elements.Length;
				}
				set
				{
					Array.Resize<VisualElement>(ref this.elements, value);
					Array.Resize<StylePropertyId>(ref this.properties, value);
					Array.Resize<TTimingData>(ref this.timing, value);
					Array.Resize<TStyleData>(ref this.style, value);
				}
			}

			// Token: 0x060024BB RID: 9403 RVA: 0x00085438 File Offset: 0x00083638
			private void LocalInit()
			{
				this.elements = new VisualElement[2];
				this.properties = new StylePropertyId[2];
				this.timing = new TTimingData[2];
				this.style = new TStyleData[2];
				this.indices = new Dictionary<StylePropertyAnimationSystem.ElementPropertyPair, int>(StylePropertyAnimationSystem.ElementPropertyPair.Comparer);
			}

			// Token: 0x060024BC RID: 9404 RVA: 0x00085488 File Offset: 0x00083688
			public static StylePropertyAnimationSystem.AnimationDataSet<TTimingData, TStyleData> Create()
			{
				StylePropertyAnimationSystem.AnimationDataSet<TTimingData, TStyleData> animationDataSet = default(StylePropertyAnimationSystem.AnimationDataSet<TTimingData, TStyleData>);
				animationDataSet.LocalInit();
				return animationDataSet;
			}

			// Token: 0x060024BD RID: 9405 RVA: 0x000854AC File Offset: 0x000836AC
			public bool IndexOf(VisualElement ve, StylePropertyId prop, out int index)
			{
				return this.indices.TryGetValue(new StylePropertyAnimationSystem.ElementPropertyPair(ve, prop), out index);
			}

			// Token: 0x060024BE RID: 9406 RVA: 0x000854D4 File Offset: 0x000836D4
			public void Add(VisualElement owner, StylePropertyId prop, TTimingData timingData, TStyleData styleData)
			{
				bool flag = this.count >= this.capacity;
				if (flag)
				{
					this.capacity *= 2;
				}
				int num = this.count;
				this.count = num + 1;
				int num2 = num;
				this.elements[num2] = owner;
				this.properties[num2] = prop;
				this.timing[num2] = timingData;
				this.style[num2] = styleData;
				this.indices.Add(new StylePropertyAnimationSystem.ElementPropertyPair(owner, prop), num2);
			}

			// Token: 0x060024BF RID: 9407 RVA: 0x0008555C File Offset: 0x0008375C
			public void Remove(int cancelledIndex)
			{
				int num = this.count - 1;
				this.count = num;
				int num2 = num;
				this.indices.Remove(new StylePropertyAnimationSystem.ElementPropertyPair(this.elements[cancelledIndex], this.properties[cancelledIndex]));
				bool flag = cancelledIndex != num2;
				if (flag)
				{
					VisualElement visualElement = (this.elements[cancelledIndex] = this.elements[num2]);
					StylePropertyId stylePropertyId = (this.properties[cancelledIndex] = this.properties[num2]);
					this.timing[cancelledIndex] = this.timing[num2];
					this.style[cancelledIndex] = this.style[num2];
					this.indices[new StylePropertyAnimationSystem.ElementPropertyPair(visualElement, stylePropertyId)] = cancelledIndex;
				}
				this.elements[num2] = null;
				this.properties[num2] = StylePropertyId.Unknown;
				this.timing[num2] = default(TTimingData);
				this.style[num2] = default(TStyleData);
			}

			// Token: 0x060024C0 RID: 9408 RVA: 0x0008565A File Offset: 0x0008385A
			public void Replace(int index, TTimingData timingData, TStyleData styleData)
			{
				this.timing[index] = timingData;
				this.style[index] = styleData;
			}

			// Token: 0x060024C1 RID: 9409 RVA: 0x00085678 File Offset: 0x00083878
			public void RemoveAll(VisualElement ve)
			{
				int num = this.count;
				for (int i = num - 1; i >= 0; i--)
				{
					bool flag = this.elements[i] == ve;
					if (flag)
					{
						this.Remove(i);
					}
				}
			}

			// Token: 0x060024C2 RID: 9410 RVA: 0x000856BC File Offset: 0x000838BC
			public void RemoveAll()
			{
				this.capacity = 2;
				int num = Mathf.Min(this.count, this.capacity);
				Array.Clear(this.elements, 0, num);
				Array.Clear(this.properties, 0, num);
				Array.Clear(this.timing, 0, num);
				Array.Clear(this.style, 0, num);
				this.count = 0;
				this.indices.Clear();
			}

			// Token: 0x060024C3 RID: 9411 RVA: 0x00085730 File Offset: 0x00083930
			public void GetActivePropertiesForElement(VisualElement ve, List<StylePropertyId> outProperties)
			{
				int num = this.count;
				for (int i = num - 1; i >= 0; i--)
				{
					bool flag = this.elements[i] == ve;
					if (flag)
					{
						outProperties.Add(this.properties[i]);
					}
				}
			}

			// Token: 0x04001072 RID: 4210
			private const int InitialSize = 2;

			// Token: 0x04001073 RID: 4211
			public VisualElement[] elements;

			// Token: 0x04001074 RID: 4212
			public StylePropertyId[] properties;

			// Token: 0x04001075 RID: 4213
			public TTimingData[] timing;

			// Token: 0x04001076 RID: 4214
			public TStyleData[] style;

			// Token: 0x04001077 RID: 4215
			public int count;

			// Token: 0x04001078 RID: 4216
			private Dictionary<StylePropertyAnimationSystem.ElementPropertyPair, int> indices;
		}

		// Token: 0x020004A5 RID: 1189
		private struct ElementPropertyPair
		{
			// Token: 0x060024C4 RID: 9412 RVA: 0x0008577A File Offset: 0x0008397A
			public ElementPropertyPair(VisualElement element, StylePropertyId property)
			{
				this.element = element;
				this.property = property;
			}

			// Token: 0x04001079 RID: 4217
			public static readonly IEqualityComparer<StylePropertyAnimationSystem.ElementPropertyPair> Comparer = new StylePropertyAnimationSystem.ElementPropertyPair.EqualityComparer();

			// Token: 0x0400107A RID: 4218
			public readonly VisualElement element;

			// Token: 0x0400107B RID: 4219
			public readonly StylePropertyId property;

			// Token: 0x020004A6 RID: 1190
			private class EqualityComparer : IEqualityComparer<StylePropertyAnimationSystem.ElementPropertyPair>
			{
				// Token: 0x060024C6 RID: 9414 RVA: 0x00085798 File Offset: 0x00083998
				public bool Equals(StylePropertyAnimationSystem.ElementPropertyPair x, StylePropertyAnimationSystem.ElementPropertyPair y)
				{
					return x.element == y.element && x.property == y.property;
				}

				// Token: 0x060024C7 RID: 9415 RVA: 0x000857CC File Offset: 0x000839CC
				public int GetHashCode(StylePropertyAnimationSystem.ElementPropertyPair obj)
				{
					return (obj.element.GetHashCode() * 397) ^ (int)obj.property;
				}
			}
		}

		// Token: 0x020004A7 RID: 1191
		private abstract class Values
		{
			// Token: 0x060024C9 RID: 9417
			public abstract void CancelAllAnimations();

			// Token: 0x060024CA RID: 9418
			public abstract void CancelAllAnimations(VisualElement ve);

			// Token: 0x060024CB RID: 9419
			public abstract void CancelAnimation(VisualElement ve, StylePropertyId id);

			// Token: 0x060024CC RID: 9420
			public abstract bool HasRunningAnimation(VisualElement ve, StylePropertyId id);

			// Token: 0x060024CD RID: 9421
			public abstract void UpdateAnimation(VisualElement ve, StylePropertyId id);

			// Token: 0x060024CE RID: 9422
			public abstract void GetAllAnimations(VisualElement ve, List<StylePropertyId> outPropertyIds);

			// Token: 0x060024CF RID: 9423
			public abstract void Update(long currentTimeMs);

			// Token: 0x060024D0 RID: 9424
			protected abstract void UpdateValues();

			// Token: 0x060024D1 RID: 9425
			protected abstract void UpdateComputedStyle();

			// Token: 0x060024D2 RID: 9426
			protected abstract void UpdateComputedStyle(int i);
		}

		// Token: 0x020004A8 RID: 1192
		private abstract class Values<T> : StylePropertyAnimationSystem.Values
		{
			// Token: 0x17000971 RID: 2417
			// (get) Token: 0x060024D4 RID: 9428 RVA: 0x000857F7 File Offset: 0x000839F7
			public bool isEmpty
			{
				get
				{
					return this.running.count + this.completed.count == 0;
				}
			}

			// Token: 0x17000972 RID: 2418
			// (get) Token: 0x060024D5 RID: 9429
			public abstract Func<T, T, bool> SameFunc { get; }

			// Token: 0x060024D6 RID: 9430 RVA: 0x00085814 File Offset: 0x00083A14
			protected virtual bool ConvertUnits(VisualElement owner, StylePropertyId prop, ref T a, ref T b)
			{
				return true;
			}

			// Token: 0x060024D7 RID: 9431 RVA: 0x00085828 File Offset: 0x00083A28
			protected Values()
			{
				this.running = StylePropertyAnimationSystem.AnimationDataSet<StylePropertyAnimationSystem.Values<T>.TimingData, StylePropertyAnimationSystem.Values<T>.StyleData>.Create();
				this.completed = StylePropertyAnimationSystem.AnimationDataSet<StylePropertyAnimationSystem.Values<T>.EmptyData, T>.Create();
				this.m_CurrentTimeMs = Panel.TimeSinceStartupMs();
			}

			// Token: 0x060024D8 RID: 9432 RVA: 0x0008587C File Offset: 0x00083A7C
			private void SwapFrameStates()
			{
				StylePropertyAnimationSystem.Values<T>.TransitionEventsFrameState currentFrameEventsState = this.m_CurrentFrameEventsState;
				this.m_CurrentFrameEventsState = this.m_NextFrameEventsState;
				this.m_NextFrameEventsState = currentFrameEventsState;
			}

			// Token: 0x060024D9 RID: 9433 RVA: 0x000858A4 File Offset: 0x00083AA4
			private void QueueEvent(EventBase evt, StylePropertyAnimationSystem.ElementPropertyPair epp)
			{
				evt.elementTarget = epp.element;
				Queue<EventBase> pooledQueue;
				bool flag = !this.m_NextFrameEventsState.elementPropertyQueuedEvents.TryGetValue(epp, out pooledQueue);
				if (flag)
				{
					pooledQueue = StylePropertyAnimationSystem.Values<T>.TransitionEventsFrameState.GetPooledQueue();
					this.m_NextFrameEventsState.elementPropertyQueuedEvents.Add(epp, pooledQueue);
				}
				pooledQueue.Enqueue(evt);
				bool flag2 = this.m_NextFrameEventsState.panel == null;
				if (flag2)
				{
					this.m_NextFrameEventsState.panel = epp.element.panel;
				}
				this.m_NextFrameEventsState.RegisterChange();
			}

			// Token: 0x060024DA RID: 9434 RVA: 0x00085930 File Offset: 0x00083B30
			private void ClearEventQueue(StylePropertyAnimationSystem.ElementPropertyPair epp)
			{
				Queue<EventBase> queue;
				bool flag = this.m_NextFrameEventsState.elementPropertyQueuedEvents.TryGetValue(epp, out queue);
				if (flag)
				{
					while (queue.Count > 0)
					{
						queue.Dequeue().Dispose();
						this.m_NextFrameEventsState.UnregisterChange();
					}
				}
			}

			// Token: 0x060024DB RID: 9435 RVA: 0x00085980 File Offset: 0x00083B80
			private void QueueTransitionRunEvent(VisualElement ve, int runningIndex)
			{
				bool flag = !ve.HasParentEventInterests(EventCategory.StyleTransition);
				if (!flag)
				{
					StylePropertyId stylePropertyId = this.running.properties[runningIndex];
					StylePropertyAnimationSystem.ElementPropertyPair elementPropertyPair = new StylePropertyAnimationSystem.ElementPropertyPair(ve, stylePropertyId);
					StylePropertyAnimationSystem.TransitionState transitionState;
					bool flag2 = this.m_NextFrameEventsState.elementPropertyStateDelta.TryGetValue(elementPropertyPair, out transitionState);
					if (flag2)
					{
						this.m_NextFrameEventsState.elementPropertyStateDelta[elementPropertyPair] = transitionState | StylePropertyAnimationSystem.TransitionState.Running;
					}
					else
					{
						this.m_NextFrameEventsState.elementPropertyStateDelta.Add(elementPropertyPair, StylePropertyAnimationSystem.TransitionState.Running);
					}
					ref StylePropertyAnimationSystem.Values<T>.TimingData ptr = ref this.running.timing[runningIndex];
					int num = ((ptr.delayMs < 0) ? Mathf.Min(Mathf.Max(-ptr.delayMs, 0), ptr.durationMs) : 0);
					TransitionRunEvent pooled = TransitionEventBase<TransitionRunEvent>.GetPooled(new StylePropertyName(stylePropertyId), (double)((float)num / 1000f));
					this.QueueEvent(pooled, elementPropertyPair);
				}
			}

			// Token: 0x060024DC RID: 9436 RVA: 0x00085A58 File Offset: 0x00083C58
			private void QueueTransitionStartEvent(VisualElement ve, int runningIndex)
			{
				bool flag = !ve.HasParentEventInterests(EventCategory.StyleTransition);
				if (!flag)
				{
					StylePropertyId stylePropertyId = this.running.properties[runningIndex];
					StylePropertyAnimationSystem.ElementPropertyPair elementPropertyPair = new StylePropertyAnimationSystem.ElementPropertyPair(ve, stylePropertyId);
					StylePropertyAnimationSystem.TransitionState transitionState;
					bool flag2 = this.m_NextFrameEventsState.elementPropertyStateDelta.TryGetValue(elementPropertyPair, out transitionState);
					if (flag2)
					{
						this.m_NextFrameEventsState.elementPropertyStateDelta[elementPropertyPair] = transitionState | StylePropertyAnimationSystem.TransitionState.Started;
					}
					else
					{
						this.m_NextFrameEventsState.elementPropertyStateDelta.Add(elementPropertyPair, StylePropertyAnimationSystem.TransitionState.Started);
					}
					ref StylePropertyAnimationSystem.Values<T>.TimingData ptr = ref this.running.timing[runningIndex];
					int num = ((ptr.delayMs < 0) ? Mathf.Min(Mathf.Max(-ptr.delayMs, 0), ptr.durationMs) : 0);
					TransitionStartEvent pooled = TransitionEventBase<TransitionStartEvent>.GetPooled(new StylePropertyName(stylePropertyId), (double)((float)num / 1000f));
					this.QueueEvent(pooled, elementPropertyPair);
				}
			}

			// Token: 0x060024DD RID: 9437 RVA: 0x00085B30 File Offset: 0x00083D30
			private void QueueTransitionEndEvent(VisualElement ve, int runningIndex)
			{
				bool flag = !ve.HasParentEventInterests(EventCategory.StyleTransition);
				if (!flag)
				{
					StylePropertyId stylePropertyId = this.running.properties[runningIndex];
					StylePropertyAnimationSystem.ElementPropertyPair elementPropertyPair = new StylePropertyAnimationSystem.ElementPropertyPair(ve, stylePropertyId);
					StylePropertyAnimationSystem.TransitionState transitionState;
					bool flag2 = this.m_NextFrameEventsState.elementPropertyStateDelta.TryGetValue(elementPropertyPair, out transitionState);
					if (flag2)
					{
						this.m_NextFrameEventsState.elementPropertyStateDelta[elementPropertyPair] = transitionState | StylePropertyAnimationSystem.TransitionState.Ended;
					}
					else
					{
						this.m_NextFrameEventsState.elementPropertyStateDelta.Add(elementPropertyPair, StylePropertyAnimationSystem.TransitionState.Ended);
					}
					ref StylePropertyAnimationSystem.Values<T>.TimingData ptr = ref this.running.timing[runningIndex];
					TransitionEndEvent pooled = TransitionEventBase<TransitionEndEvent>.GetPooled(new StylePropertyName(stylePropertyId), (double)((float)ptr.durationMs / 1000f));
					this.QueueEvent(pooled, elementPropertyPair);
				}
			}

			// Token: 0x060024DE RID: 9438 RVA: 0x00085BE8 File Offset: 0x00083DE8
			private void QueueTransitionCancelEvent(VisualElement ve, int runningIndex, long panelElapsedMs)
			{
				bool flag = !ve.HasParentEventInterests(EventCategory.StyleTransition);
				if (!flag)
				{
					StylePropertyId stylePropertyId = this.running.properties[runningIndex];
					StylePropertyAnimationSystem.ElementPropertyPair elementPropertyPair = new StylePropertyAnimationSystem.ElementPropertyPair(ve, stylePropertyId);
					StylePropertyAnimationSystem.TransitionState transitionState;
					bool flag2 = this.m_NextFrameEventsState.elementPropertyStateDelta.TryGetValue(elementPropertyPair, out transitionState);
					bool flag4;
					if (flag2)
					{
						bool flag3 = transitionState == StylePropertyAnimationSystem.TransitionState.None || (transitionState & StylePropertyAnimationSystem.TransitionState.Canceled) == StylePropertyAnimationSystem.TransitionState.Canceled;
						if (flag3)
						{
							this.m_NextFrameEventsState.elementPropertyStateDelta[elementPropertyPair] = StylePropertyAnimationSystem.TransitionState.Canceled;
							this.ClearEventQueue(elementPropertyPair);
							flag4 = true;
						}
						else
						{
							this.m_NextFrameEventsState.elementPropertyStateDelta[elementPropertyPair] = StylePropertyAnimationSystem.TransitionState.None;
							this.ClearEventQueue(elementPropertyPair);
							flag4 = false;
						}
					}
					else
					{
						this.m_NextFrameEventsState.elementPropertyStateDelta.Add(elementPropertyPair, StylePropertyAnimationSystem.TransitionState.Canceled);
						flag4 = true;
					}
					bool flag5 = !flag4;
					if (!flag5)
					{
						ref StylePropertyAnimationSystem.Values<T>.TimingData ptr = ref this.running.timing[runningIndex];
						long num = (ptr.isStarted ? (panelElapsedMs - ptr.startTimeMs) : 0L);
						bool flag6 = ptr.delayMs < 0;
						if (flag6)
						{
							num = (long)(-(long)ptr.delayMs) + num;
						}
						TransitionCancelEvent pooled = TransitionEventBase<TransitionCancelEvent>.GetPooled(new StylePropertyName(stylePropertyId), (double)((float)num / 1000f));
						this.QueueEvent(pooled, elementPropertyPair);
					}
				}
			}

			// Token: 0x060024DF RID: 9439 RVA: 0x00085D24 File Offset: 0x00083F24
			private void SendTransitionCancelEvent(VisualElement ve, int runningIndex, long panelElapsedMs)
			{
				bool flag = !ve.HasParentEventInterests(EventBase<TransitionCancelEvent>.EventCategory);
				if (!flag)
				{
					ref StylePropertyAnimationSystem.Values<T>.TimingData ptr = ref this.running.timing[runningIndex];
					StylePropertyId stylePropertyId = this.running.properties[runningIndex];
					long num = (ptr.isStarted ? (panelElapsedMs - ptr.startTimeMs) : 0L);
					bool flag2 = ptr.delayMs < 0;
					if (flag2)
					{
						num = (long)(-(long)ptr.delayMs) + num;
					}
					using (TransitionCancelEvent pooled = TransitionEventBase<TransitionCancelEvent>.GetPooled(new StylePropertyName(stylePropertyId), (double)((float)num / 1000f)))
					{
						pooled.elementTarget = ve;
						ve.SendEvent(pooled);
					}
				}
			}

			// Token: 0x060024E0 RID: 9440 RVA: 0x00085DE4 File Offset: 0x00083FE4
			public sealed override void CancelAllAnimations()
			{
				int count = this.running.count;
				bool flag = count > 0;
				if (flag)
				{
					using (new EventDispatcherGate(this.running.elements[0].panel.dispatcher))
					{
						for (int i = 0; i < count; i++)
						{
							VisualElement visualElement = this.running.elements[i];
							this.SendTransitionCancelEvent(visualElement, i, this.m_CurrentTimeMs);
							this.ForceComputedStyleEndValue(i);
							IStylePropertyAnimations styleAnimation = visualElement.styleAnimation;
							int num = styleAnimation.runningAnimationCount;
							styleAnimation.runningAnimationCount = num - 1;
						}
					}
					this.running.RemoveAll();
				}
				int count2 = this.completed.count;
				for (int j = 0; j < count2; j++)
				{
					VisualElement visualElement2 = this.completed.elements[j];
					IStylePropertyAnimations styleAnimation2 = visualElement2.styleAnimation;
					int num = styleAnimation2.completedAnimationCount;
					styleAnimation2.completedAnimationCount = num - 1;
				}
				this.completed.RemoveAll();
			}

			// Token: 0x060024E1 RID: 9441 RVA: 0x00085F0C File Offset: 0x0008410C
			public sealed override void CancelAllAnimations(VisualElement ve)
			{
				int count = this.running.count;
				bool flag = count > 0;
				if (flag)
				{
					using (new EventDispatcherGate(this.running.elements[0].panel.dispatcher))
					{
						for (int i = 0; i < count; i++)
						{
							bool flag2 = this.running.elements[i] == ve;
							if (flag2)
							{
								this.SendTransitionCancelEvent(ve, i, this.m_CurrentTimeMs);
								this.ForceComputedStyleEndValue(i);
								IStylePropertyAnimations styleAnimation = this.running.elements[i].styleAnimation;
								int num = styleAnimation.runningAnimationCount;
								styleAnimation.runningAnimationCount = num - 1;
							}
						}
					}
				}
				this.running.RemoveAll(ve);
				int count2 = this.completed.count;
				for (int j = 0; j < count2; j++)
				{
					bool flag3 = this.completed.elements[j] == ve;
					if (flag3)
					{
						IStylePropertyAnimations styleAnimation2 = this.completed.elements[j].styleAnimation;
						int num = styleAnimation2.completedAnimationCount;
						styleAnimation2.completedAnimationCount = num - 1;
					}
				}
				this.completed.RemoveAll(ve);
			}

			// Token: 0x060024E2 RID: 9442 RVA: 0x00086060 File Offset: 0x00084260
			public sealed override void CancelAnimation(VisualElement ve, StylePropertyId id)
			{
				int num;
				bool flag = this.running.IndexOf(ve, id, out num);
				if (flag)
				{
					this.QueueTransitionCancelEvent(ve, num, this.m_CurrentTimeMs);
					this.ForceComputedStyleEndValue(num);
					this.running.Remove(num);
					IStylePropertyAnimations styleAnimation = ve.styleAnimation;
					int num2 = styleAnimation.runningAnimationCount;
					styleAnimation.runningAnimationCount = num2 - 1;
				}
				int num3;
				bool flag2 = this.completed.IndexOf(ve, id, out num3);
				if (flag2)
				{
					this.completed.Remove(num3);
					IStylePropertyAnimations styleAnimation2 = ve.styleAnimation;
					int num2 = styleAnimation2.completedAnimationCount;
					styleAnimation2.completedAnimationCount = num2 - 1;
				}
			}

			// Token: 0x060024E3 RID: 9443 RVA: 0x000860F8 File Offset: 0x000842F8
			public sealed override bool HasRunningAnimation(VisualElement ve, StylePropertyId id)
			{
				int num;
				return this.running.IndexOf(ve, id, out num);
			}

			// Token: 0x060024E4 RID: 9444 RVA: 0x0008611C File Offset: 0x0008431C
			public sealed override void UpdateAnimation(VisualElement ve, StylePropertyId id)
			{
				int num;
				bool flag = this.running.IndexOf(ve, id, out num);
				if (flag)
				{
					this.UpdateComputedStyle(num);
				}
			}

			// Token: 0x060024E5 RID: 9445 RVA: 0x00086145 File Offset: 0x00084345
			public sealed override void GetAllAnimations(VisualElement ve, List<StylePropertyId> outPropertyIds)
			{
				this.running.GetActivePropertiesForElement(ve, outPropertyIds);
				this.completed.GetActivePropertiesForElement(ve, outPropertyIds);
			}

			// Token: 0x060024E6 RID: 9446 RVA: 0x00086164 File Offset: 0x00084364
			private float ComputeReversingShorteningFactor(int oldIndex)
			{
				ref StylePropertyAnimationSystem.Values<T>.TimingData ptr = ref this.running.timing[oldIndex];
				return Mathf.Clamp01(Mathf.Abs(1f - (1f - ptr.easedProgress) * ptr.reversingShorteningFactor));
			}

			// Token: 0x060024E7 RID: 9447 RVA: 0x000861AC File Offset: 0x000843AC
			private int ComputeReversingDuration(int newTransitionDurationMs, float newReversingShorteningFactor)
			{
				return Mathf.RoundToInt((float)newTransitionDurationMs * newReversingShorteningFactor);
			}

			// Token: 0x060024E8 RID: 9448 RVA: 0x000861C8 File Offset: 0x000843C8
			private int ComputeReversingDelay(int delayMs, float newReversingShorteningFactor)
			{
				return (delayMs < 0) ? Mathf.RoundToInt((float)delayMs * newReversingShorteningFactor) : delayMs;
			}

			// Token: 0x060024E9 RID: 9449 RVA: 0x000861EC File Offset: 0x000843EC
			public bool StartTransition(VisualElement owner, StylePropertyId prop, T startValue, T endValue, int durationMs, int delayMs, Func<float, float> easingCurve, long currentTimeMs)
			{
				long num = currentTimeMs + (long)delayMs;
				StylePropertyAnimationSystem.Values<T>.TimingData timingData = new StylePropertyAnimationSystem.Values<T>.TimingData
				{
					startTimeMs = num,
					durationMs = durationMs,
					easingCurve = easingCurve,
					reversingShorteningFactor = 1f,
					delayMs = delayMs
				};
				StylePropertyAnimationSystem.Values<T>.StyleData styleData = new StylePropertyAnimationSystem.Values<T>.StyleData
				{
					startValue = startValue,
					endValue = endValue,
					currentValue = startValue,
					reversingAdjustedStartValue = startValue
				};
				int num2 = Mathf.Max(0, durationMs) + delayMs;
				bool flag = !this.ConvertUnits(owner, prop, ref styleData.startValue, ref styleData.endValue);
				bool flag2;
				if (flag)
				{
					flag2 = false;
				}
				else
				{
					int num3;
					bool flag3 = this.completed.IndexOf(owner, prop, out num3);
					if (flag3)
					{
						bool flag4 = this.SameFunc(endValue, this.completed.style[num3]);
						if (flag4)
						{
							return false;
						}
						bool flag5 = num2 <= 0;
						if (flag5)
						{
							return false;
						}
						this.completed.Remove(num3);
						IStylePropertyAnimations styleAnimation = owner.styleAnimation;
						int num4 = styleAnimation.completedAnimationCount;
						styleAnimation.completedAnimationCount = num4 - 1;
					}
					int num5;
					bool flag6 = this.running.IndexOf(owner, prop, out num5);
					if (flag6)
					{
						bool flag7 = this.SameFunc(endValue, this.running.style[num5].endValue);
						if (flag7)
						{
							flag2 = false;
						}
						else
						{
							bool flag8 = this.SameFunc(endValue, this.running.style[num5].currentValue);
							if (flag8)
							{
								this.QueueTransitionCancelEvent(owner, num5, currentTimeMs);
								this.running.Remove(num5);
								IStylePropertyAnimations styleAnimation2 = owner.styleAnimation;
								int num4 = styleAnimation2.runningAnimationCount;
								styleAnimation2.runningAnimationCount = num4 - 1;
								flag2 = false;
							}
							else
							{
								bool flag9 = num2 <= 0;
								if (flag9)
								{
									this.QueueTransitionCancelEvent(owner, num5, currentTimeMs);
									this.running.Remove(num5);
									IStylePropertyAnimations styleAnimation3 = owner.styleAnimation;
									int num4 = styleAnimation3.runningAnimationCount;
									styleAnimation3.runningAnimationCount = num4 - 1;
									flag2 = false;
								}
								else
								{
									styleData.startValue = this.running.style[num5].currentValue;
									bool flag10 = !this.ConvertUnits(owner, prop, ref styleData.startValue, ref styleData.endValue);
									if (flag10)
									{
										this.QueueTransitionCancelEvent(owner, num5, currentTimeMs);
										this.running.Remove(num5);
										IStylePropertyAnimations styleAnimation4 = owner.styleAnimation;
										int num4 = styleAnimation4.runningAnimationCount;
										styleAnimation4.runningAnimationCount = num4 - 1;
										flag2 = false;
									}
									else
									{
										styleData.currentValue = styleData.startValue;
										bool flag11 = this.SameFunc(endValue, this.running.style[num5].reversingAdjustedStartValue);
										if (flag11)
										{
											float num6 = (timingData.reversingShorteningFactor = this.ComputeReversingShorteningFactor(num5));
											timingData.startTimeMs = currentTimeMs + (long)this.ComputeReversingDelay(delayMs, num6);
											timingData.durationMs = this.ComputeReversingDuration(durationMs, num6);
											styleData.reversingAdjustedStartValue = this.running.style[num5].endValue;
										}
										this.running.timing[num5].isStarted = false;
										this.QueueTransitionCancelEvent(owner, num5, currentTimeMs);
										this.QueueTransitionRunEvent(owner, num5);
										this.running.Replace(num5, timingData, styleData);
										flag2 = true;
									}
								}
							}
						}
					}
					else
					{
						bool flag12 = num2 <= 0;
						if (flag12)
						{
							flag2 = false;
						}
						else
						{
							bool flag13 = this.SameFunc(startValue, endValue);
							if (flag13)
							{
								flag2 = false;
							}
							else
							{
								this.running.Add(owner, prop, timingData, styleData);
								IStylePropertyAnimations styleAnimation5 = owner.styleAnimation;
								int num4 = styleAnimation5.runningAnimationCount;
								styleAnimation5.runningAnimationCount = num4 + 1;
								this.QueueTransitionRunEvent(owner, this.running.count - 1);
								flag2 = true;
							}
						}
					}
				}
				return flag2;
			}

			// Token: 0x060024EA RID: 9450 RVA: 0x000865D4 File Offset: 0x000847D4
			private void ForceComputedStyleEndValue(int runningIndex)
			{
				ref StylePropertyAnimationSystem.Values<T>.StyleData ptr = ref this.running.style[runningIndex];
				ptr.currentValue = ptr.endValue;
				this.UpdateComputedStyle(runningIndex);
			}

			// Token: 0x060024EB RID: 9451 RVA: 0x00086608 File Offset: 0x00084808
			public sealed override void Update(long currentTimeMs)
			{
				this.m_CurrentTimeMs = currentTimeMs;
				this.UpdateProgress(currentTimeMs);
				this.UpdateValues();
				this.UpdateComputedStyle();
				bool flag = this.m_NextFrameEventsState.StateChanged();
				if (flag)
				{
					this.ProcessEventQueue();
				}
			}

			// Token: 0x060024EC RID: 9452 RVA: 0x0008664C File Offset: 0x0008484C
			private void ProcessEventQueue()
			{
				this.SwapFrameStates();
				IPanel panel = this.m_CurrentFrameEventsState.panel;
				EventDispatcher eventDispatcher = ((panel != null) ? panel.dispatcher : null);
				using (new EventDispatcherGate(eventDispatcher))
				{
					foreach (KeyValuePair<StylePropertyAnimationSystem.ElementPropertyPair, Queue<EventBase>> keyValuePair in this.m_CurrentFrameEventsState.elementPropertyQueuedEvents)
					{
						StylePropertyAnimationSystem.ElementPropertyPair key = keyValuePair.Key;
						Queue<EventBase> value = keyValuePair.Value;
						VisualElement element = keyValuePair.Key.element;
						while (value.Count > 0)
						{
							EventBase eventBase = value.Dequeue();
							element.SendEvent(eventBase);
							eventBase.Dispose();
						}
					}
					this.m_CurrentFrameEventsState.Clear();
				}
			}

			// Token: 0x060024ED RID: 9453 RVA: 0x00086744 File Offset: 0x00084944
			private void UpdateProgress(long currentTimeMs)
			{
				int num = this.running.count;
				bool flag = num > 0;
				if (flag)
				{
					for (int i = 0; i < num; i++)
					{
						ref StylePropertyAnimationSystem.Values<T>.TimingData ptr = ref this.running.timing[i];
						bool flag2 = currentTimeMs < ptr.startTimeMs;
						if (flag2)
						{
							ptr.easedProgress = 0f;
						}
						else
						{
							bool flag3 = currentTimeMs >= ptr.startTimeMs + (long)ptr.durationMs;
							if (flag3)
							{
								ref StylePropertyAnimationSystem.Values<T>.StyleData ptr2 = ref this.running.style[i];
								ref VisualElement ptr3 = ref this.running.elements[i];
								ptr2.currentValue = ptr2.endValue;
								this.UpdateComputedStyle(i);
								this.completed.Add(ptr3, this.running.properties[i], StylePropertyAnimationSystem.Values<T>.EmptyData.Default, ptr2.endValue);
								IStylePropertyAnimations styleAnimation = ptr3.styleAnimation;
								int num2 = styleAnimation.runningAnimationCount;
								styleAnimation.runningAnimationCount = num2 - 1;
								IStylePropertyAnimations styleAnimation2 = ptr3.styleAnimation;
								num2 = styleAnimation2.completedAnimationCount;
								styleAnimation2.completedAnimationCount = num2 + 1;
								this.QueueTransitionEndEvent(ptr3, i);
								this.running.Remove(i);
								i--;
								num--;
							}
							else
							{
								bool flag4 = !ptr.isStarted;
								if (flag4)
								{
									ptr.isStarted = true;
									this.QueueTransitionStartEvent(this.running.elements[i], i);
								}
								float num3 = (float)(currentTimeMs - ptr.startTimeMs) / (float)ptr.durationMs;
								ptr.easedProgress = ptr.easingCurve(num3);
							}
						}
					}
				}
			}

			// Token: 0x0400107C RID: 4220
			private long m_CurrentTimeMs = 0L;

			// Token: 0x0400107D RID: 4221
			private StylePropertyAnimationSystem.Values<T>.TransitionEventsFrameState m_CurrentFrameEventsState = new StylePropertyAnimationSystem.Values<T>.TransitionEventsFrameState();

			// Token: 0x0400107E RID: 4222
			private StylePropertyAnimationSystem.Values<T>.TransitionEventsFrameState m_NextFrameEventsState = new StylePropertyAnimationSystem.Values<T>.TransitionEventsFrameState();

			// Token: 0x0400107F RID: 4223
			public StylePropertyAnimationSystem.AnimationDataSet<StylePropertyAnimationSystem.Values<T>.TimingData, StylePropertyAnimationSystem.Values<T>.StyleData> running;

			// Token: 0x04001080 RID: 4224
			public StylePropertyAnimationSystem.AnimationDataSet<StylePropertyAnimationSystem.Values<T>.EmptyData, T> completed;

			// Token: 0x020004A9 RID: 1193
			private class TransitionEventsFrameState
			{
				// Token: 0x060024EE RID: 9454 RVA: 0x000868E8 File Offset: 0x00084AE8
				public static Queue<EventBase> GetPooledQueue()
				{
					return StylePropertyAnimationSystem.Values<T>.TransitionEventsFrameState.k_EventQueuePool.Get();
				}

				// Token: 0x060024EF RID: 9455 RVA: 0x00086904 File Offset: 0x00084B04
				public void RegisterChange()
				{
					this.m_ChangesCount++;
				}

				// Token: 0x060024F0 RID: 9456 RVA: 0x00086915 File Offset: 0x00084B15
				public void UnregisterChange()
				{
					this.m_ChangesCount--;
				}

				// Token: 0x060024F1 RID: 9457 RVA: 0x00086928 File Offset: 0x00084B28
				public bool StateChanged()
				{
					return this.m_ChangesCount > 0;
				}

				// Token: 0x060024F2 RID: 9458 RVA: 0x00086944 File Offset: 0x00084B44
				public void Clear()
				{
					foreach (KeyValuePair<StylePropertyAnimationSystem.ElementPropertyPair, Queue<EventBase>> keyValuePair in this.elementPropertyQueuedEvents)
					{
						keyValuePair.Value.Clear();
						StylePropertyAnimationSystem.Values<T>.TransitionEventsFrameState.k_EventQueuePool.Release(keyValuePair.Value);
					}
					this.elementPropertyQueuedEvents.Clear();
					this.elementPropertyStateDelta.Clear();
					this.panel = null;
					this.m_ChangesCount = 0;
				}

				// Token: 0x04001081 RID: 4225
				private static readonly ObjectPool<Queue<EventBase>> k_EventQueuePool = new ObjectPool<Queue<EventBase>>(() => new Queue<EventBase>(4), null, null, null, true, 10, 10000);

				// Token: 0x04001082 RID: 4226
				public readonly Dictionary<StylePropertyAnimationSystem.ElementPropertyPair, StylePropertyAnimationSystem.TransitionState> elementPropertyStateDelta = new Dictionary<StylePropertyAnimationSystem.ElementPropertyPair, StylePropertyAnimationSystem.TransitionState>(StylePropertyAnimationSystem.ElementPropertyPair.Comparer);

				// Token: 0x04001083 RID: 4227
				public readonly Dictionary<StylePropertyAnimationSystem.ElementPropertyPair, Queue<EventBase>> elementPropertyQueuedEvents = new Dictionary<StylePropertyAnimationSystem.ElementPropertyPair, Queue<EventBase>>(StylePropertyAnimationSystem.ElementPropertyPair.Comparer);

				// Token: 0x04001084 RID: 4228
				public IPanel panel;

				// Token: 0x04001085 RID: 4229
				private int m_ChangesCount;
			}

			// Token: 0x020004AB RID: 1195
			public struct TimingData
			{
				// Token: 0x04001087 RID: 4231
				public long startTimeMs;

				// Token: 0x04001088 RID: 4232
				public int durationMs;

				// Token: 0x04001089 RID: 4233
				public Func<float, float> easingCurve;

				// Token: 0x0400108A RID: 4234
				public float easedProgress;

				// Token: 0x0400108B RID: 4235
				public float reversingShorteningFactor;

				// Token: 0x0400108C RID: 4236
				public bool isStarted;

				// Token: 0x0400108D RID: 4237
				public int delayMs;
			}

			// Token: 0x020004AC RID: 1196
			public struct StyleData
			{
				// Token: 0x0400108E RID: 4238
				public T startValue;

				// Token: 0x0400108F RID: 4239
				public T endValue;

				// Token: 0x04001090 RID: 4240
				public T reversingAdjustedStartValue;

				// Token: 0x04001091 RID: 4241
				public T currentValue;
			}

			// Token: 0x020004AD RID: 1197
			public struct EmptyData
			{
				// Token: 0x04001092 RID: 4242
				public static StylePropertyAnimationSystem.Values<T>.EmptyData Default = default(StylePropertyAnimationSystem.Values<T>.EmptyData);
			}
		}

		// Token: 0x020004AE RID: 1198
		private class ValuesFloat : StylePropertyAnimationSystem.Values<float>
		{
			// Token: 0x17000973 RID: 2419
			// (get) Token: 0x060024F9 RID: 9465 RVA: 0x00086A4D File Offset: 0x00084C4D
			public override Func<float, float, bool> SameFunc { get; } = new Func<float, float, bool>(StylePropertyAnimationSystem.ValuesFloat.IsSame);

			// Token: 0x060024FA RID: 9466 RVA: 0x00086A55 File Offset: 0x00084C55
			private static bool IsSame(float a, float b)
			{
				return Mathf.Approximately(a, b);
			}

			// Token: 0x060024FB RID: 9467 RVA: 0x00086A5E File Offset: 0x00084C5E
			private static float Lerp(float a, float b, float t)
			{
				return Mathf.LerpUnclamped(a, b, t);
			}

			// Token: 0x060024FC RID: 9468 RVA: 0x00086A68 File Offset: 0x00084C68
			protected sealed override void UpdateValues()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					ref StylePropertyAnimationSystem.Values<float>.TimingData ptr = ref this.running.timing[i];
					ref StylePropertyAnimationSystem.Values<float>.StyleData ptr2 = ref this.running.style[i];
					ptr2.currentValue = StylePropertyAnimationSystem.ValuesFloat.Lerp(ptr2.startValue, ptr2.endValue, ptr.easedProgress);
				}
			}

			// Token: 0x060024FD RID: 9469 RVA: 0x00086AD8 File Offset: 0x00084CD8
			protected sealed override void UpdateComputedStyle()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
				}
			}

			// Token: 0x060024FE RID: 9470 RVA: 0x00086B4C File Offset: 0x00084D4C
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}
		}

		// Token: 0x020004AF RID: 1199
		private class ValuesInt : StylePropertyAnimationSystem.Values<int>
		{
			// Token: 0x17000974 RID: 2420
			// (get) Token: 0x06002500 RID: 9472 RVA: 0x00086BBD File Offset: 0x00084DBD
			public override Func<int, int, bool> SameFunc { get; } = new Func<int, int, bool>(StylePropertyAnimationSystem.ValuesInt.IsSame);

			// Token: 0x06002501 RID: 9473 RVA: 0x00086BC5 File Offset: 0x00084DC5
			private static bool IsSame(int a, int b)
			{
				return a == b;
			}

			// Token: 0x06002502 RID: 9474 RVA: 0x00086BCB File Offset: 0x00084DCB
			private static int Lerp(int a, int b, float t)
			{
				return Mathf.RoundToInt(Mathf.LerpUnclamped((float)a, (float)b, t));
			}

			// Token: 0x06002503 RID: 9475 RVA: 0x00086BDC File Offset: 0x00084DDC
			protected sealed override void UpdateValues()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					ref StylePropertyAnimationSystem.Values<int>.TimingData ptr = ref this.running.timing[i];
					ref StylePropertyAnimationSystem.Values<int>.StyleData ptr2 = ref this.running.style[i];
					ptr2.currentValue = StylePropertyAnimationSystem.ValuesInt.Lerp(ptr2.startValue, ptr2.endValue, ptr.easedProgress);
				}
			}

			// Token: 0x06002504 RID: 9476 RVA: 0x00086C4C File Offset: 0x00084E4C
			protected sealed override void UpdateComputedStyle()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
				}
			}

			// Token: 0x06002505 RID: 9477 RVA: 0x00086CC0 File Offset: 0x00084EC0
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}
		}

		// Token: 0x020004B0 RID: 1200
		private class ValuesLength : StylePropertyAnimationSystem.Values<Length>
		{
			// Token: 0x17000975 RID: 2421
			// (get) Token: 0x06002507 RID: 9479 RVA: 0x00086D31 File Offset: 0x00084F31
			public override Func<Length, Length, bool> SameFunc { get; } = new Func<Length, Length, bool>(StylePropertyAnimationSystem.ValuesLength.IsSame);

			// Token: 0x06002508 RID: 9480 RVA: 0x00086D39 File Offset: 0x00084F39
			private static bool IsSame(Length a, Length b)
			{
				return a.unit == b.unit && Mathf.Approximately(a.value, b.value);
			}

			// Token: 0x06002509 RID: 9481 RVA: 0x00086D64 File Offset: 0x00084F64
			protected sealed override bool ConvertUnits(VisualElement owner, StylePropertyId prop, ref Length a, ref Length b)
			{
				return owner.TryConvertLengthUnits(prop, ref a, ref b, 0);
			}

			// Token: 0x0600250A RID: 9482 RVA: 0x00086D81 File Offset: 0x00084F81
			internal static Length Lerp(Length a, Length b, float t)
			{
				return new Length(Mathf.LerpUnclamped(a.value, b.value, t), b.unit);
			}

			// Token: 0x0600250B RID: 9483 RVA: 0x00086DA4 File Offset: 0x00084FA4
			protected sealed override void UpdateValues()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					ref StylePropertyAnimationSystem.Values<Length>.TimingData ptr = ref this.running.timing[i];
					ref StylePropertyAnimationSystem.Values<Length>.StyleData ptr2 = ref this.running.style[i];
					ptr2.currentValue = StylePropertyAnimationSystem.ValuesLength.Lerp(ptr2.startValue, ptr2.endValue, ptr.easedProgress);
				}
			}

			// Token: 0x0600250C RID: 9484 RVA: 0x00086E14 File Offset: 0x00085014
			protected sealed override void UpdateComputedStyle()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
				}
			}

			// Token: 0x0600250D RID: 9485 RVA: 0x00086E88 File Offset: 0x00085088
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}
		}

		// Token: 0x020004B1 RID: 1201
		private class ValuesColor : StylePropertyAnimationSystem.Values<Color>
		{
			// Token: 0x17000976 RID: 2422
			// (get) Token: 0x0600250F RID: 9487 RVA: 0x00086EF9 File Offset: 0x000850F9
			public override Func<Color, Color, bool> SameFunc { get; } = new Func<Color, Color, bool>(StylePropertyAnimationSystem.ValuesColor.IsSame);

			// Token: 0x06002510 RID: 9488 RVA: 0x00086F04 File Offset: 0x00085104
			private static bool IsSame(Color c, Color d)
			{
				return Mathf.Approximately(c.r, d.r) && Mathf.Approximately(c.g, d.g) && Mathf.Approximately(c.b, d.b) && Mathf.Approximately(c.a, d.a);
			}

			// Token: 0x06002511 RID: 9489 RVA: 0x00086F5E File Offset: 0x0008515E
			private static Color Lerp(Color a, Color b, float t)
			{
				return Color.LerpUnclamped(a, b, t);
			}

			// Token: 0x06002512 RID: 9490 RVA: 0x00086F68 File Offset: 0x00085168
			protected sealed override void UpdateValues()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					ref StylePropertyAnimationSystem.Values<Color>.TimingData ptr = ref this.running.timing[i];
					ref StylePropertyAnimationSystem.Values<Color>.StyleData ptr2 = ref this.running.style[i];
					ptr2.currentValue = StylePropertyAnimationSystem.ValuesColor.Lerp(ptr2.startValue, ptr2.endValue, ptr.easedProgress);
				}
			}

			// Token: 0x06002513 RID: 9491 RVA: 0x00086FD8 File Offset: 0x000851D8
			protected sealed override void UpdateComputedStyle()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
				}
			}

			// Token: 0x06002514 RID: 9492 RVA: 0x0008704C File Offset: 0x0008524C
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}
		}

		// Token: 0x020004B2 RID: 1202
		private abstract class ValuesDiscrete<T> : StylePropertyAnimationSystem.Values<T>
		{
			// Token: 0x17000977 RID: 2423
			// (get) Token: 0x06002516 RID: 9494 RVA: 0x000870BD File Offset: 0x000852BD
			public override Func<T, T, bool> SameFunc { get; } = new Func<T, T, bool>(StylePropertyAnimationSystem.ValuesDiscrete<T>.IsSame);

			// Token: 0x06002517 RID: 9495 RVA: 0x000870C5 File Offset: 0x000852C5
			private static bool IsSame(T a, T b)
			{
				return EqualityComparer<T>.Default.Equals(a, b);
			}

			// Token: 0x06002518 RID: 9496 RVA: 0x000870D3 File Offset: 0x000852D3
			private static T Lerp(T a, T b, float t)
			{
				return (t < 0.5f) ? a : b;
			}

			// Token: 0x06002519 RID: 9497 RVA: 0x000870E4 File Offset: 0x000852E4
			protected sealed override void UpdateValues()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					ref StylePropertyAnimationSystem.Values<T>.TimingData ptr = ref this.running.timing[i];
					ref StylePropertyAnimationSystem.Values<T>.StyleData ptr2 = ref this.running.style[i];
					ptr2.currentValue = StylePropertyAnimationSystem.ValuesDiscrete<T>.Lerp(ptr2.startValue, ptr2.endValue, ptr.easedProgress);
				}
			}
		}

		// Token: 0x020004B3 RID: 1203
		private class ValuesEnum : StylePropertyAnimationSystem.ValuesDiscrete<int>
		{
			// Token: 0x0600251B RID: 9499 RVA: 0x00087170 File Offset: 0x00085370
			protected sealed override void UpdateComputedStyle()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
				}
			}

			// Token: 0x0600251C RID: 9500 RVA: 0x000871E4 File Offset: 0x000853E4
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}
		}

		// Token: 0x020004B4 RID: 1204
		private class ValuesBackground : StylePropertyAnimationSystem.ValuesDiscrete<Background>
		{
			// Token: 0x0600251E RID: 9502 RVA: 0x00087244 File Offset: 0x00085444
			protected sealed override void UpdateComputedStyle()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
				}
			}

			// Token: 0x0600251F RID: 9503 RVA: 0x000872B8 File Offset: 0x000854B8
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}
		}

		// Token: 0x020004B5 RID: 1205
		private class ValuesFontDefinition : StylePropertyAnimationSystem.ValuesDiscrete<FontDefinition>
		{
			// Token: 0x06002521 RID: 9505 RVA: 0x00087318 File Offset: 0x00085518
			protected sealed override void UpdateComputedStyle()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
				}
			}

			// Token: 0x06002522 RID: 9506 RVA: 0x0008738C File Offset: 0x0008558C
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}
		}

		// Token: 0x020004B6 RID: 1206
		private class ValuesFont : StylePropertyAnimationSystem.ValuesDiscrete<Font>
		{
			// Token: 0x06002524 RID: 9508 RVA: 0x000873EC File Offset: 0x000855EC
			protected sealed override void UpdateComputedStyle()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
				}
			}

			// Token: 0x06002525 RID: 9509 RVA: 0x00087460 File Offset: 0x00085660
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}
		}

		// Token: 0x020004B7 RID: 1207
		private class ValuesTextShadow : StylePropertyAnimationSystem.Values<TextShadow>
		{
			// Token: 0x17000978 RID: 2424
			// (get) Token: 0x06002527 RID: 9511 RVA: 0x000874BF File Offset: 0x000856BF
			public override Func<TextShadow, TextShadow, bool> SameFunc { get; } = new Func<TextShadow, TextShadow, bool>(StylePropertyAnimationSystem.ValuesTextShadow.IsSame);

			// Token: 0x06002528 RID: 9512 RVA: 0x000874C7 File Offset: 0x000856C7
			private static bool IsSame(TextShadow a, TextShadow b)
			{
				return a == b;
			}

			// Token: 0x06002529 RID: 9513 RVA: 0x000874D0 File Offset: 0x000856D0
			private static TextShadow Lerp(TextShadow a, TextShadow b, float t)
			{
				return TextShadow.LerpUnclamped(a, b, t);
			}

			// Token: 0x0600252A RID: 9514 RVA: 0x000874DC File Offset: 0x000856DC
			protected sealed override void UpdateValues()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					ref StylePropertyAnimationSystem.Values<TextShadow>.TimingData ptr = ref this.running.timing[i];
					ref StylePropertyAnimationSystem.Values<TextShadow>.StyleData ptr2 = ref this.running.style[i];
					ptr2.currentValue = StylePropertyAnimationSystem.ValuesTextShadow.Lerp(ptr2.startValue, ptr2.endValue, ptr.easedProgress);
				}
			}

			// Token: 0x0600252B RID: 9515 RVA: 0x0008754C File Offset: 0x0008574C
			protected sealed override void UpdateComputedStyle()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
				}
			}

			// Token: 0x0600252C RID: 9516 RVA: 0x000875C0 File Offset: 0x000857C0
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}
		}

		// Token: 0x020004B8 RID: 1208
		private class ValuesScale : StylePropertyAnimationSystem.Values<Scale>
		{
			// Token: 0x17000979 RID: 2425
			// (get) Token: 0x0600252E RID: 9518 RVA: 0x00087631 File Offset: 0x00085831
			public override Func<Scale, Scale, bool> SameFunc { get; } = new Func<Scale, Scale, bool>(StylePropertyAnimationSystem.ValuesScale.IsSame);

			// Token: 0x0600252F RID: 9519 RVA: 0x00087639 File Offset: 0x00085839
			private static bool IsSame(Scale a, Scale b)
			{
				return a == b;
			}

			// Token: 0x06002530 RID: 9520 RVA: 0x00087644 File Offset: 0x00085844
			protected sealed override void UpdateComputedStyle()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
				}
			}

			// Token: 0x06002531 RID: 9521 RVA: 0x000876B8 File Offset: 0x000858B8
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}

			// Token: 0x06002532 RID: 9522 RVA: 0x0008770E File Offset: 0x0008590E
			private static Scale Lerp(Scale a, Scale b, float t)
			{
				return new Scale(Vector3.LerpUnclamped(a.value, b.value, t));
			}

			// Token: 0x06002533 RID: 9523 RVA: 0x0008772C File Offset: 0x0008592C
			protected sealed override void UpdateValues()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					ref StylePropertyAnimationSystem.Values<Scale>.TimingData ptr = ref this.running.timing[i];
					ref StylePropertyAnimationSystem.Values<Scale>.StyleData ptr2 = ref this.running.style[i];
					ptr2.currentValue = StylePropertyAnimationSystem.ValuesScale.Lerp(ptr2.startValue, ptr2.endValue, ptr.easedProgress);
				}
			}
		}

		// Token: 0x020004B9 RID: 1209
		private class ValuesRotate : StylePropertyAnimationSystem.Values<Rotate>
		{
			// Token: 0x1700097A RID: 2426
			// (get) Token: 0x06002535 RID: 9525 RVA: 0x000877B6 File Offset: 0x000859B6
			public override Func<Rotate, Rotate, bool> SameFunc { get; } = new Func<Rotate, Rotate, bool>(StylePropertyAnimationSystem.ValuesRotate.IsSame);

			// Token: 0x06002536 RID: 9526 RVA: 0x000877BE File Offset: 0x000859BE
			private static bool IsSame(Rotate a, Rotate b)
			{
				return a == b;
			}

			// Token: 0x06002537 RID: 9527 RVA: 0x000877C8 File Offset: 0x000859C8
			protected sealed override void UpdateComputedStyle()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
				}
			}

			// Token: 0x06002538 RID: 9528 RVA: 0x0008783C File Offset: 0x00085A3C
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}

			// Token: 0x06002539 RID: 9529 RVA: 0x00087894 File Offset: 0x00085A94
			private static Rotate Lerp(Rotate a, Rotate b, float t)
			{
				return new Rotate(Mathf.LerpUnclamped(a.angle.ToDegrees(), b.angle.ToDegrees(), t));
			}

			// Token: 0x0600253A RID: 9530 RVA: 0x000878D0 File Offset: 0x00085AD0
			protected sealed override void UpdateValues()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					ref StylePropertyAnimationSystem.Values<Rotate>.TimingData ptr = ref this.running.timing[i];
					ref StylePropertyAnimationSystem.Values<Rotate>.StyleData ptr2 = ref this.running.style[i];
					ptr2.currentValue = StylePropertyAnimationSystem.ValuesRotate.Lerp(ptr2.startValue, ptr2.endValue, ptr.easedProgress);
				}
			}
		}

		// Token: 0x020004BA RID: 1210
		private class ValuesTranslate : StylePropertyAnimationSystem.Values<Translate>
		{
			// Token: 0x1700097B RID: 2427
			// (get) Token: 0x0600253C RID: 9532 RVA: 0x0008795A File Offset: 0x00085B5A
			public override Func<Translate, Translate, bool> SameFunc { get; } = new Func<Translate, Translate, bool>(StylePropertyAnimationSystem.ValuesTranslate.IsSame);

			// Token: 0x0600253D RID: 9533 RVA: 0x00087962 File Offset: 0x00085B62
			private static bool IsSame(Translate a, Translate b)
			{
				return a == b;
			}

			// Token: 0x0600253E RID: 9534 RVA: 0x0008796C File Offset: 0x00085B6C
			protected sealed override bool ConvertUnits(VisualElement owner, StylePropertyId prop, ref Translate a, ref Translate b)
			{
				return owner.TryConvertTranslateUnits(ref a, ref b);
			}

			// Token: 0x0600253F RID: 9535 RVA: 0x00087988 File Offset: 0x00085B88
			protected sealed override void UpdateComputedStyle()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
				}
			}

			// Token: 0x06002540 RID: 9536 RVA: 0x000879FC File Offset: 0x00085BFC
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}

			// Token: 0x06002541 RID: 9537 RVA: 0x00087A54 File Offset: 0x00085C54
			private static Translate Lerp(Translate a, Translate b, float t)
			{
				return new Translate(StylePropertyAnimationSystem.ValuesLength.Lerp(a.x, b.x, t), StylePropertyAnimationSystem.ValuesLength.Lerp(a.y, b.y, t), Mathf.Lerp(a.z, b.z, t));
			}

			// Token: 0x06002542 RID: 9538 RVA: 0x00087AA4 File Offset: 0x00085CA4
			protected sealed override void UpdateValues()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					ref StylePropertyAnimationSystem.Values<Translate>.TimingData ptr = ref this.running.timing[i];
					ref StylePropertyAnimationSystem.Values<Translate>.StyleData ptr2 = ref this.running.style[i];
					ptr2.currentValue = StylePropertyAnimationSystem.ValuesTranslate.Lerp(ptr2.startValue, ptr2.endValue, ptr.easedProgress);
				}
			}
		}

		// Token: 0x020004BB RID: 1211
		private class ValuesTransformOrigin : StylePropertyAnimationSystem.Values<TransformOrigin>
		{
			// Token: 0x1700097C RID: 2428
			// (get) Token: 0x06002544 RID: 9540 RVA: 0x00087B2E File Offset: 0x00085D2E
			public override Func<TransformOrigin, TransformOrigin, bool> SameFunc { get; } = new Func<TransformOrigin, TransformOrigin, bool>(StylePropertyAnimationSystem.ValuesTransformOrigin.IsSame);

			// Token: 0x06002545 RID: 9541 RVA: 0x00087B36 File Offset: 0x00085D36
			private static bool IsSame(TransformOrigin a, TransformOrigin b)
			{
				return a == b;
			}

			// Token: 0x06002546 RID: 9542 RVA: 0x00087B40 File Offset: 0x00085D40
			protected sealed override bool ConvertUnits(VisualElement owner, StylePropertyId prop, ref TransformOrigin a, ref TransformOrigin b)
			{
				return owner.TryConvertTransformOriginUnits(ref a, ref b);
			}

			// Token: 0x06002547 RID: 9543 RVA: 0x00087B5C File Offset: 0x00085D5C
			protected sealed override void UpdateComputedStyle()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
				}
			}

			// Token: 0x06002548 RID: 9544 RVA: 0x00087BD0 File Offset: 0x00085DD0
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}

			// Token: 0x06002549 RID: 9545 RVA: 0x00087C28 File Offset: 0x00085E28
			private static TransformOrigin Lerp(TransformOrigin a, TransformOrigin b, float t)
			{
				return new TransformOrigin(StylePropertyAnimationSystem.ValuesLength.Lerp(a.x, b.x, t), StylePropertyAnimationSystem.ValuesLength.Lerp(a.y, b.y, t), Mathf.Lerp(a.z, b.z, t));
			}

			// Token: 0x0600254A RID: 9546 RVA: 0x00087C78 File Offset: 0x00085E78
			protected sealed override void UpdateValues()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					ref StylePropertyAnimationSystem.Values<TransformOrigin>.TimingData ptr = ref this.running.timing[i];
					ref StylePropertyAnimationSystem.Values<TransformOrigin>.StyleData ptr2 = ref this.running.style[i];
					ptr2.currentValue = StylePropertyAnimationSystem.ValuesTransformOrigin.Lerp(ptr2.startValue, ptr2.endValue, ptr.easedProgress);
				}
			}
		}

		// Token: 0x020004BC RID: 1212
		private class ValuesBackgroundPosition : StylePropertyAnimationSystem.ValuesDiscrete<BackgroundPosition>
		{
			// Token: 0x0600254C RID: 9548 RVA: 0x00087D04 File Offset: 0x00085F04
			protected sealed override void UpdateComputedStyle()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
				}
			}

			// Token: 0x0600254D RID: 9549 RVA: 0x00087D78 File Offset: 0x00085F78
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}
		}

		// Token: 0x020004BD RID: 1213
		private class ValuesBackgroundRepeat : StylePropertyAnimationSystem.ValuesDiscrete<BackgroundRepeat>
		{
			// Token: 0x0600254F RID: 9551 RVA: 0x00087DD8 File Offset: 0x00085FD8
			protected sealed override void UpdateComputedStyle()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
				}
			}

			// Token: 0x06002550 RID: 9552 RVA: 0x00087E4C File Offset: 0x0008604C
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}
		}

		// Token: 0x020004BE RID: 1214
		private class ValuesBackgroundSize : StylePropertyAnimationSystem.Values<BackgroundSize>
		{
			// Token: 0x1700097D RID: 2429
			// (get) Token: 0x06002552 RID: 9554 RVA: 0x00087EAB File Offset: 0x000860AB
			public override Func<BackgroundSize, BackgroundSize, bool> SameFunc { get; } = new Func<BackgroundSize, BackgroundSize, bool>(StylePropertyAnimationSystem.ValuesBackgroundSize.IsSame);

			// Token: 0x06002553 RID: 9555 RVA: 0x00087EB3 File Offset: 0x000860B3
			private static bool IsSame(BackgroundSize a, BackgroundSize b)
			{
				return a == b;
			}

			// Token: 0x06002554 RID: 9556 RVA: 0x00087EBC File Offset: 0x000860BC
			protected sealed override bool ConvertUnits(VisualElement owner, StylePropertyId prop, ref BackgroundSize a, ref BackgroundSize b)
			{
				return owner.TryConvertBackgroundSizeUnits(ref a, ref b);
			}

			// Token: 0x06002555 RID: 9557 RVA: 0x00087ED8 File Offset: 0x000860D8
			protected sealed override void UpdateComputedStyle()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
				}
			}

			// Token: 0x06002556 RID: 9558 RVA: 0x00087F4C File Offset: 0x0008614C
			protected sealed override void UpdateComputedStyle(int i)
			{
				this.running.elements[i].computedStyle.ApplyPropertyAnimation(this.running.elements[i], this.running.properties[i], this.running.style[i].currentValue);
			}

			// Token: 0x06002557 RID: 9559 RVA: 0x00087FA2 File Offset: 0x000861A2
			private static BackgroundSize Lerp(BackgroundSize a, BackgroundSize b, float t)
			{
				return new BackgroundSize(StylePropertyAnimationSystem.ValuesLength.Lerp(a.x, b.x, t), StylePropertyAnimationSystem.ValuesLength.Lerp(a.y, b.y, t));
			}

			// Token: 0x06002558 RID: 9560 RVA: 0x00087FD4 File Offset: 0x000861D4
			protected sealed override void UpdateValues()
			{
				int count = this.running.count;
				for (int i = 0; i < count; i++)
				{
					ref StylePropertyAnimationSystem.Values<BackgroundSize>.TimingData ptr = ref this.running.timing[i];
					ref StylePropertyAnimationSystem.Values<BackgroundSize>.StyleData ptr2 = ref this.running.style[i];
					ptr2.currentValue = StylePropertyAnimationSystem.ValuesBackgroundSize.Lerp(ptr2.startValue, ptr2.endValue, ptr.easedProgress);
				}
			}
		}
	}
}
