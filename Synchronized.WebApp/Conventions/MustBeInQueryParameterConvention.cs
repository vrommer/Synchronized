using System;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Synchronized.WebApp.Conventions
{
    public class MustBeInQueryParameterConvention : Attribute, IParameterModelConvention
    {
        public MustBeInQueryParameterConvention()
        {
        }

        public void Apply(ParameterModel model)
        {
            if (model.BindingInfo == null)
            {
                model.BindingInfo = new BindingInfo();
            }
            model.BindingInfo.BindingSource = BindingSource.Query;
        }
    }
}