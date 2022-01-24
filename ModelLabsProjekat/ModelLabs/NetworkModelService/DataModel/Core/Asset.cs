using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class Asset : IdentifiedObject
    {
        private bool acceptanceTest;
        private long assetContainer = 0;
        private long assetInfo = 0;
        private bool critical;
        private string electronicAddress;
        private string initialCondition;
        private float intialLossOfLife;
        private float purchasePrice;
        private string serialNumber;
        private string status;
        private string type;
        private string utcNumber;
		private DateTime lifeCycle;
		private string lotNmber;
		private List<long> roles = new List<long>();
        public Asset(long globalId) : base(globalId)
        {
        }

        public bool AcceptanceTest { get => acceptanceTest; set => acceptanceTest = value; }
        public long AssetContainer { get => assetContainer; set => assetContainer = value; }
        public long AssetInfo { get => assetInfo; set => assetInfo = value; }
        public bool Critical { get => critical; set => critical = value; }
        public string ElectronicAddress { get => electronicAddress; set => electronicAddress = value; }
        public string InitialCondition { get => initialCondition; set => initialCondition = value; }
        public float IntialLossOfLife { get => intialLossOfLife; set => intialLossOfLife = value; }
        public float PurchasePrice { get => purchasePrice; set => purchasePrice = value; }
        public string SerialNumber { get => serialNumber; set => serialNumber = value; }
        public string Status { get => status; set => status = value; }
        public string Type { get => type; set => type = value; }
        public string UtcNumber { get => utcNumber; set => utcNumber = value; }
        public List<long> Roles { get => roles; set => roles = value; }
        public DateTime LifeCycle { get => lifeCycle; set => lifeCycle = value; }
        public string LotNmber { get => lotNmber; set => lotNmber = value; }

        public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				Asset x = (Asset)obj;
				return ((x.assetContainer == this.assetContainer) && (x.assetInfo == this.assetInfo)&& (x.utcNumber == this.utcNumber) && (x.serialNumber == this.serialNumber) &&
					(x.type == this.type) && (x.purchasePrice == this.purchasePrice) &&
					(x.IntialLossOfLife == this.IntialLossOfLife) && (x.InitialCondition == this.InitialCondition) &&
					(x.Critical == this.Critical) && (x.AcceptanceTest == this.acceptanceTest) &&
					(x.electronicAddress == this.electronicAddress) && (x.status == this.status)&&
					(x.lifeCycle == this.lifeCycle) && (x.LotNmber == this.LotNmber) &&
					CompareHelper.CompareLists(x.roles,this.roles));
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

		public override bool IsReferenced
		{
			get
			{
				return roles.Count != 0 || base.IsReferenced;
			}
		}
		public override bool HasProperty(ModelCode property)
		{
			switch (property)
			{
				case ModelCode.ASSET_ASSETINFO:
				case ModelCode.ASSET_ACCEPTENCETEST:
				case ModelCode.ASSET_CRTITICAL:
				case ModelCode.ASSET_ELECTRONICADDRESS:
				case ModelCode.ASSET_INITIALCONDITION:
				case ModelCode.ASSET_IITIALOSTOFLIFE:
				case ModelCode.ASSET_LIFECYCLE:
				case ModelCode.ASSET_LOTNUMBER:
				case ModelCode.ASSET_PURCHASEPRICE:
				case ModelCode.ASSET_SERIALNUMBER:
				case ModelCode.ASSET_STATUS:
				case ModelCode.ASSET_TYPE:
				case ModelCode.ASSET_UTCNUMBER:
				case ModelCode.ASSET_ASSETCONTAINER:
                case ModelCode.ASSET_ORGANISATIONROLES:
					return true;

				default:
					return base.HasProperty(property);
			}
		}

		public override void GetProperty(Property property)
		{
			switch (property.Id)
			{
				case ModelCode.ASSET_ASSETINFO:
					property.SetValue(assetInfo);
					break;

				case ModelCode.ASSET_ACCEPTENCETEST:
					property.SetValue(acceptanceTest);
					break;

				case ModelCode.ASSET_CRTITICAL:
					property.SetValue(critical);
					break;

				case ModelCode.ASSET_ELECTRONICADDRESS:
					property.SetValue(electronicAddress);
					break;

				case ModelCode.ASSET_INITIALCONDITION:
					property.SetValue(InitialCondition);
					break;

				case ModelCode.ASSET_IITIALOSTOFLIFE:
					property.SetValue(intialLossOfLife);
					break;
				case ModelCode.ASSET_PURCHASEPRICE:
					property.SetValue(PurchasePrice);
					break;

				case ModelCode.ASSET_LIFECYCLE:
					property.SetValue(lifeCycle);
					break;

				case ModelCode.ASSET_LOTNUMBER:
					property.SetValue(LotNmber);
					break;

				case ModelCode.ASSET_SERIALNUMBER:
					property.SetValue(serialNumber);
					break;

				case ModelCode.ASSET_STATUS:
					property.SetValue(status);
					break;

				case ModelCode.ASSET_TYPE:
					property.SetValue(type);
					break;

				case ModelCode.ASSET_UTCNUMBER:
					property.SetValue(utcNumber);
					break;

				case ModelCode.ASSET_ASSETCONTAINER:
					property.SetValue(AssetContainer);
					break;

				case ModelCode.ASSET_ORGANISATIONROLES:
					property.SetValue(roles);
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
				case ModelCode.ASSET_ASSETINFO:
					assetInfo = property.AsReference();
					break;

				case ModelCode.ASSET_ACCEPTENCETEST:
					acceptanceTest = property.AsBool();
					break;

				case ModelCode.ASSET_CRTITICAL:
					critical = property.AsBool();
					break;

				case ModelCode.ASSET_ELECTRONICADDRESS:
					electronicAddress = property.AsString();
					break;

				case ModelCode.ASSET_INITIALCONDITION:
					initialCondition = property.AsString();
					break;

				case ModelCode.ASSET_IITIALOSTOFLIFE:
					intialLossOfLife = property.AsFloat();
					break;
				case ModelCode.ASSET_PURCHASEPRICE:
					purchasePrice = property.AsFloat();
					break;

				case ModelCode.ASSET_LIFECYCLE:
					lifeCycle = property.AsDateTime();
					break;

				case ModelCode.ASSET_LOTNUMBER:
					lotNmber = property.AsString();
					break;

				case ModelCode.ASSET_SERIALNUMBER:
					serialNumber = property.AsString();
					break;

				case ModelCode.ASSET_STATUS:
					status = property.AsString();
					break;

				case ModelCode.ASSET_TYPE:
					type = property.AsString();
					break;

				case ModelCode.ASSET_UTCNUMBER:
					utcNumber = property.AsString();
					break;

				case ModelCode.ASSET_ASSETCONTAINER:
					assetContainer=property.AsReference();
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
			if (assetContainer != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
			{
				references[ModelCode.ASSET_ASSETCONTAINER] = new List<long>();
				references[ModelCode.ASSET_ASSETCONTAINER].Add(assetContainer);
			}
			if (assetInfo != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
			{
				references[ModelCode.ASSET_ASSETINFO] = new List<long>();
				references[ModelCode.ASSET_ASSETINFO].Add(assetInfo);
			}
			if (roles != null && roles.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
			{
				references[ModelCode.ASSET_ORGANISATIONROLES] = roles.GetRange(0, roles.Count);
			}


			base.GetReferences(references, refType);
		}

		public override void AddReference(ModelCode referenceId, long globalId)
		{
			switch (referenceId)
			{
				case ModelCode.ASSET_ORGANISATION_ROLE_ASSETS:
					roles.Add(globalId);
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
				case ModelCode.ASSET_ORGANISATION_ROLE_ASSETS:

					if (roles.Contains(globalId))
					{
						roles.Remove(globalId);
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
