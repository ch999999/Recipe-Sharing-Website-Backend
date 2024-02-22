namespace RecipeSiteBackend.Validation
{
    public class ValidationError
    {
        public string? ErrorField { get; set; }
        public string? Message { get; set; }    
        public int Index { get; set; }
    }
}
