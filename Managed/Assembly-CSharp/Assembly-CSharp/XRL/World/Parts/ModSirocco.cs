using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02001169 RID: 4457
	[Serializable]
	public class ModSirocco : IModification
	{
		// Token: 0x0600B14E RID: 45390 RVA: 0x0041169B File Offset: 0x0040F89B
		public ModSirocco()
		{
		}

		// Token: 0x0600B14F RID: 45391 RVA: 0x004116A3 File Offset: 0x0040F8A3
		public ModSirocco(int Tier)
			: base(Tier)
		{
		}

		// Token: 0x0600B150 RID: 45392 RVA: 0x004116AC File Offset: 0x0040F8AC
		public override void Configure()
		{
			this.WorksOnSelf = true;
			this.NameForStatus = "Sirocco";
		}

		// Token: 0x0600B151 RID: 45393 RVA: 0x004116C0 File Offset: 0x0040F8C0
		public override bool ModificationApplicable(GameObject Object)
		{
			return Object.HasPart<MeleeWeapon>();
		}

		// Token: 0x0600B152 RID: 45394 RVA: 0x004116C8 File Offset: 0x0040F8C8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetItemElementsEvent>.ID || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600B153 RID: 45395 RVA: 0x004116E6 File Offset: 0x0040F8E6
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantObject(this.ParentObject))
			{
				E.Add("salt", 10);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B154 RID: 45396 RVA: 0x0041170A File Offset: 0x0040F90A
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules("Sirocco: Drains 1 Toughness from any organic target this weapon damages for 3-4 turns.");
			return base.HandleEvent(E);
		}

		// Token: 0x0600B155 RID: 45397 RVA: 0x00411724 File Offset: 0x0040F924
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600B156 RID: 45398 RVA: 0x00411727 File Offset: 0x0040F927
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponDealDamage");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B157 RID: 45399 RVA: 0x0041173C File Offset: 0x0040F93C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponDealDamage")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Defender");
				if (gameObjectParameter.IsOrganic && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
				{
					GameObject gameObjectParameter2 = E.GetGameObjectParameter("Attacker");
					gameObjectParameter.ApplyEffect(new ToughnessDrained(Stat.Random(3, 4)), null);
					gameObjectParameter2.ApplyEffect(new ToughnessBoosted(Stat.Random(3, 4)), null);
					gameObjectParameter.Bloodsplatter();
				}
			}
			return base.FireEvent(E);
		}
	}
}
