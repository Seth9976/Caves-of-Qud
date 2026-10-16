using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000291 RID: 657
	[Obsolete("Not sent by input backend.")]
	public sealed class PointerStationaryEvent : PointerEventBase<PointerStationaryEvent>
	{
		// Token: 0x0600132D RID: 4909 RVA: 0x0004E8CE File Offset: 0x0004CACE
		static PointerStationaryEvent()
		{
			EventBase<PointerStationaryEvent>.SetCreateFunction(() => new PointerStationaryEvent());
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x0004E8E7 File Offset: 0x0004CAE7
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x0004E8F8 File Offset: 0x0004CAF8
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.BubblesOrTricklesDown;
			((IPointerEventInternal)this).triggeredByOS = true;
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x0004E90B File Offset: 0x0004CB0B
		public PointerStationaryEvent()
		{
			this.LocalInit();
		}
	}
}
