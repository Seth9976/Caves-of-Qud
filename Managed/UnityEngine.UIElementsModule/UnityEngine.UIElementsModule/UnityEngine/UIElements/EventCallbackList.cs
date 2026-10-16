using System;
using JetBrains.Annotations;

namespace UnityEngine.UIElements
{
	// Token: 0x02000238 RID: 568
	internal class EventCallbackList
	{
		// Token: 0x060010D3 RID: 4307 RVA: 0x00048A4B File Offset: 0x00046C4B
		public EventCallbackList()
		{
			this.m_Array = EventCallbackList.EmptyArray;
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x00048A60 File Offset: 0x00046C60
		public EventCallbackList(EventCallbackList source)
		{
			this.m_Count = source.m_Count;
			this.m_Array = new EventCallbackFunctorBase[this.m_Count];
			Array.Copy(source.m_Array, this.m_Array, this.m_Count);
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x00048AA0 File Offset: 0x00046CA0
		public bool Contains(long eventTypeId, [NotNull] Delegate callback)
		{
			return this.Find(eventTypeId, callback) != null;
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x00048AC0 File Offset: 0x00046CC0
		public EventCallbackFunctorBase Find(long eventTypeId, [NotNull] Delegate callback)
		{
			for (int i = 0; i < this.m_Count; i++)
			{
				bool flag = this.m_Array[i].IsEquivalentTo(eventTypeId, callback);
				if (flag)
				{
					return this.m_Array[i];
				}
			}
			return null;
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x00048B0C File Offset: 0x00046D0C
		public bool Remove(long eventTypeId, [NotNull] Delegate callback, out EventCallbackFunctorBase removedFunctor)
		{
			for (int i = 0; i < this.m_Count; i++)
			{
				bool flag = this.m_Array[i].IsEquivalentTo(eventTypeId, callback);
				if (flag)
				{
					removedFunctor = this.m_Array[i];
					this.m_Count--;
					Array.Copy(this.m_Array, i + 1, this.m_Array, i, this.m_Count - i);
					this.m_Array[this.m_Count] = null;
					return true;
				}
			}
			removedFunctor = null;
			return false;
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x00048B98 File Offset: 0x00046D98
		public void Add(EventCallbackFunctorBase item)
		{
			bool flag = this.m_Count >= this.m_Array.Length;
			if (flag)
			{
				Array.Resize<EventCallbackFunctorBase>(ref this.m_Array, Mathf.NextPowerOfTwo(this.m_Count + 4));
			}
			EventCallbackFunctorBase[] array = this.m_Array;
			int count = this.m_Count;
			this.m_Count = count + 1;
			array[count] = item;
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x00048BF0 File Offset: 0x00046DF0
		public void AddRange(EventCallbackList list)
		{
			bool flag = this.m_Count + list.m_Count > this.m_Array.Length;
			if (flag)
			{
				Array.Resize<EventCallbackFunctorBase>(ref this.m_Array, Mathf.NextPowerOfTwo(this.m_Count + list.m_Count));
			}
			Array.Copy(list.m_Array, 0, this.m_Array, this.m_Count, list.m_Count);
			this.m_Count += list.m_Count;
		}

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x060010DA RID: 4314 RVA: 0x00048C69 File Offset: 0x00046E69
		public int Count
		{
			get
			{
				return this.m_Count;
			}
		}

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x060010DB RID: 4315 RVA: 0x00048C71 File Offset: 0x00046E71
		public Span<EventCallbackFunctorBase> Span
		{
			get
			{
				return new Span<EventCallbackFunctorBase>(this.m_Array, 0, this.m_Count);
			}
		}

		// Token: 0x17000308 RID: 776
		public EventCallbackFunctorBase this[int i]
		{
			get
			{
				return this.m_Array[i];
			}
			set
			{
				this.m_Array[i] = value;
			}
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x00048CAE File Offset: 0x00046EAE
		public void Clear()
		{
			Array.Clear(this.m_Array, 0, this.m_Count);
			this.m_Count = 0;
		}

		// Token: 0x04000A0D RID: 2573
		public static readonly EventCallbackList EmptyList = new EventCallbackList();

		// Token: 0x04000A0E RID: 2574
		private static readonly EventCallbackFunctorBase[] EmptyArray = new EventCallbackFunctorBase[0];

		// Token: 0x04000A0F RID: 2575
		private EventCallbackFunctorBase[] m_Array;

		// Token: 0x04000A10 RID: 2576
		private int m_Count;
	}
}
