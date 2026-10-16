using System;
using System.Reflection;
using XRL.World;

namespace XRL
{
	// Token: 0x020004E6 RID: 1254
	[Serializable]
	public abstract class IScribedSystem : IGameSystem
	{
		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x06003632 RID: 13874 RVA: 0x00104A37 File Offset: 0x00102C37
		public sealed override bool WantFieldReflection
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003633 RID: 13875 RVA: 0x00104A3A File Offset: 0x00102C3A
		public override void Write(SerializationWriter Writer)
		{
			Writer.WriteNamedFields(this, base.GetType(), BindingFlags.Instance | BindingFlags.Public, FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized);
		}

		// Token: 0x06003634 RID: 13876 RVA: 0x00104A50 File Offset: 0x00102C50
		public override void Read(SerializationReader Reader)
		{
			Reader.ReadNamedFields(this, base.GetType(), BindingFlags.Instance | BindingFlags.Public, FieldAttributes.Static | FieldAttributes.Literal | FieldAttributes.NotSerialized);
		}
	}
}
