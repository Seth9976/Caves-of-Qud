using System;
using System.Reflection;

namespace XRL.World
{
	// Token: 0x02000966 RID: 2406
	[Serializable]
	public abstract class IScribedPart : IPart
	{
		// Token: 0x06006358 RID: 25432 RVA: 0x001E83FE File Offset: 0x001E65FE
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			Writer.WriteNamedFields(this, base.GetType(), BindingFlags.Instance | BindingFlags.Public, FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized);
		}

		// Token: 0x06006359 RID: 25433 RVA: 0x001E8414 File Offset: 0x001E6614
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			Reader.ReadNamedFields(this, base.GetType(), BindingFlags.Instance | BindingFlags.Public, FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized);
		}
	}
}
