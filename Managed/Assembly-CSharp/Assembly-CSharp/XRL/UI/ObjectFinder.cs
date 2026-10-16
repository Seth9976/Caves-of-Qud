using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using ConsoleLib.Console;
using XRL.UI.ObjectFinderClassifiers;
using XRL.UI.ObjectFinderContexts;
using XRL.UI.ObjectFinderSorters;
using XRL.Wish;
using XRL.World;

namespace XRL.UI
{
	// Token: 0x020014BC RID: 5308
	[HasWishCommand]
	[HasGameBasedStaticCache]
	public class ObjectFinder
	{
		// Token: 0x0600D054 RID: 53332 RVA: 0x004CE018 File Offset: 0x004CC218
		[GameBasedCacheInit]
		public static ObjectFinder Reset()
		{
			ObjectFinder objectFinder = ObjectFinder.instance;
			if (objectFinder != null)
			{
				objectFinder.Destroy();
			}
			ObjectFinder.instance = new ObjectFinder();
			ObjectFinder.instance.filters.Add(new ObjectFinder.FilterRule
			{
				classifier = new Everything(),
				enabled = true,
				show = true
			});
			return ObjectFinder.instance;
		}

		// Token: 0x0600D055 RID: 53333 RVA: 0x004CE074 File Offset: 0x004CC274
		public void LoadDefaults()
		{
			this.filters.Insert(0, new ObjectFinder.FilterRule
			{
				classifier = new Cosmetic(),
				enabled = true,
				show = false
			});
			this.filters.Insert(0, new ObjectFinder.FilterRule
			{
				classifier = new Pools(),
				enabled = true,
				show = false
			});
			this.filters.Insert(0, new ObjectFinder.FilterRule
			{
				classifier = new NonCombatPlantlife(),
				enabled = true,
				show = false
			});
			this.filters.Insert(0, new ObjectFinder.FilterRule
			{
				classifier = new Walls(),
				enabled = false,
				show = false
			});
			this.filters.Insert(0, new ObjectFinder.FilterRule
			{
				classifier = new NotTakeable(),
				enabled = true,
				show = false
			});
			this.filters.Insert(0, new ObjectFinder.FilterRule
			{
				classifier = new Player(),
				enabled = true,
				show = false
			});
			this.Add(new NearbyItems());
		}

		// Token: 0x0600D056 RID: 53334 RVA: 0x004CE188 File Offset: 0x004CC388
		public void ReadOptions()
		{
			ObjectFinder.FilterRule filterRule = this.filters.Find((ObjectFinder.FilterRule r) => r.classifier is NonCombatPlantlife);
			ObjectFinder.FilterRule filterRule2 = this.filters.Find((ObjectFinder.FilterRule r) => r.classifier is Pools);
			ObjectFinder.FilterRule filterRule3 = this.filters.Find((ObjectFinder.FilterRule r) => r.classifier is NotTakeable);
			if (filterRule != null)
			{
				filterRule.show = Options.OverlayNearbyObjectsPlants;
			}
			if (filterRule2 != null)
			{
				filterRule2.show = Options.OverlayNearbyObjectsPools;
			}
			if (filterRule3 != null)
			{
				filterRule3.enabled = Options.OverlayNearbyObjectsTakeable;
			}
			this.contextUpdated = true;
		}

		// Token: 0x0600D057 RID: 53335 RVA: 0x004CE247 File Offset: 0x004CC447
		public void Add(ObjectFinder.Context context)
		{
			if (context == null)
			{
				throw new ArgumentNullException();
			}
			if (this.contexts.Contains(context))
			{
				throw new ArgumentException();
			}
			context.setFinder(this);
			this.contexts.Add(context);
			context.Enable();
		}

		// Token: 0x0600D058 RID: 53336 RVA: 0x004CE280 File Offset: 0x004CC480
		public void Remove(ObjectFinder.Context context)
		{
			if (context == null)
			{
				throw new ArgumentNullException();
			}
			if (!this.contexts.Contains(context))
			{
				throw new ArgumentException();
			}
			context.Disable();
			context.setFinder(null);
			if (this.contexts.Contains(context))
			{
				this.contexts.Remove(context);
			}
		}

