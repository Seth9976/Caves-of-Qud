using System;
using XRL.UI;

namespace XRL.World.Conversations.Parts
{
	// Token: 0x02000C19 RID: 3097
	public class TakeLiquid : IConversationPart
	{
		// Token: 0x06007C00 RID: 31744 RVA: 0x002E9898 File Offset: 0x002E7A98
		public TakeLiquid()
		{
			this.Priority = -1000;
		}

		// Token: 0x06007C01 RID: 31745 RVA: 0x002E98BD File Offset: 0x002E7ABD
		public override void LoadText(string Text)
		{
			this.Message = Text;
		}

		// Token: 0x06007C02 RID: 31746 RVA: 0x002E98C6 File Offset: 0x002E7AC6
		public TakeLiquid(string Liquids)
		{
			this.Liquids = Liquids;
		}

		// Token: 0x06007C03 RID: 31747 RVA: 0x002E98E7 File Offset: 0x002E7AE7
		public override bool WantEvent(int ID, int Propagation)
		{
			return base.WantEvent(ID, Propagation) || (this.Require && ID == EnterElementEvent.ID) || ID == EnteredElementEvent.ID;
		}

		// Token: 0x06007C04 RID: 31748 RVA: 0x002E990D File Offset: 0x002E7B0D
		public override bool HandleEvent(EnterElementEvent E)
		{
			if (!this.Execute(false))
			{
				if (!this.Message.IsNullOrEmpty())
				{
					Popup.Show(this.Message, null, "Sounds/UI/ui_notification", true, true, true, true, null);
				}
				return false;
			}
			return base.HandleEvent(E);
		}

		// Token: 0x06007C05 RID: 31749 RVA: 0x002E9944 File Offset: 0x002E7B44
		public override bool HandleEvent(EnteredElementEvent E)
		{
			this.Execute(true);
			return base.HandleEvent(E);
		}

		// Token: 0x06007C06 RID: 31750 RVA: 0x002E9958 File Offset: 0x002E7B58
		public bool Execute(bool Consume = false)
		{
			string liquids = this.Liquids;
			string[] array = ((liquids != null) ? liquids.Split(',', StringSplitOptions.None) : null) ?? new string[0];
			int[] array2 = new int[array.Length];
			bool flag = this.Amount == "*" || this.Amount.EqualsNoCase("all");
			int num = (flag ? int.MaxValue : this.Amount.RollCached());
			int num2 = 0;
			int num3 = array.Length;
			while (num2 < num3 && num > 0)
			{
				num -= (array2[num2] = Math.Min(The.Player.GetFreeDrams(array[num2], null, null, null, false), num));
				num2++;
			}
			if (flag || num <= 0)
			{
				if (Consume)
				{
					int i = 0;
					int num4 = array.Length;
					while (i < num4)
					{
						The.Player.UseDrams(array2[i], array[i], null, null, null, null, false);
						if (!this.Destroy)
						{
							The.Speaker.GiveDrams(array2[i], array[i], false, null, null, null, null, true, null);
						}
						i++;
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x04003860 RID: 14432
		public string Liquids;

		// Token: 0x04003861 RID: 14433
		public string Amount = "1";

		// Token: 0x04003862 RID: 14434
		public string Message;

		// Token: 0x04003863 RID: 14435
		public bool Destroy;

		// Token: 0x04003864 RID: 14436
		public bool Require = true;
	}
}
