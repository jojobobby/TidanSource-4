namespace wServer.realm
{
    public class ChatColor
    {
        public static int GetChatColor(int stars, bool Staff = false, bool donator = false)
        {
            if (Staff)
                return 0xFFFF00;
            if (donator)
                return 0xFF0000;
            if (stars <= 14)
                return 0x8997DD;
            else if (stars > 15 && stars <= 29)
                return 0x304CDA;
            else if (stars > 30 && stars <= 44)
                return 0xC0262C;
            else if (stars > 45 && stars <= 59)
                return 0xF6921D;
            else if (stars > 60 && stars <= 74)
                return 0xFFFF00;
            return 0xFFFFFF;
        }
    }
}