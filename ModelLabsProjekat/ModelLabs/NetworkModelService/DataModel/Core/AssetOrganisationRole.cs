using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class AssetOrganisationRole : OrganisationRole
    {
        private List<long> organisationRoles = new List<long>();
        public AssetOrganisationRole(long globalId) : base(globalId)
        {
        }
		public List<long> OrganisationRole
		{
			get
			{
				return organisationRoles;
			}

			set
			{
				organisationRoles = value;
			}
		}

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				AssetOrganisationRole x = (AssetOrganisationRole)obj;
				return ((CompareHelper.CompareLists(x.organisationRoles, this.organisationRoles)));
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
				return organisationRoles.Count != 0 || base.IsReferenced;
			}
		}
		public override bool HasProperty(ModelCode t)
		{
			switch (t)
			{
				case ModelCode.ASSET_ORGANISATION_ROLE_ASSETS:
					return true;

				default:
					return base.HasProperty(t);
			}
		}

		public override void GetProperty(Property prop)
		{
			switch (prop.Id)
			{
				case ModelCode.ASSET_ORGANISATION_ROLE_ASSETS:
					prop.SetValue(OrganisationRole);
					break;
				default:
					base.GetProperty(prop);
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

		#endregion IAccess implementation	

		#region IReference implementation


		public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
		{
			if (organisationRoles != null && organisationRoles.Count > 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
			{
				references[ModelCode.ASSET_ORGANISATION_ROLE_ASSETS] = organisationRoles.GetRange(0, organisationRoles.Count);
			}

			base.GetReferences(references, refType);
		}

		public override void AddReference(ModelCode referenceId, long globalId)
		{
			switch (referenceId)
			{
				case ModelCode.ASSET_ORGANISATIONROLES:
					organisationRoles.Add(globalId);
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
				case ModelCode.ASSET_ORGANISATIONROLES:

					if (organisationRoles.Contains(globalId))
					{
						organisationRoles.Remove(globalId);
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
