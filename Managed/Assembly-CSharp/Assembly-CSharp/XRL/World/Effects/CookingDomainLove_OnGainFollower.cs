using System;

namespace XRL.World.Effects
{
	// Token: 0x02000CC4 RID: 3268
	public class CookingDomainLove_OnGainFollower : ProceduralCookingEffectWithTrigger
	{
		// Token: 0x06008253 RID: 33363 RVA: 0x002F791D File Offset: 0x002F5B1D
		public override string GetTriggerDescription()
		{
			return "whenever @thisCreature gain@s a new follower,";
		}

		// Token: 0x06008254 RID: 33364 RVA: 0x002F7924 File Offset: 0x002F5B24
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("GainedNewFollower");
			base.Register(Object, Registrar);
		}

		// Token: 0x06008255 RID: 33365 RVA: 0x002F793C File Offset: 0x002F5B3C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "GainedNewFollower")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Object");
				if (gameObjectParameter != null && !gameObjectParameter.IsTrifling)
				{
					base.Trigger();
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003939 RID: 14649
		public bool CurrentlySlowed;
	}
}
