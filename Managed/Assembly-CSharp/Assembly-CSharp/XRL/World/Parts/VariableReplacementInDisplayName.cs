using System;

namespace XRL.World.Parts
{
	// Token: 0x02000F67 RID: 3943
	[Serializable]
	public class VariableReplacementInDisplayName : IPart
	{
		// Token: 0x06009D10 RID: 40208 RVA: 0x0038E000 File Offset: 0x0038C200
		public override bool AllowStaticRegistration()
		{
			return true;
		}

		// Token: 0x06009D11 RID: 40209 RVA: 0x0038E003 File Offset: 0x0038C203
		public override bool WantEvent(int ID, int cascade)
		{
			return base.WantEvent(ID, cascade) || ID == PooledEvent<GetDisplayNameEvent>.ID;
		}

		// Token: 0x06009D12 RID: 40210 RVA: 0x0038E01C File Offset: 0x0038C21C
		public override bool HandleEvent(GetDisplayNameEvent E)
		{
			foreach (string text in E.DB.Keys)
			{
				if (text.Contains("="))
				{
					string text2 = GameText.VariableReplace(text, this.ParentObject, null, false);
					if (text2 != text)
					{
						E.DB.Add(text2, E.DB[text]);
						E.DB.Remove(text);
					}
				}
			}
			return base.HandleEvent(E);
		}
	}
}
