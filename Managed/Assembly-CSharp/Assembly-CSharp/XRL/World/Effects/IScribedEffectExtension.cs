using System;
using System.Reflection;

namespace XRL.World.Effects
{
	// Token: 0x02000D4E RID: 3406
	public abstract class IScribedEffectExtension<T> : IEffectExtension<T> where T : Effect
	{
		// Token: 0x060086A2 RID: 34466 RVA: 0x00306705 File Offset: 0x00304905
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			Writer.WriteNamedFields(this, base.GetType(), BindingFlags.Instance | BindingFlags.Public, FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized);
		}

		// Token: 0x060086A3 RID: 34467 RVA: 0x0030671B File Offset: 0x0030491B
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			Reader.ReadNamedFields(this, base.GetType(), BindingFlags.Instance | BindingFlags.Public, FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized);
		}
	}
}
