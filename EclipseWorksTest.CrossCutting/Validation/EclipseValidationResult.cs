namespace EclipseWorksTest.CrossCutting.Validation
{
    public class EclipseValidationResult
    {
        public EclipseValidationResult()
        {
            ErrorList = new List<string>();
        }
        public bool IsValid { get => !ErrorList.Any(); }
        public List<string> ErrorList { get; set; }

        public void AddIf(bool condition, string errorMessage)
        {
            if (condition)
                ErrorList.Add(errorMessage);
        }
    }
}
