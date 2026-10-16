using System;
using System.Collections;
using System.Collections.Generic;

namespace LaundryBear
{
	// Token: 0x0200000F RID: 15
	public class Pool<T>
	{
		// Token: 0x06000050 RID: 80 RVA: 0x000029EC File Offset: 0x00000BEC
		public Pool(int initialCount, Func<T> creator, Func<T[], IEnumerator> creatorAsync, Action<T> reset)
		{
			this.m_creator = creator;
			this.m_creatorAsync = creatorAsync;
			this.m_reset = reset;
			this.m_pooledObjects = new Stack<Pool<T>.PooledObject>(initialCount);
			this.m_allPoolCreatedObjects = new List<Pool<T>.PooledObject>(initialCount);
			for (int i = 0; i < initialCount; i++)
			{
				Pool<T>.PooledObject pooledObject = new Pool<T>.PooledObject(creator(), new Action<Pool<T>.PooledObject>(this.ReturnToPool));
				this.m_pooledObjects.Push(pooledObject);
				this.m_allPoolCreatedObjects.Add(pooledObject);
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002A69 File Offset: 0x00000C69
		public IEnumerator GrowPoolAsync(int count)
		{
			T[] newPooledObjects = new T[count];
			yield return this.m_creatorAsync(newPooledObjects);
			for (int i = 0; i < newPooledObjects.Length; i++)
			{
				Pool<T>.PooledObject pooledObject = new Pool<T>.PooledObject(newPooledObjects[i], new Action<Pool<T>.PooledObject>(this.ReturnToPool));
				this.m_pooledObjects.Push(pooledObject);
				this.m_allPoolCreatedObjects.Add(pooledObject);
			}
			yield break;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002A80 File Offset: 0x00000C80
		public void GrowPool(int count)
		{
			T[] array = new T[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = this.m_creator();
			}
			for (int j = 0; j < array.Length; j++)
			{
				Pool<T>.PooledObject pooledObject = new Pool<T>.PooledObject(array[j], new Action<Pool<T>.PooledObject>(this.ReturnToPool));
				this.m_pooledObjects.Push(pooledObject);
				this.m_allPoolCreatedObjects.Add(pooledObject);
			}
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002AF4 File Offset: 0x00000CF4
		public Pool<T>.PooledObject GetObject()
		{
			if (this.m_pooledObjects.Count == 0)
			{
				this.m_pooledObjects.Push(new Pool<T>.PooledObject(this.m_creator(), new Action<Pool<T>.PooledObject>(this.ReturnToPool)));
			}
			return this.m_pooledObjects.Pop();
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002B40 File Offset: 0x00000D40
		private void ReturnToPool(Pool<T>.PooledObject source)
		{
			this.m_reset(source);
			this.m_pooledObjects.Push(source);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002B5F File Offset: 0x00000D5F
		public IEnumerator InvokeOnAllObjectsAsync(Func<T, IEnumerator> processor)
		{
			foreach (Pool<T>.PooledObject pooledObject in this.m_allPoolCreatedObjects)
			{
				yield return processor(pooledObject);
			}
			List<Pool<T>.PooledObject>.Enumerator enumerator = default(List<Pool<T>.PooledObject>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x00002B78 File Offset: 0x00000D78
		public void InvokeOnAllObjects(Action<T> processor)
		{
			foreach (Pool<T>.PooledObject pooledObject in this.m_allPoolCreatedObjects)
			{
				processor(pooledObject);
			}
		}

		// Token: 0x0400001A RID: 26
		private Func<T> m_creator;

		// Token: 0x0400001B RID: 27
		private Func<T[], IEnumerator> m_creatorAsync;

		// Token: 0x0400001C RID: 28
		private Action<T> m_reset;

		// Token: 0x0400001D RID: 29
		private Stack<Pool<T>.PooledObject> m_pooledObjects;

		// Token: 0x0400001E RID: 30
		private List<Pool<T>.PooledObject> m_allPoolCreatedObjects;

		// Token: 0x02000022 RID: 34
		public class PooledObject
		{
			// Token: 0x17000023 RID: 35
			// (get) Token: 0x060000A8 RID: 168 RVA: 0x000035B2 File Offset: 0x000017B2
			public T Value
			{
				get
				{
					return this.m_source;
				}
			}

			// Token: 0x060000A9 RID: 169 RVA: 0x000035BA File Offset: 0x000017BA
			public PooledObject(T source, Action<Pool<T>.PooledObject> returnDelegate)
			{
				this.m_source = source;
				this.m_returnToPool = returnDelegate;
			}

			// Token: 0x060000AA RID: 170 RVA: 0x000035D0 File Offset: 0x000017D0
			internal void AssignReturnDelegate(Action<Pool<T>.PooledObject> returnDelegate)
			{
				this.m_returnToPool = returnDelegate;
			}

			// Token: 0x060000AB RID: 171 RVA: 0x000035D9 File Offset: 0x000017D9
			public void Release()
			{
				this.m_returnToPool(this);
			}

			// Token: 0x060000AC RID: 172 RVA: 0x000035E7 File Offset: 0x000017E7
			public static implicit operator T(Pool<T>.PooledObject pooledObject)
			{
				return pooledObject.m_source;
			}

			// Token: 0x04000055 RID: 85
			private T m_source;

			// Token: 0x04000056 RID: 86
			internal Action<Pool<T>.PooledObject> m_returnToPool;
		}
	}
}
