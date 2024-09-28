namespace Store.Repository.Specifications.ProductSpecs
{
    public class ProductSpecification
    {
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }
        public string Sort { get; set;} 
        public int PageIndex { get; set; } = 1;

        private int _PageSize  = 6;

        private const int MAXPAGESIZE = 50;

        public int PageSize
        {
            get => _PageSize;
            set => _PageSize = (value > MAXPAGESIZE) ? MAXPAGESIZE : value;
        }
        public string? Search
        {
            get => Search;
            set => Search = value?.Trim().ToLower();
        }

        

    }
}
