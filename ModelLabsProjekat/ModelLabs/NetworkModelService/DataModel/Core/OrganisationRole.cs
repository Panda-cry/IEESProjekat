using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class OrganisationRole : IdentifiedObject
    {
        public OrganisationRole(long globalId) : base(globalId)
        {
        }
		#region IAccess implementation

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


		#endregion IAccess implementation	

	}
}
