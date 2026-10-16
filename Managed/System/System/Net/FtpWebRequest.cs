using System;
using System.IO;
using System.Net.Cache;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Unity;

namespace System.Net
{
	/// <summary>Implements a File Transfer Protocol (FTP) client.</summary>
	// Token: 0x02000591 RID: 1425
	public sealed class FtpWebRequest : WebRequest
	{
		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06002E15 RID: 11797 RVA: 0x0009EC78 File Offset: 0x0009CE78
		internal FtpMethodInfo MethodInfo
		{
			get
			{
				return this._methodInfo;
			}
		}

		/// <summary>Defines the default cache policy for all FTP requests.</summary>
		/// <returns>A <see cref="T:System.Net.Cache.RequestCachePolicy" /> that defines the cache policy for FTP requests.</returns>
		/// <exception cref="T:System.ArgumentNullException">The caller tried to set this property to null.</exception>
		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06002E16 RID: 11798 RVA: 0x0009EC80 File Offset: 0x0009CE80
		// (set) Token: 0x06002E17 RID: 11799 RVA: 0x00003917 File Offset: 0x00001B17
		public new static RequestCachePolicy DefaultCachePolicy
		{
			get
			{
				return WebRequest.DefaultCachePolicy;
			}
			set
			{
			}
		}

		/// <summary>Gets or sets the command to send to the FTP server.</summary>
		/// <returns>A <see cref="T:System.String" /> value that contains the FTP command to send to the server. The default value is <see cref="F:System.Net.WebRequestMethods.Ftp.DownloadFile" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress. </exception>
		/// <exception cref="T:System.ArgumentException">The method is invalid.- or -The method is not supported.- or -Multiple methods were specified.</exception>
		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06002E18 RID: 11800 RVA: 0x0009EC87 File Offset: 0x0009CE87
		// (set) Token: 0x06002E19 RID: 11801 RVA: 0x0009EC94 File Offset: 0x0009CE94
		public override string Method
		{
			get
			{
				return this._methodInfo.Method;
			}
			set
			{
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentException("FTP Method names cannot be null or empty.", "value");
				}
				if (this.InUse)
				{
					throw new InvalidOperationException("This operation cannot be performed after the request has been submitted.");
				}
				try
				{
					this._methodInfo = FtpMethodInfo.GetMethodInfo(value);
				}
				catch (ArgumentException)
				{
					throw new ArgumentException("This method is not supported.", "value");
				}
			}
		}

		/// <summary>Gets or sets the new name of a file being renamed.</summary>
		/// <returns>The new name of the file being renamed.</returns>
		/// <exception cref="T:System.ArgumentException">The value specified for a set operation is null or an empty string.</exception>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress. </exception>
		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06002E1A RID: 11802 RVA: 0x0009ECFC File Offset: 0x0009CEFC
		// (set) Token: 0x06002E1B RID: 11803 RVA: 0x0009ED04 File Offset: 0x0009CF04
		public string RenameTo
		{
			get
			{
				return this._renameTo;
			}
			set
			{
				if (this.InUse)
				{
					throw new InvalidOperationException("This operation cannot be performed after the request has been submitted.");
				}
				if (string.IsNullOrEmpty(value))
				{
					throw new ArgumentException("The RenameTo filename cannot be null or empty.", "value");
				}
				this._renameTo = value;
			}
		}

		/// <summary>Gets or sets the credentials used to communicate with the FTP server.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentials" /> instance; otherwise, null if the property has not been set.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is null.</exception>
		/// <exception cref="T:System.ArgumentException">An <see cref="T:System.Net.ICredentials" /> of a type other than <see cref="T:System.Net.NetworkCredential" /> was specified for a set operation.</exception>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06002E1C RID: 11804 RVA: 0x0009ED38 File Offset: 0x0009CF38
		// (set) Token: 0x06002E1D RID: 11805 RVA: 0x0009ED40 File Offset: 0x0009CF40
		public override ICredentials Credentials
		{
			get
			{
				return this._authInfo;
			}
			set
			{
				if (this.InUse)
				{
					throw new InvalidOperationException("This operation cannot be performed after the request has been submitted.");
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value == CredentialCache.DefaultNetworkCredentials)
				{
					throw new ArgumentException("Default credentials are not supported on an FTP request.", "value");
				}
				this._authInfo = value;
			}
		}

		/// <summary>Gets the URI requested by this instance.</summary>
		/// <returns>A <see cref="T:System.Uri" /> instance that identifies a resource that is accessed using the File Transfer Protocol.</returns>
		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06002E1E RID: 11806 RVA: 0x0009ED8D File Offset: 0x0009CF8D
		public override Uri RequestUri
		{
			get
			{
				return this._uri;
			}
		}

