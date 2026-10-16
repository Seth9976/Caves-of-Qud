using System;
using System.Collections.Generic;
using XRL.Language;
using XRL.Rules;

namespace XRL.World.Parts
{
	// Token: 0x02001119 RID: 4377
	[Serializable]
	public class MissilePerformance : IActivePart
	{
		// Token: 0x0600AD49 RID: 44361 RVA: 0x003F9BB8 File Offset: 0x003F7DB8
		public MissilePerformance()
		{
			this.WorksOnSelf = true;
		}

		// Token: 0x0600AD4A RID: 44362 RVA: 0x003F9BC8 File Offset: 0x003F7DC8
		public override bool SameAs(IPart p)
		{
			MissilePerformance missilePerformance = p as MissilePerformance;
			if (missilePerformance.PenetrationModifier != this.PenetrationModifier)
			{
				return false;
			}
			if (missilePerformance.PenetrationCapModifier != this.PenetrationCapModifier)
			{
				return false;
			}
			if (missilePerformance.DamageDieModifier != this.DamageDieModifier)
			{
				return false;
			}
			if (missilePerformance.DamageModifier != this.DamageModifier)
			{
				return false;
			}
			if (missilePerformance.AddAttributes != this.AddAttributes)
			{
				return false;
			}
			if (missilePerformance.RemoveAttributes != this.RemoveAttributes)
			{
				return false;
			}
			bool? penetrateCreatures = missilePerformance.PenetrateCreatures;
			bool? penetrateCreatures2 = this.PenetrateCreatures;
			return ((penetrateCreatures.GetValueOrDefault() == penetrateCreatures2.GetValueOrDefault()) & (penetrateCreatures != null == (penetrateCreatures2 != null))) && missilePerformance.ShowInShortDescription == this.ShowInShortDescription;
		}

		// Token: 0x0600AD4B RID: 44363 RVA: 0x003F9C8A File Offset: 0x003F7E8A
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == SingletonEvent<GetDebugInternalsEvent>.ID || ID == PooledEvent<GetMissileWeaponPerformanceEvent>.ID || (ID == GetShortDescriptionEvent.ID && this.ShowInShortDescription);
		}

