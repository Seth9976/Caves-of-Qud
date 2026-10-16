using System;
using System.Reflection;

namespace XRL.World
{
	// Token: 0x02000949 RID: 2377
	[Serializable]
	public abstract class IScribedEffect : Effect
	{
		// Token: 0x06005C30 RID: 23600 RVA: 0x001BD727 File Offset: 0x001BB927
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			Writer.WriteNamedFields(this, base.GetType(), BindingFlags.Instance | BindingFlags.Public, FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized);
		}

		// Token: 0x06005C31 RID: 23601 RVA: 0x001BD73D File Offset: 0x001BB93D
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			Reader.ReadNamedFields(this, base.GetType(), BindingFlags.Instance | BindingFlags.Public, FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized);
		}
	}
}