		// Token: 0x0600D059 RID: 53337 RVA: 0x004CE2D4 File Offset: 0x004CC4D4
		public void Destroy()
		{
			if (this.contextObjects != null)
			{
				foreach (ObjectFinder.Context context in this.contextObjects.Keys)
				{
					context.Disable();
				}
				this.contextObjects = null;
			}
			this.filterCache = null;
			this._updateFilterAccepted = null;
			this._updateFilterRejected = null;
		}

		// Token: 0x0600D05A RID: 53338 RVA: 0x004CE350 File Offset: 0x004CC550
		protected List<GameObject> GetContextList(ObjectFinder.Context context)
		{
			if (context == null)
			{
				return null;
			}
			if (this.contextObjects == null)
			{
				return null;
			}
			List<GameObject> list;
			if (this.contextObjects.TryGetValue(context, out list))
			{
				return list;
			}
			list = new List<GameObject>(64);
			this.contextObjects.Add(context, list);
			return list;
		}

		// Token: 0x0600D05B RID: 53339 RVA: 0x004CE394 File Offset: 0x004CC594
		[WishCommand(null, null)]
		public static bool ConfigObjectFinderFilters()
		{
			ObjectFinder objectFinder = ObjectFinder.instance;
			if (objectFinder != null)
			{
				objectFinder.ConfigFilters();
			}
			return true;
		}

		// Token: 0x0600D05C RID: 53340 RVA: 0x004CE3A8 File Offset: 0x004CC5A8
		public void ConfigFilters()
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			for (;;)
			{
				string[] array = new string[this.filters.Count];
				for (int i = 0; i < this.filters.Count; i++)
				{
					array[i] = stringBuilder.Clear().Append(this.filters[i].classifier.GetDisplayName()).AppendMarkupNode(this.filters[i].stateColor, " [" + this.filters[i].stateName + "]")
						.ToString();
				}
				int num = Popup.PickOption("Pick a filter to change", null, "", "Sounds/UI/ui_notification", array, null, null, null, null, null, null, 0, 60, 0, -1, true, false, false, true, false, null, null);
				if (num < 0)
				{
					break;
				}
				List<string> list = new List<string> { "Show Items", "Hide Items", "Ignore Rule", "Move Up", "Move Down" };
				ObjectFinder.FilterRule filterRule = this.filters[num];
				if (num == this.filters.Count - 1)
				{
					list.RemoveAt(4);
				}
				if (num == 0)
				{
					list.RemoveAt(3);
				}
				if (!filterRule.enabled)
				{
					list.RemoveAt(2);
				}
				else if (filterRule.show)
				{
					list.RemoveAt(0);
				}
				else
				{
					list.RemoveAt(1);
				}
				int num2 = Popup.PickOption(filterRule.classifier.GetDisplayName(), null, "", "Sounds/UI/ui_notification", list.ToArray(), null, null, null, null, null, null, 0, 60, 0, -1, true, false, false, true, false, null, null);
				if (num2 != -1)
				{
					if (list[num2] == "Show Items")
					{
						filterRule.enabled = (filterRule.show = true);
					}
					if (list[num2] == "Hide Items")
					{
						filterRule.enabled = true;
						filterRule.show = false;
					}
					if (list[num2] == "Ignore Rule")
					{
						filterRule.enabled = false;
					}
					if (list[num2] == "Move Up")
					{
						this.filters.RemoveAt(num);
						this.filters.Insert(num - 1, filterRule);
					}
					if (list[num2] == "Move Down")
					{
						this.filters.RemoveAt(num);
						this.filters.Insert(num + 1, filterRule);
					}
				}
			}
			this.contextUpdated = true;
		}

		// Token: 0x0600D05D RID: 53341 RVA: 0x004CE62C File Offset: 0x004CC82C
		public void UpdateContext(ObjectFinder.Context context, IEnumerable<GameObject> list)
		{
			List<GameObject> contextList = this.GetContextList(context);
			if (contextList == null)
			{
				return;
			}
			List<GameObject> list2 = contextList;
			lock (list2)
			{
				contextList.Clear();
				contextList.AddRange(list);
				this.contextUpdated = true;
			}
		}

