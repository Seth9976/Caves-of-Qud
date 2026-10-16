using System;
using System.Collections;
using System.IO;
using System.Net.Security;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Mono.Net.Security.Private;
using Mono.Security.Authenticode;
using Mono.Security.Interface;

namespace System.Net
{
	/// <summary>Provides a simple, programmatically controlled HTTP protocol listener. This class cannot be inherited.</summary>
	// Token: 0x0200068B RID: 1675
	public sealed class HttpListener : IDisposable
	{
		// Token: 0x060034B7 RID: 13495 RVA: 0x000B855D File Offset: 0x000B675D
		internal HttpListener(X509Certificate certificate, MonoTlsProvider tlsProvider, MonoTlsSettings tlsSettings)
			: this()
		{
			this.certificate = certificate;
			this.tlsProvider = tlsProvider;
			this.tlsSettings = tlsSettings;
		}

		// Token: 0x060034B8 RID: 13496 RVA: 0x000B857C File Offset: 0x000B677C
		internal X509Certificate LoadCertificateAndKey(IPAddress addr, int port)
		{
			object internalLock = this._internalLock;
			X509Certificate x509Certificate;
			lock (internalLock)
			{
				if (this.certificate != null)
				{
					x509Certificate = this.certificate;
				}
				else
				{
					try
					{
						string text = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ".mono");
						text = Path.Combine(text, "httplistener");
						string text2 = Path.Combine(text, string.Format("{0}.cer", port));
						if (!File.Exists(text2))
						{
							x509Certificate = null;
						}
						else
						{
							string text3 = Path.Combine(text, string.Format("{0}.pvk", port));
							if (!File.Exists(text3))
							{
								x509Certificate = null;
							}
							else
							{
								X509Certificate2 x509Certificate2 = new X509Certificate2(text2);
								RSA rsa = PrivateKey.CreateFromFile(text3).RSA;
								this.certificate = new X509Certificate2((X509Certificate2Impl)x509Certificate2.Impl.CopyWithPrivateKey(rsa));
								x509Certificate = this.certificate;
							}
						}
					}
					catch
					{
						this.certificate = null;
						x509Certificate = null;
					}
				}
			}
			return x509Certificate;
		}

