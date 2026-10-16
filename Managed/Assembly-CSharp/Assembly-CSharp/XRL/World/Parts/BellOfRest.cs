using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F97 RID: 3991
	[Serializable]
	public class BellOfRest : IPart
	{
		// Token: 0x06009E62 RID: 40546 RVA: 0x00395E5E File Offset: 0x0039405E
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == BeforeDestroyObjectEvent.ID;
		}

		// Token: 0x06009E63 RID: 40547 RVA: 0x00395E74 File Offset: 0x00394074
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("TookDamage");
			base.Register(Object, Registrar);
		}

		// Token: 0x06009E64 RID: 40548 RVA: 0x00395E8C File Offset: 0x0039408C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "TookDamage" && Calendar.TotalTimeTicks != this.lastDamageTurn)
			{
				this.lastDamageTurn = Calendar.TotalTimeTicks;
				this.ParentObject.ParticleSpray("&c\r", "&C\u000e", "&B\r", "&b\u000e", 6);
			}
			return true;
		}

		// Token: 0x06009E65 RID: 40549 RVA: 0x00395EE4 File Offset: 0x003940E4
		public override bool HandleEvent(BeforeDestroyObjectEvent E)
		{
			The.Game.SetIntGameState("BellOfRestDestroyed", 1);
			this.ParentObject.ParticleSpray("&c\r", "&C\u000e", "&B\r", "&b\u000e", 6);
			SoundManager.PlaySound("sfx_bellOfRest_toll", 0f, 1f, 1f, SoundRequest.SoundEffectType.None, 0f);
			return base.HandleEvent(E);
		}

		// Token: 0x04004189 RID: 16777
		public long lastDamageTurn = -1L;
	}
}
