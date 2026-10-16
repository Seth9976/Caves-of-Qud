using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000222 RID: 546
	[EventCategory(EventCategory.ChangeValue)]
	public class ChangeEvent<T> : EventBase<ChangeEvent<T>>, IChangeEvent
	{
		// Token: 0x0600103C RID: 4156 RVA: 0x0004769C File Offset: 0x0004589C
		static ChangeEvent()
		{
			EventBase<ChangeEvent<T>>.SetCreateFunction(() => new ChangeEvent<T>());
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x0600103D RID: 4157 RVA: 0x000476B5 File Offset: 0x000458B5
		// (set) Token: 0x0600103E RID: 4158 RVA: 0x000476BD File Offset: 0x000458BD
		public T previousValue { get; protected set; }

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x0600103F RID: 4159 RVA: 0x000476C6 File Offset: 0x000458C6
		// (set) Token: 0x06001040 RID: 4160 RVA: 0x000476CE File Offset: 0x000458CE
		public T newValue { get; protected set; }

		// Token: 0x06001041 RID: 4161 RVA: 0x000476D7 File Offset: 0x000458D7
		protected override void Init()
		{
			base.Init();
			this.LocalInit();
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x000476E8 File Offset: 0x000458E8
		private void LocalInit()
		{
			base.propagation = EventBase.EventPropagation.BubblesOrTricklesDown;
			this.previousValue = default(T);
			this.newValue = default(T);
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x00047720 File Offset: 0x00045920
		public static ChangeEvent<T> GetPooled(T previousValue, T newValue)
		{
			ChangeEvent<T> pooled = EventBase<ChangeEvent<T>>.GetPooled();
			pooled.previousValue = previousValue;
			pooled.newValue = newValue;
			return pooled;
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x00047749 File Offset: 0x00045949
		public ChangeEvent()
		{
			this.LocalInit();
		}
	}
}
