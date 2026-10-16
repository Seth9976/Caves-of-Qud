using System;

namespace UnityEngine.Pool
{
	// Token: 0x02000426 RID: 1062
	public class LinkedPool<T> : IDisposable, IPool, IObjectPool<T> where T : class
	{
		// Token: 0x0600299A RID: 10650 RVA: 0x000500E8 File Offset: 0x0004E2E8
		public LinkedPool(Func<T> createFunc, Action<T> actionOnGet = null, Action<T> actionOnRelease = null, Action<T> actionOnDestroy = null, bool collectionCheck = true, int maxSize = 10000)
		{
			bool flag = createFunc == null;
			if (flag)
			{
				throw new ArgumentNullException("createFunc");
			}
			bool flag2 = maxSize <= 0;
			if (flag2)
			{
				throw new ArgumentException("maxSize", "Max size must be greater than 0");
			}
			this.m_CreateFunc = createFunc;
			this.m_ActionOnGet = actionOnGet;
			this.m_ActionOnRelease = actionOnRelease;
			this.m_ActionOnDestroy = actionOnDestroy;
			this.m_Limit = maxSize;
			this.m_CollectionCheck = collectionCheck;
			PoolManager.Register(this);
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x0600299B RID: 10651 RVA: 0x00050160 File Offset: 0x0004E360
		// (set) Token: 0x0600299C RID: 10652 RVA: 0x00050168 File Offset: 0x0004E368
		public int CountInactive { get; private set; }

		// Token: 0x0600299D RID: 10653 RVA: 0x00050174 File Offset: 0x0004E374
		public T Get()
		{
			T t = default(T);
			bool flag = this.m_PoolFirst == null;
			if (flag)
			{
				t = this.m_CreateFunc();
			}
			else
			{
				LinkedPool<T>.LinkedPoolItem poolFirst = this.m_PoolFirst;
				t = poolFirst.value;
				this.m_PoolFirst = poolFirst.poolNext;
				poolFirst.poolNext = this.m_NextAvailableListItem;
				this.m_NextAvailableListItem = poolFirst;
				this.m_NextAvailableListItem.value = default(T);
				int num = this.CountInactive - 1;
				this.CountInactive = num;
			}
			Action<T> actionOnGet = this.m_ActionOnGet;
			if (actionOnGet != null)
			{
				actionOnGet(t);
			}
			return t;
		}

		// Token: 0x0600299E RID: 10654 RVA: 0x00050214 File Offset: 0x0004E414
		public PooledObject<T> Get(out T v)
		{
			return new PooledObject<T>(v = this.Get(), this);
		}

		// Token: 0x0600299F RID: 10655 RVA: 0x00050238 File Offset: 0x0004E438
		public void Release(T item)
		{
			Action<T> actionOnRelease = this.m_ActionOnRelease;
			if (actionOnRelease != null)
			{
				actionOnRelease(item);
			}
			bool flag = this.CountInactive < this.m_Limit;
			if (flag)
			{
				LinkedPool<T>.LinkedPoolItem linkedPoolItem = this.m_NextAvailableListItem;
				bool flag2 = linkedPoolItem == null;
				if (flag2)
				{
					linkedPoolItem = new LinkedPool<T>.LinkedPoolItem();
				}
				else
				{
					this.m_NextAvailableListItem = linkedPoolItem.poolNext;
				}
				linkedPoolItem.value = item;
				linkedPoolItem.poolNext = this.m_PoolFirst;
				this.m_PoolFirst = linkedPoolItem;
				int num = this.CountInactive + 1;
				this.CountInactive = num;
			}
			else
			{
				Action<T> actionOnDestroy = this.m_ActionOnDestroy;
				if (actionOnDestroy != null)
				{
					actionOnDestroy(item);
				}
			}
		}

		// Token: 0x060029A0 RID: 10656 RVA: 0x000502D8 File Offset: 0x0004E4D8
		public void Clear()
		{
			bool flag = this.m_ActionOnDestroy != null;
			if (flag)
			{
				for (LinkedPool<T>.LinkedPoolItem linkedPoolItem = this.m_PoolFirst; linkedPoolItem != null; linkedPoolItem = linkedPoolItem.poolNext)
				{
					this.m_ActionOnDestroy(linkedPoolItem.value);
				}
			}
			this.m_PoolFirst = null;
			this.m_NextAvailableListItem = null;
			this.CountInactive = 0;
		}

		// Token: 0x060029A1 RID: 10657 RVA: 0x00050337 File Offset: 0x0004E537
		public void Dispose()
		{
			this.Clear();
		}

		// Token: 0x04000BC5 RID: 3013
		private readonly Func<T> m_CreateFunc;

		// Token: 0x04000BC6 RID: 3014
		private readonly Action<T> m_ActionOnGet;

		// Token: 0x04000BC7 RID: 3015
		private readonly Action<T> m_ActionOnRelease;

		// Token: 0x04000BC8 RID: 3016
		private readonly Action<T> m_ActionOnDestroy;

		// Token: 0x04000BC9 RID: 3017
		private readonly int m_Limit;

		// Token: 0x04000BCA RID: 3018
		internal LinkedPool<T>.LinkedPoolItem m_PoolFirst;

		// Token: 0x04000BCB RID: 3019
		internal LinkedPool<T>.LinkedPoolItem m_NextAvailableListItem;

		// Token: 0x04000BCC RID: 3020
		private bool m_CollectionCheck;

		// Token: 0x02000427 RID: 1063
		internal class LinkedPoolItem
		{
			// Token: 0x04000BCE RID: 3022
			internal LinkedPool<T>.LinkedPoolItem poolNext;

			// Token: 0x04000BCF RID: 3023
			internal T value;
		}
	}
}
