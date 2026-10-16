using System;
using System.Collections.Generic;
using XRL.World;

namespace XRL.UI
{
	// Token: 0x02001490 RID: 5264
	public class CyberneticsScreenSelectSubject : CyberneticsScreen
	{
		// Token: 0x0600CEEB RID: 52971 RVA: 0x004B5194 File Offset: 0x004B3394
		protected override void OnUpdate()
		{
			this.MainText = "Select who is to Become, aristocrat.";
			CyberneticsScreenSelectSubject.Subjects.Clear();
			base.Terminal.GetAuthorizedSubjects(CyberneticsScreenSelectSubject.Subjects);
			this.ClearOptions();
			foreach (GameObject gameObject in CyberneticsScreenSelectSubject.Subjects)
			{
				this.Options.Add(gameObject.GetReferenceDisplayName(int.MaxValue, null, null, false, false, false, false, false, false, false, false, null, false));
			}
			this.Options.Add("Return to main menu");
		}

		// Token: 0x0600CEEC RID: 52972 RVA: 0x004B523C File Offset: 0x004B343C
		public override void Back()
		{
			base.Terminal.CurrentScreen = new CyberneticsScreenMainMenu();
		}

		// Token: 0x0600CEED RID: 52973 RVA: 0x004B5250 File Offset: 0x004B3450
		public override void Activate()
		{
			if (base.Terminal.Selected < CyberneticsScreenSelectSubject.Subjects.Count)
			{
				base.Terminal.Subject = CyberneticsScreenSelectSubject.Subjects[base.Terminal.Selected];
			}
			base.Terminal.CheckSecurity(20, new CyberneticsScreenMainMenu(), 1);
		}

		// Token: 0x0400505A RID: 20570
		private static List<GameObject> Subjects = new List<GameObject>();
	}
}
