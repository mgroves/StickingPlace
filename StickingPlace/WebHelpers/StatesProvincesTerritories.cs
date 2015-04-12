using System.Collections.Specialized;

namespace StickingPlace
{
    public static class StatesProvincesTerritories
    {
        public static OrderedDictionary StatesAndDC
        {
            get
            {
                return new OrderedDictionary
                {
                    {"AL", "Alabama"},
                    {"AK", "Alaska"},
                    {"AZ", "Arizona"},
                    {"AR", "Arkansas"},
                    {"CA", "California"},
                    {"CO", "Colorado"},
                    {"CT", "Connecticut"},
                    {"DE", "Delaware"},
                    {"FL", "Florida"},
                    {"GA", "Georgia"},
                    {"HI", "Hawaii"},
                    {"ID", "Idaho"},
                    {"IL", "Illinois"},
                    {"IN", "Indiana"},
                    {"IA", "Iowa"},
                    {"KS", "Kansas"},
                    {"KY", "Kentucky"},
                    {"LA", "Louisiana"},
                    {"ME", "Maine"},
                    {"MD", "Maryland"},
                    {"MA", "Massachusetts"},
                    {"MI", "Michigan"},
                    {"MN", "Minnesota"},
                    {"MS", "Mississippi"},
                    {"MO", "Missouri"},
                    {"MT", "Montana"},
                    {"NE", "Nebraska"},
                    {"NV", "Nevada"},
                    {"NH", "New Hampshire"},
                    {"NJ", "New Jersey"},
                    {"NM", "New Mexico"},
                    {"NY", "New York"},
                    {"NC", "North Carolina"},
                    {"ND", "North Dakota"},
                    {"OH", "Ohio"},
                    {"OK", "Oklahoma"},
                    {"OR", "Oregon"},
                    {"PA", "Pennsylvania"},
                    {"RI", "Rhode Island"},
                    {"SC", "South Carolina"},
                    {"SD", "South Dakota"},
                    {"TN", "Tennessee"},
                    {"TX", "Texas"},
                    {"UT", "Utah"},
                    {"VT", "Vermont"},
                    {"VA", "Virginia"},
                    {"WA", "Washington"},
                    {"WV", "West Virginia"},
                    {"WI", "Wisconsin"},
                    {"WY", "Wyoming"},
                    {"DC", "District of Columbia"}
                };
            }
        }

        public static OrderedDictionary Provinces
        {
            get
            {
                return new OrderedDictionary
                {
                    {"AB", " Alberta"},
                    {"BC", " British Columbia"},
                    {"MB", " Manitoba"},
                    {"NB", " New Brunswick"},
                    {"NL", " Newfoundland and Labrador"},
                    {"NT", " Northwest Territories"},
                    {"NS", " Nova Scotia"},
                    {"NU", " Nunavut"},
                    {"ON", " Ontario"},
                    {"PE", " Prince Edward Island"},
                    {"QC", " Quebec"},
                    {"SK", " Saskatchewan"},
                    {"YT", " Yukon"}
                };                
            }
        }

        public static OrderedDictionary Territories
        {
            get
            {
                return new OrderedDictionary
                {
                    {"AS", " American Samoa"},
                    {"FM", " Federated States of Micronesia"},
                    {"MH", " Marshall Islands"},
                    {"MP", " Northern Mariana Islands"},
                    {"PW", " Palau"},
                    {"PR", " Puerto Rico"},
                    {"VI", " Virgin Islands"},
                    {"GU", " Guam"}
                };
            }
        }
    }
}