		// Token: 0x0600AD4C RID: 44364 RVA: 0x003F9CB8 File Offset: 0x003F7EB8
		public override bool HandleEvent(GetDebugInternalsEvent E)
		{
			E.AddEntry(this, "PenetrationModifier", this.PenetrationModifier);
			E.AddEntry(this, "PenetrationCapModifier", this.PenetrationCapModifier);
			E.AddEntry(this, "DamageDieModifier", this.DamageDieModifier);
			E.AddEntry(this, "DamageModifier", this.DamageModifier);
			E.AddEntry(this, "AddAttributes", this.AddAttributes);
			E.AddEntry(this, "RemoveAttributes", this.RemoveAttributes);
			E.AddEntry(this, "PenetrateCreatures", this.PenetrateCreatures);
			E.AddEntry(this, "ShowInShortDescription", this.ShowInShortDescription);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AD4D RID: 44365 RVA: 0x003F9D5C File Offset: 0x003F7F5C
		public override bool HandleEvent(GetMissileWeaponPerformanceEvent E)
		{
			if (this.IsObjectActivePartSubject(E.Subject) && base.IsReady(E.Active, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				if (this.PenetrationModifier != 0)
				{
					E.PenetrationBonus += this.PenetrationModifier;
				}
				if (this.PenetrationCapModifier != 0)
				{
					E.PenetrationCap += this.PenetrationCapModifier;
				}
				if (this.DamageDieModifier != 0)
				{
					DieRoll damageRoll = E.GetDamageRoll();
					if (damageRoll != null)
					{
						damageRoll.AdjustDieSize(this.DamageDieModifier);
					}
				}
				if (this.DamageModifier != 0)
				{
					DieRoll damageRoll2 = E.GetDamageRoll();
					if (damageRoll2 != null)
					{
						damageRoll2.AdjustResult(this.DamageModifier);
					}
				}
				List<string> list = null;
				bool flag = false;
				if (!this.RemoveAttributes.IsNullOrEmpty() && !E.Attributes.IsNullOrEmpty())
				{
					List<string> list2 = this.RemoveAttributes.CachedCommaExpansion();
					if (list == null)
					{
						list = new List<string>(E.Attributes.Split(' ', StringSplitOptions.None));
					}
					int i = 0;
					int count = list2.Count;
					while (i < count)
					{
						if (list.Contains(list2[i]))
						{
							list.Remove(list2[i]);
							flag = true;
						}
						i++;
					}
				}
				if (!this.AddAttributes.IsNullOrEmpty())
				{
					List<string> list3 = this.AddAttributes.CachedCommaExpansion();
					if (list == null)
					{
						list = (E.Attributes.IsNullOrEmpty() ? new List<string>() : new List<string>(E.Attributes.Split(' ', StringSplitOptions.None)));
					}
					int j = 0;
					int count2 = list3.Count;
					while (j < count2)
					{
						if (!list.Contains(list3[j]))
						{
							list.Add(list3[j]);
							flag = true;
						}
						j++;
					}
				}
				if (flag)
				{
					E.Attributes = string.Join(" ", list.ToArray());
				}
				if (this.PenetrateCreatures != null)
				{
					E.PenetrateCreatures = this.PenetrateCreatures.Value;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AD4E RID: 44366 RVA: 0x003F9F58 File Offset: 0x003F8158
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.ShowInShortDescription)
			{
				bool flag = this.ChargeUse > 0 || this.ChargeMinimum > 0;
				if (this.PenetrationModifier != 0)
				{
					E.Postfix.AppendRules(string.Concat(new string[]
					{
						flag ? ("When powered, " + this.ParentObject.indicativeProximal) : this.ParentObject.IndicativeProximal,
						" ",
						this.ParentObject.IsPlural ? Grammar.Pluralize(this.ParentObject.GetDescriptiveCategory()) : this.ParentObject.GetDescriptiveCategory(),
						this.ParentObject.GetVerb("have", true, false, false, true, false),
						" ",
						this.PenetrationModifier.Signed(false),
						" to ",
						this.ParentObject.its,
						" penetration rolls."
					}));
				}
				if (this.PenetrationCapModifier != 0)
				{
					E.Postfix.AppendRules(string.Concat(new string[]
					{
						flag ? ("When powered, " + this.ParentObject.indicativeProximal) : this.ParentObject.IndicativeProximal,
						" ",
						this.ParentObject.IsPlural ? Grammar.Pluralize(this.ParentObject.GetDescriptiveCategory()) : this.ParentObject.GetDescriptiveCategory(),
						this.ParentObject.GetVerb("have", true, false, false, true, false),
						" ",
						this.PenetrationCapModifier.Signed(false),
						" to the maximum of ",
						this.ParentObject.its,
						" penetration rolls."
					}));
				}
				if (this.DamageDieModifier != 0)
				{
					E.Postfix.AppendRules(string.Concat(new string[]
					{
						flag ? ("When powered, " + this.ParentObject.indicativeProximal) : this.ParentObject.IndicativeProximal,
						" ",
						this.ParentObject.IsPlural ? Grammar.Pluralize(this.ParentObject.GetDescriptiveCategory()) : this.ParentObject.GetDescriptiveCategory(),
						this.ParentObject.GetVerb("have", true, false, false, true, false),
						" ",
						this.DamageDieModifier.Signed(false),
						" to the size of ",
						this.ParentObject.its,
						" damage dice."
					}));
				}
				if (this.DamageModifier != 0)
				{
					E.Postfix.AppendRules(string.Concat(new string[]
					{
						flag ? ("When powered, " + this.ParentObject.indicativeProximal) : this.ParentObject.IndicativeProximal,
						" ",
						this.ParentObject.IsPlural ? Grammar.Pluralize(this.ParentObject.GetDescriptiveCategory()) : this.ParentObject.GetDescriptiveCategory(),
						this.ParentObject.GetVerb("have", true, false, false, true, false),
						" ",
						this.DamageModifier.Signed(false),
						" to ",
						this.ParentObject.its,
						" damage rolls."
					}));
				}
				if (!this.RemoveAttributes.IsNullOrEmpty())
				{
					List<string> list = this.RemoveAttributes.CachedCommaExpansion();
					int i = 0;
					int count = list.Count;
					while (i < count)
					{
						if (list[i] == "Vorpal")
						{
							E.Postfix.AppendRules(string.Concat(new string[]
							{
								flag ? ("When powered, " + this.ParentObject.indicativeProximal) : this.ParentObject.IndicativeProximal,
								" ",
								this.ParentObject.IsPlural ? Grammar.Pluralize(this.ParentObject.GetDescriptiveCategory()) : this.ParentObject.GetDescriptiveCategory(),
								this.ParentObject.GetVerb("do", true, false, false, true, false),
								" not match ",
								this.ParentObject.its,
								" penetration to the armor of ",
								this.ParentObject.its,
								" targets."
							}));
						}
						else
						{
							E.Postfix.AppendRules(string.Concat(new string[]
							{
								flag ? ("When powered, " + this.ParentObject.indicativeProximal) : this.ParentObject.IndicativeProximal,
								" ",
								this.ParentObject.IsPlural ? Grammar.Pluralize(this.ParentObject.GetDescriptiveCategory()) : this.ParentObject.GetDescriptiveCategory(),
								this.ParentObject.GetVerb("remove", true, false, false, true, false),
								" the attribute ",
								list[i],
								" from its projectiles."
							}));
						}
						i++;
					}
				}
				if (!this.AddAttributes.IsNullOrEmpty())
				{
					List<string> list2 = this.AddAttributes.CachedCommaExpansion();
					int j = 0;
					int count2 = list2.Count;
					while (j < count2)
					{
						if (list2[j] == "Vorpal")
						{
							E.Postfix.AppendRules(string.Concat(new string[]
							{
								flag ? ("When powered, " + this.ParentObject.indicativeProximal) : this.ParentObject.IndicativeProximal,
								" ",
								this.ParentObject.IsPlural ? Grammar.Pluralize(this.ParentObject.GetDescriptiveCategory()) : this.ParentObject.GetDescriptiveCategory(),
								this.ParentObject.GetVerb("match", true, false, false, true, false),
								" ",
								this.ParentObject.its,
								" penetration to the armor of ",
								this.ParentObject.its,
								" targets."
							}));
						}
						else
						{
							E.Postfix.AppendRules(string.Concat(new string[]
							{
								flag ? ("When powered, " + this.ParentObject.indicativeProximal) : this.ParentObject.IndicativeProximal,
								" ",
								this.ParentObject.IsPlural ? Grammar.Pluralize(this.ParentObject.GetDescriptiveCategory()) : this.ParentObject.GetDescriptiveCategory(),
								this.ParentObject.GetVerb("add", true, false, false, true, false),
								" the attribute ",
								list2[j],
								" to its projectiles."
							}));
						}
						j++;
					}
				}
				if (this.PenetrateCreatures != null)
				{
					E.Postfix.AppendRules(string.Concat(new string[]
					{
						flag ? ("When powered, " + this.ParentObject.indicativeProximal) : this.ParentObject.IndicativeProximal,
						" ",
						this.ParentObject.IsPlural ? Grammar.Pluralize(this.ParentObject.GetDescriptiveCategory()) : this.ParentObject.GetDescriptiveCategory(),
						this.ParentObject.GetVerb("penetrate", true, false, false, true, false),
						" creatures."
					}));
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AD4F RID: 44367 RVA: 0x003FA6E0 File Offset: 0x003F88E0
		public bool WantAddAttribute(string attr)
		{
			if (this.AddAttributes.IsNullOrEmpty())
			{
				this.AddAttributes = attr;
				return true;
			}
			List<string> list = this.AddAttributes.CachedCommaExpansion();
			if (!list.Contains(attr))
			{
				list.Add(attr);
				this.AddAttributes = string.Join(",", list.ToArray());
				return true;
			}
			return false;
		}

		// Token: 0x0600AD50 RID: 44368 RVA: 0x003FA738 File Offset: 0x003F8938
		public bool WantRemoveAttribute(string attr)
		{
			if (this.RemoveAttributes.IsNullOrEmpty())
			{
				this.RemoveAttributes = attr;
				return true;
			}
			List<string> list = this.RemoveAttributes.CachedCommaExpansion();
			if (!list.Contains(attr))
			{
				list.Remove(attr);
				this.RemoveAttributes = string.Join(",", list.ToArray());
				return true;
			}
			return false;
		}

		// Token: 0x04004786 RID: 18310
		public int PenetrationModifier;

		// Token: 0x04004787 RID: 18311
		public int PenetrationCapModifier;

		// Token: 0x04004788 RID: 18312
		public int DamageDieModifier;

		// Token: 0x04004789 RID: 18313
		public int DamageModifier;

		// Token: 0x0400478A RID: 18314
		public string AddAttributes;

		// Token: 0x0400478B RID: 18315
		public string RemoveAttributes;

		// Token: 0x0400478C RID: 18316
		public bool? PenetrateCreatures;

		// Token: 0x0400478D RID: 18317
		public bool ShowInShortDescription;
	}
}
