using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;

namespace XRL.World.Parts
{
	// Token: 0x02000DC1 RID: 3521
	[GeneratePoolingPartial]
	[GenerateSerializationPartial]
	[Serializable]
	public class Inorganic : IPart
	{
		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06008D48 RID: 36168 RVA: 0x0032421B File Offset: 0x0032241B
		[GeneratedCode("PoolPartialsGenerator", "1.0.0.0")]
		public override IPartPool Pool
		{
			get
			{
				return Inorganic.InorganicPool;
			}
		}

		// Token: 0x06008D49 RID: 36169 RVA: 0x00324222 File Offset: 0x00322422
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
		}

		// Token: 0x06008D4A RID: 36170 RVA: 0x00324224 File Offset: 0x00322424
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
		}

		// Token: 0x06008D4B RID: 36171 RVA: 0x00324226 File Offset: 0x00322426
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == ApplyEffectEvent.ID || ID == BeforeApplyDamageEvent.ID || ID == CanApplyEffectEvent.ID;
		}

		// Token: 0x06008D4C RID: 36172 RVA: 0x0032424C File Offset: 0x0032244C
		public override bool HandleEvent(BeforeApplyDamageEvent E)
		{
			if (E.Object == this.ParentObject && (E.Damage.HasAttribute("Poison") || E.Damage.HasAttribute("Asphyxiation")))
			{
				NotifyTargetImmuneEvent.Send(E.Weapon, E.Object, E.Actor, E.Damage, this);
				E.Damage.Amount = 0;
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008D4D RID: 36173 RVA: 0x003242BE File Offset: 0x003224BE
		public override bool HandleEvent(CanApplyEffectEvent E)
		{
			return this.Check(E) && base.HandleEvent(E);
		}

		// Token: 0x06008D4E RID: 36174 RVA: 0x003242D2 File Offset: 0x003224D2
		public override bool HandleEvent(ApplyEffectEvent E)
		{
			return this.Check(E) && base.HandleEvent(E);
		}

		// Token: 0x06008D4F RID: 36175 RVA: 0x003242E8 File Offset: 0x003224E8
		private bool Check(IEffectCheckEvent E)
		{
			return (!(E.Name == "Sleep") || this.ParentObject.HasTag("Robot")) && !(E.Name == "Bleeding") && !(E.Name == "Disease") && !(E.Name == "DiseaseOnset") && !(E.Name == "PoisonGasPoison") && !(E.Name == "StunGasStun");
		}

		// Token: 0x04003B3A RID: 15162
		[GeneratedCode("PoolPartialsGenerator", "1.0.0.0")]
		private static readonly IPartPool InorganicPool = new IPartPool();
	}
}
