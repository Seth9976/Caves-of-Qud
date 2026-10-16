using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements
{
	// Token: 0x020002AA RID: 682
	[EventCategory(EventCategory.StyleTransition)]
	public abstract class TransitionEventBase<T> : EventBase<T>, ITransitionEvent where T : TransitionEventBase<T>, new()
	{
		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06001393 RID: 5011 RVA: 0x0004F0B3 File Offset: 0x0004D2B3
		public StylePropertyNameCollection stylePropertyNames { get; }

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06001394 RID: 5012 RVA: 0x0004F0BB File Offset: 0x0004D2BB
		// (set) Token: 0x06001395 RID: 5013 RVA: 0x0004F0C3 File Offset: 0x0004D2C3
		public double elapsedTime { get; protected set; }

		// Token: 0x06001396 RID: 5014 RVA: 0x0004F0CC File Offset: 0x0004D2CC
		protected TransitionEventBase()
		{
			this.stylePropertyNames = new StylePropertyNameCollection(new List<StylePropertyName>());
			this.LocalInit();
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x0004F0ED File Offset: 0x0004D2ED
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x0004F0FE File Offset: 0x0004D2FE
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles;
			this.stylePropertyNames.propertiesList.Clear();
			this.elapsedTime = 0.0;
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x0004F12C File Offset: 0x0004D32C
		public static T GetPooled(StylePropertyName stylePropertyName, double elapsedTime)
		{
			T pooled = EventBase<T>.GetPooled();
			pooled.stylePropertyNames.propertiesList.Add(stylePropertyName);
			pooled.elapsedTime = elapsedTime;
			return pooled;
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x0004F16C File Offset: 0x0004D36C
		public bool AffectsProperty(StylePropertyName stylePropertyName)
		{
			return this.stylePropertyNames.Contains(stylePropertyName);
		}
	}
}
