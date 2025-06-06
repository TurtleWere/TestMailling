# Email Processing System

Проект состоит из backend (ASP.NET Core Web API) и frontend (HTML/JavaScript) для обработки email-сообщений с возможностью валидации и модификации адресов согласно бизнес-правилам.
На выполнение тестового задания ушло 12 часов.

## 📌 Функционал

### Backend (C#)
- Валидация и обработка email-адресов
- Применение бизнес-правил для доменов:
  - Добавление обязательных адресов для доменов
  - Удаление исключенных адресов
- REST API эндпоинт для обработки запросов

### Frontend (JavaScript)
- Форма для ввода данных email
- Валидация формата адресов (поддержка списков через `;`)
- Отображение результатов обработки

## 🛠 Технологии
- **Backend**: ASP.NET Core 6, C#
- **Frontend**: HTML5, CSS3, JavaScript
- **Протокол**: HTTPS, CORS

## ⚙️ Установка и запуск

### Требования
- .NET 6 SDK
- Веб-браузер с поддержкой JavaScript

### Запуск backend
```bash
dotnet run
```
Сервер запустится на https://localhost:7187


### Запуск frontend
Откройте файл index.html в браузере
Или используйте Live Server в VS Code
