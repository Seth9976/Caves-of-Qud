using System;
using System.Collections.Generic;
using System.Text;

namespace XRL.World.Parts
{
	// Token: 0x02000F4A RID: 3914
	[Serializable]
	public class CyberneticsStatModifier : IPart
	{
		// Token: 0x06009C44 RID: 40004 RVA: 0x0038AD8C File Offset: 0x00388F8C
		public override bool SameAs(IPart p)
		{
			CyberneticsStatModifier cyberneticsStatModifier = p as CyberneticsStatModifier;
			return !(cyberneticsStatModifier.Stats != this.Stats) && cyberneticsStatModifier.AddOn == this.AddOn && base.SameAs(p);
		}

		// Token: 0x06009C45 RID: 40005 RVA: 0x0038ADCC File Offset: 0x00388FCC
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetCyberneticsBehaviorDescriptionEvent>.ID || ID == PooledEvent<GetItemElementsEvent>.ID || ID == ImplantedEvent.ID || ID == UnimplantedEvent.ID;
		}

		// Token: 0x06009C46 RID: 40006 RVA: 0x0038ADFC File Offset: 0x00388FFC
		public override bool HandleEvent(GetItemElementsEvent E)
		{
			if (E.IsRelevantCreature(this.ParentObject.Implantee))
			{
				foreach (string text in this.ParseStats(this.Stats).Keys)
				{
					if (text == "Strength")
					{
						E.Add("might", 1);
					}
					else if (text == "Intelligence")
					{
						E.Add("scholarship", 1);
					}
					else if (text == "Ego")
					{
						E.Add("jewels", 1);
					}
					else if (text == "Willpower")
					{
						E.Add("salt", 1);
					}
					else if (text == "Speed")
					{
						E.Add("time", 1);
					}
					else if (text == "MoveSpeed")
					{
						E.Add("travel", 1);
					}
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009C47 RID: 40007 RVA: 0x0038AF1C File Offset: 0x0038911C
		public override bool HandleEvent(ImplantedEvent E)
		{
			foreach (KeyValuePair<string, int> keyValuePair in this.ParseStats(this.Stats))
			{
				E.Actor.GetStat(keyValuePair.Key).BaseValue += keyValuePair.Value;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009C48 RID: 40008 RVA: 0x0038AF9C File Offset: 0x0038919C
		public override bool HandleEvent(UnimplantedEvent E)
		{
			foreach (KeyValuePair<string, int> keyValuePair in this.ParseStats(this.Stats))
			{
				E.Actor.GetStat(keyValuePair.Key).BaseValue -= keyValuePair.Value;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009C49 RID: 40009 RVA: 0x0038B01C File Offset: 0x0038921C
		public override bool HandleEvent(GetCyberneticsBehaviorDescriptionEvent E)
		{
			if (this.AddOn)
			{
				foreach (string text in this.Stats.Split(';', StringSplitOptions.None))
				{
					string[] array2 = text.Split(':', StringSplitOptions.None);
					try
					{
						E.Add(Statistic.GetStatAdjustDescription(array2[0], Convert.ToInt32(array2[1]), false));
					}
					catch
					{
						E.Add("[error in " + text + "]");
					}
				}
			}
			else if (this.ParentObject.Implantee != null)
			{
				StringBuilder stringBuilder = Event.NewStringBuilder(null);
				bool flag = true;
				foreach (string text2 in this.Stats.Split(';', StringSplitOptions.None))
				{
					string[] array3 = text2.Split(':', StringSplitOptions.None);
					try
					{
						if (flag)
						{
							flag = false;
						}
						else
						{
							stringBuilder.Append('\n');
						}
						Statistic.AppendStatAdjustDescription(stringBuilder, array3[0], Convert.ToInt32(array3[1]), false, false);
					}
					catch
					{
						E.Add("[error in " + text2 + "]");
					}
				}
				E.Description = stringBuilder.ToString();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009C4A RID: 40010 RVA: 0x0038B154 File Offset: 0x00389354
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009C4B RID: 40011 RVA: 0x0038B158 File Offset: 0x00389358
		public Dictionary<string, int> ParseStats(string Stats)
		{
			Dictionary<string, int> dictionary = new Dictionary<string, int>();
			string[] array = Stats.Split(';', StringSplitOptions.None);
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(':', StringSplitOptions.None);
				dictionary.Add(array2[0], Convert.ToInt32(array2[1]));
			}
			return dictionary;
		}

		// Token: 0x040040AC RID: 16556
		public string Stats;

		// Token: 0x040040AD RID: 16557
		public bool AddOn;
	}
}
