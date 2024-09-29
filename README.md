## Конфигурация
Для того, чтобы подключиться к RSS-ленте можно использовать 2 способа:
- Передать ссылку на ленту в качестве параметра --h командной строки;
  ```bash
    MonitorElectric.Client --h "[ссылка на rss-ленту]"
  ```
- Передать ссылку на ленту в качестве параметра "RssConnectionString" в конфигурационном файле appsettings.json.
#### Корректный вид строки подключения к RSS:
```
    https://example.com/rss/
```


### FAQ
- *Случайно изменил текст внутри файла appsettings.json и приложение перестало работать. Что мне делать?*<hr/>
    Ответ: Вы можете удалить файл appsettings.json и попробовать запустить приложение. В данном случае оно сформуриует файл заново в корректном формате.
