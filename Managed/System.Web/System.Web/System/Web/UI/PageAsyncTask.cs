using System;
using System.Threading;
using System.Threading.Tasks;
using Unity;

namespace System.Web.UI
{
	/// <summary>Contains information about an asynchronous task registered to a page. This class cannot be inherited.</summary>
	// Token: 0x0200020A RID: 522
	public sealed class PageAsyncTask
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PageAsyncTask" /> class using the default value for executing in parallel. </summary>
		/// <param name="beginHandler">The handler to call when beginning an asynchronous task.</param>
		/// <param name="endHandler">The handler to call when the task is completed successfully within the time-out period.</param>
		/// <param name="timeoutHandler">The handler to call when the task is not completed successfully within the time-out period.</param>
		/// <param name="state">The object that represents the state of the task.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="beginHandler" /> parameter or <paramref name="endHandler" /> parameter is not specified.</exception>
		// Token: 0x06001544 RID: 5444 RVA: 0x000379D8 File Offset: 0x00035BD8
		public PageAsyncTask(BeginEventHandler beginHandler, EndEventHandler endHandler, EndEventHandler timeoutHandler, object state)
			: this(beginHandler, endHandler, timeoutHandler, state, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PageAsyncTask" /> class using the specified value for executing in parallel. </summary>
		/// <param name="beginHandler">The handler to call when beginning an asynchronous task.</param>
		/// <param name="endHandler">The handler to call when the task is completed successfully within the time-out period.</param>
		/// <param name="timeoutHandler">The handler to call when the task is not completed successfully within the time-out period.</param>
		/// <param name="state">The object that represents the state of the task.</param>
		/// <param name="executeInParallel">The value that indicates whether the task can be processed in parallel with other tasks.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="beginHandler" /> parameter or <paramref name="endHandler" /> parameter is not specified.</exception>
		// Token: 0x06001545 RID: 5445 RVA: 0x000379E6 File Offset: 0x00035BE6
		public PageAsyncTask(BeginEventHandler beginHandler, EndEventHandler endHandler, EndEventHandler timeoutHandler, object state, bool executeInParallel)
		{
			this.beginHandler = beginHandler;
			this.endHandler = endHandler;
			this.timeoutHandler = timeoutHandler;
			this.state = state;
			this.executeInParallel = executeInParallel;
		}

		/// <summary>Gets the method to call when beginning an asynchronous task.</summary>
		/// <returns>A <see cref="T:System.Web.BeginEventHandler" /> delegate that represents the method to call when beginning the asynchronous task. </returns>
		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06001546 RID: 5446 RVA: 0x00037A13 File Offset: 0x00035C13
		public BeginEventHandler BeginHandler
		{
			get
			{
				return this.beginHandler;
			}
		}

		/// <summary>Gets the method to call when the task completes successfully within the time-out period.</summary>
		/// <returns>An <see cref="T:System.Web.EndEventHandler" /> delegate that represents the method to call when the task completes successfully within the time-out period.</returns>
		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06001547 RID: 5447 RVA: 0x00037A1B File Offset: 0x00035C1B
		public EndEventHandler EndHandler
		{
			get
			{
				return this.endHandler;
			}
		}

		/// <summary>Gets the method to call when the task does not complete successfully within the time-out period.</summary>
		/// <returns>An <see cref="T:System.Web.EndEventHandler" /> delegate that represents the method to call when the task does not complete successfully within the time-out period.</returns>
		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06001548 RID: 5448 RVA: 0x00037A23 File Offset: 0x00035C23
		public EndEventHandler TimeoutHandler
		{
			get
			{
				return this.timeoutHandler;
			}
		}

		/// <summary>Gets a value that indicates whether the task can be processed in parallel with other tasks.</summary>
		/// <returns>true if the task should be processed in parallel with other tasks; otherwise, false.</returns>
		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06001549 RID: 5449 RVA: 0x00037A2B File Offset: 0x00035C2B
		public bool ExecuteInParallel
		{
			get
			{
				return this.executeInParallel;
			}
		}

		/// <summary>Gets an object that represents the state of the task.</summary>
		/// <returns>An <see cref="T:System.Object" /> that represents the state of the task.</returns>
		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x0600154A RID: 5450 RVA: 0x00037A33 File Offset: 0x00035C33
		public object State
		{
			get
			{
				return this.state;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PageAsyncTask" /> class using an event handler that enables the task to be canceled.</summary>
		/// <param name="handler">An event handler.</param>
		// Token: 0x0600154B RID: 5451 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public PageAsyncTask(Func<CancellationToken, Task> handler)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Web.UI.PageAsyncTask" /> class using an event handler that enables the task to be handled.</summary>
		/// <param name="handler">An event handler.</param>
		// Token: 0x0600154C RID: 5452 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public PageAsyncTask(Func<Task> handler)
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001506 RID: 5382
		private BeginEventHandler beginHandler;

		// Token: 0x04001507 RID: 5383
		private EndEventHandler endHandler;

		// Token: 0x04001508 RID: 5384
		private EndEventHandler timeoutHandler;

		// Token: 0x04001509 RID: 5385
		private bool executeInParallel;

		// Token: 0x0400150A RID: 5386
		private object state;
	}
}
