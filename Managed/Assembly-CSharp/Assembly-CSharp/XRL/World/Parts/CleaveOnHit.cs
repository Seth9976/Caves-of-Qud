using System;
using System.Text;
using XRL.World.Parts.Skill;

namespace XRL.World.Parts
{
	// Token: 0x020010D2 RID: 4306
	[Serializable]
	public class CleaveOnHit : IPart
	{
		// Token: 0x0600AB31 RID: 43825 RVA: 0x003EA795 File Offset: 0x003E8995
		public CleaveOnHit()
		{
		}

		// Token: 0x0600AB32 RID: 43826 RVA: 0x003EA7A5 File Offset: 0x003E89A5
		public CleaveOnHit(int Chance)
			: this()
		{
			this.Chance = Chance;
		}

		// Token: 0x0600AB33 RID: 43827 RVA: 0x003EA7B4 File Offset: 0x003E89B4
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID;
		}

		// Token: 0x0600AB34 RID: 43828 RVA: 0x003EA7CA File Offset: 0x003E89CA
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			E.Postfix.AppendRules(new Action<StringBuilder>(this.AppendEffect));
			return base.HandleEvent(E);
		}

		// Token: 0x0600AB35 RID: 43829 RVA: 0x003EA7EC File Offset: 0x003E89EC
		public void AppendEffect(StringBuilder SB)
		{
			if (this.Chance <= 0)
			{
				return;
			}
			if (this.Chance < 100)
			{
				SB.Append(this.Chance).Append("% chance to cleave ");
			}
			else
			{
				SB.Append("Cleaves ");
			}
			MeleeWeapon meleeWeapon;
			if (this.ParentObject.TryGetPart<MeleeWeapon>(out meleeWeapon) && Statistic.IsMental(meleeWeapon.Stat))
			{
				SB.Append("mental ");
			}
			SB.Append("armor");
			if (this.CriticalOnly)
			{
				SB.Compound("on critical hit");
			}
		}

		// Token: 0x0600AB36 RID: 43830 RVA: 0x003EA87A File Offset: 0x003E8A7A
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponHit");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600AB37 RID: 43831 RVA: 0x003EA890 File Offset: 0x003E8A90
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponHit")
			{
				bool flag = E.HasFlag("Critical");
				if (this.CriticalOnly && !flag)
				{
					return base.FireEvent(E);
				}
				GameObject gameObjectParameter = E.GetGameObjectParameter("Attacker");
				GameObject gameObjectParameter2 = E.GetGameObjectParameter("Defender");
				GameObject parentObject = this.ParentObject;
				string text = null;
				int num = ((!this.CriticalOnly && flag) ? 100 : this.Chance);
				Axe_Cleave.PerformCleave(gameObjectParameter, gameObjectParameter2, parentObject, text, E.GetStringParameter("Properties", null), num, 0, null);
			}
			return base.FireEvent(E);
		}

		// Token: 0x04004695 RID: 18069
		public int Chance = 75;

		// Token: 0x04004696 RID: 18070
		public bool CriticalOnly;
	}
}
