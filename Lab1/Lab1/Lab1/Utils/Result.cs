namespace Lab1
{
    class Result
    {
        public string Key { get; set; }
        public string Value { get; set; }

        public Result(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public override string ToString()
        {
            return $"[{Key}]: {Value}";
        }
    }
}
