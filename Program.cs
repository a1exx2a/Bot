using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Test_bot4org;
class Program
{
    private static ITelegramBotClient? bot;
     private static ReceiverOptions _receiverOptions;
    static async Task Main()
    {
        bot = new TelegramBotClient(Config.token);
        var me = await bot.GetMeAsync();

         _receiverOptions = new ReceiverOptions 
        {
            AllowedUpdates = new[] 
            {
                UpdateType.Message, 
            },

            ThrowPendingUpdates = true, 
        };
        bot.StartReceiving(updateHandler: BotMethods.HandleUpdateAsync, pollingErrorHandler: BotMethods.HandlePollingErrorAsync, receiverOptions: _receiverOptions, cancellationToken: CancellationToken.None);


        Console.WriteLine($"Бот {me.FirstName} запущен!\n");
        Console.WriteLine($"Информация о боте:\nНазвание бота: {me.FirstName}\nUsername бота: @{me.Username}\n");
        Console.ReadLine();
    }
}
