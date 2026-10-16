using System;
using System.Collections;
using System.Collections.Generic;
using XRL.Rules;

namespace XRL.World
{
	// Token: 0x02000978 RID: 2424
	[Serializable]
	public class ZoneBuilderCollection : IReadOnlyList<ZoneBuilderBlueprint>, IEnumerable<ZoneBuilderBlueprint>, IEnumerable, IReadOnlyCollection<ZoneBuilderBlueprint>
	{
		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x0600644B RID: 25675 RVA: 0x001EF2AC File Offset: 0x001ED4AC
		public int Count
		{
			get
			{
				return this.Members.Count;
			}
		}

		// Token: 0x17000844 RID: 2116
		public ZoneBuilderBlueprint this[int Index]
		{
			get
			{
				return this.Members[Index].Blueprint;
			}
		}

		// Token: 0x0600644D RID: 25677 RVA: 0x001EF2CC File Offset: 0x001ED4CC
		public ZoneBuilderCollection()
		{
			this.Members = new List<OrderedBuilderBlueprint>();
		}

		// Token: 0x0600644E RID: 25678 RVA: 0x001EF2DF File Offset: 0x001ED4DF
		public ZoneBuilderCollection(string ZoneID)
		{
			this.ZoneID = ZoneID;
			this.Members = new List<OrderedBuilderBlueprint>();
		}

		// Token: 0x0600644F RID: 25679 RVA: 0x001EF2F9 File Offset: 0x001ED4F9
		public ZoneBuilderCollection(string ZoneID, int Capacity)
		{
			this.ZoneID = ZoneID;
			this.Members = new List<OrderedBuilderBlueprint>(Capacity);
		}

		// Token: 0x06006450 RID: 25680 RVA: 0x001EF314 File Offset: 0x001ED514
		public ZoneBuilderCollection(ZoneBuilderCollection Collection)
		{
			this.ZoneID = Collection.ZoneID;
			this.Members = new List<OrderedBuilderBlueprint>(Collection.Members);
		}

		// Token: 0x06006451 RID: 25681 RVA: 0x001EF33C File Offset: 0x001ED53C
		public void Add(OrderedBuilderBlueprint Member)
		{
			if (Member.Blueprint == null)
			{
				MetricsManager.LogError("Attempting to add null zone builder to collection");
				return;
			}
			int num = this.Members.Count;
			int num2 = num - 1;
			while (num2 >= 0 && Member.Priority < this.Members[num2].Priority)
			{
				num = num2;
				num2--;
			}
			this.Members.Insert(num, Member);
		}

		// Token: 0x06006452 RID: 25682 RVA: 0x001EF39E File Offset: 0x001ED59E
		public void Add(ZoneBuilderBlueprint Blueprint, int Priority)
		{
			this.Add(new OrderedBuilderBlueprint(Blueprint, Priority));
		}

		// Token: 0x06006453 RID: 25683 RVA: 0x001EF3B0 File Offset: 0x001ED5B0
		public void AddRange(IEnumerable<ZoneBuilderBlueprint> Blueprints, int Priority)
		{
			foreach (ZoneBuilderBlueprint zoneBuilderBlueprint in Blueprints)
			{
				this.Add(zoneBuilderBlueprint, Priority);
			}
		}

		// Token: 0x06006454 RID: 25684 RVA: 0x001EF3FC File Offset: 0x001ED5FC
		public void AddRange(ZoneBuilderCollection Collection)
		{
			foreach (OrderedBuilderBlueprint orderedBuilderBlueprint in Collection.Members)
			{
				this.Add(orderedBuilderBlueprint);
			}
		}

		// Token: 0x06006455 RID: 25685 RVA: 0x001EF450 File Offset: 0x001ED650
		public bool AddUnique(OrderedBuilderBlueprint Member)
		{
			if (Member.Blueprint == null)
			{
				MetricsManager.LogError("Attempting to add null zone builder to collection");
				return false;
			}
			int num = this.Members.Count;
			for (int i = num - 1; i >= 0; i--)
			{
				if (Member.Priority < this.Members[i].Priority)
				{
					num = i;
				}
				if (this.Members[i].Blueprint.Equals(Member.Blueprint))
				{
					return false;
				}
			}
			this.Members.Insert(num, Member);
			return true;
		}

