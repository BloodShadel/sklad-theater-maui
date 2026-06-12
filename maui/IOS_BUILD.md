# Сборка iOS без Mac

Собрать .NET MAUI приложение для iPhone без Mac нельзя локально — Apple требует macOS/Xcode. Но можно собрать IPA в облаке и установить его на iPhone с Windows.

## Варианты (от бесплатного к платному)

### 1. GitHub Actions — самый доступный (бесплатно, но ограниченно)

GitHub предоставляет macOS-раннеры. У бесплатного аккаунта macOS-минуты считаются с множителем ×10 (примерно 200 минут реального macOS-времени в месяц).

Workflow уже добавлен: `.github/workflows/ios-build.yml`

Запуск: **Actions → Build iOS (unsigned IPA) → Run workflow**.

### 2. Codemagic — альтернатива (500 бесплатных минут/мес)

Добавлен `codemagic.yaml` в корне. Зарегистрируйтесь на https://codemagic.io, подключите репозиторий и запустите workflow `ios-build`.

### 3. Установка IPA на iPhone с Windows

Скачайте артефакт из CI и установите через:

- **Sideloadly** (https://sideloadly.io/) — самый простой;
- **AltStore** (https://altstore.io/) — требует companion app.

Оба работают с бесплатным Apple ID. Подпись действует 7 дней, потом приложение нужно будет переустановить.

## Быстрый старт

1. Создайте репозиторий на GitHub.
2. Запушьте проект:
   ```bash
   cd E:/sklad3
   git remote add origin https://github.com/YOUR_USERNAME/sklad-theater.git
   git branch -M main
   git push -u origin main
   ```
3. Откройте **Actions** (или Codemagic) и запустите сборку.
4. Скачайте IPA, установите через Sideloadly:
   - подключите iPhone к Windows по USB;
   - перетащите IPA в Sideloadly;
   - введите Apple ID;
   - на iPhone в `Настройки → Основные → VPN и управление устройством` доверьте сертификат.

## Важные нюансы

- Бесплатный Apple ID подписывает приложение на **7 дней**.
- Сервер использует самоподписанный сертификат. В коде отключена SSL-проверка для теста. Apple может отклонить такое в App Store, но для локального теста через Sideloadly сойдёт.
- Для публикации в App Store нужен платный Apple Developer Program ($99/год) и нормальный SSL-сертификат на сервере.

## Если сборка падает

- Проверьте логи CI.
- Убедитесь, что в `SkladTheater.Maui.csproj` указан корректный `ApplicationId` (`ru.theater.sklad.maui`).
- Для подписи IPA потребуется Apple Developer и secrets; текущий workflow собирает **unsigned IPA**.
