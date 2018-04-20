using System;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Synchronized.WebApp.Conventions
{
    public class MustBeInPathParameterConvention : Attribute, IParameterModelConvention
    {
        public MustBeInPathParameterConvention()
        {
        }

        public void Apply(ParameterModel model)
        {
            if (model.BindingInfo == null)
            {
                model.BindingInfo = new BindingInfo();
            }
            model.BindingInfo.BindingSource = BindingSource.Path;
        }
    }
}