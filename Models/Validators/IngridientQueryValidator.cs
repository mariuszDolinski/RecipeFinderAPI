using FluentValidation;

namespace RecipeFinderAPI.Models.Validators
{
    public class IngridientQueryValidator : AbstractValidator<IngridientQuery>
    {
        private int[] _allowedPageSizes = new int[] { 5, 10, 15 };
        public IngridientQueryValidator()
        {
            RuleFor(iv => iv.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(iv => iv.PageSize).Custom((value, context) =>
            {
                if (!_allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must be from [{string.Join(",",_allowedPageSizes)}]");
                }
            });
        }

    }
}
