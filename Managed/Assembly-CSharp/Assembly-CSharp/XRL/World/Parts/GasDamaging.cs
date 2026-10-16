using System;
using System.Collections.Generic;
using System.Text;
using XRL.World.Anatomy;

namespace XRL.World.Parts
{
	// Token: 0x02000FC2 RID: 4034
	[Serializable]
	public class GasDamaging : IGasBehavior
	{
		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x0600A068 RID: 41064 RVA: 0x003A5E55 File Offset: 0x003A4055
		// (set) Token: 0x0600A067 RID: 41063 RVA: 0x003A5E3B File Offset: 0x003A403B
		public string TargetBodyPartCategory
		{
			get
			{
				if (this.TargetBodyPartCategoryCode == 0)
				{
					return null;
				}
				return BodyPartCategory.GetName(this.TargetBodyPartCategoryCode);
			}
			set
			{
				if (value == null)
				{
					this.TargetBodyPartCategoryCode = 0;
					return;
				}
				this.TargetBodyPartCategoryCode = BodyPartCategory.GetCode(value, true);
			}
		}

		// Token: 0x0600A069 RID: 41065 RVA: 0x003A5E6C File Offset: 0x003A406C
		public override bool SameAs(IPart p)
		{
			GasDamaging gasDamaging = p as GasDamaging;
			return !(gasDamaging.GasType != this.GasType) && !(gasDamaging.Noun != this.Noun) && !(gasDamaging.MessageColor != this.MessageColor) && !(gasDamaging.DamageAttributes != this.DamageAttributes) && !(gasDamaging.TargetPart != this.TargetPart) && !(gasDamaging.TargetTag != this.TargetTag) && !(gasDamaging.ExcludeTag != this.ExcludeTag) && !(gasDamaging.TargetTagValue != this.TargetTagValue) && !(gasDamaging.TargetEquippedPart != this.TargetEquippedPart) && !(gasDamaging.TargetEquippedTag != this.TargetEquippedTag) && !(gasDamaging.TargetEquippedTagValue != this.TargetEquippedTagValue) && gasDamaging.TargetBodyPartCategoryCode == this.TargetBodyPartCategoryCode && gasDamaging.AffectEquipment == this.AffectEquipment && gasDamaging.AffectCybernetics == this.AffectCybernetics && gasDamaging.Respiratory == this.Respiratory;
		}

		// Token: 0x0600A06A RID: 41066 RVA: 0x003A5FA8 File Offset: 0x003A41A8
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetAdjacentNavigationWeightEvent.ID || ID == GetNavigationWeightEvent.ID || ID == ObjectEnteredCellEvent.ID;
		}

