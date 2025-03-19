using AdvertisingPlatform.Exceptions;

namespace AdvertisingPlatform
{

    public class LocationNode : Dictionary<string, LocationNode>
    {
        private readonly List<string> PlatformsList;

        public LocationNode()
        {
            PlatformsList = [];
        }

        public void AddPlatformPath(string path, string platform, int ind)
        {
            if (PlatformsList.Contains(platform))
            {
                throw new NestingLocationsException("Некорректные входные данные: обнаружена вложенность путей");
            }

            if (ind == path.Length)
            {
                PlatformsList.Add(platform);
                return;
            }

            int nextInd = path.IndexOf(Symbols.slashSymb, ind + 1);
            if (nextInd == -1) nextInd = path.Length;
            var localName = path[ind..nextInd];

            if (!localName.StartsWith(Symbols.slashSymb))
            {
                throw new WrongLocationStartException("Некорректные входные данные: путь не начинается на @'\'");
            }
            if (localName.Length < 2)
            {
                throw new EmptyPartOfLocationException("Некорректные входные данные: пустой раздел пути");
            }

            var isValue = TryGetValue(localName, out LocationNode newNode);
            if (isValue)
            {
                newNode.AddPlatformPath(path, platform, nextInd);
            }
            else
            {
                var curLocalList = new LocationNode();
                Add(localName, curLocalList);
                curLocalList.AddPlatformPath(path, platform, nextInd);
            }
        }

        public void SeekByLocation(string locationPath, List<string> list, int ind)
        {
            foreach (var plat in PlatformsList)
            {
                list.Add(plat);
            }

            if (ind == locationPath.Length)
            {
                return;
            }

            int nextInd = locationPath.IndexOf(Symbols.slashSymb, ind + 1);
            if (nextInd == -1)
            {
                nextInd = locationPath.Length;
            }

            var isVal = TryGetValue(locationPath[ind..nextInd], out LocationNode node);
            if (isVal)
            {
                node.SeekByLocation(locationPath, list, nextInd);
            }
            else
            {
                list.Clear();
                return;
            }
        }

        public void CompleteList(string line, LocationNode RootNode, List<string> usedPlatforms)
        {
            int fInd = line.IndexOf(Symbols.firstSymbol);
            if (fInd == -1)
            {
                throw new MissingColonException("Некорректные входные данные: двоеточие не найдено");
            }

            var platformName = line[..fInd];
            if (platformName.Length < 1)
            {
                throw new EmptyPlatformNameException("Некорректные входные данные: пустое имя платформы");
            }
            if (usedPlatforms.Contains(platformName))
            {
                throw new PlatformDublicationException("Некорректные входные данные: имена платформ дублируются");
            }

            usedPlatforms.Add(platformName);

            var locationsStr = line[(fInd + 1)..line.Length];

            // ранжирование по возрастанию глубины путей для отслеживания вложенных путей
            var rangedLocations = locationsStr.Split(Symbols.separSymbol)
                .OrderBy(x => x.Count(ch => ch == Symbols.slashSymb));

            foreach (var location in rangedLocations)
            {
                if (location == string.Empty)
                {
                    throw new EmptyLocationException("Некорректные входные данные: пустая строка локации");
                }
                RootNode.AddPlatformPath(location, platformName, 0);
            }
        }

        public void Init(string platformDatasString)
        {
            List<string> Platforms = [];
            foreach (var platformData in platformDatasString.Split("\r\n"))
            {
                CompleteList(platformData, this, Platforms);
            }
        }
    }
}
