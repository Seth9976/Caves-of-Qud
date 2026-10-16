using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using AiUnity.Common.Attributes;
using AiUnity.NLog.Core.Common;
using AiUnity.NLog.Core.Internal;
using UnityEngine.Scripting;

namespace AiUnity.NLog.Core.Targets
{
	// Token: 0x0200022D RID: 557
	[Target("WebService")]
	[Preserve]
	public sealed class WebServiceTarget : MethodCallTargetBase
	{
		// Token: 0x0600149B RID: 5275 RVA: 0x00073F60 File Offset: 0x00072160
		public WebServiceTarget()
		{
			this.Protocol = WebServiceProtocol.HttpPost;
			this.Encoding = Encoding.UTF8;
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x0600149C RID: 5276 RVA: 0x00073F7A File Offset: 0x0007217A
		// (set) Token: 0x0600149D RID: 5277 RVA: 0x00073F82 File Offset: 0x00072182
		[Display("URL", "Web service URL.", false, 0)]
		public Uri Url { get; set; }

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x0600149E RID: 5278 RVA: 0x00073F8B File Offset: 0x0007218B
		// (set) Token: 0x0600149F RID: 5279 RVA: 0x00073F93 File Offset: 0x00072193
		public string MethodName { get; set; }

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060014A0 RID: 5280 RVA: 0x00073F9C File Offset: 0x0007219C
		// (set) Token: 0x060014A1 RID: 5281 RVA: 0x00073FA4 File Offset: 0x000721A4
		public string Namespace { get; set; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x00073FAD File Offset: 0x000721AD
		// (set) Token: 0x060014A3 RID: 5283 RVA: 0x00073FB5 File Offset: 0x000721B5
		[DefaultValue("Post")]
		public WebServiceProtocol Protocol { get; set; }

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060014A4 RID: 5284 RVA: 0x00073FBE File Offset: 0x000721BE
		// (set) Token: 0x060014A5 RID: 5285 RVA: 0x00073FC6 File Offset: 0x000721C6
		public Encoding Encoding { get; set; }

		// Token: 0x060014A6 RID: 5286 RVA: 0x00073FCF File Offset: 0x000721CF
		protected override void DoInvoke(object[] parameters)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x00073FD8 File Offset: 0x000721D8
		protected override void DoInvoke(object[] parameters, AsyncContinuation continuation)
		{
			HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.Url);
			byte[] postPayload = null;
			switch (this.Protocol)
			{
			case WebServiceProtocol.Soap11:
				postPayload = this.PrepareSoap11Request(request, parameters);
				break;
			case WebServiceProtocol.Soap12:
				postPayload = this.PrepareSoap12Request(request, parameters);
				break;
			case WebServiceProtocol.HttpPost:
				postPayload = this.PreparePostRequest(request, parameters);
				break;
			case WebServiceProtocol.HttpGet:
				postPayload = this.PrepareGetRequest(request, parameters);
				break;
			}
			AsyncCallback <>9__2;
			AsyncContinuation sendContinuation = delegate(Exception ex)
			{
				if (ex != null)
				{
					continuation(ex);
					return;
				}
				WebRequest request2 = request;
				AsyncCallback asyncCallback;
				if ((asyncCallback = <>9__2) == null)
				{
					asyncCallback = (<>9__2 = delegate(IAsyncResult r)
					{
						try
						{
							using (request.EndGetResponse(r))
							{
							}
							continuation(null);
						}
						catch (Exception ex)
						{
							if (ex.MustBeRethrown())
							{
								throw;
							}
							continuation(ex);
						}
					});
				}
				request2.BeginGetResponse(asyncCallback, null);
			};
			if (postPayload != null && postPayload.Length != 0)
			{
				request.BeginGetRequestStream(delegate(IAsyncResult r)
				{
					try
					{
						using (Stream stream = request.EndGetRequestStream(r))
						{
							stream.Write(postPayload, 0, postPayload.Length);
						}
						sendContinuation(null);
					}
					catch (Exception ex2)
					{
						if (ex2.MustBeRethrown())
						{
							throw;
						}
						continuation(ex2);
					}
				}, null);
				return;
			}
			sendContinuation(null);
		}

		// Token: 0x060014A8 RID: 5288 RVA: 0x000740CC File Offset: 0x000722CC
		private byte[] PrepareSoap11Request(HttpWebRequest request, object[] parameters)
		{
			request.Method = "POST";
			request.ContentType = "text/xml; charset=" + this.Encoding.WebName;
			if (this.Namespace.EndsWith("/", StringComparison.Ordinal))
			{
				request.Headers["SOAPAction"] = this.Namespace + this.MethodName;
			}
			else
			{
				request.Headers["SOAPAction"] = this.Namespace + "/" + this.MethodName;
			}
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				XmlWriter xmlWriter = XmlWriter.Create(memoryStream, new XmlWriterSettings
				{
					Encoding = this.Encoding
				});
				xmlWriter.WriteStartElement("soap", "Envelope", "http://schemas.xmlsoap.org/soap/envelope/");
				xmlWriter.WriteStartElement("Body", "http://schemas.xmlsoap.org/soap/envelope/");
				xmlWriter.WriteStartElement(this.MethodName, this.Namespace);
				int num = 0;
				foreach (MethodCallParameter methodCallParameter in base.Parameters)
				{
					xmlWriter.WriteElementString(methodCallParameter.Name, Convert.ToString(parameters[num]));
					num++;
				}
				xmlWriter.WriteEndElement();
				xmlWriter.WriteEndElement();
				xmlWriter.WriteEndElement();
				xmlWriter.Flush();
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x060014A9 RID: 5289 RVA: 0x00074240 File Offset: 0x00072440
		private byte[] PrepareSoap12Request(HttpWebRequest request, object[] parameterValues)
		{
			request.Method = "POST";
			request.ContentType = "text/xml; charset=" + this.Encoding.WebName;
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				XmlWriter xmlWriter = XmlWriter.Create(memoryStream, new XmlWriterSettings
				{
					Encoding = this.Encoding
				});
				xmlWriter.WriteStartElement("soap12", "Envelope", "http://www.w3.org/2003/05/soap-envelope");
				xmlWriter.WriteStartElement("Body", "http://www.w3.org/2003/05/soap-envelope");
				xmlWriter.WriteStartElement(this.MethodName, this.Namespace);
				int num = 0;
				foreach (MethodCallParameter methodCallParameter in base.Parameters)
				{
					xmlWriter.WriteElementString(methodCallParameter.Name, Convert.ToString(parameterValues[num]));
					num++;
				}
				xmlWriter.WriteEndElement();
				xmlWriter.WriteEndElement();
				xmlWriter.WriteEndElement();
				xmlWriter.Flush();
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x00074358 File Offset: 0x00072558
		private byte[] PreparePostRequest(HttpWebRequest request, object[] parameterValues)
		{
			request.Method = "POST";
			return this.PrepareHttpRequest(request, parameterValues);
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x0007436D File Offset: 0x0007256D
		private byte[] PrepareGetRequest(HttpWebRequest request, object[] parameterValues)
		{
			request.Method = "GET";
			return this.PrepareHttpRequest(request, parameterValues);
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x00074384 File Offset: 0x00072584
		private byte[] PrepareHttpRequest(HttpWebRequest request, object[] parameterValues)
		{
			request.ContentType = "application/x-www-form-urlencoded; charset=" + this.Encoding.WebName;
			string text = string.Empty;
			byte[] array;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				StreamWriter streamWriter = new StreamWriter(memoryStream, this.Encoding);
				streamWriter.Write(string.Empty);
				int num = 0;
				foreach (MethodCallParameter methodCallParameter in base.Parameters)
				{
					streamWriter.Write(text);
					streamWriter.Write(methodCallParameter.Name);
					streamWriter.Write("=");
					streamWriter.Write(UrlHelper.UrlEncode(Convert.ToString(parameterValues[num]), true));
					text = "&";
					num++;
				}
				streamWriter.Flush();
				array = memoryStream.ToArray();
			}
			return array;
		}

		// Token: 0x04000D3F RID: 3391
		private const string SoapEnvelopeNamespace = "http://schemas.xmlsoap.org/soap/envelope/";

		// Token: 0x04000D40 RID: 3392
		private const string Soap12EnvelopeNamespace = "http://www.w3.org/2003/05/soap-envelope";
	}
}