		// Token: 0x0600A06B RID: 41067 RVA: 0x003A5FD0 File Offset: 0x003A41D0
		public override bool HandleEvent(GetNavigationWeightEvent E)
		{
			if (!E.IgnoreGases && (!this.Respiratory || !E.Unbreathing) && E.PhaseMatches(this.ParentObject))
			{
				if (this.TargetPart == "Extradimensional")
				{
					E.MinWeight(2);
				}
				else if (E.Smart)
				{
					E.Uncacheable = true;
					if (CheckGasCanAffectEvent.Check(E.Actor, this.ParentObject, null) && this.Match(E.Actor))
					{
						Gas part = this.ParentObject.GetPart<Gas>();
						int num = part.Level * 10;
						int num2 = (this.Respiratory ? GetRespiratoryAgentPerformanceEvent.GetFor(E.Actor, this.ParentObject, part, null, 0, 0, false) : part.Density);
						int num3 = base.StepValue(num2, 5) + num;
						if (E.Actor != null && !this.DamageAttributes.IsNullOrEmpty() && this.DamageAttributes != "Gas")
						{
							Damage damage = new Damage(0);
							damage.AddAttributes(this.DamageAttributes);
							if (damage.IsHeatDamage())
							{
								int num4 = E.Actor.Stat("HeatResistance", 0);
								if (num4 != 0)
								{
									num3 = num3 * (100 - num4) / 100;
								}
							}
							if (damage.IsColdDamage())
							{
								int num5 = E.Actor.Stat("ColdResistance", 0);
								if (num5 != 0)
								{
									num3 = num3 * (100 - num5) / 100;
								}
							}
							if (damage.IsElectricDamage())
							{
								int num6 = E.Actor.Stat("ElectricResistance", 0);
								if (num6 != 0)
								{
									num3 = num3 * (100 - num6) / 100;
								}
							}
							if (damage.IsAcidDamage())
							{
								int num7 = E.Actor.Stat("AcidResistance", 0);
								if (num7 != 0)
								{
									num3 = num3 * (100 - num7) / 100;
								}
							}
						}
						if (num3 > 0)
						{
							E.MinWeight(num3, Math.Min(10 + num, 85));
						}
					}
				}
				else
				{
					E.MinWeight(5);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A06C RID: 41068 RVA: 0x003A61C4 File Offset: 0x003A43C4
		public override bool HandleEvent(GetAdjacentNavigationWeightEvent E)
		{
			if (!E.IgnoreGases && (!this.Respiratory || !E.Unbreathing) && E.PhaseMatches(this.ParentObject))
			{
				if (this.TargetPart == "Extradimensional")
				{
					E.MinWeight(1);
				}
				else if (E.Smart)
				{
					E.Uncacheable = true;
					if (CheckGasCanAffectEvent.Check(E.Actor, this.ParentObject, null) && this.Match(E.Actor))
					{
						Gas part = this.ParentObject.GetPart<Gas>();
						int num = part.Level * 10;
						int num2 = (this.Respiratory ? GetRespiratoryAgentPerformanceEvent.GetFor(E.Actor, this.ParentObject, part, null, 0, 0, false) : part.Density);
						int num3 = base.StepValue(num2, 5) + num;
						if (E.Actor != null && !this.DamageAttributes.IsNullOrEmpty() && this.DamageAttributes != "Gas")
						{
							Damage damage = new Damage(0);
							damage.AddAttributes(this.DamageAttributes);
							if (damage.IsHeatDamage())
							{
								int num4 = E.Actor.Stat("HeatResistance", 0);
								if (num4 != 0)
								{
									num3 = num3 * (100 - num4) / 100;
								}
							}
							if (damage.IsColdDamage())
							{
								int num5 = E.Actor.Stat("ColdResistance", 0);
								if (num5 != 0)
								{
									num3 = num3 * (100 - num5) / 100;
								}
							}
							if (damage.IsElectricDamage())
							{
								int num6 = E.Actor.Stat("ElectricResistance", 0);
								if (num6 != 0)
								{
									num3 = num3 * (100 - num6) / 100;
								}
							}
							if (damage.IsAcidDamage())
							{
								int num7 = E.Actor.Stat("AcidResistance", 0);
								if (num7 != 0)
								{
									num3 = num3 * (100 - num7) / 100;
								}
							}
						}
						if (num3 > 0)
						{
							num3 /= 5;
							if (num3 > 0)
							{
								E.MinWeight(num3, Math.Min(2 + num / 5, 17));
							}
						}
					}
				}
				else
				{
					E.MinWeight(1);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A06D RID: 41069 RVA: 0x003A63C1 File Offset: 0x003A45C1
		public override bool HandleEvent(ObjectEnteredCellEvent E)
		{
			if (E.Type != "Thrown")
			{
				this.ApplyGasToOthers(E.Object);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A06E RID: 41070 RVA: 0x003A63E8 File Offset: 0x003A45E8
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A06F RID: 41071 RVA: 0x003A63EB File Offset: 0x003A45EB
		public override void TurnTick(long TimeTick, int Amount)
		{
			this.ProcessDamagingGasBehavior();
		}

		// Token: 0x0600A070 RID: 41072 RVA: 0x003A63F4 File Offset: 0x003A45F4
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("DensityChange");
			if (this.DamageAttributes.Contains("AffectGas") || Registrar.IsUnregister)
			{
				Registrar.Register("GasPressureIn");
				Registrar.Register("GasPressureOut");
			}
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A071 RID: 41073 RVA: 0x003A6444 File Offset: 0x003A4644
		public override bool FireEvent(Event E)
		{
			if (E.ID == "GasPressureIn" || E.ID == "GasPressureOut")
			{
				GameObject gameObjectParameter = E.GetGameObjectParameter("Object");
				if (this.Match(gameObjectParameter))
				{
					this.ApplyDamagingGas(gameObjectParameter);
				}
			}
			else if (E.ID == "DensityChange" && base.StepValue(E.GetIntParameter("OldValue", 0), 5) != base.StepValue(E.GetIntParameter("NewValue", 0), 5))
			{
				base.FlushNavigationCaches();
			}
			return base.FireEvent(E);
		}

		// Token: 0x0600A072 RID: 41074 RVA: 0x003A64DC File Offset: 0x003A46DC
		private void ProcessDamagingGasBehavior()
		{
			Cell currentCell = this.ParentObject.CurrentCell;
			if (currentCell != null && currentCell.Objects.Count > 1)
			{
				List<GameObject> list = Event.NewGameObjectList();
				list.AddRange(currentCell.Objects);
				int i = 0;
				int count = list.Count;
				while (i < count)
				{
					this.ApplyGasToOthers(list[i]);
					i++;
				}
			}
		}

		// Token: 0x0600A073 RID: 41075 RVA: 0x003A6538 File Offset: 0x003A4738
		public static bool CanApplyTo(GameObject Target, string TargetPart = null, string TargetTag = null, string TargetTagValue = null, string TargetEquippedPart = null, string TargetEquippedTag = null, string TargetEquippedTagValue = null, string ExcludeTag = null, int TargetBodyPartCategoryCode = 0, bool Respiratory = false)
		{
			if (Target == null)
			{
				return false;
			}
			if (Respiratory && !Target.Respires)
			{
				return false;
			}
			if (!ExcludeTag.IsNullOrEmpty() && Target.HasTag(ExcludeTag))
			{
				return false;
			}
			if (!TargetPart.IsNullOrEmpty() && Target.HasPart(TargetPart))
			{
				return true;
			}
			Body body = null;
			if (TargetBodyPartCategoryCode != 0)
			{
				if (body == null)
				{
					body = Target.Body;
				}
				if (body != null && body.AnyCategoryParts(TargetBodyPartCategoryCode))
				{
					return true;
				}
			}
			if (!TargetTag.IsNullOrEmpty() && Target.HasTagOrProperty(TargetTag) && (TargetTagValue.IsNullOrEmpty() || Target.GetTagOrStringProperty(TargetTag, null) == TargetTagValue))
			{
				return true;
			}
			if (!TargetEquippedPart.IsNullOrEmpty() || !TargetEquippedTag.IsNullOrEmpty())
			{
				if (body == null)
				{
					body = Target.Body;
				}
				if (body != null)
				{
					foreach (BodyPart bodyPart in body.GetParts())
					{
						if (bodyPart.Equipped != null)
						{
							if (!TargetEquippedPart.IsNullOrEmpty() && bodyPart.Equipped.HasPart(TargetEquippedPart))
							{
								return true;
							}
							if (!TargetEquippedTag.IsNullOrEmpty() && bodyPart.Equipped.HasTag(TargetEquippedTag) && (TargetEquippedTagValue.IsNullOrEmpty() || bodyPart.Equipped.GetTag(TargetEquippedTag, null) == TargetEquippedTagValue))
							{
								return true;
							}
						}
					}
					return false;
				}
			}
			return false;
		}

		// Token: 0x0600A074 RID: 41076 RVA: 0x003A6694 File Offset: 0x003A4894
		public static bool BlueprintCanApplyTo(GameObject Target, GameObjectBlueprint Blueprint)
		{
			return Blueprint != null && Blueprint.HasPart("GasDamaging") && GasDamaging.CanApplyTo(Target, Blueprint.GetPartParameter<string>("GasDamaging", "TargetPart", null), Blueprint.GetPartParameter<string>("GasDamaging", "TargetTag", null), Blueprint.GetPartParameter<string>("GasDamaging", "TargetTagValue", null), Blueprint.GetPartParameter<string>("GasDamaging", "TargetEquippedPart", null), Blueprint.GetPartParameter<string>("GasDamaging", "TargetEquippedTag", null), Blueprint.GetPartParameter<string>("GasDamaging", "TargetEquippedTagValue", null), Blueprint.GetPartParameter<string>("GasDamaging", "ExcludeTag", null), BodyPartCategory.GetCodeIfExists(Blueprint.GetPartParameter<string>("GasDamaging", "TargetBodyPartCategoryCode", null)), Blueprint.GetPartParameter<bool>("GasDamaging", "Respiratory", false));
		}

		// Token: 0x0600A075 RID: 41077 RVA: 0x003A6759 File Offset: 0x003A4959
		public static bool BlueprintCanApplyTo(GameObject Target, string Blueprint)
		{
			return GasDamaging.BlueprintCanApplyTo(Target, GameObjectFactory.Factory.GetBlueprintIfExists(Blueprint));
		}

		// Token: 0x0600A076 RID: 41078 RVA: 0x003A676C File Offset: 0x003A496C
		private bool MatchInner(GameObject Target)
		{
			return GasDamaging.CanApplyTo(Target, this.TargetPart, this.TargetTag, this.TargetTagValue, this.TargetEquippedPart, this.TargetEquippedTag, this.TargetEquippedTagValue, this.ExcludeTag, this.TargetBodyPartCategoryCode, this.Respiratory);
		}

		// Token: 0x0600A077 RID: 41079 RVA: 0x003A67B5 File Offset: 0x003A49B5
		public bool Match(GameObject Target)
		{
			return this.MatchInner(Target) && this.ParentObject.PhaseMatches(Target);
		}

		// Token: 0x0600A078 RID: 41080 RVA: 0x003A67D0 File Offset: 0x003A49D0
		public bool ApplyDamagingGasWithResult(GameObject GO)
		{
			if (GO.IsInvalid())
			{
				return false;
			}
			Gas part = this.ParentObject.GetPart<Gas>();
			if (!CheckGasCanAffectEvent.Check(GO, this.ParentObject, part))
			{
				return false;
			}
			if (!this.Match(GO))
			{
				return false;
			}
			int num = (this.Respiratory ? GetRespiratoryAgentPerformanceEvent.GetFor(GO, this.ParentObject, part, null, 0, 0, false) : part.Density);
			if (num <= 0)
			{
				return false;
			}
			int num2;
			if (GO.IsCombat && !GO.HasPropertyOrTag("GasDamageAsIfInanimate"))
			{
				num2 = (int)Math.Ceiling(num * part.Level / this.CreatureDamageDivisor);
			}
			else
			{
				num2 = (int)Math.Ceiling((double)((0.75f * (float)part.Level + 0.25f) * (float)num));
			}
			if (num2 == 0)
			{
				num2 = 1;
			}
			GameObject creator = part.Creator;
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append("from ");
			if (this.ParentObject.GetDisplayName(2147483647, null, null, false, false, false, false, true, false, true, false, false, false, true, false, false, null, false, false, false, false, null, false, null, false, true) != this.Noun)
			{
				stringBuilder.Append("%t ");
			}
			else
			{
				stringBuilder.Append("the ");
			}
			if (!this.MessageColor.IsNullOrEmpty())
			{
				stringBuilder.Append("{{").Append(this.MessageColor).Append('|');
			}
			stringBuilder.Append(this.Noun);
			if (!this.MessageColor.IsNullOrEmpty())
			{
				stringBuilder.Append("}}");
			}
			stringBuilder.Append('!');
			int num3 = num2;
			string damageAttributes = this.DamageAttributes;
			return GO.TakeDamage(num3, stringBuilder.ToString(), damageAttributes, null, null, creator, null, this.ParentObject, null, null, false, true, true, false, false, false, false, false, false, 5, null);
		}

		// Token: 0x0600A079 RID: 41081 RVA: 0x003A69A8 File Offset: 0x003A4BA8
		public void ApplyDamagingGas(GameObject GO)
		{
			this.ApplyDamagingGasWithResult(GO);
		}

		// Token: 0x0600A07A RID: 41082 RVA: 0x003A69B4 File Offset: 0x003A4BB4
		private void ApplyGasToOthers(GameObject GO)
		{
			if (GO != this.ParentObject)
			{
				this.ApplyDamagingGas(GO);
				if (this.AffectEquipment || this.AffectCybernetics)
				{
					List<GameObject> list = Event.NewGameObjectList();
					if (this.AffectEquipment)
					{
						GO.GetEquippedObjects(list);
					}
					if (this.AffectCybernetics)
					{
						GO.GetInstalledCybernetics(list);
					}
					int i = 0;
					int count = list.Count;
					while (i < count)
					{
						this.ApplyDamagingGas(list[i]);
						i++;
					}
				}
			}
		}

		// Token: 0x040042A3 RID: 17059
		public string GasType = "Damaging";

		// Token: 0x040042A4 RID: 17060
		public string Noun = "damaging gas";

		// Token: 0x040042A5 RID: 17061
		public string MessageColor = "&R";

		// Token: 0x040042A6 RID: 17062
		public string DamageAttributes = "Gas";

		// Token: 0x040042A7 RID: 17063
		public string TargetPart;

		// Token: 0x040042A8 RID: 17064
		public string TargetTag;

		// Token: 0x040042A9 RID: 17065
		public string TargetTagValue;

		// Token: 0x040042AA RID: 17066
		public string TargetEquippedPart;

		// Token: 0x040042AB RID: 17067
		public string TargetEquippedTag;

		// Token: 0x040042AC RID: 17068
		public string TargetEquippedTagValue;

		// Token: 0x040042AD RID: 17069
		public string ExcludeTag;

		// Token: 0x040042AE RID: 17070
		public int TargetBodyPartCategoryCode;

		// Token: 0x040042AF RID: 17071
		public bool AffectEquipment;

		// Token: 0x040042B0 RID: 17072
		public bool AffectCybernetics;

		// Token: 0x040042B1 RID: 17073
		public bool Respiratory;

		// Token: 0x040042B2 RID: 17074
		public int CreatureDamageDivisor = 200;
	}
}
