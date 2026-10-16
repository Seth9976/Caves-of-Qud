using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting.APIUpdating;

namespace UnityEngine.AI
{
	// Token: 0x0200000A RID: 10
	[NativeHeader("Modules/AI/NavMesh/NavMesh.bindings.h")]
	[NativeHeader("Modules/AI/Components/NavMeshAgent.bindings.h")]
	[MovedFrom("UnityEngine")]
	[HelpURL("https://docs.unity3d.com/Packages/com.unity.ai.navigation@2.0/manual/NavMeshAgent.html")]
	public sealed class NavMeshAgent : Behaviour
	{
		// Token: 0x06000056 RID: 86 RVA: 0x000029D8 File Offset: 0x00000BD8
		public bool SetDestination(Vector3 target)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return NavMeshAgent.SetDestination_Injected(intPtr, ref target);
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000057 RID: 87 RVA: 0x000029FC File Offset: 0x00000BFC
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00002A24 File Offset: 0x00000C24
		public Vector3 destination
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				NavMeshAgent.get_destination_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				NavMeshAgent.set_destination_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002A48 File Offset: 0x00000C48
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00002A6C File Offset: 0x00000C6C
		public float stoppingDistance
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return NavMeshAgent.get_stoppingDistance_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				NavMeshAgent.set_stoppingDistance_Injected(intPtr, value);
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00002A90 File Offset: 0x00000C90
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00002AB8 File Offset: 0x00000CB8
		public Vector3 velocity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				NavMeshAgent.get_velocity_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				NavMeshAgent.set_velocity_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002ADC File Offset: 0x00000CDC
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00002B04 File Offset: 0x00000D04
		[NativeProperty("Position")]
		public Vector3 nextPosition
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				NavMeshAgent.get_nextPosition_Injected(intPtr, out vector);
				return vector;
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				NavMeshAgent.set_nextPosition_Injected(intPtr, ref value);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002B28 File Offset: 0x00000D28
		public Vector3 steeringTarget
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				NavMeshAgent.get_steeringTarget_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00002B50 File Offset: 0x00000D50
		public Vector3 desiredVelocity
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				NavMeshAgent.get_desiredVelocity_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002B78 File Offset: 0x00000D78
		public float remainingDistance
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return NavMeshAgent.get_remainingDistance_Injected(intPtr);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002B9C File Offset: 0x00000D9C
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00002BC0 File Offset: 0x00000DC0
		public float baseOffset
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return NavMeshAgent.get_baseOffset_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				NavMeshAgent.set_baseOffset_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002BE4 File Offset: 0x00000DE4
		public bool isOnOffMeshLink
		{
			[NativeName("IsOnOffMeshLink")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return NavMeshAgent.get_isOnOffMeshLink_Injected(intPtr);
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00002C08 File Offset: 0x00000E08
		public void ActivateCurrentOffMeshLink(bool activated)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			NavMeshAgent.ActivateCurrentOffMeshLink_Injected(intPtr, activated);
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002C2B File Offset: 0x00000E2B
		public OffMeshLinkData currentOffMeshLinkData
		{
			get
			{
				return this.GetCurrentOffMeshLinkDataInternal();
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00002C34 File Offset: 0x00000E34
		[FreeFunction("NavMeshAgentScriptBindings::GetCurrentOffMeshLinkDataInternal", HasExplicitThis = true)]
		internal OffMeshLinkData GetCurrentOffMeshLinkDataInternal()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			OffMeshLinkData offMeshLinkData;
			NavMeshAgent.GetCurrentOffMeshLinkDataInternal_Injected(intPtr, out offMeshLinkData);
			return offMeshLinkData;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002C59 File Offset: 0x00000E59
		public OffMeshLinkData nextOffMeshLinkData
		{
			get
			{
				return this.GetNextOffMeshLinkDataInternal();
			}
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00002C64 File Offset: 0x00000E64
		[FreeFunction("NavMeshAgentScriptBindings::GetNextOffMeshLinkDataInternal", HasExplicitThis = true)]
		internal OffMeshLinkData GetNextOffMeshLinkDataInternal()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			OffMeshLinkData offMeshLinkData;
			NavMeshAgent.GetNextOffMeshLinkDataInternal_Injected(intPtr, out offMeshLinkData);
			return offMeshLinkData;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00002C8C File Offset: 0x00000E8C
		public void CompleteOffMeshLink()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			NavMeshAgent.CompleteOffMeshLink_Injected(intPtr);
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00002CB0 File Offset: 0x00000EB0
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public bool autoTraverseOffMeshLink
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return NavMeshAgent.get_autoTraverseOffMeshLink_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				NavMeshAgent.set_autoTraverseOffMeshLink_Injected(intPtr, value);
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00002CF8 File Offset: 0x00000EF8
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00002D1C File Offset: 0x00000F1C
		public bool autoBraking
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return NavMeshAgent.get_autoBraking_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				NavMeshAgent.set_autoBraking_Injected(intPtr, value);
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00002D40 File Offset: 0x00000F40
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00002D64 File Offset: 0x00000F64
		public bool autoRepath
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return NavMeshAgent.get_autoRepath_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				NavMeshAgent.set_autoRepath_Injected(intPtr, value);
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00002D88 File Offset: 0x00000F88
		public bool hasPath
		{
			[NativeName("HasPath")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return NavMeshAgent.get_hasPath_Injected(intPtr);
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002DAC File Offset: 0x00000FAC
		public bool pathPending
		{
			[NativeName("PathPending")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return NavMeshAgent.get_pathPending_Injected(intPtr);
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00002DD0 File Offset: 0x00000FD0
		public bool isPathStale
		{
			[NativeName("IsPathStale")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return NavMeshAgent.get_isPathStale_Injected(intPtr);
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002DF4 File Offset: 0x00000FF4
		public NavMeshPathStatus pathStatus
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return NavMeshAgent.get_pathStatus_Injected(intPtr);
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002E18 File Offset: 0x00001018
		[NativeProperty("EndPositionOfCurrentPath")]
		public Vector3 pathEndPosition
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				Vector3 vector;
				NavMeshAgent.get_pathEndPosition_Injected(intPtr, out vector);
				return vector;
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00002E40 File Offset: 0x00001040
		public bool Warp(Vector3 newPosition)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return NavMeshAgent.Warp_Injected(intPtr, ref newPosition);
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002E64 File Offset: 0x00001064
		public void Move(Vector3 offset)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			NavMeshAgent.Move_Injected(intPtr, ref offset);
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002E88 File Offset: 0x00001088
		[Obsolete("Set isStopped to true instead.")]
		public void Stop()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			NavMeshAgent.Stop_Injected(intPtr);
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002EAA File Offset: 0x000010AA
		[Obsolete("Set isStopped to true instead.")]
		public void Stop(bool stopUpdates)
		{
			this.Stop();
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00002EB4 File Offset: 0x000010B4
		[Obsolete("Set isStopped to false instead.")]
		public void Resume()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			NavMeshAgent.Resume_Injected(intPtr);
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002ED8 File Offset: 0x000010D8
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00002EFC File Offset: 0x000010FC
		public bool isStopped
		{
			[FreeFunction("NavMeshAgentScriptBindings::GetIsStopped", HasExplicitThis = true)]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return NavMeshAgent.get_isStopped_Injected(intPtr);
			}
			[FreeFunction("NavMeshAgentScriptBindings::SetIsStopped", HasExplicitThis = true)]
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				NavMeshAgent.set_isStopped_Injected(intPtr, value);
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002F20 File Offset: 0x00001120
		public void ResetPath()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			NavMeshAgent.ResetPath_Injected(intPtr);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00002F44 File Offset: 0x00001144
		public bool SetPath([NotNull] NavMeshPath path)
		{
			if (path == null)
			{
				ThrowHelper.ThrowArgumentNullException(path, "path");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = NavMeshPath.BindingsMarshaller.ConvertToNative(path);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(path, "path");
			}
			return NavMeshAgent.SetPath_Injected(intPtr, intPtr2);
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600007F RID: 127 RVA: 0x00002F8C File Offset: 0x0000118C
		// (set) Token: 0x06000080 RID: 128 RVA: 0x00002FB0 File Offset: 0x000011B0
		public NavMeshPath path
		{
			get
			{
				NavMeshPath navMeshPath = new NavMeshPath();
				this.CopyPathTo(navMeshPath);
				return navMeshPath;
			}
			set
			{
				bool flag = value == null;
				if (flag)
				{
					throw new NullReferenceException();
				}
				this.SetPath(value);
			}
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00002FD4 File Offset: 0x000011D4
		[NativeMethod("CopyPath")]
		internal void CopyPathTo([NotNull] NavMeshPath path)
		{
			if (path == null)
			{
				ThrowHelper.ThrowArgumentNullException(path, "path");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = NavMeshPath.BindingsMarshaller.ConvertToNative(path);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(path, "path");
			}
			NavMeshAgent.CopyPathTo_Injected(intPtr, intPtr2);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000301C File Offset: 0x0000121C
		[NativeName("DistanceToEdge")]
		public bool FindClosestEdge(out NavMeshHit hit)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return NavMeshAgent.FindClosestEdge_Injected(intPtr, out hit);
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003040 File Offset: 0x00001240
		public bool Raycast(Vector3 targetPosition, out NavMeshHit hit)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return NavMeshAgent.Raycast_Injected(intPtr, ref targetPosition, out hit);
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003068 File Offset: 0x00001268
		public bool CalculatePath(Vector3 targetPosition, NavMeshPath path)
		{
			path.ClearCorners();
			return this.CalculatePathInternal(targetPosition, path);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x0000308C File Offset: 0x0000128C
		[FreeFunction("NavMeshAgentScriptBindings::CalculatePathInternal", HasExplicitThis = true)]
		private bool CalculatePathInternal(Vector3 targetPosition, [NotNull] NavMeshPath path)
		{
			if (path == null)
			{
				ThrowHelper.ThrowArgumentNullException(path, "path");
			}
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			IntPtr intPtr2 = NavMeshPath.BindingsMarshaller.ConvertToNative(path);
			if (intPtr2 == 0)
			{
				ThrowHelper.ThrowArgumentNullException(path, "path");
			}
			return NavMeshAgent.CalculatePathInternal_Injected(intPtr, ref targetPosition, intPtr2);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000030D4 File Offset: 0x000012D4
		public bool SamplePathPosition(int areaMask, float maxDistance, out NavMeshHit hit)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return NavMeshAgent.SamplePathPosition_Injected(intPtr, areaMask, maxDistance, out hit);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000030FC File Offset: 0x000012FC
		[NativeMethod("SetAreaCost")]
		[Obsolete("Use SetAreaCost instead.")]
		public void SetLayerCost(int layer, float cost)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			NavMeshAgent.SetLayerCost_Injected(intPtr, layer, cost);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003120 File Offset: 0x00001320
		[NativeMethod("GetAreaCost")]
		[Obsolete("Use GetAreaCost instead.")]
		public float GetLayerCost(int layer)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return NavMeshAgent.GetLayerCost_Injected(intPtr, layer);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003144 File Offset: 0x00001344
		public void SetAreaCost(int areaIndex, float areaCost)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			NavMeshAgent.SetAreaCost_Injected(intPtr, areaIndex, areaCost);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003168 File Offset: 0x00001368
		public float GetAreaCost(int areaIndex)
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return NavMeshAgent.GetAreaCost_Injected(intPtr, areaIndex);
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600008B RID: 139 RVA: 0x0000318B File Offset: 0x0000138B
		public Object navMeshOwner
		{
			get
			{
				return this.GetOwnerInternal();
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00003194 File Offset: 0x00001394
		// (set) Token: 0x0600008D RID: 141 RVA: 0x000031B8 File Offset: 0x000013B8
		public int agentTypeID
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return NavMeshAgent.get_agentTypeID_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				NavMeshAgent.set_agentTypeID_Injected(intPtr, value);
			}
		}

		// Token: 0x0600008E RID: 142 RVA: 0x000031DC File Offset: 0x000013DC
		[NativeName("GetCurrentPolygonOwner")]
		private Object GetOwnerInternal()
		{
			IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
			if (intPtr == 0)
			{
				ThrowHelper.ThrowNullReferenceException(this);
			}
			return Unmarshal.UnmarshalUnityObject<Object>(NavMeshAgent.GetOwnerInternal_Injected(intPtr));
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00003204 File Offset: 0x00001404
		// (set) Token: 0x06000090 RID: 144 RVA: 0x0000321C File Offset: 0x0000141C
		[Obsolete("Use areaMask instead.")]
		public int walkableMask
		{
			get
			{
				return this.areaMask;
			}
			set
			{
				this.areaMask = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00003228 File Offset: 0x00001428
		// (set) Token: 0x06000092 RID: 146 RVA: 0x0000324C File Offset: 0x0000144C
		public int areaMask
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return NavMeshAgent.get_areaMask_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				NavMeshAgent.set_areaMask_Injected(intPtr, value);
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00003270 File Offset: 0x00001470
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00003294 File Offset: 0x00001494
		public float speed
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return NavMeshAgent.get_speed_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				NavMeshAgent.set_speed_Injected(intPtr, value);
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000095 RID: 149 RVA: 0x000032B8 File Offset: 0x000014B8
		// (set) Token: 0x06000096 RID: 150 RVA: 0x000032DC File Offset: 0x000014DC
		public float angularSpeed
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return NavMeshAgent.get_angularSpeed_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				NavMeshAgent.set_angularSpeed_Injected(intPtr, value);
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003300 File Offset: 0x00001500
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00003324 File Offset: 0x00001524
		public float acceleration
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return NavMeshAgent.get_acceleration_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				NavMeshAgent.set_acceleration_Injected(intPtr, value);
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003348 File Offset: 0x00001548
		// (set) Token: 0x0600009A RID: 154 RVA: 0x0000336C File Offset: 0x0000156C
		public bool updatePosition
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return NavMeshAgent.get_updatePosition_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				NavMeshAgent.set_updatePosition_Injected(intPtr, value);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00003390 File Offset: 0x00001590
		// (set) Token: 0x0600009C RID: 156 RVA: 0x000033B4 File Offset: 0x000015B4
		public bool updateRotation
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return NavMeshAgent.get_updateRotation_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				NavMeshAgent.set_updateRotation_Injected(intPtr, value);
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600009D RID: 157 RVA: 0x000033D8 File Offset: 0x000015D8
		// (set) Token: 0x0600009E RID: 158 RVA: 0x000033FC File Offset: 0x000015FC
		public bool updateUpAxis
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return NavMeshAgent.get_updateUpAxis_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				NavMeshAgent.set_updateUpAxis_Injected(intPtr, value);
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00003420 File Offset: 0x00001620
		// (set) Token: 0x060000A0 RID: 160 RVA: 0x00003444 File Offset: 0x00001644
		public float radius
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return NavMeshAgent.get_radius_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				NavMeshAgent.set_radius_Injected(intPtr, value);
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00003468 File Offset: 0x00001668
		// (set) Token: 0x060000A2 RID: 162 RVA: 0x0000348C File Offset: 0x0000168C
		public float height
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return NavMeshAgent.get_height_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				NavMeshAgent.set_height_Injected(intPtr, value);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x000034B0 File Offset: 0x000016B0
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x000034D4 File Offset: 0x000016D4
		public ObstacleAvoidanceType obstacleAvoidanceType
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return NavMeshAgent.get_obstacleAvoidanceType_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				NavMeshAgent.set_obstacleAvoidanceType_Injected(intPtr, value);
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x000034F8 File Offset: 0x000016F8
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x0000351C File Offset: 0x0000171C
		public int avoidancePriority
		{
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return NavMeshAgent.get_avoidancePriority_Injected(intPtr);
			}
			set
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				NavMeshAgent.set_avoidancePriority_Injected(intPtr, value);
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00003540 File Offset: 0x00001740
		public bool isOnNavMesh
		{
			[NativeName("InCrowdSystem")]
			get
			{
				IntPtr intPtr = Object.MarshalledUnityObject.MarshalNotNull<NavMeshAgent>(this);
				if (intPtr == 0)
				{
					ThrowHelper.ThrowNullReferenceException(this);
				}
				return NavMeshAgent.get_isOnNavMesh_Injected(intPtr);
			}
		}

		// Token: 0x060000A9 RID: 169
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetDestination_Injected(IntPtr _unity_self, [In] ref Vector3 target);

		// Token: 0x060000AA RID: 170
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_destination_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060000AB RID: 171
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_destination_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x060000AC RID: 172
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_stoppingDistance_Injected(IntPtr _unity_self);

		// Token: 0x060000AD RID: 173
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_stoppingDistance_Injected(IntPtr _unity_self, float value);

		// Token: 0x060000AE RID: 174
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_velocity_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060000AF RID: 175
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_velocity_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x060000B0 RID: 176
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_nextPosition_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060000B1 RID: 177
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_nextPosition_Injected(IntPtr _unity_self, [In] ref Vector3 value);

		// Token: 0x060000B2 RID: 178
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_steeringTarget_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060000B3 RID: 179
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_desiredVelocity_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060000B4 RID: 180
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_remainingDistance_Injected(IntPtr _unity_self);

		// Token: 0x060000B5 RID: 181
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_baseOffset_Injected(IntPtr _unity_self);

		// Token: 0x060000B6 RID: 182
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_baseOffset_Injected(IntPtr _unity_self, float value);

		// Token: 0x060000B7 RID: 183
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isOnOffMeshLink_Injected(IntPtr _unity_self);

		// Token: 0x060000B8 RID: 184
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ActivateCurrentOffMeshLink_Injected(IntPtr _unity_self, bool activated);

		// Token: 0x060000B9 RID: 185
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetCurrentOffMeshLinkDataInternal_Injected(IntPtr _unity_self, out OffMeshLinkData ret);

		// Token: 0x060000BA RID: 186
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetNextOffMeshLinkDataInternal_Injected(IntPtr _unity_self, out OffMeshLinkData ret);

		// Token: 0x060000BB RID: 187
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CompleteOffMeshLink_Injected(IntPtr _unity_self);

		// Token: 0x060000BC RID: 188
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_autoTraverseOffMeshLink_Injected(IntPtr _unity_self);

		// Token: 0x060000BD RID: 189
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_autoTraverseOffMeshLink_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000BE RID: 190
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_autoBraking_Injected(IntPtr _unity_self);

		// Token: 0x060000BF RID: 191
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_autoBraking_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000C0 RID: 192
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_autoRepath_Injected(IntPtr _unity_self);

		// Token: 0x060000C1 RID: 193
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_autoRepath_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000C2 RID: 194
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_hasPath_Injected(IntPtr _unity_self);

		// Token: 0x060000C3 RID: 195
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_pathPending_Injected(IntPtr _unity_self);

		// Token: 0x060000C4 RID: 196
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isPathStale_Injected(IntPtr _unity_self);

		// Token: 0x060000C5 RID: 197
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern NavMeshPathStatus get_pathStatus_Injected(IntPtr _unity_self);

		// Token: 0x060000C6 RID: 198
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_pathEndPosition_Injected(IntPtr _unity_self, out Vector3 ret);

		// Token: 0x060000C7 RID: 199
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Warp_Injected(IntPtr _unity_self, [In] ref Vector3 newPosition);

		// Token: 0x060000C8 RID: 200
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Move_Injected(IntPtr _unity_self, [In] ref Vector3 offset);

		// Token: 0x060000C9 RID: 201
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Stop_Injected(IntPtr _unity_self);

		// Token: 0x060000CA RID: 202
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Resume_Injected(IntPtr _unity_self);

		// Token: 0x060000CB RID: 203
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isStopped_Injected(IntPtr _unity_self);

		// Token: 0x060000CC RID: 204
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_isStopped_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000CD RID: 205
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ResetPath_Injected(IntPtr _unity_self);

		// Token: 0x060000CE RID: 206
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetPath_Injected(IntPtr _unity_self, IntPtr path);

		// Token: 0x060000CF RID: 207
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CopyPathTo_Injected(IntPtr _unity_self, IntPtr path);

		// Token: 0x060000D0 RID: 208
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool FindClosestEdge_Injected(IntPtr _unity_self, out NavMeshHit hit);

		// Token: 0x060000D1 RID: 209
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool Raycast_Injected(IntPtr _unity_self, [In] ref Vector3 targetPosition, out NavMeshHit hit);

		// Token: 0x060000D2 RID: 210
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CalculatePathInternal_Injected(IntPtr _unity_self, [In] ref Vector3 targetPosition, IntPtr path);

		// Token: 0x060000D3 RID: 211
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SamplePathPosition_Injected(IntPtr _unity_self, int areaMask, float maxDistance, out NavMeshHit hit);

		// Token: 0x060000D4 RID: 212
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetLayerCost_Injected(IntPtr _unity_self, int layer, float cost);

		// Token: 0x060000D5 RID: 213
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetLayerCost_Injected(IntPtr _unity_self, int layer);

		// Token: 0x060000D6 RID: 214
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetAreaCost_Injected(IntPtr _unity_self, int areaIndex, float areaCost);

		// Token: 0x060000D7 RID: 215
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetAreaCost_Injected(IntPtr _unity_self, int areaIndex);

		// Token: 0x060000D8 RID: 216
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_agentTypeID_Injected(IntPtr _unity_self);

		// Token: 0x060000D9 RID: 217
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_agentTypeID_Injected(IntPtr _unity_self, int value);

		// Token: 0x060000DA RID: 218
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetOwnerInternal_Injected(IntPtr _unity_self);

		// Token: 0x060000DB RID: 219
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_areaMask_Injected(IntPtr _unity_self);

		// Token: 0x060000DC RID: 220
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_areaMask_Injected(IntPtr _unity_self, int value);

		// Token: 0x060000DD RID: 221
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_speed_Injected(IntPtr _unity_self);

		// Token: 0x060000DE RID: 222
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_speed_Injected(IntPtr _unity_self, float value);

		// Token: 0x060000DF RID: 223
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_angularSpeed_Injected(IntPtr _unity_self);

		// Token: 0x060000E0 RID: 224
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_angularSpeed_Injected(IntPtr _unity_self, float value);

		// Token: 0x060000E1 RID: 225
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_acceleration_Injected(IntPtr _unity_self);

		// Token: 0x060000E2 RID: 226
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_acceleration_Injected(IntPtr _unity_self, float value);

		// Token: 0x060000E3 RID: 227
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_updatePosition_Injected(IntPtr _unity_self);

		// Token: 0x060000E4 RID: 228
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_updatePosition_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000E5 RID: 229
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_updateRotation_Injected(IntPtr _unity_self);

		// Token: 0x060000E6 RID: 230
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_updateRotation_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000E7 RID: 231
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_updateUpAxis_Injected(IntPtr _unity_self);

		// Token: 0x060000E8 RID: 232
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_updateUpAxis_Injected(IntPtr _unity_self, bool value);

		// Token: 0x060000E9 RID: 233
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_radius_Injected(IntPtr _unity_self);

		// Token: 0x060000EA RID: 234
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_radius_Injected(IntPtr _unity_self, float value);

		// Token: 0x060000EB RID: 235
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float get_height_Injected(IntPtr _unity_self);

		// Token: 0x060000EC RID: 236
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_height_Injected(IntPtr _unity_self, float value);

		// Token: 0x060000ED RID: 237
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ObstacleAvoidanceType get_obstacleAvoidanceType_Injected(IntPtr _unity_self);

		// Token: 0x060000EE RID: 238
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_obstacleAvoidanceType_Injected(IntPtr _unity_self, ObstacleAvoidanceType value);

		// Token: 0x060000EF RID: 239
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int get_avoidancePriority_Injected(IntPtr _unity_self);

		// Token: 0x060000F0 RID: 240
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void set_avoidancePriority_Injected(IntPtr _unity_self, int value);

		// Token: 0x060000F1 RID: 241
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool get_isOnNavMesh_Injected(IntPtr _unity_self);
	}
}
