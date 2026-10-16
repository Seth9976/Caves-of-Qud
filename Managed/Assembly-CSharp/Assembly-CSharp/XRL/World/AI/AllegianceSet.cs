using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using XRL.Collections;

namespace XRL.World.AI
{
	// Token: 0x02000B6B RID: 2923
	public sealed class AllegianceSet : StringMap<int>
	{
		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x06007534 RID: 30004 RVA: 0x002BF303 File Offset: 0x002BD503
		// (set) Token: 0x06007535 RID: 30005 RVA: 0x002BF310 File Offset: 0x002BD510
		public bool Hostile
		{
			get
			{
				return (this.Flags & 1) == 1;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 1) : (this.Flags & -2));
			}
		}

		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x06007536 RID: 30006 RVA: 0x002BF32E File Offset: 0x002BD52E
		// (set) Token: 0x06007537 RID: 30007 RVA: 0x002BF33B File Offset: 0x002BD53B
		public bool Calm
		{
			get
			{
				return (this.Flags & 2) == 2;
			}
			set
			{
				this.Flags = (value ? (this.Flags | 2) : (this.Flags & -3));
			}
		}

		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x06007538 RID: 30008 RVA: 0x002BF35C File Offset: 0x002BD55C
		public int TotalWeight
		{
			get
			{
				int num = 0;
				for (int i = 0; i < this.Length; i++)
				{
					if (this.Slots[i].Key != null)
					{
						num += this.Slots[i].Value;
					}
				}
				return num;
			}
		}

		// Token: 0x06007539 RID: 30009 RVA: 0x002BF3A4 File Offset: 0x002BD5A4
		public int GetBaseFeeling(GameObject Object, IDictionary<string, int> Override = null)
		{
			float num = (float)this.TotalWeight * 1f;
			float num2 = 0f;
			if (num > 0f)
			{
				for (int i = 0; i < this.Length; i++)
				{
					if (this.Slots[i].Key != null)
					{
						num2 += (float)Factions.GetFeelingFactionToObject(this.Slots[i].Key, Object, Override) * ((float)this.Slots[i].Value / num);
					}
				}
			}
			return Mathf.RoundToInt(num2);
		}

		// Token: 0x0600753A RID: 30010 RVA: 0x002BF428 File Offset: 0x002BD628
		public int GetBaseFeeling(Faction Faction, IDictionary<string, int> Override = null)
		{
			float num = (float)this.TotalWeight * 1f;
			float num2 = 0f;
			if (num > 0f)
			{
				for (int i = 0; i < this.Length; i++)
				{
					if (this.Slots[i].Key != null)
					{
						int num3;
						if (Override != null && Override.TryGetValue(this.Slots[i].Key, out num3))
						{
							num2 += (float)num3 * ((float)this.Slots[i].Value / num);
						}
						else
						{
							num2 += (float)Faction.GetFeelingTowardsFaction(this.Slots[i].Key, null, 0) * ((float)this.Slots[i].Value / num);
						}
					}
				}
			}
			return Mathf.RoundToInt(num2);
		}

		// Token: 0x0600753B RID: 30011 RVA: 0x002BF4FC File Offset: 0x002BD6FC
		public void Copy(AllegianceSet Set)
		{
			this.Clear();
			this.Flags = Set.Flags;
			foreach (KeyValuePair<string, int> keyValuePair in Set)
			{
				base.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x0600753C RID: 30012 RVA: 0x002BF56C File Offset: 0x002BD76C
		public void AppendTo(StringBuilder Builder)
		{
			Builder.Append('(');
			bool flag = true;
			foreach (KeyValuePair<string, int> keyValuePair in this)
			{
				string text;
				int num;
				keyValuePair.Deconstruct(out text, out num);
				string text2 = text;
				int num2 = num;
				if (!flag)
				{
					Builder.Append(", ");
				}
				Builder.Append(text2).Append('-').Append(num2);
				flag = false;
			}
			Builder.Append(')');
		}

		// Token: 0x0600753D RID: 30013 RVA: 0x002BF600 File Offset: 0x002BD800
		public override void Write(SerializationWriter Writer)
		{
			Writer.WriteOptimized(this.SourceID);
			Writer.Write(this.Reason);
			Writer.WriteOptimized(this.Flags);
			Writer.WriteOptimized(this.Amount);
			for (int i = 0; i < this.Length; i++)
			{
				if (this.Slots[i].Key != null)
				{
					Writer.WriteOptimized(this.Slots[i].Key);
					Writer.WriteOptimized(this.Slots[i].Value);
				}
			}
			Writer.WriteComposite<AllegianceSet>(this.Previous);
		}

		// Token: 0x0600753E RID: 30014 RVA: 0x002BF69C File Offset: 0x002BD89C
		public override void Read(SerializationReader Reader)
		{
			this.SourceID = Reader.ReadOptimizedInt32();
			this.Reason = (IAllyReason)Reader.ReadComposite();
			this.Flags = Reader.ReadOptimizedInt32();
			int num = Reader.ReadOptimizedInt32();
			base.Resize(num);
			for (int i = 0; i < num; i++)
			{
				base.InsertInternal(Reader.ReadOptimizedString(), Reader.ReadOptimizedInt32(), false, false);
			}
			this.Previous = Reader.ReadComposite<AllegianceSet>();
		}

		// Token: 0x0600753F RID: 30015 RVA: 0x002BF710 File Offset: 0x002BD910
		public string GetDebugInternalsEntry()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			for (int i = 0; i < this.Length; i++)
			{
				if (this.Slots[i].Key != null)
				{
					stringBuilder.Compound(this.Slots[i].Key, "; ");
					stringBuilder.Compound(this.Slots[i].Value, ": ");
				}
			}
			return Event.FinalizeString(stringBuilder);
		}

		// Token: 0x06007540 RID: 30016 RVA: 0x002BF788 File Offset: 0x002BD988
		public void ClearSlots()
		{
			base.Clear();
		}

		// Token: 0x06007541 RID: 30017 RVA: 0x002BF790 File Offset: 0x002BD990
		public override void Clear()
		{
			base.Clear();
			this.SourceID = 0;
			this.Previous = null;
			this.Reason = null;
			this.Flags = 0;
		}

		// Token: 0x04003596 RID: 13718
		public int SourceID;

		// Token: 0x04003597 RID: 13719
		public AllegianceSet Previous;

		// Token: 0x04003598 RID: 13720
		public IAllyReason Reason;

		// Token: 0x04003599 RID: 13721
		public int Flags;
	}
}
