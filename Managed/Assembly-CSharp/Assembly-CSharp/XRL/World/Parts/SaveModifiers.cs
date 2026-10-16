using System;
using System.Collections.Generic;
using System.Text;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x02001094 RID: 4244
	[Serializable]
	public class SaveModifiers : IActivePart
	{
		// Token: 0x17000B11 RID: 2833
		// (set) Token: 0x0600A8E5 RID: 43237 RVA: 0x003DC950 File Offset: 0x003DAB50
		public string Modifiers
		{
			set
			{
				string[] array = value.Split(';', StringSplitOptions.None);
				for (int i = 0; i < array.Length; i++)
				{
					string[] array2 = array[i].Split(':', StringSplitOptions.None);
					if (array2.Length == 2)
					{
						int num;
						if (array2[1] == "*")
						{
							this.Tracking[array2[0]] = SavingThrows.IMMUNITY;
						}
						else if (int.TryParse(array2[1], out num))
						{
							this.Tracking[array2[0]] = num;
						}
					}
				}
			}
		}

		// Token: 0x0600A8E6 RID: 43238 RVA: 0x003DC9C6 File Offset: 0x003DABC6
		public SaveModifiers()
		{
			this.WorksOnEquipper = true;
		}

		// Token: 0x0600A8E7 RID: 43239 RVA: 0x003DC9E0 File Offset: 0x003DABE0
		public override IPart DeepCopy(GameObject Parent)
		{
			SaveModifiers saveModifiers = base.DeepCopy(Parent) as SaveModifiers;
			saveModifiers.Tracking = new Dictionary<string, int>(this.Tracking);
			return saveModifiers;
		}

		// Token: 0x0600A8E8 RID: 43240 RVA: 0x003DCA00 File Offset: 0x003DAC00
		public void AddModifier(string Vs, int Level)
		{
			int num;
			if (!this.Tracking.TryGetValue(Vs, out num))
			{
				this.Tracking.Add(Vs, Level);
				return;
			}
			if (num + Level == 0)
			{
				this.Tracking.Remove(Vs);
				return;
			}
			this.Tracking[Vs] = num + Level;
		}

		// Token: 0x0600A8E9 RID: 43241 RVA: 0x003DCA4D File Offset: 0x003DAC4D
		public override bool SameAs(IPart p)
		{
			return (p as SaveModifiers).Tracking.SameAs<string, int>(this.Tracking) && base.SameAs(p);
		}

		// Token: 0x0600A8EA RID: 43242 RVA: 0x003DCA70 File Offset: 0x003DAC70
		public override bool WantTurnTick()
		{
			return true;
		}

		// Token: 0x0600A8EB RID: 43243 RVA: 0x003DCA74 File Offset: 0x003DAC74
		public override void TurnTick(long TimeTick, int Amount)
		{
			if (!base.OnWorldMap)
			{
				base.ConsumeChargeIfOperational(false, false, false, false, false, false, false, false, false, Amount, null, false, 0, false, null);
			}
		}

		// Token: 0x0600A8EC RID: 43244 RVA: 0x003DCAAF File Offset: 0x003DACAF
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == GetShortDescriptionEvent.ID || ID == ModifyDefendingSaveEvent.ID;
		}

		// Token: 0x0600A8ED RID: 43245 RVA: 0x003DCAD0 File Offset: 0x003DACD0
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			if (this.WorksOnEquipper || this.WorksOnWearer || this.WorksOnHolder || this.WorksOnCarrier)
			{
				List<string> list = new List<string>(this.Tracking.Keys);
				list.Sort();
				using (List<string>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						string Vs = enumerator.Current;
						E.Postfix.AppendRules(delegate(StringBuilder sb)
						{
							SavingThrows.AppendSaveBonusDescription(sb, this.Tracking[Vs], Vs, false, false, false);
						}, base.GetEventSensitiveAddStatusSummary(E));
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600A8EE RID: 43246 RVA: 0x003DCB84 File Offset: 0x003DAD84
		public override bool HandleEvent(ModifyDefendingSaveEvent E)
		{
			if (this.IsObjectActivePartSubject(E.Defender) && base.IsReady(false, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				foreach (KeyValuePair<string, int> keyValuePair in this.Tracking)
				{
					if (SavingThrows.Applicable(keyValuePair.Key, E))
					{
						E.Roll += keyValuePair.Value;
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x040045C8 RID: 17864
		public Dictionary<string, int> Tracking = new Dictionary<string, int>();
	}
}
