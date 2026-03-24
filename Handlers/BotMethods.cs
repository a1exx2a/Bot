using System.Security.Cryptography.X509Certificates;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Test_bot4org;



internal class BotMethods{

    private static string currentState = "Start";
    private static string? from_language;
    private static string? into_language;
    private static string input;

    
    public static async Task HandleUpdateAsync(ITelegramBotClient client, Update update, CancellationToken cancellationToken){
        var me = await client.GetMeAsync();

        var start_replyKeyboard = new ReplyKeyboardMarkup(Reply.start_btn) { ResizeKeyboard = true };


        Console.WriteLine($"Боту {me.FirstName} пришло новое сообщение --> {update.Message?.Text ?? "[не текст]"}");
        
        if (update.Message?.Text == "/start" || update.Message?.Text == "🔄 Перезапуск 🔄")
        {
            await client.SendTextMessageAsync(update.Message?.Chat.Id, $"👋 Привет {update.Message?.From?.FirstName}! Этот бот позволяет переводить сообщения на 3 доступных языка:\n\n🇷🇺 Русский\n🇬🇧 Английский\n🇫🇷 Французский\n\nДоступные команды --> /help", replyMarkup: start_replyKeyboard);
        }
        
        
        else if (update.Message?.Text == "/help" || update.Message?.Text == "📖 Помощь 📖")
        {
            await client.SendTextMessageAsync(update.Message?.Chat.Id, "✨ <b>Доступные команды</b>:\n\n🔄 /start - <b>перезапустить бота</b>\n\n📖 /help - <b>узнать навыки бота</b>\n\n💬 /translation - <b>перевести текст</b>\n\n🗣️ /author - <b>информация об авторе</b>", replyMarkup: start_replyKeyboard, parseMode: ParseMode.Html);
        }
        

        else if ((update.Message.Text == "💬 Перевести 💬"|| update.Message.Text == "/translation") & currentState == "Start" || currentState == "InputText" || currentState == "FromLanguage" || currentState == "IntoLanguage")
        {
            await FSM_btn(client, update, cancellationToken);
        }



        else if (update.Message.Text == "/author" || update.Message.Text == "🗣️ Об авторе 🗣️"){
            var keyboard = new InlineKeyboardMarkup(new[]
            {
                new[]
                {
                    InlineKeyboardButton.WithUrl("Перейти на гитхаб автора", "https://github.com/a1exx2a")
                }
            });
            await client.SendTextMessageAsync(update.Message.Chat.Id, $"<b>👨‍💻 Информация об авторе:</b>\n\n<b>Имя</b>: Александр\n\n<b>Возраст</b>: 16 лет\n\nЗнаком с такими языками программирования, как Python и C#\n\n<b>Контакт</b>: <tg-spoiler><a href='tg://user?id={5830296756}'>Менчик ('''');</a></tg-spoiler>", parseMode: ParseMode.Html, replyMarkup: keyboard);

        }
        else
        {
            await client.SendTextMessageAsync(update.Message?.Chat.Id, "Увы, я вас не понимаю.\nОбратитесь к команде /help");
        }
    }


    public static async Task HandlePollingErrorAsync(ITelegramBotClient client, Exception exception, CancellationToken token)
    {
        Console.WriteLine($"Ошибка: {exception.Message}");
        await Task.CompletedTask;
    }