		// Token: 0x060034B9 RID: 13497 RVA: 0x000B8688 File Offset: 0x000B6888
		internal SslStream CreateSslStream(Stream innerStream, bool ownsStream, RemoteCertificateValidationCallback callback)
		{
			object internalLock = this._internalLock;
			SslStream sslStream;
			lock (internalLock)
			{
				if (this.tlsProvider == null)
				{
					this.tlsProvider = MonoTlsProviderFactory.GetProvider();
				}
				MonoTlsSettings monoTlsSettings = (this.tlsSettings ?? MonoTlsSettings.DefaultSettings).Clone();
				monoTlsSettings.RemoteCertificateValidationCallback = CallbackHelpers.PublicToMono(callback);
				sslStream = new SslStream(innerStream, ownsStream, this.tlsProvider, monoTlsSettings);
			}
			return sslStream;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpListener" /> class.</summary>
		/// <exception cref="T:System.PlatformNotSupportedException">This class cannot be used on the current operating system. Windows Server 2003 or Windows XP SP2 is required to use instances of this class.</exception>
		// Token: 0x060034BA RID: 13498 RVA: 0x000B8708 File Offset: 0x000B6908
		public HttpListener()
		{
			this._internalLock = new object();
			this.prefixes = new HttpListenerPrefixCollection(this);
			this.registry = new Hashtable();
			this.connections = Hashtable.Synchronized(new Hashtable());
			this.ctx_queue = new ArrayList();
			this.wait_queue = new ArrayList();
			this.auth_schemes = AuthenticationSchemes.Anonymous;
			this.defaultServiceNames = new ServiceNameStore();
			this.extendedProtectionPolicy = new ExtendedProtectionPolicy(PolicyEnforcement.Never);
		}

		/// <summary>Gets or sets the scheme used to authenticate clients.</summary>
		/// <returns>A bitwise combination of <see cref="T:System.Net.AuthenticationSchemes" /> enumeration values that indicates how clients are to be authenticated. The default value is <see cref="F:System.Net.AuthenticationSchemes.Anonymous" />.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x060034BB RID: 13499 RVA: 0x000B8785 File Offset: 0x000B6985
		// (set) Token: 0x060034BC RID: 13500 RVA: 0x000B878D File Offset: 0x000B698D
		public AuthenticationSchemes AuthenticationSchemes
		{
			get
			{
				return this.auth_schemes;
			}
			set
			{
				this.CheckDisposed();
				this.auth_schemes = value;
			}
		}

		/// <summary>Gets or sets the delegate called to determine the protocol used to authenticate clients.</summary>
		/// <returns>An <see cref="T:System.Net.AuthenticationSchemeSelector" /> delegate that invokes the method used to select an authentication protocol. The default value is null.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x060034BD RID: 13501 RVA: 0x000B879C File Offset: 0x000B699C
		// (set) Token: 0x060034BE RID: 13502 RVA: 0x000B87A4 File Offset: 0x000B69A4
		public AuthenticationSchemeSelector AuthenticationSchemeSelectorDelegate
		{
			get
			{
				return this.auth_selector;
			}
			set
			{
				this.CheckDisposed();
				this.auth_selector = value;
			}
		}

		/// <summary>Get or set the delegate called to determine the <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> to use for each request. </summary>
		/// <returns>A <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> that specifies the policy to use for extended protection.</returns>
		/// <exception cref="T:System.ArgumentException">An attempt was made to set the <see cref="P:System.Net.HttpListener.ExtendedProtectionSelectorDelegate" /> property, but the <see cref="P:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy.CustomChannelBinding" /> property must be null. </exception>
		/// <exception cref="T:System.ArgumentNullException">An attempt was made to set the <see cref="P:System.Net.HttpListener.ExtendedProtectionSelectorDelegate" /> property to null.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to set the <see cref="P:System.Net.HttpListener.ExtendedProtectionSelectorDelegate" /> property after the <see cref="M:System.Net.HttpListener.Start" /> method was already called.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">An attempt was made to set the <see cref="P:System.Net.HttpListener.ExtendedProtectionSelectorDelegate" /> property on a platform that does not support extended protection.</exception>
		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x060034BF RID: 13503 RVA: 0x000B87B3 File Offset: 0x000B69B3
		// (set) Token: 0x060034C0 RID: 13504 RVA: 0x000B87BB File Offset: 0x000B69BB
		public HttpListener.ExtendedProtectionSelector ExtendedProtectionSelectorDelegate
		{
			get
			{
				return this.extendedProtectionSelectorDelegate;
			}
			set
			{
				this.CheckDisposed();
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				if (!AuthenticationManager.OSSupportsExtendedProtection)
				{
					throw new PlatformNotSupportedException(SR.GetString("This operation requires OS support for extended protection."));
				}
				this.extendedProtectionSelectorDelegate = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that specifies whether your application receives exceptions that occur when an <see cref="T:System.Net.HttpListener" /> sends the response to the client.</summary>
		/// <returns>true if this <see cref="T:System.Net.HttpListener" /> should not return exceptions that occur when sending the response to the client; otherwise false. The default value is false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x060034C1 RID: 13505 RVA: 0x000B87EA File Offset: 0x000B69EA
		// (set) Token: 0x060034C2 RID: 13506 RVA: 0x000B87F2 File Offset: 0x000B69F2
		public bool IgnoreWriteExceptions
		{
			get
			{
				return this.ignore_write_exceptions;
			}
			set
			{
				this.CheckDisposed();
				this.ignore_write_exceptions = value;
			}
		}

		/// <summary>Gets a value that indicates whether <see cref="T:System.Net.HttpListener" /> has been started.</summary>
		/// <returns>true if the <see cref="T:System.Net.HttpListener" /> was started; otherwise, false.</returns>
		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x060034C3 RID: 13507 RVA: 0x000B8801 File Offset: 0x000B6A01
		public bool IsListening
		{
			get
			{
				return this.listening;
			}
		}

		/// <summary>Gets a value that indicates whether <see cref="T:System.Net.HttpListener" /> can be used with the current operating system.</summary>
		/// <returns>true if <see cref="T:System.Net.HttpListener" /> is supported; otherwise, false.</returns>
		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x060034C4 RID: 13508 RVA: 0x0000390E File Offset: 0x00001B0E
		public static bool IsSupported
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets the Uniform Resource Identifier (URI) prefixes handled by this <see cref="T:System.Net.HttpListener" /> object.</summary>
		/// <returns>An <see cref="T:System.Net.HttpListenerPrefixCollection" /> that contains the URI prefixes that this <see cref="T:System.Net.HttpListener" /> object is configured to handle. </returns>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x060034C5 RID: 13509 RVA: 0x000B8809 File Offset: 0x000B6A09
		public HttpListenerPrefixCollection Prefixes
		{
			get
			{
				this.CheckDisposed();
				return this.prefixes;
			}
		}

		/// <summary>The timeout manager for this <see cref="T:System.Net.HttpListener" /> instance.</summary>
		/// <returns>Returns <see cref="T:System.Net.HttpListenerTimeoutManager" />.The timeout manager for this <see cref="T:System.Net.HttpListener" /> instance.</returns>
		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x060034C6 RID: 13510 RVA: 0x0000829A File Offset: 0x0000649A
		[MonoTODO]
		public HttpListenerTimeoutManager TimeoutManager
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Get or set the <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> to use for extended protection for a session. </summary>
		/// <returns>A <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> that specifies the policy to use for extended protection.</returns>
		/// <exception cref="T:System.ArgumentException">An attempt was made to set the <see cref="P:System.Net.HttpListener.ExtendedProtectionPolicy" /> property, but the <see cref="P:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy.CustomChannelBinding" /> property was not null. </exception>
		/// <exception cref="T:System.ArgumentNullException">An attempt was made to set the <see cref="P:System.Net.HttpListener.ExtendedProtectionPolicy" /> property to null.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to set the <see cref="P:System.Net.HttpListener.ExtendedProtectionPolicy" /> property after the <see cref="M:System.Net.HttpListener.Start" /> method was already called.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">The <see cref="P:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy.PolicyEnforcement" /> property was set to <see cref="F:System.Security.Authentication.ExtendedProtection.PolicyEnforcement.Always" /> on a platform that does not support extended protection.</exception>
		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x060034C7 RID: 13511 RVA: 0x000B8817 File Offset: 0x000B6A17
		// (set) Token: 0x060034C8 RID: 13512 RVA: 0x000B8820 File Offset: 0x000B6A20
		[MonoTODO("not used anywhere in the implementation")]
		public ExtendedProtectionPolicy ExtendedProtectionPolicy
		{
			get
			{
				return this.extendedProtectionPolicy;
			}
			set
			{
				this.CheckDisposed();
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (!AuthenticationManager.OSSupportsExtendedProtection && value.PolicyEnforcement == PolicyEnforcement.Always)
				{
					throw new PlatformNotSupportedException(SR.GetString("This operation requires OS support for extended protection."));
				}
				if (value.CustomChannelBinding != null)
				{
					throw new ArgumentException(SR.GetString("Custom channel bindings are not supported."), "CustomChannelBinding");
				}
				this.extendedProtectionPolicy = value;
			}
		}

		/// <summary>Gets a default list of Service Provider Names (SPNs) as determined by registered prefixes.</summary>
		/// <returns>A <see cref="T:System.Security.Authentication.ExtendedProtection.ServiceNameCollection" /> that contains a list of SPNs.</returns>
		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x060034C9 RID: 13513 RVA: 0x000B8885 File Offset: 0x000B6A85
		public ServiceNameCollection DefaultServiceNames
		{
			get
			{
				return this.defaultServiceNames.ServiceNames;
			}
		}

		/// <summary>Gets or sets the realm, or resource partition, associated with this <see cref="T:System.Net.HttpListener" /> object.</summary>
		/// <returns>A <see cref="T:System.String" /> value that contains the name of the realm associated with the <see cref="T:System.Net.HttpListener" /> object.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x060034CA RID: 13514 RVA: 0x000B8892 File Offset: 0x000B6A92
		// (set) Token: 0x060034CB RID: 13515 RVA: 0x000B889A File Offset: 0x000B6A9A
		public string Realm
		{
			get
			{
				return this.realm;
			}
			set
			{
				this.CheckDisposed();
				this.realm = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that controls whether, when NTLM is used, additional requests using the same Transmission Control Protocol (TCP) connection are required to authenticate.</summary>
		/// <returns>true if the <see cref="T:System.Security.Principal.IIdentity" /> of the first request will be used for subsequent requests on the same connection; otherwise, false. The default value is false.</returns>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x060034CC RID: 13516 RVA: 0x000B88A9 File Offset: 0x000B6AA9
		// (set) Token: 0x060034CD RID: 13517 RVA: 0x000B88B1 File Offset: 0x000B6AB1
		[MonoTODO("Support for NTLM needs some loving.")]
		public bool UnsafeConnectionNtlmAuthentication
		{
			get
			{
				return this.unsafe_ntlm_auth;
			}
			set
			{
				this.CheckDisposed();
				this.unsafe_ntlm_auth = value;
			}
		}

		/// <summary>Shuts down the <see cref="T:System.Net.HttpListener" /> object immediately, discarding all currently queued requests.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060034CE RID: 13518 RVA: 0x000B88C0 File Offset: 0x000B6AC0
		public void Abort()
		{
			if (this.disposed)
			{
				return;
			}
			if (!this.listening)
			{
				return;
			}
			this.Close(true);
		}

		/// <summary>Shuts down the <see cref="T:System.Net.HttpListener" />.</summary>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060034CF RID: 13519 RVA: 0x000B88DB File Offset: 0x000B6ADB
		public void Close()
		{
			if (this.disposed)
			{
				return;
			}
			if (!this.listening)
			{
				this.disposed = true;
				return;
			}
			this.Close(true);
			this.disposed = true;
		}

		// Token: 0x060034D0 RID: 13520 RVA: 0x000B8904 File Offset: 0x000B6B04
		private void Close(bool force)
		{
			this.CheckDisposed();
			EndPointManager.RemoveListener(this);
			this.Cleanup(force);
		}

		// Token: 0x060034D1 RID: 13521 RVA: 0x000B891C File Offset: 0x000B6B1C
		private void Cleanup(bool close_existing)
		{
			object internalLock = this._internalLock;
			lock (internalLock)
			{
				if (close_existing)
				{
					ICollection keys = this.registry.Keys;
					HttpListenerContext[] array = new HttpListenerContext[keys.Count];
					keys.CopyTo(array, 0);
					this.registry.Clear();
					for (int i = array.Length - 1; i >= 0; i--)
					{
						array[i].Connection.Close(true);
					}
				}
				object syncRoot = this.connections.SyncRoot;
				lock (syncRoot)
				{
					ICollection keys2 = this.connections.Keys;
					HttpConnection[] array2 = new HttpConnection[keys2.Count];
					keys2.CopyTo(array2, 0);
					this.connections.Clear();
					for (int j = array2.Length - 1; j >= 0; j--)
					{
						array2[j].Close(true);
					}
				}
				ArrayList arrayList = this.ctx_queue;
				lock (arrayList)
				{
					HttpListenerContext[] array3 = (HttpListenerContext[])this.ctx_queue.ToArray(typeof(HttpListenerContext));
					this.ctx_queue.Clear();
					for (int k = array3.Length - 1; k >= 0; k--)
					{
						array3[k].Connection.Close(true);
					}
				}
				arrayList = this.wait_queue;
				lock (arrayList)
				{
					Exception ex = new ObjectDisposedException("listener");
					foreach (object obj in this.wait_queue)
					{
						((ListenerAsyncResult)obj).Complete(ex);
					}
					this.wait_queue.Clear();
				}
			}
		}

		/// <summary>Begins asynchronously retrieving an incoming request.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> object that indicates the status of the asynchronous operation.</returns>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that references the method to invoke when a client request is available.</param>
		/// <param name="state">A user-defined object that contains information about the operation. This object is passed to the <paramref name="callback" /> delegate when the operation completes.</param>
		/// <exception cref="T:System.Net.HttpListenerException">A Win32 function call failed. Check the exception's <see cref="P:System.Net.HttpListenerException.ErrorCode" /> property to determine the cause of the exception.</exception>
		/// <exception cref="T:System.InvalidOperationException">This object has not been started or is currently stopped.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x060034D2 RID: 13522 RVA: 0x000B8B68 File Offset: 0x000B6D68
		public IAsyncResult BeginGetContext(AsyncCallback callback, object state)
		{
			this.CheckDisposed();
			if (!this.listening)
			{
				throw new InvalidOperationException("Please, call Start before using this method.");
			}
			ListenerAsyncResult listenerAsyncResult = new ListenerAsyncResult(callback, state);
			ArrayList arrayList = this.wait_queue;
			lock (arrayList)
			{
				ArrayList arrayList2 = this.ctx_queue;
				lock (arrayList2)
				{
					HttpListenerContext contextFromQueue = this.GetContextFromQueue();
					if (contextFromQueue != null)
					{
						listenerAsyncResult.Complete(contextFromQueue, true);
						return listenerAsyncResult;
					}
				}
				this.wait_queue.Add(listenerAsyncResult);
			}
			return listenerAsyncResult;
		}

		/// <summary>Completes an asynchronous operation to retrieve an incoming client request.</summary>
		/// <returns>An <see cref="T:System.Net.HttpListenerContext" /> object that represents the client request.</returns>
		/// <param name="asyncResult">An <see cref="T:System.IAsyncResult" /> object that was obtained when the asynchronous operation was started.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> was not obtained by calling the <see cref="M:System.Net.HttpListener.BeginGetContext(System.AsyncCallback,System.Object)" /> method.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asyncResult" /> is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="M:System.Net.HttpListener.EndGetContext(System.IAsyncResult)" /> method was already called for the specified <paramref name="asyncResult" /> object.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x060034D3 RID: 13523 RVA: 0x000B8C1C File Offset: 0x000B6E1C
		public HttpListenerContext EndGetContext(IAsyncResult asyncResult)
		{
			this.CheckDisposed();
			if (asyncResult == null)
			{
				throw new ArgumentNullException("asyncResult");
			}
			ListenerAsyncResult listenerAsyncResult = asyncResult as ListenerAsyncResult;
			if (listenerAsyncResult == null)
			{
				throw new ArgumentException("Wrong IAsyncResult.", "asyncResult");
			}
			if (listenerAsyncResult.EndCalled)
			{
				throw new ArgumentException("Cannot reuse this IAsyncResult");
			}
			listenerAsyncResult.EndCalled = true;
			if (!listenerAsyncResult.IsCompleted)
			{
				listenerAsyncResult.AsyncWaitHandle.WaitOne();
			}
			ArrayList arrayList = this.wait_queue;
			lock (arrayList)
			{
				int num = this.wait_queue.IndexOf(listenerAsyncResult);
				if (num >= 0)
				{
					this.wait_queue.RemoveAt(num);
				}
			}
			HttpListenerContext context = listenerAsyncResult.GetContext();
			context.ParseAuthentication(this.SelectAuthenticationScheme(context));
			return context;
		}

		// Token: 0x060034D4 RID: 13524 RVA: 0x000B8CE8 File Offset: 0x000B6EE8
		internal AuthenticationSchemes SelectAuthenticationScheme(HttpListenerContext context)
		{
			if (this.AuthenticationSchemeSelectorDelegate != null)
			{
				return this.AuthenticationSchemeSelectorDelegate(context.Request);
			}
			return this.auth_schemes;
		}

		/// <summary>Waits for an incoming request and returns when one is received.</summary>
		/// <returns>An <see cref="T:System.Net.HttpListenerContext" /> object that represents a client request.</returns>
		/// <exception cref="T:System.Net.HttpListenerException">A Win32 function call failed. Check the exception's <see cref="P:System.Net.HttpListenerException.ErrorCode" /> property to determine the cause of the exception.</exception>
		/// <exception cref="T:System.InvalidOperationException">This object has not been started or is currently stopped.-or-The <see cref="T:System.Net.HttpListener" /> does not have any Uniform Resource Identifier (URI) prefixes to respond to. See Remarks.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		// Token: 0x060034D5 RID: 13525 RVA: 0x000B8D0C File Offset: 0x000B6F0C
		public HttpListenerContext GetContext()
		{
			if (this.prefixes.Count == 0)
			{
				throw new InvalidOperationException("Please, call AddPrefix before using this method.");
			}
			ListenerAsyncResult listenerAsyncResult = (ListenerAsyncResult)this.BeginGetContext(null, null);
			listenerAsyncResult.InGet = true;
			return this.EndGetContext(listenerAsyncResult);
		}

		/// <summary>Allows this instance to receive incoming requests.</summary>
		/// <exception cref="T:System.Net.HttpListenerException">A Win32 function call failed. Check the exception's <see cref="P:System.Net.HttpListenerException.ErrorCode" /> property to determine the cause of the exception.</exception>
		/// <exception cref="T:System.ObjectDisposedException">This object is closed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060034D6 RID: 13526 RVA: 0x000B8D4D File Offset: 0x000B6F4D
		public void Start()
		{
			this.CheckDisposed();
			if (this.listening)
			{
				return;
			}
			EndPointManager.AddListener(this);
			this.listening = true;
		}

		/// <summary>Causes this instance to stop receiving incoming requests.</summary>
		/// <exception cref="T:System.ObjectDisposedException">This object has been closed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060034D7 RID: 13527 RVA: 0x000B8D6B File Offset: 0x000B6F6B
		public void Stop()
		{
			this.CheckDisposed();
			this.listening = false;
			this.Close(false);
		}

		/// <summary>Releases the resources held by this <see cref="T:System.Net.HttpListener" /> object.</summary>
		// Token: 0x060034D8 RID: 13528 RVA: 0x000B8D81 File Offset: 0x000B6F81
		void IDisposable.Dispose()
		{
			if (this.disposed)
			{
				return;
			}
			this.Close(true);
			this.disposed = true;
		}

		/// <summary>Waits for an incoming request as an asynchronous operation.</summary>
		/// <returns>Returns <see cref="T:System.Threading.Tasks.Task`1" />.The task object representing the asynchronous operation. The <see cref="P:System.Threading.Tasks.Task`1.Result" /> property on the task object returns an <see cref="T:System.Net.HttpListenerContext" /> object that represents a client request.</returns>
		// Token: 0x060034D9 RID: 13529 RVA: 0x000B8D9A File Offset: 0x000B6F9A
		public Task<HttpListenerContext> GetContextAsync()
		{
			return Task<HttpListenerContext>.Factory.FromAsync(new Func<AsyncCallback, object, IAsyncResult>(this.BeginGetContext), new Func<IAsyncResult, HttpListenerContext>(this.EndGetContext), null);
		}

		// Token: 0x060034DA RID: 13530 RVA: 0x000B8DBF File Offset: 0x000B6FBF
		internal void CheckDisposed()
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x060034DB RID: 13531 RVA: 0x000B8DDA File Offset: 0x000B6FDA
		private HttpListenerContext GetContextFromQueue()
		{
			if (this.ctx_queue.Count == 0)
			{
				return null;
			}
			HttpListenerContext httpListenerContext = (HttpListenerContext)this.ctx_queue[0];
			this.ctx_queue.RemoveAt(0);
			return httpListenerContext;
		}

		// Token: 0x060034DC RID: 13532 RVA: 0x000B8E08 File Offset: 0x000B7008
		internal void RegisterContext(HttpListenerContext context)
		{
			object internalLock = this._internalLock;
			lock (internalLock)
			{
				this.registry[context] = context;
			}
			ListenerAsyncResult listenerAsyncResult = null;
			ArrayList arrayList = this.wait_queue;
			lock (arrayList)
			{
				if (this.wait_queue.Count == 0)
				{
					ArrayList arrayList2 = this.ctx_queue;
					lock (arrayList2)
					{
						this.ctx_queue.Add(context);
						goto IL_00A3;
					}
				}
				listenerAsyncResult = (ListenerAsyncResult)this.wait_queue[0];
				this.wait_queue.RemoveAt(0);
			}
			IL_00A3:
			if (listenerAsyncResult != null)
			{
				listenerAsyncResult.Complete(context);
			}
		}

		// Token: 0x060034DD RID: 13533 RVA: 0x000B8EEC File Offset: 0x000B70EC
		internal void UnregisterContext(HttpListenerContext context)
		{
			object internalLock = this._internalLock;
			lock (internalLock)
			{
				this.registry.Remove(context);
			}
			ArrayList arrayList = this.ctx_queue;
			lock (arrayList)
			{
				int num = this.ctx_queue.IndexOf(context);
				if (num >= 0)
				{
					this.ctx_queue.RemoveAt(num);
				}
			}
		}

		// Token: 0x060034DE RID: 13534 RVA: 0x000B8F78 File Offset: 0x000B7178
		internal void AddConnection(HttpConnection cnc)
		{
			this.connections[cnc] = cnc;
		}

		// Token: 0x060034DF RID: 13535 RVA: 0x000B8F87 File Offset: 0x000B7187
		internal void RemoveConnection(HttpConnection cnc)
		{
			this.connections.Remove(cnc);
		}

		// Token: 0x04001EEE RID: 7918
		private MonoTlsProvider tlsProvider;

		// Token: 0x04001EEF RID: 7919
		private MonoTlsSettings tlsSettings;

		// Token: 0x04001EF0 RID: 7920
		private X509Certificate certificate;

		// Token: 0x04001EF1 RID: 7921
		private AuthenticationSchemes auth_schemes;

		// Token: 0x04001EF2 RID: 7922
		private HttpListenerPrefixCollection prefixes;

		// Token: 0x04001EF3 RID: 7923
		private AuthenticationSchemeSelector auth_selector;

		// Token: 0x04001EF4 RID: 7924
		private string realm;

		// Token: 0x04001EF5 RID: 7925
		private bool ignore_write_exceptions;

		// Token: 0x04001EF6 RID: 7926
		private bool unsafe_ntlm_auth;

		// Token: 0x04001EF7 RID: 7927
		private bool listening;

		// Token: 0x04001EF8 RID: 7928
		private bool disposed;

		// Token: 0x04001EF9 RID: 7929
		private readonly object _internalLock;

		// Token: 0x04001EFA RID: 7930
		private Hashtable registry;

		// Token: 0x04001EFB RID: 7931
		private ArrayList ctx_queue;

		// Token: 0x04001EFC RID: 7932
		private ArrayList wait_queue;

		// Token: 0x04001EFD RID: 7933
		private Hashtable connections;

		// Token: 0x04001EFE RID: 7934
		private ServiceNameStore defaultServiceNames;

		// Token: 0x04001EFF RID: 7935
		private ExtendedProtectionPolicy extendedProtectionPolicy;

		// Token: 0x04001F00 RID: 7936
		private HttpListener.ExtendedProtectionSelector extendedProtectionSelectorDelegate;

		/// <summary>A delegate called to determine the <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> to use for each <see cref="T:System.Net.HttpListener" /> request.</summary>
		/// <returns>An <see cref="T:System.Security.Authentication.ExtendedProtection.ExtendedProtectionPolicy" /> object that specifies the extended protection policy to use for this request.</returns>
		/// <param name="request">The <see cref="T:System.Net.HttpListenerRequest" /> to determine the extended protection policy that the <see cref="T:System.Net.HttpListener" /> instance will use to provide extended protection.</param>
		// Token: 0x0200068C RID: 1676
		// (Invoke) Token: 0x060034E1 RID: 13537
		public delegate ExtendedProtectionPolicy ExtendedProtectionSelector(HttpListenerRequest request);
	}
}
