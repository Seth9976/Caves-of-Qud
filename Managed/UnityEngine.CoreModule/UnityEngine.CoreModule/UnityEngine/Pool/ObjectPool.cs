using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace UnityEngine.Pool
{
	// Token: 0x02000428 RID: 1064
	public class ObjectPool<T> : IDisposable, IPool, IObjectPool<T> where T : class
	{
		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x060029A3 RID: 10659 RVA: 0x00050341 File Offset: 0x0004E541
		// (set) Token: 0x060029A4 RID: 10660 RVA: 0x00050349 File Offset: 0x0004E549
		public int CountAll { get; private set; }

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x060029A5 RID: 10661 RVA: 0x00050354 File Offset: 0x0004E554
		public int CountActive
		{
			get
			{
				return this.CountAll - this.CountInactive;
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x060029A6 RID: 10662 RVA: 0x00050374 File Offset: 0x0004E574
		public int CountInactive
		{
			get
			{
				return this.m_List.Count + ((this.m_FreshlyReleased != null) ? 1 : 0);
			}
		}

		// Token: 0x060029A7 RID: 10663 RVA: 0x000503A4 File Offset: 0x0004E5A4
		public ObjectPool(Func<T> createFunc, Action<T> actionOnGet = null, Action<T> actionOnRelease = null, Action<T> actionOnDestroy = null, bool collectionCheck = true, int defaultCapacity = 10, int maxSize = 10000)
		{
			bool flag = createFunc == null;
			if (flag)
			{
				throw new ArgumentNullException("createFunc");
			}
			bool flag2 = maxSize <= 0;
			if (flag2)
			{
				throw new ArgumentException("Max Size must be greater than 0", "maxSize");
			}
			this.m_List = new List<T>(defaultCapacity);
			this.m_CreateFunc = createFunc;
			this.m_MaxSize = maxSize;
			this.m_ActionOnGet = actionOnGet;
			this.m_ActionOnRelease = actionOnRelease;
			this.m_ActionOnDestroy = actionOnDestroy;
			this.m_CollectionCheck = collectionCheck;
			PoolManager.Register(this);
		}

		// Token: 0x060029A8 RID: 10664 RVA: 0x0005042C File Offset: 0x0004E62C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public T Get()
		{
			bool flag = this.m_FreshlyReleased != null;
			T t;
			if (flag)
			{
				t = this.m_FreshlyReleased;
				this.m_FreshlyReleased = default(T);
			}
			else
			{
				bool flag2 = this.m_List.Count == 0;
				if (flag2)
				{
					t = this.m_CreateFunc();
					int countAll = this.CountAll;
					this.CountAll = countAll + 1;
				}
				else
				{
					int num = this.m_List.Count - 1;
					t = this.m_List[num];
					this.m_List.RemoveAt(num);
				}
			}
			Action<T> actionOnGet = this.m_ActionOnGet;
			if (actionOnGet != null)
			{
				actionOnGet(t);
			}
			return t;
		}

		// Token: 0x060029A9 RID: 10665 RVA: 0x000504E0 File Offset: 0x0004E6E0
		public PooledObject<T> Get(out T v)
		{
			return new PooledObject<T>(v = this.Get(), this);
		}

		// Token: 0x060029AA RID: 10666 RVA: 0x00050504 File Offset: 0x0004E704
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Release(T element)
		{
			Action<T> actionOnRelease = this.m_ActionOnRelease;
			if (actionOnRelease != null)
			{
				actionOnRelease(element);
			}
			bool flag = this.m_FreshlyReleased == null;
			if (flag)
			{
				this.m_FreshlyReleased = element;
			}
			else
			{
				bool flag2 = this.CountInactive < this.m_MaxSize;
				if (flag2)
				{
					this.m_List.Add(element);
				}
				else
				{
					int countAll = this.CountAll;
					this.CountAll = countAll - 1;
					Action<T> actionOnDestroy = this.m_ActionOnDestroy;
					if (actionOnDestroy != null)
					{
						actionOnDestroy(element);
					}
				}
			}
		}

		// Token: 0x060029AB RID: 10667 RVA: 0x0005058C File Offset: 0x0004E78C
		public void Clear()
		{
			bool flag = this.m_ActionOnDestroy != null;
			if (flag)
			{
				foreach (T t in this.m_List)
				{
					this.m_ActionOnDestroy(t);
				}
				bool flag2 = this.m_FreshlyReleased != null;
				if (flag2)
				{
					this.m_ActionOnDestroy(this.m_FreshlyReleased);
				}
			}
			this.m_FreshlyReleased = default(T);
			this.m_List.Clear();
			this.CountAll = 0;
		}

		// Token: 0x060029AC RID: 10668 RVA: 0x00050640 File Offset: 0x0004E840
		public void Dispose()
		{
			this.Clear();
		}

		// Token: 0x060029AD RID: 10669 RVA: 0x0005064C File Offset: 0x0004E84C
		internal bool HasElement(T element)
		{
			return this.m_FreshlyReleased == element || this.m_List.Contains(element);
		}

		// Token: 0x04000BD0 RID: 3024
		internal readonly List<T> m_List;

		// Token: 0x04000BD1 RID: 3025
		private readonly Func<T> m_CreateFunc;

		// Token: 0x04000BD2 RID: 3026
		private readonly Action<T> m_ActionOnGet;

		// Token: 0x04000BD3 RID: 3027
		private readonly Action<T> m_ActionOnRelease;

		// Token: 0x04000BD4 RID: 3028
		private readonly Action<T> m_ActionOnDestroy;

		// Token: 0x04000BD5 RID: 3029
		private readonly int m_MaxSize;

		// Token: 0x04000BD6 RID: 3030
		internal bool m_CollectionCheck;

		// Token: 0x04000BD7 RID: 3031
		private T m_FreshlyReleased;
	}
}
