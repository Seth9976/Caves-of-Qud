using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;

namespace XRL.World.Parts
{
	// Token: 0x02000DC6 RID: 3526
	[GenerateSerializationPartial]
	[GeneratePoolingPartial]
	[Serializable]
	public class NoDamage : IPart
	{
		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x06008E7E RID: 36478 RVA: 0x0033118A File Offset: 0x0032F38A
		[GeneratedCode("PoolPartialsGenerator", "1.0.0.0")]
		public override IPartPool Pool
		{
			get
			{
				return NoDamage.NoDamagePool;
			}
		}

		// Token: 0x06008E7F RID: 36479 RVA: 0x00331191 File Offset: 0x0032F391
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
		}

		// Token: 0x06008E80 RID: 36480 RVA: 0x00331193 File Offset: 0x0032F393
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
		}

		// Token: 0x06008E81 RID: 36481 RVA: 0x00331195 File Offset: 0x0032F395
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeApplyDamageEvent.ID || ID == PooledEvent<CanBeDismemberedEvent>.ID || ID == PooledEvent<GetElectricalConductivityEvent>.ID;
		}

		// Token: 0x06008E82 RID: 36482 RVA: 0x003311BB File Offset: 0x0032F3BB
		public override bool HandleEvent(BeforeApplyDamageEvent E)
		{
			return E.Object != this.ParentObject && base.HandleEvent(E);
		}

		// Token: 0x06008E83 RID: 36483 RVA: 0x003311D4 File Offset: 0x0032F3D4
		public override bool HandleEvent(CanBeDismemberedEvent E)
		{
			return E.Object != this.ParentObject && base.HandleEvent(E);
		}

		// Token: 0x06008E84 RID: 36484 RVA: 0x003311ED File Offset: 0x0032F3ED
		public override bool HandleEvent(GetElectricalConductivityEvent E)
		{
			if (E.Pass == 1 && E.Object == this.ParentObject)
			{
				E.Value = 0;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008E85 RID: 36485 RVA: 0x00331216 File Offset: 0x0032F416
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04003B75 RID: 15221
		[GeneratedCode("PoolPartialsGenerator", "1.0.0.0")]
		private static readonly IPartPool NoDamagePool = new IPartPool();
	}
}
