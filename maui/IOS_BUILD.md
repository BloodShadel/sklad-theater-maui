# Сборка iOS без Mac

Собрать .NET MAUI приложение для iPhone без Mac нельзя локально — Apple требует macOS/Xcode. Но можно собрать IPA в облаке и установить его на iPhone с Windows.

## Варианты (от бесплатного к платному)

### 1. GitHub Actions — самый доступный (бесплатно, но ограниченно)

GitHub предоставляет macOS-раннеры. У бесплатного аккаунта есть лимит минут (macOS считаются дороже — 1 минута macOS ≈ 10 минут Linux). Для тестовых сборок хватит.

Мы уже добавили workflow: `.github/workflows/ios-build.yml`

Что он делает:
- запускается вручную (`workflow_dispatch`) или при пуше в `main`/`master`;
- устанавливает .NET 8 + MAUI workload;
- собирает unsigned IPA (`ios-arm64`);
- выгружает IPA как артефакт.

### 2. Установка IPA на iPhone с Windows

Скачайте артефакт из GitHub Actions и установите через один из инструментов:

- **Sideloadly** (https://sideloadly.io/) — самый простой;
- **AltStore** (https://altstore.io/) — требует установки companion app.

Оба работают с бесплатным Apple ID. Подпись действует 7 дней, потом приложение нужно будет переустановить.

### 3. Альтернативы

- **Codemagic** (https://codemagic.io/) — 500 бесплатных build-минут/мес на macOS.
- **MacinCloud / MacStadium** — аренда Mac в облаке (от ~$30/мес).
- **AWS EC2 Mac** — почасовая оплата (~$1–2/час), минимум 24 часа.

## Как запустить сборку

1. Создайте репозиторий на GitHub.
2. Запушьте текущий проект:
   ```bash
   git remote add origin https://github.com/YOUR_USERNAME/sklad-theater.git
   git branch -M main
   git push -u origin main
   ```
3. Откройте вкладку **Actions** → выберите **Build iOS (unsigned IPA)** → **Run workflow**.
4. После завершения скачайте артефакт `SkladTheater-iOS-unsigned`.
5. Распакуйте ZIP — внутри `.ipa`.
6. Установите на iPhone через Sideloadly:
   - подключите iPhone к Windows по USB;
   - перетащите IPA в Sideloadly;
   - введите Apple ID;
   - на iPhone в `Настройки → Основные → VPN и управление устройством` доверьте сертификат.

## Важные нюансы

- Для подписи бесплатным Apple ID приложение будет работать 7 дней.
- Сервер использует самоподписанный сертификат. В коде отключена SSL-проверка для теста (через `NSUrlSessionHandler.TrustOverrideForUrl`). Apple может отклонить такое в App Store, но для локального теста через Sideloadly сойдёт.
- Для публикации в App Store нужен платный Apple Developer Program ($99/год) и нормальный SSL-сертификат на сервере.

## Если GitHub Actions не собирает

Откройте логи workflow. Частые проблемы:
- несовпадение версий Xcode и .NET — workflow ставит `latest-stable`;
- ошибки подписи — мы собираем unsigned, поэтому сертификаты не нужны;
- ошибки в XAML/C# — исправьте и запушьте.

## Дальше

- Добавить экраны **Задания**, **Сундуки**, **Чат**.
- Настроить push-уведомления (APNs + сервер).
- Заменить самоподписанный сертификат сервера на нормальный Let's Encrypt.
