using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class AssetInfo : IdentifiedObject
    {
		private List<long> models = new List<long>();
		private List<long> assets = new List<long>();
		public AssetInfo(long globalId) : base(globalId)
		{
		}

		public List<long> Models
        {
            get { return models; }
            set { models = value; }
        }
		public override bool IsReferenced
		{
			get
			{
				return models.Count != 0 || assets.Count != 0 || base.IsReferenced;
			}
		}
		public override bool HasProperty(ModelCode property)
		{
			switch (property)
			{
				case ModelCode.ASSETINFO_ASSETMODEL:
					return true;
				case ModelCode.ASSETINFO_ASSETS:
					return true;

				default:
					return base.HasProperty(property);
			}
		}

		public override void GetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.ASSETINFO_ASSETMODEL:
					property.SetValue(models);
					break;
				case ModelCode.ASSETINFO_ASSETS:
					property.SetValue(assets);
					break;



				default:
					base.GetProperty(property);
					break;
			}
		}

		public override void SetProperty(Property property)
		{
			switch (property.Id)
			{
				default:
					base.SetProperty(property);
					break;
			}
		}



		public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
		{
			
			if (models != null && models.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
			{
				references[ModelCode.ASSETINFO_ASSETMODEL] = models.GetRange(0, models.Count);
			}
			if (assets != null && assets.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
			{
				references[ModelCode.ASSETINFO_ASSETS] = models.GetRange(0, assets.Count);
			}


			base.GetReferences(references, refType);
		}

		public override void AddReference(ModelCode referenceId, long globalId)
		{
			switch (referenceId)
			{
				case ModelCode.ASSETMODEL_ASSETINFO:
					models.Add(globalId);
					break;
				case ModelCode.ASSET_ASSETINFO:
					assets.Add(globalId);
					break;
				default:
					base.AddReference(referenceId, globalId);
					break;
			}
		}

		public override void RemoveReference(ModelCode referenceId, long globalId)
		{
			switch (referenceId)
			{
				case ModelCode.ASSETMODEL_ASSETINFO:

					if (models.Contains(globalId))
					{
						models.Remove(globalId);
					}
					else
					{
						CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
					}

					break;
				case ModelCode.ASSET_ASSETINFO:

					if (assets.Contains(globalId))
					{
						assets.Remove(globalId);
					}
					else
					{
						CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
					}

					break;

				default:
					base.RemoveReference(referenceId, globalId);
					break;
			}
		}
	}
}
