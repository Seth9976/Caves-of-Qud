using System;
using System.Collections.Generic;
using Qud.UI;
using XRL;
using XRL.CharacterBuilds;
using XRL.UI;
using XRL.UI.Framework;
using XRL.World;

// Token: 0x02000075 RID: 117
public abstract class TutorialStep
{
	// Token: 0x06000462 RID: 1122 RVA: 0x0001DAD3 File Offset: 0x0001BCD3
	public bool ConstrainToCurrentZone(Cell nextCell)
	{
		Zone zone = ((nextCell != null) ? nextCell.ParentZone : null);
		Cell playerCell = The.PlayerCell;
		if (zone != ((playerCell != null) ? playerCell.ParentZone : null))
		{
			Popup.Show("We're not quite ready to leave yet.", null, "Sounds/UI/ui_notification", true, true, true, true, null);
			return false;
		}
		return true;
	}

	// Token: 0x17000079 RID: 121
	// (get) Token: 0x06000463 RID: 1123 RVA: 0x0001DB0C File Offset: 0x0001BD0C
	protected string CurrentGameView
	{
		get
		{
			return GameManager.Instance.CurrentGameView;
		}
	}

	// Token: 0x06000464 RID: 1124 RVA: 0x0001DB18 File Offset: 0x0001BD18
	public virtual bool StartingLineTooltip(GameObject go, GameObject compareGo)
	{
		return true;
	}

	// Token: 0x06000465 RID: 1125 RVA: 0x0001DB1C File Offset: 0x0001BD1C
	public virtual void BeforeRenderEvent()
	{
		GameObject player = The.Player;
		Zone zone = ((player != null) ? player.CurrentZone : null);
		GameObject player2 = The.Player;
		Cell cell = ((player2 != null) ? player2.CurrentCell : null);
		if (zone != null && cell != null && zone.Z > 10)
		{
			zone.AddLight(cell.X, cell.Y, 5);
		}
	}

	// Token: 0x06000466 RID: 1126 RVA: 0x0001DB70 File Offset: 0x0001BD70
	public virtual int AdjustExaminationRoll(int result)
	{
		return result;
	}

	// Token: 0x06000467 RID: 1127 RVA: 0x0001DB73 File Offset: 0x0001BD73
	public virtual bool AllowOverlandEncounters()
	{
		return false;
	}

	// Token: 0x06000468 RID: 1128 RVA: 0x0001DB76 File Offset: 0x0001BD76
	public virtual bool OnTradeOffer()
	{
		return true;
	}

	// Token: 0x06000469 RID: 1129 RVA: 0x0001DB79 File Offset: 0x0001BD79
	public virtual bool OnTradeComplete()
	{
		return true;
	}

	// Token: 0x0600046A RID: 1130 RVA: 0x0001DB7C File Offset: 0x0001BD7C
	public virtual bool AllowTooltipUpdate()
	{
		return true;
	}

	// Token: 0x0600046B RID: 1131 RVA: 0x0001DB7F File Offset: 0x0001BD7F
	public virtual void GameSync()
	{
	}

	// Token: 0x0600046C RID: 1132 RVA: 0x0001DB84 File Offset: 0x0001BD84
	public virtual bool IsEditableChargenPanel(string id)
	{
		return id == "Pregens" || id == "Genotypes" || id == "Game Modes" || id == "Summary" || id == "Customize" || id == "Starting Location";
	}

	// Token: 0x0600046D RID: 1133 RVA: 0x0001DBDF File Offset: 0x0001BDDF
	public virtual bool AllowSelectPregen(string id)
	{
		return id == "Marsh Taur";
	}

	// Token: 0x0600046E RID: 1134 RVA: 0x0001DBEC File Offset: 0x0001BDEC
	public virtual bool AllowSelectGenotype(string id)
	{
		return id == "Mutated Human";
	}

	// Token: 0x0600046F RID: 1135 RVA: 0x0001DBF9 File Offset: 0x0001BDF9
	public virtual bool AllowTargetPick(GameObject go, Type ability, List<Cell> target)
	{
		return true;
	}

	// Token: 0x06000470 RID: 1136 RVA: 0x0001DBFC File Offset: 0x0001BDFC
	public virtual bool AllowSelectedPopupCommand(string id, QudMenuItem command)
	{
		return true;
	}

	// Token: 0x06000471 RID: 1137 RVA: 0x0001DBFF File Offset: 0x0001BDFF
	public virtual bool AllowInventoryInteract(GameObject go)
	{
		return true;
	}

	// Token: 0x06000472 RID: 1138 RVA: 0x0001DC02 File Offset: 0x0001BE02
	public virtual bool AllowOnSelected(FrameworkDataElement element)
	{
		return true;
	}

	// Token: 0x06000473 RID: 1139 RVA: 0x0001DC05 File Offset: 0x0001BE05
	public virtual bool AllowCommand(string id)
	{
		return true;
	}

	// Token: 0x06000474 RID: 1140 RVA: 0x0001DC08 File Offset: 0x0001BE08
	public virtual void LateUpdate()
	{
	}

	// Token: 0x06000475 RID: 1141 RVA: 0x0001DC0A File Offset: 0x0001BE0A
	public virtual void OnBootGame(XRLGame game, EmbarkInfo info)
	{
	}

	// Token: 0x06000476 RID: 1142 RVA: 0x0001DC0C File Offset: 0x0001BE0C
	public virtual void OnTrigger(string trigger)
	{
	}

	// Token: 0x06000477 RID: 1143 RVA: 0x0001DC0E File Offset: 0x0001BE0E
	public virtual bool BeforePlayerEnterCell(Cell cell)
	{
		return true;
	}

	// Token: 0x0400035E RID: 862
	public int step;

	// Token: 0x0400035F RID: 863
	public TutorialManager manager;
}
