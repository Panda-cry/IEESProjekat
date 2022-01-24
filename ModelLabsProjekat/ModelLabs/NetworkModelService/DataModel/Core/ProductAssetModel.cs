using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
   
    public class ProductAssetModel : AssetModel
    {
        private CorporateStandardKind corporateStandardKind;
        private string modelnumber;
        private string modelVersion;
        private AssetModelUsageKind userKind;
        private float weight;

        public ProductAssetModel(long globalId) : base(globalId)
        {
        }

        public CorporateStandardKind CorporateKind
        {
            get { return corporateStandardKind; }
            set { corporateStandardKind = value; }
        }
        public AssetModelUsageKind UserKind
        {
            get { return userKind; }
            set { userKind = value; }
        }

        public string ModelVersion
        {
            get { return modelVersion; }
            set { modelVersion = value; }
        }

        public string ModelNumber
        {
            get { return modelnumber; }
            set { modelnumber = value; }
        }
        public float Weight
        {
            get { return weight; }
            set { weight = value; }
        }

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				ProductAssetModel x = (ProductAssetModel)obj;
				return ((x.userKind == this.userKind) &&
						(x.weight == this.weight) && (x.corporateStandardKind == this.corporateStandardKind) &&
						(x.modelnumber == this.modelnumber) && (x.ModelVersion == this.modelVersion));
			}
			else
			{
				return false;
			}
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		#region IAccess implementation

		public override bool HasProperty(ModelCode property)
		{
			switch (property)
			{
				case ModelCode.PRODUCTASSET_MODEL_NUMBER:
				case ModelCode.PRODUCTASSET_MODEL_STANDARDKIND:
				case ModelCode.PRODUCTASSET_MODEL_USERKIND:
				case ModelCode.PRODUCTASSET_MODEL_VERSION:
				case ModelCode.PRODUCTASSET_MODEL_WEIGHT:

					return true;
				default:
					return base.HasProperty(property);
			}
		}

		public override void GetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.PRODUCTASSET_MODEL_NUMBER:
					property.SetValue(modelnumber);
					break;

				case ModelCode.PRODUCTASSET_MODEL_STANDARDKIND:
					property.SetValue((short)corporateStandardKind);
					break;
				case ModelCode.PRODUCTASSET_MODEL_USERKIND:
					property.SetValue((short)userKind);
					break;

				case ModelCode.PRODUCTASSET_MODEL_VERSION:
					property.SetValue(modelVersion);
					break;
				case ModelCode.PRODUCTASSET_MODEL_WEIGHT:
					property.SetValue(weight);
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
				case ModelCode.PRODUCTASSET_MODEL_NUMBER:
					modelnumber = property.AsString();
					break;

				case ModelCode.PRODUCTASSET_MODEL_STANDARDKIND:
					corporateStandardKind = (CorporateStandardKind)property.AsEnum();
					break;
				case ModelCode.PRODUCTASSET_MODEL_USERKIND:
					userKind = (AssetModelUsageKind)property.AsEnum();
					break;

				case ModelCode.PRODUCTASSET_MODEL_VERSION:
					modelVersion = property.AsString();
					break;
				case ModelCode.PRODUCTASSET_MODEL_WEIGHT:
					weight = property.AsFloat();
					break;
				default:
					base.SetProperty(property);
					break;
			}
		}

		#endregion IAccess implementation
	}
}
