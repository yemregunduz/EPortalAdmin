namespace EPortalAdmin.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ExplorableEndpointAttribute : Attribute
    {
        public string Description { get; set; }
        public ExplorableEndpointAttribute()
        {
            Description = string.Empty;
        }
        public ExplorableEndpointAttribute(string description)
        {
            Description = description;
        }
    }
}
