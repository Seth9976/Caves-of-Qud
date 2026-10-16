using System;
using XRL.World;
using XRL.World.Parts;
using XRL.World.Parts.Skill;

namespace XRL
{
	// Token: 0x02000532 RID: 1330
	public abstract class IBaseSkillEntry : IPartEntry
	{
		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x060038F4 RID: 14580 RVA: 0x00117BF8 File Offset: 0x00115DF8
		public BaseSkill Generic
		{
			get
			{
				BaseSkill baseSkill;
				if ((baseSkill = this._Generic) == null)
				{
					baseSkill = (this._Generic = Skills.GetGenericSkill(this.Class, null));
				}
				return baseSkill;
			}
		}

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x060038F5 RID: 14581 RVA: 0x00117C24 File Offset: 0x00115E24
		public override IPart Instance
		{
			get
			{
				return this.Generic;
			}
		}

		// Token: 0x060038F6 RID: 14582
		public abstract bool MeetsRequirements(GameObject Object, bool ShowPopup = false);

		// Token: 0x060038F7 RID: 14583 RVA: 0x00117C2C File Offset: 0x00115E2C
		public string GetFormattedDescription()
		{
			return this.Generic.GetDescription(this);
		}

		// Token: 0x060038F8 RID: 14584 RVA: 0x00117C3C File Offset: 0x00115E3C
		public override void HandleXMLNode(XmlDataHelper Reader)
		{
			this.Tile = Reader.ParseAttribute<string>("Tile", this.Tile, false, null);
			this.Foreground = Reader.ParseAttribute<string>("Foreground", this.Foreground, false, null);
			this.Detail = Reader.ParseAttribute<string>("Detail", this.Detail, false, null);
			this.Description = Reader.ParseAttribute<string>("Description", this.Description, false, null);
			string text = null;
			text = Reader.ParseAttribute<string>("ReplaceDescriptionIfMod", text, false, null);
			if (!text.IsNullOrEmpty())
			{
				int num = text.IndexOf(':');
				if (num != -1 && ModManager.ModLoadedBySpec(text.Substring(0, num)))
				{
					this.Description = text.Substring(num + 1);
				}
			}
			string text2 = null;
			text2 = Reader.ParseAttribute<string>("AddDescriptionIfMod", text2, false, null);
			if (!text2.IsNullOrEmpty())
			{
				int num2 = text2.IndexOf(':');
				if (num2 != -1 && ModManager.ModLoadedBySpec(text2.Substring(0, num2)))
				{
					this.Description = this.Description + " " + text2.Substring(num2 + 1);
				}
			}
			string text3 = null;
			text3 = Reader.ParseAttribute<string>("AddDescription", text3, false, null);
			if (!text3.IsNullOrEmpty())
			{
				this.Description = this.Description + " " + text3;
			}
			base.HandleXMLNode(Reader);
		}

		// Token: 0x04001CCE RID: 7374
		public string Tile;

		// Token: 0x04001CCF RID: 7375
		public string Foreground = "w";

		// Token: 0x04001CD0 RID: 7376
		public string Detail = "B";

		// Token: 0x04001CD1 RID: 7377
		public string Description;

		/// <summary>Entry name will be rendered as '???' until requirements are met.</summary>
		// Token: 0x04001CD2 RID: 7378
		private BaseSkill _Generic;
	}
}
