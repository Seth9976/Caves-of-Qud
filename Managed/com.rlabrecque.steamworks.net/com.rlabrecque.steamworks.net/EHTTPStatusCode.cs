using System;

namespace Steamworks
{
	// Token: 0x02000153 RID: 339
	public enum EHTTPStatusCode
	{
		// Token: 0x04000949 RID: 2377
		k_EHTTPStatusCodeInvalid,
		// Token: 0x0400094A RID: 2378
		k_EHTTPStatusCode100Continue = 100,
		// Token: 0x0400094B RID: 2379
		k_EHTTPStatusCode101SwitchingProtocols,
		// Token: 0x0400094C RID: 2380
		k_EHTTPStatusCode200OK = 200,
		// Token: 0x0400094D RID: 2381
		k_EHTTPStatusCode201Created,
		// Token: 0x0400094E RID: 2382
		k_EHTTPStatusCode202Accepted,
		// Token: 0x0400094F RID: 2383
		k_EHTTPStatusCode203NonAuthoritative,
		// Token: 0x04000950 RID: 2384
		k_EHTTPStatusCode204NoContent,
		// Token: 0x04000951 RID: 2385
		k_EHTTPStatusCode205ResetContent,
		// Token: 0x04000952 RID: 2386
		k_EHTTPStatusCode206PartialContent,
		// Token: 0x04000953 RID: 2387
		k_EHTTPStatusCode300MultipleChoices = 300,
		// Token: 0x04000954 RID: 2388
		k_EHTTPStatusCode301MovedPermanently,
		// Token: 0x04000955 RID: 2389
		k_EHTTPStatusCode302Found,
		// Token: 0x04000956 RID: 2390
		k_EHTTPStatusCode303SeeOther,
		// Token: 0x04000957 RID: 2391
		k_EHTTPStatusCode304NotModified,
		// Token: 0x04000958 RID: 2392
		k_EHTTPStatusCode305UseProxy,
		// Token: 0x04000959 RID: 2393
		k_EHTTPStatusCode307TemporaryRedirect = 307,
		// Token: 0x0400095A RID: 2394
		k_EHTTPStatusCode308PermanentRedirect,
		// Token: 0x0400095B RID: 2395
		k_EHTTPStatusCode400BadRequest = 400,
		// Token: 0x0400095C RID: 2396
		k_EHTTPStatusCode401Unauthorized,
		// Token: 0x0400095D RID: 2397
		k_EHTTPStatusCode402PaymentRequired,
		// Token: 0x0400095E RID: 2398
		k_EHTTPStatusCode403Forbidden,
		// Token: 0x0400095F RID: 2399
		k_EHTTPStatusCode404NotFound,
		// Token: 0x04000960 RID: 2400
		k_EHTTPStatusCode405MethodNotAllowed,
		// Token: 0x04000961 RID: 2401
		k_EHTTPStatusCode406NotAcceptable,
		// Token: 0x04000962 RID: 2402
		k_EHTTPStatusCode407ProxyAuthRequired,
		// Token: 0x04000963 RID: 2403
		k_EHTTPStatusCode408RequestTimeout,
		// Token: 0x04000964 RID: 2404
		k_EHTTPStatusCode409Conflict,
		// Token: 0x04000965 RID: 2405
		k_EHTTPStatusCode410Gone,
		// Token: 0x04000966 RID: 2406
		k_EHTTPStatusCode411LengthRequired,
		// Token: 0x04000967 RID: 2407
		k_EHTTPStatusCode412PreconditionFailed,
		// Token: 0x04000968 RID: 2408
		k_EHTTPStatusCode413RequestEntityTooLarge,
		// Token: 0x04000969 RID: 2409
		k_EHTTPStatusCode414RequestURITooLong,
		// Token: 0x0400096A RID: 2410
		k_EHTTPStatusCode415UnsupportedMediaType,
		// Token: 0x0400096B RID: 2411
		k_EHTTPStatusCode416RequestedRangeNotSatisfiable,
		// Token: 0x0400096C RID: 2412
		k_EHTTPStatusCode417ExpectationFailed,
		// Token: 0x0400096D RID: 2413
		k_EHTTPStatusCode4xxUnknown,
		// Token: 0x0400096E RID: 2414
		k_EHTTPStatusCode429TooManyRequests = 429,
		// Token: 0x0400096F RID: 2415
		k_EHTTPStatusCode444ConnectionClosed = 444,
		// Token: 0x04000970 RID: 2416
		k_EHTTPStatusCode500InternalServerError = 500,
		// Token: 0x04000971 RID: 2417
		k_EHTTPStatusCode501NotImplemented,
		// Token: 0x04000972 RID: 2418
		k_EHTTPStatusCode502BadGateway,
		// Token: 0x04000973 RID: 2419
		k_EHTTPStatusCode503ServiceUnavailable,
		// Token: 0x04000974 RID: 2420
		k_EHTTPStatusCode504GatewayTimeout,
		// Token: 0x04000975 RID: 2421
		k_EHTTPStatusCode505HTTPVersionNotSupported,
		// Token: 0x04000976 RID: 2422
		k_EHTTPStatusCode5xxUnknown = 599
	}
}
