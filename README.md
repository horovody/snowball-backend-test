# Тестовое Middle .NET Developer

### **Название задания:**

Сервис конвертации валют

---

### **Описание:**

Разработайте REST API для конвертации валют, который предоставляет пользователю возможность:

1. Получать список поддерживаемых валют.
2. Конвертировать сумму из одной валюты в другую.
3. Управлять кешированием курсов валют.

---

### **Функционал:**

1. **Получение списка валют:**
    - Endpoint: `GET /api/currencies`
    - Описание: Возвращает список доступных валют с их кодами (например, USD, EUR, RUB).
2. **Конвертация валют:**
    - Endpoint: `GET /api/convert`
    - Параметры запроса:
        - `fromCurrency` (string) - код исходной валюты.
        - `toCurrency` (string) - код целевой валюты.
        - `amount` (decimal) - сумма для конвертации.
    - Описание: Возвращает результат конвертации суммы из одной валюты в другую.
    - Например: fromCurrency = USD, toCurrency = RUB, amount = 1, Результат - 100. Или fromCurrency = EUR, toCurrency = USD, amount = 100, Результат - 102.94.
3. **Обновление курсов валют (опционально):**
    - Endpoint: `POST /api/currencies/update`
    - Описание: Обновляет кэшированные данные о курсах валют. Данные берутся из API ЦБ РФ.

---

### **Детали реализации:**

1. **Источник данных:**
    
    Данные о курсах валют берутся из API ЦБ РФ. Для экономии времени, очень упрощенный пример реализации сервиса получения данных вы найдёте в файле `CbrService.cs` (там описана модель и запрос на получение данных). Вы можете модифицировать этот сервис на ваше усмотрение или написать свой.
    
2. **Требования:**
    - Реализовать API с использованием **ASP.NET Core Web API**.
    - Использовать кэширование курса валют, чтобы не обращаться к внешнему API каждый раз. Например, кеш обновляется каждые 5 минут. (Обычный  `IMemoryCache` пойдёт, не обязательно заводить для этого БД)
    - Правильная обработка ошибок

### Необязательно, но будет плюсом

- Использование Swagger + документирование API
