using System;
using System.Collections;
using System.Collections.Generic;
using XRL.Rules;

namespace XRL.World
{
	// Token: 0x02000998 RID: 2456
	[Serializable]
	public class ZonePartCollection : IReadOnlyCollection<ZonePartBlueprint>, IEnumerable<ZonePartBlueprint>, IEnumerable
	{
		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06006A0D RID: 27149 RVA: 0x002186C8 File Offset: 0x002168C8
		public int Count
		{
			get
			{
				return this.Members.Count;
			}
		}

		// Token: 0x1700088B RID: 2187
		public ZonePartBlueprint this[int Index]
		{
			get
			{
				return this.Members[Index];
			}
		}

		// Token: 0x06006A0F RID: 27151 RVA: 0x002186E3 File Offset: 0x002168E3
		public ZonePartCollection()
		{
			this.Members = new List<ZonePartBlueprint>();
		}

		// Token: 0x06006A10 RID: 27152 RVA: 0x002186F6 File Offset: 0x002168F6
		public ZonePartCollection(string ZoneID)
		{
			this.ZoneID = ZoneID;
			this.Members = new List<ZonePartBlueprint>();
		}

		// Token: 0x06006A11 RID: 27153 RVA: 0x00218710 File Offset: 0x00216910
		public ZonePartCollection(string ZoneID, int Capacity)
		{
			this.ZoneID = ZoneID;
			this.Members = new List<ZonePartBlueprint>(Capacity);
		}

		// Token: 0x06006A12 RID: 27154 RVA: 0x0021872B File Offset: 0x0021692B
		public ZonePartCollection(ZonePartCollection Collection)
		{
			this.ZoneID = Collection.ZoneID;
			this.Members = new List<ZonePartBlueprint>(Collection.Members);
		}

		// Token: 0x06006A13 RID: 27155 RVA: 0x00218750 File Offset: 0x00216950
		public void Add(ZonePartBlueprint Blueprint)
		{
			this.Members.Add(Blueprint);
		}

		// Token: 0x06006A14 RID: 27156 RVA: 0x0021875E File Offset: 0x0021695E
		public void AddRange(ZonePartCollection Collection)
		{
			this.Members.AddRange(Collection.Members);
		}

		// Token: 0x06006A15 RID: 27157 RVA: 0x00218774 File Offset: 0x00216974
		public bool AddUnique(ZonePartBlueprint Member)
		{
			for (int i = this.Members.Count - 1; i >= 0; i--)
			{
				if (this.Members[i].Equals(Member))
				{
					return false;
				}
			}
			this.Add(Member);
			return true;
		}