		/// <summary>Gets or sets the number of milliseconds to wait for a request.</summary>
		/// <returns>An <see cref="T:System.Int32" /> value that contains the number of milliseconds to wait before a request times out. The default value is <see cref="F:System.Threading.Timeout.Infinite" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified is less than zero and is not <see cref="F:System.Threading.Timeout.Infinite" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress. </exception>
		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06002E1F RID: 11807 RVA: 0x0009ED95 File Offset: 0x0009CF95
		// (set) Token: 0x06002E20 RID: 11808 RVA: 0x0009EDA0 File Offset: 0x0009CFA0
		public override int Timeout
		{
			get
			{
				return this._timeout;
			}
			set
			{
				if (this.InUse)
				{
					throw new InvalidOperationException("This operation cannot be performed after the request has been submitted.");
				}
				if (value < 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value", "Timeout can be only be set to 'System.Threading.Timeout.Infinite' or a value >= 0.");
				}
				if (this._timeout != value)
				{
					this._timeout = value;
					this._timerQueue = null;
				}
			}
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06002E21 RID: 11809 RVA: 0x0009EDEF File Offset: 0x0009CFEF
		internal int RemainingTimeout
		{
			get
			{
				return this._remainingTimeout;
			}
		}

		/// <summary>Gets or sets a time-out when reading from or writing to a stream.</summary>
		/// <returns>The number of milliseconds before the reading or writing times out. The default value is 300,000 milliseconds (5 minutes).</returns>
		/// <exception cref="T:System.InvalidOperationException">The request has already been sent. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for a set operation is less than or equal to zero and is not equal to <see cref="F:System.Threading.Timeout.Infinite" />. </exception>
		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06002E22 RID: 11810 RVA: 0x0009EDF7 File Offset: 0x0009CFF7
		// (set) Token: 0x06002E23 RID: 11811 RVA: 0x0009EDFF File Offset: 0x0009CFFF
		public int ReadWriteTimeout
		{
			get
			{
				return this._readWriteTimeout;
			}
			set
			{
				if (this._getResponseStarted)
				{
					throw new InvalidOperationException("This operation cannot be performed after the request has been submitted.");
				}
				if (value <= 0 && value != -1)
				{
					throw new ArgumentOutOfRangeException("value", "Timeout can be only be set to 'System.Threading.Timeout.Infinite' or a value > 0.");
				}
				this._readWriteTimeout = value;
			}
		}

		/// <summary>Gets or sets a byte offset into the file being downloaded by this request.</summary>
		/// <returns>An <see cref="T:System.Int64" /> instance that specifies the file offset, in bytes. The default value is zero.</returns>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified for this property is less than zero. </exception>
		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06002E24 RID: 11812 RVA: 0x0009EE33 File Offset: 0x0009D033
		// (set) Token: 0x06002E25 RID: 11813 RVA: 0x0009EE3B File Offset: 0x0009D03B
		public long ContentOffset
		{
			get
			{
				return this._contentOffset;
			}
			set
			{
				if (this.InUse)
				{
					throw new InvalidOperationException("This operation cannot be performed after the request has been submitted.");
				}
				if (value < 0L)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._contentOffset = value;
			}
		}

		/// <summary>Gets or sets a value that is ignored by the <see cref="T:System.Net.FtpWebRequest" /> class.</summary>
		/// <returns>An <see cref="T:System.Int64" /> value that should be ignored.</returns>
		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06002E26 RID: 11814 RVA: 0x0009EE67 File Offset: 0x0009D067
		// (set) Token: 0x06002E27 RID: 11815 RVA: 0x0009EE6F File Offset: 0x0009D06F
		public override long ContentLength
		{
			get
			{
				return this._contentLength;
			}
			set
			{
				this._contentLength = value;
			}
		}

		/// <summary>Gets or sets the proxy used to communicate with the FTP server.</summary>
		/// <returns>An <see cref="T:System.Net.IWebProxy" /> instance responsible for communicating with the FTP server.</returns>
		/// <exception cref="T:System.ArgumentNullException">This property cannot be set to null.</exception>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06002E28 RID: 11816 RVA: 0x00002F6A File Offset: 0x0000116A
		// (set) Token: 0x06002E29 RID: 11817 RVA: 0x0009EE78 File Offset: 0x0009D078
		public override IWebProxy Proxy
		{
			get
			{
				return null;
			}
			set
			{
				if (this.InUse)
				{
					throw new InvalidOperationException("This operation cannot be performed after the request has been submitted.");
				}
			}
		}

		/// <summary>Gets or sets the name of the connection group that contains the service point used to send the current request.</summary>
		/// <returns>A <see cref="T:System.String" /> value that contains a connection group name.</returns>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress. </exception>
		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06002E2A RID: 11818 RVA: 0x0009EE8D File Offset: 0x0009D08D
		// (set) Token: 0x06002E2B RID: 11819 RVA: 0x0009EE95 File Offset: 0x0009D095
		public override string ConnectionGroupName
		{
			get
			{
				return this._connectionGroupName;
			}
			set
			{
				if (this.InUse)
				{
					throw new InvalidOperationException("This operation cannot be performed after the request has been submitted.");
				}
				this._connectionGroupName = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Net.ServicePoint" /> object used to connect to the FTP server.</summary>
		/// <returns>A <see cref="T:System.Net.ServicePoint" /> object that can be used to customize connection behavior.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06002E2C RID: 11820 RVA: 0x0009EEB1 File Offset: 0x0009D0B1
		public ServicePoint ServicePoint
		{
			get
			{
				if (this._servicePoint == null)
				{
					this._servicePoint = ServicePointManager.FindServicePoint(this._uri);
				}
				return this._servicePoint;
			}
		}

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06002E2D RID: 11821 RVA: 0x0009EED2 File Offset: 0x0009D0D2
		internal bool Aborted
		{
			get
			{
				return this._aborted;
			}
		}

		// Token: 0x06002E2E RID: 11822 RVA: 0x0009EEDC File Offset: 0x0009D0DC
		internal FtpWebRequest(Uri uri)
		{
			this._timeout = 100000;
			this._passive = true;
			this._binary = true;
			this._timerQueue = FtpWebRequest.s_DefaultTimerQueue;
			this._readWriteTimeout = 300000;
			base..ctor();
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, uri, ".ctor");
			}
			if (uri.Scheme != Uri.UriSchemeFtp)
			{
				throw new ArgumentOutOfRangeException("uri");
			}
			this._timerCallback = new TimerThread.Callback(this.TimerCallback);
			this._syncObject = new object();
			NetworkCredential networkCredential = null;
			this._uri = uri;
			this._methodInfo = FtpMethodInfo.GetMethodInfo("RETR");
			if (this._uri.UserInfo != null && this._uri.UserInfo.Length != 0)
			{
				string userInfo = this._uri.UserInfo;
				string text = userInfo;
				string text2 = "";
				int num = userInfo.IndexOf(':');
				if (num != -1)
				{
					text = Uri.UnescapeDataString(userInfo.Substring(0, num));
					num++;
					text2 = Uri.UnescapeDataString(userInfo.Substring(num, userInfo.Length - num));
				}
				networkCredential = new NetworkCredential(text, text2);
			}
			if (networkCredential == null)
			{
				networkCredential = FtpWebRequest.s_defaultFtpNetworkCredential;
			}
			this._authInfo = networkCredential;
		}

		/// <summary>Returns the FTP server response.</summary>
		/// <returns>A <see cref="T:System.Net.WebResponse" /> reference that contains an <see cref="T:System.Net.FtpWebResponse" /> instance. This object contains the FTP server's response to the request.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.FtpWebRequest.GetResponse" /> or <see cref="M:System.Net.FtpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" /> has already been called for this instance.- or -An HTTP proxy is enabled, and you attempted to use an FTP command other than <see cref="F:System.Net.WebRequestMethods.Ftp.DownloadFile" />, <see cref="F:System.Net.WebRequestMethods.Ftp.ListDirectory" />, or <see cref="F:System.Net.WebRequestMethods.Ftp.ListDirectoryDetails" />.</exception>
		/// <exception cref="T:System.Net.WebException">
		///   <see cref="P:System.Net.FtpWebRequest.EnableSsl" /> is set to true, but the server does not support this feature.- or -A <see cref="P:System.Net.FtpWebRequest.Timeout" /> was specified and the timeout has expired.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E2F RID: 11823 RVA: 0x0009F008 File Offset: 0x0009D208
		public override WebResponse GetResponse()
		{
			if (NetEventSource.IsEnabled)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Enter(this, null, "GetResponse");
				}
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, FormattableStringFactory.Create("Method: {0}", new object[] { this._methodInfo.Method }), "GetResponse");
				}
			}
			try
			{
				this.CheckError();
				if (this._ftpWebResponse != null)
				{
					return this._ftpWebResponse;
				}
				if (this._getResponseStarted)
				{
					throw new InvalidOperationException("Cannot re-call BeginGetRequestStream/BeginGetResponse while a previous call is still in progress.");
				}
				this._getResponseStarted = true;
				this._startTime = DateTime.UtcNow;
				this._remainingTimeout = this.Timeout;
				if (this.Timeout != -1)
				{
					this._remainingTimeout = this.Timeout - (int)(DateTime.UtcNow - this._startTime).TotalMilliseconds;
					if (this._remainingTimeout <= 0)
					{
						throw ExceptionHelper.TimeoutException;
					}
				}
				FtpWebRequest.RequestStage requestStage = this.FinishRequestStage(FtpWebRequest.RequestStage.RequestStarted);
				if (requestStage >= FtpWebRequest.RequestStage.RequestStarted)
				{
					if (requestStage < FtpWebRequest.RequestStage.ReadReady)
					{
						object syncObject = this._syncObject;
						lock (syncObject)
						{
							if (this._requestStage < FtpWebRequest.RequestStage.ReadReady)
							{
								this._readAsyncResult = new LazyAsyncResult(null, null, null);
							}
						}
						if (this._readAsyncResult != null)
						{
							this._readAsyncResult.InternalWaitForCompletion();
						}
						this.CheckError();
					}
				}
				else
				{
					this.SubmitRequest(false);
					if (this._methodInfo.IsUpload)
					{
						this.FinishRequestStage(FtpWebRequest.RequestStage.WriteReady);
					}
					else
					{
						this.FinishRequestStage(FtpWebRequest.RequestStage.ReadReady);
					}
					this.CheckError();
					this.EnsureFtpWebResponse(null);
				}
			}
			catch (Exception ex)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Error(this, ex, "GetResponse");
				}
				if (this._exception == null)
				{
					if (NetEventSource.IsEnabled)
					{
						NetEventSource.Error(this, ex, "GetResponse");
					}
					this.SetException(ex);
					this.FinishRequestStage(FtpWebRequest.RequestStage.CheckForError);
				}
				throw;
			}
			finally
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Exit(this, this._ftpWebResponse, "GetResponse");
				}
			}
			return this._ftpWebResponse;
		}

		/// <summary>Begins sending a request and receiving a response from an FTP server asynchronously.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> instance that indicates the status of the operation.</returns>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete. </param>
		/// <param name="state">A user-defined object that contains information about the operation. This object is passed to the <paramref name="callback" /> delegate when the operation completes. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.FtpWebRequest.GetResponse" /> or <see cref="M:System.Net.FtpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" /> has already been called for this instance. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E30 RID: 11824 RVA: 0x0009F234 File Offset: 0x0009D434
		public override IAsyncResult BeginGetResponse(AsyncCallback callback, object state)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(this, null, "BeginGetResponse");
				NetEventSource.Info(this, FormattableStringFactory.Create("Method: {0}", new object[] { this._methodInfo.Method }), "BeginGetResponse");
			}
			ContextAwareResult contextAwareResult;
			try
			{
				if (this._ftpWebResponse != null)
				{
					contextAwareResult = new ContextAwareResult(this, state, callback);
					contextAwareResult.InvokeCallback(this._ftpWebResponse);
					return contextAwareResult;
				}
				if (this._getResponseStarted)
				{
					throw new InvalidOperationException("Cannot re-call BeginGetRequestStream/BeginGetResponse while a previous call is still in progress.");
				}
				this._getResponseStarted = true;
				this.CheckError();
				FtpWebRequest.RequestStage requestStage = this.FinishRequestStage(FtpWebRequest.RequestStage.RequestStarted);
				contextAwareResult = new ContextAwareResult(true, true, this, state, callback);
				this._readAsyncResult = contextAwareResult;
				if (requestStage >= FtpWebRequest.RequestStage.RequestStarted)
				{
					contextAwareResult.StartPostingAsyncOp();
					contextAwareResult.FinishPostingAsyncOp();
					if (requestStage >= FtpWebRequest.RequestStage.ReadReady)
					{
						contextAwareResult = null;
					}
					else
					{
						object obj = this._syncObject;
						lock (obj)
						{
							if (this._requestStage >= FtpWebRequest.RequestStage.ReadReady)
							{
								contextAwareResult = null;
							}
						}
					}
					if (contextAwareResult == null)
					{
						contextAwareResult = (ContextAwareResult)this._readAsyncResult;
						if (!contextAwareResult.InternalPeekCompleted)
						{
							contextAwareResult.InvokeCallback();
						}
					}
				}
				else
				{
					object obj = contextAwareResult.StartPostingAsyncOp();
					lock (obj)
					{
						this.SubmitRequest(true);
						contextAwareResult.FinishPostingAsyncOp();
					}
					this.FinishRequestStage(FtpWebRequest.RequestStage.CheckForError);
				}
			}
			catch (Exception ex)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Error(this, ex, "BeginGetResponse");
				}
				throw;
			}
			finally
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Exit(this, null, "BeginGetResponse");
				}
			}
			return contextAwareResult;
		}

		/// <summary>Ends a pending asynchronous operation started with <see cref="M:System.Net.FtpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" />.</summary>
		/// <returns>A <see cref="T:System.Net.WebResponse" /> reference that contains an <see cref="T:System.Net.FtpWebResponse" /> instance. This object contains the FTP server's response to the request.</returns>
		/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> that was returned when the operation started. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not obtained by calling <see cref="M:System.Net.FtpWebRequest.BeginGetResponse(System.AsyncCallback,System.Object)" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">This method was already called for the operation identified by <paramref name="asyncResult" />. </exception>
		/// <exception cref="T:System.Net.WebException">An error occurred using an HTTP proxy. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E31 RID: 11825 RVA: 0x0009F40C File Offset: 0x0009D60C
		public override WebResponse EndGetResponse(IAsyncResult asyncResult)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(this, null, "EndGetResponse");
			}
			try
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				LazyAsyncResult lazyAsyncResult = asyncResult as LazyAsyncResult;
				if (lazyAsyncResult == null)
				{
					throw new ArgumentException("The IAsyncResult object was not returned from the corresponding asynchronous method on this class.", "asyncResult");
				}
				if (lazyAsyncResult.EndCalled)
				{
					throw new InvalidOperationException(SR.Format("{0} can only be called once for each asynchronous operation.", "EndGetResponse"));
				}
				lazyAsyncResult.InternalWaitForCompletion();
				lazyAsyncResult.EndCalled = true;
				this.CheckError();
			}
			catch (Exception ex)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Error(this, ex, "EndGetResponse");
				}
				throw;
			}
			finally
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Exit(this, null, "EndGetResponse");
				}
			}
			return this._ftpWebResponse;
		}

		/// <summary>Retrieves the stream used to upload data to an FTP server.</summary>
		/// <returns>A writable <see cref="T:System.IO.Stream" /> instance used to store data to be sent to the server by the current request.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Net.FtpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" /> has been called and has not completed. - or -An HTTP proxy is enabled, and you attempted to use an FTP command other than <see cref="F:System.Net.WebRequestMethods.Ftp.DownloadFile" />, <see cref="F:System.Net.WebRequestMethods.Ftp.ListDirectory" />, or <see cref="F:System.Net.WebRequestMethods.Ftp.ListDirectoryDetails" />.</exception>
		/// <exception cref="T:System.Net.WebException">A connection to the FTP server could not be established. </exception>
		/// <exception cref="T:System.Net.ProtocolViolationException">The <see cref="P:System.Net.FtpWebRequest.Method" /> property is not set to <see cref="F:System.Net.WebRequestMethods.Ftp.UploadFile" /> or <see cref="F:System.Net.WebRequestMethods.Ftp.AppendFile" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E32 RID: 11826 RVA: 0x0009F4D4 File Offset: 0x0009D6D4
		public override Stream GetRequestStream()
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(this, null, "GetRequestStream");
				NetEventSource.Info(this, FormattableStringFactory.Create("Method: {0}", new object[] { this._methodInfo.Method }), "GetRequestStream");
			}
			try
			{
				if (this._getRequestStreamStarted)
				{
					throw new InvalidOperationException("Cannot re-call BeginGetRequestStream/BeginGetResponse while a previous call is still in progress.");
				}
				this._getRequestStreamStarted = true;
				if (!this._methodInfo.IsUpload)
				{
					throw new ProtocolViolationException("Cannot send a content-body with this verb-type.");
				}
				this.CheckError();
				this._startTime = DateTime.UtcNow;
				this._remainingTimeout = this.Timeout;
				if (this.Timeout != -1)
				{
					this._remainingTimeout = this.Timeout - (int)(DateTime.UtcNow - this._startTime).TotalMilliseconds;
					if (this._remainingTimeout <= 0)
					{
						throw ExceptionHelper.TimeoutException;
					}
				}
				this.FinishRequestStage(FtpWebRequest.RequestStage.RequestStarted);
				this.SubmitRequest(false);
				this.FinishRequestStage(FtpWebRequest.RequestStage.WriteReady);
				this.CheckError();
				if (this._stream.CanTimeout)
				{
					this._stream.WriteTimeout = this.ReadWriteTimeout;
					this._stream.ReadTimeout = this.ReadWriteTimeout;
				}
			}
			catch (Exception ex)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Error(this, ex, "GetRequestStream");
				}
				throw;
			}
			finally
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Exit(this, null, "GetRequestStream");
				}
			}
			return this._stream;
		}

		/// <summary>Begins asynchronously opening a request's content stream for writing.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> instance that indicates the status of the operation.</returns>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when the operation is complete. </param>
		/// <param name="state">A user-defined object that contains information about the operation. This object is passed to the <paramref name="callback" /> delegate when the operation completes. </param>
		/// <exception cref="T:System.InvalidOperationException">A previous call to this method or <see cref="M:System.Net.FtpWebRequest.GetRequestStream" /> has not yet completed. </exception>
		/// <exception cref="T:System.Net.WebException">A connection to the FTP server could not be established. </exception>
		/// <exception cref="T:System.Net.ProtocolViolationException">The <see cref="P:System.Net.FtpWebRequest.Method" /> property is not set to <see cref="F:System.Net.WebRequestMethods.Ftp.UploadFile" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.DnsPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E33 RID: 11827 RVA: 0x0009F648 File Offset: 0x0009D848
		public override IAsyncResult BeginGetRequestStream(AsyncCallback callback, object state)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(this, null, "BeginGetRequestStream");
				NetEventSource.Info(this, FormattableStringFactory.Create("Method: {0}", new object[] { this._methodInfo.Method }), "BeginGetRequestStream");
			}
			ContextAwareResult contextAwareResult = null;
			try
			{
				if (this._getRequestStreamStarted)
				{
					throw new InvalidOperationException("Cannot re-call BeginGetRequestStream/BeginGetResponse while a previous call is still in progress.");
				}
				this._getRequestStreamStarted = true;
				if (!this._methodInfo.IsUpload)
				{
					throw new ProtocolViolationException("Cannot send a content-body with this verb-type.");
				}
				this.CheckError();
				this.FinishRequestStage(FtpWebRequest.RequestStage.RequestStarted);
				contextAwareResult = new ContextAwareResult(true, true, this, state, callback);
				object obj = contextAwareResult.StartPostingAsyncOp();
				lock (obj)
				{
					this._writeAsyncResult = contextAwareResult;
					this.SubmitRequest(true);
					contextAwareResult.FinishPostingAsyncOp();
					this.FinishRequestStage(FtpWebRequest.RequestStage.CheckForError);
				}
			}
			catch (Exception ex)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Error(this, ex, "BeginGetRequestStream");
				}
				throw;
			}
			finally
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Exit(this, null, "BeginGetRequestStream");
				}
			}
			return contextAwareResult;
		}

		/// <summary>Ends a pending asynchronous operation started with <see cref="M:System.Net.FtpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" />.</summary>
		/// <returns>A writable <see cref="T:System.IO.Stream" /> instance associated with this instance.</returns>
		/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> object that was returned when the operation started. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not obtained by calling <see cref="M:System.Net.FtpWebRequest.BeginGetRequestStream(System.AsyncCallback,System.Object)" />. </exception>
		/// <exception cref="T:System.InvalidOperationException">This method was already called for the operation identified by <paramref name="asyncResult" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E34 RID: 11828 RVA: 0x0009F770 File Offset: 0x0009D970
		public override Stream EndGetRequestStream(IAsyncResult asyncResult)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(this, null, "EndGetRequestStream");
			}
			Stream stream = null;
			try
			{
				if (asyncResult == null)
				{
					throw new ArgumentNullException("asyncResult");
				}
				LazyAsyncResult lazyAsyncResult = asyncResult as LazyAsyncResult;
				if (lazyAsyncResult == null)
				{
					throw new ArgumentException("The IAsyncResult object was not returned from the corresponding asynchronous method on this class.", "asyncResult");
				}
				if (lazyAsyncResult.EndCalled)
				{
					throw new InvalidOperationException(SR.Format("{0} can only be called once for each asynchronous operation.", "EndGetResponse"));
				}
				lazyAsyncResult.InternalWaitForCompletion();
				lazyAsyncResult.EndCalled = true;
				this.CheckError();
				stream = this._stream;
				lazyAsyncResult.EndCalled = true;
				if (stream.CanTimeout)
				{
					stream.WriteTimeout = this.ReadWriteTimeout;
					stream.ReadTimeout = this.ReadWriteTimeout;
				}
			}
			catch (Exception ex)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Error(this, ex, "EndGetRequestStream");
				}
				throw;
			}
			finally
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Exit(this, null, "EndGetRequestStream");
				}
			}
			return stream;
		}

		// Token: 0x06002E35 RID: 11829 RVA: 0x0009F860 File Offset: 0x0009DA60
		private void SubmitRequest(bool isAsync)
		{
			try
			{
				this._async = isAsync;
				for (;;)
				{
					FtpControlStream ftpControlStream = this._connection;
					if (ftpControlStream == null)
					{
						if (isAsync)
						{
							break;
						}
						ftpControlStream = this.CreateConnection();
						this._connection = ftpControlStream;
					}
					if (!isAsync && this.Timeout != -1)
					{
						this._remainingTimeout = this.Timeout - (int)(DateTime.UtcNow - this._startTime).TotalMilliseconds;
						if (this._remainingTimeout <= 0)
						{
							goto Block_6;
						}
					}
					if (NetEventSource.IsEnabled)
					{
						NetEventSource.Info(this, "Request being submitted", "SubmitRequest");
					}
					ftpControlStream.SetSocketTimeoutOption(this.RemainingTimeout);
					try
					{
						this.TimedSubmitRequestHelper(isAsync);
					}
					catch (Exception ex)
					{
						if (this.AttemptedRecovery(ex))
						{
							if (!isAsync && this.Timeout != -1)
							{
								this._remainingTimeout = this.Timeout - (int)(DateTime.UtcNow - this._startTime).TotalMilliseconds;
								if (this._remainingTimeout <= 0)
								{
									throw;
								}
							}
							continue;
						}
						throw;
					}
					goto IL_00E9;
				}
				this.CreateConnectionAsync();
				return;
				Block_6:
				throw ExceptionHelper.TimeoutException;
				IL_00E9:;
			}
			catch (WebException ex2)
			{
				IOException ex3 = ex2.InnerException as IOException;
				if (ex3 != null)
				{
					SocketException ex4 = ex3.InnerException as SocketException;
					if (ex4 != null && ex4.SocketErrorCode == SocketError.TimedOut)
					{
						this.SetException(new WebException("The operation has timed out.", WebExceptionStatus.Timeout));
					}
				}
				this.SetException(ex2);
			}
			catch (Exception ex5)
			{
				this.SetException(ex5);
			}
		}

		// Token: 0x06002E36 RID: 11830 RVA: 0x0009F9DC File Offset: 0x0009DBDC
		private Exception TranslateConnectException(Exception e)
		{
			SocketException ex = e as SocketException;
			if (ex == null)
			{
				return e;
			}
			if (ex.SocketErrorCode == SocketError.HostNotFound)
			{
				return new WebException("The remote name could not be resolved", WebExceptionStatus.NameResolutionFailure);
			}
			return new WebException("Unable to connect to the remote server", WebExceptionStatus.ConnectFailure);
		}

		// Token: 0x06002E37 RID: 11831 RVA: 0x0009FA1C File Offset: 0x0009DC1C
		private async void CreateConnectionAsync()
		{
			string host = this._uri.Host;
			int port = this._uri.Port;
			TcpClient client = new TcpClient();
			object obj;
			try
			{
				await client.ConnectAsync(host, port).ConfigureAwait(false);
				obj = new FtpControlStream(client);
			}
			catch (Exception ex)
			{
				obj = this.TranslateConnectException(ex);
			}
			this.AsyncRequestCallback(obj);
		}

		// Token: 0x06002E38 RID: 11832 RVA: 0x0009FA54 File Offset: 0x0009DC54
		private FtpControlStream CreateConnection()
		{
			string host = this._uri.Host;
			int port = this._uri.Port;
			TcpClient tcpClient = new TcpClient();
			try
			{
				tcpClient.Connect(host, port);
			}
			catch (Exception ex)
			{
				throw this.TranslateConnectException(ex);
			}
			return new FtpControlStream(tcpClient);
		}

		// Token: 0x06002E39 RID: 11833 RVA: 0x0009FAA8 File Offset: 0x0009DCA8
		private Stream TimedSubmitRequestHelper(bool isAsync)
		{
			if (isAsync)
			{
				if (this._requestCompleteAsyncResult == null)
				{
					this._requestCompleteAsyncResult = new LazyAsyncResult(null, null, null);
				}
				return this._connection.SubmitRequest(this, true, true);
			}
			Stream stream = null;
			bool flag = false;
			TimerThread.Timer timer = this.TimerQueue.CreateTimer(this._timerCallback, null);
			try
			{
				stream = this._connection.SubmitRequest(this, false, true);
			}
			catch (Exception ex)
			{
				if ((!(ex is SocketException) && !(ex is ObjectDisposedException)) || !timer.HasExpired)
				{
					timer.Cancel();
					throw;
				}
				flag = true;
			}
			if (flag || !timer.Cancel())
			{
				this._timedOut = true;
				throw ExceptionHelper.TimeoutException;
			}
			if (stream != null)
			{
				object syncObject = this._syncObject;
				lock (syncObject)
				{
					if (this._aborted)
					{
						((ICloseEx)stream).CloseEx(CloseExState.Abort | CloseExState.Silent);
						this.CheckError();
						throw new InternalException();
					}
					this._stream = stream;
				}
			}
			return stream;
		}

		// Token: 0x06002E3A RID: 11834 RVA: 0x0009FBAC File Offset: 0x0009DDAC
		private void TimerCallback(TimerThread.Timer timer, int timeNoticed, object context)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, null, "TimerCallback");
			}
			FtpControlStream connection = this._connection;
			if (connection != null)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, "aborting connection", "TimerCallback");
				}
				connection.AbortConnect();
			}
		}

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06002E3B RID: 11835 RVA: 0x0009FBF3 File Offset: 0x0009DDF3
		private TimerThread.Queue TimerQueue
		{
			get
			{
				if (this._timerQueue == null)
				{
					this._timerQueue = TimerThread.GetOrCreateQueue(this.RemainingTimeout);
				}
				return this._timerQueue;
			}
		}

		// Token: 0x06002E3C RID: 11836 RVA: 0x0009FC14 File Offset: 0x0009DE14
		private bool AttemptedRecovery(Exception e)
		{
			if (e is OutOfMemoryException || this._onceFailed || this._aborted || this._timedOut || this._connection == null || !this._connection.RecoverableFailure)
			{
				return false;
			}
			this._onceFailed = true;
			object syncObject = this._syncObject;
			lock (syncObject)
			{
				if (this._connection == null)
				{
					return false;
				}
				this._connection.CloseSocket();
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, FormattableStringFactory.Create("Releasing connection: {0}", new object[] { this._connection }), "AttemptedRecovery");
				}
				this._connection = null;
			}
			return true;
		}

		// Token: 0x06002E3D RID: 11837 RVA: 0x0009FCD8 File Offset: 0x0009DED8
		private void SetException(Exception exception)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, null, "SetException");
			}
			if (exception is OutOfMemoryException)
			{
				this._exception = exception;
				throw exception;
			}
			FtpControlStream connection = this._connection;
			if (this._exception == null)
			{
				if (exception is WebException)
				{
					this.EnsureFtpWebResponse(exception);
					this._exception = new WebException(exception.Message, null, ((WebException)exception).Status, this._ftpWebResponse);
				}
				else if (exception is AuthenticationException || exception is SecurityException)
				{
					this._exception = exception;
				}
				else if (connection != null && connection.StatusCode != FtpStatusCode.Undefined)
				{
					this.EnsureFtpWebResponse(exception);
					this._exception = new WebException(SR.Format("The remote server returned an error: {0}.", connection.StatusLine), exception, WebExceptionStatus.ProtocolError, this._ftpWebResponse);
				}
				else
				{
					this._exception = new WebException(exception.Message, exception);
				}
				if (connection != null && this._ftpWebResponse != null)
				{
					this._ftpWebResponse.UpdateStatus(connection.StatusCode, connection.StatusLine, connection.ExitMessage);
				}
			}
		}

		// Token: 0x06002E3E RID: 11838 RVA: 0x0009FDD9 File Offset: 0x0009DFD9
		private void CheckError()
		{
			if (this._exception != null)
			{
				ExceptionDispatchInfo.Throw(this._exception);
			}
		}

		// Token: 0x06002E3F RID: 11839 RVA: 0x0009FDEE File Offset: 0x0009DFEE
		internal void RequestCallback(object obj)
		{
			if (this._async)
			{
				this.AsyncRequestCallback(obj);
				return;
			}
			this.SyncRequestCallback(obj);
		}

		// Token: 0x06002E40 RID: 11840 RVA: 0x0009FE08 File Offset: 0x0009E008
		private void SyncRequestCallback(object obj)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(this, obj, "SyncRequestCallback");
			}
			FtpWebRequest.RequestStage requestStage = FtpWebRequest.RequestStage.CheckForError;
			try
			{
				bool flag = obj == null;
				Exception ex = obj as Exception;
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, FormattableStringFactory.Create("exp:{0} completedRequest:{1}", new object[] { ex, flag }), "SyncRequestCallback");
				}
				if (ex != null)
				{
					this.SetException(ex);
				}
				else
				{
					if (!flag)
					{
						throw new InternalException();
					}
					FtpControlStream connection = this._connection;
					if (connection != null)
					{
						this.EnsureFtpWebResponse(null);
						this._ftpWebResponse.UpdateStatus(connection.StatusCode, connection.StatusLine, connection.ExitMessage);
					}
					requestStage = FtpWebRequest.RequestStage.ReleaseConnection;
				}
			}
			catch (Exception ex2)
			{
				this.SetException(ex2);
			}
			finally
			{
				this.FinishRequestStage(requestStage);
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Exit(this, null, "SyncRequestCallback");
				}
				this.CheckError();
			}
		}

		// Token: 0x06002E41 RID: 11841 RVA: 0x0009FEF8 File Offset: 0x0009E0F8
		private void AsyncRequestCallback(object obj)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(this, obj, "AsyncRequestCallback");
			}
			FtpWebRequest.RequestStage requestStage = FtpWebRequest.RequestStage.CheckForError;
			try
			{
				FtpControlStream ftpControlStream = obj as FtpControlStream;
				FtpDataStream ftpDataStream = obj as FtpDataStream;
				Exception ex = obj as Exception;
				bool flag = obj == null;
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Info(this, FormattableStringFactory.Create("stream:{0} conn:{1} exp:{2} completedRequest:{3}", new object[] { ftpDataStream, ftpControlStream, ex, flag }), "AsyncRequestCallback");
				}
				for (;;)
				{
					if (ex != null)
					{
						if (this.AttemptedRecovery(ex))
						{
							ftpControlStream = this.CreateConnection();
							if (ftpControlStream == null)
							{
								break;
							}
							ex = null;
						}
						if (ex != null)
						{
							goto Block_9;
						}
					}
					if (ftpControlStream != null)
					{
						object obj2 = this._syncObject;
						lock (obj2)
						{
							if (this._aborted)
							{
								if (NetEventSource.IsEnabled)
								{
									NetEventSource.Info(this, FormattableStringFactory.Create("Releasing connect:{0}", new object[] { ftpControlStream }), "AsyncRequestCallback");
								}
								ftpControlStream.CloseSocket();
								break;
							}
							this._connection = ftpControlStream;
							if (NetEventSource.IsEnabled)
							{
								NetEventSource.Associate(this, this._connection, "AsyncRequestCallback");
							}
						}
						try
						{
							ftpDataStream = (FtpDataStream)this.TimedSubmitRequestHelper(true);
						}
						catch (Exception ex)
						{
							continue;
						}
						break;
					}
					goto IL_012F;
				}
				return;
				Block_9:
				this.SetException(ex);
				return;
				IL_012F:
				if (ftpDataStream != null)
				{
					object obj2 = this._syncObject;
					lock (obj2)
					{
						if (this._aborted)
						{
							((ICloseEx)ftpDataStream).CloseEx(CloseExState.Abort | CloseExState.Silent);
							goto IL_01CA;
						}
						this._stream = ftpDataStream;
					}
					ftpDataStream.SetSocketTimeoutOption(this.Timeout);
					this.EnsureFtpWebResponse(null);
					requestStage = (ftpDataStream.CanRead ? FtpWebRequest.RequestStage.ReadReady : FtpWebRequest.RequestStage.WriteReady);
				}
				else
				{
					if (!flag)
					{
						throw new InternalException();
					}
					ftpControlStream = this._connection;
					if (ftpControlStream != null)
					{
						this.EnsureFtpWebResponse(null);
						this._ftpWebResponse.UpdateStatus(ftpControlStream.StatusCode, ftpControlStream.StatusLine, ftpControlStream.ExitMessage);
					}
					requestStage = FtpWebRequest.RequestStage.ReleaseConnection;
				}
				IL_01CA:;
			}
			catch (Exception ex2)
			{
				this.SetException(ex2);
			}
			finally
			{
				this.FinishRequestStage(requestStage);
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Exit(this, null, "AsyncRequestCallback");
				}
			}
		}

		// Token: 0x06002E42 RID: 11842 RVA: 0x000A0178 File Offset: 0x0009E378
		private FtpWebRequest.RequestStage FinishRequestStage(FtpWebRequest.RequestStage stage)
		{
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, FormattableStringFactory.Create("state:{0}", new object[] { stage }), "FinishRequestStage");
			}
			if (this._exception != null)
			{
				stage = FtpWebRequest.RequestStage.ReleaseConnection;
			}
			object syncObject = this._syncObject;
			FtpWebRequest.RequestStage requestStage;
			LazyAsyncResult writeAsyncResult;
			LazyAsyncResult readAsyncResult;
			FtpControlStream connection;
			lock (syncObject)
			{
				requestStage = this._requestStage;
				if (stage == FtpWebRequest.RequestStage.CheckForError)
				{
					return requestStage;
				}
				if (requestStage == FtpWebRequest.RequestStage.ReleaseConnection && stage == FtpWebRequest.RequestStage.ReleaseConnection)
				{
					return FtpWebRequest.RequestStage.ReleaseConnection;
				}
				if (stage > requestStage)
				{
					this._requestStage = stage;
				}
				if (stage <= FtpWebRequest.RequestStage.RequestStarted)
				{
					return requestStage;
				}
				writeAsyncResult = this._writeAsyncResult;
				readAsyncResult = this._readAsyncResult;
				connection = this._connection;
				if (stage == FtpWebRequest.RequestStage.ReleaseConnection)
				{
					if (this._exception == null && !this._aborted && requestStage != FtpWebRequest.RequestStage.ReadReady && this._methodInfo.IsDownload && !this._ftpWebResponse.IsFromCache)
					{
						return requestStage;
					}
					this._connection = null;
				}
			}
			FtpWebRequest.RequestStage requestStage2;
			try
			{
				if ((stage == FtpWebRequest.RequestStage.ReleaseConnection || requestStage == FtpWebRequest.RequestStage.ReleaseConnection) && connection != null)
				{
					try
					{
						if (this._exception != null)
						{
							connection.Abort(this._exception);
						}
					}
					finally
					{
						if (NetEventSource.IsEnabled)
						{
							NetEventSource.Info(this, FormattableStringFactory.Create("Releasing connection: {0}", new object[] { connection }), "FinishRequestStage");
						}
						connection.CloseSocket();
						if (this._async && this._requestCompleteAsyncResult != null)
						{
							this._requestCompleteAsyncResult.InvokeCallback();
						}
					}
				}
				requestStage2 = requestStage;
			}
			finally
			{
				try
				{
					if (stage >= FtpWebRequest.RequestStage.WriteReady)
					{
						if (this._methodInfo.IsUpload && !this._getRequestStreamStarted)
						{
							if (this._stream != null)
							{
								this._stream.Close();
							}
						}
						else if (writeAsyncResult != null && !writeAsyncResult.InternalPeekCompleted)
						{
							writeAsyncResult.InvokeCallback();
						}
					}
				}
				finally
				{
					if (stage >= FtpWebRequest.RequestStage.ReadReady && readAsyncResult != null && !readAsyncResult.InternalPeekCompleted)
					{
						readAsyncResult.InvokeCallback();
					}
				}
			}
			return requestStage2;
		}

		/// <summary>Terminates an asynchronous FTP operation.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06002E43 RID: 11843 RVA: 0x000A036C File Offset: 0x0009E56C
		public override void Abort()
		{
			if (this._aborted)
			{
				return;
			}
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Enter(this, null, "Abort");
			}
			try
			{
				object syncObject = this._syncObject;
				Stream stream;
				FtpControlStream connection;
				lock (syncObject)
				{
					if (this._requestStage >= FtpWebRequest.RequestStage.ReleaseConnection)
					{
						return;
					}
					this._aborted = true;
					stream = this._stream;
					connection = this._connection;
					this._exception = ExceptionHelper.RequestAbortedException;
				}
				if (stream != null)
				{
					if (!(stream is ICloseEx))
					{
						NetEventSource.Fail(this, "The _stream member is not CloseEx hence the risk of connection been orphaned.", "Abort");
					}
					((ICloseEx)stream).CloseEx(CloseExState.Abort | CloseExState.Silent);
				}
				if (connection != null)
				{
					connection.Abort(ExceptionHelper.RequestAbortedException);
				}
			}
			catch (Exception ex)
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Error(this, ex, "Abort");
				}
				throw;
			}
			finally
			{
				if (NetEventSource.IsEnabled)
				{
					NetEventSource.Exit(this, null, "Abort");
				}
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether the control connection to the FTP server is closed after the request completes.</summary>
		/// <returns>true if the connection to the server should not be destroyed; otherwise, false. The default value is true.</returns>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress. </exception>
		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06002E44 RID: 11844 RVA: 0x0000390E File Offset: 0x00001B0E
		// (set) Token: 0x06002E45 RID: 11845 RVA: 0x0009EE78 File Offset: 0x0009D078
		public bool KeepAlive
		{
			get
			{
				return true;
			}
			set
			{
				if (this.InUse)
				{
					throw new InvalidOperationException("This operation cannot be performed after the request has been submitted.");
				}
			}
		}

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06002E46 RID: 11846 RVA: 0x000A046C File Offset: 0x0009E66C
		// (set) Token: 0x06002E47 RID: 11847 RVA: 0x0009EE78 File Offset: 0x0009D078
		public override RequestCachePolicy CachePolicy
		{
			get
			{
				return FtpWebRequest.DefaultCachePolicy;
			}
			set
			{
				if (this.InUse)
				{
					throw new InvalidOperationException("This operation cannot be performed after the request has been submitted.");
				}
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies the data type for file transfers.</summary>
		/// <returns>true to indicate to the server that the data to be transferred is binary; false to indicate that the data is text. The default value is true.</returns>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress.</exception>
		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06002E48 RID: 11848 RVA: 0x000A0473 File Offset: 0x0009E673
		// (set) Token: 0x06002E49 RID: 11849 RVA: 0x000A047B File Offset: 0x0009E67B
		public bool UseBinary
		{
			get
			{
				return this._binary;
			}
			set
			{
				if (this.InUse)
				{
					throw new InvalidOperationException("This operation cannot be performed after the request has been submitted.");
				}
				this._binary = value;
			}
		}

		/// <summary>Gets or sets the behavior of a client application's data transfer process.</summary>
		/// <returns>false if the client application's data transfer process listens for a connection on the data port; otherwise, true if the client should initiate a connection on the data port. The default value is true.</returns>
		/// <exception cref="T:System.InvalidOperationException">A new value was specified for this property for a request that is already in progress. </exception>
		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06002E4A RID: 11850 RVA: 0x000A0497 File Offset: 0x0009E697
		// (set) Token: 0x06002E4B RID: 11851 RVA: 0x000A049F File Offset: 0x0009E69F
		public bool UsePassive
		{
			get
			{
				return this._passive;
			}
			set
			{
				if (this.InUse)
				{
					throw new InvalidOperationException("This operation cannot be performed after the request has been submitted.");
				}
				this._passive = value;
			}
		}

		/// <summary>Gets or sets the certificates used for establishing an encrypted connection to the FTP server.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509CertificateCollection" /> object that contains the client certificates.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation is null.</exception>
		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06002E4C RID: 11852 RVA: 0x000A04BB File Offset: 0x0009E6BB
		// (set) Token: 0x06002E4D RID: 11853 RVA: 0x000A04ED File Offset: 0x0009E6ED
		public X509CertificateCollection ClientCertificates
		{
			get
			{
				return LazyInitializer.EnsureInitialized<X509CertificateCollection>(ref this._clientCertificates, ref this._syncObject, () => new X509CertificateCollection());
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._clientCertificates = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> that specifies that an SSL connection should be used.</summary>
		/// <returns>true if control and data transmissions are encrypted; otherwise, false. The default value is false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The connection to the FTP server has already been established.</exception>
		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06002E4E RID: 11854 RVA: 0x000A0504 File Offset: 0x0009E704
		// (set) Token: 0x06002E4F RID: 11855 RVA: 0x000A050C File Offset: 0x0009E70C
		public bool EnableSsl
		{
			get
			{
				return this._enableSsl;
			}
			set
			{
				if (this.InUse)
				{
					throw new InvalidOperationException("This operation cannot be performed after the request has been submitted.");
				}
				this._enableSsl = value;
			}
		}

		/// <summary>Gets an empty <see cref="T:System.Net.WebHeaderCollection" /> object.</summary>
		/// <returns>An empty <see cref="T:System.Net.WebHeaderCollection" /> object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Net.WebPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06002E50 RID: 11856 RVA: 0x000A0528 File Offset: 0x0009E728
		// (set) Token: 0x06002E51 RID: 11857 RVA: 0x000A0543 File Offset: 0x0009E743
		public override WebHeaderCollection Headers
		{
			get
			{
				if (this._ftpRequestHeaders == null)
				{
					this._ftpRequestHeaders = new WebHeaderCollection();
				}
				return this._ftpRequestHeaders;
			}
			set
			{
				this._ftpRequestHeaders = value;
			}
		}

		/// <summary>Always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>Always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">Content type information is not supported for FTP.</exception>
		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06002E52 RID: 11858 RVA: 0x000A054C File Offset: 0x0009E74C
		// (set) Token: 0x06002E53 RID: 11859 RVA: 0x000A054C File Offset: 0x0009E74C
		public override string ContentType
		{
			get
			{
				throw ExceptionHelper.PropertyNotSupportedException;
			}
			set
			{
				throw ExceptionHelper.PropertyNotSupportedException;
			}
		}

		/// <summary>Always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>Always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">Default credentials are not supported for FTP.</exception>
		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06002E54 RID: 11860 RVA: 0x000A054C File Offset: 0x0009E74C
		// (set) Token: 0x06002E55 RID: 11861 RVA: 0x000A054C File Offset: 0x0009E74C
		public override bool UseDefaultCredentials
		{
			get
			{
				throw ExceptionHelper.PropertyNotSupportedException;
			}
			set
			{
				throw ExceptionHelper.PropertyNotSupportedException;
			}
		}

		/// <summary>Always throws a <see cref="T:System.NotSupportedException" />.</summary>
		/// <returns>Always throws a <see cref="T:System.NotSupportedException" />.</returns>
		/// <exception cref="T:System.NotSupportedException">Preauthentication is not supported for FTP.</exception>
		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06002E56 RID: 11862 RVA: 0x000A054C File Offset: 0x0009E74C
		// (set) Token: 0x06002E57 RID: 11863 RVA: 0x000A054C File Offset: 0x0009E74C
		public override bool PreAuthenticate
		{
			get
			{
				throw ExceptionHelper.PropertyNotSupportedException;
			}
			set
			{
				throw ExceptionHelper.PropertyNotSupportedException;
			}
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06002E58 RID: 11864 RVA: 0x000A0553 File Offset: 0x0009E753
		private bool InUse
		{
			get
			{
				return this._getRequestStreamStarted || this._getResponseStarted;
			}
		}

		// Token: 0x06002E59 RID: 11865 RVA: 0x000A0568 File Offset: 0x0009E768
		private void EnsureFtpWebResponse(Exception exception)
		{
			if (this._ftpWebResponse == null || (this._ftpWebResponse.GetResponseStream() is FtpWebResponse.EmptyStream && this._stream != null))
			{
				object syncObject = this._syncObject;
				lock (syncObject)
				{
					if (this._ftpWebResponse == null || (this._ftpWebResponse.GetResponseStream() is FtpWebResponse.EmptyStream && this._stream != null))
					{
						Stream stream = this._stream;
						if (this._methodInfo.IsUpload)
						{
							stream = null;
						}
						if (this._stream != null && this._stream.CanRead && this._stream.CanTimeout)
						{
							this._stream.ReadTimeout = this.ReadWriteTimeout;
							this._stream.WriteTimeout = this.ReadWriteTimeout;
						}
						FtpControlStream connection = this._connection;
						long num = ((connection != null) ? connection.ContentLength : (-1L));
						if (stream == null && num < 0L)
						{
							num = 0L;
						}
						if (this._ftpWebResponse != null)
						{
							this._ftpWebResponse.SetResponseStream(stream);
						}
						else if (connection != null)
						{
							this._ftpWebResponse = new FtpWebResponse(stream, num, connection.ResponseUri, connection.StatusCode, connection.StatusLine, connection.LastModified, connection.BannerMessage, connection.WelcomeMessage, connection.ExitMessage);
						}
						else
						{
							this._ftpWebResponse = new FtpWebResponse(stream, -1L, this._uri, FtpStatusCode.Undefined, null, DateTime.Now, null, null, null);
						}
					}
				}
			}
			if (NetEventSource.IsEnabled)
			{
				NetEventSource.Info(this, FormattableStringFactory.Create("Returns {0} with stream {1}", new object[]
				{
					this._ftpWebResponse,
					this._ftpWebResponse._responseStream
				}), "EnsureFtpWebResponse");
			}
		}

		// Token: 0x06002E5A RID: 11866 RVA: 0x000A0728 File Offset: 0x0009E928
		internal void DataStreamClosed(CloseExState closeState)
		{
			if ((closeState & CloseExState.Abort) == CloseExState.Normal)
			{
				if (this._async)
				{
					this._requestCompleteAsyncResult.InternalWaitForCompletion();
					this.CheckError();
					return;
				}
				if (this._connection != null)
				{
					this._connection.CheckContinuePipeline();
					return;
				}
			}
			else
			{
				FtpControlStream connection = this._connection;
				if (connection != null)
				{
					connection.Abort(ExceptionHelper.RequestAbortedException);
				}
			}
		}

		// Token: 0x06002E5C RID: 11868 RVA: 0x00013BAE File Offset: 0x00011DAE
		internal FtpWebRequest()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x0400198A RID: 6538
		private object _syncObject;

		// Token: 0x0400198B RID: 6539
		private ICredentials _authInfo;

		// Token: 0x0400198C RID: 6540
		private readonly Uri _uri;

		// Token: 0x0400198D RID: 6541
		private FtpMethodInfo _methodInfo;

		// Token: 0x0400198E RID: 6542
		private string _renameTo;

		// Token: 0x0400198F RID: 6543
		private bool _getRequestStreamStarted;

		// Token: 0x04001990 RID: 6544
		private bool _getResponseStarted;

		// Token: 0x04001991 RID: 6545
		private DateTime _startTime;

		// Token: 0x04001992 RID: 6546
		private int _timeout;

		// Token: 0x04001993 RID: 6547
		private int _remainingTimeout;

		// Token: 0x04001994 RID: 6548
		private long _contentLength;

		// Token: 0x04001995 RID: 6549
		private long _contentOffset;

		// Token: 0x04001996 RID: 6550
		private X509CertificateCollection _clientCertificates;

		// Token: 0x04001997 RID: 6551
		private bool _passive;

		// Token: 0x04001998 RID: 6552
		private bool _binary;

		// Token: 0x04001999 RID: 6553
		private string _connectionGroupName;

		// Token: 0x0400199A RID: 6554
		private ServicePoint _servicePoint;

		// Token: 0x0400199B RID: 6555
		private bool _async;

		// Token: 0x0400199C RID: 6556
		private bool _aborted;

		// Token: 0x0400199D RID: 6557
		private bool _timedOut;

		// Token: 0x0400199E RID: 6558
		private Exception _exception;

		// Token: 0x0400199F RID: 6559
		private TimerThread.Queue _timerQueue;

		// Token: 0x040019A0 RID: 6560
		private TimerThread.Callback _timerCallback;

		// Token: 0x040019A1 RID: 6561
		private bool _enableSsl;

		// Token: 0x040019A2 RID: 6562
		private FtpControlStream _connection;

		// Token: 0x040019A3 RID: 6563
		private Stream _stream;

		// Token: 0x040019A4 RID: 6564
		private FtpWebRequest.RequestStage _requestStage;

		// Token: 0x040019A5 RID: 6565
		private bool _onceFailed;

		// Token: 0x040019A6 RID: 6566
		private WebHeaderCollection _ftpRequestHeaders;

		// Token: 0x040019A7 RID: 6567
		private FtpWebResponse _ftpWebResponse;

		// Token: 0x040019A8 RID: 6568
		private int _readWriteTimeout;

		// Token: 0x040019A9 RID: 6569
		private ContextAwareResult _writeAsyncResult;

		// Token: 0x040019AA RID: 6570
		private LazyAsyncResult _readAsyncResult;

		// Token: 0x040019AB RID: 6571
		private LazyAsyncResult _requestCompleteAsyncResult;

		// Token: 0x040019AC RID: 6572
		private static readonly NetworkCredential s_defaultFtpNetworkCredential = new NetworkCredential("anonymous", "anonymous@", string.Empty);

		// Token: 0x040019AD RID: 6573
		private const int s_DefaultTimeout = 100000;

		// Token: 0x040019AE RID: 6574
		private static readonly TimerThread.Queue s_DefaultTimerQueue = TimerThread.GetOrCreateQueue(100000);

		// Token: 0x02000592 RID: 1426
		private enum RequestStage
		{
			// Token: 0x040019B0 RID: 6576
			CheckForError,
			// Token: 0x040019B1 RID: 6577
			RequestStarted,
			// Token: 0x040019B2 RID: 6578
			WriteReady,
			// Token: 0x040019B3 RID: 6579
			ReadReady,
			// Token: 0x040019B4 RID: 6580
			ReleaseConnection
		}
	}
}
