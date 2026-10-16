using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using XRL.Rules;

namespace XRL
{
	// Token: 0x02000536 RID: 1334
	public abstract class PopulationList : PopulationItem
	{
		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x06003916 RID: 14614 RVA: 0x001182D0 File Offset: 0x001164D0
		public ulong TotalWeight
		{
			get
			{
				ulong num = 0UL;
				foreach (PopulationItem populationItem in this.Items)
				{
					num += (ulong)populationItem.Weight;
				}
				return num;
			}
		}

		// Token: 0x06003917 RID: 14615 RVA: 0x0011832C File Offset: 0x0011652C
		public bool TryFindSimilar(PopulationItem Needle, out PopulationItem Item)
		{
			return this.TryFindSimilar(Needle.Name, Needle.GetType(), out Item);
		}

		// Token: 0x06003918 RID: 14616 RVA: 0x00118344 File Offset: 0x00116544
		public bool TryFindSimilar(string Name, Type Type, out PopulationItem Item)
		{
			if (!Name.IsNullOrEmpty())
			{
				foreach (PopulationItem populationItem in this.Items)
				{
					if (populationItem.Name == Name && populationItem.GetType() == Type)
					{
						Item = populationItem;
						return true;
					}
				}
			}
			Item = null;
			return false;
		}

		// Token: 0x06003919 RID: 14617 RVA: 0x001183C4 File Offset: 0x001165C4
		public override void Generate(List<PopulationResult> Result, Dictionary<string, string> Vars = null, string DefaultHint = null)
		{
			if (this.Style.EqualsNoCase("pickone"))
			{
				ulong num = this.TotalWeight;
				ulong num2 = Stat.NextULong(0UL, num);
				num = 0UL;
				using (List<PopulationItem>.Enumerator enumerator = this.Items.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						PopulationItem populationItem = enumerator.Current;
						if (num2 >= num && num2 < num + (ulong)populationItem.Weight)
						{
							populationItem.Generate(Result, Vars, this.Hint ?? DefaultHint);
							break;
						}
						num += (ulong)populationItem.Weight;
					}
					return;
				}
			}
			foreach (PopulationItem populationItem2 in this.Items)
			{
				populationItem2.Generate(Result, Vars, this.Hint ?? DefaultHint);
			}
		}

