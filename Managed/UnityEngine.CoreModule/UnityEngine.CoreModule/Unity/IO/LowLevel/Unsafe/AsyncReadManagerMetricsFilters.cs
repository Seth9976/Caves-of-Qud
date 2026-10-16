using System;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace Unity.IO.LowLevel.Unsafe
{
	// Token: 0x02000090 RID: 144
	[RequiredByNativeCode]
	[NativeConditional("ENABLE_PROFILER")]
	[NativeAsStruct]
	[StructLayout(LayoutKind.Sequential)]
	public class AsyncReadManagerMetricsFilters
	{
		// Token: 0x060002D5 RID: 725 RVA: 0x00005BA9 File Offset: 0x00003DA9
		public AsyncReadManagerMetricsFilters()
		{
			this.ClearFilters();
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00005BBA File Offset: 0x00003DBA
		public AsyncReadManagerMetricsFilters(ulong typeID)
		{
			this.ClearFilters();
			this.SetTypeIDFilter(typeID);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00005BD3 File Offset: 0x00003DD3
		public AsyncReadManagerMetricsFilters(ProcessingState state)
		{
			this.ClearFilters();
			this.SetStateFilter(state);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00005BEC File Offset: 0x00003DEC
		public AsyncReadManagerMetricsFilters(FileReadType readType)
		{
			this.ClearFilters();
			this.SetReadTypeFilter(readType);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00005C05 File Offset: 0x00003E05
		public AsyncReadManagerMetricsFilters(Priority priorityLevel)
		{
			this.ClearFilters();
			this.SetPriorityFilter(priorityLevel);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00005C1E File Offset: 0x00003E1E
		public AsyncReadManagerMetricsFilters(AssetLoadingSubsystem subsystem)
		{
			this.ClearFilters();
			this.SetSubsystemFilter(subsystem);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00005C37 File Offset: 0x00003E37
		public AsyncReadManagerMetricsFilters(ulong[] typeIDs)
		{
			this.ClearFilters();
			this.SetTypeIDFilter(typeIDs);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00005C50 File Offset: 0x00003E50
		public AsyncReadManagerMetricsFilters(ProcessingState[] states)
		{
			this.ClearFilters();
			this.SetStateFilter(states);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00005C69 File Offset: 0x00003E69
		public AsyncReadManagerMetricsFilters(FileReadType[] readTypes)
		{
			this.ClearFilters();
			this.SetReadTypeFilter(readTypes);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00005C82 File Offset: 0x00003E82
		public AsyncReadManagerMetricsFilters(Priority[] priorityLevels)
		{
			this.ClearFilters();
			this.SetPriorityFilter(priorityLevels);
		}

		// Token: 0x060002DF RID: 735 RVA: 0x00005C9B File Offset: 0x00003E9B
		public AsyncReadManagerMetricsFilters(AssetLoadingSubsystem[] subsystems)
		{
			this.ClearFilters();
			this.SetSubsystemFilter(subsystems);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00005CB4 File Offset: 0x00003EB4
		public AsyncReadManagerMetricsFilters(ulong[] typeIDs, ProcessingState[] states, FileReadType[] readTypes, Priority[] priorityLevels, AssetLoadingSubsystem[] subsystems)
		{
			this.ClearFilters();
			this.SetTypeIDFilter(typeIDs);
			this.SetStateFilter(states);
			this.SetReadTypeFilter(readTypes);
			this.SetPriorityFilter(priorityLevels);
			this.SetSubsystemFilter(subsystems);
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00005CEF File Offset: 0x00003EEF
		public void SetTypeIDFilter(ulong[] _typeIDs)
		{
			this.TypeIDs = _typeIDs;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00005CF9 File Offset: 0x00003EF9
		public void SetStateFilter(ProcessingState[] _states)
		{
			this.States = _states;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00005D03 File Offset: 0x00003F03
		public void SetReadTypeFilter(FileReadType[] _readTypes)
		{
			this.ReadTypes = _readTypes;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00005D0D File Offset: 0x00003F0D
		public void SetPriorityFilter(Priority[] _priorityLevels)
		{
			this.PriorityLevels = _priorityLevels;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00005D17 File Offset: 0x00003F17
		public void SetSubsystemFilter(AssetLoadingSubsystem[] _subsystems)
		{
			this.Subsystems = _subsystems;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x00005D21 File Offset: 0x00003F21
		public void SetTypeIDFilter(ulong _typeID)
		{
			this.TypeIDs = new ulong[] { _typeID };
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x00005D34 File Offset: 0x00003F34
		public void SetStateFilter(ProcessingState _state)
		{
			this.States = new ProcessingState[] { _state };
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00005D47 File Offset: 0x00003F47
		public void SetReadTypeFilter(FileReadType _readType)
		{
			this.ReadTypes = new FileReadType[] { _readType };
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x00005D5A File Offset: 0x00003F5A
		public void SetPriorityFilter(Priority _priorityLevel)
		{
			this.PriorityLevels = new Priority[] { _priorityLevel };
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00005D6D File Offset: 0x00003F6D
		public void SetSubsystemFilter(AssetLoadingSubsystem _subsystem)
		{
			this.Subsystems = new AssetLoadingSubsystem[] { _subsystem };
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00005D80 File Offset: 0x00003F80
		public void RemoveTypeIDFilter()
		{
			this.TypeIDs = null;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00005D8A File Offset: 0x00003F8A
		public void RemoveStateFilter()
		{
			this.States = null;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00005D94 File Offset: 0x00003F94
		public void RemoveReadTypeFilter()
		{
			this.ReadTypes = null;
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00005D9E File Offset: 0x00003F9E
		public void RemovePriorityFilter()
		{
			this.PriorityLevels = null;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00005DA8 File Offset: 0x00003FA8
		public void RemoveSubsystemFilter()
		{
			this.Subsystems = null;
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x00005DB2 File Offset: 0x00003FB2
		public void ClearFilters()
		{
			this.RemoveTypeIDFilter();
			this.RemoveStateFilter();
			this.RemoveReadTypeFilter();
			this.RemovePriorityFilter();
			this.RemoveSubsystemFilter();
		}

		// Token: 0x04000248 RID: 584
		[NativeName("typeIDs")]
		internal ulong[] TypeIDs;

		// Token: 0x04000249 RID: 585
		[NativeName("states")]
		internal ProcessingState[] States;

		// Token: 0x0400024A RID: 586
		[NativeName("readTypes")]
		internal FileReadType[] ReadTypes;

		// Token: 0x0400024B RID: 587
		[NativeName("priorityLevels")]
		internal Priority[] PriorityLevels;

		// Token: 0x0400024C RID: 588
		[NativeName("subsystems")]
		internal AssetLoadingSubsystem[] Subsystems;
	}
}
