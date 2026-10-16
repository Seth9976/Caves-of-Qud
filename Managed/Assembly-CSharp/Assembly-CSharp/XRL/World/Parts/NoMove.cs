using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;

namespace XRL.World.Parts
{
	// Token: 0x02000DCF RID: 3535
	[GenerateSerializationPartial]
	[Serializable]
	public class NoMove : IPart
	{
		// Token: 0x06008FF5 RID: 36853 RVA: 0x0033EB31 File Offset: 0x0033CD31
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
		}

		// Token: 0x06008FF6 RID: 36854 RVA: 0x0033EB33 File Offset: 0x0033CD33
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
		}

		// Token: 0x06008FF7 RID: 36855 RVA: 0x0033EB35 File Offset: 0x0033CD35
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CanBeInvoluntarilyMovedEvent>.ID;
		}

		// Token: 0x06008FF8 RID: 36856 RVA: 0x0033EB4B File Offset: 0x0033CD4B
		public override bool HandleEvent(CanBeInvoluntarilyMovedEvent E)
		{
			return false;
		}

		// Token: 0x06008FF9 RID: 36857 RVA: 0x0033EB4E File Offset: 0x0033CD4E
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("BeginMove");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008FFA RID: 36858 RVA: 0x0033EB63 File Offset: 0x0033CD63
		public override bool FireEvent(Event E)
		{
			return !(E.ID == "BeginMove") && base.FireEvent(E);
		}
	}
}
