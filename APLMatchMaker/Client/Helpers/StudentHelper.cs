namespace APLMatchMaker.Client.Helpers
{
    public static class StudentHelper
    {
        public static string GetKnowledgeLevelString(int knowledgeLevel)
        {
            return knowledgeLevel switch
            {
                0 => "Ej angivet",
                1 => "Röd",
                2 => "Gul",
                3 => "Grön",
                _ => "Okänd",
            };
        }
    }
}
