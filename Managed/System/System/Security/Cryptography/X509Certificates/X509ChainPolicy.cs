using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents the chain policy to be applied when building an X509 certificate chain. This class cannot be inherited.</summary>
	// Token: 0x020002E2 RID: 738
	public sealed class X509ChainPolicy
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainPolicy" /> class. </summary>
		// Token: 0x0600175F RID: 5983 RVA: 0x0005CA25 File Offset: 0x0005AC25
		public X509ChainPolicy()
		{
			this.Reset();
		}

		// Token: 0x06001760 RID: 5984 RVA: 0x0005CA33 File Offset: 0x0005AC33
		internal X509ChainPolicy(X509CertificateCollection store)
		{
			this.store = store;
			this.Reset();
		}

		/// <summary>Gets a collection of object identifiers (OIDs) specifying which application policies or enhanced key usages (EKUs) the certificate supports.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.OidCollection" />  object.</returns>
		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06001761 RID: 5985 RVA: 0x0005CA48 File Offset: 0x0005AC48
		public OidCollection ApplicationPolicy
		{
			get
			{
				return this.apps;
			}
		}

		/// <summary>Gets a collection of object identifiers (OIDs) specifying which certificate policies the certificate supports.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.OidCollection" /> object.</returns>
		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06001762 RID: 5986 RVA: 0x0005CA50 File Offset: 0x0005AC50
		public OidCollection CertificatePolicy
		{
			get
			{
				return this.cert;
			}
		}

		/// <summary>Represents an additional collection of certificates that can be searched by the chaining engine when validating a certificate chain.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</returns>
		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06001763 RID: 5987 RVA: 0x0005CA58 File Offset: 0x0005AC58
		// (set) Token: 0x06001764 RID: 5988 RVA: 0x0005CAE0 File Offset: 0x0005ACE0
		public X509Certificate2Collection ExtraStore
		{
			get
			{
				if (this.store2 != null)
				{
					return this.store2;
				}
				this.store2 = new X509Certificate2Collection();
				if (this.store != null)
				{
					foreach (X509Certificate x509Certificate in this.store)
					{
						this.store2.Add(new X509Certificate2(x509Certificate));
					}
				}
				return this.store2;
			}
			internal set
			{
				this.store2 = value;
			}
		}

		/// <summary>Gets or sets values for X509 revocation flags.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509RevocationFlag" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Security.Cryptography.X509Certificates.X509RevocationFlag" /> value supplied is not a valid flag. </exception>
		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06001765 RID: 5989 RVA: 0x0005CAE9 File Offset: 0x0005ACE9
		// (set) Token: 0x06001766 RID: 5990 RVA: 0x0005CAF1 File Offset: 0x0005ACF1
		public X509RevocationFlag RevocationFlag
		{
			get
			{
				return this.rflag;
			}
			set
			{
				if (value < X509RevocationFlag.EndCertificateOnly || value > X509RevocationFlag.ExcludeRoot)
				{
					throw new ArgumentException("RevocationFlag");
				}
				this.rflag = value;
			}
		}

		/// <summary>Gets or sets values for X509 certificate revocation mode.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509RevocationMode" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Security.Cryptography.X509Certificates.X509RevocationMode" /> value supplied is not a valid flag. </exception>
		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06001767 RID: 5991 RVA: 0x0005CB0D File Offset: 0x0005AD0D
		// (set) Token: 0x06001768 RID: 5992 RVA: 0x0005CB15 File Offset: 0x0005AD15
		public X509RevocationMode RevocationMode
		{
			get
			{
				return this.mode;
			}
			set
			{
				if (value < X509RevocationMode.NoCheck || value > X509RevocationMode.Offline)
				{
					throw new ArgumentException("RevocationMode");
				}
				this.mode = value;
			}
		}

		/// <summary>Gets the time span that elapsed during online revocation verification or downloading the certificate revocation list (CRL).</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> object.</returns>
		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x06001769 RID: 5993 RVA: 0x0005CB31 File Offset: 0x0005AD31
		// (set) Token: 0x0600176A RID: 5994 RVA: 0x0005CB39 File Offset: 0x0005AD39
		public TimeSpan UrlRetrievalTimeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				this.timeout = value;
			}
		}

		/// <summary>Gets verification flags for the certificate.</summary>
		/// <returns>A value from the <see cref="T:System.Security.Cryptography.X509Certificates.X509VerificationFlags" /> enumeration.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Security.Cryptography.X509Certificates.X509VerificationFlags" /> value supplied is not a valid flag. <see cref="F:System.Security.Cryptography.X509Certificates.X509VerificationFlags.NoFlag" /> is the default value. </exception>
		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x0600176B RID: 5995 RVA: 0x0005CB42 File Offset: 0x0005AD42
		// (set) Token: 0x0600176C RID: 5996 RVA: 0x0005CB4A File Offset: 0x0005AD4A
		public X509VerificationFlags VerificationFlags
		{
			get
			{
				return this.vflags;
			}
			set
			{
				if ((value | X509VerificationFlags.AllFlags) != X509VerificationFlags.AllFlags)
				{
					throw new ArgumentException("VerificationFlags");
				}
				this.vflags = value;
			}
		}

		/// <summary>The time that the certificate was verified expressed in local time.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object.</returns>
		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x0600176D RID: 5997 RVA: 0x0005CB6C File Offset: 0x0005AD6C
		// (set) Token: 0x0600176E RID: 5998 RVA: 0x0005CB74 File Offset: 0x0005AD74
		public DateTime VerificationTime
		{
			get
			{
				return this.vtime;
			}
			set
			{
				this.vtime = value;
			}
		}

		/// <summary>Resets the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainPolicy" /> members to their default values.</summary>
		// Token: 0x0600176F RID: 5999 RVA: 0x0005CB80 File Offset: 0x0005AD80
		public void Reset()
		{
			this.apps = new OidCollection();
			this.cert = new OidCollection();
			this.store2 = null;
			this.rflag = X509RevocationFlag.ExcludeRoot;
			this.mode = X509RevocationMode.Online;
			this.timeout = TimeSpan.Zero;
			this.vflags = X509VerificationFlags.NoFlag;
			this.vtime = DateTime.Now;
		}

		// Token: 0x04000D3B RID: 3387
		private OidCollection apps;

		// Token: 0x04000D3C RID: 3388
		private OidCollection cert;

		// Token: 0x04000D3D RID: 3389
		private X509CertificateCollection store;

		// Token: 0x04000D3E RID: 3390
		private X509Certificate2Collection store2;

		// Token: 0x04000D3F RID: 3391
		private X509RevocationFlag rflag;

		// Token: 0x04000D40 RID: 3392
		private X509RevocationMode mode;

		// Token: 0x04000D41 RID: 3393
		private TimeSpan timeout;

		// Token: 0x04000D42 RID: 3394
		private X509VerificationFlags vflags;

		// Token: 0x04000D43 RID: 3395
		private DateTime vtime;
	}
}
