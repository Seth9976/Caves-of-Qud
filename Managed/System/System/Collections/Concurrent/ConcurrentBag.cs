using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace System.Collections.Concurrent
{
	/// <summary>Represents a thread-safe, unordered collection of objects.</summary>
	/// <typeparam name="T">The type of the elements to be stored in the collection.</typeparam>
	// Token: 0x020004A0 RID: 1184
	[DebuggerDisplay("Count = {Count}")]
	[DebuggerTypeProxy(typeof(IProducerConsumerCollectionDebugView<>))]
	[Serializable]
	public class ConcurrentBag<T> : IProducerConsumerCollection<T>, IEnumerable<T>, IEnumerable, ICollection, IReadOnlyCollection<T>
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> class.</summary>
		// Token: 0x060025E8 RID: 9704 RVA: 0x00084918 File Offset: 0x00082B18
		public ConcurrentBag()
		{
			this._locals = new ThreadLocal<ConcurrentBag<T>.WorkStealingQueue>();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> class that contains elements copied from the specified collection.</summary>
		/// <param name="collection">The collection whose elements are copied to the new <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="collection" /> is a null reference (Nothing in Visual Basic).</exception>
		// Token: 0x060025E9 RID: 9705 RVA: 0x0008492C File Offset: 0x00082B2C
		public ConcurrentBag(IEnumerable<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection", "The collection argument is null.");
			}
			this._locals = new ThreadLocal<ConcurrentBag<T>.WorkStealingQueue>();
			ConcurrentBag<T>.WorkStealingQueue currentThreadWorkStealingQueue = this.GetCurrentThreadWorkStealingQueue(true);
			foreach (T t in collection)
			{
				currentThreadWorkStealingQueue.LocalPush(t, ref this._emptyToNonEmptyListTransitionCount);
			}
		}

		/// <summary>Adds an object to the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</summary>
		/// <param name="item">The object to be added to the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />. The value can be a null reference (Nothing in Visual Basic) for reference types.</param>
		// Token: 0x060025EA RID: 9706 RVA: 0x000849A8 File Offset: 0x00082BA8
		public void Add(T item)
		{
			this.GetCurrentThreadWorkStealingQueue(true).LocalPush(item, ref this._emptyToNonEmptyListTransitionCount);
		}

		/// <summary>Attempts to add an object to the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</summary>
		/// <returns>Always returns true</returns>
		/// <param name="item">The object to be added to the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />. The value can be a null reference (Nothing in Visual Basic) for reference types.</param>
		// Token: 0x060025EB RID: 9707 RVA: 0x000849BD File Offset: 0x00082BBD
		bool IProducerConsumerCollection<T>.TryAdd(T item)
		{
			this.Add(item);
			return true;
		}

		/// <summary>Attempts to remove and return an object from the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</summary>
		/// <returns>true if an object was removed successfully; otherwise, false.</returns>
		/// <param name="result">When this method returns, <paramref name="result" /> contains the object removed from the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> or the default value of <paramref name="T" /> if the bag is empty.</param>
		// Token: 0x060025EC RID: 9708 RVA: 0x000849C8 File Offset: 0x00082BC8
		public bool TryTake(out T result)
		{
			ConcurrentBag<T>.WorkStealingQueue currentThreadWorkStealingQueue = this.GetCurrentThreadWorkStealingQueue(false);
			return (currentThreadWorkStealingQueue != null && currentThreadWorkStealingQueue.TryLocalPop(out result)) || this.TrySteal(out result, true);
		}

		/// <summary>Attempts to return an object from the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> without removing it.</summary>
		/// <returns>true if and object was returned successfully; otherwise, false.</returns>
		/// <param name="result">When this method returns, <paramref name="result" /> contains an object from the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> or the default value of <paramref name="T" /> if the operation failed.</param>
		// Token: 0x060025ED RID: 9709 RVA: 0x000849F4 File Offset: 0x00082BF4
		public bool TryPeek(out T result)
		{
			ConcurrentBag<T>.WorkStealingQueue currentThreadWorkStealingQueue = this.GetCurrentThreadWorkStealingQueue(false);
			return (currentThreadWorkStealingQueue != null && currentThreadWorkStealingQueue.TryLocalPeek(out result)) || this.TrySteal(out result, false);
		}

		// Token: 0x060025EE RID: 9710 RVA: 0x00084A1F File Offset: 0x00082C1F
		private ConcurrentBag<T>.WorkStealingQueue GetCurrentThreadWorkStealingQueue(bool forceCreate)
		{
			ConcurrentBag<T>.WorkStealingQueue workStealingQueue;
			if ((workStealingQueue = this._locals.Value) == null)
			{
				if (!forceCreate)
				{
					return null;
				}
				workStealingQueue = this.CreateWorkStealingQueueForCurrentThread();
			}
			return workStealingQueue;
		}

		// Token: 0x060025EF RID: 9711 RVA: 0x00084A3C File Offset: 0x00082C3C
		private ConcurrentBag<T>.WorkStealingQueue CreateWorkStealingQueueForCurrentThread()
		{
			object globalQueuesLock = this.GlobalQueuesLock;
			ConcurrentBag<T>.WorkStealingQueue workStealingQueue2;
			lock (globalQueuesLock)
			{
				ConcurrentBag<T>.WorkStealingQueue workStealingQueues = this._workStealingQueues;
				ConcurrentBag<T>.WorkStealingQueue workStealingQueue = ((workStealingQueues != null) ? this.GetUnownedWorkStealingQueue() : null);
				if (workStealingQueue == null)
				{
					workStealingQueue = (this._workStealingQueues = new ConcurrentBag<T>.WorkStealingQueue(workStealingQueues));
				}
				this._locals.Value = workStealingQueue;
				workStealingQueue2 = workStealingQueue;
			}
			return workStealingQueue2;
		}

		// Token: 0x060025F0 RID: 9712 RVA: 0x00084AB0 File Offset: 0x00082CB0
		private ConcurrentBag<T>.WorkStealingQueue GetUnownedWorkStealingQueue()
		{
			int currentManagedThreadId = Environment.CurrentManagedThreadId;
			for (ConcurrentBag<T>.WorkStealingQueue workStealingQueue = this._workStealingQueues; workStealingQueue != null; workStealingQueue = workStealingQueue._nextQueue)
			{
				if (workStealingQueue._ownerThreadId == currentManagedThreadId)
				{
					return workStealingQueue;
				}
			}
			return null;
		}

		// Token: 0x060025F1 RID: 9713 RVA: 0x00084AE4 File Offset: 0x00082CE4
		private bool TrySteal(out T result, bool take)
		{
			if (take)
			{
				CDSCollectionETWBCLProvider.Log.ConcurrentBag_TryTakeSteals();
			}
			else
			{
				CDSCollectionETWBCLProvider.Log.ConcurrentBag_TryPeekSteals();
			}
			for (;;)
			{
				long num = Interlocked.Read(ref this._emptyToNonEmptyListTransitionCount);
				ConcurrentBag<T>.WorkStealingQueue currentThreadWorkStealingQueue = this.GetCurrentThreadWorkStealingQueue(false);
				if ((currentThreadWorkStealingQueue == null) ? this.TryStealFromTo(this._workStealingQueues, null, out result, take) : (this.TryStealFromTo(currentThreadWorkStealingQueue._nextQueue, null, out result, take) || this.TryStealFromTo(this._workStealingQueues, currentThreadWorkStealingQueue, out result, take)))
				{
					break;
				}
				if (Interlocked.Read(ref this._emptyToNonEmptyListTransitionCount) == num)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060025F2 RID: 9714 RVA: 0x00084B6C File Offset: 0x00082D6C
		private bool TryStealFromTo(ConcurrentBag<T>.WorkStealingQueue startInclusive, ConcurrentBag<T>.WorkStealingQueue endExclusive, out T result, bool take)
		{
			for (ConcurrentBag<T>.WorkStealingQueue workStealingQueue = startInclusive; workStealingQueue != endExclusive; workStealingQueue = workStealingQueue._nextQueue)
			{
				if (workStealingQueue.TrySteal(out result, take))
				{
					return true;
				}
			}
			result = default(T);
			return false;
		}

		/// <summary>Copies the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> elements to an existing one-dimensional <see cref="T:System.Array" />, starting at the specified array index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> is equal to or greater than the length of the <paramref name="array" /> -or- the number of elements in the source <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />.</exception>
		// Token: 0x060025F3 RID: 9715 RVA: 0x00084BA0 File Offset: 0x00082DA0
		public void CopyTo(T[] array, int index)
		{
			if (array == null)
			{
				throw new ArgumentNullException("array", "The array argument is null.");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", "The index argument must be greater than or equal zero.");
			}
			if (this._workStealingQueues == null)
			{
				return;
			}
			bool flag = false;
			try
			{
				this.FreezeBag(ref flag);
				int dangerousCount = this.DangerousCount;
				if (index > array.Length - dangerousCount)
				{
					throw new ArgumentException("The number of elements in the collection is greater than the available space from index to the end of the destination array.", "index");
				}
				try
				{
					this.CopyFromEachQueueToArray(array, index);
				}
				catch (ArrayTypeMismatchException ex)
				{
					throw new InvalidCastException(ex.Message, ex);
				}
			}
			finally
			{
				this.UnfreezeBag(flag);
			}
		}

		// Token: 0x060025F4 RID: 9716 RVA: 0x00084C48 File Offset: 0x00082E48
		private int CopyFromEachQueueToArray(T[] array, int index)
		{
			int num = index;
			for (ConcurrentBag<T>.WorkStealingQueue workStealingQueue = this._workStealingQueues; workStealingQueue != null; workStealingQueue = workStealingQueue._nextQueue)
			{
				num += workStealingQueue.DangerousCopyTo(array, num);
			}
			return num - index;
		}

		/// <summary>Copies the elements of the <see cref="T:System.Collections.ICollection" /> to an <see cref="T:System.Array" />, starting at a particular <see cref="T:System.Array" /> index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied from the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />. The <see cref="T:System.Array" /> must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="array" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="array" /> is multidimensional. -or- <paramref name="array" /> does not have zero-based indexing. -or- <paramref name="index" /> is equal to or greater than the length of the <paramref name="array" /> -or- The number of elements in the source <see cref="T:System.Collections.ICollection" /> is greater than the available space from <paramref name="index" /> to the end of the destination <paramref name="array" />. -or- The type of the source <see cref="T:System.Collections.ICollection" /> cannot be cast automatically to the type of the destination <paramref name="array" />.</exception>
		// Token: 0x060025F5 RID: 9717 RVA: 0x00084C7C File Offset: 0x00082E7C
		void ICollection.CopyTo(Array array, int index)
		{
			T[] array2 = array as T[];
			if (array2 != null)
			{
				this.CopyTo(array2, index);
				return;
			}
			if (array == null)
			{
				throw new ArgumentNullException("array", "The array argument is null.");
			}
			this.ToArray().CopyTo(array, index);
		}

		/// <summary>Copies the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> elements to a new array.</summary>
		/// <returns>A new array containing a snapshot of elements copied from the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</returns>
		// Token: 0x060025F6 RID: 9718 RVA: 0x00084CBC File Offset: 0x00082EBC
		public T[] ToArray()
		{
			if (this._workStealingQueues != null)
			{
				bool flag = false;
				try
				{
					this.FreezeBag(ref flag);
					int dangerousCount = this.DangerousCount;
					if (dangerousCount > 0)
					{
						T[] array = new T[dangerousCount];
						this.CopyFromEachQueueToArray(array, 0);
						return array;
					}
				}
				finally
				{
					this.UnfreezeBag(flag);
				}
			}
			return Array.Empty<T>();
		}

		// Token: 0x060025F7 RID: 9719 RVA: 0x00084D20 File Offset: 0x00082F20
		public void Clear()
		{
			if (this._workStealingQueues == null)
			{
				return;
			}
			ConcurrentBag<T>.WorkStealingQueue currentThreadWorkStealingQueue = this.GetCurrentThreadWorkStealingQueue(false);
			if (currentThreadWorkStealingQueue != null)
			{
				currentThreadWorkStealingQueue.LocalClear();
				if (currentThreadWorkStealingQueue._nextQueue == null && currentThreadWorkStealingQueue == this._workStealingQueues)
				{
					return;
				}
			}
			bool flag = false;
			try
			{
				this.FreezeBag(ref flag);
				for (ConcurrentBag<T>.WorkStealingQueue workStealingQueue = this._workStealingQueues; workStealingQueue != null; workStealingQueue = workStealingQueue._nextQueue)
				{
					T t;
					while (workStealingQueue.TrySteal(out t, true))
					{
					}
				}
			}
			finally
			{
				this.UnfreezeBag(flag);
			}
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</summary>
		/// <returns>An enumerator for the contents of the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</returns>
		// Token: 0x060025F8 RID: 9720 RVA: 0x00084DA4 File Offset: 0x00082FA4
		public IEnumerator<T> GetEnumerator()
		{
			return new ConcurrentBag<T>.Enumerator(this.ToArray());
		}

		/// <summary>Returns an enumerator that iterates through the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</summary>
		/// <returns>An enumerator for the contents of the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</returns>
		// Token: 0x060025F9 RID: 9721 RVA: 0x00084DB1 File Offset: 0x00082FB1
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		/// <summary>Gets the number of elements contained in the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</summary>
		/// <returns>The number of elements contained in the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />.</returns>
		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x060025FA RID: 9722 RVA: 0x00084DBC File Offset: 0x00082FBC
		public int Count
		{
			get
			{
				if (this._workStealingQueues == null)
				{
					return 0;
				}
				bool flag = false;
				int dangerousCount;
				try
				{
					this.FreezeBag(ref flag);
					dangerousCount = this.DangerousCount;
				}
				finally
				{
					this.UnfreezeBag(flag);
				}
				return dangerousCount;
			}
		}

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x060025FB RID: 9723 RVA: 0x00084E04 File Offset: 0x00083004
		private int DangerousCount
		{
			get
			{
				int num = 0;
				checked
				{
					for (ConcurrentBag<T>.WorkStealingQueue workStealingQueue = this._workStealingQueues; workStealingQueue != null; workStealingQueue = workStealingQueue._nextQueue)
					{
						num += workStealingQueue.DangerousCount;
					}
					return num;
				}
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> is empty.</summary>
		/// <returns>true if the <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" /> is empty; otherwise, false.</returns>
		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x060025FC RID: 9724 RVA: 0x00084E34 File Offset: 0x00083034
		public bool IsEmpty
		{
			get
			{
				ConcurrentBag<T>.WorkStealingQueue currentThreadWorkStealingQueue = this.GetCurrentThreadWorkStealingQueue(false);
				if (currentThreadWorkStealingQueue != null)
				{
					if (!currentThreadWorkStealingQueue.IsEmpty)
					{
						return false;
					}
					if (currentThreadWorkStealingQueue._nextQueue == null && currentThreadWorkStealingQueue == this._workStealingQueues)
					{
						return true;
					}
				}
				bool flag = false;
				try
				{
					this.FreezeBag(ref flag);
					for (ConcurrentBag<T>.WorkStealingQueue workStealingQueue = this._workStealingQueues; workStealingQueue != null; workStealingQueue = workStealingQueue._nextQueue)
					{
						if (!workStealingQueue.IsEmpty)
						{
							return false;
						}
					}
				}
				finally
				{
					this.UnfreezeBag(flag);
				}
				return true;
			}
		}

		/// <summary>Gets a value indicating whether access to the <see cref="T:System.Collections.ICollection" /> is synchronized with the SyncRoot.</summary>
		/// <returns>true if access to the <see cref="T:System.Collections.ICollection" /> is synchronized with the SyncRoot; otherwise, false. For <see cref="T:System.Collections.Concurrent.ConcurrentBag`1" />, this property always returns false.</returns>
		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x060025FD RID: 9725 RVA: 0x00003062 File Offset: 0x00001262
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets an object that can be used to synchronize access to the <see cref="T:System.Collections.ICollection" />. This property is not supported.</summary>
		/// <returns>Returns null  (Nothing in Visual Basic).</returns>
		/// <exception cref="T:System.NotSupportedException">The SyncRoot property is not supported.</exception>
		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x060025FE RID: 9726 RVA: 0x00083987 File Offset: 0x00081B87
		object ICollection.SyncRoot
		{
			get
			{
				throw new NotSupportedException("The SyncRoot property may not be used for the synchronization of concurrent collections.");
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x060025FF RID: 9727 RVA: 0x00084EB4 File Offset: 0x000830B4
		private object GlobalQueuesLock
		{
			get
			{
				return this._locals;
			}
		}

		// Token: 0x06002600 RID: 9728 RVA: 0x00084EBC File Offset: 0x000830BC
		private void FreezeBag(ref bool lockTaken)
		{
			Monitor.Enter(this.GlobalQueuesLock, ref lockTaken);
			ConcurrentBag<T>.WorkStealingQueue workStealingQueues = this._workStealingQueues;
			for (ConcurrentBag<T>.WorkStealingQueue workStealingQueue = workStealingQueues; workStealingQueue != null; workStealingQueue = workStealingQueue._nextQueue)
			{
				Monitor.Enter(workStealingQueue, ref workStealingQueue._frozen);
			}
			Interlocked.MemoryBarrier();
			for (ConcurrentBag<T>.WorkStealingQueue workStealingQueue2 = workStealingQueues; workStealingQueue2 != null; workStealingQueue2 = workStealingQueue2._nextQueue)
			{
				if (workStealingQueue2._currentOp != 0)
				{
					SpinWait spinWait = default(SpinWait);
					do
					{
						spinWait.SpinOnce();
					}
					while (workStealingQueue2._currentOp != 0);
				}
			}
		}

		// Token: 0x06002601 RID: 9729 RVA: 0x00084F30 File Offset: 0x00083130
		private void UnfreezeBag(bool lockTaken)
		{
			if (lockTaken)
			{
				for (ConcurrentBag<T>.WorkStealingQueue workStealingQueue = this._workStealingQueues; workStealingQueue != null; workStealingQueue = workStealingQueue._nextQueue)
				{
					if (workStealingQueue._frozen)
					{
						workStealingQueue._frozen = false;
						Monitor.Exit(workStealingQueue);
					}
				}
				Monitor.Exit(this.GlobalQueuesLock);
			}
		}

		// Token: 0x040014ED RID: 5357
		private readonly ThreadLocal<ConcurrentBag<T>.WorkStealingQueue> _locals;

		// Token: 0x040014EE RID: 5358
		private volatile ConcurrentBag<T>.WorkStealingQueue _workStealingQueues;

		// Token: 0x040014EF RID: 5359
		private long _emptyToNonEmptyListTransitionCount;

		// Token: 0x020004A1 RID: 1185
		private sealed class WorkStealingQueue
		{
			// Token: 0x06002602 RID: 9730 RVA: 0x00084F75 File Offset: 0x00083175
			internal WorkStealingQueue(ConcurrentBag<T>.WorkStealingQueue nextQueue)
			{
				this._ownerThreadId = Environment.CurrentManagedThreadId;
				this._nextQueue = nextQueue;
			}

			// Token: 0x170007A3 RID: 1955
			// (get) Token: 0x06002603 RID: 9731 RVA: 0x00084FA8 File Offset: 0x000831A8
			internal bool IsEmpty
			{
				get
				{
					return this._headIndex >= this._tailIndex;
				}
			}

			// Token: 0x06002604 RID: 9732 RVA: 0x00084FC0 File Offset: 0x000831C0
			internal void LocalPush(T item, ref long emptyToNonEmptyListTransitionCount)
			{
				bool flag = false;
				try
				{
					Interlocked.Exchange(ref this._currentOp, 1);
					int num = this._tailIndex;
					if (num == 2147483647)
					{
						this._currentOp = 0;
						lock (this)
						{
							this._headIndex &= this._mask;
							num = (this._tailIndex = num & this._mask);
							Interlocked.Exchange(ref this._currentOp, 1);
						}
					}
					int num2 = this._headIndex;
					if (!this._frozen && ((num2 < num - 1) & (num < num2 + this._mask)))
					{
						this._array[num & this._mask] = item;
						this._tailIndex = num + 1;
					}
					else
					{
						this._currentOp = 0;
						Monitor.Enter(this, ref flag);
						num2 = this._headIndex;
						int num3 = num - num2;
						if (num3 >= this._mask)
						{
							T[] array = new T[this._array.Length << 1];
							int num4 = num2 & this._mask;
							if (num4 == 0)
							{
								Array.Copy(this._array, 0, array, 0, this._array.Length);
							}
							else
							{
								Array.Copy(this._array, num4, array, 0, this._array.Length - num4);
								Array.Copy(this._array, 0, array, this._array.Length - num4, num4);
							}
							this._array = array;
							this._headIndex = 0;
							num = (this._tailIndex = num3);
							this._mask = (this._mask << 1) | 1;
						}
						this._array[num & this._mask] = item;
						this._tailIndex = num + 1;
						if (num3 == 0)
						{
							Interlocked.Increment(ref emptyToNonEmptyListTransitionCount);
						}
						this._addTakeCount -= this._stealCount;
						this._stealCount = 0;
					}
					checked
					{
						this._addTakeCount++;
					}
				}
				finally
				{
					this._currentOp = 0;
					if (flag)
					{
						Monitor.Exit(this);
					}
				}
			}

			// Token: 0x06002605 RID: 9733 RVA: 0x00085218 File Offset: 0x00083418
			internal void LocalClear()
			{
				lock (this)
				{
					if (this._headIndex < this._tailIndex)
					{
						this._headIndex = (this._tailIndex = 0);
						this._addTakeCount = (this._stealCount = 0);
						Array.Clear(this._array, 0, this._array.Length);
					}
				}
			}

			// Token: 0x06002606 RID: 9734 RVA: 0x0008529C File Offset: 0x0008349C
			internal bool TryLocalPop(out T result)
			{
				int num = this._tailIndex;
				if (this._headIndex >= num)
				{
					result = default(T);
					return false;
				}
				bool flag = false;
				bool flag2;
				try
				{
					this._currentOp = 2;
					Interlocked.Exchange(ref this._tailIndex, --num);
					if (!this._frozen && this._headIndex < num)
					{
						int num2 = num & this._mask;
						result = this._array[num2];
						this._array[num2] = default(T);
						this._addTakeCount--;
						flag2 = true;
					}
					else
					{
						this._currentOp = 0;
						Monitor.Enter(this, ref flag);
						if (this._headIndex <= num)
						{
							int num3 = num & this._mask;
							result = this._array[num3];
							this._array[num3] = default(T);
							this._addTakeCount--;
							flag2 = true;
						}
						else
						{
							this._tailIndex = num + 1;
							result = default(T);
							flag2 = false;
						}
					}
				}
				finally
				{
					this._currentOp = 0;
					if (flag)
					{
						Monitor.Exit(this);
					}
				}
				return flag2;
			}

			// Token: 0x06002607 RID: 9735 RVA: 0x000853E8 File Offset: 0x000835E8
			internal bool TryLocalPeek(out T result)
			{
				int tailIndex = this._tailIndex;
				if (this._headIndex < tailIndex)
				{
					lock (this)
					{
						if (this._headIndex < tailIndex)
						{
							result = this._array[(tailIndex - 1) & this._mask];
							return true;
						}
					}
				}
				result = default(T);
				return false;
			}

			// Token: 0x06002608 RID: 9736 RVA: 0x0008546C File Offset: 0x0008366C
			internal bool TrySteal(out T result, bool take)
			{
				lock (this)
				{
					int headIndex = this._headIndex;
					if (take)
					{
						if (headIndex < this._tailIndex - 1 && this._currentOp != 1)
						{
							SpinWait spinWait = default(SpinWait);
							do
							{
								spinWait.SpinOnce();
							}
							while (this._currentOp == 1);
						}
						Interlocked.Exchange(ref this._headIndex, headIndex + 1);
						if (headIndex < this._tailIndex)
						{
							int num = headIndex & this._mask;
							result = this._array[num];
							this._array[num] = default(T);
							this._stealCount++;
							return true;
						}
						this._headIndex = headIndex;
					}
					else if (headIndex < this._tailIndex)
					{
						result = this._array[headIndex & this._mask];
						return true;
					}
				}
				result = default(T);
				return false;
			}

			// Token: 0x06002609 RID: 9737 RVA: 0x0008558C File Offset: 0x0008378C
			internal int DangerousCopyTo(T[] array, int arrayIndex)
			{
				int headIndex = this._headIndex;
				int dangerousCount = this.DangerousCount;
				for (int i = arrayIndex + dangerousCount - 1; i >= arrayIndex; i--)
				{
					array[i] = this._array[headIndex++ & this._mask];
				}
				return dangerousCount;
			}

			// Token: 0x170007A4 RID: 1956
			// (get) Token: 0x0600260A RID: 9738 RVA: 0x000855DC File Offset: 0x000837DC
			internal int DangerousCount
			{
				get
				{
					return this._addTakeCount - this._stealCount;
				}
			}

			// Token: 0x040014F0 RID: 5360
			private const int InitialSize = 32;

			// Token: 0x040014F1 RID: 5361
			private const int StartIndex = 0;

			// Token: 0x040014F2 RID: 5362
			private volatile int _headIndex;

			// Token: 0x040014F3 RID: 5363
			private volatile int _tailIndex;

			// Token: 0x040014F4 RID: 5364
			private volatile T[] _array = new T[32];

			// Token: 0x040014F5 RID: 5365
			private volatile int _mask = 31;

			// Token: 0x040014F6 RID: 5366
			private int _addTakeCount;

			// Token: 0x040014F7 RID: 5367
			private int _stealCount;

			// Token: 0x040014F8 RID: 5368
			internal volatile int _currentOp;

			// Token: 0x040014F9 RID: 5369
			internal bool _frozen;

			// Token: 0x040014FA RID: 5370
			internal readonly ConcurrentBag<T>.WorkStealingQueue _nextQueue;

			// Token: 0x040014FB RID: 5371
			internal readonly int _ownerThreadId;
		}

		// Token: 0x020004A2 RID: 1186
		internal enum Operation
		{
			// Token: 0x040014FD RID: 5373
			None,
			// Token: 0x040014FE RID: 5374
			Add,
			// Token: 0x040014FF RID: 5375
			Take
		}

		// Token: 0x020004A3 RID: 1187
		[Serializable]
		private sealed class Enumerator : IEnumerator<T>, IDisposable, IEnumerator
		{
			// Token: 0x0600260B RID: 9739 RVA: 0x000855EB File Offset: 0x000837EB
			public Enumerator(T[] array)
			{
				this._array = array;
			}

			// Token: 0x0600260C RID: 9740 RVA: 0x000855FC File Offset: 0x000837FC
			public bool MoveNext()
			{
				if (this._index < this._array.Length)
				{
					T[] array = this._array;
					int index = this._index;
					this._index = index + 1;
					this._current = array[index];
					return true;
				}
				this._index = this._array.Length + 1;
				return false;
			}

			// Token: 0x170007A5 RID: 1957
			// (get) Token: 0x0600260D RID: 9741 RVA: 0x0008564E File Offset: 0x0008384E
			public T Current
			{
				get
				{
					return this._current;
				}
			}

			// Token: 0x170007A6 RID: 1958
			// (get) Token: 0x0600260E RID: 9742 RVA: 0x00085656 File Offset: 0x00083856
			object IEnumerator.Current
			{
				get
				{
					if (this._index == 0 || this._index == this._array.Length + 1)
					{
						throw new InvalidOperationException("Enumeration has either not started or has already finished.");
					}
					return this.Current;
				}
			}

			// Token: 0x0600260F RID: 9743 RVA: 0x00085688 File Offset: 0x00083888
			public void Reset()
			{
				this._index = 0;
				this._current = default(T);
			}

			// Token: 0x06002610 RID: 9744 RVA: 0x00003917 File Offset: 0x00001B17
			public void Dispose()
			{
			}

			// Token: 0x04001500 RID: 5376
			private readonly T[] _array;

			// Token: 0x04001501 RID: 5377
			private T _current;

			// Token: 0x04001502 RID: 5378
			private int _index;
		}
	}
}
