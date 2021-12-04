namespace Lab4
{
    class Hash
    {
        public string HashText { get; set; }

        public string Salt { get; set; }

        public string Text { get; set; }

        public Hash(string hashTaxt, string salt, string text)
        {
            HashText = hashTaxt;
            Salt = salt;
            Text = text;
        }

        public string PrivateWrite(char separator = ';')
        {
            return $"{HashText}{separator}{Salt}";
        }

        public string PublicWrite(char separator = ';')
        {
            return $"{HashText}{separator}{Salt}{separator}{Text}";
        }

        public override string ToString()
        {
            return $"{HashText}\t{Salt}\t{Text}";
        }
    }
}
