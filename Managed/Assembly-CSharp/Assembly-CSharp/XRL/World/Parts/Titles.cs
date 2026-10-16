using System;
using System.Collections.Generic;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x02000F66 RID: 3942
	[Serializable]
	public class Titles : IPart
	{
		// Token: 0x06009D06 RID: 40198 RVA: 0x0038DD26 File Offset: 0x0038BF26
		public Titles()
		{
		}

		// Token: 0x06009D07 RID: 40199 RVA: 0x0038DD2E File Offset: 0x0038BF2E
		public Titles(string List, string Order = null)
			: this()
		{
			this.TitleList = List;
			this.TitleOrder = Order;
		}

		// Token: 0x17000AC2 RID: 2754
		// (set) Token: 0x06009D08 RID: 40200 RVA: 0x0038DD44 File Offset: 0x0038BF44
		public string Primary
		{
			set
			{
				this.AddTitle(value, -40);
			}
		}

		// Token: 0x17000AC3 RID: 2755
		// (set) Token: 0x06009D09 RID: 40201 RVA: 0x0038DD4F File Offset: 0x0038BF4F
		public string Ordinary
		{
			set
			{
				this.AddTitle(value, 0);
			}
		}

		// Token: 0x06009D0A RID: 40202 RVA: 0x0038DD5C File Offset: 0x0038BF5C
		public override bool SameAs(IPart p)
		{
			Titles titles = p as Titles;
			return !(titles.TitleList != this.TitleList) && !(titles.TitleOrder != this.TitleOrder) && base.SameAs(p);
		}

		// Token: 0x06009D0B RID: 40203 RVA: 0x0038DDA1 File Offset: 0x0038BFA1
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == ReplicaCreatedEvent.ID;
		}

		// Token: 0x06009D0C RID: 40204 RVA: 0x0038DDBF File Offset: 0x0038BFBF
		public override bool HandleEvent(ReplicaCreatedEvent E)
		{
			if (E.Object == this.ParentObject && Cloning.IsCloning(E.Context))
			{
				E.WantToRemove(this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009D0D RID: 40205 RVA: 0x0038DDEC File Offset: 0x0038BFEC
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!E.WithoutTitles && E.Understood() && !this.TitleList.IsNullOrEmpty())
			{
				if (this.TitleOrder.IsNullOrEmpty())
				{
					using (List<string>.Enumerator enumerator = this.TitleList.CachedDoubleSemicolonExpansion().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string text = enumerator.Current;
							if (text.Contains("="))
							{
								text = GameText.VariableReplace(text, this.ParentObject, null, E.NoColor);
							}
							E.AddTitle(text, 0);
						}
						goto IL_00E3;
					}
				}
				Dictionary<string, int> dictionary = this.TitleOrder.CachedNumericDictionaryExpansion();
				foreach (string text2 in this.TitleList.CachedDoubleSemicolonExpansion())
				{
					int num;
					dictionary.TryGetValue(text2, out num);
					E.AddTitle(text2, num);
				}
			}
			IL_00E3:
			return base.HandleEvent(E);
		}

		// Token: 0x06009D0E RID: 40206 RVA: 0x0038DF00 File Offset: 0x0038C100
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009D0F RID: 40207 RVA: 0x0038DF04 File Offset: 0x0038C104
		public void AddTitle(string Title, int Order = 0)
		{
			if (Title.Contains(";;"))
			{
				MetricsManager.LogError("Cannot track title containing double semicolon, had " + Title);
				return;
			}
			if (this.TitleList.IsNullOrEmpty())
			{
				this.TitleList = Title;
			}
			else
			{
				if (this.TitleList.HasDelimitedSubstring(";;", Title, StringComparison.Ordinal))
				{
					MetricsManager.LogError("Already have title " + Title);
					return;
				}
				this.TitleList = this.TitleList + ";;" + Title;
			}
			if (Order != 0)
			{
				if (Title.Contains("::"))
				{
					MetricsManager.LogError("Cannot track order for title containing double colon, had " + Title);
					return;
				}
				if (this.TitleOrder.IsNullOrEmpty())
				{
					this.TitleOrder = Title + "::" + Order.ToString();
					return;
				}
				this.TitleOrder = string.Concat(new string[]
				{
					this.TitleOrder,
					";;",
					Title,
					"::",
					Order.ToString()
				});
			}
		}

		// Token: 0x040040E9 RID: 16617
		public string TitleList;

		// Token: 0x040040EA RID: 16618
		public string TitleOrder;
	}
}
