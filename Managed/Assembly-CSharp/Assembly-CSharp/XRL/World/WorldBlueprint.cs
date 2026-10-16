using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace XRL.World
{
	// Token: 0x02000987 RID: 2439
	[Serializable]
	public class WorldBlueprint
	{
		// Token: 0x1700085E RID: 2142
		// (get) Token: 0x06006710 RID: 26384 RVA: 0x00200680 File Offset: 0x001FE880
		public IZoneFactory ZoneFactoryInstance
		{
			get
			{
				if (this._ZoneFactoryInstance == null && !this.ZoneFactory.IsNullOrEmpty())
				{
					Type type = ModManager.ResolveType("XRL.World.ZoneFactories." + this.ZoneFactory, false, false, true);
					this._ZoneFactoryInstance = Activator.CreateInstance(type) as IZoneFactory;
					if (this._ZoneFactoryInstance != null)
					{
						this._ZoneFactoryInstance.Blueprint = this;
						this._ZoneFactoryInstance.Initialize();
					}
				}
				return this._ZoneFactoryInstance;
			}
		}

		// Token: 0x06006711 RID: 26385 RVA: 0x002006F4 File Offset: 0x001FE8F4
		public bool IsMatch(string ZoneID)
		{
			if (ZoneID.IsNullOrEmpty())
			{
				return false;
			}
			if (this.ZoneFactoryRegex.IsNullOrEmpty())
			{
				return false;
			}
			bool flag;
			try
			{
				if (this.Compiled == null)
				{
					this.Compiled = new Regex(this.ZoneFactoryRegex, RegexOptions.Compiled);
				}
				flag = this.Compiled.IsMatch(ZoneID);
			}
			catch (Exception ex)
			{
				MetricsManager.LogException("World::ZoneFactoryRegex", ex, "game_exception");
				flag = false;
			}
			return flag;
		}

		// Token: 0x06006712 RID: 26386 RVA: 0x0020076C File Offset: 0x001FE96C
		public ZoneBlueprint GetBlueprintFor(ZoneRequest Request)
		{
			if (Request.Blueprints == null)
			{
				this._Blueprints.Clear();
				Request.Blueprints = this._Blueprints;
			}
			IZoneFactory zoneFactoryInstance = this.ZoneFactoryInstance;
			if (zoneFactoryInstance != null)
			{
				zoneFactoryInstance.AddBlueprintsFor(Request);
			}
			return Request.Blueprints.FirstOrDefault<ZoneBlueprint>();
		}

		// Token: 0x06006713 RID: 26387 RVA: 0x002007AC File Offset: 0x001FE9AC
		public List<ZoneBlueprint> GetBlueprintsFor(ZoneRequest Request)
		{
			ref List<ZoneBlueprint> ptr = ref Request.Blueprints;
			if (ptr == null)
			{
				ptr = new List<ZoneBlueprint>();
			}
			IZoneFactory zoneFactoryInstance = this.ZoneFactoryInstance;
			if (zoneFactoryInstance != null)
			{
				zoneFactoryInstance.AddBlueprintsFor(Request);
			}
			return Request.Blueprints;
		}

		// Token: 0x06006714 RID: 26388 RVA: 0x002007E4 File Offset: 0x001FE9E4
		public Zone GenerateZone(ZoneRequest Request, int Width, int Height)
		{
			IZoneFactory zoneFactoryInstance = this.ZoneFactoryInstance;
			return ((zoneFactoryInstance != null) ? zoneFactoryInstance.GenerateZone(Request, Width, Height) : null) ?? new Zone(Width, Height);
		}

		// Token: 0x04002F9D RID: 12189
		public string DisplayName;

		// Token: 0x04002F9E RID: 12190
		public string Name;

		// Token: 0x04002F9F RID: 12191
		public string Map;

		// Token: 0x04002FA0 RID: 12192
		public string AmbientBed;

		// Token: 0x04002FA1 RID: 12193
		public string ZoneFactory;

		// Token: 0x04002FA2 RID: 12194
		public string ZoneFactoryRegex;

		// Token: 0x04002FA3 RID: 12195
		public string Plane;

		// Token: 0x04002FA4 RID: 12196
		public string Protocol;

		// Token: 0x04002FA5 RID: 12197
		public string CustomClock;

		// Token: 0x04002FA6 RID: 12198
		public int? PsychicHunterChance;

		// Token: 0x04002FA7 RID: 12199
		[NonSerialized]
		private IZoneFactory _ZoneFactoryInstance;

		// Token: 0x04002FA8 RID: 12200
		[NonSerialized]
		private Regex Compiled;

		// Token: 0x04002FA9 RID: 12201
		[NonSerialized]
		private List<ZoneBlueprint> _Blueprints = new List<ZoneBlueprint>();

		// Token: 0x04002FAA RID: 12202
		public List<ZoneBuilderBlueprint> Builders = new List<ZoneBuilderBlueprint>();

		// Token: 0x04002FAB RID: 12203
		public Dictionary<string, CellBlueprint> CellBlueprintsByApplication = new Dictionary<string, CellBlueprint>();

		// Token: 0x04002FAC RID: 12204
		public Dictionary<string, CellBlueprint> CellBlueprintsByName = new Dictionary<string, CellBlueprint>();
	}
}
