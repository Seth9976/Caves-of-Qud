using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;

namespace System
{
	/// <summary>Represents a typed weak reference, which references an object while still allowing that object to be reclaimed by garbage collection.</summary>
	/// <typeparam name="T">The type of the object referenced.</typeparam>
	// Token: 0x0200026B RID: 619
	[Serializable]
	public sealed class WeakReference<T> : ISerializable where T : class
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.WeakReference`1" /> class that references the specified object.</summary>
		/// <param name="target">The object to reference, or null.</param>
		// Token: 0x06001C23 RID: 7203 RVA: 0x00069370 File Offset: 0x00067570
		public WeakReference(T target)
			: this(target, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.WeakReference`1" /> class that references the specified object and uses the specified resurrection tracking.</summary>
		/// <param name="target">The object to reference, or null.</param>
		/// <param name="trackResurrection">true to track the object after finalization; false to track the object only until finalization.</param>
		// Token: 0x06001C24 RID: 7204 RVA: 0x0006937C File Offset: 0x0006757C
		public WeakReference(T target, bool trackResurrection)
		{
			this.trackResurrection = trackResurrection;
			GCHandleType gchandleType = (trackResurrection ? GCHandleType.WeakTrackResurrection : GCHandleType.Weak);
			this.handle = GCHandle.Alloc(target, gchandleType);
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x000693B0 File Offset: 0x000675B0
		private WeakReference(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this.trackResurrection = info.GetBoolean("TrackResurrection");
			object value = info.GetValue("TrackedObject", typeof(T));
			GCHandleType gchandleType = (this.trackResurrection ? GCHandleType.WeakTrackResurrection : GCHandleType.Weak);
			this.handle = GCHandle.Alloc(value, gchandleType);
		}

		/// <summary>Populates a <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with all the data necessary to serialize the current <see cref="T:System.WeakReference`1" /> object.</summary>
		/// <param name="info">An object that holds all the data necessary to serialize or deserialize the current <see cref="T:System.WeakReference`1" /> object.</param>
		/// <param name="context">The location where serialized data is stored and retrieved.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is null. </exception>
		// Token: 0x06001C26 RID: 7206 RVA: 0x00069414 File Offset: 0x00067614
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("TrackResurrection", this.trackResurrection);
			if (this.handle.IsAllocated)
			{
				info.AddValue("TrackedObject", this.handle.Target);
				return;
			}
			info.AddValue("TrackedObject", null);
		}

		/// <summary>Sets the target object that is referenced by this <see cref="T:System.WeakReference`1" /> object.</summary>
		/// <param name="target">The new target object.</param>
		// Token: 0x06001C27 RID: 7207 RVA: 0x00069470 File Offset: 0x00067670
		public void SetTarget(T target)
		{
			this.handle.Target = target;
		}

		/// <summary>Tries to retrieve the target object that is referenced by the current <see cref="T:System.WeakReference`1" /> object.</summary>
		/// <returns>true if the target was retrieved; otherwise, false.</returns>
		/// <param name="target">When this method returns, contains the target object, if it is available. This parameter is treated as uninitialized.</param>
		// Token: 0x06001C28 RID: 7208 RVA: 0x00069483 File Offset: 0x00067683
		public bool TryGetTarget(out T target)
		{
			if (!this.handle.IsAllocated)
			{
				target = default(T);
				return false;
			}
			target = (T)((object)this.handle.Target);
			return target != null;
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x000694C0 File Offset: 0x000676C0
		~WeakReference()
		{
			this.handle.Free();
		}

		// Token: 0x040019BE RID: 6590
		private GCHandle handle;

		// Token: 0x040019BF RID: 6591
		private bool trackResurrection;
	}
}
