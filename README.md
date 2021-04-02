# YaCloudKit ![](./Assets/icon-main.png)
<p align="center">
    <img src="./Assets/logo-main.png">
</p>
Набор инструментов для работы с сервисами Yandex.Cloud

[![Build status](https://ci.appveyor.com/api/projects/status/dvorvf11kanlyai1?svg=true)](https://ci.appveyor.com/project/gkurbesov/yacloudkit)
[![Nuget](https://img.shields.io/nuget/v/YaCloudKit.Core?label=Core)](https://www.nuget.org/packages/YaCloudKit.Core)
[![Nuget](https://img.shields.io/nuget/v/YaCloudKit.MQ?label=MQ)](https://www.nuget.org/packages/YaCloudKit.MQ)
[![Nuget](https://img.shields.io/nuget/v/YaCloudKit.TTS?label=TTS)](https://www.nuget.org/packages/YaCloudKit.TTS)
[![Nuget](https://img.shields.io/nuget/v/YaCloudKit.IAM?label=IAM)](https://www.nuget.org/packages/YaCloudKit.IAM)
## Описание
YaCloudKit - это набор инструментов, который позволит взаимодействовать вашему приложению с сервисами облачной платформы Яндекс.Облако.

Поддерживаются различные реализации .NET, которые реализуют .NET Standard 2.0

## Инструменты
В этом разделе представлен список инструментов, которые уже реализованы или находятся в планах на ближайшее будующее.

**Реализованно:**
- [YaCloudKit.Core](./YaCloudKit.Core) - основная библиотека с базовыми классами и типами
- [YaCloudKit.MQ](./YaCloudKit.MQ) - клиент для работы с очередью сообщений Яндекс.Облака
- [YaCloudKit.MQ.Transport](./YaCloudKit.MQ.Transport) - Расширение для клиента очереди сообщений для отправки объектов-сообщений
- [YaCloudKit.TTS](./YaCloudKit.TTS) - клиент SpeechKit для синтеза речи из текста
- [YaCloudKit.IAM](./YaCloudKit.IAM) - клиент сервиса идентификации и контроля доступа к ресурсам в Yandex.Cloud

**Планируемтся:**
- YaCloudKit.STT - клиент SpeechKit для распознования речи
- YaCloudKit.Monitoring - клиент для передачи метрик в Yandex.Cloud Monitoring

## Лицензия
YaCloudKit предоставляется как есть по лицензии MIT. Для получения дополнительной информации см. [LICENSE](./LICENSE).

