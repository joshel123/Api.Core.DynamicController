using System;

namespace Api.Core.DynamicController
{
    public class ControllerCreationOptions
    {
        public Type ControllerTemplateType { get; set; }

        public Type ControllerEntityType { get; set; }

        public string ControllerRouting { get; set; }

        public string ObjectFullNamespace { get; set; }

        public Guid ControllerId { get; set; }
    }
}
