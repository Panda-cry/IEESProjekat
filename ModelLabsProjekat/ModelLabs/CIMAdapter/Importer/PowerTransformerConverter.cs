namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	using FTN.Common;

	/// <summary>
	/// PowerTransformerConverter has methods for populating
	/// ResourceDescription objects using PowerTransformerCIMProfile_Labs objects.
	/// </summary>
	public static class PowerTransformerConverter
	{

		#region Populate ResourceDescription
		public static void PopulateIdentifiedObjectProperties(FTN.IdentifiedObject cimIdentifiedObject, ResourceDescription rd)
		{
			if ((cimIdentifiedObject != null) && (rd != null))
			{
				if (cimIdentifiedObject.MRIDHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_MRID, cimIdentifiedObject.MRID));
				}
				if (cimIdentifiedObject.NameHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_NAME, cimIdentifiedObject.Name));
				}
				if (cimIdentifiedObject.AliasNameHasValue)
				{
					rd.AddProperty(new Property(ModelCode.IDOBJ_ALIASNAME, cimIdentifiedObject.AliasName));
				}
			}
		}

		public static void PopulateAssetInfoProperties(FTN.AssetInfo assetInfo, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((assetInfo != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(assetInfo, rd);
			}
		}


		public static void PopulateAssetModelProperties(FTN.AssetModel assetModel, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((assetModel != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(assetModel, rd);

				if (assetModel.AssetInfoHasValue)
				{
					long gid = importHelper.GetMappedGID(assetModel.AssetInfo.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(assetModel.GetType().ToString()).Append(" rdfID = \"").Append(assetModel.ID);
						report.Report.Append("\" - Failed to set reference to AssetModel: rdfID \"").Append(assetModel.AssetInfo.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.ASSETMODEL_ASSETINFO, gid));
				}
			}
		}


		public static void PopulateProductAssetModelProperties(FTN.ProductAssetModel cimPowerSystemResource, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((cimPowerSystemResource != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateAssetModelProperties(cimPowerSystemResource, rd,importHelper,report);

				if (cimPowerSystemResource.CorporateStandardKindHasValue)
				{
					rd.AddProperty(new Property(ModelCode.PRODUCTASSET_MODEL_STANDARDKIND, (short)cimPowerSystemResource.CorporateStandardKind));
				}
				if (cimPowerSystemResource.UsageKindHasValue)
				{
					rd.AddProperty(new Property(ModelCode.PRODUCTASSET_MODEL_USERKIND, (short)cimPowerSystemResource.UsageKind));
				}
				if (cimPowerSystemResource.ModelNumberHasValue)
				{
					rd.AddProperty(new Property(ModelCode.PRODUCTASSET_MODEL_NUMBER, cimPowerSystemResource.ModelNumber));
				}
				if (cimPowerSystemResource.ModelVersionHasValue)
				{
					rd.AddProperty(new Property(ModelCode.PRODUCTASSET_MODEL_VERSION, cimPowerSystemResource.ModelVersion));
				}
				if (cimPowerSystemResource.WeightTotalHasValue)
				{
					rd.AddProperty(new Property(ModelCode.PRODUCTASSET_MODEL_WEIGHT, cimPowerSystemResource.WeightTotal));
				}
			}
		}
		public static void PopulateAssetOrganisationRoleProperties(FTN.AssetOrganisationRole assetInfo, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((assetInfo != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateOrganisationRoleProperties(assetInfo, rd,importHelper,report);

			}
		}
		public static void PopulateOrganisationRoleProperties(FTN.OrganisationRole assetInfo, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((assetInfo != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(assetInfo, rd);

			}
		}

		public static void PopulateOwnerProperties(FTN.AssetOwner assetInfo, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((assetInfo != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateAssetOrganisationRoleProperties(assetInfo, rd,importHelper,report);

			}
		}

		public static void PopulateAssetContainerProperties(FTN.AssetContainer assetInfo, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((assetInfo != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateAssetProperties(assetInfo, rd,importHelper,report);

			}
		}

		public static void PopulateComMediaProperties(FTN.ComMedia assetInfo, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((assetInfo != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateAssetProperties(assetInfo, rd,importHelper,report);

			}
		}

		public static void PopulateSealProperties(FTN.Seal seal, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((seal != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(seal, rd);

                if (seal.AppliedDateTimeHasValue)
                {
					rd.AddProperty(new Property(ModelCode.SEAL_APPLIEDDATETIME, seal.AppliedDateTime));
				}
				if (seal.SealNumberHasValue)
				{
					rd.AddProperty(new Property(ModelCode.SEAL_SEALNUMBER, seal.SealNumber));
				}
				if (seal.KindHasValue)
				{
					rd.AddProperty(new Property(ModelCode.SEAL_KIND, (short)seal.Kind));
				}
				if (seal.ConditionHasValue)
				{
					rd.AddProperty(new Property(ModelCode.SEAL_CONDITION,(short) seal.Condition));
				}
				if (seal.AssetContainerHasValue)
				{
					long gid = importHelper.GetMappedGID(seal.AssetContainer.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(seal.GetType().ToString()).Append(" rdfID = \"").Append(seal.ID);
						report.Report.Append("\" - Failed to set reference to Location: rdfID \"").Append(seal.AssetContainer.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.SEAL_ASSETCONTAINER, gid));
				}
			}
		}
		public static void PopulateAssetProperties(FTN.Asset assetInfo, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
		{
			if ((assetInfo != null) && (rd != null))
			{
				PowerTransformerConverter.PopulateIdentifiedObjectProperties(assetInfo, rd);
				if (assetInfo.AcceptanceTestHasValue)
				{
					rd.AddProperty(new Property(ModelCode.ASSET_ACCEPTENCETEST, assetInfo.AcceptanceTest));
				}
                if (assetInfo.CriticalHasValue)
                {
					rd.AddProperty(new Property(ModelCode.ASSET_CRTITICAL, assetInfo.Critical));
				}
				if (assetInfo.ElectronicAddressHasValue)
				{
					rd.AddProperty(new Property(ModelCode.ASSET_ELECTRONICADDRESS, assetInfo.ElectronicAddress));
				}
				if (assetInfo.InitialConditionHasValue)
				{
					rd.AddProperty(new Property(ModelCode.ASSET_INITIALCONDITION, assetInfo.InitialCondition));
				}
				if (assetInfo.InitialLossOfLifeHasValue)
				{
					rd.AddProperty(new Property(ModelCode.ASSET_IITIALOSTOFLIFE, assetInfo.InitialLossOfLife));
				}
				if (assetInfo.LifecycleHasValue)
				{
					rd.AddProperty(new Property(ModelCode.ASSET_LIFECYCLE, assetInfo.Lifecycle));
				}
				if (assetInfo.LotNumberHasValue)
				{
					rd.AddProperty(new Property(ModelCode.ASSET_LOTNUMBER, assetInfo.LotNumber));
				}
				if (assetInfo.PurchasePriceHasValue)
				{
					rd.AddProperty(new Property(ModelCode.ASSET_PURCHASEPRICE, assetInfo.PurchasePrice));
				}
				if (assetInfo.SerialNumberHasValue)
				{
					rd.AddProperty(new Property(ModelCode.ASSET_SERIALNUMBER, assetInfo.SerialNumber));
				}
				if (assetInfo.StatusHasValue)
				{
					rd.AddProperty(new Property(ModelCode.ASSET_STATUS, assetInfo.Status));
				}
				if (assetInfo.TypeHasValue)
				{
					rd.AddProperty(new Property(ModelCode.ASSET_TYPE, assetInfo.Type));
				}
				if (assetInfo.UtcNumberHasValue)
				{
					rd.AddProperty(new Property(ModelCode.ASSET_UTCNUMBER, assetInfo.UtcNumber));
				}
				if (assetInfo.AssetContainerHasValue)
				{
					long gid = importHelper.GetMappedGID(assetInfo.AssetContainer.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(assetInfo.GetType().ToString()).Append(" rdfID = \"").Append(assetInfo.ID);
						report.Report.Append("\" - Failed to set reference to Asset: rdfID \"").Append(assetInfo.AssetContainer.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.ASSET_ASSETCONTAINER, gid));
				}
				if (assetInfo.AssetInfoHasValue)
				{
					long gid = importHelper.GetMappedGID(assetInfo.AssetInfo.ID);
					if (gid < 0)
					{
						report.Report.Append("WARNING: Convert ").Append(assetInfo.GetType().ToString()).Append(" rdfID = \"").Append(assetInfo.ID);
						report.Report.Append("\" - Failed to set reference to Asset: rdfID \"").Append(assetInfo.AssetInfo.ID).AppendLine(" \" is not mapped to GID!");
					}
					rd.AddProperty(new Property(ModelCode.ASSET_ASSETINFO, gid));
				}


			}
		}




		#endregion Populate ResourceDescription

		#region Enums convert
		public static CorporateStandardKind GetDMSCorporateStandardKind(FTN.CorporateStandardKind phases)
		{
			switch (phases)
			{
				case FTN.CorporateStandardKind.experimental:
					return CorporateStandardKind.experimental;
				case FTN.CorporateStandardKind.standard:
					return CorporateStandardKind.standard;
				case FTN.CorporateStandardKind.underEvaluation:
					return CorporateStandardKind.underEvaluation;
				default: return CorporateStandardKind.other;
			}
		}

		public static AssetModelUsageKind GetDMSAssetUserKind(FTN.AssetModelUsageKind transformerFunction)
		{
			switch (transformerFunction)
			{
				case FTN.AssetModelUsageKind.customerSubstation:
					return AssetModelUsageKind.customerSubstation;
				case FTN.AssetModelUsageKind.distributionOverhead:
					return AssetModelUsageKind.distributionOverhead;
				case FTN.AssetModelUsageKind.distributionUnderground:
					return AssetModelUsageKind.distributionUnderground;
				case FTN.AssetModelUsageKind.streetlight:
					return AssetModelUsageKind.streetlight;
				case FTN.AssetModelUsageKind.substation:
					return AssetModelUsageKind.substation;
				case FTN.AssetModelUsageKind.transmission:
					return AssetModelUsageKind.transmission;
				case FTN.AssetModelUsageKind.other:
					return AssetModelUsageKind.other;
			
				default:
					return AssetModelUsageKind.unknown;
			}
		}

		public static SealKind GetDMSSealKind(FTN.SealKind windingType)
		{
			switch (windingType)
			{
				case FTN.SealKind.lead:
					return SealKind.lead;
				case FTN.SealKind.@lock:
					return SealKind.@lock;

				case FTN.SealKind.steel:
					return SealKind.steel;
				default:
					return SealKind.other;
			}
		}

		public static SealConditionKind GetDMSWindingConnection(FTN.SealConditionKind windingConnection)
		{
			switch (windingConnection)
			{
				case FTN.SealConditionKind.broken:
					return SealConditionKind.broken;
				case FTN.SealConditionKind.locked:
					return SealConditionKind.locked;
				case FTN.SealConditionKind.missing:
					return SealConditionKind.missing;

				case FTN.SealConditionKind.open:
					return SealConditionKind.open;
		
				default:
					return SealConditionKind.other;
			}
		}
		#endregion Enums convert
	}
}