		// Token: 0x0600D05E RID: 53342 RVA: 0x004CE684 File Offset: 0x004CC884
		public bool UpdateFilter(bool force = false)
		{
			if (this.contextUpdated || force)
			{
				this.contextUpdated = false;
				this._updateFilterAccepted.Clear();
				this._updateFilterRejected.Clear();
				foreach (KeyValuePair<ObjectFinder.Context, List<GameObject>> keyValuePair in this.contextObjects)
				{
					List<GameObject> value = keyValuePair.Value;
					ObjectFinder.Context key = keyValuePair.Key;
					List<GameObject> list = value;
					lock (list)
					{
						foreach (GameObject gameObject in value)
						{
							if (!this._updateFilterAccepted.Contains(new ValueTuple<GameObject, ObjectFinder.Context>(gameObject, key)) && !this._updateFilterRejected.Contains(new ValueTuple<GameObject, ObjectFinder.Context>(gameObject, key)))
							{
								bool? flag2 = null;
								int num = 0;
								bool? flag3;
								bool flag4;
								for (;;)
								{
									flag3 = flag2;
									flag4 = false;
									if (((flag3.GetValueOrDefault() == flag4) & (flag3 != null)) || num >= this.filters.Count)
									{
										break;
									}
									if (this.filters[num].enabled && this.filters[num].classifier.Check(gameObject, key))
									{
										flag2 = new bool?(this.filters[num].show);
									}
									num++;
								}
								flag3 = flag2;
								flag4 = true;
								if ((flag3.GetValueOrDefault() == flag4) & (flag3 != null))
								{
									this._updateFilterAccepted.Add(new ValueTuple<GameObject, ObjectFinder.Context>(gameObject, key));
								}
								else
								{
									this._updateFilterRejected.Add(new ValueTuple<GameObject, ObjectFinder.Context>(gameObject, key));
								}
							}
						}
					}
				}
				if (this._updateFilterAccepted.Count != this.filterCache.Count || !this._updateFilterAccepted.All<ValueTuple<GameObject, ObjectFinder.Context>>(new Func<ValueTuple<GameObject, ObjectFinder.Context>, bool>(this.filterCache.Contains)))
				{
					List<ValueTuple<GameObject, ObjectFinder.Context>> list2 = this.filterCache;
					this.filterCache = this._updateFilterAccepted;
					this._updateFilterAccepted = list2;
					this.filterCacheUpdated = true;
				}
				this.filterCache.Sort(this.activeSorter);
			}
			return this.filterCacheUpdated;
		}

		// Token: 0x0600D05F RID: 53343 RVA: 0x004CE908 File Offset: 0x004CCB08
		[return: TupleElementNames(new string[] { "go", "context" })]
		public IEnumerable<ValueTuple<GameObject, ObjectFinder.Context>> peekItems()
		{
			return this.readItems(true);
		}

		// Token: 0x0600D060 RID: 53344 RVA: 0x004CE911 File Offset: 0x004CCB11
		[return: TupleElementNames(new string[] { "go", "context" })]
		public IEnumerable<ValueTuple<GameObject, ObjectFinder.Context>> readItems(bool peek = false)
		{
			if (!peek)
			{
				this.filterCacheUpdated = false;
			}
			return this.filterCache;
		}

		// Token: 0x040051C4 RID: 20932
		public static ObjectFinder instance;

		// Token: 0x040051C5 RID: 20933
		private List<ObjectFinder.Context> contexts = new List<ObjectFinder.Context>();

		// Token: 0x040051C6 RID: 20934
		private Dictionary<ObjectFinder.Context, List<GameObject>> contextObjects = new Dictionary<ObjectFinder.Context, List<GameObject>>();

		// Token: 0x040051C7 RID: 20935
		private bool contextUpdated;

		// Token: 0x040051C8 RID: 20936
		[TupleElementNames(new string[] { "go", "context" })]
		private List<ValueTuple<GameObject, ObjectFinder.Context>> filterCache = new List<ValueTuple<GameObject, ObjectFinder.Context>>();

		// Token: 0x040051C9 RID: 20937
		private bool filterCacheUpdated;

		// Token: 0x040051CA RID: 20938
		private List<ObjectFinder.FilterRule> filters = new List<ObjectFinder.FilterRule>();

		// Token: 0x040051CB RID: 20939
		private ObjectFinder.Sorter activeSorter = new ValueSorter();

		// Token: 0x040051CC RID: 20940
		[TupleElementNames(new string[] { "go", "context" })]
		private List<ValueTuple<GameObject, ObjectFinder.Context>> _updateFilterAccepted = new List<ValueTuple<GameObject, ObjectFinder.Context>>();

