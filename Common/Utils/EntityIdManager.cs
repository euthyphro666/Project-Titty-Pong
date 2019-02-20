namespace Common.Utils
{
    public class EntityIdManager
    {
        private static int CurrentId = 0;
        public static int NextAvailableId => CurrentId++;
    }
}