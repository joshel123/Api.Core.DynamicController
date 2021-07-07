using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Collections.Generic;
using System.Linq;

namespace Api.Core.DynamicController
{
    public class ControllerRouteConvention : IControllerModelConvention
    {
        private readonly IList<ControllerCreationOptions> _controllerCreationOptions;

        public ControllerRouteConvention(IEnumerable<ControllerCreationOptions> controllerCreationOptions) 
        {
            _controllerCreationOptions = controllerCreationOptions.ToList();
        }

        public void Apply(ControllerModel controller)
        {
            if (controller.ControllerType.IsGenericType)
            {
                var match = _controllerCreationOptions.FirstOrDefault(x =>
                    x.ControllerEntityType.Name == controller.ControllerType.GenericTypeArguments[0].Name
                    && x.ControllerTemplateType.Name == controller.ControllerName);

                if (match != null)
                {
                    controller.Selectors.Add(new SelectorModel
                    {
                        AttributeRouteModel = new AttributeRouteModel(new RouteAttribute(match.ControllerRouting)),
                    });
                }
            }
        }
    }
}
