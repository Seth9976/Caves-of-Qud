using System;

namespace XRL.World
{
	// Token: 0x02000986 RID: 2438
	public abstract class IZoneFactory
	{
		// Token: 0x06006707 RID: 26375 RVA: 0x0020065A File Offset: 0x001FE85A
		public virtual void Initialize()
		{
		}

		// Token: 0x06006708 RID: 26376 RVA: 0x0020065C File Offset: 0x001FE85C
		[Obsolete("CanBuildZone(string) superseded by CanBuildZone(ZoneRequest).")]
		public virtual bool CanBuildZone(string ZoneID)
		{
			return true;
		}

		// Token: 0x06006709 RID: 26377 RVA: 0x0020065F File Offset: 0x001FE85F
		public virtual bool CanBuildZone(ZoneRequest Request)
		{
			return true;
		}

		// Token: 0x0600670A RID: 26378 RVA: 0x00200662 File Offset: 0x001FE862
		public virtual void AddBlueprintsFor(ZoneRequest Request)
		{
		}

		// Token: 0x0600670B RID: 26379 RVA: 0x00200664 File Offset: 0x001FE864
		public virtual Zone GenerateZone(ZoneRequest Request, int Width, int Height)
		{
			return new Zone(Width, Height);
		}

		// Token: 0x0600670C RID: 26380 RVA: 0x0020066D File Offset: 0x001FE86D
		[Obsolete("BuildZone(string) superseded by BuildZone(ZoneRequest).")]
		public virtual Zone BuildZone(string ZoneID)
		{
			return null;
		}

		// Token: 0x0600670D RID: 26381 RVA: 0x00200670 File Offset: 0x001FE870
		public virtual Zone BuildZone(ZoneRequest Request)
		{
			return null;
		}

		// Token: 0x0600670E RID: 26382 RVA: 0x00200673 File Offset: 0x001FE873
		public virtual void AfterBuildZone(Zone Zone, ZoneManager Manager)
		{
		}

		// Token: 0x04002F9C RID: 12188
		public WorldBlueprint Blueprint;
	}
}
