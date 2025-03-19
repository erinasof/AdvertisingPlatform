# AdvertisingPlatform

После запуска приложения API становится доступно по url - http://localhost:5163/api/v1

В нем реализовано два метода согласно заданию:

1. Загрузка файла-справочника рекламных площадок (в кодировке UTF-8)
   Post http://localhost:5163/api/v1/Advertising/RefreshDictionary
   В теле запроса необходимо передавать файл в бинарном виде (пример в приложенной Postman-коллекции).

2. Поиск доступных рекламных площадок по локации (ниже пример с параметром запроса location)
   Get http://localhost:5163/api/v1/Advertising?location=/ru/svrd/revd

В проекте в каталоге AdvertisingPlatform\Environment для облегчения проверки тестового задания содержится коллекция Postman и тестовый файл-справочник для загрузки.

Должны быть установлены пакеты .NET SDK 8.0 и ASP.NET Core 8.0 Runtime
