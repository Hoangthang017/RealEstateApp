namespace RealEstateApplication.Model
{
    public static class BackupListRE
    {
        public static Container Container { get; set; }

        public static void Clear()
        {
            Container = null;
        }
    }
}
