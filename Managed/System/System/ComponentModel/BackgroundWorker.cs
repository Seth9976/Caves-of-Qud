using System;
using System.Threading;
using System.Threading.Tasks;

namespace System.ComponentModel
{
	/// <summary>Executes an operation on a separate thread.</summary>
	// Token: 0x02000366 RID: 870
	[DefaultEvent("DoWork")]
	public class BackgroundWorker : Component
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.BackgroundWorker" /> class.</summary>
		// Token: 0x06001CC5 RID: 7365 RVA: 0x00067B18 File Offset: 0x00065D18
		public BackgroundWorker()
		{
			this._operationCompleted = new SendOrPostCallback(this.AsyncOperationCompleted);
			this._progressReporter = new SendOrPostCallback(this.ProgressReporter);
		}

		// Token: 0x06001CC6 RID: 7366 RVA: 0x00067B44 File Offset: 0x00065D44
		private void AsyncOperationCompleted(object arg)
		{
			this._isRunning = false;
			this._cancellationPending = false;
			this.OnRunWorkerCompleted((RunWorkerCompletedEventArgs)arg);
		}

		/// <summary>Gets a value indicating whether the application has requested cancellation of a background operation.</summary>
		/// <returns>true if the application has requested cancellation of a background operation; otherwise, false. The default is false.</returns>
		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001CC7 RID: 7367 RVA: 0x00067B60 File Offset: 0x00065D60
		public bool CancellationPending
		{
			get
			{
				return this._cancellationPending;
			}
		}

		/// <summary>Requests cancellation of a pending background operation.</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.ComponentModel.BackgroundWorker.WorkerSupportsCancellation" /> is false. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001CC8 RID: 7368 RVA: 0x00067B68 File Offset: 0x00065D68
		public void CancelAsync()
		{
			if (!this.WorkerSupportsCancellation)
			{
				throw new InvalidOperationException("This BackgroundWorker states that it doesn't support cancellation. Modify WorkerSupportsCancellation to state that it does support cancellation.");
			}
			this._cancellationPending = true;
		}

		/// <summary>Occurs when <see cref="M:System.ComponentModel.BackgroundWorker.RunWorkerAsync" /> is called.</summary>
		// Token: 0x14000025 RID: 37
		// (add) Token: 0x06001CC9 RID: 7369 RVA: 0x00067B84 File Offset: 0x00065D84
		// (remove) Token: 0x06001CCA RID: 7370 RVA: 0x00067BBC File Offset: 0x00065DBC
		public event DoWorkEventHandler DoWork;

