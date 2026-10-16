using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200022E RID: 558
	[EventCategory(EventCategory.Default)]
	public abstract class EventBase<T> : EventBase where T : EventBase<T>, new()
	{
		// Token: 0x060010A7 RID: 4263 RVA: 0x000485E4 File Offset: 0x000467E4
		internal static void SetCreateFunction(Func<T> createMethod)
		{
			EventBase<T>.s_Pool.CreateFunc = createMethod;
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x000485F2 File Offset: 0x000467F2
		protected EventBase()
			: base(EventBase<T>.EventCategory)
		{
			this.m_RefCount = 0;
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x00048608 File Offset: 0x00046808
		public static long TypeId()
		{
			return EventBase<T>.s_TypeId;
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x00048620 File Offset: 0x00046820
		protected override void Init()
		{
			base.Init();
			bool flag = this.m_RefCount != 0;
			if (flag)
			{
				Debug.Log("Event improperly released.");
				this.m_RefCount = 0;
			}
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x00048658 File Offset: 0x00046858
		public static T GetPooled()
		{
			T t = EventBase<T>.s_Pool.Get();
			t.Init();
			t.pooled = true;
			t.Acquire();
			return t;
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x0004869C File Offset: 0x0004689C
		internal static T GetPooled(EventBase e)
		{
			T pooled = EventBase<T>.GetPooled();
			bool flag = e != null;
			if (flag)
			{
				pooled.SetTriggerEventId(e.eventId);
			}
			return pooled;
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x000486D4 File Offset: 0x000468D4
		private static void ReleasePooled(T evt)
		{
			bool pooled = evt.pooled;
			if (pooled)
			{
				evt.Init();
				EventBase<T>.s_Pool.Release(evt);
				evt.pooled = false;
			}
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x00048718 File Offset: 0x00046918
		internal override void Acquire()
		{
			this.m_RefCount++;
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0004872C File Offset: 0x0004692C
		public sealed override void Dispose()
		{
			int num = this.m_RefCount - 1;
			this.m_RefCount = num;
			bool flag = num == 0;
			if (flag)
			{
				EventBase<T>.ReleasePooled((T)((object)this));
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x060010B0 RID: 4272 RVA: 0x00048760 File Offset: 0x00046960
		public override long eventTypeId
		{
			get
			{
				return EventBase<T>.s_TypeId;
			}
		}

		// Token: 0x040009FB RID: 2555
		private static readonly long s_TypeId = EventBase.RegisterEventType();

		// Token: 0x040009FC RID: 2556
		private static readonly ObjectPool<T> s_Pool = new ObjectPool<T>(() => new T(), 100);

		// Token: 0x040009FD RID: 2557
		private int m_RefCount;

		// Token: 0x040009FE RID: 2558
		internal static readonly EventCategory EventCategory = EventInterestReflectionUtils.GetEventCategory(typeof(T));
	}
}
