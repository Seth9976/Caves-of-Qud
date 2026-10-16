using System;
using System.IO;
using System.Text;
using System.Web.Services.Diagnostics;
using System.Xml.Serialization;

namespace System.Web.Services.Protocols
{
	// Token: 0x02000094 RID: 148
	internal class XmlReturnWriter : MimeReturnWriter
	{
		// Token: 0x060003DE RID: 990 RVA: 0x00012278 File Offset: 0x00010478
		public override void Initialize(object o)
		{
			this.xmlSerializer = (XmlSerializer)o;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0001218C File Offset: 0x0001038C
		public override object[] GetInitializers(LogicalMethodInfo[] methodInfos)
		{
			return XmlReturn.GetInitializers(methodInfos);
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00012194 File Offset: 0x00010394
		public override object GetInitializer(LogicalMethodInfo methodInfo)
		{
			return XmlReturn.GetInitializer(methodInfo);
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x00012288 File Offset: 0x00010488
		internal override void Write(HttpResponse response, Stream outputStream, object returnValue)
		{
			Encoding encoding = new UTF8Encoding(false);
			response.ContentType = ContentType.Compose("text/xml", encoding);
			StreamWriter streamWriter = new StreamWriter(outputStream, encoding);
			TraceMethod traceMethod = (Tracing.On ? new TraceMethod(this, "Write", Array.Empty<object>()) : null);
			if (Tracing.On)
			{
				Tracing.Enter(Tracing.TraceId("XmlSerializer [Write Response]"), traceMethod, new TraceMethod(this.xmlSerializer, "Serialize", new object[] { streamWriter, returnValue }));
			}
			this.xmlSerializer.Serialize(streamWriter, returnValue);
			if (Tracing.On)
			{
				Tracing.Exit(Tracing.TraceId("XmlSerializer [Write Response]"), traceMethod);
			}
		}

		// Token: 0x04000311 RID: 785
		private XmlSerializer xmlSerializer;
	}
}
