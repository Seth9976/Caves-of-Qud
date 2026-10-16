using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace System.Net
{
	// Token: 0x0200066A RID: 1642
	internal class ServerCertValidationCallback
	{
		// Token: 0x060033C6 RID: 13254 RVA: 0x000B4450 File Offset: 0x000B2650
		internal ServerCertValidationCallback(RemoteCertificateValidationCallback validationCallback)
		{
			this.m_ValidationCallback = validationCallback;
			this.m_Context = ExecutionContext.Capture();
		}

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x060033C7 RID: 13255 RVA: 0x000B446A File Offset: 0x000B266A
		internal RemoteCertificateValidationCallback ValidationCallback
		{
			get
			{
				return this.m_ValidationCallback;
			}
		}

		// Token: 0x060033C8 RID: 13256 RVA: 0x000B4474 File Offset: 0x000B2674
		internal void Callback(object state)
		{
			ServerCertValidationCallback.CallbackContext callbackContext = (ServerCertValidationCallback.CallbackContext)state;
			callbackContext.result = this.m_ValidationCallback(callbackContext.request, callbackContext.certificate, callbackContext.chain, callbackContext.sslPolicyErrors);
		}

		// Token: 0x060033C9 RID: 13257 RVA: 0x000B44B4 File Offset: 0x000B26B4
		internal bool Invoke(object request, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			if (this.m_Context == null)
			{
				return this.m_ValidationCallback(request, certificate, chain, sslPolicyErrors);
			}
			ExecutionContext executionContext = this.m_Context.CreateCopy();
			ServerCertValidationCallback.CallbackContext callbackContext = new ServerCertValidationCallback.CallbackContext(request, certificate, chain, sslPolicyErrors);
			ExecutionContext.Run(executionContext, new ContextCallback(this.Callback), callbackContext);
			return callbackContext.result;
		}

		// Token: 0x04001E75 RID: 7797
		private readonly RemoteCertificateValidationCallback m_ValidationCallback;

		// Token: 0x04001E76 RID: 7798
		private readonly ExecutionContext m_Context;

		// Token: 0x0200066B RID: 1643
		private class CallbackContext
		{
			// Token: 0x060033CA RID: 13258 RVA: 0x000B4508 File Offset: 0x000B2708
			internal CallbackContext(object request, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
			{
				this.request = request;
				this.certificate = certificate;
				this.chain = chain;
				this.sslPolicyErrors = sslPolicyErrors;
			}

			// Token: 0x04001E77 RID: 7799
			internal readonly object request;

			// Token: 0x04001E78 RID: 7800
			internal readonly X509Certificate certificate;

			// Token: 0x04001E79 RID: 7801
			internal readonly X509Chain chain;

			// Token: 0x04001E7A RID: 7802
			internal readonly SslPolicyErrors sslPolicyErrors;

			// Token: 0x04001E7B RID: 7803
			internal bool result;
		}
	}
}
