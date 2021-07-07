using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Collections.Generic;
using System.Reflection;

namespace Api.Core.DynamicController
{
    public class ControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        private IEnumerable<ControllerCreationOptions> _apiToGenerate;

        public ControllerFeatureProvider(IEnumerable<ControllerCreationOptions> toGenerate)
        {
            _apiToGenerate = toGenerate;
        }

        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            foreach (var api in _apiToGenerate)
            {                
                feature.Controllers.Add(
                   api.ControllerTemplateType.MakeGenericType(api.ControllerEntityType)
                   .GetTypeInfo());
            }
        }
    }
}