    public static async Task FSM_btn(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
    {
        var с_replyKeyboard = new ReplyKeyboardMarkup(Reply.from_language) { ResizeKeyboard = true };
        var na_replyKeyboard = new ReplyKeyboardMarkup(Reply.into_language) { ResizeKeyboard = true };


        switch (currentState)
        {
            case "Start":
                await client.SendTextMessageAsync(update.Message?.Chat.Id, "✍️ Введите текст, который хотите перевести: ");

                currentState = "InputText";
                break;
            
            case "InputText":
                input = update.Message.Text;

                if(Config.apiKey != "Тут твой токен от yandex api если его используешь"){
                    
                    await client.SendTextMessageAsync(update.Message?.Chat.Id, "Выберите язык <b>с которого</b> переводим: \n\n✨ Если вы введёте не тот язык на котором написан текст, то он у вас не переведётся!", replyMarkup: с_replyKeyboard, parseMode: ParseMode.Html);
                    currentState = "FromLanguage";

                }
                else
                {
                    await client.SendTextMessageAsync(update.Message.Chat.Id, "Выберите язык <b>на который</b> переводим: \n\n✨ Если вы введёте не правильный язык, то бот возьмёт предыдущий вариант языка для перевода!",replyMarkup: na_replyKeyboard, parseMode: ParseMode.Html);
                    currentState = "IntoLanguage";
                }
                break;
                

            case "FromLanguage":

                
                if (!string.IsNullOrEmpty(update.Message.Text) & currentState == "FromLanguage")
                {

                    
                    if (update.Message.Text.ToLower() == "🇷🇺 русского 🇷🇺")
                    {
                        from_language = "ru";
                    }
                    else if(update.Message.Text.ToLower() == "🇬🇧 английского 🇬🇧"){
                        from_language = "en";

                    }
                    else if(update.Message.Text.ToLower() == "🇫🇷 французского 🇫🇷"){
                        from_language = "fr";
                    }
                    else{
                        await client.SendTextMessageAsync(update.Message.Chat.Id, "Ой! Вы что-то не то нажали.");
                        currentState = "Start";

                    }

                    await client.SendTextMessageAsync(update.Message.Chat.Id, "Выберите язык <b>на который</b> переводим: \n\n✨ Если вы введёте не правильный язык, то бот возьмёт предыдущий вариант языка для перевода!",replyMarkup: na_replyKeyboard, parseMode: ParseMode.Html);
                    currentState = "IntoLanguage";

                }

                else
                {
                    await client.SendTextMessageAsync(update.Message.Chat.Id, "Ой! Вы что-то не то нажали.");
                }
              
                break;

            case "IntoLanguage":
                  
                if (!string.IsNullOrEmpty(update.Message.Text) & currentState == "IntoLanguage")
                {

                    if (update.Message.Text.ToLower() == "🇷🇺 русский 🇷🇺")
                    {
                        into_language = "ru";

                    }
                    else if(update.Message.Text.ToLower() == "🇬🇧 английский 🇬🇧"){
                        into_language = "en";
                    }
                    else if(update.Message.Text.ToLower() == "🇫🇷 французский 🇫🇷"){
                        into_language = "fr";
                    }
                    else{
                        await client.SendTextMessageAsync(update.Message.Chat.Id, "Ой! Вы что-то не то нажали.");

                    }


                    var start_replyKeyboard = new ReplyKeyboardMarkup(Reply.start_btn) { ResizeKeyboard = true };


                    
                    if(string.IsNullOrEmpty(into_language)){

                        await client.SendTextMessageAsync(update.Message.Chat.Id, $"Попробуй снова.",replyMarkup: start_replyKeyboard);
                        currentState = "Start"; 
                        break;
                    }
                    
                    else{
                        currentState = "Start"; 
                        Console.WriteLine($"\nТекст который переводим {input}\nЯзык с которого переводим {from_language}\nЯзык на который переводим {into_language}");

                        if(Config.apiKey != "Тут твой токен от yandex api если его используешь"){
                            string translatedText = await TranslationYa_input.TranslateText(input, from_language, into_language); 
                            await client.SendTextMessageAsync(update.Message.Chat.Id,$"🗣️ Переведенный текст с помощью <b>Yandex Translate</b>: {translatedText}", replyMarkup: start_replyKeyboard, parseMode: ParseMode.Html);
                        }

                        else{
                            string translatedText = await TranslationGo_input.TranslateText(into_language, input); 
                            await client.SendTextMessageAsync(update.Message.Chat.Id,$"🗣️ Переведенный текст с помощью <b>Google Translate</b>: {translatedText}", replyMarkup: start_replyKeyboard, parseMode: ParseMode.Html);
                        }
                    }
                    
                }
                else
                {
                    await client.SendTextMessageAsync(update.Message.Chat.Id, "Ой! Вы что-то не то нажали.");
                }
                
                break;    
                    
        }
    }
    
    
    
}
