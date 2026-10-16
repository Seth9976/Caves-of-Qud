using System;
using System.Security.Permissions;

namespace System.Web.Services.Description
{
	// Token: 0x0200011A RID: 282
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	internal class Soap12ProtocolImporter : SoapProtocolImporter
	{
		// Token: 0x1700022B RID: 555
		// (get) Token: 0x06000885 RID: 2181 RVA: 0x0003BE8B File Offset: 0x0003A08B
		public override string ProtocolName
		{
			get
			{
				return "Soap12";
			}
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x0003BE94 File Offset: 0x0003A094
		protected override bool IsBindingSupported()
		{
			Soap12Binding soap12Binding = (Soap12Binding)base.Binding.Extensions.Find(typeof(Soap12Binding));
			if (soap12Binding == null)
			{
				return false;
			}
			if (base.GetTransport(soap12Binding.Transport) == null)
			{
				base.UnsupportedBindingWarning(Res.GetString("There is no SoapTransportImporter that understands the transport '{0}'.", new object[] { soap12Binding.Transport }));
				return false;
			}
			return true;
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x0003BEF8 File Offset: 0x0003A0F8
		protected override bool IsSoapEncodingPresent(string uriList)
		{
			int num = 0;
			for (;;)
			{
				num = uriList.IndexOf("http://www.w3.org/2003/05/soap-encoding", num, StringComparison.Ordinal);
				if (num < 0)
				{
					goto IL_0052;
				}
				int num2 = num + "http://www.w3.org/2003/05/soap-encoding".Length;
				if ((num == 0 || uriList[num - 1] == ' ') && (num2 == uriList.Length || uriList[num2] == ' '))
				{
					break;
				}
				num = num2;
				if (num >= uriList.Length)
				{
					goto IL_0052;
				}
			}
			return true;
			IL_0052:
			if (base.IsSoapEncodingPresent(uriList))
			{
				base.UnsupportedOperationBindingWarning(Res.GetString("The SOAP 1.1 encoding style is not supported for operations that use SOAP 1.2. Use the URI '{0}' to refer to the SOAP 1.2 encoding style.", new object[] { "http://www.w3.org/2003/05/soap-encoding" }));
			}
			return false;
		}
	}
}
