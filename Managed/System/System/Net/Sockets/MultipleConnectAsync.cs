using System;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace System.Net.Sockets
{
	// Token: 0x02000791 RID: 1937
	internal abstract class MultipleConnectAsync
	{
		// Token: 0x06003CE0 RID: 15584 RVA: 0x000CEF70 File Offset: 0x000CD170
		public bool StartConnectAsync(SocketAsyncEventArgs args, DnsEndPoint endPoint)
		{
			object lockObject = this._lockObject;
			bool flag2;
			lock (lockObject)
			{
				if (endPoint.AddressFamily != AddressFamily.Unspecified && endPoint.AddressFamily != AddressFamily.InterNetwork && endPoint.AddressFamily != AddressFamily.InterNetworkV6)
				{
					NetEventSource.Fail(this, FormattableStringFactory.Create("Unexpected endpoint address family: {0}", new object[] { endPoint.AddressFamily }), "StartConnectAsync");
				}
				this._userArgs = args;
				this._endPoint = endPoint;
				if (this._state == MultipleConnectAsync.State.Canceled)
				{
					this.SyncFail(new SocketException(995));
					flag2 = false;
				}
				else
				{
					if (this._state != MultipleConnectAsync.State.NotStarted)
					{
						NetEventSource.Fail(this, "MultipleConnectAsync.StartConnectAsync(): Unexpected object state", "StartConnectAsync");
					}
					this._state = MultipleConnectAsync.State.DnsQuery;
					IAsyncResult asyncResult = Dns.BeginGetHostAddresses(endPoint.Host, new AsyncCallback(this.DnsCallback), null);
					if (asyncResult.CompletedSynchronously)
					{
						flag2 = this.DoDnsCallback(asyncResult, true);
					}
					else
					{
						flag2 = true;
					}
				}
			}
			return flag2;
		}

		// Token: 0x06003CE1 RID: 15585 RVA: 0x000CF068 File Offset: 0x000CD268
		private void DnsCallback(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				this.DoDnsCallback(result, false);
			}
		}

		// Token: 0x06003CE2 RID: 15586 RVA: 0x000CF07C File Offset: 0x000CD27C
		private bool DoDnsCallback(IAsyncResult result, bool sync)
		{
			Exception ex = null;
			object lockObject = this._lockObject;
			lock (lockObject)
			{
				if (this._state == MultipleConnectAsync.State.Canceled)
				{
					return true;
				}
				if (this._state != MultipleConnectAsync.State.DnsQuery)
				{
					NetEventSource.Fail(this, "MultipleConnectAsync.DoDnsCallback(): Unexpected object state", "DoDnsCallback");
				}
				try
				{
					this._addressList = Dns.EndGetHostAddresses(result);
					if (this._addressList == null)
					{
						NetEventSource.Fail(this, "MultipleConnectAsync.DoDnsCallback(): EndGetHostAddresses returned null!", "DoDnsCallback");
					}
				}
				catch (Exception ex2)
				{
					this._state = MultipleConnectAsync.State.Completed;
					ex = ex2;
				}
				if (ex == null)
				{
					this._state = MultipleConnectAsync.State.ConnectAttempt;
					this._internalArgs = new SocketAsyncEventArgs();
					this._internalArgs.Completed += this.InternalConnectCallback;
					this._internalArgs.CopyBufferFrom(this._userArgs);
					ex = this.AttemptConnection();
					if (ex != null)
					{
						this._state = MultipleConnectAsync.State.Completed;
					}
				}
			}
			return ex == null || this.Fail(sync, ex);
		}

		// Token: 0x06003CE3 RID: 15587 RVA: 0x000CF17C File Offset: 0x000CD37C
		private void InternalConnectCallback(object sender, SocketAsyncEventArgs args)
		{
			Exception ex = null;
			object lockObject = this._lockObject;
			lock (lockObject)
			{
				if (this._state == MultipleConnectAsync.State.Canceled)
				{
					ex = new SocketException(995);
				}
				else if (args.SocketError == SocketError.Success)
				{
					this._state = MultipleConnectAsync.State.Completed;
				}
				else if (args.SocketError == SocketError.OperationAborted)
				{
					ex = new SocketException(995);
					this._state = MultipleConnectAsync.State.Canceled;
				}
				else
				{
					SocketError socketError = args.SocketError;
					args.in_progress = 0;
					Exception ex2 = this.AttemptConnection();
					if (ex2 == null)
					{
						return;
					}
					SocketException ex3 = ex2 as SocketException;
					if (ex3 != null && ex3.SocketErrorCode == SocketError.NoData)
					{
						ex = new SocketException((int)socketError);
					}
					else
					{
						ex = ex2;
					}
					this._state = MultipleConnectAsync.State.Completed;
				}
			}
			if (ex == null)
			{
				this.Succeed();
				return;
			}
			this.AsyncFail(ex);
		}

		// Token: 0x06003CE4 RID: 15588 RVA: 0x000CF260 File Offset: 0x000CD460
		private Exception AttemptConnection()
		{
			Exception ex;
			try
			{
				Socket socket;
				IPAddress nextAddress = this.GetNextAddress(out socket);
				if (nextAddress == null)
				{
					ex = new SocketException(11004);
				}
				else
				{
					this._internalArgs.RemoteEndPoint = new IPEndPoint(nextAddress, this._endPoint.Port);
					ex = this.AttemptConnection(socket, this._internalArgs);
				}
			}
			catch (Exception ex2)
			{
				if (ex2 is ObjectDisposedException)
				{
					NetEventSource.Fail(this, "unexpected ObjectDisposedException", "AttemptConnection");
				}
				ex = ex2;
			}
			return ex;
		}

		// Token: 0x06003CE5 RID: 15589 RVA: 0x000CF2E0 File Offset: 0x000CD4E0
		private Exception AttemptConnection(Socket attemptSocket, SocketAsyncEventArgs args)
		{
			try
			{
				if (attemptSocket == null)
				{
					NetEventSource.Fail(null, "attemptSocket is null!", "AttemptConnection");
				}
				if (!attemptSocket.ConnectAsync(args))
				{
					this.InternalConnectCallback(null, args);
				}
			}
			catch (ObjectDisposedException)
			{
				return new SocketException(995);
			}
			catch (Exception ex)
			{
				return ex;
			}
			return null;
		}

		// Token: 0x06003CE6 RID: 15590
		protected abstract void OnSucceed();

		// Token: 0x06003CE7 RID: 15591 RVA: 0x000CF344 File Offset: 0x000CD544
		private void Succeed()
		{
			this.OnSucceed();
			this._userArgs.FinishWrapperConnectSuccess(this._internalArgs.ConnectSocket, this._internalArgs.BytesTransferred, this._internalArgs.SocketFlags);
			this._internalArgs.Dispose();
		}

		// Token: 0x06003CE8 RID: 15592
		protected abstract void OnFail(bool abortive);

		// Token: 0x06003CE9 RID: 15593 RVA: 0x000CF383 File Offset: 0x000CD583
		private bool Fail(bool sync, Exception e)
		{
			if (sync)
			{
				this.SyncFail(e);
				return false;
			}
			this.AsyncFail(e);
			return true;
		}

		// Token: 0x06003CEA RID: 15594 RVA: 0x000CF39C File Offset: 0x000CD59C
		private void SyncFail(Exception e)
		{
			this.OnFail(false);
			if (this._internalArgs != null)
			{
				this._internalArgs.Dispose();
			}
			SocketException ex = e as SocketException;
			if (ex != null)
			{
				this._userArgs.FinishConnectByNameSyncFailure(ex, 0, SocketFlags.None);
				return;
			}
			ExceptionDispatchInfo.Throw(e);
		}

		// Token: 0x06003CEB RID: 15595 RVA: 0x000CF3E2 File Offset: 0x000CD5E2
		private void AsyncFail(Exception e)
		{
			this.OnFail(false);
			if (this._internalArgs != null)
			{
				this._internalArgs.Dispose();
			}
			this._userArgs.FinishOperationAsyncFailure(e, 0, SocketFlags.None);
		}

		// Token: 0x06003CEC RID: 15596 RVA: 0x000CF40C File Offset: 0x000CD60C
		public void Cancel()
		{
			bool flag = false;
			object lockObject = this._lockObject;
			lock (lockObject)
			{
				switch (this._state)
				{
				case MultipleConnectAsync.State.NotStarted:
					flag = true;
					break;
				case MultipleConnectAsync.State.DnsQuery:
					Task.Factory.StartNew(delegate(object s)
					{
						this.CallAsyncFail(s);
					}, null, CancellationToken.None, TaskCreationOptions.DenyChildAttach, TaskScheduler.Default);
					flag = true;
					break;
				case MultipleConnectAsync.State.ConnectAttempt:
					flag = true;
					break;
				case MultipleConnectAsync.State.Completed:
					break;
				default:
					NetEventSource.Fail(this, "Unexpected object state", "Cancel");
					break;
				}
				this._state = MultipleConnectAsync.State.Canceled;
			}
			if (flag)
			{
				this.OnFail(true);
			}
		}

		// Token: 0x06003CED RID: 15597 RVA: 0x000CF4B8 File Offset: 0x000CD6B8
		private void CallAsyncFail(object ignored)
		{
			this.AsyncFail(new SocketException(995));
		}

		// Token: 0x06003CEE RID: 15598
		protected abstract IPAddress GetNextAddress(out Socket attemptSocket);

		// Token: 0x0400242C RID: 9260
		protected SocketAsyncEventArgs _userArgs;

		// Token: 0x0400242D RID: 9261
		protected SocketAsyncEventArgs _internalArgs;

		// Token: 0x0400242E RID: 9262
		protected DnsEndPoint _endPoint;

		// Token: 0x0400242F RID: 9263
		protected IPAddress[] _addressList;

		// Token: 0x04002430 RID: 9264
		protected int _nextAddress;

		// Token: 0x04002431 RID: 9265
		private MultipleConnectAsync.State _state;

		// Token: 0x04002432 RID: 9266
		private object _lockObject = new object();

		// Token: 0x02000792 RID: 1938
		private enum State
		{
			// Token: 0x04002434 RID: 9268
			NotStarted,
			// Token: 0x04002435 RID: 9269
			DnsQuery,
			// Token: 0x04002436 RID: 9270
			ConnectAttempt,
			// Token: 0x04002437 RID: 9271
			Completed,
			// Token: 0x04002438 RID: 9272
			Canceled
		}
	}
}
