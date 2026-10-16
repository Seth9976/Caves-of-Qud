using System;
using Microsoft.Win32.SafeHandles;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents a chain-building engine for <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> certificates.</summary>
	// Token: 0x020002DC RID: 732
	public class X509Chain : IDisposable
	{
		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x060016F8 RID: 5880 RVA: 0x0005B27C File Offset: 0x0005947C
		internal X509ChainImpl Impl
		{
			get
			{
				X509Helper2.ThrowIfContextInvalid(this.impl);
				return this.impl;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x060016F9 RID: 5881 RVA: 0x0005B28F File Offset: 0x0005948F
		internal bool IsValid
		{
			get
			{
				return X509Helper2.IsValid(this.impl);
			}
		}

		// Token: 0x060016FA RID: 5882 RVA: 0x0005B29C File Offset: 0x0005949C
		internal void ThrowIfContextInvalid()
		{
			X509Helper2.ThrowIfContextInvalid(this.impl);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> class.</summary>
		// Token: 0x060016FB RID: 5883 RVA: 0x0005B2A9 File Offset: 0x000594A9
		public X509Chain()
			: this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> class specifying a value that indicates whether the machine context should be used.</summary>
		/// <param name="useMachineContext">true to use the machine context; false to use the current user context. </param>
		// Token: 0x060016FC RID: 5884 RVA: 0x0005B2B2 File Offset: 0x000594B2
		public X509Chain(bool useMachineContext)
		{
			this.impl = X509Helper2.CreateChainImpl(useMachineContext);
		}

		// Token: 0x060016FD RID: 5885 RVA: 0x0005B2C6 File Offset: 0x000594C6
		internal X509Chain(X509ChainImpl impl)
		{
			X509Helper2.ThrowIfContextInvalid(impl);
			this.impl = impl;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> class using an <see cref="T:System.IntPtr" /> handle to an X.509 chain.</summary>
		/// <param name="chainContext">An <see cref="T:System.IntPtr" /> handle to an X.509 chain.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="chainContext" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="chainContext" /> parameter points to an invalid context.</exception>
		// Token: 0x060016FE RID: 5886 RVA: 0x0005B2DB File Offset: 0x000594DB
		[MonoTODO("Mono's X509Chain is fully managed. All handles are invalid.")]
		public X509Chain(IntPtr chainContext)
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets a handle to an X.509 chain.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> handle to an X.509 chain.</returns>
		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x060016FF RID: 5887 RVA: 0x0005B2E8 File Offset: 0x000594E8
		[MonoTODO("Mono's X509Chain is fully managed. Always returns IntPtr.Zero.")]
		public IntPtr ChainContext
		{
			get
			{
				if (this.impl != null && this.impl.IsValid)
				{
					return this.impl.Handle;
				}
				return IntPtr.Zero;
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElement" /> objects.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" /> object.</returns>
		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06001700 RID: 5888 RVA: 0x0005B310 File Offset: 0x00059510
		public X509ChainElementCollection ChainElements
		{
			get
			{
				return this.Impl.ChainElements;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainPolicy" /> to use when building an X.509 certificate chain.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainPolicy" /> object associated with this X.509 chain.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value being set for this property is null.</exception>
		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06001701 RID: 5889 RVA: 0x0005B31D File Offset: 0x0005951D
		// (set) Token: 0x06001702 RID: 5890 RVA: 0x0005B32A File Offset: 0x0005952A
		public X509ChainPolicy ChainPolicy
		{
			get
			{
				return this.Impl.ChainPolicy;
			}
			set
			{
				this.Impl.ChainPolicy = value;
			}
		}

		/// <summary>Gets the status of each element in an <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> object.</summary>
		/// <returns>An array of <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainStatus" /> objects.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700046B RID: 1131
		// (get) Token: 0x06001703 RID: 5891 RVA: 0x0005B338 File Offset: 0x00059538
		public X509ChainStatus[] ChainStatus
		{
			get
			{
				return this.Impl.ChainStatus;
			}
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06001704 RID: 5892 RVA: 0x0000829A File Offset: 0x0000649A
		public SafeX509ChainHandle SafeHandle
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Builds an X.509 chain using the policy specified in <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainPolicy" />.</summary>
		/// <returns>true if the X.509 certificate is valid; otherwise, false.</returns>
		/// <param name="certificate">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="certificate" /> is not a valid certificate or is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="certificate" /> is unreadable. </exception>
		// Token: 0x06001705 RID: 5893 RVA: 0x0005B345 File Offset: 0x00059545
		[MonoTODO("Not totally RFC3280 compliant, but neither is MS implementation...")]
		public bool Build(X509Certificate2 certificate)
		{
			return this.Impl.Build(certificate);
		}

		/// <summary>Clears the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> object.</summary>
		// Token: 0x06001706 RID: 5894 RVA: 0x0005B353 File Offset: 0x00059553
		public void Reset()
		{
			this.Impl.Reset();
		}

		/// <summary>Creates an <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> object after querying for the mapping defined in the CryptoConfig file, and maps the chain to that mapping.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06001707 RID: 5895 RVA: 0x0005B360 File Offset: 0x00059560
		public static X509Chain Create()
		{
			return (X509Chain)CryptoConfig.CreateFromName("X509Chain");
		}

		// Token: 0x06001708 RID: 5896 RVA: 0x0005B371 File Offset: 0x00059571
		[SecuritySafeCritical]
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001709 RID: 5897 RVA: 0x0005B380 File Offset: 0x00059580
		protected virtual void Dispose(bool disposing)
		{
			if (this.impl != null)
			{
				this.impl.Dispose();
				this.impl = null;
			}
		}

		// Token: 0x0600170A RID: 5898 RVA: 0x0005B39C File Offset: 0x0005959C
		~X509Chain()
		{
			this.Dispose(false);
		}

		// Token: 0x04000D24 RID: 3364
		private X509ChainImpl impl;
	}
}
