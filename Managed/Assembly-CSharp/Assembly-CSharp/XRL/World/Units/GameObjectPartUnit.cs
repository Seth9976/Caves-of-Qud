using System;
using ConsoleLib.Console;

namespace XRL.World.Units
{
	// Token: 0x020009AF RID: 2479
	[Serializable]
	public class GameObjectPartUnit : GameObjectUnit
	{
		// Token: 0x06006AE8 RID: 27368 RVA: 0x002270FE File Offset: 0x002252FE
		public override void Apply(GameObject Object)
		{
			Object.AddPart(this.Part.DeepCopy(Object), true, false);
		}

		// Token: 0x06006AE9 RID: 27369 RVA: 0x00227115 File Offset: 0x00225315
		public override void Remove(GameObject Object)
		{
			Object.RemovePart(this.Part.Name);
		}

		// Token: 0x06006AEA RID: 27370 RVA: 0x00227129 File Offset: 0x00225329
		public override void Reset()
		{
			base.Reset();
			this.Part = null;
		}

		// Token: 0x06006AEB RID: 27371 RVA: 0x00227138 File Offset: 0x00225338
		public override bool CanInscribe()
		{
			return !this.Part.WantEvent(GetShortDescriptionEvent.ID, MinEvent.CascadeLevel);
		}

		// Token: 0x06006AEC RID: 27372 RVA: 0x00227154 File Offset: 0x00225354
		public override string GetDescription(bool Inscription = false)
		{
			if (this.Description == null)
			{
				try
				{
					if (this.Part.WantEvent(GetShortDescriptionEvent.ID, MinEvent.CascadeLevel))
					{
						GetShortDescriptionEvent getShortDescriptionEvent = GetShortDescriptionEvent.FromPool();
						getShortDescriptionEvent.AsIfKnown = true;
						getShortDescriptionEvent.NoStatus = true;
						this.Part.HandleEvent(getShortDescriptionEvent);
						this.Description = ColorUtility.StripFormatting(getShortDescriptionEvent.Postfix.ToString()).Trim();
						GetShortDescriptionEvent.ResetTo(ref getShortDescriptionEvent);
					}
				}
				catch (Exception ex)
				{
					string text = "[DescriptionError: {0}]";
					IPart part = this.Part;
					this.Description = string.Format(text, (part != null) ? part.GetType() : null);
					MetricsManager.LogException("GameObjectPartUnit.GetShortDescriptionEvent", ex, "game_exception");
				}
			}
			return this.Description;
		}

		// Token: 0x040030F6 RID: 12534
		public IPart Part;

		// Token: 0x040030F7 RID: 12535
		public string Description;
	}
}
