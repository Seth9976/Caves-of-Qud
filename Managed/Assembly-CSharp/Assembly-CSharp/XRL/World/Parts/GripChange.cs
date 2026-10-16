using System;
using System.Collections.Generic;
using ConsoleLib.Console;
using XRL.UI;
using XRL.World.Skills;

namespace XRL.World.Parts
{
	// Token: 0x020010E1 RID: 4321
	[Serializable]
	public class GripChange : IPart
	{
		// Token: 0x0600AB7E RID: 43902 RVA: 0x003EC491 File Offset: 0x003EA691
		public override bool WantEvent(int ID, int Cascade)
		{
			return ID == GetInventoryActionsEvent.ID || ID == InventoryActionEvent.ID || ID == EquippedEvent.ID || ID == UnequippedEvent.ID || ID == PooledEvent<CommandEvent>.ID;
		}

		// Token: 0x0600AB7F RID: 43903 RVA: 0x003EC4C0 File Offset: 0x003EA6C0
		public override bool HandleEvent(GetInventoryActionsEvent E)
		{
			E.AddAction("CommandChangeGrip", "change grip", "CommandChangeGrip", null, 'c', false, 0, 0, false, false, false, false, true, null, false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AB80 RID: 43904 RVA: 0x003EC4F7 File Offset: 0x003EA6F7
		public override bool HandleEvent(InventoryActionEvent E)
		{
			if (E.Command == "CommandChangeGrip" && this.TryChooseGrip(E.Actor))
			{
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AB81 RID: 43905 RVA: 0x003EC528 File Offset: 0x003EA728
		public override bool HandleEvent(EquippedEvent E)
		{
			if (this.AbilityID == Guid.Empty && this.ParentObject.IsEquippedProperly(E.Part))
			{
				Renderable renderable = null;
				MeleeWeapon meleeWeapon;
				IBaseSkillEntry baseSkillEntry;
				if (this.ParentObject.TryGetPart<MeleeWeapon>(out meleeWeapon) && SkillFactory.Factory.TryGetFirstEntry(meleeWeapon.Skill, out baseSkillEntry))
				{
					renderable = new Renderable(baseSkillEntry.Tile, " ", "&" + baseSkillEntry.Foreground, null, baseSkillEntry.Detail[0]);
				}
				this.AbilityID = E.Actor.AddActivatedAbility("Change Grip", "CommandChangeGrip", "Items", null, "é", null, false, false, false, false, false, false, false, false, true, true, false, false, -1, null, renderable, null, null, null);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AB82 RID: 43906 RVA: 0x003EC5F3 File Offset: 0x003EA7F3
		public override bool HandleEvent(UnequippedEvent E)
		{
			E.Actor.RemoveActivatedAbility(ref this.AbilityID);
			return base.HandleEvent(E);
		}

		// Token: 0x0600AB83 RID: 43907 RVA: 0x003EC60E File Offset: 0x003EA80E
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == "CommandChangeGrip" && this.TryChooseGrip(E.Actor))
			{
				E.RequestInterfaceExit();
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600AB84 RID: 43908 RVA: 0x003EC640 File Offset: 0x003EA840
		public bool TryChooseGrip(GameObject Actor)
		{
			if (!Actor.IsPlayer())
			{
				return false;
			}
			List<string> list;
			if ((list = this.Options) == null)
			{
				list = (this.Options = new List<string>());
			}
			List<string> list2 = list;
			List<char> list3;
			if ((list3 = this.Hotkeys) == null)
			{
				list3 = (this.Hotkeys = new List<char>());
			}
			List<char> list4 = list3;
			List<IRenderable> list5;
			if ((list5 = this.Icons) == null)
			{
				list5 = (this.Icons = new List<IRenderable>());
			}
			List<IRenderable> list6 = list5;
			list2.Clear();
			list4.Clear();
			list6.Clear();
			MeleeWeapon part = this.ParentObject.GetPart<MeleeWeapon>();
			if (part == null)
			{
				return false;
			}
			int num = Array.IndexOf<string>(GripChange.Skills, part.Skill);
			int i = 0;
			int num2 = GripChange.Skills.Length;
			while (i < num2)
			{
				IBaseSkillEntry firstEntry = SkillFactory.Factory.GetFirstEntry(GripChange.Skills[i], null);
				list2.Add((i == num) ? ("{{W|" + firstEntry.Name + "}}") : firstEntry.Name);
				list4.Add(ControlManager.GetHotkeyCharFor(firstEntry.Name, list4));
				list6.Add(new Renderable(firstEntry.Tile, " ", "&" + firstEntry.Foreground, null, firstEntry.Detail[0]));
				i++;
			}
			int num3 = Popup.PickOption("Choose a style", null, "", "Sounds/UI/ui_notification", list2, list4, list6, null, null, null, null, 0, 60, num, -1, true, false, false, true, false, null, null);
			if (num3 < 0 || num3 >= GripChange.Skills.Length)
			{
				return false;
			}
			if (num3 != num)
			{
				part.Skill = GripChange.Skills[num3];
				GameObject equipped = this.ParentObject.Equipped;
				if (equipped != null)
				{
					this.SetAbilityIcon(equipped, (Renderable)list6[num3]);
					equipped.PlayWorldSound(GripChange.EquipSounds[num3], 0.5f, 0f, false, 0f, 1f, 1f, int.MaxValue);
				}
			}
			return true;
		}

		// Token: 0x0600AB85 RID: 43909 RVA: 0x003EC82C File Offset: 0x003EAA2C
		public void SetAbilityIcon(GameObject Actor, Renderable Renderable)
		{
			ActivatedAbilityEntry activatedAbility = Actor.GetActivatedAbility(this.AbilityID);
			if (activatedAbility == null)
			{
				return;
			}
			activatedAbility.UITileDefault = Renderable;
		}

		// Token: 0x040046BF RID: 18111
		public const string ABL_CMD = "CommandChangeGrip";

		// Token: 0x040046C0 RID: 18112
		public Guid AbilityID;

		// Token: 0x040046C1 RID: 18113
		private static readonly string[] Skills = new string[] { "Axe", "Cudgel", "LongBlades", "ShortBlades" };

		// Token: 0x040046C2 RID: 18114
		private static readonly string[] EquipSounds = new string[] { "sfx_equip_weapon_axe_metal_heavy", "sfx_equip_weapon_bludgeon_metal_heavy", "sfx_equip_weapon_blade_long_metal_heavy", "sfx_equip_weapon_blade_short_metal_heavy" };

		// Token: 0x040046C3 RID: 18115
		[NonSerialized]
		private List<string> Options;

		// Token: 0x040046C4 RID: 18116
		[NonSerialized]
		private List<char> Hotkeys;

		// Token: 0x040046C5 RID: 18117
		[NonSerialized]
		private List<IRenderable> Icons;
	}
}
