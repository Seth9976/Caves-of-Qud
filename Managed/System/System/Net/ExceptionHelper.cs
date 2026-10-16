using System;

namespace System.Net
{
	// Token: 0x020005E4 RID: 1508
	internal static class ExceptionHelper
	{
		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06003062 RID: 12386 RVA: 0x000A6A94 File Offset: 0x000A4C94
		internal static NotImplementedException MethodNotImplementedException
		{
			get
			{
				return new NotImplementedException(SR.GetString("This method is not implemented by this class."));
			}
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06003063 RID: 12387 RVA: 0x000A6AA5 File Offset: 0x000A4CA5
		internal static NotImplementedException PropertyNotImplementedException
		{
			get
			{
				return new NotImplementedException(SR.GetString("This property is not implemented by this class."));
			}
		}

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06003064 RID: 12388 RVA: 0x000A6AB6 File Offset: 0x000A4CB6
		internal static WebException TimeoutException
		{
			get
			{
				return new WebException("The operation has timed out.");
			}
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06003065 RID: 12389 RVA: 0x000A6AC2 File Offset: 0x000A4CC2
		internal static NotSupportedException MethodNotSupportedException
		{
			get
			{
				return new NotSupportedException(SR.GetString("This method is not supported by this class."));
			}
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06003066 RID: 12390 RVA: 0x000A6AD3 File Offset: 0x000A4CD3
		internal static NotSupportedException PropertyNotSupportedException
		{
			get
			{
				return new NotSupportedException(SR.GetString("This property is not supported by this class."));
			}
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06003067 RID: 12391 RVA: 0x000A6AE4 File Offset: 0x000A4CE4
		internal static WebException IsolatedException
		{
			get
			{
				return new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.KeepAliveFailure), WebExceptionStatus.KeepAliveFailure, WebExceptionInternalStatus.Isolated, null);
			}
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06003068 RID: 12392 RVA: 0x000A6AFB File Offset: 0x000A4CFB
		internal static WebException RequestAbortedException
		{
			get
			{
				return new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.RequestCanceled), WebExceptionStatus.RequestCanceled);
			}
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x06003069 RID: 12393 RVA: 0x000A6B0E File Offset: 0x000A4D0E
		internal static WebException CacheEntryNotFoundException
		{
			get
			{
				return new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.CacheEntryNotFound), WebExceptionStatus.CacheEntryNotFound);
			}
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x0600306A RID: 12394 RVA: 0x000A6B23 File Offset: 0x000A4D23
		internal static WebException RequestProhibitedByCachePolicyException
		{
			get
			{
				return new WebException(NetRes.GetWebStatusString("net_requestaborted", WebExceptionStatus.RequestProhibitedByCachePolicy), WebExceptionStatus.RequestProhibitedByCachePolicy);
			}
		}
	}
}
