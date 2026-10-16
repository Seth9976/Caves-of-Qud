using System;
using XRL.Rules;
using XRL.World.Capabilities;
using XRL.World.Parts;

namespace XRL.World.AI.GoalHandlers
{
	// Token: 0x02000B97 RID: 2967
	[Serializable]
	public class GraftekGraft : IPart
	{
		// Token: 0x0600766B RID: 30315 RVA: 0x002C834A File Offset: 0x002C654A
		public override bool SameAs(IPart p)
		{
			return (p as GraftekGraft).GraftType == this.GraftType && base.SameAs(p);
		}

		// Token: 0x0600766C RID: 30316 RVA: 0x002C8368 File Offset: 0x002C6568
		public override void Initialize()
		{
			base.Initialize();
			if (this.GraftType == 0)
			{
				this.GraftType = Stat.Random(1, 3);
			}
			base.StatShifter.SetStatShift("Hitpoints", Stat.Random(6, 12), true);
			if (this.GraftType == 1)
			{
				base.StatShifter.SetStatShift("Strength", Stat.Random(4, 6), true);
				base.StatShifter.SetStatShift("Toughness", Stat.Random(4, 6), true);
			}
			else if (this.GraftType == 2)
			{
				base.StatShifter.SetStatShift("Agility", Stat.Random(4, 6), true);
				base.StatShifter.SetStatShift("DV", Stat.Random(4, 8), true);
			}
			else if (this.GraftType == 3)
			{
				base.StatShifter.SetStatShift("AV", Stat.Random(2, 4), true);
				base.StatShifter.SetStatShift("HeatResistance", Stat.Random(50, 75), true);
				base.StatShifter.SetStatShift("ColdResistance", Stat.Random(50, 75), true);
			}
			this.ParentObject.RequirePart<Metal>(false);
		}

		// Token: 0x0600766D RID: 30317 RVA: 0x002C848C File Offset: 0x002C668C
		public override void Remove()
		{
			base.StatShifter.RemoveStatShifts();
			base.Remove();
		}

		// Token: 0x0600766E RID: 30318 RVA: 0x002C84A0 File Offset: 0x002C66A0
		public override bool Render(RenderEvent E)
		{
			string text = null;
			switch (this.GraftType)
			{
			case 1:
				text = "&c";
				break;
			case 2:
				text = "&c";
				break;
			case 3:
				text = "&C";
				break;
			}
			if (!text.IsNullOrEmpty())
			{
				E.ApplyColors(text, GraftekGraft.ICON_COLOR_PRIORITY);
			}
			return base.Render(E);
		}

		// Token: 0x0600766F RID: 30319 RVA: 0x002C84FD File Offset: 0x002C66FD
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID || ID == GetShortDescriptionEvent.ID || ID == GetUnknownShortDescriptionEvent.ID || ID == ReplicaCreatedEvent.ID;
		}

		// Token: 0x06007670 RID: 30320 RVA: 0x002C852B File Offset: 0x002C672B
		public override bool HandleEvent(ReplicaCreatedEvent E)
		{
			if (E.Object == this.ParentObject && Cloning.IsCloning(E.Context))
			{
				E.WantToRemove(this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007671 RID: 30321 RVA: 0x002C8558 File Offset: 0x002C6758
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			if (E.Object == this.ParentObject && !E.Reference)
			{
				switch (this.GraftType)
				{
				case 1:
					E.AddAdjective("grafted", 0);
					E.AddColor('c', 20);
					break;
				case 2:
					E.AddAdjective("cyberized", 0);
					E.AddColor('c', 20);
					break;
				case 3:
					E.AddAdjective("chromed", 0);
					E.AddColor('C', 20);
					break;
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007672 RID: 30322 RVA: 0x002C85E5 File Offset: 0x002C67E5
		public override bool HandleEvent(GetShortDescriptionEvent E)
		{
			this.AddShortDescription(E);
			return base.HandleEvent(E);
		}

		// Token: 0x06007673 RID: 30323 RVA: 0x002C85F5 File Offset: 0x002C67F5
		public override bool HandleEvent(GetUnknownShortDescriptionEvent E)
		{
			this.AddShortDescription(E);
			return base.HandleEvent(E);
		}

		// Token: 0x06007674 RID: 30324 RVA: 0x002C8608 File Offset: 0x002C6808
		public void AddShortDescription(IShortDescriptionEvent E)
		{
			switch (this.GraftType)
			{
			case 1:
				E.Postfix.Append("\nTwining metallic musculature has been grotesquely merged with =pronouns.possessive= body.");
				return;
			case 2:
				E.Postfix.Append("\nShining metallic veins pulse beneath =pronouns.possessive= skin.");
				return;
			case 3:
				E.Postfix.Append("\n=pronouns.Possessive= skin is tesselated in tiny chrome scales.");
				return;
			default:
				return;
			}
		}

		// Token: 0x04003646 RID: 13894
		public static readonly int ICON_COLOR_PRIORITY = 40;

		// Token: 0x04003647 RID: 13895
		public int GraftType;
	}
}
