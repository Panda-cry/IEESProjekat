using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class AssetModel : IdentifiedObject
    {
        private long assetInfo;
        public AssetModel(long globalId) : base(globalId)
        {
        }
        private long AssetInfo
        {
            get { return assetInfo; }
            set { assetInfo = value; }
        }

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				AssetModel x = (AssetModel)obj;
				return (x.assetInfo == this.assetInfo);
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
				case ModelCode.ASSETMODEL_ASSETINFO:
					return true;

				default:
					return base.HasProperty(property);
			}
		}

		public override void GetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.ASSETMODEL_ASSETINFO:
					property.SetValue(assetInfo);
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

				case ModelCode.ASSETMODEL_ASSETINFO:
					assetInfo = property.AsReference();
					break;

				default:
					base.SetProperty(property);
					break;
			}
		}

		#endregion IAccess implementation

		#region IReference implementation

		public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
		{
			if (assetInfo != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
			{
				references[ModelCode.ASSETMODEL_ASSETINFO] = new List<long>();
				references[ModelCode.ASSETMODEL_ASSETINFO].Add(assetInfo);
			}

			base.GetReferences(references, refType);
		}

		#endregion IReference implementation		
	}
}
