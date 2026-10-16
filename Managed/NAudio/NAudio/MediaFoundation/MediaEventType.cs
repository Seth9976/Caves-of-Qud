using System;

namespace NAudio.MediaFoundation
{
	// Token: 0x0200004D RID: 77
	public enum MediaEventType
	{
		// Token: 0x0400023F RID: 575
		MEUnknown,
		// Token: 0x04000240 RID: 576
		MEError,
		// Token: 0x04000241 RID: 577
		MEExtendedType,
		// Token: 0x04000242 RID: 578
		MENonFatalError,
		// Token: 0x04000243 RID: 579
		MESessionUnknown = 100,
		// Token: 0x04000244 RID: 580
		MESessionTopologySet,
		// Token: 0x04000245 RID: 581
		MESessionTopologiesCleared,
		// Token: 0x04000246 RID: 582
		MESessionStarted,
		// Token: 0x04000247 RID: 583
		MESessionPaused,
		// Token: 0x04000248 RID: 584
		MESessionStopped,
		// Token: 0x04000249 RID: 585
		MESessionClosed,
		// Token: 0x0400024A RID: 586
		MESessionEnded,
		// Token: 0x0400024B RID: 587
		MESessionRateChanged,
		// Token: 0x0400024C RID: 588
		MESessionScrubSampleComplete,
		// Token: 0x0400024D RID: 589
		MESessionCapabilitiesChanged,
		// Token: 0x0400024E RID: 590
		MESessionTopologyStatus,
		// Token: 0x0400024F RID: 591
		MESessionNotifyPresentationTime,
		// Token: 0x04000250 RID: 592
		MENewPresentation,
		// Token: 0x04000251 RID: 593
		MELicenseAcquisitionStart,
		// Token: 0x04000252 RID: 594
		MELicenseAcquisitionCompleted,
		// Token: 0x04000253 RID: 595
		MEIndividualizationStart,
		// Token: 0x04000254 RID: 596
		MEIndividualizationCompleted,
		// Token: 0x04000255 RID: 597
		MEEnablerProgress,
		// Token: 0x04000256 RID: 598
		MEEnablerCompleted,
		// Token: 0x04000257 RID: 599
		MEPolicyError,
		// Token: 0x04000258 RID: 600
		MEPolicyReport,
		// Token: 0x04000259 RID: 601
		MEBufferingStarted,
		// Token: 0x0400025A RID: 602
		MEBufferingStopped,
		// Token: 0x0400025B RID: 603
		MEConnectStart,
		// Token: 0x0400025C RID: 604
		MEConnectEnd,
		// Token: 0x0400025D RID: 605
		MEReconnectStart,
		// Token: 0x0400025E RID: 606
		MEReconnectEnd,
		// Token: 0x0400025F RID: 607
		MERendererEvent,
		// Token: 0x04000260 RID: 608
		MESessionStreamSinkFormatChanged,
		// Token: 0x04000261 RID: 609
		MESourceUnknown = 200,
		// Token: 0x04000262 RID: 610
		MESourceStarted,
		// Token: 0x04000263 RID: 611
		MEStreamStarted,
		// Token: 0x04000264 RID: 612
		MESourceSeeked,
		// Token: 0x04000265 RID: 613
		MEStreamSeeked,
		// Token: 0x04000266 RID: 614
		MENewStream,
		// Token: 0x04000267 RID: 615
		MEUpdatedStream,
		// Token: 0x04000268 RID: 616
		MESourceStopped,
		// Token: 0x04000269 RID: 617
		MEStreamStopped,
		// Token: 0x0400026A RID: 618
		MESourcePaused,
		// Token: 0x0400026B RID: 619
		MEStreamPaused,
		// Token: 0x0400026C RID: 620
		MEEndOfPresentation,
		// Token: 0x0400026D RID: 621
		MEEndOfStream,
		// Token: 0x0400026E RID: 622
		MEMediaSample,
		// Token: 0x0400026F RID: 623
		MEStreamTick,
		// Token: 0x04000270 RID: 624
		MEStreamThinMode,
		// Token: 0x04000271 RID: 625
		MEStreamFormatChanged,
		// Token: 0x04000272 RID: 626
		MESourceRateChanged,
		// Token: 0x04000273 RID: 627
		MEEndOfPresentationSegment,
		// Token: 0x04000274 RID: 628
		MESourceCharacteristicsChanged,
		// Token: 0x04000275 RID: 629
		MESourceRateChangeRequested,
		// Token: 0x04000276 RID: 630
		MESourceMetadataChanged,
		// Token: 0x04000277 RID: 631
		MESequencerSourceTopologyUpdated,
		// Token: 0x04000278 RID: 632
		MESinkUnknown = 300,
		// Token: 0x04000279 RID: 633
		MEStreamSinkStarted,
		// Token: 0x0400027A RID: 634
		MEStreamSinkStopped,
		// Token: 0x0400027B RID: 635
		MEStreamSinkPaused,
		// Token: 0x0400027C RID: 636
		MEStreamSinkRateChanged,
		// Token: 0x0400027D RID: 637
		MEStreamSinkRequestSample,
		// Token: 0x0400027E RID: 638
		MEStreamSinkMarker,
		// Token: 0x0400027F RID: 639
		MEStreamSinkPrerolled,
		// Token: 0x04000280 RID: 640
		MEStreamSinkScrubSampleComplete,
		// Token: 0x04000281 RID: 641
		MEStreamSinkFormatChanged,
		// Token: 0x04000282 RID: 642
		MEStreamSinkDeviceChanged,
		// Token: 0x04000283 RID: 643
		MEQualityNotify,
		// Token: 0x04000284 RID: 644
		MESinkInvalidated,
		// Token: 0x04000285 RID: 645
		MEAudioSessionNameChanged,
		// Token: 0x04000286 RID: 646
		MEAudioSessionVolumeChanged,
		// Token: 0x04000287 RID: 647
		MEAudioSessionDeviceRemoved,
		// Token: 0x04000288 RID: 648
		MEAudioSessionServerShutdown,
		// Token: 0x04000289 RID: 649
		MEAudioSessionGroupingParamChanged,
		// Token: 0x0400028A RID: 650
		MEAudioSessionIconChanged,
		// Token: 0x0400028B RID: 651
		MEAudioSessionFormatChanged,
		// Token: 0x0400028C RID: 652
		MEAudioSessionDisconnected,
		// Token: 0x0400028D RID: 653
		MEAudioSessionExclusiveModeOverride,
		// Token: 0x0400028E RID: 654
		METrustUnknown = 400,
		// Token: 0x0400028F RID: 655
		MEPolicyChanged,
		// Token: 0x04000290 RID: 656
		MEContentProtectionMessage,
		// Token: 0x04000291 RID: 657
		MEPolicySet,
		// Token: 0x04000292 RID: 658
		MEWMDRMLicenseBackupCompleted = 500,
		// Token: 0x04000293 RID: 659
		MEWMDRMLicenseBackupProgress,
		// Token: 0x04000294 RID: 660
		MEWMDRMLicenseRestoreCompleted,
		// Token: 0x04000295 RID: 661
		MEWMDRMLicenseRestoreProgress,
		// Token: 0x04000296 RID: 662
		MEWMDRMLicenseAcquisitionCompleted = 506,
		// Token: 0x04000297 RID: 663
		MEWMDRMIndividualizationCompleted = 508,
		// Token: 0x04000298 RID: 664
		MEWMDRMIndividualizationProgress = 513,
		// Token: 0x04000299 RID: 665
		MEWMDRMProximityCompleted,
		// Token: 0x0400029A RID: 666
		MEWMDRMLicenseStoreCleaned,
		// Token: 0x0400029B RID: 667
		MEWMDRMRevocationDownloadCompleted,
		// Token: 0x0400029C RID: 668
		METransformUnknown = 600,
		// Token: 0x0400029D RID: 669
		METransformNeedInput,
		// Token: 0x0400029E RID: 670
		METransformHaveOutput,
		// Token: 0x0400029F RID: 671
		METransformDrainComplete,
		// Token: 0x040002A0 RID: 672
		METransformMarker
	}
}
