using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Btls
{
	// Token: 0x02000116 RID: 278
	internal class X509ChainImplBtls : X509ChainImpl
	{
		// Token: 0x0600069E RID: 1694 RVA: 0x000121CF File Offset: 0x000103CF
		internal X509ChainImplBtls(MonoBtlsX509Chain chain)
		{
			this.chain = chain.Copy();
			this.policy = new X509ChainPolicy();
		}

		// Token: 0x0600069F RID: 1695 RVA: 0x000121F0 File Offset: 0x000103F0
		internal X509ChainImplBtls(MonoBtlsX509StoreCtx storeCtx)
		{
			this.storeCtx = storeCtx.Copy();
			this.chain = storeCtx.GetChain();
			this.policy = new X509ChainPolicy();
			this.untrustedChain = storeCtx.GetUntrusted();
			if (this.untrustedChain != null)
			{
				this.untrusted = new X509Certificate2Collection();
				this.policy.ExtraStore = this.untrusted;
				for (int i = 0; i < this.untrustedChain.Count; i++)
				{
					using (X509CertificateImplBtls x509CertificateImplBtls = new X509CertificateImplBtls(this.untrustedChain.GetCertificate(i)))
					{
						this.untrusted.Add(new X509Certificate2(x509CertificateImplBtls));
					}
				}
			}
		}

		// Token: 0x060006A0 RID: 1696 RVA: 0x000122AC File Offset: 0x000104AC
		internal X509ChainImplBtls()
		{
			this.chain = new MonoBtlsX509Chain();
			this.elements = new X509ChainElementCollection();
			this.policy = new X509ChainPolicy();
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060006A1 RID: 1697 RVA: 0x000122D5 File Offset: 0x000104D5
		public override bool IsValid
		{
			get
			{
				return this.chain != null && this.chain.IsValid;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060006A2 RID: 1698 RVA: 0x000122EC File Offset: 0x000104EC
		public override IntPtr Handle
		{
			get
			{
				return this.chain.Handle.DangerousGetHandle();
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060006A3 RID: 1699 RVA: 0x000122FE File Offset: 0x000104FE
		internal MonoBtlsX509Chain Chain
		{
			get
			{
				base.ThrowIfContextInvalid();
				return this.chain;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060006A4 RID: 1700 RVA: 0x0001230C File Offset: 0x0001050C
		internal MonoBtlsX509StoreCtx StoreCtx
		{
			get
			{
				base.ThrowIfContextInvalid();
				return this.storeCtx;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060006A5 RID: 1701 RVA: 0x0001231C File Offset: 0x0001051C
		public override X509ChainElementCollection ChainElements
		{
			get
			{
				base.ThrowIfContextInvalid();
				if (this.elements != null)
				{
					return this.elements;
				}
				this.elements = new X509ChainElementCollection();
				this.certificates = new X509Certificate2[this.chain.Count];
				for (int i = 0; i < this.certificates.Length; i++)
				{
					using (X509CertificateImplBtls x509CertificateImplBtls = new X509CertificateImplBtls(this.chain.GetCertificate(i)))
					{
						this.certificates[i] = new X509Certificate2(x509CertificateImplBtls);
					}
					this.elements.Add(this.certificates[i]);
				}
				return this.elements;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060006A6 RID: 1702 RVA: 0x000123C8 File Offset: 0x000105C8
		// (set) Token: 0x060006A7 RID: 1703 RVA: 0x000123D0 File Offset: 0x000105D0
		public override X509ChainPolicy ChainPolicy
		{
			get
			{
				return this.policy;
			}
			set
			{
				this.policy = value;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060006A8 RID: 1704 RVA: 0x000123D9 File Offset: 0x000105D9
		public override X509ChainStatus[] ChainStatus
		{
			get
			{
				List<X509ChainStatus> list = this.chainStatusList;
				return ((list != null) ? list.ToArray() : null) ?? new X509ChainStatus[0];
			}
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x000123F7 File Offset: 0x000105F7
		public override void AddStatus(X509ChainStatusFlags errorCode)
		{
			if (this.chainStatusList == null)
			{
				this.chainStatusList = new List<X509ChainStatus>();
			}
			this.chainStatusList.Add(new X509ChainStatus(errorCode));
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x00003062 File Offset: 0x00001262
		public override bool Build(X509Certificate2 certificate)
		{
			return false;
		}

		// Token: 0x060006AB RID: 1707 RVA: 0x00012420 File Offset: 0x00010620
		public override void Reset()
		{
			if (this.certificates != null)
			{
				X509Certificate2[] array = this.certificates;
				for (int i = 0; i < array.Length; i++)
				{
					array[i].Dispose();
				}
				this.certificates = null;
			}
			if (this.elements != null)
			{
				this.elements.Clear();
				this.elements = null;
			}
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x00012474 File Offset: 0x00010674
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.chain != null)
				{
					this.chain.Dispose();
					this.chain = null;
				}
				if (this.storeCtx != null)
				{
					this.storeCtx.Dispose();
					this.storeCtx = null;
				}
				if (this.untrustedChain != null)
				{
					this.untrustedChain.Dispose();
					this.untrustedChain = null;
				}
				if (this.untrusted != null)
				{
					foreach (X509Certificate2 x509Certificate in this.untrusted)
					{
						x509Certificate.Dispose();
					}
					this.untrusted = null;
				}
				if (this.certificates != null)
				{
					X509Certificate2[] array = this.certificates;
					for (int i = 0; i < array.Length; i++)
					{
						array[i].Dispose();
					}
					this.certificates = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x04000493 RID: 1171
		private MonoBtlsX509StoreCtx storeCtx;

		// Token: 0x04000494 RID: 1172
		private MonoBtlsX509Chain chain;

		// Token: 0x04000495 RID: 1173
		private MonoBtlsX509Chain untrustedChain;

		// Token: 0x04000496 RID: 1174
		private X509ChainElementCollection elements;

		// Token: 0x04000497 RID: 1175
		private X509Certificate2Collection untrusted;

		// Token: 0x04000498 RID: 1176
		private X509Certificate2[] certificates;

		// Token: 0x04000499 RID: 1177
		private X509ChainPolicy policy;

		// Token: 0x0400049A RID: 1178
		private List<X509ChainStatus> chainStatusList;
	}
}