		// Token: 0x06006A16 RID: 27158 RVA: 0x002187B8 File Offset: 0x002169B8
		public bool Remove(string Name)
		{
			for (int i = this.Members.Count - 1; i >= 0; i--)
			{
				if (!(this.Members[i].Name != Name))
				{
					this.Members.RemoveAt(i);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006A17 RID: 27159 RVA: 0x00218808 File Offset: 0x00216A08
		public bool Remove(ZonePartBlueprint Blueprint)
		{
			for (int i = this.Members.Count - 1; i >= 0; i--)
			{
				if (this.Members[i].Equals(Blueprint))
				{
					this.Members.RemoveAt(i);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006A18 RID: 27160 RVA: 0x00218850 File Offset: 0x00216A50
		public int RemoveAll(string Name, Predicate<ZonePartBlueprint> Predicate = null)
		{
			int num = 0;
			for (int i = this.Members.Count - 1; i >= 0; i--)
			{
				if (!(this.Members[i].Name != Name) && (Predicate == null || Predicate(this.Members[i])))
				{
					this.Members.RemoveAt(i);
					num++;
				}
			}
			return num;
		}

		// Token: 0x06006A19 RID: 27161 RVA: 0x002188B8 File Offset: 0x00216AB8
		public int RemoveAll(Predicate<ZonePartBlueprint> Predicate)
		{
			int num = 0;
			for (int i = this.Members.Count - 1; i >= 0; i--)
			{
				if (Predicate(this.Members[i]))
				{
					this.Members.RemoveAt(i);
					num++;
				}
			}
			return num;
		}

		// Token: 0x06006A1A RID: 27162 RVA: 0x00218904 File Offset: 0x00216B04
		public bool ApplyTo(Zone Zone, string Seed = "", bool Force = false)
		{
			int i = 0;
			int count = this.Members.Count;
			while (i < count)
			{
				ZonePartBlueprint zonePartBlueprint = this.Members[i];
				ZonePartBlueprint zonePartBlueprint2 = zonePartBlueprint;
				string text = Seed + ((zonePartBlueprint2 != null) ? zonePartBlueprint2.ToString() : null) + i.ToString();
				MetricsManager.rngCheckpoint(text);
				Stat.ReseedFrom(text, false);
				Coach.StartSection(zonePartBlueprint.ToString(), true);
				if (!zonePartBlueprint.ApplyTo(Zone) && !Force)
				{
					string text2 = "Part setup failed: ";
					ZonePartBlueprint zonePartBlueprint3 = zonePartBlueprint;
					MetricsManager.LogError(text2 + ((zonePartBlueprint3 != null) ? zonePartBlueprint3.ToString() : null));
					Coach.EndSection();
					return false;
				}
				Coach.EndSection();
				i++;
			}
			return true;
		}

		// Token: 0x06006A1B RID: 27163 RVA: 0x0021899D File Offset: 0x00216B9D
		public void Clear()
		{
			this.Members.Clear();
		}

		// Token: 0x06006A1C RID: 27164 RVA: 0x002189AC File Offset: 0x00216BAC
		public static void Save(SerializationWriter Writer, ZonePartCollection Collection)
		{
			Writer.Write(Collection.ZoneID);
			Writer.Write(Collection.Members.Count);
			foreach (ZonePartBlueprint zonePartBlueprint in Collection.Members)
			{
				zonePartBlueprint.SaveTokenized(Writer);
			}
		}

		// Token: 0x06006A1D RID: 27165 RVA: 0x00218A1C File Offset: 0x00216C1C
		public static ZonePartCollection Load(SerializationReader Reader, List<ZonePartBlueprint> Blueprints)
		{
			string text = Reader.ReadString();
			int num = Reader.ReadInt32();
			ZonePartCollection zonePartCollection = new ZonePartCollection(text, num);
			for (int i = 0; i < num; i++)
			{
				int num2 = Reader.ReadInt32();
				if (num2 >= Blueprints.Count)
				{
					Blueprints.Add(ZonePartBlueprint.GetSerialized(Reader, num2));
				}
				zonePartCollection.Add(Blueprints[num2]);
			}
			return zonePartCollection;
		}

		// Token: 0x06006A1E RID: 27166 RVA: 0x00218A74 File Offset: 0x00216C74
		public ZonePartCollection.Enumerator GetEnumerator()
		{
			return new ZonePartCollection.Enumerator(this);
		}

		// Token: 0x06006A1F RID: 27167 RVA: 0x00218A7C File Offset: 0x00216C7C
		IEnumerator<ZonePartBlueprint> IEnumerable<ZonePartBlueprint>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06006A20 RID: 27168 RVA: 0x00218A89 File Offset: 0x00216C89
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04003086 RID: 12422
		public string ZoneID;

		// Token: 0x04003087 RID: 12423
		public List<ZonePartBlueprint> Members;

		// Token: 0x02001AAA RID: 6826
		[Serializable]
		public struct Enumerator : IEnumerator<ZonePartBlueprint>, IEnumerator, IDisposable
		{
			// Token: 0x17000EEA RID: 3818
			// (get) Token: 0x0600E8A0 RID: 59552 RVA: 0x00538EFF File Offset: 0x005370FF
			public ZonePartBlueprint Current
			{
				get
				{
					return this.Blueprint;
				}
			}

			// Token: 0x17000EEB RID: 3819
			// (get) Token: 0x0600E8A1 RID: 59553 RVA: 0x00538F07 File Offset: 0x00537107
			object IEnumerator.Current
			{
				get
				{
					return this.Blueprint;
				}
			}

			// Token: 0x0600E8A2 RID: 59554 RVA: 0x00538F0F File Offset: 0x0053710F
			public Enumerator(ZonePartCollection Collection)
			{
				this.Collection = Collection;
				this.Index = 0;
				this.Blueprint = null;
			}

			// Token: 0x0600E8A3 RID: 59555 RVA: 0x00538F26 File Offset: 0x00537126
			public void Dispose()
			{
				this.Collection = null;
				this.Blueprint = null;
			}

			// Token: 0x0600E8A4 RID: 59556 RVA: 0x00538F38 File Offset: 0x00537138
			public bool MoveNext()
			{
				if (this.Index >= this.Collection.Members.Count)
				{
					this.Index = this.Collection.Members.Count + 1;
					this.Blueprint = null;
					return false;
				}
				this.Blueprint = this.Collection.Members[this.Index];
				this.Index++;
				return true;
			}

			// Token: 0x0600E8A5 RID: 59557 RVA: 0x00538FA9 File Offset: 0x005371A9
			void IEnumerator.Reset()
			{
				this.Index = 0;
				this.Blueprint = null;
			}

			// Token: 0x04006A2F RID: 27183
			private ZonePartCollection Collection;

			// Token: 0x04006A30 RID: 27184
			private int Index;

			// Token: 0x04006A31 RID: 27185
			private ZonePartBlueprint Blueprint;
		}
	}
}
