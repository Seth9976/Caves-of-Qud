using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TMPro
{
	// Token: 0x0200003E RID: 62
	internal class TMP_ObjectPool<T> where T : new()
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060002DB RID: 731 RVA: 0x00011AD1 File Offset: 0x0000FCD1
		// (set) Token: 0x060002DC RID: 732 RVA: 0x00011AD9 File Offset: 0x0000FCD9
		public int countAll { get; private set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060002DD RID: 733 RVA: 0x00011AE2 File Offset: 0x0000FCE2
		public int countActive
		{
			get
			{
				return this.countAll - this.countInactive;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060002DE RID: 734 RVA: 0x00011AF1 File Offset: 0x0000FCF1
		public int countInactive
		{
			get
			{
				return this.m_Stack.Count;
			}
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00011AFE File Offset: 0x0000FCFE
		public TMP_ObjectPool(UnityAction<T> actionOnGet, UnityAction<T> actionOnRelease)
		{
			this.m_ActionOnGet = actionOnGet;
			this.m_ActionOnRelease = actionOnRelease;
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00011B20 File Offset: 0x0000FD20
		public T Get()
		{
			T t;
			if (this.m_Stack.Count == 0)
			{
				t = new T();
				int countAll = this.countAll;
				this.countAll = countAll + 1;
			}
			else
			{
				t = this.m_Stack.Pop();
			}
			if (this.m_ActionOnGet != null)
			{
				this.m_ActionOnGet(t);
			}
			return t;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00011B74 File Offset: 0x0000FD74
		public void Release(T element)
		{
			if (this.m_Stack.Count > 0 && this.m_Stack.Peek() == element)
			{
				Debug.LogError("Internal error. Trying to destroy object that is already released to pool.");
			}
			if (this.m_ActionOnRelease != null)
			{
				this.m_ActionOnRelease(element);
			}
			this.m_Stack.Push(element);
		}

		// Token: 0x040001D4 RID: 468
		private readonly Stack<T> m_Stack = new Stack<T>();

		// Token: 0x040001D5 RID: 469
		private readonly UnityAction<T> m_ActionOnGet;

		// Token: 0x040001D6 RID: 470
		private readonly UnityAction<T> m_ActionOnRelease;
	}
}
