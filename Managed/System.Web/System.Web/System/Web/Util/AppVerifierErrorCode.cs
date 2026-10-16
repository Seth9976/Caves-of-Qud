using System;

namespace System.Web.Util
{
	// Token: 0x02000109 RID: 265
	internal enum AppVerifierErrorCode
	{
		// Token: 0x04001173 RID: 4467
		Ok,
		// Token: 0x04001174 RID: 4468
		HttpApplicationInstanceWasNull,
		// Token: 0x04001175 RID: 4469
		BeginHandlerDelegateWasNull,
		// Token: 0x04001176 RID: 4470
		AsyncCallbackInvokedMultipleTimes,
		// Token: 0x04001177 RID: 4471
		AsyncCallbackInvokedWithNullParameter,
		// Token: 0x04001178 RID: 4472
		AsyncCallbackGivenAsyncResultWhichWasNotCompleted,
		// Token: 0x04001179 RID: 4473
		AsyncCallbackInvokedSynchronouslyButAsyncResultWasNotMarkedCompletedSynchronously,
		// Token: 0x0400117A RID: 4474
		AsyncCallbackInvokedAsynchronouslyButAsyncResultWasMarkedCompletedSynchronously,
		// Token: 0x0400117B RID: 4475
		AsyncCallbackInvokedWithUnexpectedAsyncResultInstance,
		// Token: 0x0400117C RID: 4476
		AsyncCallbackInvokedAsynchronouslyThenBeginHandlerThrew,
		// Token: 0x0400117D RID: 4477
		BeginHandlerThrewThenAsyncCallbackInvokedAsynchronously,
		// Token: 0x0400117E RID: 4478
		AsyncCallbackInvokedSynchronouslyThenBeginHandlerThrew,
		// Token: 0x0400117F RID: 4479
		AsyncCallbackInvokedWithUnexpectedAsyncResultAsyncState,
		// Token: 0x04001180 RID: 4480
		AsyncCallbackCalledAfterHttpApplicationReassigned,
		// Token: 0x04001181 RID: 4481
		BeginHandlerReturnedNull,
		// Token: 0x04001182 RID: 4482
		BeginHandlerReturnedAsyncResultMarkedCompletedSynchronouslyButWhichWasNotCompleted,
		// Token: 0x04001183 RID: 4483
		BeginHandlerReturnedAsyncResultMarkedCompletedSynchronouslyButAsyncCallbackNeverCalled,
		// Token: 0x04001184 RID: 4484
		BeginHandlerReturnedUnexpectedAsyncResultInstance,
		// Token: 0x04001185 RID: 4485
		BeginHandlerReturnedUnexpectedAsyncResultAsyncState,
		// Token: 0x04001186 RID: 4486
		SyncContextSendOrPostCalledAfterRequestCompleted,
		// Token: 0x04001187 RID: 4487
		SyncContextSendOrPostCalledBetweenNotifications,
		// Token: 0x04001188 RID: 4488
		SyncContextPostCalledInNestedNotification,
		// Token: 0x04001189 RID: 4489
		RequestNotificationCompletedSynchronouslyWithNotificationContextPending,
		// Token: 0x0400118A RID: 4490
		NotificationContextHasChangedAfterSynchronouslyProcessingNotification,
		// Token: 0x0400118B RID: 4491
		PendingProcessRequestNotificationStatusAfterCompletingNestedNotification
	}
}
