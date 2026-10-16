using System;
using System.CodeDom.Compiler;
using Occult.Engine.CodeGeneration;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x02000DBE RID: 3518
	[GeneratePoolingPartial]
	[GenerateSerializationPartial]
	[Serializable]
	public class BurnToAshesIfOrganic : IPart
	{
		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06008D26 RID: 36134 RVA: 0x00323484 File Offset: 0x00321684
		[GeneratedCode("PoolPartialsGenerator", "1.0.0.0")]
		public override IPartPool Pool
		{
			get
			{
				return BurnToAshesIfOrganic.BurnToAshesIfOrganicPool;
			}
		}

		// Token: 0x06008D27 RID: 36135 RVA: 0x0032348B File Offset: 0x0032168B
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
		}

		// Token: 0x06008D28 RID: 36136 RVA: 0x0032348D File Offset: 0x0032168D
		[GeneratedCode("SerializationPartialsGenerator", "1.0.0.0")]
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
		}

		// Token: 0x06008D29 RID: 36137 RVA: 0x0032348F File Offset: 0x0032168F
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeDeathRemovalEvent.ID;
		}

		// Token: 0x06008D2A RID: 36138 RVA: 0x003234A8 File Offset: 0x003216A8
		public override bool HandleEvent(BeforeDeathRemovalEvent E)
		{
			if (this.ParentObject.Physics != null && (this.ParentObject.Physics.LastDamagedByType == "Fire" || this.ParentObject.Physics.LastDamagedByType == "Light") && this.ParentObject.IsOrganic && !this.ParentObject.HasPart<Metal>() && !this.ParentObject.HasPart<Crysteel>() && !this.ParentObject.HasPart<Zetachrome>() && this.ParentObject.GetIntProperty("SuppressCorpseDrops", 0) <= 0)
			{
				IInventory dropInventory = this.ParentObject.GetDropInventory();
				if (dropInventory != null)
				{
					GameObject gameObject = GameObject.Create("Ashes");
					this.DoCarryOvers(this.ParentObject, gameObject);
					dropInventory.AddObjectToInventory(gameObject, null, false, false, true, null, E);
					DroppedEvent.Send(this.ParentObject, gameObject, false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06008D2B RID: 36139 RVA: 0x00323591 File Offset: 0x00321791
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06008D2C RID: 36140 RVA: 0x00323594 File Offset: 0x00321794
		private void DoCarryOvers(GameObject From, GameObject To)
		{
			if (From.HasProperty("StoredByPlayer") || From.HasProperty("FromStoredByPlayer"))
			{
				To.SetIntProperty("FromStoredByPlayer", 1, false);
			}
			Temporary.CarryOver(From, To, false);
			Phase.carryOver(From, To);
		}

		// Token: 0x04003B32 RID: 15154
		[GeneratedCode("PoolPartialsGenerator", "1.0.0.0")]
		private static readonly IPartPool BurnToAshesIfOrganicPool = new IPartPool();
	}
}
