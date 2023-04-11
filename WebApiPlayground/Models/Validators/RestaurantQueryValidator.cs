using FluentValidation;
using WebApiPlayground.Entities;

namespace WebApiPlayground.Models.Validators
{
    public class RestaurantQueryValidator : AbstractValidator<RestaurantQuery>
    {
        private readonly string[] _allowedColumnNames = new[]
        {
            nameof(Restaurant.Name),
            nameof(Restaurant.Description),
            nameof(Restaurant.Category),
        };

        private readonly int[] _allowedPageSizes = new[] { 5, 10, 15 };

        public RestaurantQueryValidator()
        {
            RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(x => x.PageSize).Custom((value, context) =>
            {
                if (!_allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize must be in [{string.Join(", ", _allowedPageSizes)}]");
                }
            });
            RuleFor(x => x.SortBy).Custom((value, context) =>
            {
                if (!string.IsNullOrEmpty(value) && !_allowedColumnNames.Contains(value))
                {
                    context.AddFailure("SortBy", $"SortBy must be in [{string.Join(", ", _allowedColumnNames)}]");
                }
            });
        }
    }
}