using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class AssetOwner : AssetOrganisationRole
    {
        public AssetOwner(long globalId) : base(globalId)
        {
        }

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
		public override bool HasProperty(ModelCode property)
		{
			switch (property)
			{
				default:
					return base.HasProperty(property);
			}
		}

		public override void GetProperty(Property property)
		{
			switch (property.Id)
			{
			
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


	}
}
