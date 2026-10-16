using System;
using System.Collections.Specialized;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Permissions;

namespace System.Web
{
	/// <summary>Encapsulates the HTTP intrinsic object that provides helper methods for processing Web requests.</summary>
	// Token: 0x020000B5 RID: 181
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	[AspNetHostingPermission(SecurityAction.InheritanceDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public class HttpServerUtilityWrapper : HttpServerUtilityBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Web.HttpServerUtilityWrapper" /> class.</summary>
		/// <param name="httpServerUtility">The object that this wrapper class provides access to.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="httpServerUtility" /> parameter is null.</exception>
		// Token: 0x060009EA RID: 2538 RVA: 0x000183C5 File Offset: 0x000165C5
		public HttpServerUtilityWrapper(HttpServerUtility httpServerUtility)
		{
			if (httpServerUtility == null)
			{
				throw new ArgumentNullException("httpServerUtility");
			}
			this.w = httpServerUtility;
		}

		/// <summary>Gets the server's computer name.</summary>
		/// <returns>The name of the server computer.</returns>
		/// <exception cref="T:System.Web.HttpException">The computer name cannot be found.</exception>
		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x060009EB RID: 2539 RVA: 0x000183E2 File Offset: 0x000165E2
		public override string MachineName
		{
			get
			{
				return this.w.MachineName;
			}
		}

		/// <summary>Gets or sets the request time-out value in seconds.</summary>
		/// <returns>The time-out value for requests.</returns>
		/// <exception cref="T:System.Web.HttpException">The current <see cref="T:System.Web.HttpContext" /> object is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The time-out period is null or otherwise cannot be set.</exception>
		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x000183EF File Offset: 0x000165EF
		// (set) Token: 0x060009ED RID: 2541 RVA: 0x000183FC File Offset: 0x000165FC
		public override int ScriptTimeout
		{
			get
			{
				return this.w.ScriptTimeout;
			}
			set
			{
				this.w.ScriptTimeout = value;
			}
		}

		/// <summary>Clears the most recent exception.</summary>
		// Token: 0x060009EE RID: 2542 RVA: 0x0001840A File Offset: 0x0001660A
		public override void ClearError()
		{
			this.w.ClearError();
		}

		/// <summary>Creates a server instance of a COM object that is identified by the object's programmatic identifier (ProgID).</summary>
		/// <returns>The new object.</returns>
		/// <param name="progID">The class or type of object to create an instance of.</param>
		/// <exception cref="T:System.Web.HttpException">An instance of the object could not be created.</exception>
		// Token: 0x060009EF RID: 2543 RVA: 0x00018417 File Offset: 0x00016617
		public override object CreateObject(string progID)
		{
			return this.w.CreateObject(progID);
		}

		/// <summary>Creates a server instance of a COM object that is identified by the object's type.</summary>
		/// <returns>The new object.</returns>
		/// <param name="type">A type that represents the object to create.</param>
		// Token: 0x060009F0 RID: 2544 RVA: 0x00018425 File Offset: 0x00016625
		public override object CreateObject(Type type)
		{
			return this.w.CreateObject(type);
		}

		/// <summary>Creates a server instance of a COM object that is identified by the object's class identifier (CLSID).</summary>
		/// <returns>The new object.</returns>
		/// <param name="clsid">The class identifier of the object to create an instance of.</param>
		/// <exception cref="T:System.Web.HttpException">An instance of the object cannot be created.</exception>
		// Token: 0x060009F1 RID: 2545 RVA: 0x00018433 File Offset: 0x00016633
		public override object CreateObjectFromClsid(string clsid)
		{
			return this.w.CreateObjectFromClsid(clsid);
		}

		/// <summary>Executes the handler for the specified virtual path in the context of the current process.</summary>
		/// <param name="path">The URL of the handler to execute.</param>
		/// <exception cref="T:System.Web.HttpException">The current <see cref="T:System.Web.HttpContext" /> object is null.- or -An error occurred when the handler specified by <paramref name="path" /> was executed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. - or -<paramref name="path" /> is not a virtual path.</exception>
		// Token: 0x060009F2 RID: 2546 RVA: 0x00018441 File Offset: 0x00016641
		public override void Execute(string path)
		{
			this.w.Execute(path);
		}

		/// <summary>Executes the handler for the specified virtual path in the context of the current process and specifies whether to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</summary>
		/// <param name="path">The URL of the handler to execute. </param>
		/// <param name="preserveForm">true to preserve the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections; false to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</param>
		/// <exception cref="T:System.Web.HttpException">The current <see cref="T:System.Web.HttpContext" /> object is null.- or -An error occurred when the handler specified by <paramref name="path" /> was executed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. - or -<paramref name="path" /> is not a virtual path. </exception>
		// Token: 0x060009F3 RID: 2547 RVA: 0x0001844F File Offset: 0x0001664F
		public override void Execute(string path, bool preserveForm)
		{
			this.w.Execute(path, preserveForm);
		}

		/// <summary>Executes the handler for the specified virtual path in the context of the current process, using a <see cref="T:System.IO.TextWriter" /> instance to capture output from the executed handler.</summary>
		/// <param name="path">The URL of the handler to execute. </param>
		/// <param name="writer">An object to capture the output. </param>
		/// <exception cref="T:System.Web.HttpException">The current <see cref="T:System.Web.HttpContext" /> is null. - or -An error occurred when the handler specified by <paramref name="path" /> was executed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. - or -<paramref name="path" /> is not a virtual path. </exception>
		// Token: 0x060009F4 RID: 2548 RVA: 0x0001845E File Offset: 0x0001665E
		public override void Execute(string path, TextWriter writer)
		{
			this.w.Execute(path, writer);
		}

		/// <summary>Executes the handler for the specified virtual path in the context of the current request, using a <see cref="T:System.IO.TextWriter" /> instance to capture output from the page and a value that indicates whether to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</summary>
		/// <param name="path">The URL of the handler to execute.</param>
		/// <param name="writer">The object to capture the output.</param>
		/// <param name="preserveForm">true to preserve the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections; false to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</param>
		/// <exception cref="T:System.Web.HttpException">The current <see cref="T:System.Web.HttpContext" /> instance is null. - or -<paramref name="path" /> ends with a period (.).- or -An error occurred when the handler specified by <paramref name="path" /> was executed.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="path" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="path" /> is not a virtual path.</exception>
		// Token: 0x060009F5 RID: 2549 RVA: 0x0001846D File Offset: 0x0001666D
		public override void Execute(string path, TextWriter writer, bool preserveForm)
		{
			this.w.Execute(path, writer, preserveForm);
		}

		/// <summary>Executes the specified handler in the context of the current process, using a <see cref="T:System.IO.TextWriter" /> instance to capture output from the executed handler and a value that specifies whether to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</summary>
		/// <param name="handler">The HTTP handler that implements the interface to transfer the current request to.</param>
		/// <param name="writer">The object to capture the output.</param>
		/// <param name="preserveForm">true to preserve the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections; false to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</param>
		/// <exception cref="T:System.Web.HttpException">An error occurred when the handler specified by <paramref name="handler" /> was executed.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="handler" /> parameter is null.</exception>
		// Token: 0x060009F6 RID: 2550 RVA: 0x0001847D File Offset: 0x0001667D
		public override void Execute(IHttpHandler handler, TextWriter writer, bool preserveForm)
		{
			this.w.Execute(handler, writer, preserveForm);
		}

		/// <summary>Returns the most recent exception.</summary>
		/// <returns>The previous exception that was thrown.</returns>
		// Token: 0x060009F7 RID: 2551 RVA: 0x0001848D File Offset: 0x0001668D
		public override Exception GetLastError()
		{
			return this.w.GetLastError();
		}

		/// <summary>Decodes an HTML-encoded string and returns the decoded string.</summary>
		/// <returns>The decoded text.</returns>
		/// <param name="s">The HTML string to decode.</param>
		// Token: 0x060009F8 RID: 2552 RVA: 0x0001849A File Offset: 0x0001669A
		public override string HtmlDecode(string s)
		{
			return this.w.HtmlDecode(s);
		}

		/// <summary>Decodes an HTML-encoded string and returns the results in a stream.</summary>
		/// <param name="s">The HTML string to decode.</param>
		/// <param name="output">The stream to contain the decoded string.</param>
		// Token: 0x060009F9 RID: 2553 RVA: 0x000184A8 File Offset: 0x000166A8
		public override void HtmlDecode(string s, TextWriter output)
		{
			this.w.HtmlDecode(s, output);
		}

		/// <summary>HTML-encodes a string and returns the encoded string.</summary>
		/// <returns>The HTML-encoded text.</returns>
		/// <param name="s">The string to encode.</param>
		// Token: 0x060009FA RID: 2554 RVA: 0x000184B7 File Offset: 0x000166B7
		public override string HtmlEncode(string s)
		{
			return this.w.HtmlEncode(s);
		}

		/// <summary>HTML-encodes a string and sends the resulting output to an output stream.</summary>
		/// <param name="s">The string to encode. </param>
		/// <param name="output">The stream to contain the encoded string.</param>
		// Token: 0x060009FB RID: 2555 RVA: 0x000184C5 File Offset: 0x000166C5
		public override void HtmlEncode(string s, TextWriter output)
		{
			this.w.HtmlEncode(s, output);
		}

		/// <summary>Returns the physical file path that corresponds to the specified virtual path on the Web server.</summary>
		/// <returns>The physical file path that corresponds to <paramref name="path" />.</returns>
		/// <param name="path">The virtual path to get the physical path for.</param>
		/// <exception cref="T:System.Web.HttpException">The current <see cref="T:System.Web.HttpContext" /> object is null.</exception>
		// Token: 0x060009FC RID: 2556 RVA: 0x000184D4 File Offset: 0x000166D4
		public override string MapPath(string path)
		{
			return this.w.MapPath(path);
		}

		/// <summary>Terminates execution of the current process and starts execution of a page or handler that is specified with a URL.</summary>
		/// <param name="path">The URL of the page or handler to execute.</param>
		// Token: 0x060009FD RID: 2557 RVA: 0x000184E2 File Offset: 0x000166E2
		public override void Transfer(string path)
		{
			this.w.Transfer(path);
		}

		/// <summary>Terminates execution of the current page and starts execution of a different page or handler by using the specified URL and a value that specifies whether to clear the <see cref="P:System.Web.HttpRequestBase.QueryString" /> and <see cref="P:System.Web.HttpRequestBase.Form" /> collections.</summary>
		/// <param name="path">The URL of the page or handler to execute.</param>
		/// <param name="preserveForm">true to preserve the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections; false to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</param>
		/// <exception cref="T:System.ApplicationException">The current page request is a callback.</exception>
		// Token: 0x060009FE RID: 2558 RVA: 0x000184F0 File Offset: 0x000166F0
		public override void Transfer(string path, bool preserveForm)
		{
			this.w.Transfer(path, preserveForm);
		}

		/// <summary>Terminates execution of the current process and starts execution of a new request, using a custom HTTP handler and a value that specifies whether to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</summary>
		/// <param name="handler">The HTTP handler to transfer the current request to.</param>
		/// <param name="preserveForm">true to preserve the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections; false to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</param>
		/// <exception cref="T:System.ApplicationException">The current page request is a callback.</exception>
		// Token: 0x060009FF RID: 2559 RVA: 0x000184FF File Offset: 0x000166FF
		public override void Transfer(IHttpHandler handler, bool preserveForm)
		{
			this.w.Transfer(handler, preserveForm);
		}

		/// <summary>Asynchronously executes the end point at the specified URL.</summary>
		/// <param name="path">The URL of the page or handler to execute.</param>
		/// <exception cref="T:System.PlatformNotSupportedException">The request requires the integrated pipeline mode of IIS 7.0.</exception>
		/// <exception cref="T:System.Web.HttpException">The server is not available to handle the request.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> parameter is invalid.</exception>
		// Token: 0x06000A00 RID: 2560 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO]
		public override void TransferRequest(string path)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously executes the endpoint at the specified URL and specifies whether to clear the <see cref="P:System.Web.HttpRequestBase.QueryString" /> and <see cref="P:System.Web.HttpRequestBase.Form" /> collections.</summary>
		/// <param name="path">The URL of the page to execute.</param>
		/// <param name="preserveForm">true to preserve the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections; false to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</param>
		/// <exception cref="T:System.PlatformNotSupportedException">The request requires the integrated pipeline mode of IIS 7.0.</exception>
		/// <exception cref="T:System.Web.HttpException">The server is not available to handle the request.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> parameter is invalid.</exception>
		// Token: 0x06000A01 RID: 2561 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO]
		public override void TransferRequest(string path, bool preserveForm)
		{
			throw new NotImplementedException();
		}

		/// <summary>Asynchronously executes the endpoint at the specified URL by using the specified HTTP method and headers.</summary>
		/// <param name="path">The URL of the page or handler to execute.</param>
		/// <param name="preserveForm">true to preserve the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections; false to clear the <see cref="P:System.Web.HttpRequest.QueryString" /> and <see cref="P:System.Web.HttpRequest.Form" /> collections.</param>
		/// <param name="method">The HTTP method (GET, POST, and so on) to use for the new request. If null, the HTTP method of the original request is used.</param>
		/// <param name="headers">A collection of request headers for the new request.</param>
		/// <exception cref="T:System.PlatformNotSupportedException">The request requires IIS 7.0 running in integrated mode.</exception>
		/// <exception cref="T:System.Web.HttpException">The server is not available to handle the request.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="path" /> parameter is invalid.</exception>
		// Token: 0x06000A02 RID: 2562 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO]
		public override void TransferRequest(string path, bool preserveForm, string method, NameValueCollection headers)
		{
			throw new NotImplementedException();
		}

		/// <summary>Decodes a URL-encoded string and returns the decoded string.</summary>
		/// <returns>The decoded text.</returns>
		/// <param name="s">The string to decode.</param>
		// Token: 0x06000A03 RID: 2563 RVA: 0x0001850E File Offset: 0x0001670E
		public override string UrlDecode(string s)
		{
			return this.w.UrlDecode(s);
		}

		/// <summary>Decodes a URL-encoded string and sends the resulting output to a stream.</summary>
		/// <param name="s">The HTML string to decode.</param>
		/// <param name="output">The stream to contain the decoded string.</param>
		// Token: 0x06000A04 RID: 2564 RVA: 0x0001851C File Offset: 0x0001671C
		public override void UrlDecode(string s, TextWriter output)
		{
			this.w.UrlDecode(s, output);
		}

		/// <summary>URL-encodes a string and returns the encoded string.</summary>
		/// <returns>The URL-encoded text.</returns>
		/// <param name="s">The text to URL-encode.</param>
		// Token: 0x06000A05 RID: 2565 RVA: 0x0001852B File Offset: 0x0001672B
		public override string UrlEncode(string s)
		{
			return this.w.UrlEncode(s);
		}

		/// <summary>URL-encodes a string and sends the resulting output to a stream.</summary>
		/// <param name="s">The string to encode.</param>
		/// <param name="output">The stream to contain the encoded string.</param>
		// Token: 0x06000A06 RID: 2566 RVA: 0x00018539 File Offset: 0x00016739
		public override void UrlEncode(string s, TextWriter output)
		{
			this.w.UrlEncode(s, output);
		}

		/// <summary>URL-encodes the path section of a URL string.</summary>
		/// <returns>The URL-encoded text.</returns>
		/// <param name="s">The string to URL-encode.</param>
		// Token: 0x06000A07 RID: 2567 RVA: 0x00018548 File Offset: 0x00016748
		public override string UrlPathEncode(string s)
		{
			return this.w.UrlPathEncode(s);
		}

		/// <summary>Decodes a URL string token into an equivalent byte array by using base64 digits.</summary>
		/// <returns>The byte array that contains the decoded URL string token.</returns>
		/// <param name="input">The URL string token to decode.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="input" /> parameter is null.</exception>
		// Token: 0x06000A08 RID: 2568 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO]
		public override byte[] UrlTokenDecode(string input)
		{
			throw new NotImplementedException();
		}

		/// <summary>Encodes a byte array into an equivalent string representation by using base64 digits, which makes it usable for transmission on the URL.</summary>
		/// <returns>The string that contains the encoded array if the length of <paramref name="input" /> is greater than 1; otherwise, an empty string ("").</returns>
		/// <param name="input">The byte array to encode.</param>
		/// <exception cref="T:System.ArgumentNullException">The value of the <paramref name="input" /> parameter is null.</exception>
		// Token: 0x06000A09 RID: 2569 RVA: 0x00003A2B File Offset: 0x00001C2B
		[global::System.MonoTODO]
		public override string UrlTokenEncode(byte[] input)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04001018 RID: 4120
		private HttpServerUtility w;
	}
}
