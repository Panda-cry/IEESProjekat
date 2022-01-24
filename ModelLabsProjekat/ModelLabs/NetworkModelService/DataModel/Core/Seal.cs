using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
   
    public class Seal : IdentifiedObject
    {
        private DateTime appliedDateTime;
        private SealConditionKind condition;
        private SealKind kind;
        private string sealNumber;
		private long assetContainer;
        public Seal(long globalId) : base(globalId)
        {
        }

		public long AssetContaier
        {
            get { return assetContainer; }
            set { assetContainer = value; }
        }
        public DateTime AppliedTime
        {
            get { return appliedDateTime; }
            set { appliedDateTime = value; }
        }

        public SealConditionKind Condition
        {
            get { return condition; }
            set { condition = value; }
        }
        public SealKind Kind
        {
            get { return kind; }
            set { kind = value; }
        }
        public string SealNumber
        {
            get { return sealNumber; }
            set { sealNumber = value; }
        }

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				Seal x = (Seal)obj;
				return (x.sealNumber == this.sealNumber && x.kind == this.kind && x.appliedDateTime == this.appliedDateTime &&
						x.condition == this.condition && x.assetContainer == this.assetContainer);
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
				case ModelCode.SEAL_APPLIEDDATETIME:
				case ModelCode.SEAL_ASSETCONTAINER:
				case ModelCode.SEAL_CONDITION:
				case ModelCode.SEAL_KIND:
				case ModelCode.SEAL_SEALNUMBER:
					return true;

				default:
					return base.HasProperty(t);

			}
		}

		public override void GetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.SEAL_APPLIEDDATETIME:
					property.SetValue(appliedDateTime);
					break;

				case ModelCode.SEAL_KIND:
					property.SetValue((short)kind);
					break;

				case ModelCode.SEAL_CONDITION:
					property.SetValue((short)condition);
					break;

				case ModelCode.SEAL_SEALNUMBER:
					property.SetValue(sealNumber);
					break;

				case ModelCode.SEAL_ASSETCONTAINER:
					property.SetValue(assetContainer);
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
				case ModelCode.SEAL_APPLIEDDATETIME:
					appliedDateTime = property.AsDateTime();
					break;

				case ModelCode.SEAL_ASSETCONTAINER:
					assetContainer = property.AsReference();
					break;

				case ModelCode.SEAL_CONDITION:
					condition = (SealConditionKind)property.AsEnum();
					break;

				case ModelCode.SEAL_KIND:
					kind = (SealKind)property.AsEnum();
					break;

				case ModelCode.SEAL_SEALNUMBER:
					sealNumber = property.AsString();
					break;

				default:
					base.SetProperty(property);
					break;
			}
		}

		#endregion IAccess implementation

		#region IReference implementation

		public override bool IsReferenced
		{
			get
			{
				return  base.IsReferenced;
			}
		}

		public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
		{
			if (assetContainer != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
			{
				references[ModelCode.SEAL_ASSETCONTAINER] = new List<long>();
				references[ModelCode.SEAL_ASSETCONTAINER].Add(assetContainer);
			}

			base.GetReferences(references, refType);
		}

		#endregion IReference implementation
	}
}
