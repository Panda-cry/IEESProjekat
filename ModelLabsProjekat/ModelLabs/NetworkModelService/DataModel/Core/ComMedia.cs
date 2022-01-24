using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class ComMedia : Asset
    {
        public ComMedia(long globalId) : base(globalId)
        {
        }
		#region IAccess implementation

		public override bool HasProperty(ModelCode t)
		{
			switch (t)
			{

				default:
					return base.HasProperty(t);
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
