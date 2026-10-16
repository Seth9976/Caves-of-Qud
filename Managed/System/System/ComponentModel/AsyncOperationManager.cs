using System;
using System.Threading;

namespace System.ComponentModel
{
	/// <summary>Provides concurrency management for classes that support asynchronous method calls. This class cannot be inherited.</summary>
	// Token: 0x02000365 RID: 869
	public static class AsyncOperationManager
	{
		/// <summary>Returns an <see cref="T:System.ComponentModel.AsyncOperation" /> for tracking the duration of a particular asynchronous operation.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.AsyncOperation" /> that you can use to track the duration of an asynchronous method invocation.</returns>
		/// <param name="userSuppliedState">An object used to associate a piece of client state, such as a task ID, with a particular asynchronous operation. </param>
		// Token: 0x06001CC2 RID: 7362 RVA: 0x00067AEB File Offset: 0x00065CEB
		public static AsyncOperation CreateOperation(object userSuppliedState)
		{
			return AsyncOperation.CreateOperation(userSuppliedState, AsyncOperationManager.SynchronizationContext);
		}

		/// <summary>Gets or sets the synchronization context for the asynchronous operation.</summary>
		/// <returns>The synchronization context for the asynchronous operation.</returns>
		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001CC3 RID: 7363 RVA: 0x00067AF8 File Offset: 0x00065CF8
		// (set) Token: 0x06001CC4 RID: 7364 RVA: 0x00067B10 File Offset: 0x00065D10
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static SynchronizationContext SynchronizationContext
		{
			get
			{
				if (SynchronizationContext.Current == null)
				{
					SynchronizationContext.SetSynchronizationContext(new SynchronizationContext());
				}
				return SynchronizationContext.Current;
			}
			set
			{
				SynchronizationContext.SetSynchronizationContext(value);
			}
		}
	}
}
