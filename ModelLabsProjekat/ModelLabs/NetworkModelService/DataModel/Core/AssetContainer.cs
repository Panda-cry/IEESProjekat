using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class AssetContainer : Asset
    {
        private List<long> seals = new List<long>();
        private List<long> assets = new List<long>();
        public AssetContainer(long globalId) : base(globalId)
        {
        }

        public List<long> Seals
        {
            get { return seals; }
            set { seals = value; }
        }
        public List<long> Assets
        {
            get { return assets; }
            set { assets = value; }
        }
        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                AssetContainer x = (AssetContainer)obj;
                return (CompareHelper.CompareLists(x.assets, this.assets) && CompareHelper.CompareLists(x.seals,this.seals));
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

		public override bool HasProperty(ModelCode t)
		{
			switch (t)
			{
				case ModelCode.ASSET_CONTAINER_ASSETS:
				case ModelCode.ASSET_CONTAINER_SEAL:
					return true;

				default:
					return base.HasProperty(t);

			}
		}

		public override void GetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.ASSET_CONTAINER_ASSETS:
					property.SetValue(assets);
					break;

				case ModelCode.ASSET_CONTAINER_SEAL:
					property.SetValue(seals);
					break;

				default:
					base.GetProperty(property);
					break;
			}
		}
		#endregion

		#region IReference implementation

		public override bool IsReferenced
		{
			get
			{
				return assets.Count != 0 || seals.Count != 0|| base.IsReferenced;
			}
		}

		public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
		{
			

			if (assets != null && assets.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
			{
				references[ModelCode.ASSET_CONTAINER_ASSETS] = assets.GetRange(0, assets.Count);
			}
			if (seals != null && seals.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
			{
				references[ModelCode.ASSET_CONTAINER_SEAL] = seals.GetRange(0, seals.Count);
			}

			base.GetReferences(references, refType);
		}

		public override void AddReference(ModelCode referenceId, long globalId)
		{
			switch (referenceId)
			{
				case ModelCode.ASSET_ASSETCONTAINER:
					assets.Add(globalId);
					break;
				case ModelCode.SEAL_ASSETCONTAINER:
					seals.Add(globalId);
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
				case ModelCode.ASSET_ASSETCONTAINER:

					if (assets.Contains(globalId))
					{
						assets.Remove(globalId);
					}
					else
					{
						CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
					}

					break;
				case ModelCode.SEAL_ASSETCONTAINER:

					if (seals.Contains(globalId))
					{
						seals.Remove(globalId);
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

		#endregion IReference implementation
	}
}
