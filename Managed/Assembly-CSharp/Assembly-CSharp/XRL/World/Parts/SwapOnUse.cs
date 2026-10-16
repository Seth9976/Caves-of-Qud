using System;

namespace XRL.World.Parts
{
	// Token: 0x020010A9 RID: 4265
	[Serializable]
	public class SwapOnUse : IPart
	{
		// Token: 0x0600A9B8 RID: 43448 RVA: 0x003E0937 File Offset: 0x003DEB37
		public override bool SameAs(IPart p)
		{
			return false;
		}

		// Token: 0x0600A9B9 RID: 43449 RVA: 0x003E093A File Offset: 0x003DEB3A
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CanSmartUse");
			Registrar.Register("CommandSmartUse");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600A9BA RID: 43450 RVA: 0x003E095C File Offset: 0x003DEB5C
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CanSmartUse")
			{
				return false;
			}
			if (E.ID == "CommandSmartUse")
			{
				if (!string.IsNullOrEmpty(this.Message))
				{
					base.EmitMessage(this.Message, ' ', false, false, false, null, null);
				}
				this.ParentObject.ReplaceWith(GameObject.Create(this.Blueprint));
			}
			return base.FireEvent(E);
		}

		// Token: 0x040045FC RID: 17916
		public string Blueprint = "Campfire";

		// Token: 0x040045FD RID: 17917
		public string Message;
	}
}
