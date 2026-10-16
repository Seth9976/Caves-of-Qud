using System;
using System.Collections.Generic;
using System.Text;

namespace XRL.World.Parts
{
	// Token: 0x02001025 RID: 4133
	[Serializable]
	public class EquipIntProperties : IPoweredPart
	{
		// Token: 0x0600A48E RID: 42126 RVA: 0x003C2B60 File Offset: 0x003C0D60
		public EquipIntProperties()
		{
			this.ChargeUse = 0;
			this.IsBootSensitive = false;
			this.IsEMPSensitive = false;
			this.WorksOnEquipper = true;
		}

		// Token: 0x0600A48F RID: 42127 RVA: 0x003C2B8F File Offset: 0x003C0D8F
		public EquipIntProperties(string Props)
			: this()
		{
			this.Props = Props;
		}

		// Token: 0x0600A490 RID: 42128 RVA: 0x003C2BA0 File Offset: 0x003C0DA0
		public static void AppendIntPropertiesOnEquip(GameObject obj, string prop)
		{
			if (obj.HasPart<EquipIntProperties>())
			{
				EquipIntProperties part = obj.GetPart<EquipIntProperties>();
				part.AddProps(prop);
				return;
			}
			obj.AddPart<EquipIntProperties>(new EquipIntProperties(prop)
			{
				DescribeStatusForProperty = null
			}, true, false);
		}

		// Token: 0x0600A491 RID: 42129 RVA: 0x003C2BE0 File Offset: 0x003C0DE0
		public void AddProps(string Spec)
		{
			Dictionary<string, int> bonusList = this.GetBonusList(false);
			Dictionary<string, int> dictionary = EquipIntProperties.DeterminePropsList(Spec);
			foreach (string text in dictionary.Keys)
			{
				if (bonusList.ContainsKey(text))
				{
					Dictionary<string, int> dictionary2 = bonusList;
					string text2 = text;
					dictionary2[text2] += dictionary[text];
				}
				else
				{
					bonusList.Add(text, dictionary[text]);
				}
			}
			this.Props = EquipIntProperties.PropsToString(bonusList);
		}

		// Token: 0x0600A492 RID: 42130 RVA: 0x003C2C80 File Offset: 0x003C0E80
		private static Dictionary<string, int> DeterminePropsList(string Spec)
		{
			if (string.IsNullOrEmpty(Spec))
			{
				return new Dictionary<string, int>(1);
			}
			string[] array = Spec.Split(';', StringSplitOptions.None);
			Dictionary<string, int> dictionary = new Dictionary<string, int>(array.Length);
			foreach (string text in array)
			{
				if (!string.IsNullOrEmpty(text))
				{
					string[] array3 = text.Split(':', StringSplitOptions.None);
					if (dictionary.ContainsKey(array3[0]))
					{
						Dictionary<string, int> dictionary2 = dictionary;
						string text2 = array3[0];
						dictionary2[text2] += Convert.ToInt32(array3[1]);
					}
					else
					{
						dictionary.Add(array3[0], Convert.ToInt32(array3[1]));
					}
				}
			}
			return dictionary;
		}

		// Token: 0x0600A493 RID: 42131 RVA: 0x003C2D1C File Offset: 0x003C0F1C
		private static string PropsToString(Dictionary<string, int> Bonuses)
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			int num = 0;
			foreach (KeyValuePair<string, int> keyValuePair in Bonuses)
			{
				if (num++ > 0)
				{
					stringBuilder.Append(';');
				}
				stringBuilder.Append(keyValuePair.Key).Append(':').Append(keyValuePair.Value);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600A494 RID: 42132 RVA: 0x003C2DA4 File Offset: 0x003C0FA4
		private Dictionary<string, int> GetBonusList(bool bForceRebuild = false)
		{
			if (this._BonusList == null || bForceRebuild)
			{
				this._BonusList = EquipIntProperties.DeterminePropsList(this.Props);
			}
			return this._BonusList;
		}

		// Token: 0x0600A495 RID: 42133 RVA: 0x003C2DCC File Offset: 0x003C0FCC
		public void Apply(GameObject Object)
		{
			if (this.bApplied)
			{
				return;
			}
			Dictionary<string, int> bonusList = this.GetBonusList(false);
			foreach (string text in bonusList.Keys)
			{
				if (!string.IsNullOrEmpty(text))
				{
					int num = bonusList[text];
					Object.ModIntProperty(text, num, false);
				}
			}
			this.bApplied = true;
			Object.SyncMutationLevelAndGlimmer();
		}

		// Token: 0x0600A496 RID: 42134 RVA: 0x003C2E50 File Offset: 0x003C1050
		public void UnapplyEffects(GameObject Object)
		{
			if (!this.bApplied)
			{
				return;
			}
			Dictionary<string, int> bonusList = this.GetBonusList(false);
			foreach (string text in bonusList.Keys)
			{
				if (!string.IsNullOrEmpty(text))
				{
					int num = bonusList[text];
					Object.ModIntProperty(text, -num, false);
				}
			}
			this.bApplied = false;
			Object.SyncMutationLevelAndGlimmer();
		}

		// Token: 0x0600A497 RID: 42135 RVA: 0x003C2ED8 File Offset: 0x003C10D8
		public override bool SameAs(IPart p)
		{
			return !((p as EquipIntProperties).Props != this.Props) && base.SameAs(p);
		}

		// Token: 0x0600A498 RID: 42136 RVA: 0x003C2EFB File Offset: 0x003C10FB
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A499 RID: 42137 RVA: 0x003C2F00 File Offset: 0x003C1100
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (this.bApplied)
			{
				base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, Amount, null, false, 0, false, null);
			}
			this.CheckApplyEffects(null);
		}

		// Token: 0x0600A49A RID: 42138 RVA: 0x003C2F42 File Offset: 0x003C1142
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == CellChangedEvent.ID || ID == EffectAppliedEvent.ID || ID == EquippedEvent.ID || ID == UnequippedEvent.ID;
		}

		// Token: 0x0600A49B RID: 42139 RVA: 0x003C2F70 File Offset: 0x003C1170
		public override bool HandleEvent(EffectAppliedEvent E)
		{
			this.CheckApplyEffects(null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A49C RID: 42140 RVA: 0x003C2F80 File Offset: 0x003C1180
		public override bool HandleEvent(EquippedEvent E)
		{
			this.CheckApplyEffects(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A49D RID: 42141 RVA: 0x003C2F95 File Offset: 0x003C1195
		public override bool HandleEvent(UnequippedEvent E)
		{
			this.UnapplyEffects(E.Actor);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A49E RID: 42142 RVA: 0x003C2FAA File Offset: 0x003C11AA
		public override bool HandleEvent(CellChangedEvent E)
		{
			this.CheckApplyEffects(null);
			return base.HandleEvent(E);
		}

		// Token: 0x0600A49F RID: 42143 RVA: 0x003C2FBA File Offset: 0x003C11BA
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x0600A4A0 RID: 42144 RVA: 0x003C2FC0 File Offset: 0x003C11C0
		public void CheckApplyEffects(GameObject obj = null)
		{
			if (obj == null)
			{
				obj = this.ParentObject.Physics.Equipped;
				if (obj == null)
				{
					return;
				}
			}
			if (base.IsDisabled(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				this.UnapplyEffects(obj);
				return;
			}
			this.Apply(obj);
		}

		// Token: 0x0400442F RID: 17455
		public string Props = "";

		// Token: 0x04004430 RID: 17456
		[NonSerialized]
		public Dictionary<string, int> _BonusList;

		// Token: 0x04004431 RID: 17457
		public bool bApplied;
	}
}
