using System;

namespace System.Net
{
	/// <summary>The HTTP headers that may be specified in a client request.</summary>
	// Token: 0x020005D5 RID: 1493
	public enum HttpRequestHeader
	{
		/// <summary>The Cache-Control header, which specifies directives that must be obeyed by all cache control mechanisms along the request/response chain.</summary>
		// Token: 0x04001AD3 RID: 6867
		CacheControl,
		/// <summary>The Connection header, which specifies options that are desired for a particular connection.</summary>
		// Token: 0x04001AD4 RID: 6868
		Connection,
		/// <summary>The Date header, which specifies the date and time at which the request originated.</summary>
		// Token: 0x04001AD5 RID: 6869
		Date,
		/// <summary>The Keep-Alive header, which specifies a parameter used into order to maintain a persistent connection.</summary>
		// Token: 0x04001AD6 RID: 6870
		KeepAlive,
		/// <summary>The Pragma header, which specifies implementation-specific directives that might apply to any agent along the request/response chain.</summary>
		// Token: 0x04001AD7 RID: 6871
		Pragma,
		/// <summary>The Trailer header, which specifies the header fields present in the trailer of a message encoded with chunked transfer-coding.</summary>
		// Token: 0x04001AD8 RID: 6872
		Trailer,
		/// <summary>The Transfer-Encoding header, which specifies what (if any) type of transformation that has been applied to the message body.</summary>
		// Token: 0x04001AD9 RID: 6873
		TransferEncoding,
		/// <summary>The Upgrade header, which specifies additional communications protocols that the client supports.</summary>
		// Token: 0x04001ADA RID: 6874
		Upgrade,
		/// <summary>The Via header, which specifies intermediate protocols to be used by gateway and proxy agents.</summary>
		// Token: 0x04001ADB RID: 6875
		Via,
		/// <summary>The Warning header, which specifies additional information about that status or transformation of a message that might not be reflected in the message.</summary>
		// Token: 0x04001ADC RID: 6876
		Warning,
		/// <summary>The Allow header, which specifies the set of HTTP methods supported.</summary>
		// Token: 0x04001ADD RID: 6877
		Allow,
		/// <summary>The Content-Length header, which specifies the length, in bytes, of the accompanying body data.</summary>
		// Token: 0x04001ADE RID: 6878
		ContentLength,
		/// <summary>The Content-Type header, which specifies the MIME type of the accompanying body data.</summary>
		// Token: 0x04001ADF RID: 6879
		ContentType,
		/// <summary>The Content-Encoding header, which specifies the encodings that have been applied to the accompanying body data.</summary>
		// Token: 0x04001AE0 RID: 6880
		ContentEncoding,
		/// <summary>The Content-Langauge header, which specifies the natural language(s) of the accompanying body data.</summary>
		// Token: 0x04001AE1 RID: 6881
		ContentLanguage,
		/// <summary>The Content-Location header, which specifies a URI from which the accompanying body may be obtained.</summary>
		// Token: 0x04001AE2 RID: 6882
		ContentLocation,
		/// <summary>The Content-MD5 header, which specifies the MD5 digest of the accompanying body data, for the purpose of providing an end-to-end message integrity check.</summary>
		// Token: 0x04001AE3 RID: 6883
		ContentMd5,
		/// <summary>The Content-Range header, which specifies where in the full body the accompanying partial body data should be applied.</summary>
		// Token: 0x04001AE4 RID: 6884
		ContentRange,
		/// <summary>The Expires header, which specifies the date and time after which the accompanying body data should be considered stale.</summary>
		// Token: 0x04001AE5 RID: 6885
		Expires,
		/// <summary>The Last-Modified header, which specifies the date and time at which the accompanying body data was last modified.</summary>
		// Token: 0x04001AE6 RID: 6886
		LastModified,
		/// <summary>The Accept header, which specifies the MIME types that are acceptable for the response.</summary>
		// Token: 0x04001AE7 RID: 6887
		Accept,
		/// <summary>The Accept-Charset header, which specifies the character sets that are acceptable for the response.</summary>
		// Token: 0x04001AE8 RID: 6888
		AcceptCharset,
		/// <summary>The Accept-Encoding header, which specifies the content encodings that are acceptable for the response.</summary>
		// Token: 0x04001AE9 RID: 6889
		AcceptEncoding,
		/// <summary>The Accept-Langauge header, which specifies that natural languages that are preferred for the response.</summary>
		// Token: 0x04001AEA RID: 6890
		AcceptLanguage,
		/// <summary>The Authorization header, which specifies the credentials that the client presents in order to authenticate itself to the server.</summary>
		// Token: 0x04001AEB RID: 6891
		Authorization,
		/// <summary>The Cookie header, which specifies cookie data presented to the server.</summary>
		// Token: 0x04001AEC RID: 6892
		Cookie,
		/// <summary>The Expect header, which specifies particular server behaviors that are required by the client.</summary>
		// Token: 0x04001AED RID: 6893
		Expect,
		/// <summary>The From header, which specifies an Internet E-mail address for the human user who controls the requesting user agent.</summary>
		// Token: 0x04001AEE RID: 6894
		From,
		/// <summary>The Host header, which specifies the host name and port number of the resource being requested.</summary>
		// Token: 0x04001AEF RID: 6895
		Host,
		/// <summary>The If-Match header, which specifies that the requested operation should be performed only if the client's cached copy of the indicated resource is current.</summary>
		// Token: 0x04001AF0 RID: 6896
		IfMatch,
		/// <summary>The If-Modified-Since header, which specifies that the requested operation should be performed only if the requested resource has been modified since the indicated data and time.</summary>
		// Token: 0x04001AF1 RID: 6897
		IfModifiedSince,
		/// <summary>The If-None-Match header, which specifies that the requested operation should be performed only if none of client's cached copies of the indicated resources are current.</summary>
		// Token: 0x04001AF2 RID: 6898
		IfNoneMatch,
		/// <summary>The If-Range header, which specifies that only the specified range of the requested resource should be sent, if the client's cached copy is current.</summary>
		// Token: 0x04001AF3 RID: 6899
		IfRange,
		/// <summary>The If-Unmodified-Since header, which specifies that the requested operation should be performed only if the requested resource has not been modified since the indicated date and time.</summary>
		// Token: 0x04001AF4 RID: 6900
		IfUnmodifiedSince,
		/// <summary>The Max-Forwards header, which specifies an integer indicating the remaining number of times that this request may be forwarded.</summary>
		// Token: 0x04001AF5 RID: 6901
		MaxForwards,
		/// <summary>The Proxy-Authorization header, which specifies the credentials that the client presents in order to authenticate itself to a proxy.</summary>
		// Token: 0x04001AF6 RID: 6902
		ProxyAuthorization,
		/// <summary>The Referer header, which specifies the URI of the resource from which the request URI was obtained.</summary>
		// Token: 0x04001AF7 RID: 6903
		Referer,
		/// <summary>The Range header, which specifies the the sub-range(s) of the response that the client requests be returned in lieu of the entire response.</summary>
		// Token: 0x04001AF8 RID: 6904
		Range,
		/// <summary>The TE header, which specifies the transfer encodings that are acceptable for the response.</summary>
		// Token: 0x04001AF9 RID: 6905
		Te,
		/// <summary>The Translate header, a Microsoft extension to the HTTP specification used in conjunction with WebDAV functionality.</summary>
		// Token: 0x04001AFA RID: 6906
		Translate,
		/// <summary>The User-Agent header, which specifies information about the client agent.</summary>
		// Token: 0x04001AFB RID: 6907
		UserAgent
	}
}
