using System;
using System.Text;
using ConsoleLib.Console;
using XRL.Rules;

namespace XRL.World
{
	// Token: 0x02000743 RID: 1859
	[GameEvent(Cache = MinEvent.Cache.Pool)]
	public class GetDisplayNameEvent : PooledEvent<GetDisplayNameEvent>
	{
		// Token: 0x06004CF5 RID: 19701 RVA: 0x0017B789 File Offset: 0x00179989
		public override bool Dispatch(IEventHandler Handler)
		{
			return Handler.HandleEvent(this);
		}

		// Token: 0x06004CF6 RID: 19702 RVA: 0x0017B794 File Offset: 0x00179994
		public override void Reset()
		{
			base.Reset();
			this.Object = null;
			this.DB.Clear();
			this.Context = null;
			this.AsIfKnown = false;
			this.Single = false;
			this.NoConfusion = false;
			this.NoColor = false;
			this.ColorOnly = false;
			this.Visible = false;
			this.UsingAdjunctNoun = false;
			this.WithoutTitles = false;
			this.ForSort = false;
			this.Reference = false;
			this.IncludeImplantPrefix = false;
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06004CF7 RID: 19703 RVA: 0x0017B80D File Offset: 0x00179A0D
		// (set) Token: 0x06004CF8 RID: 19704 RVA: 0x0017B81A File Offset: 0x00179A1A
		public int Cutoff
		{
			get
			{
				return this.DB.Cutoff;
			}
			set
			{
				this.DB.Cutoff = value;
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06004CF9 RID: 19705 RVA: 0x0017B828 File Offset: 0x00179A28
		// (set) Token: 0x06004CFA RID: 19706 RVA: 0x0017B835 File Offset: 0x00179A35
		public bool BaseOnly
		{
			get
			{
				return this.DB.BaseOnly;
			}
			set
			{
				this.DB.BaseOnly = value;
			}
		}

		// Token: 0x06004CFB RID: 19707 RVA: 0x0017B844 File Offset: 0x00179A44
		public static string GetFor(GameObject Object, string Base, int Cutoff = 2147483647, string Context = null, bool AsIfKnown = false, bool Single = false, bool NoConfusion = false, bool NoColor = false, bool ColorOnly = false, bool Visible = true, bool BaseOnly = false, bool UsingAdjunctNoun = false, bool WithoutTitles = false, bool ForSort = false, bool Reference = false, bool IncludeImplantPrefix = true)
		{
			GetDisplayNameEvent getDisplayNameEvent = PooledEvent<GetDisplayNameEvent>.FromPool();
			getDisplayNameEvent.DB.Reset();
			getDisplayNameEvent.Object = Object;
			getDisplayNameEvent.Context = Context;
			getDisplayNameEvent.AsIfKnown = AsIfKnown;
			getDisplayNameEvent.Single = Single;
			getDisplayNameEvent.NoConfusion = NoConfusion;
			getDisplayNameEvent.NoColor = NoColor;
			getDisplayNameEvent.ColorOnly = ColorOnly;
			getDisplayNameEvent.Visible = Visible;
			getDisplayNameEvent.UsingAdjunctNoun = UsingAdjunctNoun;
			getDisplayNameEvent.WithoutTitles = WithoutTitles;
			getDisplayNameEvent.ForSort = ForSort;
			getDisplayNameEvent.Reference = Reference;
			getDisplayNameEvent.IncludeImplantPrefix = IncludeImplantPrefix;
			getDisplayNameEvent.AddBase(Base, 0, false);
			getDisplayNameEvent.Cutoff = Cutoff;
			getDisplayNameEvent.BaseOnly = BaseOnly;
			getDisplayNameEvent.DB.Object = Object;
			string text;
			if (ColorOnly)
			{
				if (!getDisplayNameEvent.DB.Color.IsNullOrEmpty())
				{
					text = getDisplayNameEvent.DB.Color;
				}
				text = ColorUtility.GetMainForegroundColor(getDisplayNameEvent.ProcessFor(Object, false));
			}
			else
			{
				text = getDisplayNameEvent.ProcessFor(Object, false);
			}
			getDisplayNameEvent.DB.Reset();
			PooledEvent<GetDisplayNameEvent>.ResetTo(ref getDisplayNameEvent);
			return text;
		}

		// Token: 0x06004CFC RID: 19708 RVA: 0x0017B93A File Offset: 0x00179B3A
		public static void ReplaceFor(GameObject Object, GetDisplayNameEvent E, string Base)
		{
			E.ReplacePrimaryBase(Base, 0);
			E.ProcessFor(Object, true);
		}

		// Token: 0x06004CFD RID: 19709 RVA: 0x0017B94D File Offset: 0x00179B4D
		public void Add(string desc, int order = 0)
		{
			this.DB.Add(desc, order);
		}

		// Token: 0x06004CFE RID: 19710 RVA: 0x0017B95C File Offset: 0x00179B5C
		public void AddBase(string desc, int orderAdjust = 0, bool secondary = false)
		{
			this.DB.AddBase(desc, orderAdjust, secondary);
		}

		// Token: 0x06004CFF RID: 19711 RVA: 0x0017B96C File Offset: 0x00179B6C
		public void ReplaceEntirety(string Name)
		{
			this.DB.Clear();
			this.DB.AddBase(Name, 0, false);
		}

		// Token: 0x06004D00 RID: 19712 RVA: 0x0017B987 File Offset: 0x00179B87
		public void ReplacePrimaryBase(string desc, int orderAdjust = 0)
		{
			this.DB.ReplacePrimaryBase(desc, orderAdjust);
		}

		// Token: 0x06004D01 RID: 19713 RVA: 0x0017B996 File Offset: 0x00179B96
		public string GetPrimaryBase()
		{
			return this.DB.PrimaryBase;
		}

		// Token: 0x06004D02 RID: 19714 RVA: 0x0017B9A3 File Offset: 0x00179BA3
		public void AddAdjective(string desc, int orderAdjust = 0)
		{
			this.DB.AddAdjective(desc, orderAdjust);
		}

		// Token: 0x06004D03 RID: 19715 RVA: 0x0017B9B2 File Offset: 0x00179BB2
		public void ApplySizeAdjective(string Adjective, int Priority = 10, int OrderAdjust = 0)
		{
			this.DB.ApplySizeAdjective(Adjective, Priority, OrderAdjust);
		}

		// Token: 0x06004D04 RID: 19716 RVA: 0x0017B9C2 File Offset: 0x00179BC2
		public void AddClause(string Clause, int OrderAdjust = 0)
		{
			this.DB.AddClause(Clause, OrderAdjust);
		}

		// Token: 0x06004D05 RID: 19717 RVA: 0x0017B9D1 File Offset: 0x00179BD1
		public void AddHonorific(string Honorific, int OrderAdjust = 0)
		{
			if (!this.WithoutTitles || OrderAdjust >= 20)
			{
				this.DB.AddHonorific(Honorific, OrderAdjust);
			}
		}

		// Token: 0x06004D06 RID: 19718 RVA: 0x0017B9ED File Offset: 0x00179BED
		public void AddEpithet(string Epithet, int OrderAdjust = 0)
		{
			if (!this.WithoutTitles)
			{
				this.DB.AddEpithet(Epithet, OrderAdjust);
			}
		}

		// Token: 0x06004D07 RID: 19719 RVA: 0x0017BA04 File Offset: 0x00179C04
		public void AddTitle(string Title, int OrderAdjust = 0)
		{
			if (!this.WithoutTitles)
			{
				this.DB.AddTitle(Title, OrderAdjust);
			}
		}

		// Token: 0x06004D08 RID: 19720 RVA: 0x0017BA1B File Offset: 0x00179C1B
		public void AddWithClause(string Clause)
		{
			this.DB.AddWithClause(Clause);
		}

		// Token: 0x06004D09 RID: 19721 RVA: 0x0017BA29 File Offset: 0x00179C29
		public void AddTag(string Tag, int OrderAdjust = 0)
		{
			this.DB.AddTag(Tag, OrderAdjust);
		}

		// Token: 0x06004D0A RID: 19722 RVA: 0x0017BA38 File Offset: 0x00179C38
		public void AddMark(string Mark, int OrderAdjust = 0)
		{
			this.DB.AddMark(Mark, OrderAdjust);
		}

		// Token: 0x06004D0B RID: 19723 RVA: 0x0017BA47 File Offset: 0x00179C47
		public void AddColor(string Color, int Priority = 0)
		{
			if (!this.NoColor)
			{
				this.DB.AddColor(Color, Priority);
			}
		}

		// Token: 0x06004D0C RID: 19724 RVA: 0x0017BA5E File Offset: 0x00179C5E
		public void AddColor(char Color, int Priority = 0)
		{
			if (!this.NoColor)
			{
				this.DB.AddColor(Color, Priority);
			}
		}

		// Token: 0x06004D0D RID: 19725 RVA: 0x0017BA75 File Offset: 0x00179C75
		public bool Understood()
		{
			return this.AsIfKnown || this.Object == null || this.Object.Understood();
		}

		// Token: 0x06004D0E RID: 19726 RVA: 0x0017BA94 File Offset: 0x00179C94
		public void AddMissileWeaponDamageTag(GetMissileWeaponPerformanceEvent E)
		{
			StringBuilder stringBuilder = Event.NewStringBuilder(null);
			if (E.Attributes == null || !E.Attributes.Contains("NonPenetrating"))
			{
				string text = (E.PenetrateWalls ? "m" : (E.PenetrateCreatures ? "W" : "c"));
				if (!GetDisplayNamePenetrationColorEvent.GetFor(this.Object, text).IsNullOrEmpty())
				{
					stringBuilder.Append("{{").Append(text).Append('|')
						.Append('\u001a')
						.Append("}}");
				}
				else
				{
					stringBuilder.Append('\u001a');
				}
				if (E.Attributes != null && E.Attributes.Contains("Vorpal"))
				{
					stringBuilder.Append('÷');
				}
				else
				{
					stringBuilder.Append(Math.Max(E.Penetration + RuleSettings.VISUAL_PENETRATION_BONUS, 1));
				}
			}
			if (E.DamageRoll != null || (!E.BaseDamage.IsNullOrEmpty() && E.BaseDamage != "0"))
			{
				stringBuilder.Compound("{{").Append(E.GetDamageColor()).Append('|')
					.Append('\u0003')
					.Append("}}")
					.Append((E.DamageRoll != null) ? E.DamageRoll.ToString() : E.BaseDamage);
			}
			if (stringBuilder.Length > 0)
			{
				this.AddTag(stringBuilder.ToString(), -20);
			}
		}

		// Token: 0x06004D0F RID: 19727 RVA: 0x0017BC00 File Offset: 0x00179E00
		public string ProcessFor(GameObject obj, bool NoReturn = false)
		{
			obj.HandleEvent(this);
			if (obj.HasRegisteredEvent("GetDisplayName"))
			{
				string text = this.DB.ToString();
				StringBuilder stringBuilder = Event.NewStringBuilder(null);
				StringBuilder stringBuilder2 = Event.NewStringBuilder(null);
				StringBuilder stringBuilder3 = Event.NewStringBuilder(null);
				StringBuilder stringBuilder4 = Event.NewStringBuilder(null);
				StringBuilder stringBuilder5 = Event.NewStringBuilder(null);
				stringBuilder.Append(text);
				Event @event = Event.New("GetDisplayName", 0, 0, 0);
				@event.SetParameter("Object", obj);
				@event.SetParameter("DisplayName", stringBuilder);
				@event.SetParameter("Prefix", stringBuilder2);
				@event.SetParameter("Infix", stringBuilder3);
				@event.SetParameter("Postfix", stringBuilder4);
				@event.SetParameter("PostPostfix", stringBuilder5);
				@event.SetParameter("Cutoff", this.Cutoff);
				@event.SetParameter("Context", this.Context);
				@event.SetFlag("BaseOnly", this.BaseOnly);
				@event.SetFlag("AsIfKnown", this.AsIfKnown);
				@event.SetFlag("Single", this.Single);
				@event.SetFlag("NoConfusion", this.NoConfusion);
				@event.SetFlag("NoColor", this.NoColor);
				@event.SetFlag("ColorOnly", this.ColorOnly);
				@event.SetFlag("Visible", this.Visible);
				@event.SetFlag("UsingAdjunctNoun", this.UsingAdjunctNoun);
				@event.SetFlag("WithoutTitles", this.WithoutTitles);
				@event.SetFlag("ForSort", this.ForSort);
				@event.SetFlag("Reference", this.Reference);
				obj.FireEvent(@event);
				if (stringBuilder.Length != text.Length || stringBuilder.ToString() != text)
				{
					this.DB.Clear();
					this.DB.AddBase(stringBuilder.ToString(), 0, false);
				}
				if (!this.BaseOnly)
				{
					if (stringBuilder2.Length != 0)
					{
						this.AddAdjective(stringBuilder2.ToString().Trim(), 0);
					}
					if (stringBuilder3.Length != 0)
					{
						this.AddClause(stringBuilder3.ToString().Trim(), 0);
					}
					if (stringBuilder4.Length != 0)
					{
						this.AddTag(stringBuilder4.ToString().Trim(), 0);
					}
					if (stringBuilder5.Length != 0)
					{
						this.AddTag(stringBuilder5.ToString().Trim(), 20);
					}
				}
			}
			if (!NoReturn)
			{
				return Markup.Wrap(this.DB.ToString());
			}
			return null;
		}

		// Token: 0x04002543 RID: 9539
		public GameObject Object;

		// Token: 0x04002544 RID: 9540
		public DescriptionBuilder DB = new DescriptionBuilder();

		// Token: 0x04002545 RID: 9541
		public string Context;

		// Token: 0x04002546 RID: 9542
		public bool AsIfKnown;

		// Token: 0x04002547 RID: 9543
		public bool Single;

		// Token: 0x04002548 RID: 9544
		public bool NoConfusion;

		// Token: 0x04002549 RID: 9545
		public bool NoColor;

		// Token: 0x0400254A RID: 9546
		public bool ColorOnly;

		// Token: 0x0400254B RID: 9547
		public bool Visible;

		// Token: 0x0400254C RID: 9548
		public bool UsingAdjunctNoun;

		// Token: 0x0400254D RID: 9549
		public bool WithoutTitles;

		// Token: 0x0400254E RID: 9550
		public bool ForSort;

		// Token: 0x0400254F RID: 9551
		public bool Reference;

		// Token: 0x04002550 RID: 9552
		public bool IncludeImplantPrefix;
	}
}
