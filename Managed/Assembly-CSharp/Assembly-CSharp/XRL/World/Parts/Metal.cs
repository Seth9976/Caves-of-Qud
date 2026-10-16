using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;

namespace XRL.World.Parts
{
	// Token: 0x02000DC5 RID: 3525
	[GeneratePoolingPartial(Capacity = 64)]
	[Serializable]
	public class Metal : IPart
	{
		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x06008E73 RID: 36467 RVA: 0x00331064 File Offset: 0x0032F264
		[GeneratedCode("PoolPartialsGenerator", "1.0.0.0")]
		public override IPartPool Pool
		{
			get
			{
				return Metal.MetalPool;
			}
		}

		// Token: 0x06008E74 RID: 36468 RVA: 0x0033106B File Offset: 0x0032F26B
		public override void AddedAfterCreation()
		{
			base.AddedAfterCreation();
			this.ParentObject.MakeNonflammable();
		}

		// Token: 0x06008E75 RID: 36469 RVA: 0x0033107F File Offset: 0x0032F27F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeApplyDamageEvent.ID || ID == PooledEvent<CanBeMagneticallyManipulatedEvent>.ID || ID == PooledEvent<GetElectricalConductivityEvent>.ID || ID == PooledEvent<GetMaximumLiquidExposureEvent>.ID || ID == ObjectCreatedEvent.ID || ID == PooledEvent<TransparentToEMPEvent>.ID;
		}

		// Token: 0x06008E76 RID: 36470 RVA: 0x003310BD File Offset: 0x0032F2BD
		public override bool HandleEvent(CanBeMagneticallyManipulatedEvent E)
		{
			E.Allow = true;
			return base.HandleEvent(E);
		}

		// Token: 0x06008E77 RID: 36471 RVA: 0x003310CD File Offset: 0x0032F2CD
		public override bool HandleEvent(GetElectricalConductivityEvent E)
		{
			if (E.Pass == 2 && E.Object == this.ParentObject)
			{
				E.MinValue(90);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008E78 RID: 36472 RVA: 0x003310F5 File Offset: 0x0032F2F5
		public override bool HandleEvent(GetMaximumLiquidExposureEvent E)
		{
			E.PercentageReduction = 25;
			return base.HandleEvent(E);
		}

		// Token: 0x06008E79 RID: 36473 RVA: 0x00331108 File Offset: 0x0032F308
		public override bool HandleEvent(BeforeApplyDamageEvent E)
		{
			if (E.Object == this.ParentObject && !this.ParentObject.HasTag("Creature") && E.Damage.IsAcidDamage())
			{
				E.Damage.Amount /= 4;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008E7A RID: 36474 RVA: 0x0033115C File Offset: 0x0032F35C
		public override bool HandleEvent(TransparentToEMPEvent E)
		{
			return false;
		}

		// Token: 0x06008E7B RID: 36475 RVA: 0x0033115F File Offset: 0x0032F35F
		public override bool HandleEvent(ObjectCreatedEvent E)
		{
			this.ParentObject.MakeNonflammable();
			return base.HandleEvent(E);
		}

		// Token: 0x04003B74 RID: 15220
		[GeneratedCode("PoolPartialsGenerator", "1.0.0.0")]
		private static readonly IPartPool MetalPool = new IPartPool(64);
	}
}
