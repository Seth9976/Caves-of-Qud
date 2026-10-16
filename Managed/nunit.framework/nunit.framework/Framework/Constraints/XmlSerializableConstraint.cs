using System;
using System.IO;
using System.Xml.Serialization;

namespace NUnit.Framework.Constraints
{
	// Token: 0x0200014C RID: 332
	public class XmlSerializableConstraint : Constraint
	{
		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060009BD RID: 2493 RVA: 0x00017A38 File Offset: 0x00015C38
		public override string Description
		{
			get
			{
				return "xml serializable";
			}
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x00017A40 File Offset: 0x00015C40
		public override ConstraintResult ApplyTo(object actual)
		{
			if (actual == null)
			{
				throw new ArgumentNullException("actual");
			}
			MemoryStream memoryStream = new MemoryStream();
			bool flag = false;
			try
			{
				this.serializer = new XmlSerializer(actual.GetType());
				this.serializer.Serialize(memoryStream, actual);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				flag = this.serializer.Deserialize(memoryStream) != null;
			}
			catch (NotSupportedException)
			{
			}
			catch (InvalidOperationException)
			{
			}
			return new ConstraintResult(this, actual.GetType(), flag);
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00017ACC File Offset: 0x00015CCC
		protected override string GetStringRepresentation()
		{
			return "<xmlserializable>";
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00011D90 File Offset: 0x0000FF90
		public XmlSerializableConstraint()
			: base(new object[0])
		{
		}

		// Token: 0x0400020D RID: 525
		private XmlSerializer serializer;
	}
}
