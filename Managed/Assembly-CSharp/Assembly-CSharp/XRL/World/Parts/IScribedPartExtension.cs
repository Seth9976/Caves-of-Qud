using System;
using System.Reflection;

namespace XRL.World.Parts
{
	// Token: 0x02000FE4 RID: 4068
	public abstract class IScribedPartExtension<T> : IPartExtension<T> where T : IPart
	{
		// Token: 0x0600A1BF RID: 41407 RVA: 0x003AFD3D File Offset: 0x003ADF3D
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			Writer.WriteNamedFields(this, base.GetType(), BindingFlags.Instance | BindingFlags.Public, FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized);
		}

		// Token: 0x0600A1C0 RID: 41408 RVA: 0x003AFD53 File Offset: 0x003ADF53
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			Reader.ReadNamedFields(this, base.GetType(), BindingFlags.Instance | BindingFlags.Public, FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized);
		}
	}
}
