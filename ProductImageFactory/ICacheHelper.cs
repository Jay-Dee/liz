namespace ProductImageFactory {
    public interface ICacheHelper<TItem>
    {
        public bool ContainsKey(string key);

        public void Add(string key, TItem value);

        public TItem this[string key] { get; }
    }
}
