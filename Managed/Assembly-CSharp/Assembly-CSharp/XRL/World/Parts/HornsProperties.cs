using System;
using System.Text;
using XRL.World.Effects;
using XRL.World.Parts.Mutation;

namespace XRL.World.Parts
{
	// Token: 0x020011F1 RID: 4593
	[Serializable]
	public class HornsProperties : IPart
	{
		// Token: 0x0600B6EA RID: 46826 RVA: 0x0042ACD5 File Offset: 0x00428ED5
		public override bool SameAs(IPart p)
		{
			return (p as HornsProperties).HornLevel == this.HornLevel && base.SameAs(p);
		}

		// Token: 0x0600B6EB RID: 46827 RVA: 0x0042ACF3 File Offset: 0x00428EF3
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<GetDebugInternalsEvent>.ID || ID == PooledEvent<GetToHitModifierEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == PooledEvent<GetMeleeAttackChanceEvent>.ID;
		}

		// Token: 0x0600B6EC RID: 46828 RVA: 0x0042AD21 File Offset: 0x00428F21
		public override bool HandleEvent(GetToHitModifierEvent E)
		{
			if (E.Weapon == this.ParentObject && E.Checking == "Actor")
			{
				E.Modifier += this.GetToHitBonus();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B6ED RID: 46829 RVA: 0x0042AD60 File Offset: 0x00428F60
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			string text;
			int num;
			this.GetBleedingPerformance(out text, out num);
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append("+" + this.GetToHitBonus().ToString() + " to hit").AppendLine().Append("On penetration, this weapon causes bleeding: ")
				.Append(text)
				.Append(" damage per round; save difficulty ")
				.Append(num)
				.Append(".");
			E.Postfix.AppendRules(stringBuilder.ToString());
			return base.HandleEvent(E);
		}

		// Token: 0x0600B6EE RID: 46830 RVA: 0x0042ADEC File Offset: 0x00428FEC
		public override bool HandleEvent(GetMeleeAttackChanceEvent E)
		{
			if (E.Intrinsic && !E.Primary && E.Weapon == this.ParentObject)
			{
				E.SetFinalizedChance(E.Properties.HasDelimitedSubstring(',', "Charging", StringComparison.Ordinal) ? 100 : 20);
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B6EF RID: 46831 RVA: 0x0042AE41 File Offset: 0x00429041
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			E.AddEntry(this, "HornLevel", this.HornLevel);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B6F0 RID: 46832 RVA: 0x0042AE5C File Offset: 0x0042905C
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("WeaponDealDamage");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600B6F1 RID: 46833 RVA: 0x0042AE74 File Offset: 0x00429074
		public override bool FireEvent(Event E)
		{
			if (E.ID == "WeaponDealDamage" && E.GetIntParameter("Penetrations", 0) > 0)
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Defender");
				if (gameObjectParameter != null)
				{
					GameObject gameObjectParameter2 = E.GetGameObjectParameter("Attacker");
					string text;
					int num;
					this.GetBleedingPerformance(out text, out num);
					gameObjectParameter.ApplyEffect(new Bleeding(text, num, gameObjectParameter2, true, false, false, false), null);
				}
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600B6F2 RID: 46834 RVA: 0x0042AEE4 File Offset: 0x004290E4
		public void GetBleedingPerformance(out string Damage, out int SaveTarget)
		{
			int hornLevel = this.GetHornLevel();
			Damage = "1";
			if (hornLevel > 4)
			{
				Damage = "1d2";
				int num = (hornLevel - 4) / 4;
				if (num > 0)
				{
					Damage += num.Signed(false);
				}
			}
			SaveTarget = 20 + 2 * hornLevel;
		}

		// Token: 0x0600B6F3 RID: 46835 RVA: 0x0042AF2C File Offset: 0x0042912C
		public int GetToHitBonus()
		{
			return this.GetHornLevel() / 2 + 1;
		}

		// Token: 0x0600B6F4 RID: 46836 RVA: 0x0042AF38 File Offset: 0x00429138
		public static int GetToHitBonus(int Level)
		{
			return Level / 2 + 1;
		}

		// Token: 0x0600B6F5 RID: 46837 RVA: 0x0042AF40 File Offset: 0x00429140
		public int GetHornLevel()
		{
			int num = 1;
			if (this.HornLevel != 0)
			{
				num = this.HornLevel;
			}
			else
			{
				GameObject parentObject = this.ParentObject;
				Mutations mutations;
				if (parentObject == null)
				{
					mutations = null;
				}
				else
				{
					GameObject equipped = parentObject.Equipped;
					mutations = ((equipped != null) ? equipped.GetPart<Mutations>() : null);
				}
				Mutations mutations2 = mutations;
				if (mutations2 != null)
				{
					Horns horns = mutations2.GetMutation("Horns") as Horns;
					if (horns != null)
					{
						num = horns.Level;
					}
				}
			}
			return num;
		}

		// Token: 0x040049BB RID: 18875
		public int HornLevel;
	}
}
