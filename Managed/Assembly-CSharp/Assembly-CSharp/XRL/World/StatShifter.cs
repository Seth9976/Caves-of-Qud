using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XRL.Language;

namespace XRL.World
{
	// Token: 0x0200096D RID: 2413
	public class StatShifter
	{
		// Token: 0x060063C9 RID: 25545 RVA: 0x001EBF88 File Offset: 0x001EA188
		public static void Save(StatShifter Shifter, SerializationWriter Writer)
		{
			Dictionary<string, Dictionary<string, Guid>> dictionary = ((Shifter != null) ? Shifter.ActiveShifts : null);
			int num = ((dictionary != null) ? dictionary.Count : 0);
			Writer.WriteOptimized(num);
			if (num > 0)
			{
				Writer.WriteOptimized(Shifter.DefaultDisplayName);
				foreach (KeyValuePair<string, Dictionary<string, Guid>> keyValuePair in dictionary)
				{
					Writer.WriteOptimized(keyValuePair.Key);
					Writer.WriteOptimized(keyValuePair.Value.Count);
					foreach (KeyValuePair<string, Guid> keyValuePair2 in keyValuePair.Value)
					{
						Writer.WriteOptimized(keyValuePair2.Key);
						Writer.Write(keyValuePair2.Value);
					}
				}
			}
		}

		// Token: 0x060063CA RID: 25546 RVA: 0x001EC07C File Offset: 0x001EA27C
		public static StatShifter Load(SerializationReader Reader, GameObject Owner)
		{
			int num = Reader.ReadOptimizedInt32();
			if (num <= 0)
			{
				return null;
			}
			StatShifter statShifter = new StatShifter(Owner);
			statShifter.DefaultDisplayName = Reader.ReadOptimizedString();
			statShifter.ActiveShifts = new Dictionary<string, Dictionary<string, Guid>>(num);
			for (int i = 0; i < num; i++)
			{
				string text = Reader.ReadOptimizedString();
				int num2 = Reader.ReadOptimizedInt32();
				Dictionary<string, Guid> dictionary = new Dictionary<string, Guid>(num2);
				for (int j = 0; j < num2; j++)
				{
					dictionary[Reader.ReadOptimizedString()] = Reader.ReadGuid();
				}
				statShifter.ActiveShifts[text] = dictionary;
			}
			return statShifter;
		}

		// Token: 0x060063CB RID: 25547 RVA: 0x001EC10C File Offset: 0x001EA30C
		public StatShifter(GameObject Owner)
		{
			this.Owner = Owner;
		}

		// Token: 0x060063CC RID: 25548 RVA: 0x001EC11B File Offset: 0x001EA31B
		public StatShifter(GameObject Owner, string DefaultDisplayName)
		{
			this.Owner = Owner;
			this.DefaultDisplayName = DefaultDisplayName;
		}

		// Token: 0x060063CD RID: 25549 RVA: 0x001EC134 File Offset: 0x001EA334
		public StatShifter(GameObject Owner, StatShifter Source)
		{
			this.Owner = Owner;
			if (Source != null && Source.ActiveShifts != null)
			{
				this.ActiveShifts = new Dictionary<string, Dictionary<string, Guid>>();
				foreach (KeyValuePair<string, Dictionary<string, Guid>> keyValuePair in Source.ActiveShifts)
				{
					this.ActiveShifts.Add(keyValuePair.Key, keyValuePair.Value.ToDictionary<KeyValuePair<string, Guid>, string, Guid>((KeyValuePair<string, Guid> e) => e.Key, (KeyValuePair<string, Guid> e) => e.Value));
				}
			}
		}

		/// <summary>Shift a stat on the owner by an amount, further calls to SetStatShift with the same stat will
		/// undo the previous shift, and set to the new amount.</summary>
		// Token: 0x060063CE RID: 25550 RVA: 0x001EC208 File Offset: 0x001EA408
		public bool SetStatShift(string statName, int amount, bool baseValue = false)
		{
			return this.SetStatShift(this.Owner, statName, amount, baseValue);
		}

		/// <summary>Get the value of the stat shift (optional base value) applied by this object</summary>
		// Token: 0x060063CF RID: 25551 RVA: 0x001EC219 File Offset: 0x001EA419
		public int GetStatShift(string statNAme, bool baseValue = false)
		{
			return this.GetStatShift(this.Owner, statNAme, baseValue);
		}

		/// <summary>Get the value of the stat shift (optional base value) applied by this object</summary>
		// Token: 0x060063D0 RID: 25552 RVA: 0x001EC22C File Offset: 0x001EA42C
		public int GetStatShift(GameObject target, string statName, bool baseValue = false)
		{
			if (target == null || !target.IsValid() || !target.HasStat(statName) || this.ActiveShifts == null)
			{
				return 0;
			}
			Dictionary<string, Guid> dictionary;
			if (!this.ActiveShifts.TryGetValue(target.ID, out dictionary))
			{
				return 0;
			}
			string text = statName + (baseValue ? ":base" : "");
			Guid guid;
			if (!dictionary.TryGetValue(text, out guid))
			{
				return 0;
			}
			return target.Statistics[statName].GetShift(guid).Amount;
		}

