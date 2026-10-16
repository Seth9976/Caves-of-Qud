using System;
using System.IO;
using HistoryKit;
using Qud.UI;
using UnityEngine.UI;
using XRL.Annals;
using XRL.UI;

// Token: 0x0200008E RID: 142
[UIView("HistoryTest", false, false, false, null, null, false, 0, false, NavCategory = "Menu", UICanvas = "HistoryTest", UICanvasHost = 1)]
public class HistoryTestView : SingletonWindowBase<HistoryTestView>
{
	// Token: 0x060004FA RID: 1274 RVA: 0x00020F1C File Offset: 0x0001F11C
	public void OnCommand(string Command)
	{
		if (Command == "Back")
		{
			UIManager.getWindow("HistoryTest").Hide();
			UIManager.showWindow("MainMenu", false);
		}
		if (Command == "Generate")
		{
			History history = QudHistoryFactory.GenerateNewSultanHistory();
			base.GetChildComponent<UnityEngine.UI.Text>("Controls/Scroll View/Viewport/Content/Output").text = history.Dump(false);
			base.GetChildComponent<ScrollRect>("Controls/Scroll View").verticalNormalizedPosition = 1f;
			File.WriteAllText("history_log.txt", base.GetChildComponent<UnityEngine.UI.Text>("Controls/Scroll View/Viewport/Content/Output").text);
		}
		Command == "FailureRedirect";
		if (Command == "TestVar")
		{
			History history2 = QudHistoryFactory.GenerateNewSultanHistory();
			HistoricEntity historicEntity = history2.CreateEntity(0L);
			historicEntity.ApplyEvent(new SetEntityProperty("organizingPrincipleType", "glassblower"), long.MinValue);
			base.GetChildComponent<UnityEngine.UI.Text>("Controls/Scroll View/Viewport/Content/Output").text = HistoricStringExpander.ExpandString("<$prof=entity.organizingPrincipleType><spice.professions.$prof.plural>", historicEntity.GetCurrentSnapshot(), history2, null, null);
		}
	}
}
