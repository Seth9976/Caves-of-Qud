using System;
using System.Collections.Generic;

namespace XRL.World.Parts
{
	// Token: 0x02000E54 RID: 3668
	[Serializable]
	public class CannotBeInfluenced : IActivePart
	{
		// Token: 0x0600947A RID: 38010 RVA: 0x00357113 File Offset: 0x00355313
		public CannotBeInfluenced()
		{
			this.WorksOnSelf = true;
			this.IsBreakageSensitive = false;
			this.IsRustSensitive = false;
		}

		// Token: 0x0600947B RID: 38011 RVA: 0x00357130 File Offset: 0x00355330
		public override bool SameAs(IPart p)
		{
			return !((p as CannotBeInfluenced).Messages != this.Messages) && base.SameAs(p);
		}

		// Token: 0x0600947C RID: 38012 RVA: 0x00357153 File Offset: 0x00355353
		public override void Register(GameObject Object, IEventRegistrar Registrar)
		{
			Registrar.Register("CanBeInfluenced");
			base.Register(Object, Registrar);
		}

		// Token: 0x0600947D RID: 38013 RVA: 0x00357168 File Offset: 0x00355368
		public override bool FireEvent(Event E)
		{
			if (E.ID == "CanBeInfluenced" && base.IsReady(true, false, false, false, false, false, false, false, false, false, 1, null, false, 0L, null))
			{
				if (!string.IsNullOrEmpty(this.Messages))
				{
					string stringParameter = E.GetStringParameter("Type", null);
					Dictionary<string, string> dictionary = this.Messages.CachedDictionaryExpansion();
					string text = null;
					if (stringParameter != null && dictionary.TryGetValue(stringParameter, out text))
					{
						E.SetParameter("Message", text);
					}
					else if (dictionary.TryGetValue("default", out text))
					{
						E.SetParameter("Message", text);
					}
				}
				return false;
			}
			return base.FireEvent(E);
		}

		// Token: 0x04003D65 RID: 15717
		public string Messages;
	}
}
