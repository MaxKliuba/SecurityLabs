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

        public string PrivateWrite()
        {
            return $"{HashText};{Salt}";
        }

        public string PublicWrite()
        {
            return $"{HashText};{Salt};{Text}";
        }

        public override string ToString()
        {
            return $"{HashText}\t{Salt}\t{Text}";
        }
    }
}