		// Token: 0x0600391A RID: 14618 RVA: 0x001184B0 File Offset: 0x001166B0
		public override PopulationResult GenerateOne(Dictionary<string, string> Vars = null, string DefaultHint = null)
		{
			if (this.Style.EqualsNoCase("pickone"))
			{
				ulong num = this.TotalWeight;
				ulong num2 = Stat.NextULong(0UL, num);
				num = 0UL;
				using (List<PopulationItem>.Enumerator enumerator = this.Items.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						PopulationItem populationItem = enumerator.Current;
						if (num2 >= num && num2 < num + (ulong)populationItem.Weight)
						{
							return populationItem.GenerateOne(Vars, this.Hint ?? DefaultHint);
						}
						num += (ulong)populationItem.Weight;
					}
					goto IL_00CF;
				}
			}
			foreach (PopulationItem populationItem2 in this.Items)
			{
				PopulationResult populationResult = populationItem2.GenerateOne(Vars, this.Hint ?? DefaultHint);
				if (populationResult != null)
				{
					return populationResult;
				}
			}
			IL_00CF:
			return null;
		}

		// Token: 0x0600391B RID: 14619 RVA: 0x001185AC File Offset: 0x001167AC
		public override void GenerateStructured(PopulationStructuredResult Result, Dictionary<string, string> Vars = null)
		{
			if (this.Style.EqualsNoCase("pickone"))
			{
				ulong num = this.TotalWeight;
				ulong num2 = Stat.NextULong(0UL, num);
				num = 0UL;
				using (List<PopulationItem>.Enumerator enumerator = this.Items.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						PopulationItem populationItem = enumerator.Current;
						if (num2 >= num && num2 < num + (ulong)populationItem.Weight)
						{
							populationItem.GenerateStructured(Result, Vars);
							break;
						}
						num += (ulong)populationItem.Weight;
					}
					return;
				}
			}
			foreach (PopulationItem populationItem2 in this.Items)
			{
				populationItem2.GenerateStructured(Result, Vars);
			}
		}

		// Token: 0x0600391C RID: 14620 RVA: 0x00118684 File Offset: 0x00116884
		public int Count(Predicate<PopulationItem> Predicate)
		{
			int num = 0;
			foreach (PopulationItem populationItem in this.Items)
			{
				if (Predicate(populationItem))
				{
					num++;
				}
				PopulationList populationList = populationItem as PopulationList;
				if (populationList != null)
				{
					num += populationList.Count(Predicate);
				}
			}
			return num;
		}

		// Token: 0x0600391D RID: 14621 RVA: 0x001186F4 File Offset: 0x001168F4
		public void ForEach(Action<PopulationItem> Action)
		{
			foreach (PopulationItem populationItem in this.Items)
			{
				Action(populationItem);
				PopulationList populationList = populationItem as PopulationList;
				if (populationList != null)
				{
					populationList.ForEach(Action);
				}
			}
		}

		// Token: 0x0600391E RID: 14622 RVA: 0x00118758 File Offset: 0x00116958
		public override PopulationItem Find(Predicate<PopulationItem> Predicate)
		{
			if (Predicate(this))
			{
				return this;
			}
			foreach (PopulationItem populationItem in this.Items)
			{
				PopulationItem populationItem2 = populationItem.Find(Predicate);
				if (populationItem2 != null)
				{
					return populationItem2;
				}
			}
			return null;
		}

		// Token: 0x0600391F RID: 14623 RVA: 0x001187C0 File Offset: 0x001169C0
		public void AddItem(PopulationItem Item)
		{
			PopulationGroup populationGroup = Item as PopulationGroup;
			if (populationGroup != null)
			{
				populationGroup.Parent = this;
				if (!Item.Name.IsNullOrEmpty())
				{
					populationGroup.Info.GroupLookup.Add(Item.Name, populationGroup);
				}
			}
			this.Items.Add(Item);
		}

		// Token: 0x06003920 RID: 14624 RVA: 0x0011880E File Offset: 0x00116A0E
		public void RemoveItem(PopulationItem Item)
		{
			this.Items.Remove(Item);
		}

		// Token: 0x06003921 RID: 14625 RVA: 0x00118820 File Offset: 0x00116A20
		public override void MergeFrom(PopulationItem Item, PopulationInfo Info)
		{
			base.MergeFrom(Item, Info);
			PopulationList populationList = (PopulationList)Item;
			if (populationList.Style != null)
			{
				this.Style = populationList.Style;
			}
			foreach (PopulationItem populationItem in populationList.Items)
			{
				bool flag = !populationItem.Remove;
				if (populationItem.Merge)
				{
					PopulationGroup populationGroup = populationItem as PopulationGroup;
					PopulationItem populationItem2;
					if (populationGroup != null)
					{
						PopulationGroup populationGroup2;
						if (!populationGroup.Name.IsNullOrEmpty() && Info.GroupLookup.TryGetValue(populationGroup.Name, out populationGroup2))
						{
							populationGroup2.MergeFrom(populationGroup, Info);
							flag = false;
						}
					}
					else if (this.TryFindSimilar(populationItem, out populationItem2))
					{
						populationItem2.MergeFrom(populationItem, Info);
						flag = false;
					}
				}
				else if (populationItem.Remove || populationItem.Replace)
				{
					PopulationGroup populationGroup3 = populationItem as PopulationGroup;
					PopulationItem populationItem3;
					if (populationGroup3 != null)
					{
						PopulationGroup populationGroup4;
						if (!populationGroup3.Name.IsNullOrEmpty() && Info.GroupLookup.TryGetValue(populationGroup3.Name, out populationGroup4))
						{
							populationGroup4.Parent.Items.Remove(populationGroup4);
							Info.GroupLookup.Remove(populationGroup4.Name);
							if (populationItem.Replace)
							{
								populationGroup4.Parent.AddItem(populationItem);
							}
							flag = false;
						}
					}
					else if (this.TryFindSimilar(populationItem, out populationItem3))
					{
						this.Items.Remove(populationItem3);
					}
				}
				if (flag)
				{
					this.AddItem(populationItem);
				}
			}
		}

		// Token: 0x06003922 RID: 14626 RVA: 0x001189C0 File Offset: 0x00116BC0
		public PopulationList.ItemEnumerator IterateItems()
		{
			return new PopulationList.ItemEnumerator(this);
		}

		// Token: 0x04001CDB RID: 7387
		public string Style;

		// Token: 0x04001CDC RID: 7388
		public List<PopulationItem> Items = new List<PopulationItem>();

		// Token: 0x02001960 RID: 6496
		public struct ItemEnumerator : IDisposable
		{
			// Token: 0x17000E67 RID: 3687
			// (get) Token: 0x0600E371 RID: 58225 RVA: 0x00527BBF File Offset: 0x00525DBF
			public PopulationItem Current
			{
				get
				{
					return this.Item;
				}
			}

			// Token: 0x0600E372 RID: 58226 RVA: 0x00527BC8 File Offset: 0x00525DC8
			public ItemEnumerator(PopulationList List)
			{
				if (!PopulationList.ItemEnumerator.Queues.TryPop(out this.Queue))
				{
					this.Queue = new Queue<PopulationList>();
				}
				this.List = List;
				this.Item = null;
				this.Index = 0;
				this.Count = List.Items.Count;
			}

			// Token: 0x0600E373 RID: 58227 RVA: 0x00527C18 File Offset: 0x00525E18
			public PopulationList.ItemEnumerator GetEnumerator()
			{
				return this;
			}

			// Token: 0x0600E374 RID: 58228 RVA: 0x00527C20 File Offset: 0x00525E20
			public bool MoveNext()
			{
				while (this.Index == this.Count)
				{
					if (!this.Queue.TryDequeue(out this.List))
					{
						return false;
					}
					this.Index = 0;
					this.Count = this.List.Items.Count;
				}
				List<PopulationItem> items = this.List.Items;
				int index = this.Index;
				this.Index = index + 1;
				this.Item = items[index];
				PopulationList populationList = this.Item as PopulationList;
				if (populationList != null)
				{
					this.Queue.Enqueue(populationList);
				}
				return true;
			}

			// Token: 0x0600E375 RID: 58229 RVA: 0x00527CB4 File Offset: 0x00525EB4
			public void Dispose()
			{
				this.Queue.Clear();
				PopulationList.ItemEnumerator.Queues.Push(this.Queue);
			}

			// Token: 0x040065AF RID: 26031
			private static ConcurrentStack<Queue<PopulationList>> Queues = new ConcurrentStack<Queue<PopulationList>>();

			// Token: 0x040065B0 RID: 26032
			private Queue<PopulationList> Queue;

			// Token: 0x040065B1 RID: 26033
			private PopulationList List;

			// Token: 0x040065B2 RID: 26034
			private PopulationItem Item;

			// Token: 0x040065B3 RID: 26035
			private int Index;

			// Token: 0x040065B4 RID: 26036
			private int Count;
		}
	}
}
