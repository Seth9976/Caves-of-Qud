using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002E0 RID: 736
	internal abstract class X509ChainImpl : IDisposable
	{
		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06001727 RID: 5927
		public abstract bool IsValid { get; }

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001728 RID: 5928
		public abstract IntPtr Handle { get; }

		// Token: 0x06001729 RID: 5929 RVA: 0x0005B7D5 File Offset: 0x000599D5
		protected void ThrowIfContextInvalid()
		{
			if (!this.IsValid)
			{
				throw X509Helper2.GetInvalidChainContextException();
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x0600172A RID: 5930
		public abstract X509ChainElementCollection ChainElements { get; }

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x0600172B RID: 5931
		// (set) Token: 0x0600172C RID: 5932
		public abstract X509ChainPolicy ChainPolicy { get; set; }

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x0600172D RID: 5933
		public abstract X509ChainStatus[] ChainStatus { get; }

		// Token: 0x0600172E RID: 5934
		public abstract bool Build(X509Certificate2 certificate);

		// Token: 0x0600172F RID: 5935
		public abstract void AddStatus(X509ChainStatusFlags errorCode);

		// Token: 0x06001730 RID: 5936
		public abstract void Reset();

		// Token: 0x06001731 RID: 5937 RVA: 0x0005B7E5 File Offset: 0x000599E5
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001732 RID: 5938 RVA: 0x00003917 File Offset: 0x00001B17
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06001733 RID: 5939 RVA: 0x0005B7F4 File Offset: 0x000599F4
		~X509ChainImpl()
		{
			this.Dispose(false);
		}
	}
}
