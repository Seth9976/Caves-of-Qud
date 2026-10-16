using System;

namespace XRL.World.Parts.Skill
{
	// Token: 0x02001373 RID: 4979
	[Serializable]
	public class Rifle_OneShot : BaseSkill
	{
		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x0600C180 RID: 49536 RVA: 0x0046C24C File Offset: 0x0046A44C
		// (set) Token: 0x0600C181 RID: 49537 RVA: 0x0046C25C File Offset: 0x0046A45C
		public int Cooldown
		{
			get
			{
				return this.CommandCooldown.Segments;
			}
			set
			{
				if (this.CommandCooldown == null)
				{
					this.CommandCooldown = new CommandCooldown("CommandRifleOneShot", 0);
				}
				if (value > 0 && this.CommandCooldown.Segments <= 0)
				{
					this.ParentObject.Abilities.AddCooldown(this.CommandCooldown);
				}
				else if (value <= 0 && this.CommandCooldown.Segments > 0)
				{
					this.ParentObject.Abilities.RemoveCooldown(this.CommandCooldown);
				}
				this.CommandCooldown.Segments = value;
			}
		}

		// Token: 0x0600C182 RID: 49538 RVA: 0x0046C2E0 File Offset: 0x0046A4E0
		public override void Initialize()
		{
			if (this.CommandCooldown == null)
			{
				this.CommandCooldown = new CommandCooldown("CommandRifleOneShot", 0);
			}
		}

		// Token: 0x0600C183 RID: 49539 RVA: 0x0046C2FB File Offset: 0x0046A4FB
		public override void Write(GameObject Basis, SerializationWriter Writer)
		{
			base.Write(Basis, Writer);
			Writer.WriteTokenized(this.CommandCooldown);
		}

		// Token: 0x0600C184 RID: 49540 RVA: 0x0046C311 File Offset: 0x0046A511
		public override void Read(GameObject Basis, SerializationReader Reader)
		{
			base.Read(Basis, Reader);
			this.CommandCooldown = (CommandCooldown)Reader.ReadTokenized();
		}

		// Token: 0x04004D79 RID: 19833
		public const string CMD_ID = "CommandRifleOneShot";

		// Token: 0x04004D7A RID: 19834
		private CommandCooldown CommandCooldown;
	}
}