		/// <summary>Shift a stat on target object by an amount, further calls to SetStatShift with the same stat will
		/// undo the previous shift, and set to the new amount.</summary>
		// Token: 0x060063D1 RID: 25553 RVA: 0x001EC2A8 File Offset: 0x001EA4A8
		public bool SetStatShift(GameObject target, string statName, int amount, bool baseValue = false)
		{
			if (target == null || !target.IsValid() || !target.HasStat(statName))
			{
				return false;
			}
			if (this.ActiveShifts == null)
			{
				this.ActiveShifts = new Dictionary<string, Dictionary<string, Guid>>();
			}
			Dictionary<string, Guid> dictionary;
			if (!this.ActiveShifts.TryGetValue(target.ID, out dictionary))
			{
				dictionary = new Dictionary<string, Guid>();
				this.ActiveShifts.Add(target.ID, dictionary);
			}
			Statistic statistic = target.Statistics[statName];
			string text = this.DefaultDisplayName;
			if (target != this.Owner)
			{
				if (string.IsNullOrEmpty(this.DefaultDisplayName))
				{
					text = this.Owner.ShortDisplayName;
				}
				else
				{
					text = Grammar.MakePossessive(this.Owner.ShortDisplayName) + " " + this.DefaultDisplayName;
				}
			}
			string text2 = statName;
			if (baseValue)
			{
				text2 += ":base";
			}
			Guid guid;
			if (!dictionary.TryGetValue(text2, out guid))
			{
				if (amount == 0)
				{
					return true;
				}
				guid = statistic.AddShift(amount, text, baseValue);
				dictionary.Add(text2, guid);
			}
			else if (amount == 0)
			{
				statistic.RemoveShift(guid);
				dictionary.Remove(text2);
			}
			else if (!statistic.UpdateShift(guid, amount))
			{
				dictionary[text2] = statistic.AddShift(amount, text, false);
			}
			return true;
		}

		// Token: 0x060063D2 RID: 25554 RVA: 0x001EC3CE File Offset: 0x001EA5CE
		public bool HasStatShifts()
		{
			return this.ActiveShifts != null && this.ActiveShifts.Count > 0;
		}

		// Token: 0x060063D3 RID: 25555 RVA: 0x001EC3E8 File Offset: 0x001EA5E8
		public void RemoveStatShifts()
		{
			if (!this.HasStatShifts())
			{
				return;
			}
			foreach (KeyValuePair<string, Dictionary<string, Guid>> keyValuePair in this.ActiveShifts)
			{
				string text;
				Dictionary<string, Guid> dictionary;
				keyValuePair.Deconstruct(out text, out dictionary);
				string text2 = text;
				Dictionary<string, Guid> dictionary2 = dictionary;
				try
				{
					GameObject gameObject = ((this.Owner != null && this.Owner.ID == text2) ? this.Owner : GameObject.FindByID(text2));
					if (gameObject == null)
					{
						throw new Exception("Can't resolve object id " + text2);
					}
					this.RemoveTargetShifts(gameObject, dictionary2);
				}
				catch (Exception ex)
				{
					MetricsManager.LogError("Unresolved object trying to remove stat shifts", ex);
				}
			}
			this.ActiveShifts.Clear();
		}

		// Token: 0x060063D4 RID: 25556 RVA: 0x001EC4C0 File Offset: 0x001EA6C0
		public void RemoveStatShifts(GameObject target)
		{
			if (!GameObject.Validate(ref target) || !this.HasStatShifts())
			{
				return;
			}
			Dictionary<string, Guid> dictionary;
			if (this.ActiveShifts.TryGetValue(target.ID, out dictionary))
			{
				this.RemoveTargetShifts(target, dictionary);
				this.ActiveShifts.Remove(target.ID);
			}
		}

		// Token: 0x060063D5 RID: 25557 RVA: 0x001EC510 File Offset: 0x001EA710
		private void RemoveTargetShifts(GameObject Target, Dictionary<string, Guid> Shifts)
		{
			foreach (KeyValuePair<string, Guid> keyValuePair in Shifts)
			{
				string text = (keyValuePair.Key.EndsWith(":base") ? keyValuePair.Key.Substring(0, keyValuePair.Key.Length - 5) : keyValuePair.Key);
				Target.Statistics[text].RemoveShift(keyValuePair.Value);
			}
		}

		// Token: 0x060063D6 RID: 25558 RVA: 0x001EC5A8 File Offset: 0x001EA7A8
		public void RemoveStatShift(GameObject target, string stat, bool baseValue = false)
		{
			if (!GameObject.Validate(ref target) || !this.HasStatShifts())
			{
				return;
			}
			Dictionary<string, Guid> dictionary;
			if (this.ActiveShifts.TryGetValue(target.ID, out dictionary))
			{
				string text = stat + (baseValue ? ":base" : "");
				Guid guid;
				if (dictionary.TryGetValue(text, out guid))
				{
					target.Statistics[stat].RemoveShift(guid);
					dictionary.Remove(text);
				}
			}
		}

		// Token: 0x060063D7 RID: 25559 RVA: 0x001EC618 File Offset: 0x001EA818
		public override string ToString()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			stringBuilder.Append("[StatShifter Owner:").Append(this.Owner.ID).Append(" Description: ")
				.Append(this.DefaultDisplayName);
			stringBuilder.Append(" ");
			if (this.HasStatShifts())
			{
				foreach (KeyValuePair<string, Dictionary<string, Guid>> keyValuePair in this.ActiveShifts)
				{
					stringBuilder.Append("[Object:").Append(keyValuePair.Key).Append(" ");
					foreach (KeyValuePair<string, Guid> keyValuePair2 in keyValuePair.Value)
					{
						stringBuilder.Append(" Stat:").Append(keyValuePair2.Key).Append(":")
							.Append(keyValuePair2.Value);
					}
					stringBuilder.Append("] ");
				}
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x04002EDE RID: 11998
		public string DefaultDisplayName;

		// Token: 0x04002EDF RID: 11999
		public GameObject Owner;

		// Token: 0x04002EE0 RID: 12000
		public Dictionary<string, Dictionary<string, Guid>> ActiveShifts;
	}
}
