using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Services.Core.Internal;
using UnityEngine.Scripting;

namespace Unity.Services.Qos.Internal
{
	// Token: 0x0200000A RID: 10
	[RequireImplementors]
	public interface IQosResults : IServiceComponent
	{
		// Token: 0x06000013 RID: 19
		Task<IList<QosResult>> GetSortedQosResultsAsync(string service, IList<string> regions);
	}
}
