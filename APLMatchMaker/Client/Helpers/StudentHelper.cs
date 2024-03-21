namespace APLMatchMaker.Client.Helpers
{
    public static class StudentHelper
    {
        public static string GetKnowledgeLevelString(int knowledgeLevel)
        {
            return knowledgeLevel switch
            {
                0 => "Not set",
                1 => "Red",
                2 => "Yellow",
                3 => "Green",
                _ => "Unknown",
            };
        }
    }
}
