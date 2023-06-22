namespace Insurance.Domain.Helpers
{
    /// <summary>
    /// ErrorMessages.
    /// </summary>
    public static class ErrorMessages
    {
        public static string UNEXPECTED_ERROR = "An unexpected error has occured";
        public static string NO_DATA_FOR_ID = "No data for id: {0}";
        public static string NO_DATA = "No data found";
        public static string CANNOT_FETCH_DATA = "Could not fetch data";
        public static string NOT_FOUND = "{0} not found";
        public static string ID_NULL = "Product Id is null and cannot fetch data";
        public static string SURCHARGE_NULL = "Surcharge is null and cannot calculate data";
        public static string CANNOT_CREATE = "An unexpected error has occured.Cannot add surcharge.";
        public static string CREATE_SURCHARGE = "Surcharge added successfully";
    }
}