		/// <summary>Gets a value indicating whether the <see cref="T:System.ComponentModel.BackgroundWorker" /> is running an asynchronous operation.</summary>
		/// <returns>true, if the <see cref="T:System.ComponentModel.BackgroundWorker" /> is running an asynchronous operation; otherwise, false.</returns>
		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001CCB RID: 7371 RVA: 0x00067BF1 File Offset: 0x00065DF1
		public bool IsBusy
		{
			get
			{
				return this._isRunning;
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BackgroundWorker.DoWork" /> event. </summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data.</param>
		// Token: 0x06001CCC RID: 7372 RVA: 0x00067BFC File Offset: 0x00065DFC
		protected virtual void OnDoWork(DoWorkEventArgs e)
		{
			DoWorkEventHandler doWork = this.DoWork;
			if (doWork != null)
			{
				doWork(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BackgroundWorker.RunWorkerCompleted" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001CCD RID: 7373 RVA: 0x00067C1C File Offset: 0x00065E1C
		protected virtual void OnRunWorkerCompleted(RunWorkerCompletedEventArgs e)
		{
			RunWorkerCompletedEventHandler runWorkerCompleted = this.RunWorkerCompleted;
			if (runWorkerCompleted != null)
			{
				runWorkerCompleted(this, e);
			}
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BackgroundWorker.ProgressChanged" /> event.</summary>
		/// <param name="e">An <see cref="T:System.EventArgs" /> that contains the event data. </param>
		// Token: 0x06001CCE RID: 7374 RVA: 0x00067C3C File Offset: 0x00065E3C
		protected virtual void OnProgressChanged(ProgressChangedEventArgs e)
		{
			ProgressChangedEventHandler progressChanged = this.ProgressChanged;
			if (progressChanged != null)
			{
				progressChanged(this, e);
			}
		}

		/// <summary>Occurs when <see cref="M:System.ComponentModel.BackgroundWorker.ReportProgress(System.Int32)" /> is called.</summary>
		// Token: 0x14000026 RID: 38
		// (add) Token: 0x06001CCF RID: 7375 RVA: 0x00067C5C File Offset: 0x00065E5C
		// (remove) Token: 0x06001CD0 RID: 7376 RVA: 0x00067C94 File Offset: 0x00065E94
		public event ProgressChangedEventHandler ProgressChanged;

		// Token: 0x06001CD1 RID: 7377 RVA: 0x00067CC9 File Offset: 0x00065EC9
		private void ProgressReporter(object arg)
		{
			this.OnProgressChanged((ProgressChangedEventArgs)arg);
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BackgroundWorker.ProgressChanged" /> event.</summary>
		/// <param name="percentProgress">The percentage, from 0 to 100, of the background operation that is complete. </param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.ComponentModel.BackgroundWorker.WorkerReportsProgress" /> property is set to false. </exception>
		// Token: 0x06001CD2 RID: 7378 RVA: 0x00067CD7 File Offset: 0x00065ED7
		public void ReportProgress(int percentProgress)
		{
			this.ReportProgress(percentProgress, null);
		}

		/// <summary>Raises the <see cref="E:System.ComponentModel.BackgroundWorker.ProgressChanged" /> event.</summary>
		/// <param name="percentProgress">The percentage, from 0 to 100, of the background operation that is complete.</param>
		/// <param name="userState">The state object passed to <see cref="M:System.ComponentModel.BackgroundWorker.RunWorkerAsync(System.Object)" />.</param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.ComponentModel.BackgroundWorker.WorkerReportsProgress" /> property is set to false. </exception>
		// Token: 0x06001CD3 RID: 7379 RVA: 0x00067CE4 File Offset: 0x00065EE4
		public void ReportProgress(int percentProgress, object userState)
		{
			if (!this.WorkerReportsProgress)
			{
				throw new InvalidOperationException("This BackgroundWorker states that it doesn't report progress. Modify WorkerReportsProgress to state that it does report progress.");
			}
			ProgressChangedEventArgs e = new ProgressChangedEventArgs(percentProgress, userState);
			if (this._asyncOperation != null)
			{
				this._asyncOperation.Post(this._progressReporter, e);
				return;
			}
			this._progressReporter(e);
		}

		/// <summary>Starts execution of a background operation.</summary>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.ComponentModel.BackgroundWorker.IsBusy" /> is true.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001CD4 RID: 7380 RVA: 0x00067D33 File Offset: 0x00065F33
		public void RunWorkerAsync()
		{
			this.RunWorkerAsync(null);
		}

		/// <summary>Starts execution of a background operation.</summary>
		/// <param name="argument">A parameter for use by the background operation to be executed in the <see cref="E:System.ComponentModel.BackgroundWorker.DoWork" /> event handler. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="P:System.ComponentModel.BackgroundWorker.IsBusy" /> is true. </exception>
		// Token: 0x06001CD5 RID: 7381 RVA: 0x00067D3C File Offset: 0x00065F3C
		public void RunWorkerAsync(object argument)
		{
			if (this._isRunning)
			{
				throw new InvalidOperationException("This BackgroundWorker is currently busy and cannot run multiple tasks concurrently.");
			}
			this._isRunning = true;
			this._cancellationPending = false;
			this._asyncOperation = AsyncOperationManager.CreateOperation(null);
			Task.Factory.StartNew(delegate(object arg)
			{
				this.WorkerThreadStart(arg);
			}, argument, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
		}

		/// <summary>Occurs when the background operation has completed, has been canceled, or has raised an exception.</summary>
		// Token: 0x14000027 RID: 39
		// (add) Token: 0x06001CD6 RID: 7382 RVA: 0x00067D9C File Offset: 0x00065F9C
		// (remove) Token: 0x06001CD7 RID: 7383 RVA: 0x00067DD4 File Offset: 0x00065FD4
		public event RunWorkerCompletedEventHandler RunWorkerCompleted;

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.ComponentModel.BackgroundWorker" /> can report progress updates.</summary>
		/// <returns>true if the <see cref="T:System.ComponentModel.BackgroundWorker" /> supports progress updates; otherwise false. The default is false.</returns>
		// Token: 0x170005CD RID: 1485
		// (get) Token: 0x06001CD8 RID: 7384 RVA: 0x00067E09 File Offset: 0x00066009
		// (set) Token: 0x06001CD9 RID: 7385 RVA: 0x00067E11 File Offset: 0x00066011
		public bool WorkerReportsProgress
		{
			get
			{
				return this._workerReportsProgress;
			}
			set
			{
				this._workerReportsProgress = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the <see cref="T:System.ComponentModel.BackgroundWorker" /> supports asynchronous cancellation.</summary>
		/// <returns>true if the <see cref="T:System.ComponentModel.BackgroundWorker" /> supports cancellation; otherwise false. The default is false.</returns>
		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x06001CDA RID: 7386 RVA: 0x00067E1A File Offset: 0x0006601A
		// (set) Token: 0x06001CDB RID: 7387 RVA: 0x00067E22 File Offset: 0x00066022
		public bool WorkerSupportsCancellation
		{
			get
			{
				return this._canCancelWorker;
			}
			set
			{
				this._canCancelWorker = value;
			}
		}

		// Token: 0x06001CDC RID: 7388 RVA: 0x00067E2C File Offset: 0x0006602C
		private void WorkerThreadStart(object argument)
		{
			object obj = null;
			Exception ex = null;
			bool flag = false;
			try
			{
				DoWorkEventArgs e = new DoWorkEventArgs(argument);
				this.OnDoWork(e);
				if (e.Cancel)
				{
					flag = true;
				}
				else
				{
					obj = e.Result;
				}
			}
			catch (Exception ex)
			{
			}
			RunWorkerCompletedEventArgs e2 = new RunWorkerCompletedEventArgs(obj, ex, flag);
			this._asyncOperation.PostOperationCompleted(this._operationCompleted, e2);
		}

		// Token: 0x06001CDD RID: 7389 RVA: 0x00003917 File Offset: 0x00001B17
		protected override void Dispose(bool disposing)
		{
		}

		// Token: 0x04000EB6 RID: 3766
		private bool _canCancelWorker;

		// Token: 0x04000EB7 RID: 3767
		private bool _workerReportsProgress;

		// Token: 0x04000EB8 RID: 3768
		private bool _cancellationPending;

		// Token: 0x04000EB9 RID: 3769
		private bool _isRunning;

		// Token: 0x04000EBA RID: 3770
		private AsyncOperation _asyncOperation;

		// Token: 0x04000EBB RID: 3771
		private readonly SendOrPostCallback _operationCompleted;

		// Token: 0x04000EBC RID: 3772
		private readonly SendOrPostCallback _progressReporter;
	}
}
