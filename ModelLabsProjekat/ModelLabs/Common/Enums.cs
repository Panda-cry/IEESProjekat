using System;

namespace FTN.Common
{
	public enum SealConditionKind
	{
		broken,
		locked,
		missing,
		open,
		other
	}
	public enum SealKind
	{
		lead,
		@lock,
		other,
		steel
	}
	public enum CorporateStandardKind
	{
		experimental,
		other,
		standard,
		underEvaluation
	}
	public enum AssetModelUsageKind
	{
		customerSubstation,
		distributionOverhead,
		distributionUnderground,
		other,
		streetlight,
		substation,
		transmission,
		unknown
	}
}
