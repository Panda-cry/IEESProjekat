using System;
using System.Collections.Generic;
using CIM.Model;
using FTN.Common;
using FTN.ESI.SIMES.CIM.CIMAdapter.Manager;

namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	/// <summary>
	/// PowerTransformerImporter
	/// </summary>
	public class PowerTransformerImporter
	{
		/// <summary> Singleton </summary>
		private static PowerTransformerImporter ptImporter = null;
		private static object singletoneLock = new object();

		private ConcreteModel concreteModel;
		private Delta delta;
		private ImportHelper importHelper;
		private TransformAndLoadReport report;


		#region Properties
		public static PowerTransformerImporter Instance
		{
			get
			{
				if (ptImporter == null)
				{
					lock (singletoneLock)
					{
						if (ptImporter == null)
						{
							ptImporter = new PowerTransformerImporter();
							ptImporter.Reset();
						}
					}
				}
				return ptImporter;
			}
		}

		public Delta NMSDelta
		{
			get 
			{
				return delta;
			}
		}
		#endregion Properties


		public void Reset()
		{
			concreteModel = null;
			delta = new Delta();
			importHelper = new ImportHelper();
			report = null;
		}

		public TransformAndLoadReport CreateNMSDelta(ConcreteModel cimConcreteModel)
		{
			LogManager.Log("Importing PowerTransformer Elements...", LogLevel.Info);
			report = new TransformAndLoadReport();
			concreteModel = cimConcreteModel;
			delta.ClearDeltaOperations();

			if ((concreteModel != null) && (concreteModel.ModelMap != null))
			{
				try
				{
					// convert into DMS elements
					ConvertModelAndPopulateDelta();
				}
				catch (Exception ex)
				{
					string message = string.Format("{0} - ERROR in data import - {1}", DateTime.Now, ex.Message);
					LogManager.Log(message);
					report.Report.AppendLine(ex.Message);
					report.Success = false;
				}
			}
			LogManager.Log("Importing PowerTransformer Elements - END.", LogLevel.Info);
			return report;
		}

		/// <summary>
		/// Method performs conversion of network elements from CIM based concrete model into DMS model.
		/// </summary>
		private void ConvertModelAndPopulateDelta()
		{
			LogManager.Log("Loading elements and creating delta...", LogLevel.Info);

			//// import all concrete model types (DMSType enum)

			ImportAssetInfo();
			ImportProductAssetModels();
			ImportAssetOwners();
			ImportAssetContainers();
			ImportAssets();
			ImportSeals();
			ImportComMedia();

			LogManager.Log("Loading elements and creating delta completed.", LogLevel.Info);
		}

		#region Import


		private void ImportAssetInfo()
        {
			SortedDictionary<string, object> cimBaseInfos = concreteModel.GetAllObjectsOfType("FTN.AssetInfo");
			if (cimBaseInfos != null)
			{
				foreach (KeyValuePair<string, object> cimBaseVoltagePair in cimBaseInfos)
				{
					FTN.AssetInfo cimBaseVoltage = cimBaseVoltagePair.Value as FTN.AssetInfo;

					ResourceDescription rd = CreateAssetInfoResourceDescription(cimBaseVoltage);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("AssetInfo ID = ").Append(cimBaseVoltage.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("AssetInfo ID = ").Append(cimBaseVoltage.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreateAssetInfoResourceDescription(FTN.AssetInfo cimBaseVoltage)
		{
			ResourceDescription rd = null;
			if (cimBaseVoltage != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.ASSET_INFO, importHelper.CheckOutIndexForDMSType(DMSType.ASSET_INFO));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimBaseVoltage.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateAssetInfoProperties(cimBaseVoltage, rd,importHelper,report);
			}
			return rd;
		}

		private void ImportProductAssetModels()
		{
			SortedDictionary<string, object> cimBaseInfos = concreteModel.GetAllObjectsOfType("FTN.ProductAssetModel");
			if (cimBaseInfos != null)
			{
				foreach (KeyValuePair<string, object> cimBaseVoltagePair in cimBaseInfos)
				{
					FTN.ProductAssetModel cimBaseVoltage = cimBaseVoltagePair.Value as FTN.ProductAssetModel;

					ResourceDescription rd = CreateProductAssetModelsDescription(cimBaseVoltage);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("ProductModel ID = ").Append(cimBaseVoltage.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("ProductModel ID = ").Append(cimBaseVoltage.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreateProductAssetModelsDescription(FTN.ProductAssetModel cimBaseVoltage)
		{
			ResourceDescription rd = null;
			if (cimBaseVoltage != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.PRODUCTASSET_MODEL, importHelper.CheckOutIndexForDMSType(DMSType.PRODUCTASSET_MODEL));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimBaseVoltage.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateProductAssetModelProperties(cimBaseVoltage, rd, importHelper, report);
			}
			return rd;
		}

		private void ImportAssetOwners()
		{

			SortedDictionary<string, object> cimBaseInfos = concreteModel.GetAllObjectsOfType("FTN.AssetOwner");
			if (cimBaseInfos != null)
			{
				foreach (KeyValuePair<string, object> cimBaseVoltagePair in cimBaseInfos)
				{
					FTN.AssetOwner cimBaseVoltage = cimBaseVoltagePair.Value as FTN.AssetOwner;

					ResourceDescription rd = CreateAssetOwnersDescription(cimBaseVoltage);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("AssetOwner ID = ").Append(cimBaseVoltage.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("AssetOwner ID = ").Append(cimBaseVoltage.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreateAssetOwnersDescription(FTN.AssetOwner cimBaseVoltage)
		{
			ResourceDescription rd = null;
			if (cimBaseVoltage != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.ASSET_OWNER, importHelper.CheckOutIndexForDMSType(DMSType.ASSET_OWNER));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimBaseVoltage.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateOwnerProperties(cimBaseVoltage, rd, importHelper, report);
			}
			return rd;
		}


		private void ImportAssetContainers()
		{

			SortedDictionary<string, object> cimBaseInfos = concreteModel.GetAllObjectsOfType("FTN.AssetContainer");
			if (cimBaseInfos != null)
			{
				foreach (KeyValuePair<string, object> cimBaseVoltagePair in cimBaseInfos)
				{
					FTN.AssetContainer cimBaseVoltage = cimBaseVoltagePair.Value as FTN.AssetContainer;

					ResourceDescription rd = CreateAssetContainerDescription(cimBaseVoltage);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("AssetContainer ID = ").Append(cimBaseVoltage.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("AssetContainer ID = ").Append(cimBaseVoltage.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreateAssetContainerDescription(FTN.AssetContainer cimBaseVoltage)
		{
			ResourceDescription rd = null;
			if (cimBaseVoltage != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.ASSET_CONTAINER, importHelper.CheckOutIndexForDMSType(DMSType.ASSET_CONTAINER));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimBaseVoltage.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateAssetContainerProperties(cimBaseVoltage, rd, importHelper, report);
			}
			return rd;
		}

		private void ImportAssets()
		{

			SortedDictionary<string, object> cimBaseInfos = concreteModel.GetAllObjectsOfType("FTN.Asset");
			if (cimBaseInfos != null)
			{
				foreach (KeyValuePair<string, object> cimBaseVoltagePair in cimBaseInfos)
				{
					FTN.Asset cimBaseVoltage = cimBaseVoltagePair.Value as FTN.Asset;

					ResourceDescription rd = CreateAssetsDescription(cimBaseVoltage);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("Asset ID = ").Append(cimBaseVoltage.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("Asset ID = ").Append(cimBaseVoltage.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreateAssetsDescription(FTN.Asset cimBaseVoltage)
		{
			ResourceDescription rd = null;
			if (cimBaseVoltage != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.ASSET, importHelper.CheckOutIndexForDMSType(DMSType.ASSET));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimBaseVoltage.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateAssetProperties(cimBaseVoltage, rd, importHelper, report);
			}
			return rd;
		}


		private void ImportSeals()
		{

			SortedDictionary<string, object> cimBaseInfos = concreteModel.GetAllObjectsOfType("FTN.Seal");
			if (cimBaseInfos != null)
			{
				foreach (KeyValuePair<string, object> cimBaseVoltagePair in cimBaseInfos)
				{
					FTN.Seal cimBaseVoltage = cimBaseVoltagePair.Value as FTN.Seal;

					ResourceDescription rd = CreateSealsDescription(cimBaseVoltage);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("Seal ID = ").Append(cimBaseVoltage.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("Seal ID = ").Append(cimBaseVoltage.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreateSealsDescription(FTN.Seal cimBaseVoltage)
		{
			ResourceDescription rd = null;
			if (cimBaseVoltage != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.SEAL, importHelper.CheckOutIndexForDMSType(DMSType.SEAL));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimBaseVoltage.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateSealProperties(cimBaseVoltage, rd, importHelper, report);
			}
			return rd;
		}



		private void ImportComMedia()
		{

			SortedDictionary<string, object> cimBaseInfos = concreteModel.GetAllObjectsOfType("FTN.ComMedia");
			if (cimBaseInfos != null)
			{
				foreach (KeyValuePair<string, object> cimBaseVoltagePair in cimBaseInfos)
				{
					FTN.ComMedia cimBaseVoltage = cimBaseVoltagePair.Value as FTN.ComMedia;

					ResourceDescription rd = CreateComMediaDescription(cimBaseVoltage);
					if (rd != null)
					{
						delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
						report.Report.Append("ComMedia ID = ").Append(cimBaseVoltage.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
					}
					else
					{
						report.Report.Append("ComMedia ID = ").Append(cimBaseVoltage.ID).AppendLine(" FAILED to be converted");
					}
				}
				report.Report.AppendLine();
			}
		}

		private ResourceDescription CreateComMediaDescription(FTN.ComMedia cimBaseVoltage)
		{
			ResourceDescription rd = null;
			if (cimBaseVoltage != null)
			{
				long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.COMMEDIA, importHelper.CheckOutIndexForDMSType(DMSType.COMMEDIA));
				rd = new ResourceDescription(gid);
				importHelper.DefineIDMapping(cimBaseVoltage.ID, gid);

				////populate ResourceDescription
				PowerTransformerConverter.PopulateComMediaProperties(cimBaseVoltage, rd, importHelper, report);
			}
			return rd;
		}



		#endregion Import
	}
}

