using System;
using System.Security.Cryptography.X509Certificates;

namespace System.Net
{
	// Token: 0x02000676 RID: 1654
	internal class DefaultCertificatePolicy : ICertificatePolicy
	{
		// Token: 0x060033FA RID: 13306 RVA: 0x000B51C5 File Offset: 0x000B33C5
		public bool CheckValidationResult(ServicePoint point, X509Certificate certificate, WebRequest request, int certificateProblem)
		{
			return ServicePointManager.ServerCertificateValidationCallback != null || (certificateProblem == -2146762495 || certificateProblem == 0);
		}
	}
}
