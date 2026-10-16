using System;
using System.Security;

namespace System.IO.IsolatedStorage
{
	/// <summary>Provides settings for maintaining the quota size for isolated storage. </summary>
	// Token: 0x02000B78 RID: 2936
	public class IsolatedStorageSecurityState : SecurityState
	{
		// Token: 0x06006AF7 RID: 27383 RVA: 0x0016E173 File Offset: 0x0016C373
		internal IsolatedStorageSecurityState()
		{
		}

		/// <summary>Gets the option for managing isolated storage security. </summary>
		/// <returns>The option to increase the isolated quota storage size.</returns>
		// Token: 0x17001269 RID: 4713
		// (get) Token: 0x06006AF8 RID: 27384 RVA: 0x00022807 File Offset: 0x00020A07
		public IsolatedStorageSecurityOptions Options
		{
			get
			{
				return IsolatedStorageSecurityOptions.IncreaseQuotaForApplication;
			}
		}

		/// <summary>Gets or sets the current size of the quota for isolated storage.</summary>
		/// <returns>The current quota size, in bytes.</returns>
		// Token: 0x1700126A RID: 4714
		// (get) Token: 0x06006AF9 RID: 27385 RVA: 0x000479F8 File Offset: 0x00045BF8
		// (set) Token: 0x06006AFA RID: 27386 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public long Quota
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
			}
		}

		/// <summary>Gets the current usage size in isolated storage.</summary>
		/// <returns>The current usage size, in bytes.</returns>
		// Token: 0x1700126B RID: 4715
		// (get) Token: 0x06006AFB RID: 27387 RVA: 0x000479F8 File Offset: 0x00045BF8
		public long UsedSize
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <exception cref="T:System.IO.IsolatedStorage.IsolatedStorageException">The state is not available.</exception>
		// Token: 0x06006AFC RID: 27388 RVA: 0x000479F8 File Offset: 0x00045BF8
		public override void EnsureState()
		{
			throw new NotImplementedException();
		}
	}
}
