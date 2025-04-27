using Telegram.Bot.Types.ReplyMarkups;

class Reply
{

    public static List<KeyboardButton[]> start_btn = new List<KeyboardButton[]>()
            {
                new KeyboardButton[] { new KeyboardButton("💬 Перевести 💬") },
                new KeyboardButton[] { new KeyboardButton("🗣️ Об авторе 🗣️"), new KeyboardButton("📖 Помощь 📖") },
                new KeyboardButton[] { new KeyboardButton("🔄 Перезапуск 🔄")}
            };


    public static List<KeyboardButton[]> from_language = new List<KeyboardButton[]>()
            {
                new KeyboardButton[] { new KeyboardButton("🇷🇺 Русского 🇷🇺") },
                new KeyboardButton[] { new KeyboardButton("🇬🇧 Английского 🇬🇧") },
                new KeyboardButton[] { new KeyboardButton("🇫🇷 Французского 🇫🇷") }
            };

    
    public static List<KeyboardButton[]> into_language = new List<KeyboardButton[]>()
            {
                new KeyboardButton[] { new KeyboardButton("🇷🇺 Русский 🇷🇺") },
                new KeyboardButton[] { new KeyboardButton("🇬🇧 Английский 🇬🇧") },
                new KeyboardButton[] { new KeyboardButton("🇫🇷 Французский 🇫🇷") }
            };
}