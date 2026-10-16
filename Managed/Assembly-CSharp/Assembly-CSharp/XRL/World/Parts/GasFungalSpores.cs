using System;
using XRL.Rules;
using XRL.World.Effects;

namespace XRL.World.Parts
{
	// Token: 0x02000FC4 RID: 4036
	[Serializable]
	public class GasFungalSpores : IObjectGasBehavior
	{
		// Token: 0x0600A089 RID: 41097 RVA: 0x003A6E1C File Offset: 0x003A501C
		public override bool SameAs(IPart p)
		{
			GasFungalSpores gasFungalSpores = p as GasFungalSpores;
			return !(gasFungalSpores.Infection != this.Infection) && !(gasFungalSpores.GasType != this.GasType) && gasFungalSpores.GasLevel == this.GasLevel && base.SameAs(p);
		}

		// Token: 0x0600A08A RID: 41098 RVA: 0x003A6E71 File Offset: 0x003A5071
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetAdjacentNavigationWeightEvent.ID || ID == GetNavigationWeightEvent.ID;
		}

		// Token: 0x0600A08B RID: 41099 RVA: 0x003A6E90 File Offset: 0x003A5090
		public override bool HandleEvent(GetNavigationWeightEvent E)
		{
			if (!E.IgnoreGases && E.PhaseMatches(this.ParentObject))
			{
				if (E.Smart)
				{
					E.Uncacheable = true;
					if (CheckGasCanAffectEvent.Check(E.Actor, this.ParentObject, null))
					{
						if (E.Actor != null)
						{
							GameObject actor = E.Actor;
							Gas part = this.ParentObject.GetPart<Gas>();
							if (actor == ((part != null) ? part.Creator : null) || !E.Actor.FireEvent("CanApplySpores") || E.Actor.HasEffect<FungalSporeInfection>() || !E.Actor.PhaseMatches(this.ParentObject))
							{
								goto IL_00B9;
							}
						}
						E.MinWeight(base.GasDensityStepped(5) / 2 + 20, 80);
					}
				}
				else
				{
					E.MinWeight(10);
				}
			}
			IL_00B9:
			return base.HandleEvent(E);
		}

		// Token: 0x0600A08C RID: 41100 RVA: 0x003A6F60 File Offset: 0x003A5160
		public override bool HandleEvent(GetAdjacentNavigationWeightEvent E)
		{
			if (!E.IgnoreGases && E.PhaseMatches(this.ParentObject))
			{
				if (E.Smart)
				{
					E.Uncacheable = true;
					if (CheckGasCanAffectEvent.Check(E.Actor, this.ParentObject, null))
					{
						if (E.Actor != null)
						{
							GameObject actor = E.Actor;
							Gas part = this.ParentObject.GetPart<Gas>();
							if (actor == ((part != null) ? part.Creator : null) || !E.Actor.FireEvent("CanApplySpores") || E.Actor.HasEffect<FungalSporeInfection>() || !E.Actor.PhaseMatches(this.ParentObject))
							{
								goto IL_00B8;
							}
						}
						E.MinWeight(base.GasDensityStepped(5) / 10 + 4, 16);
					}
				}
				else
				{
					E.MinWeight(2);
				}
			}
			IL_00B8:
			return base.HandleEvent(E);
		}

		// Token: 0x0600A08D RID: 41101 RVA: 0x003A702C File Offset: 0x003A522C
		public override bool ApplyGas(GameObject Object)
		{
			if (Object == this.ParentObject)
			{
				return false;
			}
			GameObject gameObject = null;
			Gas part = this.ParentObject.GetPart<Gas>();
			if (part != null)
			{
				gameObject = part.Creator;
			}
			if (Object == gameObject)
			{
				return false;
			}
			if (!CheckGasCanAffectEvent.Check(Object, this.ParentObject, part))
			{
				return false;
			}
			if (!Object.FireEvent("CanApplySpores"))
			{
				return false;
			}
			if (!Object.FireEvent("ApplySpores"))
			{
				return false;
			}
			if (Object.HasTagOrProperty("ImmuneToFungus"))
			{
				return false;
			}
			if (!Object.PhaseMatches(this.ParentObject))
			{
				return false;
			}
			Object.RemoveEffect<SporeCloudPoison>();
			if (!Object.HasStat("Toughness"))
			{
				return false;
			}
			if (Object.HasEffect<FungalSporeInfection>())
			{
				return false;
			}
			bool flag = false;
			SporeCloudPoison sporeCloudPoison = Object.GetEffect<SporeCloudPoison>();
			if (this.Infection == "PaxInfection")
			{
				this.GasLevel = 8;
			}
			else
			{
				int num = Stat.Random(2, 5);
				bool flag2 = sporeCloudPoison != null;
				if (sporeCloudPoison == null)
				{
					sporeCloudPoison = new SporeCloudPoison(num, (part != null) ? part.Creator : null);
				}
				else
				{
					if (sporeCloudPoison.Duration < num)
					{
						sporeCloudPoison.Duration = num;
					}
					if (!GameObject.Validate(ref sporeCloudPoison.Owner) && part != null && part.Creator != null)
					{
						sporeCloudPoison.Owner = part.Creator;
					}
				}
				sporeCloudPoison.Damage = Math.Min(1, this.GasLevel);
				if (flag2 || Object.ApplyEffect(sporeCloudPoison, null))
				{
					flag = true;
				}
			}
			int num2 = 10 + this.GasLevel / 3;
			if (!Object.MakeSave("Toughness", num2, null, null, "Fungal Disease Contact Gas", false, false, false, false, this.ParentObject))
			{
				if (Object.IsPlayer())
				{
					IComponent<GameObject>.AddPlayerMessage("Your skin itches.", null, true);
				}
				Event @event = new Event("BeforeApplyFungalInfection");
				bool flag3 = false;
				if (!Object.FireEvent(@event) || @event.HasParameter("Cancelled"))
				{
					flag3 = true;
				}
				FungalSporeInfection fungalSporeInfection;
				if (this.Infection == "PaxInfection")
				{
					fungalSporeInfection = new FungalSporeInfection(3, this.Infection);
				}
				else
				{
					fungalSporeInfection = new FungalSporeInfection(flag3 ? Stat.Random(8, 10) : (Stat.Random(20, 30) * 120), this.Infection);
					fungalSporeInfection.Fake = flag3;
				}
				if (Object.ApplyEffect(fungalSporeInfection, null))
				{
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x040042B4 RID: 17076
		public string Infection = "LuminousInfection";

		// Token: 0x040042B5 RID: 17077
		public string GasType = "FungalSpores";

		// Token: 0x040042B6 RID: 17078
		public int GasLevel = 1;
	}
}
