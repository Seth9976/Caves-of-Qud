using System;
using System.Globalization;
using Roslyn.Utilities;

namespace Microsoft.CodeAnalysis.CSharp
{
	// Token: 0x02000145 RID: 325
	internal sealed class XmlSyntaxDiagnosticInfo : SyntaxDiagnosticInfo
	{
		// Token: 0x06001993 RID: 6547 RVA: 0x000880E3 File Offset: 0x000862E3
		static XmlSyntaxDiagnosticInfo()
		{
			ObjectBinder.RegisterTypeReader(typeof(XmlSyntaxDiagnosticInfo), (ObjectReader r) => new XmlSyntaxDiagnosticInfo(r));
		}

		// Token: 0x06001994 RID: 6548 RVA: 0x00088104 File Offset: 0x00086304
		internal XmlSyntaxDiagnosticInfo(XmlParseErrorCode code, params object[] args)
			: this(0, 0, code, args)
		{
		}

		// Token: 0x06001995 RID: 6549 RVA: 0x00088110 File Offset: 0x00086310
		internal XmlSyntaxDiagnosticInfo(int offset, int width, XmlParseErrorCode code, params object[] args)
			: base(offset, width, ErrorCode.WRN_XMLParseError, args)
		{
			this._xmlErrorCode = code;
		}

		// Token: 0x06001996 RID: 6550 RVA: 0x00088128 File Offset: 0x00086328
		protected override void WriteTo(ObjectWriter writer)
		{
			base.WriteTo(writer);
			writer.WriteUInt32((uint)this._xmlErrorCode);
		}

		// Token: 0x06001997 RID: 6551 RVA: 0x0008813D File Offset: 0x0008633D
		private XmlSyntaxDiagnosticInfo(ObjectReader reader)
			: base(reader)
		{
			this._xmlErrorCode = (XmlParseErrorCode)reader.ReadUInt32();
		}

		// Token: 0x06001998 RID: 6552 RVA: 0x00088154 File Offset: 0x00086354
		public override string GetMessage(IFormatProvider formatProvider = null)
		{
			CultureInfo cultureInfo = formatProvider as CultureInfo;
			string text = base.MessageProvider.LoadMessage(base.Code, cultureInfo);
			string message = ErrorFacts.GetMessage(this._xmlErrorCode, cultureInfo);
			if (base.Arguments == null || base.Arguments.Length == 0)
			{
				return string.Format(formatProvider, text, message);
			}
			return string.Format(formatProvider, string.Format(formatProvider, text, message), base.GetArgumentsToUse(formatProvider));
		}

		// Token: 0x04000DE7 RID: 3559
		private readonly XmlParseErrorCode _xmlErrorCode;
	}
}
