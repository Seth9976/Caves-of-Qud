using System;
using XRL.World;

namespace XRL.CharacterBuilds.Qud
{
	// Token: 0x0200059F RID: 1439
	public class QudCustomizeCharacterModule : QudEmbarkBuilderModule<QudCustomizeCharacterModuleData>
	{
		// Token: 0x06003D88 RID: 15752 RVA: 0x0012D8D1 File Offset: 0x0012BAD1
		public override bool shouldBeEnabled()
		{
			EmbarkBuilder builder = this.builder;
			object obj;
			if (builder == null)
			{
				obj = null;
			}
			else
			{
				QudSubtypeModule module = builder.GetModule<QudSubtypeModule>();
				if (module == null)
				{
					obj = null;
				}
				else
				{
					QudSubtypeModuleData data = module.data;
					obj = ((data != null) ? data.Subtype : null);
				}
			}
			return obj != null;
		}

		// Token: 0x06003D89 RID: 15753 RVA: 0x0012D900 File Offset: 0x0012BB00
		public override string DataErrors()
		{
			return null;
		}

		// Token: 0x06003D8A RID: 15754 RVA: 0x0012D903 File Offset: 0x0012BB03
		public override string DataWarnings()
		{
			return null;
		}

		// Token: 0x06003D8B RID: 15755 RVA: 0x0012D906 File Offset: 0x0012BB06
		public override void InitFromSeed(string seed)
		{
		}

		// Token: 0x06003D8C RID: 15756 RVA: 0x0012D908 File Offset: 0x0012BB08
		public void setPet(string pet)
		{
			if (base.data == null)
			{
				base.data = new QudCustomizeCharacterModuleData();
			}
			base.data.pet = (string.IsNullOrEmpty(pet) ? null : pet);
			this.setData(base.data);
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06003D8D RID: 15757 RVA: 0x0012D940 File Offset: 0x0012BB40
		public string genderName
		{
			get
			{
				if (base.data.gender != null)
				{
					return base.data.gender.Name;
				}
				return "nonspecific";
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06003D8E RID: 15758 RVA: 0x0012D965 File Offset: 0x0012BB65
		public string pronounSetName
		{
			get
			{
				if (base.data.pronounSet != null)
				{
					return base.data.pronounSet.Name;
				}
				return "<from gender>";
			}
		}

		// Token: 0x06003D8F RID: 15759 RVA: 0x0012D98A File Offset: 0x0012BB8A
		public void setName(string name)
		{
			if (base.data == null)
			{
				base.data = new QudCustomizeCharacterModuleData();
			}
			base.data.name = (string.IsNullOrEmpty(name) ? null : name);
			this.setData(base.data);
		}

		// Token: 0x06003D90 RID: 15760 RVA: 0x0012D9C2 File Offset: 0x0012BBC2
		public override void Init()
		{
			PronounSet.Reinit();
		}

		// Token: 0x06003D91 RID: 15761 RVA: 0x0012D9CC File Offset: 0x0012BBCC
		public override object handleBootEvent(string id, XRLGame game, EmbarkInfo info, object element = null)
		{
			if (id == QudGameBootModule.BOOTEVENT_BEFOREBOOTPLAYEROBJECT)
			{
				QudCustomizeCharacterModuleData data = info.getData<QudCustomizeCharacterModuleData>();
				string text;
				if (data == null)
				{
					text = null;
				}
				else
				{
					string name = data.name;
					text = ((name != null) ? name.Trim() : null);
				}
				string text2 = text;
				if (!string.IsNullOrEmpty(text2))
				{
					game.PlayerName = text2;
				}
			}
			if (id == QudGameBootModule.BOOTEVENT_AFTERBOOTPLAYEROBJECT)
			{
				GameObject gameObject = element as GameObject;
				if (base.data != null && base.data.pronounSet != null && gameObject != null)
				{
					base.data.pronounSet.Register();
					gameObject.SetPronounSet(base.data.pronounSet);
				}
				if (base.data != null && gameObject != null && base.data.gender != null)
				{
					base.data.gender.Register();
					gameObject.SetGender(base.data.gender);
				}
			}
			return base.handleBootEvent(id, game, info, element);
		}
	}
}
