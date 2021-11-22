namespace Lab4
{
    class Hash
    {
        public string HashText { get; set; }

        public string Salt { get; set; }

        public Hash(string hashPassword, string salt)
        {
            HashText = hashPassword;
            Salt = salt;
        }

        public override string ToString()
        {
            return $"{HashText};{Salt}";
        }
    }
}
