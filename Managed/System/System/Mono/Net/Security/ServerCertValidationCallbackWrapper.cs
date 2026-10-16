using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Interface;

namespace Mono.Net.Security
{
	// Token: 0x02000098 RID: 152
	// (Invoke) Token: 0x06000250 RID: 592
	internal delegate bool ServerCertValidationCallbackWrapper(ServerCertValidationCallback callback, X509Certificate certificate, X509Chain chain, MonoSslPolicyErrors sslPolicyErrors);
}
