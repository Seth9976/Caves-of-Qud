using System;
using System.Collections.Generic;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x02000F5C RID: 3932
	[Serializable]
	public class Honorifics : IPart
	{
		// Token: 0x06009CC1 RID: 40129 RVA: 0x0038CD51 File Offset: 0x0038AF51
		public Honorifics()
		{
		}

		// Token: 0x06009CC2 RID: 40130 RVA: 0x0038CD59 File Offset: 0x0038AF59
		public Honorifics(string List, string Order = null)
			: this()
		{
			this.HonorificList = List;
			this.HonorificOrder = Order;
		}

		// Token: 0x17000ABE RID: 2750
		// (set) Token: 0x06009CC3 RID: 40131 RVA: 0x0038CD6F File Offset: 0x0038AF6F
		public string Primary
		{
			set
			{
				this.AddHonorific(value, 40);
			}
		}

		// Token: 0x17000ABF RID: 2751
		// (set) Token: 0x06009CC4 RID: 40132 RVA: 0x0038CD7A File Offset: 0x0038AF7A
		public string Ordinary
		{
			set
			{
				this.AddHonorific(value, 0);
			}
		}

		// Token: 0x06009CC5 RID: 40133 RVA: 0x0038CD84 File Offset: 0x0038AF84
		public override bool SameAs(IPart p)
		{
			Honorifics honorifics = p as Honorifics;
			return !(honorifics.HonorificList != this.HonorificList) && !(honorifics.HonorificOrder != this.HonorificOrder) && base.SameAs(p);
		}

		// Token: 0x06009CC6 RID: 40134 RVA: 0x0038CDC9 File Offset: 0x0038AFC9
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == ReplicaCreatedEvent.ID;
		}

		// Token: 0x06009CC7 RID: 40135 RVA: 0x0038CDE7 File Offset: 0x0038AFE7
		public override bool HandleEvent(ReplicaCreatedEvent E)
		{
			if (E.Object == this.ParentObject && Cloning.IsCloning(E.Context))
			{
				E.WantToRemove(this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06009CC8 RID: 40136 RVA: 0x0038CE14 File Offset: 0x0038B014
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (!this.HonorificList.IsNullOrEmpty() && E.Understood())
			{
				if (this.HonorificOrder.IsNullOrEmpty())
				{
					if (E.WithoutTitles)
					{
						goto IL_00F1;
					}
					using (List<string>.Enumerator enumerator = this.HonorificList.CachedDoubleSemicolonExpansion().GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string text = enumerator.Current;
							if (text.Contains("="))
							{
								text = GameText.VariableReplace(text, this.ParentObject, null, E.NoColor);
							}
							E.AddHonorific(text, 0);
						}
						goto IL_00F1;
					}
				}
				Dictionary<string, int> dictionary = this.HonorificOrder.CachedNumericDictionaryExpansion();
				foreach (string text2 in this.HonorificList.CachedDoubleSemicolonExpansion())
				{
					int num;
					dictionary.TryGetValue(text2, out num);
					if (!E.WithoutTitles || num >= 20)
					{
						E.AddHonorific(text2, num);
					}
				}
			}
			IL_00F1:
			return base.HandleEvent(E);
		}

		// Token: 0x06009CC9 RID: 40137 RVA: 0x0038CF38 File Offset: 0x0038B138
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009CCA RID: 40138 RVA: 0x0038CF3C File Offset: 0x0038B13C
		public void AddHonorific(string Honorific, int Order = 0)
		{
			if (Honorific.Contains(";;"))
			{
				MetricsManager.LogError("Cannot track honorific containing double semicolon, had " + Honorific);
				return;
			}
			if (this.HonorificList.IsNullOrEmpty())
			{
				this.HonorificList = Honorific;
			}
			else
			{
				if (this.HonorificList.HasDelimitedSubstring(";;", Honorific, StringComparison.Ordinal))
				{
					MetricsManager.LogError("Already have honorific " + Honorific);
					return;
				}
				this.HonorificList = this.HonorificList + ";;" + Honorific;
			}
			if (Order != 0)
			{
				if (Honorific.Contains("::"))
				{
					MetricsManager.LogError("Cannot track order for honorific containing double colon, had " + Honorific);
					return;
				}
				if (this.HonorificOrder.IsNullOrEmpty())
				{
					this.HonorificOrder = Honorific + "::" + Order.ToString();
					return;
				}
				this.HonorificOrder = string.Concat(new string[]
				{
					this.HonorificOrder,
					";;",
					Honorific,
					"::",
					Order.ToString()
				});
			}
		}

		// Token: 0x040040D2 RID: 16594
		public string HonorificList;

		// Token: 0x040040D3 RID: 16595
		public string HonorificOrder;
	}
}
