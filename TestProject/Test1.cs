using AdvertisingPlatform.Exceptions;
using AdvertisingPlatform;

namespace TestProject
{
    [TestClass]
    public sealed class Test1
    {
        [TestMethod]
        public void TestMethod_Container()
        {
            var contentStr =
                "Яндекс.Директ:/ru" +
                "\r\nРевдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik" +
                "\r\nГазета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl" +
                "\r\nКрутая реклама:/ru/svrd";

            LocationNode RootNode = [];
            RootNode.Init(contentStr);

            List<string> list = [];
            RootNode.SeekByLocation("/ru/msk", list, 0);

            Assert.IsTrue(list.Contains("Яндекс.Директ"));
        }

        [TestMethod]
        public void TestMethod_NoMatch()
        {
            var contentStr = "Яндекс.Директ:/ru";

            LocationNode RootNode = [];
            RootNode.Init(contentStr);

            List<string> list = [];
            RootNode.SeekByLocation("/ru/abcdefg", list, 0);

            Assert.IsTrue(list.Count == 0);
        }

        [TestMethod]
        public void TestMethod_ColonMissingException()
        {
            var contentStr = "Яндекс.Директ/ru";

            LocationNode RootNode = [];
            Assert.Throws<ColonMissingException>(() => RootNode.Init(contentStr));
        }

        [TestMethod]
        public void TestMethod_EmptyLocationException()
        {
            var contentStr = "Яндекс.Директ:,,";

            LocationNode RootNode = [];
            Assert.Throws<EmptyLocationException>(() => RootNode.Init(contentStr));
        }

        [TestMethod]
        public void TestMethod_WrongLocationStartException()
        {
            var contentStr = "Яндекс.Директ:улицаПушкина/домКолотушкина";

            LocationNode RootNode = [];
            Assert.Throws<WrongLocationStartException>(() => RootNode.Init(contentStr));
        }

        [TestMethod]
        public void TestMethod_EmptyPartOfLocationException()
        {
            var contentStr = "Яндекс.Директ:/улицаПушкина/домКолотушкина//";

            LocationNode RootNode = [];
            Assert.Throws<EmptyPartOfLocationException>(() => RootNode.Init(contentStr));
        }

        [TestMethod]
        public void TestMethod_NestingLocationsException()
        {
            var contentStr = "Яндекс.Директ:/улицаПушкина/домКолотушкина,/улицаПушкина/домКолотушкина/домик";

            LocationNode RootNode = [];
            Assert.Throws<NestingLocationsException>(() => RootNode.Init(contentStr));
        }

        [TestMethod]
        public void TestMethod_EmptyPlatformNameException()
        {
            var contentStr = ":/ru";

            LocationNode RootNode = [];
            Assert.Throws<EmptyPlatformNameException>(() => RootNode.Init(contentStr));
        }

        [TestMethod]
        public void TestMethod_PlatformDublicationException()
        {
            var contentStr = "Яндекс:/ru"
                + "\r\nЯндекс:/en";

            LocationNode RootNode = [];
            Assert.Throws<PlatformDublicationException>(() => RootNode.Init(contentStr));
        }
        
    }
}
