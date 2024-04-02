using APLMatchMaker.Server.Models;
using APLMatchMaker.Shared.DTOs.StudentsDTOs;

namespace APLMatchMaker.Server.Services
{
    public class PropertyMappingService : IPropertyMappingService
    {
        // Create a student property dictionary
        private readonly Dictionary<string, PropertyMappingValue> _studentPropertyMapping =
            new(StringComparer.OrdinalIgnoreCase)
            {
                {"Name", new(new[] {"FirstName","LastName"}, false) },
                {"LastName", new(new[] {"LastName", "FirstName"}, false)},
                {"KnowledgeLevel", new(new[] {"KnowledgeLevel"}, false)},
                {"Language", new(new[] { "Language", }, false)},
                {"Nationality", new(new[] {"Nationality"},false)},
            };
        // Create other property dictionaries here



        // List of property mapping dictionaries
        private readonly IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();


        // Constructor
        public PropertyMappingService()
        {
            // Adds the student property dictionary to the list of property mapping dictionaries
            _propertyMappings.Add(new PropertyMapping<StudentForListDTO, ApplicationUser>(
                _studentPropertyMapping));
            // Add other property dictionaries here



        } // End Constructor

        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
        {
            // get matching mapping
            var matchingMapping = _propertyMappings
                .OfType<PropertyMapping<TSource, TDestination>>();

            if (matchingMapping.Count() == 1)
            {
                return matchingMapping.First().MappingDictionary;
            }

            throw new Exception($"Cannot find exact property mapping instance " +
                $"for <{typeof(TSource)},{typeof(TDestination)}");
        }
    }
}
