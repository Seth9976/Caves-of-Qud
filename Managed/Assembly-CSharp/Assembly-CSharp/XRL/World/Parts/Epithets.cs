using System;
using System.Collections.Generic;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x02000F59 RID: 3929
	[Serializable]
	public class Epithets : IPart
	{
		// Token: 0x06009CAB RID: 40107 RVA: 0x0038C918 File Offset: 0x0038AB18
		public Epithets()
		{
		}

		// Token: 0x06009CAC RID: 40108 RVA: 0x0038C920 File Offset: 0x0038AB20
		public Epithets(string List, string Order = null)
			: this()
		{
			this.EpithetList = List;
			this.EpithetOrder = Order;
		}

		// Token: 0x17000ABC RID: 2748
		// (set) Token: 0x06009CAD RID: 40109 RVA: 0x0038C936 File Offset: 0x0038AB36
		public string Primary
		{
			set
			{
				this.AddEpithet(value, -40);
			}
		}

		// Token: 0x17000ABD RID: 2749
		// (set) Token: 0x06009CAE RID: 40110 RVA: 0x0038C941 File Offset: 0x0038AB41
		public string Ordinary
		{
			set
			{
				this.AddEpithet(value, 0);
			}
		}

		// Token: 0x06009CAF RID: 40111 RVA: 0x0038C94C File Offset: 0x0038AB4C
		public override bool SameAs(IPart Part)
		{
			Epithets epithets = Part as Epithets;
			return !(epithets.EpithetList != this.EpithetList) && !(epithets.EpithetOrder != this.EpithetOrder) && base.SameAs(Part);
		}

		// Token: 0x06009CB0 RID: 40112 RVA: 0x0038C991 File Offset: 0x0038AB91
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == ReplicaCreatedEvent.ID;
		}

		// Token: 0x06009CB1 RID: 40113 RVA: 0x0038C9AF File Offset: 0x0038ABAF
		public override bool HandleEvent(ReplicaCreatedEvent E)
		{
			if (E.Object == this.ParentObject && Cloning.IsCloning(E.Context))
			{
				E.WantToRemove(this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009CB2 RID: 40114 RVA: 0x0038C9DC File Offset: 0x0038ABDC
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!E.WithoutTitles && E.Understood() && !this.EpithetList.IsNullOrEmpty())
			{
				if (this.EpithetOrder.IsNullOrEmpty())
				{
					using (List<string>.Enumerator enumerator = this.EpithetList.CachedDoubleSemicolonExpansion().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string text = enumerator.Current;
							if (text.Contains("="))
							{
								text = GameText.VariableReplace(text, this.ParentObject, null, E.NoColor);
							}
							E.AddEpithet(text, 0);
						}
						goto IL_00E3;
					}
				}
				Dictionary<string, int> dictionary = this.EpithetOrder.CachedNumericDictionaryExpansion();
				foreach (string text2 in this.EpithetList.CachedDoubleSemicolonExpansion())
				{
					int num;
					dictionary.TryGetValue(text2, out num);
					E.AddEpithet(text2, num);
				}
			}
			IL_00E3:
			return base.HandleEvent(E);
		}

		// Token: 0x06009CB3 RID: 40115 RVA: 0x0038CAF0 File Offset: 0x0038ACF0
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009CB4 RID: 40116 RVA: 0x0038CAF4 File Offset: 0x0038ACF4
		public void AddEpithet(string Epithet, int Order = 0)
		{
			if (Epithet.Contains(";;"))
			{
				MetricsManager.LogError("Cannot track epithet containing double semicolon, had " + Epithet);
				return;
			}
			if (this.EpithetList.IsNullOrEmpty())
			{
				this.EpithetList = Epithet;
			}
			else
			{
				if (this.EpithetList.HasDelimitedSubstring(";;", Epithet, StringComparison.Ordinal))
				{
					MetricsManager.LogError("Already have epithet " + Epithet);
					return;
				}
				this.EpithetList = this.EpithetList + ";;" + Epithet;
			}
			if (Order != 0)
			{
				if (Epithet.Contains(";;"))
				{
					MetricsManager.LogError("Cannot track order for epithet containing double semicolon, had " + Epithet);
					return;
				}
				if (this.EpithetOrder.IsNullOrEmpty())
				{
					this.EpithetOrder = Epithet + "::" + Order.ToString();
					return;
				}
				this.EpithetOrder = string.Concat(new string[]
				{
					this.EpithetOrder,
					";;",
					Epithet,
					"::",
					Order.ToString()
				});
			}
		}

		// Token: 0x040040CB RID: 16587
		public string EpithetList;

		// Token: 0x040040CC RID: 16588
		public string EpithetOrder;
	}
}
