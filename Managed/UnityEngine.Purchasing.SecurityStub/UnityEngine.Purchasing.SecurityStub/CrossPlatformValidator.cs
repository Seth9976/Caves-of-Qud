using System;

namespace UnityEngine.Purchasing.Security
{
	// Token: 0x02000005 RID: 5
	public class CrossPlatformValidator
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020E1 File Offset: 0x000002E1
		public CrossPlatformValidator(byte[] googlePublicKey, byte[] appleRootCert, string appBundleId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020EE File Offset: 0x000002EE
		public CrossPlatformValidator(byte[] googlePublicKey, byte[] appleRootCert, string googleBundleId, string appleBundleId)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020FB File Offset: 0x000002FB
		public IPurchaseReceipt[] Validate(string unityIAPReceipt)
		{
			throw new NotImplementedException();
		}
	}
}
