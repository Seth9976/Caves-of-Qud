using System;
using XRL.UI;
using XRL.World.Capabilities;

namespace XRL.World.Parts
{
	// Token: 0x0200122E RID: 4654
	[Serializable]
	public class Digging : IPart
	{
		// Token: 0x0600B8D0 RID: 47312 RVA: 0x00436568 File Offset: 0x00434768
		public override void Initialize()
		{
			this.ActivatedAbilityID = base.AddMyActivatedAbility("Dig", Digging.COMMAND_NAME, "Maneuvers", null, "â", null, false, false, false, false, false, false, false, false, true, true, false, -1, null, null, null, null, null, null);
			base.Initialize();
		}

		// Token: 0x0600B8D1 RID: 47313 RVA: 0x004365AF File Offset: 0x004347AF
		public override void Remove()
		{
			base.RemoveMyActivatedAbility(ref this.ActivatedAbilityID, null);
			base.Remove();
		}

		// Token: 0x0600B8D2 RID: 47314 RVA: 0x004365C5 File Offset: 0x004347C5
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<CommandEvent>.ID || ID == GetMovementCapabilitiesEvent.ID || ID == PooledEvent<NeedPartSupportEvent>.ID || ID == PooledEvent<ShouldAttackToReachTargetEvent>.ID;
		}

		// Token: 0x0600B8D3 RID: 47315 RVA: 0x004365F3 File Offset: 0x004347F3
		public override bool HandleEvent(GetMovementCapabilitiesEvent E)
		{
			E.Add("Dig To Square", Digging.COMMAND_NAME, 1500, base.MyActivatedAbility(this.ActivatedAbilityID, null), false);
			return base.HandleEvent(E);
		}

		// Token: 0x0600B8D4 RID: 47316 RVA: 0x00436620 File Offset: 0x00434820
		public override bool HandleEvent(CommandEvent E)
		{
			if (E.Command == Digging.COMMAND_NAME)
			{
				if (this.ParentObject.OnWorldMap())
				{
					return this.ParentObject.Fail("You cannot dig on the world map.");
				}
				Cell currentCell = this.ParentObject.CurrentCell;
				Cell cell = base.PickDestinationCell(9999999, AllowVis.Any, false, false, false, true, PickTarget.PickStyle.EmptyCell, "Dig to where?", false, null);
				if (cell != null && currentCell != null)
				{
					AutoAct.Setting = string.Concat(new string[]
					{
						"d",
						currentCell.X.ToString(),
						",",
						currentCell.Y.ToString(),
						",",
						cell.X.ToString(),
						",",
						cell.Y.ToString()
					});
					this.ParentObject.ForfeitTurn(false);
				}
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B8D5 RID: 47317 RVA: 0x00436706 File Offset: 0x00434906
		public override bool HandleEvent(NeedPartSupportEvent E)
		{
			if (E.Type == "Digging" && !PartSupportEvent.Check(E, this))
			{
				this.ParentObject.RemovePart(this);
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B8D6 RID: 47318 RVA: 0x00436738 File Offset: 0x00434938
		public override bool HandleEvent(ShouldAttackToReachTargetEvent E)
		{
			if (!E.ShouldAttack && E.Actor == this.ParentObject && E.Object.IsDiggable() && OkayToDamageEvent.Check(E.Object, E.Actor))
			{
				E.ShouldAttack = true;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x0600B8D7 RID: 47319 RVA: 0x00436789 File Offset: 0x00434989
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x04004A66 RID: 19046
		public const string SUPPORT_TYPE = "Digging";

		// Token: 0x04004A67 RID: 19047
		public static readonly string COMMAND_NAME = "CommandDig";

		// Token: 0x04004A68 RID: 19048
		public Guid ActivatedAbilityID = Guid.Empty;
	}
}
