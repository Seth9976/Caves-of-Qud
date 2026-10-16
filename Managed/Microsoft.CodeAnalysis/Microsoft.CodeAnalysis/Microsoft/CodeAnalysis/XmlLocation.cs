using System;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Linq;
using Microsoft.CodeAnalysis.Text;

namespace Microsoft.CodeAnalysis
{
	// Token: 0x02000184 RID: 388
	[NullableContext(1)]
	[Nullable(0)]
	internal class XmlLocation : Location, IEquatable<XmlLocation>
	{
		// Token: 0x0600100A RID: 4106 RVA: 0x0002EC28 File Offset: 0x0002CE28
		private XmlLocation(string path, int lineNumber, int columnNumber)
		{
			LinePosition linePosition = new LinePosition(lineNumber, columnNumber);
			LinePosition linePosition2 = new LinePosition(lineNumber, columnNumber + 1);
			this._positionSpan = new FileLinePositionSpan(path, linePosition, linePosition2);
		}

		// Token: 0x0600100B RID: 4107 RVA: 0x0002EC60 File Offset: 0x0002CE60
		public static XmlLocation Create(XmlException exception, string path)
		{
			int num = Math.Max(exception.LineNumber - 1, 0);
			int num2 = Math.Max(exception.LinePosition - 1, 0);
			return new XmlLocation(path, num, num2);
		}

		// Token: 0x0600100C RID: 4108 RVA: 0x0002EC94 File Offset: 0x0002CE94
		public static XmlLocation Create(XObject obj, string path)
		{
			int num = Math.Max(obj.LineNumber - 1, 0);
			int num2 = Math.Max(obj.LinePosition - 1, 0);
			return new XmlLocation(path, num, num2);
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x0600100D RID: 4109 RVA: 0x0002ECC7 File Offset: 0x0002CEC7
		public override LocationKind Kind
		{
			get
			{
				return LocationKind.XmlFile;
			}
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x0002ECCA File Offset: 0x0002CECA
		public override FileLinePositionSpan GetLineSpan()
		{
			return this._positionSpan;
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x0002ECD4 File Offset: 0x0002CED4
		[NullableContext(2)]
		public bool Equals(XmlLocation other)
		{
			return this == other || (other != null && other._positionSpan.Equals(this._positionSpan));
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x0002ED06 File Offset: 0x0002CF06
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return this.Equals(obj as XmlLocation);
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x0002ED14 File Offset: 0x0002CF14
		public override int GetHashCode()
		{
			return this._positionSpan.GetHashCode();
		}

		// Token: 0x04000472 RID: 1138
		private readonly FileLinePositionSpan _positionSpan;
	}
}