		// Token: 0x06006456 RID: 25686 RVA: 0x001EF4D4 File Offset: 0x001ED6D4
		public bool AddUnique(ZoneBuilderBlueprint Blueprint, int Priority)
		{
			return this.AddUnique(new OrderedBuilderBlueprint(Blueprint, Priority));
		}

		// Token: 0x06006457 RID: 25687 RVA: 0x001EF4E4 File Offset: 0x001ED6E4
		public bool Remove(string Class)
		{
			for (int i = this.Members.Count - 1; i >= 0; i--)
			{
				if (!(this.Members[i].Blueprint.Class != Class))
				{
					this.Members.RemoveAt(i);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006458 RID: 25688 RVA: 0x001EF538 File Offset: 0x001ED738
		public bool Remove(ZoneBuilderBlueprint Blueprint)
		{
			for (int i = this.Members.Count - 1; i >= 0; i--)
			{
				if (this.Members[i].Blueprint.Equals(Blueprint))
				{
					this.Members.RemoveAt(i);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06006459 RID: 25689 RVA: 0x001EF588 File Offset: 0x001ED788
		public int RemoveAll(string Class, Predicate<ZoneBuilderBlueprint> Predicate = null)
		{
			int num = 0;
			for (int i = this.Members.Count - 1; i >= 0; i--)
			{
				if (!(this.Members[i].Blueprint.Class != Class) && (Predicate == null || Predicate(this.Members[i].Blueprint)))
				{
					this.Members.RemoveAt(i);
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600645A RID: 25690 RVA: 0x001EF5FC File Offset: 0x001ED7FC
		public int RemoveAll(Predicate<ZoneBuilderBlueprint> Predicate)
		{
			int num = 0;
			for (int i = this.Members.Count - 1; i >= 0; i--)
			{
				if (Predicate(this.Members[i].Blueprint))
				{
					this.Members.RemoveAt(i);
					num++;
				}
			}
			return num;
		}

		// Token: 0x0600645B RID: 25691 RVA: 0x001EF650 File Offset: 0x001ED850
		public bool ApplyTo(Zone Zone, string Seed = "", bool Force = false)
		{
			bool flag = false;
			int i = 0;
			int count = this.Members.Count;
			while (i < count)
			{
				if (this.Members[i].Priority < 0)
				{
					flag = true;
				}
				else if (flag)
				{
					break;
				}
				ZoneBuilderBlueprint blueprint = this.Members[i].Blueprint;
				ZoneBuilderBlueprint zoneBuilderBlueprint = blueprint;
				string text = Seed + ((zoneBuilderBlueprint != null) ? zoneBuilderBlueprint.ToString() : null) + i.ToString();
				MetricsManager.rngCheckpoint(text);
				Stat.ReseedFrom(text, false);
				Coach.StartSection(blueprint.ToString(), true);
				if (!blueprint.ApplyTo(Zone) && !Force)
				{
					string text2 = "Builder failed: ";
					ZoneBuilderBlueprint zoneBuilderBlueprint2 = blueprint;
					MetricsManager.LogError(text2 + ((zoneBuilderBlueprint2 != null) ? zoneBuilderBlueprint2.ToString() : null));
					Coach.EndSection();
					return false;
				}
				Coach.EndSection();
				i++;
			}
			return true;
		}

		// Token: 0x0600645C RID: 25692 RVA: 0x001EF714 File Offset: 0x001ED914
		public void Clear()
		{
			this.Members.Clear();
		}

		// Token: 0x0600645D RID: 25693 RVA: 0x001EF724 File Offset: 0x001ED924
		public static void Save(SerializationWriter Writer, ZoneBuilderCollection Collection)
		{
			Writer.Write(Collection.ZoneID);
			if (Collection.Members == null)
			{
				MetricsManager.LogError("Zone builder collection members was null.");
				Writer.WriteOptimized(0);
				return;
			}
			int count = Collection.Members.Count;
			Writer.WriteOptimized(count);
			for (int i = 0; i < count; i++)
			{
				OrderedBuilderBlueprint orderedBuilderBlueprint = Collection.Members[i];
				Writer.WriteOptimized(orderedBuilderBlueprint.Priority);
				if (orderedBuilderBlueprint.Blueprint == null)
				{
					MetricsManager.LogError("Zone builder member was null.");
					Writer.WriteOptimized(-1);
				}
				else
				{
					orderedBuilderBlueprint.Blueprint.SaveTokenized(Writer);
				}
			}
		}

		// Token: 0x0600645E RID: 25694 RVA: 0x001EF7B8 File Offset: 0x001ED9B8
		public static ZoneBuilderCollection Load(SerializationReader Reader, List<ZoneBuilderBlueprint> Blueprints)
		{
			string text = Reader.ReadString();
			int num = Reader.ReadOptimizedInt32();
			ZoneBuilderCollection zoneBuilderCollection = new ZoneBuilderCollection(text, num);
			for (int i = 0; i < num; i++)
			{
				int num2 = Reader.ReadOptimizedInt32();
				int num3 = Reader.ReadOptimizedInt32();
				if (num3 >= 0)
				{
					if (num3 >= Blueprints.Count)
					{
						Blueprints.Add(ZoneBuilderBlueprint.GetSerialized(Reader, num3));
						if (Blueprints[num3] == null)
						{
							MetricsManager.LogError("Deserialized null zone builder");
						}
					}
					ZoneBuilderBlueprint zoneBuilderBlueprint = Blueprints[num3];
					if (zoneBuilderBlueprint != null)
					{
						zoneBuilderCollection.Add(zoneBuilderBlueprint, num2);
					}
				}
			}
			return zoneBuilderCollection;
		}

		// Token: 0x0600645F RID: 25695 RVA: 0x001EF83D File Offset: 0x001EDA3D
		public ZoneBuilderCollection.Enumerator GetEnumerator()
		{
			return new ZoneBuilderCollection.Enumerator(this);
		}

		// Token: 0x06006460 RID: 25696 RVA: 0x001EF845 File Offset: 0x001EDA45
		IEnumerator<ZoneBuilderBlueprint> IEnumerable<ZoneBuilderBlueprint>.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06006461 RID: 25697 RVA: 0x001EF852 File Offset: 0x001EDA52
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04002F13 RID: 12051
		public string ZoneID;

		// Token: 0x04002F14 RID: 12052
		public List<OrderedBuilderBlueprint> Members;

		// Token: 0x02001A7A RID: 6778
		[Serializable]
		public struct Enumerator : IEnumerator<ZoneBuilderBlueprint>, IEnumerator, IDisposable
		{
			// Token: 0x17000ECC RID: 3788
			// (get) Token: 0x0600E7D1 RID: 59345 RVA: 0x00536DDB File Offset: 0x00534FDB
			public ZoneBuilderBlueprint Current
			{
				get
				{
					return this.Blueprint;
				}
			}

			// Token: 0x17000ECD RID: 3789
			// (get) Token: 0x0600E7D2 RID: 59346 RVA: 0x00536DE3 File Offset: 0x00534FE3
			object IEnumerator.Current
			{
				get
				{
					return this.Blueprint;
				}
			}

			// Token: 0x0600E7D3 RID: 59347 RVA: 0x00536DEB File Offset: 0x00534FEB
			public Enumerator(ZoneBuilderCollection Collection)
			{
				this.Collection = Collection;
				this.Index = 0;
				this.Blueprint = null;
			}

			// Token: 0x0600E7D4 RID: 59348 RVA: 0x00536E02 File Offset: 0x00535002
			public void Dispose()
			{
				this.Collection = null;
				this.Blueprint = null;
			}

			// Token: 0x0600E7D5 RID: 59349 RVA: 0x00536E14 File Offset: 0x00535014
			public bool MoveNext()
			{
				if (this.Index >= this.Collection.Members.Count)
				{
					this.Index = this.Collection.Members.Count + 1;
					this.Blueprint = null;
					return false;
				}
				OrderedBuilderBlueprint orderedBuilderBlueprint = this.Collection.Members[this.Index];
				this.Blueprint = orderedBuilderBlueprint.Blueprint;
				this.Index++;
				return true;
			}

			// Token: 0x0600E7D6 RID: 59350 RVA: 0x00536E8C File Offset: 0x0053508C
			void IEnumerator.Reset()
			{
				this.Index = 0;
				this.Blueprint = null;
			}

			// Token: 0x04006968 RID: 26984
			private ZoneBuilderCollection Collection;

			// Token: 0x04006969 RID: 26985
			private int Index;

			// Token: 0x0400696A RID: 26986
			private ZoneBuilderBlueprint Blueprint;
		}
	}
}