		// Token: 0x040051CD RID: 20941
		[TupleElementNames(new string[] { "go", "context" })]
		private List<ValueTuple<GameObject, ObjectFinder.Context>> _updateFilterRejected = new List<ValueTuple<GameObject, ObjectFinder.Context>>();

		/// <summary> 
		///   Contexts are created when the object finder is open, and the context has been "enabled".
		/// </summary>
		// Token: 0x02001DCB RID: 7627
		public abstract class Context
		{
			// Token: 0x0600F54A RID: 62794 RVA: 0x0055584B File Offset: 0x00553A4B
			public virtual string GetDisplayName()
			{
				return base.GetType().Name;
			}

			// Token: 0x0600F54B RID: 62795 RVA: 0x00555858 File Offset: 0x00553A58
			public virtual IRenderable GetIcon()
			{
				return null;
			}

			// Token: 0x0600F54C RID: 62796 RVA: 0x0055585B File Offset: 0x00553A5B
			public virtual void Enable()
			{
			}

			// Token: 0x0600F54D RID: 62797 RVA: 0x0055585D File Offset: 0x00553A5D
			public virtual void Disable()
			{
			}

			// Token: 0x0600F54E RID: 62798 RVA: 0x0055585F File Offset: 0x00553A5F
			public Context()
			{
			}

			// Token: 0x0600F54F RID: 62799 RVA: 0x00555867 File Offset: 0x00553A67
			public Context(ObjectFinder finder)
			{
				this.setFinder(finder);
			}

			// Token: 0x0600F550 RID: 62800 RVA: 0x00555876 File Offset: 0x00553A76
			public void setFinder(ObjectFinder finder)
			{
				this.finder = finder;
			}

			// Token: 0x0400741E RID: 29726
			protected ObjectFinder finder;
		}

		// Token: 0x02001DCC RID: 7628
		public abstract class Classifier
		{
			// Token: 0x0600F551 RID: 62801 RVA: 0x0055587F File Offset: 0x00553A7F
			public virtual string GetDisplayName()
			{
				return base.GetType().Name;
			}

			// Token: 0x0600F552 RID: 62802 RVA: 0x0055588C File Offset: 0x00553A8C
			public virtual IRenderable GetIcon()
			{
				return null;
			}

			// Token: 0x0600F553 RID: 62803
			public abstract bool Check(GameObject go, ObjectFinder.Context context);
		}

		// Token: 0x02001DCD RID: 7629
		public class FilterRule
		{
			// Token: 0x17000FF5 RID: 4085
			// (get) Token: 0x0600F555 RID: 62805 RVA: 0x00555897 File Offset: 0x00553A97
			public string stateName
			{
				get
				{
					if (!this.enabled)
					{
						return "Disabled";
					}
					if (!this.show)
					{
						return "Hide";
					}
					return "Show";
				}
			}

			// Token: 0x17000FF6 RID: 4086
			// (get) Token: 0x0600F556 RID: 62806 RVA: 0x005558BA File Offset: 0x00553ABA
			public string stateColor
			{
				get
				{
					if (!this.enabled)
					{
						return "K";
					}
					if (!this.show)
					{
						return "R";
					}
					return "G";
				}
			}

			// Token: 0x0400741F RID: 29727
			public bool enabled = true;

			// Token: 0x04007420 RID: 29728
			public bool show = true;

			// Token: 0x04007421 RID: 29729
			public ObjectFinder.Classifier classifier;
		}

		// Token: 0x02001DCE RID: 7630
		public abstract class Sorter : IComparer<ValueTuple<GameObject, ObjectFinder.Context>>
		{
			// Token: 0x0600F558 RID: 62808 RVA: 0x005558F3 File Offset: 0x00553AF3
			public virtual string GetDisplayName()
			{
				return base.GetType().Name;
			}

			// Token: 0x0600F559 RID: 62809 RVA: 0x00555900 File Offset: 0x00553B00
			public virtual IRenderable GetIcon()
			{
				return null;
			}

			// Token: 0x0600F55A RID: 62810 RVA: 0x00555903 File Offset: 0x00553B03
			public virtual int Compare([TupleElementNames(new string[] { "go", "context" })] ValueTuple<GameObject, ObjectFinder.Context> a, [TupleElementNames(new string[] { "go", "context" })] ValueTuple<GameObject, ObjectFinder.Context> b)
			{
				return 0;
			}
		}
	}
}
