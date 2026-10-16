using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.SubsystemsImplementation
{
	// Token: 0x02000012 RID: 18
	[NativeHeader("Modules/Subsystems/SubsystemManager.h")]
	public static class SubsystemDescriptorStore
	{
		// Token: 0x06000056 RID: 86 RVA: 0x000028F7 File Offset: 0x00000AF7
		[RequiredByNativeCode]
		internal static void InitializeManagedDescriptor(IntPtr ptr, IntegratedSubsystemDescriptor desc)
		{
			desc.m_Ptr = ptr;
			SubsystemDescriptorStore.s_IntegratedDescriptors.Add(desc);
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002910 File Offset: 0x00000B10
		[RequiredByNativeCode]
		internal static void ClearManagedDescriptors()
		{
			foreach (IntegratedSubsystemDescriptor integratedSubsystemDescriptor in SubsystemDescriptorStore.s_IntegratedDescriptors)
			{
				integratedSubsystemDescriptor.m_Ptr = IntPtr.Zero;
			}
			SubsystemDescriptorStore.s_IntegratedDescriptors.Clear();
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00002974 File Offset: 0x00000B74
		private unsafe static void ReportSingleSubsystemAnalytics(string id)
		{
			try
			{
				ManagedSpanWrapper managedSpanWrapper;
				if (!StringMarshaller.TryMarshalEmptyOrNullString(id, ref managedSpanWrapper))
				{
					ReadOnlySpan<char> readOnlySpan = id.AsSpan();
					fixed (char* ptr = readOnlySpan.GetPinnableReference())
					{
						managedSpanWrapper = new ManagedSpanWrapper((void*)ptr, readOnlySpan.Length);
					}
				}
				SubsystemDescriptorStore.ReportSingleSubsystemAnalytics_Injected(ref managedSpanWrapper);
			}
			finally
			{
				char* ptr = null;
			}
		}

		// Token: 0x06000059 RID: 89 RVA: 0x000029C8 File Offset: 0x00000BC8
		public static void RegisterDescriptor(SubsystemDescriptorWithProvider descriptor)
		{
			descriptor.ThrowIfInvalid();
			SubsystemDescriptorStore.RegisterDescriptor<SubsystemDescriptorWithProvider, SubsystemDescriptorWithProvider>(descriptor, SubsystemDescriptorStore.s_StandaloneDescriptors);
		}

		// Token: 0x0600005A RID: 90 RVA: 0x000029E0 File Offset: 0x00000BE0
		internal static void GetAllSubsystemDescriptors(List<ISubsystemDescriptor> descriptors)
		{
			descriptors.Clear();
			int num = SubsystemDescriptorStore.s_IntegratedDescriptors.Count + SubsystemDescriptorStore.s_StandaloneDescriptors.Count + SubsystemDescriptorStore.s_DeprecatedDescriptors.Count;
			bool flag = descriptors.Capacity < num;
			if (flag)
			{
				descriptors.Capacity = num;
			}
			SubsystemDescriptorStore.AddDescriptorSubset<IntegratedSubsystemDescriptor>(SubsystemDescriptorStore.s_IntegratedDescriptors, descriptors);
			SubsystemDescriptorStore.AddDescriptorSubset<SubsystemDescriptorWithProvider>(SubsystemDescriptorStore.s_StandaloneDescriptors, descriptors);
			SubsystemDescriptorStore.AddDescriptorSubset<SubsystemDescriptor>(SubsystemDescriptorStore.s_DeprecatedDescriptors, descriptors);
		}

		// Token: 0x0600005B RID: 91 RVA: 0x00002A50 File Offset: 0x00000C50
		private static void AddDescriptorSubset<TBaseTypeInList>(List<TBaseTypeInList> copyFrom, List<ISubsystemDescriptor> copyTo) where TBaseTypeInList : ISubsystemDescriptor
		{
			foreach (TBaseTypeInList tbaseTypeInList in copyFrom)
			{
				copyTo.Add(tbaseTypeInList);
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00002AA8 File Offset: 0x00000CA8
		internal static void GetSubsystemDescriptors<T>(List<T> descriptors) where T : ISubsystemDescriptor
		{
			descriptors.Clear();
			SubsystemDescriptorStore.AddDescriptorSubset<IntegratedSubsystemDescriptor, T>(SubsystemDescriptorStore.s_IntegratedDescriptors, descriptors);
			SubsystemDescriptorStore.AddDescriptorSubset<SubsystemDescriptorWithProvider, T>(SubsystemDescriptorStore.s_StandaloneDescriptors, descriptors);
			SubsystemDescriptorStore.AddDescriptorSubset<SubsystemDescriptor, T>(SubsystemDescriptorStore.s_DeprecatedDescriptors, descriptors);
		}

		// Token: 0x0600005D RID: 93 RVA: 0x00002AD8 File Offset: 0x00000CD8
		private static void AddDescriptorSubset<TBaseTypeInList, TQueryType>(List<TBaseTypeInList> copyFrom, List<TQueryType> copyTo) where TBaseTypeInList : ISubsystemDescriptor where TQueryType : ISubsystemDescriptor
		{
			foreach (TBaseTypeInList tbaseTypeInList in copyFrom)
			{
				TQueryType tqueryType;
				bool flag;
				if (tbaseTypeInList is TQueryType)
				{
					tqueryType = tbaseTypeInList as TQueryType;
					flag = true;
				}
				else
				{
					flag = false;
				}
				bool flag2 = flag;
				if (flag2)
				{
					copyTo.Add(tqueryType);
				}
			}
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00002B54 File Offset: 0x00000D54
		internal static void RegisterDescriptor<TDescriptor, TBaseTypeInList>(TDescriptor descriptor, List<TBaseTypeInList> storeInList) where TDescriptor : TBaseTypeInList where TBaseTypeInList : ISubsystemDescriptor
		{
			for (int i = 0; i < storeInList.Count; i++)
			{
				TBaseTypeInList tbaseTypeInList = storeInList[i];
				bool flag = tbaseTypeInList.id != descriptor.id;
				if (!flag)
				{
					Debug.LogWarning("Registering subsystem descriptor with duplicate ID '" + descriptor.id + "' - overwriting previous entry.");
					storeInList[i] = (TBaseTypeInList)((object)descriptor);
					return;
				}
			}
			SubsystemDescriptorStore.ReportSingleSubsystemAnalytics(descriptor.id);
			storeInList.Add((TBaseTypeInList)((object)descriptor));
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00002C01 File Offset: 0x00000E01
		internal static void RegisterDeprecatedDescriptor(SubsystemDescriptor descriptor)
		{
			SubsystemDescriptorStore.RegisterDescriptor<SubsystemDescriptor, SubsystemDescriptor>(descriptor, SubsystemDescriptorStore.s_DeprecatedDescriptors);
		}

		// Token: 0x06000061 RID: 97
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReportSingleSubsystemAnalytics_Injected(ref ManagedSpanWrapper id);

		// Token: 0x0400000E RID: 14
		private static List<IntegratedSubsystemDescriptor> s_IntegratedDescriptors = new List<IntegratedSubsystemDescriptor>();

		// Token: 0x0400000F RID: 15
		private static List<SubsystemDescriptorWithProvider> s_StandaloneDescriptors = new List<SubsystemDescriptorWithProvider>();

		// Token: 0x04000010 RID: 16
		private static List<SubsystemDescriptor> s_DeprecatedDescriptors = new List<SubsystemDescriptor>();
	}
}
