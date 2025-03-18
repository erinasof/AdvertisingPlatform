namespace AdvertisingPlatform.Services
{
    public class AdvertisingPlatformService
    {
        private static LocationNode LocalsRootNode = [];
        public bool IsReferenceEmpty()
        {
            return LocalsRootNode == null || LocalsRootNode.Count == 0;
        }

        public void RefreshLocationReference(string referenceFileContent) 
        {
            LocationNode LocalsRootNodeCur = [];
            LocalsRootNodeCur.Init(referenceFileContent);           
            LocalsRootNode = LocalsRootNodeCur;
        }

        public IEnumerable<string> SeekByLocation(string location) 
        {
            List<string> list = [];
            LocalsRootNode.SeekByLocation(location, list, 0);
            return [.. list];
        }

    }
}
