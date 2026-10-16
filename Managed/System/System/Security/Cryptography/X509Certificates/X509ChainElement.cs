using System;
using Unity;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents an element of an X.509 chain.</summary>
	// Token: 0x020002DD RID: 733
	public class X509ChainElement
	{
		// Token: 0x0600170B RID: 5899 RVA: 0x0005B3CC File Offset: 0x000595CC
		internal X509ChainElement(X509Certificate2 certificate)
		{
			this.certificate = certificate;
			this.info = string.Empty;
		}

		/// <summary>Gets the X.509 certificate at a particular chain element.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object.</returns>
		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x0600170C RID: 5900 RVA: 0x0005B3E6 File Offset: 0x000595E6
		public X509Certificate2 Certificate
		{
			get
			{
				return this.certificate;
			}
		}

		/// <summary>Gets the error status of the current X.509 certificate in a chain.</summary>
		/// <returns>An array of <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainStatus" /> objects.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x0600170D RID: 5901 RVA: 0x0005B3EE File Offset: 0x000595EE
		public X509ChainStatus[] ChainElementStatus
		{
			get
			{
				return this.status;
			}
		}

		/// <summary>Gets additional error information from an unmanaged certificate chain structure.</summary>
		/// <returns>A string representing the pwszExtendedErrorInfo member of the unmanaged CERT_CHAIN_ELEMENT structure in the Crypto API.</returns>
		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x0600170E RID: 5902 RVA: 0x0005B3F6 File Offset: 0x000595F6
		public string Information
		{
			get
			{
				return this.info;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x0600170F RID: 5903 RVA: 0x0005B3FE File Offset: 0x000595FE
		// (set) Token: 0x06001710 RID: 5904 RVA: 0x0005B406 File Offset: 0x00059606
		internal X509ChainStatusFlags StatusFlags
		{
			get
			{
				return this.compressed_status_flags;
			}
			set
			{
				this.compressed_status_flags = value;
			}
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x0005B410 File Offset: 0x00059610
		private int Count(X509ChainStatusFlags flags)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 1;
			while (num2++ < 32)
			{
				if ((flags & (X509ChainStatusFlags)num3) == (X509ChainStatusFlags)num3)
				{
					num++;
				}
				num3 <<= 1;
			}
			return num;
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x0005B43F File Offset: 0x0005963F
		private void Set(X509ChainStatus[] status, ref int position, X509ChainStatusFlags flags, X509ChainStatusFlags mask)
		{
			if ((flags & mask) != X509ChainStatusFlags.NoError)
			{
				status[position].Status = mask;
				status[position].StatusInformation = X509ChainStatus.GetInformation(mask);
				position++;
			}
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x0005B470 File Offset: 0x00059670
		internal void UncompressFlags()
		{
			if (this.compressed_status_flags == X509ChainStatusFlags.NoError)
			{
				this.status = new X509ChainStatus[0];
				return;
			}
			int num = this.Count(this.compressed_status_flags);
			this.status = new X509ChainStatus[num];
			int num2 = 0;
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.UntrustedRoot);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.NotTimeValid);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.NotTimeNested);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.Revoked);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.NotSignatureValid);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.NotValidForUsage);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.RevocationStatusUnknown);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.Cyclic);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.InvalidExtension);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.InvalidPolicyConstraints);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.InvalidBasicConstraints);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.InvalidNameConstraints);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.HasNotSupportedNameConstraint);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.HasNotDefinedNameConstraint);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.HasNotPermittedNameConstraint);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.HasExcludedNameConstraint);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.PartialChain);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.CtlNotTimeValid);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.CtlNotSignatureValid);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.CtlNotValidForUsage);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.OfflineRevocation);
			this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.NoIssuanceChainPolicy);
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x00013BAE File Offset: 0x00011DAE
		internal X509ChainElement()
		{
			global::Unity.ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04000D25 RID: 3365
		private X509Certificate2 certificate;

		// Token: 0x04000D26 RID: 3366
		private X509ChainStatus[] status;

		// Token: 0x04000D27 RID: 3367
		private string info;

		// Token: 0x04000D28 RID: 3368
		private X509ChainStatusFlags compressed_status_flags;
	}
}
