using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000249 RID: 585
	[EventCategory(EventCategory.Keyboard)]
	internal class IMEEvent : EventBase<IMEEvent>
	{
		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06001155 RID: 4437 RVA: 0x0004A6AA File Offset: 0x000488AA
		// (set) Token: 0x06001156 RID: 4438 RVA: 0x0004A6B2 File Offset: 0x000488B2
		public string compositionString { get; protected set; }

		// Token: 0x06001157 RID: 4439 RVA: 0x0004A6BB File Offset: 0x000488BB
		static IMEEvent()
		{
			EventBase<IMEEvent>.SetCreateFunction(() => new IMEEvent());
		}

		// Token: 0x06001158 RID: 4440 RVA: 0x0004A6D4 File Offset: 0x000488D4
		public static IMEEvent GetPooled(string compositionString)
		{
			IMEEvent pooled = EventBase<IMEEvent>.GetPooled();
			pooled.compositionString = compositionString;
			return pooled;
		}

		// Token: 0x06001159 RID: 4441 RVA: 0x0004A6F5 File Offset: 0x000488F5
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x0600115A RID: 4442 RVA: 0x0004A706 File Offset: 0x00048906
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.Bubbles | EventBase.EventPropagation.TricklesDown | EventBase.EventPropagation.SkipDisabledElements;
			this.compositionString = null;
		}

		// Token: 0x0600115B RID: 4443 RVA: 0x0004A719 File Offset: 0x00048919
		public IMEEvent()
		{
			this.LocalInit();
		}
	}
}
